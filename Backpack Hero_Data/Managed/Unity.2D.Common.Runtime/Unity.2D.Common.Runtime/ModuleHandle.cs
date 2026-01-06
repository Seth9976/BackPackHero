using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace UnityEngine.U2D.Common.UTess
{
	// Token: 0x0200001B RID: 27
	internal struct ModuleHandle
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00005B03 File Offset: 0x00003D03
		internal static void Copy<T>(NativeArray<T> src, int srcIndex, NativeArray<T> dst, int dstIndex, int length) where T : struct
		{
			NativeArray<T>.Copy(src, srcIndex, dst, dstIndex, length);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00005B10 File Offset: 0x00003D10
		internal static void Copy<T>(NativeArray<T> src, NativeArray<T> dst, int length) where T : struct
		{
			ModuleHandle.Copy<T>(src, 0, dst, 0, length);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00005B1C File Offset: 0x00003D1C
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

		// Token: 0x0600005D RID: 93 RVA: 0x00005B80 File Offset: 0x00003D80
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

		// Token: 0x0600005E RID: 94 RVA: 0x00005BD4 File Offset: 0x00003DD4
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

		// Token: 0x0600005F RID: 95 RVA: 0x00005C28 File Offset: 0x00003E28
		internal static int GetEqual<T, U, X>(Array<T> values, int count, U check, X condition) where T : struct where U : struct where X : ICondition2<T, U>
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

		// Token: 0x06000060 RID: 96 RVA: 0x00005C84 File Offset: 0x00003E84
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

		// Token: 0x06000061 RID: 97 RVA: 0x00005CE0 File Offset: 0x00003EE0
		internal static float OrientFast(float2 a, float2 b, float2 c)
		{
			float num = 1.110223E-16f;
			float num2 = (3f + 16f * num) * num;
			float num3 = (a.y - c.y) * (b.x - c.x);
			float num4 = (a.x - c.x) * (b.y - c.y);
			float num5 = num3 - num4;
			float num6;
			if (num3 > 0f)
			{
				if (num4 <= 0f)
				{
					return num5;
				}
				num6 = num3 + num4;
			}
			else
			{
				if (num3 >= 0f)
				{
					return num5;
				}
				if (num4 >= 0f)
				{
					return num5;
				}
				num6 = -(num3 + num4);
			}
			float num7 = num2 * num6;
			if (num5 >= num7 || num5 <= -num7)
			{
				return num5;
			}
			return num;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00005D98 File Offset: 0x00003F98
		internal static double OrientFastDouble(double2 a, double2 b, double2 c)
		{
			double num = 1.1102230246251565E-16;
			double num2 = (3.0 + 16.0 * num) * num;
			double num3 = (a.y - c.y) * (b.x - c.x);
			double num4 = (a.x - c.x) * (b.y - c.y);
			double num5 = num3 - num4;
			double num6;
			if (num3 > 0.0)
			{
				if (num4 <= 0.0)
				{
					return num5;
				}
				num6 = num3 + num4;
			}
			else
			{
				if (num3 >= 0.0)
				{
					return num5;
				}
				if (num4 >= 0.0)
				{
					return num5;
				}
				num6 = -(num3 + num4);
			}
			double num7 = num2 * num6;
			if (num5 >= num7 || num5 <= -num7)
			{
				return num5;
			}
			return num;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00005E70 File Offset: 0x00004070
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

		// Token: 0x06000064 RID: 100 RVA: 0x00006053 File Offset: 0x00004253
		internal static bool IsInsideCircle(UCircle c, float2 v)
		{
			return math.distance(v, c.center) < c.radius;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000606C File Offset: 0x0000426C
		internal static float TriangleArea(float2 va, float2 vb, float2 vc)
		{
			float3 @float = new float3(va.x, va.y, 0f);
			float3 float2 = new float3(vb.x, vb.y, 0f);
			float3 float3 = new float3(vc.x, vc.y, 0f);
			return math.abs(math.cross(@float - float2, @float - float3).z) * 0.5f;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000060E4 File Offset: 0x000042E4
		internal static float Sign(float2 p1, float2 p2, float2 p3)
		{
			return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00006120 File Offset: 0x00004320
		internal static bool IsInsideTriangle(float2 pt, float2 v1, float2 v2, float2 v3)
		{
			float num = ModuleHandle.Sign(pt, v1, v2);
			float num2 = ModuleHandle.Sign(pt, v2, v3);
			float num3 = ModuleHandle.Sign(pt, v3, v1);
			bool flag = num < 0f || num2 < 0f || num3 < 0f;
			bool flag2 = num > 0f || num2 > 0f || num3 > 0f;
			return !flag || !flag2;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00006184 File Offset: 0x00004384
		internal static bool IsInsideTriangleApproximate(float2 pt, float2 v1, float2 v2, float2 v3)
		{
			float num = ModuleHandle.TriangleArea(v1, v2, v3);
			float num2 = ModuleHandle.TriangleArea(pt, v1, v2);
			float num3 = ModuleHandle.TriangleArea(pt, v2, v3);
			float num4 = ModuleHandle.TriangleArea(pt, v3, v1);
			float num5 = 1.110223E-16f;
			return Mathf.Abs(num - (num2 + num3 + num4)) < num5;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000061C8 File Offset: 0x000043C8
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

		// Token: 0x0600006A RID: 106 RVA: 0x000062BC File Offset: 0x000044BC
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

		// Token: 0x0600006B RID: 107 RVA: 0x000063AC File Offset: 0x000045AC
		internal static void BuildTriangles(NativeArray<float2> vertices, int vertexCount, NativeArray<int> indices, int indexCount, ref Array<UTriangle> triangles, ref int triangleCount, ref float maxArea, ref float avgArea, ref float minArea)
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

		// Token: 0x0600006C RID: 108 RVA: 0x0000649C File Offset: 0x0000469C
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

		// Token: 0x0600006D RID: 109 RVA: 0x00006638 File Offset: 0x00004838
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

		// Token: 0x0600006E RID: 110 RVA: 0x000067C0 File Offset: 0x000049C0
		private static void CopyGraph(NativeArray<float2> srcPoints, int srcPointCount, ref NativeArray<float2> dstPoints, ref int dstPointCount, NativeArray<int2> srcEdges, int srcEdgeCount, ref NativeArray<int2> dstEdges, ref int dstEdgeCount)
		{
			dstEdgeCount = srcEdgeCount;
			dstPointCount = srcPointCount;
			ModuleHandle.Copy<int2>(srcEdges, dstEdges, srcEdgeCount);
			ModuleHandle.Copy<float2>(srcPoints, dstPoints, srcPointCount);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000067E7 File Offset: 0x000049E7
		private static void CopyGeometry(NativeArray<int> srcIndices, int srcIndexCount, ref NativeArray<int> dstIndices, ref int dstIndexCount, NativeArray<float2> srcVertices, int srcVertexCount, ref NativeArray<float2> dstVertices, ref int dstVertexCount)
		{
			dstIndexCount = srcIndexCount;
			dstVertexCount = srcVertexCount;
			ModuleHandle.Copy<int>(srcIndices, dstIndices, srcIndexCount);
			ModuleHandle.Copy<float2>(srcVertices, dstVertices, srcVertexCount);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000680E File Offset: 0x00004A0E
		private static void TransferOutput(NativeArray<int2> srcEdges, int srcEdgeCount, ref NativeArray<int2> dstEdges, ref int dstEdgeCount, NativeArray<int> srcIndices, int srcIndexCount, ref NativeArray<int> dstIndices, ref int dstIndexCount, NativeArray<float2> srcVertices, int srcVertexCount, ref NativeArray<float2> dstVertices, ref int dstVertexCount)
		{
			dstEdgeCount = srcEdgeCount;
			dstIndexCount = srcIndexCount;
			dstVertexCount = srcVertexCount;
			ModuleHandle.Copy<int2>(srcEdges, dstEdges, srcEdgeCount);
			ModuleHandle.Copy<int>(srcIndices, dstIndices, srcIndexCount);
			ModuleHandle.Copy<float2>(srcVertices, dstVertices, srcVertexCount);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000684C File Offset: 0x00004A4C
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

		// Token: 0x06000072 RID: 114 RVA: 0x00006AAC File Offset: 0x00004CAC
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

		// Token: 0x06000073 RID: 115 RVA: 0x00006B14 File Offset: 0x00004D14
		internal static void VertexCleanupConditioner(int startVertexCount, ref NativeArray<int> indices, ref int indexCount, ref NativeArray<float2> vertices, ref int vertexCount)
		{
			for (int i = startVertexCount; i < vertexCount; i++)
			{
				ModuleHandle.Reorder(startVertexCount, i, ref indices, ref indexCount, ref vertices, ref vertexCount);
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00006B3C File Offset: 0x00004D3C
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

		// Token: 0x06000075 RID: 117 RVA: 0x00006BC8 File Offset: 0x00004DC8
		public static float4 Tessellate(Allocator allocator, in NativeArray<float2> points, in NativeArray<int2> edges, ref NativeArray<float2> outVertices, out int outVertexCount, ref NativeArray<int> outIndices, out int outIndexCount, ref NativeArray<int2> outEdges, out int outEdgeCount)
		{
			float4 zero = float4.zero;
			outEdgeCount = 0;
			outIndexCount = 0;
			outVertexCount = 0;
			NativeArray<float2> nativeArray = points;
			if (nativeArray.Length >= 3)
			{
				nativeArray = points;
				if (nativeArray.Length < ModuleHandle.kMaxVertexCount)
				{
					bool flag = false;
					bool flag2 = false;
					int num = 0;
					int num2 = 0;
					NativeArray<int2> nativeArray2 = edges;
					NativeArray<int2> nativeArray3 = new NativeArray<int2>(nativeArray2.Length * 8, allocator, NativeArrayOptions.ClearMemory);
					nativeArray = points;
					NativeArray<float2> nativeArray4 = new NativeArray<float2>(nativeArray.Length * 4, allocator, NativeArrayOptions.ClearMemory);
					nativeArray2 = edges;
					if (nativeArray2.Length != 0)
					{
						nativeArray = points;
						int length = nativeArray.Length;
						nativeArray2 = edges;
						flag = PlanarGraph.Validate(allocator, in points, length, in edges, nativeArray2.Length, ref nativeArray4, out num2, ref nativeArray3, out num);
					}
					if (!flag)
					{
						nativeArray2 = edges;
						outEdgeCount = nativeArray2.Length;
						nativeArray = points;
						outVertexCount = nativeArray.Length;
						NativeArray<int2> nativeArray5 = edges;
						NativeArray<int2> nativeArray6 = outEdges;
						nativeArray2 = edges;
						ModuleHandle.Copy<int2>(nativeArray5, nativeArray6, nativeArray2.Length);
						NativeArray<float2> nativeArray7 = points;
						NativeArray<float2> nativeArray8 = outVertices;
						nativeArray = points;
						ModuleHandle.Copy<float2>(nativeArray7, nativeArray8, nativeArray.Length);
					}
					if (num2 > 2 && num > 2)
					{
						NativeArray<int> nativeArray9 = new NativeArray<int>(num2 * 8, allocator, NativeArrayOptions.ClearMemory);
						NativeArray<float2> nativeArray10 = new NativeArray<float2>(num2 * 4, allocator, NativeArrayOptions.ClearMemory);
						int num3 = 0;
						int num4 = 0;
						flag = Tessellator.Tessellate(allocator, nativeArray4, num2, nativeArray3, num, ref nativeArray10, ref num4, ref nativeArray9, ref num3);
						if (flag)
						{
							ModuleHandle.TransferOutput(nativeArray3, num, ref outEdges, ref outEdgeCount, nativeArray9, num3, ref outIndices, ref outIndexCount, nativeArray10, num4, ref outVertices, ref outVertexCount);
							if (flag2)
							{
								outEdgeCount = 0;
							}
						}
						nativeArray10.Dispose();
						nativeArray9.Dispose();
					}
					nativeArray4.Dispose();
					nativeArray3.Dispose();
					return zero;
				}
			}
			return zero;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00006D78 File Offset: 0x00004F78
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

		// Token: 0x04000044 RID: 68
		internal static readonly int kMaxArea = 65536;

		// Token: 0x04000045 RID: 69
		internal static readonly int kMaxEdgeCount = 65536;

		// Token: 0x04000046 RID: 70
		internal static readonly int kMaxIndexCount = 65536;

		// Token: 0x04000047 RID: 71
		internal static readonly int kMaxVertexCount = 65536;

		// Token: 0x04000048 RID: 72
		internal static readonly int kMaxTriangleCount = ModuleHandle.kMaxIndexCount / 3;

		// Token: 0x04000049 RID: 73
		internal static readonly int kMaxRefineIterations = 48;

		// Token: 0x0400004A RID: 74
		internal static readonly int kMaxSmoothenIterations = 256;

		// Token: 0x0400004B RID: 75
		internal static readonly float kIncrementAreaFactor = 1.2f;
	}
}
