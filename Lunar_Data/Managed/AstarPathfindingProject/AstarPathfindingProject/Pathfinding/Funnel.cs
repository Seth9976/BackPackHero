using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Pathfinding.Util;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200013E RID: 318
	[BurstCompile]
	public static class Funnel
	{
		// Token: 0x0600097C RID: 2428 RVA: 0x0003400C File Offset: 0x0003220C
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
				GraphNode graphNode = path2[i];
				if (graphNode is TriangleMeshNode || graphNode is GridNodeBase)
				{
					int num = i;
					uint graphIndex = graphNode.GraphIndex;
					while (i < path2.Count && (path2[i].GraphIndex == graphIndex || path2[i] is NodeLink3Node))
					{
						i++;
					}
					i--;
					int num2 = i;
					List<Funnel.PathPart> list2 = list;
					Funnel.PathPart pathPart = new Funnel.PathPart
					{
						type = Funnel.PartType.NodeSequence,
						startIndex = num,
						endIndex = num2,
						startPoint = ((num == 0) ? path.vectorPath[0] : ((Vector3)path2[num - 1].position)),
						endPoint = ((num2 == path2.Count - 1) ? path.vectorPath[path.vectorPath.Count - 1] : ((Vector3)path2[num2 + 1].position))
					};
					list2.Add(pathPart);
				}
				else
				{
					if (!(graphNode is LinkNode))
					{
						throw new Exception("Unsupported node type or null node");
					}
					int num3 = i;
					uint graphIndex2 = graphNode.GraphIndex;
					while (i < path2.Count && path2[i].GraphIndex == graphIndex2)
					{
						i++;
					}
					i--;
					if (i - num3 == 0)
					{
						if (num3 > 0 && num3 + 1 < path2.Count && path2[num3 - 1] == path2[num3 + 1])
						{
							path2.RemoveRange(num3, 2);
							i--;
							throw new Exception("Link node connected back to the previous node in the path. This should not happen.");
						}
						path2.RemoveAt(num3);
						i--;
					}
					else
					{
						if (i - num3 != 1)
						{
							throw new Exception("Off mesh link included more than two nodes: " + (i - num3 + 1).ToString());
						}
						List<Funnel.PathPart> list3 = list;
						Funnel.PathPart pathPart = new Funnel.PathPart
						{
							type = Funnel.PartType.OffMeshLink,
							startIndex = num3,
							endIndex = i,
							startPoint = (Vector3)path2[num3].position,
							endPoint = (Vector3)path2[i].position
						};
						list3.Add(pathPart);
					}
				}
			}
			if (list[0].type == Funnel.PartType.OffMeshLink)
			{
				list.RemoveAt(0);
			}
			if (list[list.Count - 1].type == Funnel.PartType.OffMeshLink)
			{
				list.RemoveAt(list.Count - 1);
			}
			return list;
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00034298 File Offset: 0x00032498
		public static void Simplify(List<Funnel.PathPart> parts, ref List<GraphNode> nodes)
		{
			List<GraphNode> list = ListPool<GraphNode>.Claim();
			int i = 0;
			while (i < parts.Count)
			{
				Funnel.PathPart pathPart = parts[i];
				Funnel.PathPart pathPart2 = pathPart;
				pathPart2.startIndex = list.Count;
				if (pathPart.type != Funnel.PartType.NodeSequence)
				{
					goto IL_0074;
				}
				IRaycastableGraph raycastableGraph = nodes[pathPart.startIndex].Graph as IRaycastableGraph;
				if (raycastableGraph == null)
				{
					goto IL_0074;
				}
				Funnel.Simplify(pathPart, raycastableGraph, nodes, list, Path.ZeroTagPenalties, -1);
				pathPart2.endIndex = list.Count - 1;
				parts[i] = pathPart2;
				IL_00B4:
				i++;
				continue;
				IL_0074:
				for (int j = pathPart.startIndex; j <= pathPart.endIndex; j++)
				{
					list.Add(nodes[j]);
				}
				pathPart2.endIndex = list.Count - 1;
				parts[i] = pathPart2;
				goto IL_00B4;
			}
			ListPool<GraphNode>.Release(ref nodes);
			nodes = list;
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00034374 File Offset: 0x00032574
		public static void Simplify(Funnel.PathPart part, IRaycastableGraph graph, List<GraphNode> nodes, List<GraphNode> result, int[] tagPenalties, int traversableTags)
		{
			int num = part.startIndex;
			int endIndex = part.endIndex;
			Vector3 startPoint = part.startPoint;
			Vector3 endPoint = part.endPoint;
			if (graph == null)
			{
				throw new ArgumentNullException("graph");
			}
			if (num > endIndex)
			{
				throw new ArgumentException("start > end");
			}
			GraphHitInfo graphHitInfo;
			if (!graph.Linecast(startPoint, endPoint, out graphHitInfo, null, null) && graphHitInfo.node == nodes[endIndex])
			{
				graph.Linecast(startPoint, endPoint, out graphHitInfo, result, null);
				long num2 = 0L;
				long num3 = 0L;
				for (int i = num; i <= endIndex; i++)
				{
					num2 += (long)((ulong)nodes[i].Penalty + (ulong)((long)tagPenalties[(int)nodes[i].Tag]));
				}
				bool flag = true;
				for (int j = 0; j < result.Count; j++)
				{
					num3 += (long)((ulong)result[j].Penalty + (ulong)((long)tagPenalties[(int)result[j].Tag]));
					flag &= ((traversableTags >> (int)result[j].Tag) & 1) == 1;
				}
				if (flag && (double)num2 * 1.4 * (double)result.Count >= (double)(num3 * (long)(endIndex - num + 1)))
				{
					return;
				}
				result.Clear();
			}
			int num4 = num;
			int num5 = 0;
			while (num5++ <= 1000)
			{
				if (num == endIndex)
				{
					result.Add(nodes[endIndex]);
					return;
				}
				int count = result.Count;
				int k = endIndex + 1;
				int num6 = num + 1;
				bool flag2 = false;
				while (k > num6 + 1)
				{
					int num7 = (k + num6) / 2;
					Vector3 vector = ((num == num4) ? startPoint : ((Vector3)nodes[num].position));
					Vector3 vector2 = ((num7 == endIndex) ? endPoint : ((Vector3)nodes[num7].position));
					GraphHitInfo graphHitInfo2;
					if (graph.Linecast(vector, vector2, out graphHitInfo2, null, null) || graphHitInfo2.node != nodes[num7])
					{
						k = num7;
					}
					else
					{
						flag2 = true;
						num6 = num7;
					}
				}
				if (!flag2)
				{
					result.Add(nodes[num]);
					num = num6;
				}
				else
				{
					Vector3 vector3 = ((num == num4) ? startPoint : ((Vector3)nodes[num].position));
					Vector3 vector4 = ((num6 == endIndex) ? endPoint : ((Vector3)nodes[num6].position));
					GraphHitInfo graphHitInfo3;
					graph.Linecast(vector3, vector4, out graphHitInfo3, result, null);
					long num8 = 0L;
					long num9 = 0L;
					for (int l = num; l <= num6; l++)
					{
						num8 += (long)((ulong)nodes[l].Penalty + (ulong)((long)tagPenalties[(int)nodes[l].Tag]));
					}
					bool flag3 = true;
					for (int m = count; m < result.Count; m++)
					{
						num9 += (long)((ulong)result[m].Penalty + (ulong)((long)tagPenalties[(int)result[m].Tag]));
						flag3 &= ((traversableTags >> (int)result[m].Tag) & 1) == 1;
					}
					if (!flag3 || (double)num8 * 1.4 * (double)(result.Count - count) < (double)(num9 * (long)(num6 - num + 1)) || result[result.Count - 1] != nodes[num6])
					{
						result.RemoveRange(count, result.Count - count);
						result.Add(nodes[num]);
						num++;
					}
					else
					{
						result.RemoveAt(result.Count - 1);
						num = num6;
					}
				}
			}
			Debug.LogError("Was the path really long or have we got cought in an infinite loop?");
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x000346E4 File Offset: 0x000328E4
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
				Vector3 vector;
				Vector3 vector2;
				if (nodes[i].GetPortal(nodes[i + 1], out vector, out vector2))
				{
					list.Add(vector);
					list2.Add(vector2);
				}
				else
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

		// Token: 0x06000980 RID: 2432 RVA: 0x0003485C File Offset: 0x00032A5C
		private static float2 Unwrap(float3 leftPortal, float3 rightPortal, float2 leftUnwrappedPortal, float2 rightUnwrappedPortal, float3 point, float sideMultiplier, float3 projectionAxis)
		{
			leftPortal -= math.dot(leftPortal, projectionAxis);
			rightPortal -= math.dot(rightPortal, projectionAxis);
			point -= math.dot(point, projectionAxis);
			float3 @float = rightPortal - leftPortal;
			float num = 1f / math.lengthsq(@float);
			if (float.IsPositiveInfinity(num))
			{
				return leftUnwrappedPortal + new float2(-math.length(point - leftPortal), 0f);
			}
			float num2 = math.length(math.cross(point - leftPortal, @float)) * num;
			float num3 = math.dot(point - leftPortal, @float) * num;
			if (num2 < 0.002f)
			{
				if (math.abs(num3) < 0.002f)
				{
					return leftUnwrappedPortal;
				}
				if (math.abs(num3 - 1f) < 0.002f)
				{
					return rightUnwrappedPortal;
				}
			}
			float2 float2 = rightUnwrappedPortal - leftUnwrappedPortal;
			float2 float3 = new float2(-float2.y, float2.x);
			return leftUnwrappedPortal + math.mad(float2, num3, float3 * (num2 * sideMultiplier));
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00034967 File Offset: 0x00032B67
		private static bool RightOrColinear(Vector2 a, Vector2 b)
		{
			return a.x * b.y - b.x * a.y <= 0f;
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0003498E File Offset: 0x00032B8E
		private static bool LeftOrColinear(Vector2 a, Vector2 b)
		{
			return a.x * b.y - b.x * a.y >= 0f;
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x000349B8 File Offset: 0x00032BB8
		public unsafe static List<Vector3> Calculate(Funnel.FunnelPortals funnel, bool splitAtEveryPortal)
		{
			Funnel.FunnelState funnelState = new Funnel.FunnelState(funnel, Allocator.Temp);
			float3 @float = *funnelState.leftFunnel.First;
			float3 float2 = *funnelState.leftFunnel.Last;
			funnelState.PopStart();
			funnelState.PopEnd();
			NativeList<float3> nativeList = new NativeList<float3>(Allocator.Temp);
			funnelState.CalculateNextCorners(int.MaxValue, splitAtEveryPortal, @float, float2, nativeList);
			funnelState.Dispose();
			List<Vector3> list = ListPool<Vector3>.Claim(nativeList.Length);
			for (int i = 0; i < nativeList.Length; i++)
			{
				list.Add(nativeList[i]);
			}
			nativeList.Dispose();
			return list;
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x00034A68 File Offset: 0x00032C68
		[BurstCompile]
		private static int Calculate(ref NativeCircularBuffer<float4> unwrappedPortals, ref NativeCircularBuffer<float3> leftPortals, ref NativeCircularBuffer<float3> rightPortals, ref float3 startPoint, ref float3 endPoint, ref UnsafeSpan<int> funnelPath, int maxCorners, ref float3 projectionAxis, out bool lastCorner)
		{
			return Funnel.Calculate_0000090B$BurstDirectCall.Invoke(ref unwrappedPortals, ref leftPortals, ref rightPortals, ref startPoint, ref endPoint, ref funnelPath, maxCorners, ref projectionAxis, out lastCorner);
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00034A88 File Offset: 0x00032C88
		[BurstCompile]
		[MethodImpl(256)]
		public unsafe static int Calculate$BurstManaged(ref NativeCircularBuffer<float4> unwrappedPortals, ref NativeCircularBuffer<float3> leftPortals, ref NativeCircularBuffer<float3> rightPortals, ref float3 startPoint, ref float3 endPoint, ref UnsafeSpan<int> funnelPath, int maxCorners, ref float3 projectionAxis, out bool lastCorner)
		{
			lastCorner = false;
			if (leftPortals.Length <= 0)
			{
				lastCorner = true;
				return 0;
			}
			if (maxCorners <= 0)
			{
				return 0;
			}
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			if (unwrappedPortals.Length == 0)
			{
				unwrappedPortals.PushEnd(new float4(new float2(0f, 0f), new float2(math.length(rightPortals[0] - leftPortals[0]))));
			}
			float2 @float = Funnel.Unwrap(leftPortals[0], rightPortals[0], unwrappedPortals[0].xy, unwrappedPortals[0].zw, startPoint, -1f, projectionAxis);
			float2 float2 = float2.zero;
			float2 float3 = float2.zero;
			int i = 0;
			while (i <= leftPortals.Length)
			{
				float2 float5;
				float2 float4;
				if (i == unwrappedPortals.Length)
				{
					if (i == leftPortals.Length)
					{
						float4 = (float5 = Funnel.Unwrap(leftPortals[i - 1], rightPortals[i - 1], unwrappedPortals[i - 1].xy, unwrappedPortals[i - 1].zw, endPoint, 1f, projectionAxis) - @float);
					}
					else
					{
						float2 float6 = Funnel.Unwrap(leftPortals[i - 1], rightPortals[i - 1], unwrappedPortals[i - 1].xy, unwrappedPortals[i - 1].zw, leftPortals[i], 1f, projectionAxis);
						float2 float7 = Funnel.Unwrap(leftPortals[i], rightPortals[i - 1], float6, unwrappedPortals[i - 1].zw, rightPortals[i], 1f, projectionAxis);
						unwrappedPortals.PushEnd(new float4(float6, float7));
						float5 = float6 - @float;
						float4 = float7 - @float;
					}
				}
				else
				{
					float5 = unwrappedPortals[i].xy - @float;
					float4 = unwrappedPortals[i].zw - @float;
				}
				if (!Funnel.LeftOrColinear(float3, float4))
				{
					goto IL_0296;
				}
				if (Funnel.RightOrColinear(float2, float4))
				{
					float3 = float4;
					num = i;
					goto IL_0296;
				}
				float2 = (float3 = float2.zero);
				int num4 = (i = (num = num2));
				@float = unwrappedPortals[i].xy;
				*funnelPath[num3++] = num4;
				if (num3 >= maxCorners)
				{
					return num3;
				}
				IL_0308:
				i++;
				continue;
				IL_0296:
				if (!Funnel.RightOrColinear(float2, float5))
				{
					goto IL_0308;
				}
				if (Funnel.LeftOrColinear(float3, float5))
				{
					float2 = float5;
					num2 = i;
					goto IL_0308;
				}
				float2 = (float3 = float2.zero);
				num4 = (i = (num2 = num));
				@float = unwrappedPortals[i].zw;
				*funnelPath[num3++] = num4 | 1073741824;
				if (num3 >= maxCorners)
				{
					return num3;
				}
				goto IL_0308;
			}
			lastCorner = true;
			return num3;
		}

		// Token: 0x04000685 RID: 1669
		public const int RightSideBit = 1073741824;

		// Token: 0x04000686 RID: 1670
		public const int FunnelPortalIndexMask = 1073741823;

		// Token: 0x0200013F RID: 319
		public struct FunnelPortals
		{
			// Token: 0x04000687 RID: 1671
			public List<Vector3> left;

			// Token: 0x04000688 RID: 1672
			public List<Vector3> right;
		}

		// Token: 0x02000140 RID: 320
		public enum PartType
		{
			// Token: 0x0400068A RID: 1674
			OffMeshLink,
			// Token: 0x0400068B RID: 1675
			NodeSequence
		}

		// Token: 0x02000141 RID: 321
		public struct PathPart
		{
			// Token: 0x0400068C RID: 1676
			public int startIndex;

			// Token: 0x0400068D RID: 1677
			public int endIndex;

			// Token: 0x0400068E RID: 1678
			public Vector3 startPoint;

			// Token: 0x0400068F RID: 1679
			public Vector3 endPoint;

			// Token: 0x04000690 RID: 1680
			public Funnel.PartType type;
		}

		// Token: 0x02000142 RID: 322
		[BurstCompile]
		public struct FunnelState
		{
			// Token: 0x06000986 RID: 2438 RVA: 0x00034DB8 File Offset: 0x00032FB8
			public FunnelState(int initialCapacity, Allocator allocator)
			{
				this.leftFunnel = new NativeCircularBuffer<float3>(initialCapacity, allocator);
				this.rightFunnel = new NativeCircularBuffer<float3>(initialCapacity, allocator);
				this.unwrappedPortals = new NativeCircularBuffer<float4>(initialCapacity, allocator);
				this.projectionAxis = float3.zero;
			}

			// Token: 0x06000987 RID: 2439 RVA: 0x00034E08 File Offset: 0x00033008
			public FunnelState(Funnel.FunnelPortals portals, Allocator allocator)
			{
				this = new Funnel.FunnelState(portals.left.Count, allocator);
				if (portals.left.Count != portals.right.Count)
				{
					throw new ArgumentException("portals.left.Count != portals.right.Count");
				}
				for (int i = 0; i < portals.left.Count; i++)
				{
					this.PushEnd(portals.left[i], portals.right[i]);
				}
			}

			// Token: 0x06000988 RID: 2440 RVA: 0x00034E80 File Offset: 0x00033080
			public Funnel.FunnelState Clone()
			{
				return new Funnel.FunnelState
				{
					leftFunnel = this.leftFunnel.Clone(),
					rightFunnel = this.rightFunnel.Clone(),
					unwrappedPortals = this.unwrappedPortals.Clone(),
					projectionAxis = this.projectionAxis
				};
			}

			// Token: 0x06000989 RID: 2441 RVA: 0x00034ED9 File Offset: 0x000330D9
			public void Clear()
			{
				this.leftFunnel.Clear();
				this.rightFunnel.Clear();
				this.unwrappedPortals.Clear();
				this.projectionAxis = float3.zero;
			}

			// Token: 0x0600098A RID: 2442 RVA: 0x00034F07 File Offset: 0x00033107
			public void PopStart()
			{
				this.leftFunnel.PopStart();
				this.rightFunnel.PopStart();
				if (this.unwrappedPortals.Length > 0)
				{
					this.unwrappedPortals.PopStart();
				}
			}

			// Token: 0x0600098B RID: 2443 RVA: 0x00034F3B File Offset: 0x0003313B
			public void PopEnd()
			{
				this.leftFunnel.PopEnd();
				this.rightFunnel.PopEnd();
				this.unwrappedPortals.TrimTo(this.leftFunnel.Length);
			}

			// Token: 0x0600098C RID: 2444 RVA: 0x00034F6B File Offset: 0x0003316B
			public void Pop(bool fromStart)
			{
				if (fromStart)
				{
					this.PopStart();
					return;
				}
				this.PopEnd();
			}

			// Token: 0x0600098D RID: 2445 RVA: 0x00034F7D File Offset: 0x0003317D
			public void PushStart(float3 newLeftPortal, float3 newRightPortal)
			{
				Funnel.FunnelState.PushStart(ref this.leftFunnel, ref this.rightFunnel, ref this.unwrappedPortals, ref newLeftPortal, ref newRightPortal, ref this.projectionAxis);
			}

			// Token: 0x0600098E RID: 2446 RVA: 0x00034FA0 File Offset: 0x000331A0
			private static bool DifferentSidesOfLine(float3 start, float3 end, float3 a, float3 b)
			{
				float3 @float = math.normalizesafe(end - start, default(float3));
				float3 float2 = a - start;
				float3 float3 = b - start;
				float2 -= @float * math.dot(float2, @float);
				float3 -= @float * math.dot(float3, @float);
				return math.dot(float2, float3) < 0f;
			}

			// Token: 0x0600098F RID: 2447 RVA: 0x0003500C File Offset: 0x0003320C
			public unsafe bool IsReasonableToPopStart(float3 startPoint, float3 endPoint)
			{
				if (this.leftFunnel.Length == 0)
				{
					return false;
				}
				int num = 1;
				while (num < this.leftFunnel.Length && VectorMath.IsColinear(*this.leftFunnel.First, *this.rightFunnel.First, this.leftFunnel[num]))
				{
					num++;
				}
				return !Funnel.FunnelState.DifferentSidesOfLine(*this.leftFunnel.First, *this.rightFunnel.First, startPoint, (num < this.leftFunnel.Length) ? this.leftFunnel[num] : endPoint);
			}

			// Token: 0x06000990 RID: 2448 RVA: 0x000350C8 File Offset: 0x000332C8
			public unsafe bool IsReasonableToPopEnd(float3 startPoint, float3 endPoint)
			{
				if (this.leftFunnel.Length == 0)
				{
					return false;
				}
				int num = this.leftFunnel.Length - 1;
				while (num >= 0 && VectorMath.IsColinear(*this.leftFunnel.Last, *this.rightFunnel.Last, this.leftFunnel[num]))
				{
					num--;
				}
				return !Funnel.FunnelState.DifferentSidesOfLine(*this.leftFunnel.Last, *this.rightFunnel.Last, endPoint, (num >= 0) ? this.leftFunnel[num] : startPoint);
			}

			// Token: 0x06000991 RID: 2449 RVA: 0x0003517A File Offset: 0x0003337A
			[BurstCompile]
			private static void PushStart(ref NativeCircularBuffer<float3> leftPortals, ref NativeCircularBuffer<float3> rightPortals, ref NativeCircularBuffer<float4> unwrappedPortals, ref float3 newLeftPortal, ref float3 newRightPortal, ref float3 projectionAxis)
			{
				Funnel.FunnelState.PushStart_00000917$BurstDirectCall.Invoke(ref leftPortals, ref rightPortals, ref unwrappedPortals, ref newLeftPortal, ref newRightPortal, ref projectionAxis);
			}

			// Token: 0x06000992 RID: 2450 RVA: 0x00035189 File Offset: 0x00033389
			public void Splice(int startIndex, int toRemove, List<float3> newLeftPortal, List<float3> newRightPortal)
			{
				this.leftFunnel.Splice(startIndex, toRemove, newLeftPortal);
				this.rightFunnel.Splice(startIndex, toRemove, newRightPortal);
				this.unwrappedPortals.TrimTo(startIndex);
			}

			// Token: 0x06000993 RID: 2451 RVA: 0x000351B4 File Offset: 0x000333B4
			public void PushEnd(Vector3 newLeftPortal, Vector3 newRightPortal)
			{
				this.leftFunnel.PushEnd(newLeftPortal);
				this.rightFunnel.PushEnd(newRightPortal);
			}

			// Token: 0x06000994 RID: 2452 RVA: 0x000351D8 File Offset: 0x000333D8
			public void Push(bool toStart, Vector3 newLeftPortal, Vector3 newRightPortal)
			{
				if (toStart)
				{
					this.PushStart(newLeftPortal, newRightPortal);
					return;
				}
				this.PushEnd(newLeftPortal, newRightPortal);
			}

			// Token: 0x06000995 RID: 2453 RVA: 0x000351F8 File Offset: 0x000333F8
			public void Dispose()
			{
				this.leftFunnel.Dispose();
				this.rightFunnel.Dispose();
				this.unwrappedPortals.Dispose();
			}

			// Token: 0x06000996 RID: 2454 RVA: 0x0003521C File Offset: 0x0003341C
			public int CalculateNextCornerIndices(int maxCorners, NativeArray<int> result, float3 startPoint, float3 endPoint, out bool lastCorner)
			{
				if (result.Length < math.min(maxCorners, this.leftFunnel.Length))
				{
					throw new ArgumentException("result array may not be large enough to hold all corners");
				}
				UnsafeSpan<int> unsafeSpan = result.AsUnsafeSpan<int>();
				return Funnel.Calculate(ref this.unwrappedPortals, ref this.leftFunnel, ref this.rightFunnel, ref startPoint, ref endPoint, ref unsafeSpan, maxCorners, ref this.projectionAxis, out lastCorner);
			}

			// Token: 0x06000997 RID: 2455 RVA: 0x0003527C File Offset: 0x0003347C
			public void CalculateNextCorners(int maxCorners, bool splitAtEveryPortal, float3 startPoint, float3 endPoint, NativeList<float3> result)
			{
				NativeArray<int> nativeArray = new NativeArray<int>(math.min(maxCorners, this.leftFunnel.Length), Allocator.Temp, NativeArrayOptions.ClearMemory);
				bool flag;
				int num = this.CalculateNextCornerIndices(maxCorners, nativeArray, startPoint, endPoint, out flag);
				this.ConvertCornerIndicesToPath(nativeArray, num, splitAtEveryPortal, startPoint, endPoint, flag, result);
				nativeArray.Dispose();
			}

			// Token: 0x06000998 RID: 2456 RVA: 0x000352C8 File Offset: 0x000334C8
			public unsafe void ConvertCornerIndicesToPath(NativeArray<int> indices, int numCorners, bool splitAtEveryPortal, float3 startPoint, float3 endPoint, bool lastCorner, NativeList<float3> result)
			{
				if (result.Capacity < numCorners)
				{
					result.Capacity = numCorners;
				}
				result.Add(in startPoint);
				if (this.leftFunnel.Length == 0)
				{
					if (lastCorner)
					{
						result.Add(in endPoint);
					}
					return;
				}
				if (splitAtEveryPortal)
				{
					float2 @float = Funnel.Unwrap(this.leftFunnel[0], this.rightFunnel[0], this.unwrappedPortals[0].xy, this.unwrappedPortals[0].zw, startPoint, -1f, this.projectionAxis);
					int num = 0;
					for (int i = 0; i < numCorners; i++)
					{
						int num2 = indices[i] & 1073741823;
						bool flag = (indices[i] & 1073741824) != 0;
						float2 float2 = (flag ? this.unwrappedPortals[num2].zw : this.unwrappedPortals[num2].xy);
						Funnel.FunnelState.CalculatePortalIntersections(num + 1, num2 - 1, this.leftFunnel, this.rightFunnel, this.unwrappedPortals, @float, float2, result);
						num = math.abs(num2);
						@float = float2;
						float3 float3 = (flag ? this.rightFunnel[num2] : this.leftFunnel[num2]);
						result.Add(in float3);
					}
					if (lastCorner)
					{
						float2 float4 = Funnel.Unwrap(*this.leftFunnel.Last, *this.rightFunnel.Last, this.unwrappedPortals.Last.xy, this.unwrappedPortals.Last.zw, endPoint, 1f, this.projectionAxis);
						Funnel.FunnelState.CalculatePortalIntersections(num + 1, this.unwrappedPortals.Length - 1, this.leftFunnel, this.rightFunnel, this.unwrappedPortals, @float, float4, result);
						result.Add(in endPoint);
						return;
					}
				}
				else
				{
					for (int j = 0; j < numCorners; j++)
					{
						int num3 = indices[j];
						float3 float3 = (((num3 & 1073741824) != 0) ? this.rightFunnel[num3 & 1073741823] : this.leftFunnel[num3 & 1073741823]);
						result.Add(in float3);
					}
					if (lastCorner)
					{
						result.Add(in endPoint);
					}
				}
			}

			// Token: 0x06000999 RID: 2457 RVA: 0x0003551C File Offset: 0x0003371C
			public void ConvertCornerIndicesToPathProjected(UnsafeSpan<int> indices, bool splitAtEveryPortal, float3 startPoint, float3 endPoint, bool lastCorner, NativeList<float3> result, float3 up)
			{
				int num = indices.Length + 1 + (lastCorner ? 1 : 0);
				if (result.Capacity < num)
				{
					result.Capacity = num;
				}
				result.ResizeUninitialized(num);
				UnsafeSpan<float3> unsafeSpan = result.AsUnsafeSpan<float3>();
				Funnel.FunnelState.ConvertCornerIndicesToPathProjected(ref this, ref indices, splitAtEveryPortal, in startPoint, in endPoint, lastCorner, in this.projectionAxis, ref unsafeSpan, in up);
			}

			// Token: 0x0600099A RID: 2458 RVA: 0x00035578 File Offset: 0x00033778
			public float4x3 UnwrappedPortalsToWorldMatrix(float3 up)
			{
				int num = 0;
				while (num < this.unwrappedPortals.Length && math.lengthsq(this.unwrappedPortals[num].xy - this.unwrappedPortals[num].zw) <= 1E-05f)
				{
					num++;
				}
				if (num >= this.unwrappedPortals.Length)
				{
					return new float4x3(1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 0f, 1f);
				}
				float2 xy = this.unwrappedPortals[num].xy;
				float2 zw = this.unwrappedPortals[num].zw;
				float3 @float = this.leftFunnel[num];
				float3 float2 = this.rightFunnel[num];
				float2 float3 = zw - xy;
				float3 float4 = float2 - @float;
				float2 float5 = float3 * math.rcp(math.lengthsq(float3));
				float2x2 float2x = new float2x2(new float2(float5.x, -float5.y), new float2(float5.y, float5.x));
				float2 float6 = math.mul(float2x, -xy);
				float4x3 float4x = new float4x3(new float4(float2x.c0.x, 0f, float2x.c0.y, 0f), new float4(float2x.c1.x, 0f, float2x.c1.y, 0f), new float4(float6.x, 0f, float6.y, 1f));
				return math.mul(new float4x4(new float4(float4, 0f), new float4(up, 0f), new float4(math.cross(float4, up), 0f), new float4(@float, 1f)), float4x);
			}

			// Token: 0x0600099B RID: 2459 RVA: 0x00035780 File Offset: 0x00033980
			[BurstCompile]
			public static void ConvertCornerIndicesToPathProjected(ref Funnel.FunnelState funnelState, ref UnsafeSpan<int> indices, bool splitAtEveryPortal, in float3 startPoint, in float3 endPoint, bool lastCorner, in float3 projectionAxis, ref UnsafeSpan<float3> result, in float3 up)
			{
				Funnel.FunnelState.ConvertCornerIndicesToPathProjected_00000921$BurstDirectCall.Invoke(ref funnelState, ref indices, splitAtEveryPortal, in startPoint, in endPoint, lastCorner, in projectionAxis, ref result, in up);
			}

			// Token: 0x0600099C RID: 2460 RVA: 0x000357A0 File Offset: 0x000339A0
			private static void CalculatePortalIntersections(int startIndex, int endIndex, NativeCircularBuffer<float3> leftPortals, NativeCircularBuffer<float3> rightPortals, NativeCircularBuffer<float4> unwrappedPortals, float2 from, float2 to, NativeList<float3> result)
			{
				for (int i = startIndex; i < endIndex; i++)
				{
					float4 @float = unwrappedPortals[i];
					float2 xy = @float.xy;
					float2 zw = @float.zw;
					float num;
					if (!VectorMath.LineLineIntersectionFactor(xy, zw - xy, from, to - from, out num))
					{
						num = 0.5f;
					}
					float3 float2 = math.lerp(leftPortals[i], rightPortals[i], num);
					result.Add(in float2);
				}
			}

			// Token: 0x0600099D RID: 2461 RVA: 0x00035818 File Offset: 0x00033A18
			[BurstCompile]
			[MethodImpl(256)]
			public unsafe static void PushStart$BurstManaged(ref NativeCircularBuffer<float3> leftPortals, ref NativeCircularBuffer<float3> rightPortals, ref NativeCircularBuffer<float4> unwrappedPortals, ref float3 newLeftPortal, ref float3 newRightPortal, ref float3 projectionAxis)
			{
				if (unwrappedPortals.Length == 0)
				{
					leftPortals.PushStart(newLeftPortal);
					rightPortals.PushStart(newRightPortal);
					return;
				}
				float4 @float = *unwrappedPortals.First;
				float2 float2 = Funnel.Unwrap(*leftPortals.First, *rightPortals.First, @float.xy, @float.zw, newRightPortal, -1f, projectionAxis);
				float2 float3 = Funnel.Unwrap(*leftPortals.First, newRightPortal, @float.xy, float2, newLeftPortal, -1f, projectionAxis);
				leftPortals.PushStart(newLeftPortal);
				rightPortals.PushStart(newRightPortal);
				unwrappedPortals.PushStart(new float4(float3, float2));
			}

			// Token: 0x0600099E RID: 2462 RVA: 0x000358E8 File Offset: 0x00033AE8
			[BurstCompile]
			[MethodImpl(256)]
			public unsafe static void ConvertCornerIndicesToPathProjected$BurstManaged(ref Funnel.FunnelState funnelState, ref UnsafeSpan<int> indices, bool splitAtEveryPortal, in float3 startPoint, in float3 endPoint, bool lastCorner, in float3 projectionAxis, ref UnsafeSpan<float3> result, in float3 up)
			{
				int num = 0;
				*result[num++] = startPoint;
				if (funnelState.leftFunnel.Length == 0)
				{
					if (lastCorner)
					{
						*result[num++] = endPoint;
					}
					return;
				}
				float4x3 float4x = funnelState.UnwrappedPortalsToWorldMatrix(up);
				if (splitAtEveryPortal)
				{
					throw new NotImplementedException();
				}
				for (int i = 0; i < indices.Length; i++)
				{
					int num2 = *indices[i];
					float2 @float = (((num2 & 1073741824) != 0) ? funnelState.unwrappedPortals[num2 & 1073741823].zw : funnelState.unwrappedPortals[num2 & 1073741823].xy);
					*result[num++] = math.mul(float4x, new float3(@float, 1f)).xyz;
				}
				if (lastCorner)
				{
					float2 float2 = Funnel.Unwrap(*funnelState.leftFunnel.Last, *funnelState.rightFunnel.Last, funnelState.unwrappedPortals.Last.xy, funnelState.unwrappedPortals.Last.zw, endPoint, 1f, projectionAxis);
					*result[num++] = math.mul(float4x, new float3(float2, 1f)).xyz;
				}
			}

			// Token: 0x04000691 RID: 1681
			public NativeCircularBuffer<float3> leftFunnel;

			// Token: 0x04000692 RID: 1682
			public NativeCircularBuffer<float3> rightFunnel;

			// Token: 0x04000693 RID: 1683
			public NativeCircularBuffer<float4> unwrappedPortals;

			// Token: 0x04000694 RID: 1684
			public float3 projectionAxis;

			// Token: 0x02000143 RID: 323
			// (Invoke) Token: 0x060009A0 RID: 2464
			public delegate void PushStart_00000917$PostfixBurstDelegate(ref NativeCircularBuffer<float3> leftPortals, ref NativeCircularBuffer<float3> rightPortals, ref NativeCircularBuffer<float4> unwrappedPortals, ref float3 newLeftPortal, ref float3 newRightPortal, ref float3 projectionAxis);

			// Token: 0x02000144 RID: 324
			internal static class PushStart_00000917$BurstDirectCall
			{
				// Token: 0x060009A3 RID: 2467 RVA: 0x00035A68 File Offset: 0x00033C68
				[BurstDiscard]
				private static void GetFunctionPointerDiscard(ref IntPtr A_0)
				{
					if (Funnel.FunnelState.PushStart_00000917$BurstDirectCall.Pointer == 0)
					{
						Funnel.FunnelState.PushStart_00000917$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(Funnel.FunnelState.PushStart_00000917$BurstDirectCall.DeferredCompilation, methodof(Funnel.FunnelState.PushStart$BurstManaged(ref NativeCircularBuffer<float3>, ref NativeCircularBuffer<float3>, ref NativeCircularBuffer<float4>, ref float3, ref float3, ref float3)).MethodHandle, typeof(Funnel.FunnelState.PushStart_00000917$PostfixBurstDelegate).TypeHandle);
					}
					A_0 = Funnel.FunnelState.PushStart_00000917$BurstDirectCall.Pointer;
				}

				// Token: 0x060009A4 RID: 2468 RVA: 0x00035A94 File Offset: 0x00033C94
				private static IntPtr GetFunctionPointer()
				{
					IntPtr intPtr = (IntPtr)0;
					Funnel.FunnelState.PushStart_00000917$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
					return intPtr;
				}

				// Token: 0x060009A5 RID: 2469 RVA: 0x00035AAC File Offset: 0x00033CAC
				public static void Constructor()
				{
					Funnel.FunnelState.PushStart_00000917$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(Funnel.FunnelState.PushStart(ref NativeCircularBuffer<float3>, ref NativeCircularBuffer<float3>, ref NativeCircularBuffer<float4>, ref float3, ref float3, ref float3)).MethodHandle);
				}

				// Token: 0x060009A6 RID: 2470 RVA: 0x000033F6 File Offset: 0x000015F6
				public static void Initialize()
				{
				}

				// Token: 0x060009A7 RID: 2471 RVA: 0x00035ABD File Offset: 0x00033CBD
				// Note: this type is marked as 'beforefieldinit'.
				static PushStart_00000917$BurstDirectCall()
				{
					Funnel.FunnelState.PushStart_00000917$BurstDirectCall.Constructor();
				}

				// Token: 0x060009A8 RID: 2472 RVA: 0x00035AC4 File Offset: 0x00033CC4
				public static void Invoke(ref NativeCircularBuffer<float3> leftPortals, ref NativeCircularBuffer<float3> rightPortals, ref NativeCircularBuffer<float4> unwrappedPortals, ref float3 newLeftPortal, ref float3 newRightPortal, ref float3 projectionAxis)
				{
					if (BurstCompiler.IsEnabled)
					{
						IntPtr functionPointer = Funnel.FunnelState.PushStart_00000917$BurstDirectCall.GetFunctionPointer();
						if (functionPointer != 0)
						{
							calli(System.Void(Pathfinding.Util.NativeCircularBuffer`1<Unity.Mathematics.float3>&,Pathfinding.Util.NativeCircularBuffer`1<Unity.Mathematics.float3>&,Pathfinding.Util.NativeCircularBuffer`1<Unity.Mathematics.float4>&,Unity.Mathematics.float3&,Unity.Mathematics.float3&,Unity.Mathematics.float3&), ref leftPortals, ref rightPortals, ref unwrappedPortals, ref newLeftPortal, ref newRightPortal, ref projectionAxis, functionPointer);
							return;
						}
					}
					Funnel.FunnelState.PushStart$BurstManaged(ref leftPortals, ref rightPortals, ref unwrappedPortals, ref newLeftPortal, ref newRightPortal, ref projectionAxis);
				}

				// Token: 0x04000695 RID: 1685
				private static IntPtr Pointer;

				// Token: 0x04000696 RID: 1686
				private static IntPtr DeferredCompilation;
			}

			// Token: 0x02000145 RID: 325
			// (Invoke) Token: 0x060009AA RID: 2474
			public delegate void ConvertCornerIndicesToPathProjected_00000921$PostfixBurstDelegate(ref Funnel.FunnelState funnelState, ref UnsafeSpan<int> indices, bool splitAtEveryPortal, in float3 startPoint, in float3 endPoint, bool lastCorner, in float3 projectionAxis, ref UnsafeSpan<float3> result, in float3 up);

			// Token: 0x02000146 RID: 326
			internal static class ConvertCornerIndicesToPathProjected_00000921$BurstDirectCall
			{
				// Token: 0x060009AD RID: 2477 RVA: 0x00035B03 File Offset: 0x00033D03
				[BurstDiscard]
				private static void GetFunctionPointerDiscard(ref IntPtr A_0)
				{
					if (Funnel.FunnelState.ConvertCornerIndicesToPathProjected_00000921$BurstDirectCall.Pointer == 0)
					{
						Funnel.FunnelState.ConvertCornerIndicesToPathProjected_00000921$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(Funnel.FunnelState.ConvertCornerIndicesToPathProjected_00000921$BurstDirectCall.DeferredCompilation, methodof(Funnel.FunnelState.ConvertCornerIndicesToPathProjected$BurstManaged(ref Funnel.FunnelState, ref UnsafeSpan<int>, bool, ref float3, ref float3, bool, ref float3, ref UnsafeSpan<float3>, ref float3)).MethodHandle, typeof(Funnel.FunnelState.ConvertCornerIndicesToPathProjected_00000921$PostfixBurstDelegate).TypeHandle);
					}
					A_0 = Funnel.FunnelState.ConvertCornerIndicesToPathProjected_00000921$BurstDirectCall.Pointer;
				}

				// Token: 0x060009AE RID: 2478 RVA: 0x00035B30 File Offset: 0x00033D30
				private static IntPtr GetFunctionPointer()
				{
					IntPtr intPtr = (IntPtr)0;
					Funnel.FunnelState.ConvertCornerIndicesToPathProjected_00000921$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
					return intPtr;
				}

				// Token: 0x060009AF RID: 2479 RVA: 0x00035B48 File Offset: 0x00033D48
				public static void Constructor()
				{
					Funnel.FunnelState.ConvertCornerIndicesToPathProjected_00000921$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(Funnel.FunnelState.ConvertCornerIndicesToPathProjected(ref Funnel.FunnelState, ref UnsafeSpan<int>, bool, ref float3, ref float3, bool, ref float3, ref UnsafeSpan<float3>, ref float3)).MethodHandle);
				}

				// Token: 0x060009B0 RID: 2480 RVA: 0x000033F6 File Offset: 0x000015F6
				public static void Initialize()
				{
				}

				// Token: 0x060009B1 RID: 2481 RVA: 0x00035B59 File Offset: 0x00033D59
				// Note: this type is marked as 'beforefieldinit'.
				static ConvertCornerIndicesToPathProjected_00000921$BurstDirectCall()
				{
					Funnel.FunnelState.ConvertCornerIndicesToPathProjected_00000921$BurstDirectCall.Constructor();
				}

				// Token: 0x060009B2 RID: 2482 RVA: 0x00035B60 File Offset: 0x00033D60
				public static void Invoke(ref Funnel.FunnelState funnelState, ref UnsafeSpan<int> indices, bool splitAtEveryPortal, in float3 startPoint, in float3 endPoint, bool lastCorner, in float3 projectionAxis, ref UnsafeSpan<float3> result, in float3 up)
				{
					if (BurstCompiler.IsEnabled)
					{
						IntPtr functionPointer = Funnel.FunnelState.ConvertCornerIndicesToPathProjected_00000921$BurstDirectCall.GetFunctionPointer();
						if (functionPointer != 0)
						{
							calli(System.Void(Pathfinding.Funnel/FunnelState&,Pathfinding.Util.UnsafeSpan`1<System.Int32>&,System.Boolean,Unity.Mathematics.float3&,Unity.Mathematics.float3&,System.Boolean,Unity.Mathematics.float3&,Pathfinding.Util.UnsafeSpan`1<Unity.Mathematics.float3>&,Unity.Mathematics.float3&), ref funnelState, ref indices, splitAtEveryPortal, ref startPoint, ref endPoint, lastCorner, ref projectionAxis, ref result, ref up, functionPointer);
							return;
						}
					}
					Funnel.FunnelState.ConvertCornerIndicesToPathProjected$BurstManaged(ref funnelState, ref indices, splitAtEveryPortal, in startPoint, in endPoint, lastCorner, in projectionAxis, ref result, in up);
				}

				// Token: 0x04000697 RID: 1687
				private static IntPtr Pointer;

				// Token: 0x04000698 RID: 1688
				private static IntPtr DeferredCompilation;
			}
		}

		// Token: 0x02000147 RID: 327
		// (Invoke) Token: 0x060009B4 RID: 2484
		public delegate int Calculate_0000090B$PostfixBurstDelegate(ref NativeCircularBuffer<float4> unwrappedPortals, ref NativeCircularBuffer<float3> leftPortals, ref NativeCircularBuffer<float3> rightPortals, ref float3 startPoint, ref float3 endPoint, ref UnsafeSpan<int> funnelPath, int maxCorners, ref float3 projectionAxis, out bool lastCorner);

		// Token: 0x02000148 RID: 328
		internal static class Calculate_0000090B$BurstDirectCall
		{
			// Token: 0x060009B7 RID: 2487 RVA: 0x00035BAB File Offset: 0x00033DAB
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (Funnel.Calculate_0000090B$BurstDirectCall.Pointer == 0)
				{
					Funnel.Calculate_0000090B$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(Funnel.Calculate_0000090B$BurstDirectCall.DeferredCompilation, methodof(Funnel.Calculate$BurstManaged(ref NativeCircularBuffer<float4>, ref NativeCircularBuffer<float3>, ref NativeCircularBuffer<float3>, ref float3, ref float3, ref UnsafeSpan<int>, int, ref float3, ref bool)).MethodHandle, typeof(Funnel.Calculate_0000090B$PostfixBurstDelegate).TypeHandle);
				}
				A_0 = Funnel.Calculate_0000090B$BurstDirectCall.Pointer;
			}

			// Token: 0x060009B8 RID: 2488 RVA: 0x00035BD8 File Offset: 0x00033DD8
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				Funnel.Calculate_0000090B$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x060009B9 RID: 2489 RVA: 0x00035BF0 File Offset: 0x00033DF0
			public static void Constructor()
			{
				Funnel.Calculate_0000090B$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(Funnel.Calculate(ref NativeCircularBuffer<float4>, ref NativeCircularBuffer<float3>, ref NativeCircularBuffer<float3>, ref float3, ref float3, ref UnsafeSpan<int>, int, ref float3, ref bool)).MethodHandle);
			}

			// Token: 0x060009BA RID: 2490 RVA: 0x000033F6 File Offset: 0x000015F6
			public static void Initialize()
			{
			}

			// Token: 0x060009BB RID: 2491 RVA: 0x00035C01 File Offset: 0x00033E01
			// Note: this type is marked as 'beforefieldinit'.
			static Calculate_0000090B$BurstDirectCall()
			{
				Funnel.Calculate_0000090B$BurstDirectCall.Constructor();
			}

			// Token: 0x060009BC RID: 2492 RVA: 0x00035C08 File Offset: 0x00033E08
			public static int Invoke(ref NativeCircularBuffer<float4> unwrappedPortals, ref NativeCircularBuffer<float3> leftPortals, ref NativeCircularBuffer<float3> rightPortals, ref float3 startPoint, ref float3 endPoint, ref UnsafeSpan<int> funnelPath, int maxCorners, ref float3 projectionAxis, out bool lastCorner)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = Funnel.Calculate_0000090B$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						return calli(System.Int32(Pathfinding.Util.NativeCircularBuffer`1<Unity.Mathematics.float4>&,Pathfinding.Util.NativeCircularBuffer`1<Unity.Mathematics.float3>&,Pathfinding.Util.NativeCircularBuffer`1<Unity.Mathematics.float3>&,Unity.Mathematics.float3&,Unity.Mathematics.float3&,Pathfinding.Util.UnsafeSpan`1<System.Int32>&,System.Int32,Unity.Mathematics.float3&,System.Boolean&), ref unwrappedPortals, ref leftPortals, ref rightPortals, ref startPoint, ref endPoint, ref funnelPath, maxCorners, ref projectionAxis, ref lastCorner, functionPointer);
					}
				}
				return Funnel.Calculate$BurstManaged(ref unwrappedPortals, ref leftPortals, ref rightPortals, ref startPoint, ref endPoint, ref funnelPath, maxCorners, ref projectionAxis, out lastCorner);
			}

			// Token: 0x04000699 RID: 1689
			private static IntPtr Pointer;

			// Token: 0x0400069A RID: 1690
			private static IntPtr DeferredCompilation;
		}
	}
}
