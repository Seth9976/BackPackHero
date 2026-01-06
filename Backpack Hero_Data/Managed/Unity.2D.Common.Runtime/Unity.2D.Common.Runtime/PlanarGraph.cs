using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace UnityEngine.U2D.Common.UTess
{
	// Token: 0x02000006 RID: 6
	internal struct PlanarGraph
	{
		// Token: 0x0600001E RID: 30 RVA: 0x0000240C File Offset: 0x0000060C
		internal static void RemoveDuplicateEdges(ref Array<int2> edges, ref int edgeCount, Array<int> duplicates, int duplicateCount)
		{
			if (duplicateCount == 0)
			{
				for (int i = 0; i < edgeCount; i++)
				{
					int2 @int = edges[i];
					@int.x = math.min(edges[i].x, edges[i].y);
					@int.y = math.max(edges[i].x, edges[i].y);
					edges[i] = @int;
				}
			}
			else
			{
				for (int j = 0; j < edgeCount; j++)
				{
					int2 int2 = edges[j];
					int num = duplicates[int2.x];
					int num2 = duplicates[int2.y];
					int2.x = math.min(num, num2);
					int2.y = math.max(num, num2);
					edges[j] = int2;
				}
			}
			ModuleHandle.InsertionSort<int2, TessEdgeCompare>(edges.UnsafePtr, 0, edgeCount - 1, default(TessEdgeCompare));
			int num3 = 1;
			for (int k = 1; k < edgeCount; k++)
			{
				int2 int3 = edges[k - 1];
				int2 int4 = edges[k];
				if ((int4.x != int3.x || int4.y != int3.y) && int4.x != int4.y)
				{
					edges[num3++] = int4;
				}
			}
			edgeCount = num3;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002568 File Offset: 0x00000768
		internal static bool CheckCollinear(double2 a0, double2 a1, double2 b0, double2 b1)
		{
			double num = (a1.y - a0.y) / (a1.x - a0.x);
			double num2 = (b0.y - a0.y) / (b0.x - a0.x);
			double num3 = (b1.y - a0.y) / (b1.x - a0.x);
			return (!math.isinf(num) || !math.isinf(num2) || !math.isinf(num3)) && math.abs(num - num2) > PlanarGraph.kEpsilon && math.abs(num - num3) > PlanarGraph.kEpsilon;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002614 File Offset: 0x00000814
		internal static bool LineLineIntersection(double2 a0, double2 a1, double2 b0, double2 b1)
		{
			double num = ModuleHandle.OrientFastDouble(a0, b0, b1);
			double num2 = ModuleHandle.OrientFastDouble(a1, b0, b1);
			if ((num > PlanarGraph.kEpsilon && num2 > PlanarGraph.kEpsilon) || (num < -PlanarGraph.kEpsilon && num2 < -PlanarGraph.kEpsilon))
			{
				return false;
			}
			double num3 = ModuleHandle.OrientFastDouble(b0, a0, a1);
			double num4 = ModuleHandle.OrientFastDouble(b1, a0, a1);
			return (num3 <= PlanarGraph.kEpsilon || num4 <= PlanarGraph.kEpsilon) && (num3 >= -PlanarGraph.kEpsilon || num4 >= -PlanarGraph.kEpsilon) && (math.abs(num) >= PlanarGraph.kEpsilon || math.abs(num2) >= PlanarGraph.kEpsilon || math.abs(num3) >= PlanarGraph.kEpsilon || math.abs(num4) >= PlanarGraph.kEpsilon || PlanarGraph.CheckCollinear(a0, a1, b0, b1));
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000026CC File Offset: 0x000008CC
		internal static bool LineLineIntersection(double2 p1, double2 p2, double2 p3, double2 p4, ref double2 result)
		{
			double num = p2.x - p1.x;
			double num2 = p2.y - p1.y;
			double num3 = p4.x - p3.x;
			double num4 = p4.y - p3.y;
			double num5 = num * num4 - num2 * num3;
			if (math.abs(num5) < PlanarGraph.kEpsilon)
			{
				return false;
			}
			double num6 = p3.x - p1.x;
			double num7 = p3.y - p1.y;
			double num8 = (num6 * num4 - num7 * num3) / num5;
			if (num8 >= -PlanarGraph.kEpsilon && num8 <= 1.0 + PlanarGraph.kEpsilon)
			{
				result.x = p1.x + num8 * num;
				result.y = p1.y + num8 * num2;
				return true;
			}
			return false;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002798 File Offset: 0x00000998
		internal static bool CalculateEdgeIntersections(Array<int2> edges, int edgeCount, Array<double2> points, int pointCount, ref Array<int2> results, ref Array<double2> intersects, ref int resultCount)
		{
			resultCount = 0;
			for (int i = 0; i < edgeCount; i++)
			{
				for (int j = i + 1; j < edgeCount; j++)
				{
					int2 @int = edges[i];
					int2 int2 = edges[j];
					if (@int.x != int2.x && @int.x != int2.y && @int.y != int2.x && @int.y != int2.y)
					{
						double2 @double = points[@int.x];
						double2 double2 = points[@int.y];
						double2 double3 = points[int2.x];
						double2 double4 = points[int2.y];
						double2 zero = double2.zero;
						if (PlanarGraph.LineLineIntersection(@double, double2, double3, double4) && PlanarGraph.LineLineIntersection(@double, double2, double3, double4, ref zero))
						{
							if (resultCount >= intersects.Length)
							{
								return false;
							}
							intersects[resultCount] = zero;
							int num = resultCount;
							resultCount = num + 1;
							results[num] = new int2(i, j);
						}
					}
				}
			}
			if (resultCount > edgeCount * PlanarGraph.kMaxIntersectionTolerance)
			{
				return false;
			}
			IntersectionCompare intersectionCompare = default(IntersectionCompare);
			intersectionCompare.edges = edges;
			intersectionCompare.points = points;
			ModuleHandle.InsertionSort<int2, IntersectionCompare>(results.UnsafePtr, 0, resultCount - 1, intersectionCompare);
			return true;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002900 File Offset: 0x00000B00
		internal static bool CalculateTJunctions(Array<int2> edges, int edgeCount, Array<double2> points, int pointCount, Array<int2> results, ref int resultCount)
		{
			resultCount = 0;
			for (int i = 0; i < edgeCount; i++)
			{
				for (int j = 0; j < pointCount; j++)
				{
					int2 @int = edges[i];
					if (@int.x != j && @int.y != j)
					{
						double2 @double = points[@int.x];
						double2 double2 = points[@int.y];
						double2 double3 = points[j];
						double2 double4 = points[j];
						if (PlanarGraph.LineLineIntersection(@double, double2, double3, double4))
						{
							if (resultCount >= results.Length)
							{
								return false;
							}
							int num = resultCount;
							resultCount = num + 1;
							results[num] = new int2(i, j);
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000029B8 File Offset: 0x00000BB8
		internal static bool CutEdges(ref Array<double2> points, ref int pointCount, ref Array<int2> edges, ref int edgeCount, ref Array<int2> tJunctions, ref int tJunctionCount, Array<int2> intersections, Array<double2> intersects, int intersectionCount)
		{
			for (int i = 0; i < intersectionCount; i++)
			{
				int2 @int = intersections[i];
				int x = @int.x;
				int y = @int.y;
				int2 zero = int2.zero;
				zero.x = x;
				zero.y = pointCount;
				int num = tJunctionCount;
				tJunctionCount = num + 1;
				tJunctions[num] = zero;
				int2 zero2 = int2.zero;
				zero2.x = y;
				zero2.y = pointCount;
				num = tJunctionCount;
				tJunctionCount = num + 1;
				tJunctions[num] = zero2;
				if (pointCount >= points.Length)
				{
					return false;
				}
				num = pointCount;
				pointCount = num + 1;
				points[num] = intersects[i];
			}
			ModuleHandle.InsertionSort<int2, TessJunctionCompare>(tJunctions.UnsafePtr, 0, tJunctionCount - 1, default(TessJunctionCompare));
			for (int j = tJunctionCount - 1; j >= 0; j--)
			{
				int2 int2 = tJunctions[j];
				int x2 = int2.x;
				int2 int3 = edges[x2];
				int num2 = int3.x;
				int num3 = int3.y;
				double2 @double = points[num2];
				double2 double2 = points[num3];
				if (@double.x - double2.x < 0.0 || (@double.x == double2.x && @double.y - double2.y < 0.0))
				{
					int num4 = num2;
					num2 = num3;
					num3 = num4;
				}
				int3.x = num2;
				int num5 = (int3.y = int2.y);
				edges[x2] = int3;
				int num;
				while (j > 0 && tJunctions[j - 1].x == x2)
				{
					int y2 = tJunctions[--j].y;
					int2 int4 = default(int2);
					int4.x = num5;
					int4.y = y2;
					num = edgeCount;
					edgeCount = num + 1;
					edges[num] = int4;
					num5 = y2;
				}
				int2 int5 = default(int2);
				int5.x = num5;
				int5.y = num3;
				num = edgeCount;
				edgeCount = num + 1;
				edges[num] = int5;
			}
			return true;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002BF0 File Offset: 0x00000DF0
		internal static void RemoveDuplicatePoints(ref Array<double2> points, ref int pointCount, ref Array<int> duplicates, ref int duplicateCount, Allocator allocator)
		{
			TessLink tessLink = TessLink.CreateLink(pointCount, allocator);
			for (int i = 0; i < pointCount; i++)
			{
				for (int j = i + 1; j < pointCount; j++)
				{
					if (math.distance(points[i], points[j]) < PlanarGraph.kEpsilon)
					{
						tessLink.Link(i, j);
					}
				}
			}
			duplicateCount = 0;
			for (int k = 0; k < pointCount; k++)
			{
				int num = tessLink.Find(k);
				if (num != k)
				{
					duplicateCount++;
					points[num] = math.min(points[k], points[num]);
				}
			}
			if (duplicateCount != 0)
			{
				int num2 = pointCount;
				pointCount = 0;
				for (int l = 0; l < num2; l++)
				{
					if (tessLink.Find(l) == l)
					{
						duplicates[l] = pointCount;
						int num3 = pointCount;
						pointCount = num3 + 1;
						points[num3] = points[l];
					}
					else
					{
						duplicates[l] = -1;
					}
				}
				for (int m = 0; m < num2; m++)
				{
					if (duplicates[m] < 0)
					{
						duplicates[m] = duplicates[tessLink.Find(m)];
					}
				}
			}
			TessLink.DestroyLink(tessLink);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002D1C File Offset: 0x00000F1C
		internal unsafe static bool Validate(Allocator allocator, in NativeArray<float2> inputPoints, int pointCount, in NativeArray<int2> inputEdges, int edgeCount, ref NativeArray<float2> outputPoints, out int outputPointCount, ref NativeArray<int2> outputEdges, out int outputEdgeCount)
		{
			outputPointCount = 0;
			outputEdgeCount = 0;
			float num = 10000f;
			int num2 = edgeCount;
			bool flag = true;
			bool flag2 = false;
			int num3 = edgeCount;
			Array<int> array = new Array<int>(num3, ModuleHandle.kMaxEdgeCount, allocator, NativeArrayOptions.UninitializedMemory);
			Array<int2> array2 = new Array<int2>(num3, ModuleHandle.kMaxEdgeCount, allocator, NativeArrayOptions.UninitializedMemory);
			Array<int2> array3 = new Array<int2>(num3, ModuleHandle.kMaxEdgeCount, allocator, NativeArrayOptions.UninitializedMemory);
			Array<int2> array4 = new Array<int2>(num3, ModuleHandle.kMaxEdgeCount, allocator, NativeArrayOptions.UninitializedMemory);
			Array<double2> array5 = new Array<double2>(pointCount * 2, pointCount * 8, allocator, NativeArrayOptions.UninitializedMemory);
			Array<double2> array6 = new Array<double2>(pointCount * 2, pointCount * 8, allocator, NativeArrayOptions.UninitializedMemory);
			for (int i = 0; i < pointCount; i++)
			{
				int num4 = i;
				NativeArray<float2> nativeArray = inputPoints;
				array5[num4] = nativeArray[i] * num;
			}
			UnsafeUtility.MemCpy(array2.UnsafeReadOnlyPtr, inputEdges.GetUnsafePtr<int2>(), (long)(edgeCount * sizeof(int2)));
			PlanarGraph.RemoveDuplicateEdges(ref array2, ref edgeCount, array, 0);
			while (flag && --num2 > 0)
			{
				int num5 = 0;
				flag2 = PlanarGraph.CalculateEdgeIntersections(array2, edgeCount, array5, pointCount, ref array4, ref array6, ref num5);
				if (!flag2)
				{
					break;
				}
				int num6 = 0;
				flag2 = PlanarGraph.CalculateTJunctions(array2, edgeCount, array5, pointCount, array3, ref num6);
				if (!flag2)
				{
					break;
				}
				flag2 = PlanarGraph.CutEdges(ref array5, ref pointCount, ref array2, ref edgeCount, ref array3, ref num6, array4, array6, num5);
				if (!flag2)
				{
					break;
				}
				int num7 = 0;
				PlanarGraph.RemoveDuplicatePoints(ref array5, ref pointCount, ref array, ref num7, allocator);
				PlanarGraph.RemoveDuplicateEdges(ref array2, ref edgeCount, array, num7);
				flag = num5 != 0 || num6 != 0;
			}
			if (flag2)
			{
				outputEdgeCount = edgeCount;
				outputPointCount = pointCount;
				UnsafeUtility.MemCpy(outputEdges.GetUnsafePtr<int2>(), array2.UnsafeReadOnlyPtr, (long)(edgeCount * sizeof(int2)));
				for (int j = 0; j < pointCount; j++)
				{
					outputPoints[j] = new float2((float)(array5[j].x / (double)num), (float)(array5[j].y / (double)num));
				}
			}
			array2.Dispose();
			array5.Dispose();
			array6.Dispose();
			array.Dispose();
			array3.Dispose();
			array4.Dispose();
			return flag2 && num2 > 0;
		}

		// Token: 0x0400000A RID: 10
		private static readonly double kEpsilon = 1E-05;

		// Token: 0x0400000B RID: 11
		private static readonly int kMaxIntersectionTolerance = 4;
	}
}
