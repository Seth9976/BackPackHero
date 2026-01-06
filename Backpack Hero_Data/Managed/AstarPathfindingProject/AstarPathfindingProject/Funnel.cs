using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000098 RID: 152
	public class Funnel
	{
		// Token: 0x0600072C RID: 1836 RVA: 0x0002B600 File Offset: 0x00029800
		public static List<Funnel.PathPart> SplitIntoParts(Path path)
		{
			List<GraphNode> path2 = path.path;
			List<Funnel.PathPart> list = ListPool<Funnel.PathPart>.Claim();
			if (path2 == null || path2.Count == 0)
			{
				return list;
			}
			for (int i = 0; i < path2.Count; i++)
			{
				if (path2[i] is TriangleMeshNode || path2[i] is GridNodeBase)
				{
					Funnel.PathPart pathPart = default(Funnel.PathPart);
					pathPart.startIndex = i;
					uint graphIndex = path2[i].GraphIndex;
					while (i < path2.Count && (path2[i].GraphIndex == graphIndex || path2[i] is NodeLink3Node))
					{
						i++;
					}
					i--;
					pathPart.endIndex = i;
					if (pathPart.startIndex == 0)
					{
						pathPart.startPoint = path.vectorPath[0];
					}
					else
					{
						pathPart.startPoint = (Vector3)path2[pathPart.startIndex - 1].position;
					}
					if (pathPart.endIndex == path2.Count - 1)
					{
						pathPart.endPoint = path.vectorPath[path.vectorPath.Count - 1];
					}
					else
					{
						pathPart.endPoint = (Vector3)path2[pathPart.endIndex + 1].position;
					}
					list.Add(pathPart);
				}
				else
				{
					if (!(NodeLink2.GetNodeLink(path2[i]) != null))
					{
						throw new Exception("Unsupported node type or null node");
					}
					Funnel.PathPart pathPart2 = default(Funnel.PathPart);
					pathPart2.startIndex = i;
					uint graphIndex2 = path2[i].GraphIndex;
					i++;
					while (i < path2.Count && path2[i].GraphIndex == graphIndex2)
					{
						i++;
					}
					i--;
					if (i - pathPart2.startIndex != 0)
					{
						if (i - pathPart2.startIndex != 1)
						{
							throw new Exception("NodeLink2 link length greater than two (2) nodes. " + (i - pathPart2.startIndex + 1).ToString());
						}
						pathPart2.endIndex = i;
						pathPart2.isLink = true;
						pathPart2.startPoint = (Vector3)path2[pathPart2.startIndex].position;
						pathPart2.endPoint = (Vector3)path2[pathPart2.endIndex].position;
						list.Add(pathPart2);
					}
				}
			}
			return list;
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0002B848 File Offset: 0x00029A48
		public static Funnel.FunnelPortals ConstructFunnelPortals(List<GraphNode> nodes, Funnel.PathPart part)
		{
			if (nodes == null || nodes.Count == 0)
			{
				return new Funnel.FunnelPortals
				{
					left = ListPool<Vector3>.Claim(0),
					right = ListPool<Vector3>.Claim(0)
				};
			}
			if (part.endIndex < part.startIndex || part.startIndex < 0 || part.endIndex > nodes.Count)
			{
				throw new ArgumentOutOfRangeException();
			}
			List<Vector3> list = ListPool<Vector3>.Claim(nodes.Count + 1);
			List<Vector3> list2 = ListPool<Vector3>.Claim(nodes.Count + 1);
			list.Add(part.startPoint);
			list2.Add(part.startPoint);
			for (int i = part.startIndex; i < part.endIndex; i++)
			{
				if (!nodes[i].GetPortal(nodes[i + 1], list, list2, false))
				{
					list.Add((Vector3)nodes[i].position);
					list2.Add((Vector3)nodes[i].position);
					list.Add((Vector3)nodes[i + 1].position);
					list2.Add((Vector3)nodes[i + 1].position);
				}
			}
			list.Add(part.endPoint);
			list2.Add(part.endPoint);
			return new Funnel.FunnelPortals
			{
				left = list,
				right = list2
			};
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0002B9A8 File Offset: 0x00029BA8
		public static void ShrinkPortals(Funnel.FunnelPortals portals, float shrink)
		{
			if (shrink <= 1E-05f)
			{
				return;
			}
			for (int i = 0; i < portals.left.Count; i++)
			{
				Vector3 vector = portals.left[i];
				Vector3 vector2 = portals.right[i];
				float magnitude = (vector - vector2).magnitude;
				if (magnitude > 0f)
				{
					float num = Mathf.Min(shrink / magnitude, 0.4f);
					portals.left[i] = Vector3.Lerp(vector, vector2, num);
					portals.right[i] = Vector3.Lerp(vector, vector2, 1f - num);
				}
			}
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0002BA4C File Offset: 0x00029C4C
		private static bool UnwrapHelper(Vector3 portalStart, Vector3 portalEnd, Vector3 prevPoint, Vector3 nextPoint, ref Quaternion mRot, ref Vector3 mOffset)
		{
			if (VectorMath.IsColinear(portalStart, portalEnd, nextPoint))
			{
				return false;
			}
			Vector3 vector = portalEnd - portalStart;
			float sqrMagnitude = vector.sqrMagnitude;
			prevPoint -= Vector3.Dot(prevPoint - portalStart, vector) / sqrMagnitude * vector;
			nextPoint -= Vector3.Dot(nextPoint - portalStart, vector) / sqrMagnitude * vector;
			Quaternion quaternion = Quaternion.FromToRotation(nextPoint - portalStart, portalStart - prevPoint);
			mOffset += mRot * (portalStart - quaternion * portalStart);
			mRot *= quaternion;
			return true;
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0002BB04 File Offset: 0x00029D04
		public static void Unwrap(Funnel.FunnelPortals funnel, Vector2[] left, Vector2[] right)
		{
			int num = 1;
			Vector3 vector = Vector3.Cross(funnel.right[1] - funnel.left[0], funnel.left[1] - funnel.left[0]);
			while (vector.sqrMagnitude <= 1E-08f && num + 1 < funnel.left.Count)
			{
				num++;
				vector = Vector3.Cross(funnel.right[num] - funnel.left[0], funnel.left[num] - funnel.left[0]);
			}
			left[0] = (right[0] = Vector2.zero);
			Vector3 vector2 = funnel.left[1];
			Vector3 vector3 = funnel.right[1];
			Vector3 vector4 = funnel.left[0];
			Quaternion quaternion = Quaternion.FromToRotation(vector, Vector3.forward);
			Vector3 vector5 = quaternion * -funnel.right[0];
			for (int i = 1; i < funnel.left.Count; i++)
			{
				if (Funnel.UnwrapHelper(vector2, vector3, vector4, funnel.left[i], ref quaternion, ref vector5))
				{
					vector4 = vector2;
					vector2 = funnel.left[i];
				}
				left[i] = quaternion * funnel.left[i] + vector5;
				if (Funnel.UnwrapHelper(vector2, vector3, vector4, funnel.right[i], ref quaternion, ref vector5))
				{
					vector4 = vector3;
					vector3 = funnel.right[i];
				}
				right[i] = quaternion * funnel.right[i] + vector5;
			}
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0002BCE8 File Offset: 0x00029EE8
		private static int FixFunnel(Vector2[] left, Vector2[] right, int numPortals)
		{
			if (numPortals > left.Length || numPortals > right.Length)
			{
				throw new ArgumentException("Arrays do not have as many elements as specified");
			}
			if (numPortals < 3)
			{
				return -1;
			}
			int num = 0;
			while (left[num + 1] == left[num + 2] && right[num + 1] == right[num + 2])
			{
				left[num + 1] = left[num];
				right[num + 1] = right[num];
				num++;
				if (numPortals - num < 3)
				{
					return -1;
				}
			}
			return num;
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0002BD75 File Offset: 0x00029F75
		protected static Vector2 ToXZ(Vector3 p)
		{
			return new Vector2(p.x, p.z);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0002BD88 File Offset: 0x00029F88
		protected static Vector3 FromXZ(Vector2 p)
		{
			return new Vector3(p.x, 0f, p.y);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0002BDA0 File Offset: 0x00029FA0
		protected static bool RightOrColinear(Vector2 a, Vector2 b)
		{
			return a.x * b.y - b.x * a.y <= 0f;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0002BDC7 File Offset: 0x00029FC7
		protected static bool LeftOrColinear(Vector2 a, Vector2 b)
		{
			return a.x * b.y - b.x * a.y >= 0f;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0002BDF0 File Offset: 0x00029FF0
		public static List<Vector3> Calculate(Funnel.FunnelPortals funnel, bool unwrap, bool splitAtEveryPortal)
		{
			if (funnel.left.Count != funnel.right.Count)
			{
				throw new ArgumentException("funnel.left.Count != funnel.right.Count");
			}
			Vector2[] array = ArrayPool<Vector2>.Claim(funnel.left.Count);
			Vector2[] array2 = ArrayPool<Vector2>.Claim(funnel.left.Count);
			if (unwrap)
			{
				Funnel.Unwrap(funnel, array, array2);
			}
			else
			{
				for (int i = 0; i < funnel.left.Count; i++)
				{
					array[i] = Funnel.ToXZ(funnel.left[i]);
					array2[i] = Funnel.ToXZ(funnel.right[i]);
				}
			}
			int num = Funnel.FixFunnel(array, array2, funnel.left.Count);
			List<int> list = ListPool<int>.Claim();
			if (num == -1)
			{
				list.Add(0);
				list.Add(funnel.left.Count - 1);
			}
			else
			{
				bool flag;
				Funnel.Calculate(array, array2, funnel.left.Count, num, list, int.MaxValue, out flag);
			}
			List<Vector3> list2 = ListPool<Vector3>.Claim(list.Count);
			Vector2 vector = array[0];
			int num2 = 0;
			for (int j = 0; j < list.Count; j++)
			{
				int num3 = list[j];
				if (splitAtEveryPortal)
				{
					Vector2 vector2 = ((num3 >= 0) ? array[num3] : array2[-num3]);
					for (int k = num2 + 1; k < Math.Abs(num3); k++)
					{
						float num4;
						if (!VectorMath.LineLineIntersectionFactor(array[k], array2[k] - array[k], vector, vector2 - vector, out num4))
						{
							num4 = 0.5f;
						}
						list2.Add(Vector3.Lerp(funnel.left[k], funnel.right[k], num4));
					}
					num2 = Mathf.Abs(num3);
					vector = vector2;
				}
				if (num3 >= 0)
				{
					list2.Add(funnel.left[num3]);
				}
				else
				{
					list2.Add(funnel.right[-num3]);
				}
			}
			ListPool<int>.Release(ref list);
			ArrayPool<Vector2>.Release(ref array, false);
			ArrayPool<Vector2>.Release(ref array2, false);
			return list2;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0002C020 File Offset: 0x0002A220
		private static void Calculate(Vector2[] left, Vector2[] right, int numPortals, int startIndex, List<int> funnelPath, int maxCorners, out bool lastCorner)
		{
			if (left.Length != right.Length)
			{
				throw new ArgumentException();
			}
			lastCorner = false;
			int num = startIndex + 1;
			int num2 = startIndex + 1;
			Vector2 vector = left[startIndex];
			Vector2 vector2 = left[num2];
			Vector2 vector3 = right[num];
			funnelPath.Add(startIndex);
			int i = startIndex + 2;
			while (i < numPortals)
			{
				if (funnelPath.Count >= maxCorners)
				{
					return;
				}
				if (funnelPath.Count > 2000)
				{
					Debug.LogWarning("Avoiding infinite loop. Remove this check if you have this long paths.");
					break;
				}
				Vector2 vector4 = left[i];
				Vector2 vector5 = right[i];
				if (!Funnel.LeftOrColinear(vector3 - vector, vector5 - vector))
				{
					goto IL_00DD;
				}
				if (vector == vector3 || Funnel.RightOrColinear(vector2 - vector, vector5 - vector))
				{
					vector3 = vector5;
					num = i;
					goto IL_00DD;
				}
				vector3 = (vector = vector2);
				funnelPath.Add(i = (num = num2));
				IL_0134:
				i++;
				continue;
				IL_00DD:
				if (!Funnel.RightOrColinear(vector2 - vector, vector4 - vector))
				{
					goto IL_0134;
				}
				if (vector == vector2 || Funnel.LeftOrColinear(vector3 - vector, vector4 - vector))
				{
					vector2 = vector4;
					num2 = i;
					goto IL_0134;
				}
				vector2 = (vector = vector3);
				funnelPath.Add(-(i = (num2 = num)));
				goto IL_0134;
			}
			lastCorner = true;
			funnelPath.Add(numPortals - 1);
		}

		// Token: 0x02000145 RID: 325
		public struct FunnelPortals
		{
			// Token: 0x04000764 RID: 1892
			public List<Vector3> left;

			// Token: 0x04000765 RID: 1893
			public List<Vector3> right;
		}

		// Token: 0x02000146 RID: 326
		public struct PathPart
		{
			// Token: 0x04000766 RID: 1894
			public int startIndex;

			// Token: 0x04000767 RID: 1895
			public int endIndex;

			// Token: 0x04000768 RID: 1896
			public Vector3 startPoint;

			// Token: 0x04000769 RID: 1897
			public Vector3 endPoint;

			// Token: 0x0400076A RID: 1898
			public bool isLink;
		}
	}
}
