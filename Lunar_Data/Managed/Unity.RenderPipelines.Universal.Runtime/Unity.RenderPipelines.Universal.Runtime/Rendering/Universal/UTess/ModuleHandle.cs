using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal.UTess
{
	// Token: 0x02000130 RID: 304
	internal struct ModuleHandle
	{
		// Token: 0x0600091D RID: 2333 RVA: 0x0003CEAB File Offset: 0x0003B0AB
		internal static void Copy<T>(NativeArray<T> src, int srcIndex, NativeArray<T> dst, int dstIndex, int length) where T : struct
		{
			NativeArray<T>.Copy(src, srcIndex, dst, dstIndex, length);
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0003CEB8 File Offset: 0x0003B0B8
		internal static void Copy<T>(NativeArray<T> src, NativeArray<T> dst, int length) where T : struct
		{
			ModuleHandle.Copy<T>(src, 0, dst, 0, length);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0003CEC4 File Offset: 0x0003B0C4
		internal unsafe static void InsertionSort<T, U>(void* array, int lo, int hi, U comp) where T : struct where U : IComparer<T>
		{
			for (int i = lo; i < hi; i++)
			{
				int num = i;
				T t = UnsafeUtility.ReadArrayElement<T>(array, i + 1);
				while (num >= lo && comp.Compare(t, UnsafeUtility.ReadArrayElement<T>(array, num)) < 0)
				{
					UnsafeUtility.WriteArrayElement<T>(array, num + 1, UnsafeUtility.ReadArrayElement<T>(array, num));
					num--;
				}
				UnsafeUtility.WriteArrayElement<T>(array, num + 1, t);
			}
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0003CF28 File Offset: 0x0003B128
		internal static int GetLower<T, U, X>(NativeArray<T> values, int count, U check, X condition) where T : struct where U : struct where X : ICondition2<T, U>
		{
			int i = 0;
			int num = count - 1;
			int num2 = i - 1;
			while (i <= num)
			{
				int num3 = i + num >> 1;
				float num4 = 0f;
				if (condition.Test(values[num3], check, ref num4))
				{
					num2 = num3;
					i = num3 + 1;
				}
				else
				{
					num = num3 - 1;
				}
			}
			return num2;
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0003CF7C File Offset: 0x0003B17C
		internal static int GetUpper<T, U, X>(NativeArray<T> values, int count, U check, X condition) where T : struct where U : struct where X : ICondition2<T, U>
		{
			int i = 0;
			int num = count - 1;
			int num2 = num + 1;
			while (i <= num)
			{
				int num3 = i + num >> 1;
				float num4 = 0f;
				if (condition.Test(values[num3], check, ref num4))
				{
					num2 = num3;
					num = num3 - 1;
				}
				else
				{
					i = num3 + 1;
				}
			}
			return num2;
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0003CFD0 File Offset: 0x0003B1D0
		internal static int GetEqual<T, U, X>(NativeArray<T> values, int count, U check, X condition) where T : struct where U : struct where X : ICondition2<T, U>
		{
			int i = 0;
			int num = count - 1;
			while (i <= num)
			{
				int num2 = i + num >> 1;
				float num3 = 0f;
				condition.Test(values[num2], check, ref num3);
				if (num3 == 0f)
				{
					return num2;
				}
				if (num3 <= 0f)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2 - 1;
				}
			}
			return -1;
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0003D02C File Offset: 0x0003B22C
		internal static float OrientFast(float2 a, float2 b, float2 c)
		{
			float num = 1.110223E-16f;
			float num2 = (b.y - a.y) * (c.x - b.x) - (b.x - a.x) * (c.y - b.y);
			if (math.abs(num2) < num)
			{
				return 0f;
			}
			return num2;
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0003D088 File Offset: 0x0003B288
		internal static double OrientFastDouble(double2 a, double2 b, double2 c)
		{
			double num = 1.1102230246251565E-16;
			double num2 = (b.y - a.y) * (c.x - b.x) - (b.x - a.x) * (c.y - b.y);
			if (math.abs(num2) < num)
			{
				return 0.0;
			}
			return num2;
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0003D0EC File Offset: 0x0003B2EC
		internal static UCircle CircumCircle(UTriangle tri)
		{
			float num = tri.va.x * tri.va.x;
			float num2 = tri.vb.x * tri.vb.x;
			float num3 = tri.vc.x * tri.vc.x;
			float num4 = tri.va.y * tri.va.y;
			float num5 = tri.vb.y * tri.vb.y;
			float num6 = tri.vc.y * tri.vc.y;
			float num7 = 2f * ((tri.vb.x - tri.va.x) * (tri.vc.y - tri.va.y) - (tri.vb.y - tri.va.y) * (tri.vc.x - tri.va.x));
			float num8 = ((tri.vc.y - tri.va.y) * (num2 - num + num5 - num4) + (tri.va.y - tri.vb.y) * (num3 - num + num6 - num4)) / num7;
			float num9 = ((tri.va.x - tri.vc.x) * (num2 - num + num5 - num4) + (tri.vb.x - tri.va.x) * (num3 - num + num6 - num4)) / num7;
			float num10 = tri.va.x - num8;
			float num11 = tri.va.y - num9;
			return new UCircle
			{
				center = new float2(num8, num9),
				radius = math.sqrt(num10 * num10 + num11 * num11)
			};
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0003D2CF File Offset: 0x0003B4CF
		internal static bool IsInsideCircle(UCircle c, float2 v)
		{
			return math.distance(v, c.center) < c.radius;
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0003D2E8 File Offset: 0x0003B4E8
		internal static float TriangleArea(float2 va, float2 vb, float2 vc)
		{
			float3 @float = new float3(va.x, va.y, 0f);
			float3 float2 = new float3(vb.x, vb.y, 0f);
			float3 float3 = new float3(vc.x, vc.y, 0f);
			return math.abs(math.cross(@float - float2, @float - float3).z) * 0.5f;
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0003D360 File Offset: 0x0003B560
		internal static float Sign(float2 p1, float2 p2, float2 p3)
		{
			return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0003D39C File Offset: 0x0003B59C
		internal static bool IsInsideTriangle(float2 pt, float2 v1, float2 v2, float2 v3)
		{
			float num = ModuleHandle.Sign(pt, v1, v2);
			float num2 = ModuleHandle.Sign(pt, v2, v3);
			float num3 = ModuleHandle.Sign(pt, v3, v1);
			bool flag = num < 0f || num2 < 0f || num3 < 0f;
			bool flag2 = num > 0f || num2 > 0f || num3 > 0f;
			return !flag || !flag2;
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0003D400 File Offset: 0x0003B600
		internal static bool IsInsideTriangleApproximate(float2 pt, float2 v1, float2 v2, float2 v3)
		{
			float num = ModuleHandle.TriangleArea(v1, v2, v3);
			float num2 = ModuleHandle.TriangleArea(pt, v1, v2);
			float num3 = ModuleHandle.TriangleArea(pt, v2, v3);
			float num4 = ModuleHandle.TriangleArea(pt, v3, v1);
			float num5 = 1.110223E-16f;
			return Mathf.Abs(num - (num2 + num3 + num4)) < num5;
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0003D444 File Offset: 0x0003B644
		internal static bool IsInsideCircle(float2 a, float2 b, float2 c, float2 p)
		{
			float num = math.dot(a, a);
			float num2 = math.dot(b, b);
			float num3 = math.dot(c, c);
			float x = a.x;
			float y = a.y;
			float x2 = b.x;
			float y2 = b.y;
			float x3 = c.x;
			float y3 = c.y;
			float num4 = (num * (y3 - y2) + num2 * (y - y3) + num3 * (y2 - y)) / (x * (y3 - y2) + x2 * (y - y3) + x3 * (y2 - y));
			float num5 = (num * (x3 - x2) + num2 * (x - x3) + num3 * (x2 - x)) / (y * (x3 - x2) + y2 * (x - x3) + y3 * (x2 - x));
			float2 @float = new float2
			{
				x = num4 / 2f,
				y = num5 / 2f
			};
			float num6 = math.distance(a, @float);
			float num7 = math.distance(p, @float);
			return num6 - num7 > 1E-05f;
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0003D538 File Offset: 0x0003B738
		internal static void BuildTriangles(NativeArray<float2> vertices, int vertexCount, NativeArray<int> indices, int indexCount, ref NativeArray<UTriangle> triangles, ref int triangleCount, ref float maxArea, ref float avgArea, ref float minArea)
		{
			for (int i = 0; i < indexCount; i += 3)
			{
				UTriangle utriangle = default(UTriangle);
				int num = indices[i];
				int num2 = indices[i + 1];
				int num3 = indices[i + 2];
				utriangle.va = vertices[num];
				utriangle.vb = vertices[num2];
				utriangle.vc = vertices[num3];
				utriangle.c = ModuleHandle.CircumCircle(utriangle);
				utriangle.area = ModuleHandle.TriangleArea(utriangle.va, utriangle.vb, utriangle.vc);
				maxArea = math.max(utriangle.area, maxArea);
				minArea = math.min(utriangle.area, minArea);
				avgArea += utriangle.area;
				int num4 = triangleCount;
				triangleCount = num4 + 1;
				triangles[num4] = utriangle;
			}
			avgArea /= (float)triangleCount;
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0003D628 File Offset: 0x0003B828
		internal static void BuildTriangles(NativeArray<float2> vertices, int vertexCount, NativeArray<int> indices, int indexCount, ref NativeArray<UTriangle> triangles, ref int triangleCount, ref float maxArea, ref float avgArea, ref float minArea, ref float maxEdge, ref float avgEdge, ref float minEdge)
		{
			for (int i = 0; i < indexCount; i += 3)
			{
				UTriangle utriangle = default(UTriangle);
				int num = indices[i];
				int num2 = indices[i + 1];
				int num3 = indices[i + 2];
				utriangle.va = vertices[num];
				utriangle.vb = vertices[num2];
				utriangle.vc = vertices[num3];
				utriangle.c = ModuleHandle.CircumCircle(utriangle);
				utriangle.area = ModuleHandle.TriangleArea(utriangle.va, utriangle.vb, utriangle.vc);
				maxArea = math.max(utriangle.area, maxArea);
				minArea = math.min(utriangle.area, minArea);
				avgArea += utriangle.area;
				float num4 = math.distance(utriangle.va, utriangle.vb);
				float num5 = math.distance(utriangle.vb, utriangle.vc);
				float num6 = math.distance(utriangle.vc, utriangle.va);
				maxEdge = math.max(num4, maxEdge);
				maxEdge = math.max(num5, maxEdge);
				maxEdge = math.max(num6, maxEdge);
				minEdge = math.min(num4, minEdge);
				minEdge = math.min(num5, minEdge);
				minEdge = math.min(num6, minEdge);
				avgEdge += num4;
				avgEdge += num5;
				avgEdge += num6;
				int num7 = triangleCount;
				triangleCount = num7 + 1;
				triangles[num7] = utriangle;
			}
			avgArea /= (float)triangleCount;
			avgEdge /= (float)indexCount;
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0003D7C4 File Offset: 0x0003B9C4
		internal static void BuildTrianglesAndEdges(NativeArray<float2> vertices, int vertexCount, NativeArray<int> indices, int indexCount, ref NativeArray<UTriangle> triangles, ref int triangleCount, ref NativeArray<int4> delaEdges, ref int delaEdgeCount, ref float maxArea, ref float avgArea, ref float minArea)
		{
			for (int i = 0; i < indexCount; i += 3)
			{
				UTriangle utriangle = default(UTriangle);
				int num = indices[i];
				int num2 = indices[i + 1];
				int num3 = indices[i + 2];
				utriangle.va = vertices[num];
				utriangle.vb = vertices[num2];
				utriangle.vc = vertices[num3];
				utriangle.c = ModuleHandle.CircumCircle(utriangle);
				utriangle.area = ModuleHandle.TriangleArea(utriangle.va, utriangle.vb, utriangle.vc);
				maxArea = math.max(utriangle.area, maxArea);
				minArea = math.min(utriangle.area, minArea);
				avgArea += utriangle.area;
				utriangle.indices = new int3(num, num2, num3);
				int num4 = delaEdgeCount;
				delaEdgeCount = num4 + 1;
				delaEdges[num4] = new int4(math.min(num, num2), math.max(num, num2), triangleCount, -1);
				num4 = delaEdgeCount;
				delaEdgeCount = num4 + 1;
				delaEdges[num4] = new int4(math.min(num2, num3), math.max(num2, num3), triangleCount, -1);
				num4 = delaEdgeCount;
				delaEdgeCount = num4 + 1;
				delaEdges[num4] = new int4(math.min(num3, num), math.max(num3, num), triangleCount, -1);
				num4 = triangleCount;
				triangleCount = num4 + 1;
				triangles[num4] = utriangle;
			}
			avgArea /= (float)triangleCount;
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0003D94C File Offset: 0x0003BB4C
		private static void CopyGraph(NativeArray<float2> srcPoints, int srcPointCount, ref NativeArray<float2> dstPoints, ref int dstPointCount, NativeArray<int2> srcEdges, int srcEdgeCount, ref NativeArray<int2> dstEdges, ref int dstEdgeCount)
		{
			dstEdgeCount = srcEdgeCount;
			dstPointCount = srcPointCount;
			ModuleHandle.Copy<int2>(srcEdges, dstEdges, srcEdgeCount);
			ModuleHandle.Copy<float2>(srcPoints, dstPoints, srcPointCount);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0003D973 File Offset: 0x0003BB73
		private static void CopyGeometry(NativeArray<int> srcIndices, int srcIndexCount, ref NativeArray<int> dstIndices, ref int dstIndexCount, NativeArray<float2> srcVertices, int srcVertexCount, ref NativeArray<float2> dstVertices, ref int dstVertexCount)
		{
			dstIndexCount = srcIndexCount;
			dstVertexCount = srcVertexCount;
			ModuleHandle.Copy<int>(srcIndices, dstIndices, srcIndexCount);
			ModuleHandle.Copy<float2>(srcVertices, dstVertices, srcVertexCount);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0003D99A File Offset: 0x0003BB9A
		private static void TransferOutput(NativeArray<int2> srcEdges, int srcEdgeCount, ref NativeArray<int2> dstEdges, ref int dstEdgeCount, NativeArray<int> srcIndices, int srcIndexCount, ref NativeArray<int> dstIndices, ref int dstIndexCount, NativeArray<float2> srcVertices, int srcVertexCount, ref NativeArray<float2> dstVertices, ref int dstVertexCount)
		{
			dstEdgeCount = srcEdgeCount;
			dstIndexCount = srcIndexCount;
			dstVertexCount = srcVertexCount;
			ModuleHandle.Copy<int2>(srcEdges, dstEdges, srcEdgeCount);
			ModuleHandle.Copy<int>(srcIndices, dstIndices, srcIndexCount);
			ModuleHandle.Copy<float2>(srcVertices, dstVertices, srcVertexCount);
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0003D9D8 File Offset: 0x0003BBD8
		private static void GraphConditioner(NativeArray<float2> points, ref NativeArray<float2> pgPoints, ref int pgPointCount, ref NativeArray<int2> pgEdges, ref int pgEdgeCount, bool resetTopology)
		{
			float2 @float = new float2(float.PositiveInfinity, float.PositiveInfinity);
			float2 float2 = float2.zero;
			for (int i = 0; i < points.Length; i++)
			{
				@float = math.min(points[i], @float);
				float2 = math.max(points[i], float2);
			}
			float2 float3 = (float2 - @float) * 0.5f;
			float num = 0.0001f;
			pgPointCount = (resetTopology ? 0 : pgPointCount);
			int num2 = pgPointCount;
			int num3 = pgPointCount;
			pgPointCount = num3 + 1;
			pgPoints[num3] = new float2(@float.x, @float.y);
			num3 = pgPointCount;
			pgPointCount = num3 + 1;
			pgPoints[num3] = new float2(@float.x - num, @float.y + float3.y);
			num3 = pgPointCount;
			pgPointCount = num3 + 1;
			pgPoints[num3] = new float2(@float.x, float2.y);
			num3 = pgPointCount;
			pgPointCount = num3 + 1;
			pgPoints[num3] = new float2(@float.x + float3.x, float2.y + num);
			num3 = pgPointCount;
			pgPointCount = num3 + 1;
			pgPoints[num3] = new float2(float2.x, float2.y);
			num3 = pgPointCount;
			pgPointCount = num3 + 1;
			pgPoints[num3] = new float2(float2.x + num, @float.y + float3.y);
			num3 = pgPointCount;
			pgPointCount = num3 + 1;
			pgPoints[num3] = new float2(float2.x, @float.y);
			num3 = pgPointCount;
			pgPointCount = num3 + 1;
			pgPoints[num3] = new float2(@float.x + float3.x, @float.y - num);
			pgEdgeCount = 8;
			pgEdges[0] = new int2(num2, num2 + 1);
			pgEdges[1] = new int2(num2 + 1, num2 + 2);
			pgEdges[2] = new int2(num2 + 2, num2 + 3);
			pgEdges[3] = new int2(num2 + 3, num2 + 4);
			pgEdges[4] = new int2(num2 + 4, num2 + 5);
			pgEdges[5] = new int2(num2 + 5, num2 + 6);
			pgEdges[6] = new int2(num2 + 6, num2 + 7);
			pgEdges[7] = new int2(num2 + 7, num2);
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0003DC38 File Offset: 0x0003BE38
		private static void Reorder(int startVertexCount, int index, ref NativeArray<int> indices, ref int indexCount, ref NativeArray<float2> vertices, ref int vertexCount)
		{
			bool flag = false;
			for (int i = 0; i < indexCount; i++)
			{
				if (indices[i] == index)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				vertexCount--;
				vertices[index] = vertices[vertexCount];
				for (int j = 0; j < indexCount; j++)
				{
					if (indices[j] == vertexCount)
					{
						indices[j] = index;
					}
				}
			}
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0003DCA0 File Offset: 0x0003BEA0
		internal static void VertexCleanupConditioner(int startVertexCount, ref NativeArray<int> indices, ref int indexCount, ref NativeArray<float2> vertices, ref int vertexCount)
		{
			for (int i = startVertexCount; i < vertexCount; i++)
			{
				ModuleHandle.Reorder(startVertexCount, i, ref indices, ref indexCount, ref vertices, ref vertexCount);
			}
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0003DCC8 File Offset: 0x0003BEC8
		public static float4 ConvexQuad(Allocator allocator, NativeArray<float2> points, NativeArray<int2> edges, ref NativeArray<float2> outVertices, ref int outVertexCount, ref NativeArray<int> outIndices, ref int outIndexCount, ref NativeArray<int2> outEdges, ref int outEdgeCount)
		{
			float4 zero = float4.zero;
			outEdgeCount = 0;
			outIndexCount = 0;
			outVertexCount = 0;
			if (points.Length < 3 || points.Length >= ModuleHandle.kMaxVertexCount)
			{
				return zero;
			}
			int num = 0;
			int num2 = 0;
			NativeArray<int2> nativeArray = new NativeArray<int2>(ModuleHandle.kMaxEdgeCount, allocator, NativeArrayOptions.ClearMemory);
			NativeArray<float2> nativeArray2 = new NativeArray<float2>(ModuleHandle.kMaxVertexCount, allocator, NativeArrayOptions.ClearMemory);
			ModuleHandle.GraphConditioner(points, ref nativeArray2, ref num2, ref nativeArray, ref num, true);
			Tessellator.Tessellate(allocator, nativeArray2, num2, nativeArray, num, ref outVertices, ref outVertexCount, ref outIndices, ref outIndexCount);
			nativeArray2.Dispose();
			nativeArray.Dispose();
			return zero;
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0003DD54 File Offset: 0x0003BF54
		public static float4 Tessellate(Allocator allocator, NativeArray<float2> points, NativeArray<int2> edges, ref NativeArray<float2> outVertices, ref int outVertexCount, ref NativeArray<int> outIndices, ref int outIndexCount, ref NativeArray<int2> outEdges, ref int outEdgeCount)
		{
			float4 zero = float4.zero;
			outEdgeCount = 0;
			outIndexCount = 0;
			outVertexCount = 0;
			if (points.Length < 3 || points.Length >= ModuleHandle.kMaxVertexCount)
			{
				return zero;
			}
			bool flag = false;
			bool flag2 = false;
			int num = 0;
			int num2 = 0;
			NativeArray<int2> nativeArray = new NativeArray<int2>(edges.Length * 8, allocator, NativeArrayOptions.ClearMemory);
			NativeArray<float2> nativeArray2 = new NativeArray<float2>(points.Length * 4, allocator, NativeArrayOptions.ClearMemory);
			if (edges.Length != 0)
			{
				flag = PlanarGraph.Validate(allocator, points, points.Length, edges, edges.Length, ref nativeArray2, ref num2, ref nativeArray, ref num);
			}
			if (!flag)
			{
				outEdgeCount = edges.Length;
				outVertexCount = points.Length;
				ModuleHandle.Copy<int2>(edges, outEdges, edges.Length);
				ModuleHandle.Copy<float2>(points, outVertices, points.Length);
			}
			if (num2 > 2 && num > 2)
			{
				NativeArray<int> nativeArray3 = new NativeArray<int>(num2 * 8, allocator, NativeArrayOptions.ClearMemory);
				NativeArray<float2> nativeArray4 = new NativeArray<float2>(num2 * 4, allocator, NativeArrayOptions.ClearMemory);
				int num3 = 0;
				int num4 = 0;
				flag = Tessellator.Tessellate(allocator, nativeArray2, num2, nativeArray, num, ref nativeArray4, ref num4, ref nativeArray3, ref num3);
				if (flag)
				{
					ModuleHandle.TransferOutput(nativeArray, num, ref outEdges, ref outEdgeCount, nativeArray3, num3, ref outIndices, ref outIndexCount, nativeArray4, num4, ref outVertices, ref outVertexCount);
					if (flag2)
					{
						outEdgeCount = 0;
					}
				}
				nativeArray4.Dispose();
				nativeArray3.Dispose();
			}
			nativeArray2.Dispose();
			nativeArray.Dispose();
			return zero;
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0003DEA4 File Offset: 0x0003C0A4
		public static float4 Subdivide(Allocator allocator, NativeArray<float2> points, NativeArray<int2> edges, ref NativeArray<float2> outVertices, ref int outVertexCount, ref NativeArray<int> outIndices, ref int outIndexCount, ref NativeArray<int2> outEdges, ref int outEdgeCount, float areaFactor, float targetArea, int refineIterations, int smoothenIterations)
		{
			float4 zero = float4.zero;
			outEdgeCount = 0;
			outIndexCount = 0;
			outVertexCount = 0;
			if (points.Length < 3 || points.Length >= ModuleHandle.kMaxVertexCount || edges.Length == 0)
			{
				return zero;
			}
			int num = 0;
			int num2 = 0;
			NativeArray<int> nativeArray = new NativeArray<int>(ModuleHandle.kMaxIndexCount, allocator, NativeArrayOptions.ClearMemory);
			NativeArray<float2> nativeArray2 = new NativeArray<float2>(ModuleHandle.kMaxVertexCount, allocator, NativeArrayOptions.ClearMemory);
			bool flag = Tessellator.Tessellate(allocator, points, points.Length, edges, edges.Length, ref nativeArray2, ref num2, ref nativeArray, ref num);
			bool flag2 = false;
			bool flag3 = targetArea != 0f || areaFactor != 0f;
			if (flag && flag3)
			{
				float num3 = 0f;
				int num4 = 0;
				int num5 = 0;
				int num6 = 0;
				int num7 = 0;
				NativeArray<int2> nativeArray3 = new NativeArray<int2>(ModuleHandle.kMaxEdgeCount, allocator, NativeArrayOptions.ClearMemory);
				NativeArray<float2> nativeArray4 = new NativeArray<float2>(ModuleHandle.kMaxVertexCount, allocator, NativeArrayOptions.ClearMemory);
				NativeArray<int> nativeArray5 = new NativeArray<int>(ModuleHandle.kMaxIndexCount, allocator, NativeArrayOptions.ClearMemory);
				NativeArray<float2> nativeArray6 = new NativeArray<float2>(ModuleHandle.kMaxVertexCount, allocator, NativeArrayOptions.ClearMemory);
				zero.x = 0f;
				refineIterations = Math.Min(refineIterations, ModuleHandle.kMaxRefineIterations);
				if (targetArea != 0f)
				{
					float num8 = targetArea / 10f;
					while (targetArea < (float)ModuleHandle.kMaxArea)
					{
						if (refineIterations <= 0)
						{
							break;
						}
						ModuleHandle.CopyGraph(points, points.Length, ref nativeArray4, ref num5, edges, edges.Length, ref nativeArray3, ref num4);
						ModuleHandle.CopyGeometry(nativeArray, num, ref nativeArray5, ref num6, nativeArray2, num2, ref nativeArray6, ref num7);
						flag2 = Refinery.Condition(allocator, areaFactor, targetArea, ref nativeArray4, ref num5, ref nativeArray3, ref num4, ref nativeArray6, ref num7, ref nativeArray5, ref num6, ref num3);
						if (flag2 && num6 > num5)
						{
							zero.x = areaFactor;
							ModuleHandle.TransferOutput(nativeArray3, num4, ref outEdges, ref outEdgeCount, nativeArray5, num6, ref outIndices, ref outIndexCount, nativeArray6, num7, ref outVertices, ref outVertexCount);
							break;
						}
						flag2 = false;
						targetArea += num8;
						refineIterations--;
					}
				}
				else if (areaFactor != 0f)
				{
					areaFactor = math.lerp(0.1f, 0.54f, (areaFactor - 0.05f) / 0.45f);
					float num8 = areaFactor / 10f;
					while (areaFactor < 0.8f && refineIterations > 0)
					{
						ModuleHandle.CopyGraph(points, points.Length, ref nativeArray4, ref num5, edges, edges.Length, ref nativeArray3, ref num4);
						ModuleHandle.CopyGeometry(nativeArray, num, ref nativeArray5, ref num6, nativeArray2, num2, ref nativeArray6, ref num7);
						flag2 = Refinery.Condition(allocator, areaFactor, targetArea, ref nativeArray4, ref num5, ref nativeArray3, ref num4, ref nativeArray6, ref num7, ref nativeArray5, ref num6, ref num3);
						if (flag2 && num6 > num5)
						{
							zero.x = areaFactor;
							ModuleHandle.TransferOutput(nativeArray3, num4, ref outEdges, ref outEdgeCount, nativeArray5, num6, ref outIndices, ref outIndexCount, nativeArray6, num7, ref outVertices, ref outVertexCount);
							break;
						}
						flag2 = false;
						areaFactor += num8;
						refineIterations--;
					}
				}
				if (flag2)
				{
					if (zero.x != 0f)
					{
						ModuleHandle.VertexCleanupConditioner(num2, ref nativeArray5, ref num6, ref nativeArray6, ref num7);
					}
					zero.y = 0f;
					smoothenIterations = math.clamp(smoothenIterations, 0, ModuleHandle.kMaxSmoothenIterations);
					while (smoothenIterations > 0 && Smoothen.Condition(allocator, ref nativeArray4, num5, nativeArray3, num4, ref nativeArray6, ref num7, ref nativeArray5, ref num6))
					{
						zero.y = (float)smoothenIterations;
						ModuleHandle.TransferOutput(nativeArray3, num4, ref outEdges, ref outEdgeCount, nativeArray5, num6, ref outIndices, ref outIndexCount, nativeArray6, num7, ref outVertices, ref outVertexCount);
						smoothenIterations--;
					}
					if (zero.y != 0f)
					{
						ModuleHandle.VertexCleanupConditioner(num2, ref outIndices, ref outIndexCount, ref outVertices, ref outVertexCount);
					}
				}
				nativeArray6.Dispose();
				nativeArray5.Dispose();
				nativeArray4.Dispose();
				nativeArray3.Dispose();
			}
			if (flag && !flag2)
			{
				ModuleHandle.TransferOutput(edges, edges.Length, ref outEdges, ref outEdgeCount, nativeArray, num, ref outIndices, ref outIndexCount, nativeArray2, num2, ref outVertices, ref outVertexCount);
			}
			nativeArray2.Dispose();
			nativeArray.Dispose();
			return zero;
		}

		// Token: 0x04000855 RID: 2133
		internal static readonly int kMaxArea = 65536;

		// Token: 0x04000856 RID: 2134
		internal static readonly int kMaxEdgeCount = 65536;

		// Token: 0x04000857 RID: 2135
		internal static readonly int kMaxIndexCount = 65536;

		// Token: 0x04000858 RID: 2136
		internal static readonly int kMaxVertexCount = 65536;

		// Token: 0x04000859 RID: 2137
		internal static readonly int kMaxTriangleCount = ModuleHandle.kMaxIndexCount / 3;

		// Token: 0x0400085A RID: 2138
		internal static readonly int kMaxRefineIterations = 48;

		// Token: 0x0400085B RID: 2139
		internal static readonly int kMaxSmoothenIterations = 256;

		// Token: 0x0400085C RID: 2140
		internal static readonly float kIncrementAreaFactor = 1.2f;
	}
}
