using System;
using System.Collections.Generic;
using Pathfinding.Util;
using Unity.Mathematics;
using Unity.Profiling;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200014A RID: 330
	public static class GridStringPulling
	{
		// Token: 0x060009BF RID: 2495 RVA: 0x00035D58 File Offset: 0x00033F58
		private static long Cross(int2 lhs, int2 rhs)
		{
			return (long)lhs.x * (long)rhs.y - (long)lhs.y * (long)rhs.x;
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00035D79 File Offset: 0x00033F79
		private static long Dot(int2 a, int2 b)
		{
			return (long)a.x * (long)b.x + (long)a.y * (long)b.y;
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00035D9C File Offset: 0x00033F9C
		private static bool RightOrColinear(int2 a, int2 b, int2 p)
		{
			return (long)(b.x - a.x) * (long)(p.y - a.y) - (long)(p.x - a.x) * (long)(b.y - a.y) <= 0L;
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00035DEB File Offset: 0x00033FEB
		private static int2 Perpendicular(int2 v)
		{
			return new int2(-v.y, v.x);
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00035DFF File Offset: 0x00033FFF
		private static int2 ToFixedPrecision(Vector2 p)
		{
			return new int2(math.round(new float2(p) * 1024f));
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00035E20 File Offset: 0x00034020
		private static Vector2 FromFixedPrecision(int2 p)
		{
			return p * 0.0009765625f;
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00035E38 File Offset: 0x00034038
		private static Side Side2D(int2 a, int2 b, int2 p)
		{
			long num = GridStringPulling.Cross(b - a, p - a);
			if (num > 0L)
			{
				return Side.Left;
			}
			if (num >= 0L)
			{
				return Side.Colinear;
			}
			return Side.Right;
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00035E68 File Offset: 0x00034068
		public static float IntersectionLength(int2 nodeCenter, int2 segmentStart, int2 segmentEnd)
		{
			float2 @float = math.rcp(segmentEnd - segmentStart);
			float num = math.length(segmentEnd - segmentStart);
			float num2 = float.NegativeInfinity;
			float num3 = float.PositiveInfinity;
			int2 @int = segmentEnd - segmentStart;
			int2 int2 = nodeCenter + new int2(1024, 1024);
			if ((double)@int.x != 0.0)
			{
				float num4 = (float)(nodeCenter.x - segmentStart.x) * @float.x;
				float num5 = (float)(int2.x - segmentStart.x) * @float.x;
				num2 = math.max(num2, math.min(num4, num5));
				num3 = math.min(num3, math.max(num4, num5));
			}
			else if (segmentStart.x < nodeCenter.x || segmentStart.x > int2.x)
			{
				return 0f;
			}
			if ((double)@int.y != 0.0)
			{
				float num6 = (float)(nodeCenter.y - segmentStart.y) * @float.y;
				float num7 = (float)(int2.y - segmentStart.y) * @float.y;
				num2 = math.max(num2, math.min(num6, num7));
				num3 = math.min(num3, math.max(num6, num7));
			}
			else if (segmentStart.y < nodeCenter.y || segmentStart.y > int2.y)
			{
				return 0f;
			}
			num2 = math.max(0f, num2);
			num3 = math.min(1f, num3);
			return math.max(num3 - num2, 0f) * num * 0.0009765625f;
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x000033F6 File Offset: 0x000015F6
		internal static void TestIntersectionLength()
		{
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x00036008 File Offset: 0x00034208
		private static uint LinecastCost(List<GraphNode> trace, int2 segmentStart, int2 segmentEnd, GridGraph gg, Func<GraphNode, uint> traversalCost)
		{
			uint num = 0U;
			for (int i = 0; i < trace.Count; i++)
			{
				GridNodeBase gridNodeBase = trace[i] as GridNodeBase;
				num += (uint)((traversalCost(gridNodeBase) + gg.nodeSize * 1000f) * GridStringPulling.IntersectionLength(new int2(gridNodeBase.XCoordinateInGrid, gridNodeBase.ZCoordinateInGrid) * 1024, segmentStart, segmentEnd));
			}
			return num;
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00036074 File Offset: 0x00034274
		public static List<Vector3> Calculate(List<GraphNode> pathNodes, int nodeStartIndex, int nodeEndIndex, Vector3 startPoint, Vector3 endPoint, Func<GraphNode, uint> traversalCost = null, Func<GraphNode, bool> filter = null, int maxCorners = 2147483647)
		{
			List<int> list = ListPool<int>.Claim();
			list.Add(0);
			int num = nodeEndIndex - nodeStartIndex + 1;
			GridGraph gridGraph = pathNodes[nodeStartIndex].Graph as GridGraph;
			List<GraphNode> list2 = ListPool<GraphNode>.Claim();
			Side side = Side.Colinear;
			int num2 = 0;
			num += 2;
			int num3 = num;
			GridNodeBase[] array = ArrayPool<GridNodeBase>.Claim(num3 * 2);
			int2[] array2 = ArrayPool<int2>.Claim(num3 * 2);
			int2[] array3 = ArrayPool<int2>.Claim(num3 * 2);
			uint[] array4 = ArrayPool<uint>.Claim(num3 * 2);
			uint num4 = 0U;
			for (int i = 0; i < num; i++)
			{
				GridNodeBase gridNodeBase = (array[i] = pathNodes[math.clamp(nodeStartIndex + i - 1, nodeStartIndex, nodeEndIndex)] as GridNodeBase);
				int2 @int = new int2(gridNodeBase.XCoordinateInGrid, gridNodeBase.ZCoordinateInGrid);
				int2 int2 = @int * 1024;
				int2 int3;
				if (i == 0)
				{
					int3 = GridStringPulling.ToFixedPrecision(gridNodeBase.NormalizePoint(startPoint));
					int3 = math.clamp(int3, int2.zero, new int2(1024, 1024));
				}
				else if (i == num - 1)
				{
					int3 = GridStringPulling.ToFixedPrecision(gridNodeBase.NormalizePoint(endPoint));
					int3 = math.clamp(int3, int2.zero, new int2(1024, 1024));
				}
				else
				{
					int3 = new int2(512, 512);
				}
				array2[i] = int2 + int3;
				array3[i] = int3;
				if (i > 0 && traversalCost != null)
				{
					num4 += (uint)((traversalCost(array[i - 1]) + gridGraph.nodeSize * 1000f) * GridStringPulling.IntersectionLength(new int2(array[i - 1].XCoordinateInGrid, array[i - 1].ZCoordinateInGrid) * 1024, array2[i - 1], array2[i]));
					num4 += (uint)((traversalCost(array[i]) + gridGraph.nodeSize * 1000f) * GridStringPulling.IntersectionLength(@int * 1024, array2[i - 1], array2[i]));
				}
				array4[i] = num4;
			}
			int num5 = 0;
			int num6 = 1;
			int j = 1;
			while (j < num)
			{
				if (list.Count < maxCorners)
				{
					num2++;
					if (num2 <= 10000)
					{
						int num7 = list[list.Count - 1];
						int2 int4 = array3[num7];
						int num8 = ((list.Count > 1) ? list[list.Count - 2] : (-1));
						GridNodeBase gridNodeBase2 = array[num7];
						int num9 = num - j - 1;
						int num10 = 0;
						int k = math.min(4, num9);
						for (;;)
						{
							int num11 = j + k;
							if (list.Count > 1 && GridStringPulling.Side2D(array2[num8], array2[num7], array2[num11]) != side)
							{
								goto Block_11;
							}
							list2.Clear();
							GridHitInfo gridHitInfo;
							if (gridGraph.Linecast(gridNodeBase2, int4, array[num11], array3[num11], out gridHitInfo, list2, filter, false))
							{
								goto Block_12;
							}
							if (traversalCost != null)
							{
								uint num12 = GridStringPulling.LinecastCost(list2, array2[num7], array2[num11], gridGraph, traversalCost);
								if (num12 > array4[num11] - array4[num7] + 5U)
								{
									goto Block_14;
								}
							}
							if (k >= num9)
							{
								goto IL_037A;
							}
							num10 = k;
							k = math.min(k * 2, num9);
						}
						IL_037D:
						GridStringPulling.PredicateFailMode predicateFailMode;
						if (predicateFailMode == GridStringPulling.PredicateFailMode.ReachedEnd)
						{
							list.Add(num - 1);
							goto IL_0905;
						}
						while (k > num10 + 1)
						{
							int num13 = (num10 + k) / 2;
							int num14 = j + num13;
							bool flag;
							if (flag = list.Count > 1 && GridStringPulling.Side2D(array2[num8], array2[num7], array2[num14]) != side)
							{
								predicateFailMode = GridStringPulling.PredicateFailMode.Turn;
							}
							else
							{
								list2.Clear();
								GridHitInfo gridHitInfo;
								if (gridGraph.Linecast(gridNodeBase2, int4, array[num14], array3[num14], out gridHitInfo, list2, filter, false))
								{
									predicateFailMode = GridStringPulling.PredicateFailMode.LinecastObstacle;
									flag = true;
								}
								else if (traversalCost != null)
								{
									uint num15 = GridStringPulling.LinecastCost(list2, array2[num7], array2[num14], gridGraph, traversalCost);
									if (num15 > array4[num14] - array4[num7] + 5U)
									{
										predicateFailMode = GridStringPulling.PredicateFailMode.LinecastCost;
										flag = true;
									}
								}
							}
							if (flag)
							{
								k = num13;
							}
							else
							{
								num10 = num13;
							}
						}
						if (num10 > 0)
						{
							num5 = num7;
							num6 = j + num10;
						}
						else
						{
							bool flag2;
							if (!(flag2 = list.Count > 1 && GridStringPulling.Side2D(array2[num8], array2[num7], array2[j + num10]) != side))
							{
								list2.Clear();
								GridHitInfo gridHitInfo;
								if (gridGraph.Linecast(gridNodeBase2, int4, array[j + num10], array3[j + num10], out gridHitInfo, list2, filter, false))
								{
									flag2 = true;
								}
								else if (traversalCost != null)
								{
									uint num16 = GridStringPulling.LinecastCost(list2, array2[num7], array2[j + num10], gridGraph, traversalCost);
									if (num16 > array4[j + num10] - array4[num7] + 5U)
									{
										flag2 = true;
									}
								}
							}
							if (!flag2)
							{
								num5 = num7;
								num6 = j + num10;
							}
						}
						j += k;
						list2.Clear();
						list2.Clear();
						if (predicateFailMode == GridStringPulling.PredicateFailMode.LinecastCost)
						{
							list.Add(num6);
							side = GridStringPulling.Side2D(array2[num7], array2[num6], array2[j]);
							num5 = num6;
							j--;
							continue;
						}
						if (predicateFailMode == GridStringPulling.PredicateFailMode.LinecastObstacle)
						{
							list2.Clear();
							GridHitInfo gridHitInfo;
							int num17;
							if (gridGraph.Linecast(array[num5], array3[num5], array[num6], array3[num6], out gridHitInfo, list2, filter, false))
							{
								num17 = num6;
								Debug.LogError("Inconsistent linecasts");
							}
							else
							{
								list2.Add(array[j]);
								GridNodeBase gridNodeBase3 = null;
								int2 int5 = default(int2);
								uint num18 = 0U;
								int2 int6 = default(int2);
								int2 int7 = array2[num5];
								int2 int8 = array2[num6];
								int2 int9 = int8 - int7;
								GridStringPulling.TriangleBounds triangleBounds = new GridStringPulling.TriangleBounds(int7, int8, array2[j]);
								int num19 = Math.Sign(GridStringPulling.Cross(int9, array2[j] - int7));
								uint num20 = array4[num5];
								for (int l = 0; l < list2.Count; l++)
								{
									GridNodeBase gridNodeBase4 = list2[l] as GridNodeBase;
									int2 int10 = new int2(gridNodeBase4.XCoordinateInGrid, gridNodeBase4.ZCoordinateInGrid) * 1024;
									if (traversalCost != null)
									{
										num20 += (uint)((traversalCost(gridNodeBase4) + gridGraph.nodeSize * 1000f) * GridStringPulling.IntersectionLength(int10, int7, int8));
									}
									for (int m = 0; m < 4; m++)
									{
										if (!gridNodeBase4.HasConnectionInDirection(m) || (filter != null && !filter(gridNodeBase4.GetNeighbourAlongDirection(m))))
										{
											for (int n = 0; n < 2; n++)
											{
												int2 int11 = GridStringPulling.directionToCorners[(m + n) & 3];
												int2 int12 = int10 + int11;
												if (triangleBounds.Contains(int12))
												{
													int2 int13 = int12 - int7;
													if (!math.all(int13 == 0) && !math.all(int12 == int8))
													{
														long num21 = GridStringPulling.Cross(int13, int6);
														if (gridNodeBase3 == null || Math.Sign(num21) == num19 || (num21 == 0L && math.lengthsq(int13) > math.lengthsq(int6)))
														{
															int6 = int13;
															gridNodeBase3 = gridNodeBase4;
															int5 = int11;
															num18 = num20;
														}
													}
												}
											}
										}
									}
								}
								if (gridNodeBase3 == null)
								{
									num17 = num6;
								}
								else
								{
									num17 = num3;
									array[num3] = gridNodeBase3;
									array3[num3] = int5;
									int2 int14 = new int2(gridNodeBase3.XCoordinateInGrid, gridNodeBase3.ZCoordinateInGrid);
									array2[num3] = int14 * 1024 + int5;
									array4[num3] = num18;
									num3++;
								}
							}
							list.Add(num17);
							side = GridStringPulling.Side2D(array2[num7], array2[num17], array2[j]);
							num5 = num17;
							j--;
							continue;
						}
						num5 = num7;
						num6 = j;
						if (list.Count <= 1)
						{
							continue;
						}
						int num22 = list[list.Count - 2];
						Side side2 = GridStringPulling.Side2D(array2[num22], array2[num7], array2[j]);
						if (side != side2)
						{
							num5 = list[list.Count - 2];
							num6 = list[list.Count - 1];
							list.RemoveAt(list.Count - 1);
							if (list.Count > 1)
							{
								num7 = num22;
								num22 = list[list.Count - 2];
								side = GridStringPulling.Side2D(array2[num22], array2[num7], array2[j]);
							}
							else
							{
								side = Side.Colinear;
							}
							j--;
							continue;
						}
						continue;
						Block_11:
						predicateFailMode = GridStringPulling.PredicateFailMode.Turn;
						goto IL_037D;
						Block_12:
						predicateFailMode = GridStringPulling.PredicateFailMode.LinecastObstacle;
						goto IL_037D;
						Block_14:
						predicateFailMode = GridStringPulling.PredicateFailMode.LinecastCost;
						goto IL_037D;
						IL_037A:
						predicateFailMode = GridStringPulling.PredicateFailMode.ReachedEnd;
						goto IL_037D;
					}
					Debug.LogError("Inf loop");
				}
				IL_0905:
				List<Vector3> list3 = ListPool<Vector3>.Claim(list.Count);
				for (int num23 = 0; num23 < list.Count; num23++)
				{
					int num24 = list[num23];
					list3.Add(array[num24].UnNormalizePoint(GridStringPulling.FromFixedPrecision(array3[num24])));
				}
				ArrayPool<GridNodeBase>.Release(ref array, false);
				ArrayPool<int2>.Release(ref array2, false);
				ArrayPool<int2>.Release(ref array3, false);
				ArrayPool<uint>.Release(ref array4, false);
				ListPool<int>.Release(ref list);
				ListPool<GraphNode>.Release(ref list2);
				return list3;
			}
			list.Add(num - 1);
			goto IL_0905;
		}

		// Token: 0x0400069B RID: 1691
		private static int2[] directionToCorners = new int2[]
		{
			new int2(0, 0),
			new int2(1024, 0),
			new int2(1024, 1024),
			new int2(0, 1024)
		};

		// Token: 0x0400069C RID: 1692
		private const int FixedPrecisionScale = 1024;

		// Token: 0x0400069D RID: 1693
		private static ProfilerMarker marker1 = new ProfilerMarker("Linecast hit");

		// Token: 0x0400069E RID: 1694
		private static ProfilerMarker marker2 = new ProfilerMarker("Linecast success");

		// Token: 0x0400069F RID: 1695
		private static ProfilerMarker marker3 = new ProfilerMarker("Trace");

		// Token: 0x040006A0 RID: 1696
		private static ProfilerMarker marker4 = new ProfilerMarker("Neighbours");

		// Token: 0x040006A1 RID: 1697
		private static ProfilerMarker marker5 = new ProfilerMarker("Re-evaluate linecast");

		// Token: 0x040006A2 RID: 1698
		private static ProfilerMarker marker6 = new ProfilerMarker("Init");

		// Token: 0x040006A3 RID: 1699
		private static ProfilerMarker marker7 = new ProfilerMarker("Initloop");

		// Token: 0x0200014B RID: 331
		private struct TriangleBounds
		{
			// Token: 0x060009CB RID: 2507 RVA: 0x00036AD0 File Offset: 0x00034CD0
			public TriangleBounds(int2 p1, int2 p2, int2 p3)
			{
				if (GridStringPulling.RightOrColinear(p1, p2, p3))
				{
					int2 @int = p3;
					p3 = p1;
					p1 = @int;
				}
				this.d1 = GridStringPulling.Perpendicular(p2 - p1);
				this.d2 = GridStringPulling.Perpendicular(p3 - p2);
				this.d3 = GridStringPulling.Perpendicular(p1 - p3);
				this.t1 = GridStringPulling.Dot(this.d1, p1);
				this.t2 = GridStringPulling.Dot(this.d2, p2);
				this.t3 = GridStringPulling.Dot(this.d3, p3);
			}

			// Token: 0x060009CC RID: 2508 RVA: 0x00036B5C File Offset: 0x00034D5C
			public bool Contains(int2 p)
			{
				return GridStringPulling.Dot(this.d1, p) >= this.t1 && GridStringPulling.Dot(this.d2, p) >= this.t2 && GridStringPulling.Dot(this.d3, p) >= this.t3;
			}

			// Token: 0x040006A4 RID: 1700
			private int2 d1;

			// Token: 0x040006A5 RID: 1701
			private int2 d2;

			// Token: 0x040006A6 RID: 1702
			private int2 d3;

			// Token: 0x040006A7 RID: 1703
			private long t1;

			// Token: 0x040006A8 RID: 1704
			private long t2;

			// Token: 0x040006A9 RID: 1705
			private long t3;
		}

		// Token: 0x0200014C RID: 332
		private enum PredicateFailMode
		{
			// Token: 0x040006AB RID: 1707
			Undefined,
			// Token: 0x040006AC RID: 1708
			Turn,
			// Token: 0x040006AD RID: 1709
			LinecastObstacle,
			// Token: 0x040006AE RID: 1710
			LinecastCost,
			// Token: 0x040006AF RID: 1711
			ReachedEnd
		}
	}
}
