using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200009A RID: 154
	public static class PathUtilities
	{
		// Token: 0x0600073B RID: 1851 RVA: 0x0002C264 File Offset: 0x0002A464
		public static bool IsPathPossible(GraphNode node1, GraphNode node2)
		{
			return node1.Walkable && node2.Walkable && node1.Area == node2.Area;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0002C288 File Offset: 0x0002A488
		public static bool IsPathPossible(List<GraphNode> nodes)
		{
			if (nodes.Count == 0)
			{
				return true;
			}
			uint area = nodes[0].Area;
			for (int i = 0; i < nodes.Count; i++)
			{
				if (!nodes[i].Walkable || nodes[i].Area != area)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0002C2E0 File Offset: 0x0002A4E0
		public static bool IsPathPossible(List<GraphNode> nodes, int tagMask)
		{
			if (nodes.Count == 0)
			{
				return true;
			}
			if (((tagMask >> (int)nodes[0].Tag) & 1) == 0)
			{
				return false;
			}
			if (!PathUtilities.IsPathPossible(nodes))
			{
				return false;
			}
			List<GraphNode> reachableNodes = PathUtilities.GetReachableNodes(nodes[0], tagMask, null);
			bool flag = true;
			for (int i = 1; i < nodes.Count; i++)
			{
				if (!reachableNodes.Contains(nodes[i]))
				{
					flag = false;
					break;
				}
			}
			ListPool<GraphNode>.Release(ref reachableNodes);
			return flag;
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0002C358 File Offset: 0x0002A558
		public static List<GraphNode> GetReachableNodes(GraphNode seed, int tagMask = -1, Func<GraphNode, bool> filter = null)
		{
			Stack<GraphNode> dfsStack = StackPool<GraphNode>.Claim();
			List<GraphNode> reachable = ListPool<GraphNode>.Claim();
			HashSet<GraphNode> map = new HashSet<GraphNode>();
			Action<GraphNode> action;
			if (tagMask == -1 && filter == null)
			{
				action = delegate(GraphNode node)
				{
					if (node.Walkable && map.Add(node))
					{
						reachable.Add(node);
						dfsStack.Push(node);
					}
				};
			}
			else
			{
				action = delegate(GraphNode node)
				{
					if (node.Walkable && ((tagMask >> (int)node.Tag) & 1) != 0 && map.Add(node))
					{
						if (filter != null && !filter(node))
						{
							return;
						}
						reachable.Add(node);
						dfsStack.Push(node);
					}
				};
			}
			action(seed);
			while (dfsStack.Count > 0)
			{
				dfsStack.Pop().GetConnections(action);
			}
			StackPool<GraphNode>.Release(dfsStack);
			return reachable;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0002C400 File Offset: 0x0002A600
		public static List<GraphNode> BFS(GraphNode seed, int depth, int tagMask = -1, Func<GraphNode, bool> filter = null)
		{
			PathUtilities.BFSQueue = PathUtilities.BFSQueue ?? new Queue<GraphNode>();
			Queue<GraphNode> que = PathUtilities.BFSQueue;
			PathUtilities.BFSMap = PathUtilities.BFSMap ?? new Dictionary<GraphNode, int>();
			Dictionary<GraphNode, int> map = PathUtilities.BFSMap;
			que.Clear();
			map.Clear();
			List<GraphNode> result = ListPool<GraphNode>.Claim();
			int currentDist = -1;
			Action<GraphNode> action;
			if (tagMask == -1)
			{
				action = delegate(GraphNode node)
				{
					if (node.Walkable && !map.ContainsKey(node))
					{
						if (filter != null && !filter(node))
						{
							return;
						}
						map.Add(node, currentDist + 1);
						result.Add(node);
						que.Enqueue(node);
					}
				};
			}
			else
			{
				action = delegate(GraphNode node)
				{
					if (node.Walkable && ((tagMask >> (int)node.Tag) & 1) != 0 && !map.ContainsKey(node))
					{
						if (filter != null && !filter(node))
						{
							return;
						}
						map.Add(node, currentDist + 1);
						result.Add(node);
						que.Enqueue(node);
					}
				};
			}
			action(seed);
			while (que.Count > 0)
			{
				GraphNode graphNode = que.Dequeue();
				currentDist = map[graphNode];
				if (currentDist >= depth)
				{
					break;
				}
				graphNode.GetConnections(action);
			}
			que.Clear();
			map.Clear();
			return result;
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0002C50C File Offset: 0x0002A70C
		public static List<Vector3> GetSpiralPoints(int count, float clearance)
		{
			List<Vector3> list = ListPool<Vector3>.Claim(count);
			float num = clearance / 6.2831855f;
			float num2 = 0f;
			list.Add(PathUtilities.InvoluteOfCircle(num, num2));
			for (int i = 0; i < count; i++)
			{
				Vector3 vector = list[list.Count - 1];
				float num3 = -num2 / 2f + Mathf.Sqrt(num2 * num2 / 4f + 2f * clearance / num);
				float num4 = num2 + num3;
				float num5 = num2 + 2f * num3;
				while (num5 - num4 > 0.01f)
				{
					float num6 = (num4 + num5) / 2f;
					if ((PathUtilities.InvoluteOfCircle(num, num6) - vector).sqrMagnitude < clearance * clearance)
					{
						num4 = num6;
					}
					else
					{
						num5 = num6;
					}
				}
				list.Add(PathUtilities.InvoluteOfCircle(num, num5));
				num2 = num5;
			}
			return list;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0002C5E6 File Offset: 0x0002A7E6
		private static Vector3 InvoluteOfCircle(float a, float t)
		{
			return new Vector3(a * (Mathf.Cos(t) + t * Mathf.Sin(t)), 0f, a * (Mathf.Sin(t) - t * Mathf.Cos(t)));
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0002C614 File Offset: 0x0002A814
		public static void GetPointsAroundPointWorld(Vector3 p, IRaycastableGraph g, List<Vector3> previousPoints, float radius, float clearanceRadius)
		{
			if (previousPoints.Count == 0)
			{
				return;
			}
			Vector3 vector = Vector3.zero;
			for (int i = 0; i < previousPoints.Count; i++)
			{
				vector += previousPoints[i];
			}
			vector /= (float)previousPoints.Count;
			for (int j = 0; j < previousPoints.Count; j++)
			{
				int num = j;
				previousPoints[num] -= vector;
			}
			PathUtilities.GetPointsAroundPoint(p, g, previousPoints, radius, clearanceRadius);
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0002C694 File Offset: 0x0002A894
		public static void GetPointsAroundPoint(Vector3 center, IRaycastableGraph g, List<Vector3> previousPoints, float radius, float clearanceRadius)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			NavGraph navGraph = g as NavGraph;
			if (navGraph == null)
			{
				throw new ArgumentException("g is not a NavGraph");
			}
			NNInfoInternal nearestForce = navGraph.GetNearestForce(center, NNConstraint.Default);
			center = nearestForce.clampedPosition;
			if (nearestForce.node == null)
			{
				return;
			}
			radius = Mathf.Max(radius, 1.4142f * clearanceRadius * Mathf.Sqrt((float)previousPoints.Count));
			clearanceRadius *= clearanceRadius;
			int i = 0;
			while (i < previousPoints.Count)
			{
				Vector3 vector = previousPoints[i];
				float magnitude = vector.magnitude;
				if (magnitude > 0f)
				{
					vector /= magnitude;
				}
				float num = radius;
				vector *= num;
				int num2 = 0;
				Vector3 vector2;
				for (;;)
				{
					vector2 = center + vector;
					GraphHitInfo graphHitInfo;
					if (g.Linecast(center, vector2, out graphHitInfo, null, null))
					{
						if (graphHitInfo.point == Vector3.zero)
						{
							num2++;
							if (num2 > 8)
							{
								goto Block_7;
							}
						}
						else
						{
							vector2 = graphHitInfo.point;
						}
					}
					bool flag = false;
					for (float num3 = 0.1f; num3 <= 1f; num3 += 0.05f)
					{
						Vector3 vector3 = Vector3.Lerp(center, vector2, num3);
						flag = true;
						for (int j = 0; j < i; j++)
						{
							if ((previousPoints[j] - vector3).sqrMagnitude < clearanceRadius)
							{
								flag = false;
								break;
							}
						}
						if (flag || num2 > 8)
						{
							flag = true;
							previousPoints[i] = vector3;
							break;
						}
					}
					if (flag)
					{
						break;
					}
					clearanceRadius *= 0.9f;
					vector = Random.onUnitSphere * Mathf.Lerp(num, radius, (float)(num2 / 5));
					vector.y = 0f;
					num2++;
				}
				IL_0194:
				i++;
				continue;
				Block_7:
				previousPoints[i] = vector2;
				goto IL_0194;
			}
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0002C848 File Offset: 0x0002AA48
		public static List<Vector3> GetPointsOnNodes(List<GraphNode> nodes, int count, float clearanceRadius = 0f)
		{
			if (nodes == null)
			{
				throw new ArgumentNullException("nodes");
			}
			if (nodes.Count == 0)
			{
				throw new ArgumentException("no nodes passed");
			}
			List<Vector3> list = ListPool<Vector3>.Claim(count);
			clearanceRadius *= clearanceRadius;
			if (clearanceRadius > 0f || nodes[0] is TriangleMeshNode || nodes[0] is GridNode)
			{
				List<float> list2 = ListPool<float>.Claim(nodes.Count);
				float num = 0f;
				for (int i = 0; i < nodes.Count; i++)
				{
					float num2 = nodes[i].SurfaceArea();
					num2 += 0.001f;
					num += num2;
					list2.Add(num);
				}
				for (int j = 0; j < count; j++)
				{
					int num3 = 0;
					int num4 = 10;
					bool flag = false;
					while (!flag)
					{
						flag = true;
						if (num3 >= num4)
						{
							clearanceRadius *= 0.80999994f;
							num4 += 10;
							if (num4 > 100)
							{
								clearanceRadius = 0f;
							}
						}
						float num5 = Random.value * num;
						int num6 = list2.BinarySearch(num5);
						if (num6 < 0)
						{
							num6 = ~num6;
						}
						if (num6 >= nodes.Count)
						{
							flag = false;
						}
						else
						{
							Vector3 vector = nodes[num6].RandomPointOnSurface();
							if (clearanceRadius > 0f)
							{
								for (int k = 0; k < list.Count; k++)
								{
									if ((list[k] - vector).sqrMagnitude < clearanceRadius)
									{
										flag = false;
										break;
									}
								}
							}
							if (flag)
							{
								list.Add(vector);
								break;
							}
							num3++;
						}
					}
				}
				ListPool<float>.Release(ref list2);
			}
			else
			{
				for (int l = 0; l < count; l++)
				{
					list.Add(nodes[Random.Range(0, nodes.Count)].RandomPointOnSurface());
				}
			}
			return list;
		}

		// Token: 0x0400041F RID: 1055
		private static Queue<GraphNode> BFSQueue;

		// Token: 0x04000420 RID: 1056
		private static Dictionary<GraphNode, int> BFSMap;
	}
}
