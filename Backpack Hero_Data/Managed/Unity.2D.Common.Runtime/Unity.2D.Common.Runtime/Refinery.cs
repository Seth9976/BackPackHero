using System;
using Unity.Collections;
using Unity.Mathematics;

namespace UnityEngine.U2D.Common.UTess
{
	// Token: 0x02000007 RID: 7
	internal struct Refinery
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002F41 File Offset: 0x00001141
		private static bool RequiresRefining(UTriangle tri, float maxArea)
		{
			return tri.area > maxArea;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002F4C File Offset: 0x0000114C
		private static void FetchEncroachedSegments(NativeArray<float2> pgPoints, int pgPointCount, NativeArray<int2> pgEdges, int pgEdgeCount, ref Array<UEncroachingSegment> encroach, ref int encroachCount, UCircle c)
		{
			for (int i = 0; i < pgEdgeCount; i++)
			{
				int2 @int = pgEdges[i];
				float2 @float = pgPoints[@int.x];
				float2 float2 = pgPoints[@int.y];
				if (math.any(c.center - @float) && math.any(c.center - float2))
				{
					float2 float3 = @float - float2;
					float2 float4 = (@float + float2) * 0.5f;
					float num = math.length(float3) * 0.5f;
					if (math.length(float4 - c.center) <= num)
					{
						UEncroachingSegment uencroachingSegment = default(UEncroachingSegment);
						uencroachingSegment.a = @float;
						uencroachingSegment.b = float2;
						uencroachingSegment.index = i;
						int num2 = encroachCount;
						encroachCount = num2 + 1;
						encroach[num2] = uencroachingSegment;
					}
				}
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003030 File Offset: 0x00001230
		private static void InsertVertex(ref NativeArray<float2> pgPoints, ref int pgPointCount, float2 newVertex, ref int nid)
		{
			nid = pgPointCount;
			pgPoints[nid] = newVertex;
			pgPointCount++;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003048 File Offset: 0x00001248
		private static void SplitSegments(ref NativeArray<float2> pgPoints, ref int pgPointCount, ref NativeArray<int2> pgEdges, ref int pgEdgeCount, UEncroachingSegment es)
		{
			int index = es.index;
			int2 @int = pgEdges[index];
			float2 @float = pgPoints[@int.x];
			float2 float2 = pgPoints[@int.y];
			float2 float3 = (@float + float2) * 0.5f;
			int num = 0;
			if (math.abs(@int.x - @int.y) == 1)
			{
				num = ((@int.x > @int.y) ? @int.x : @int.y);
				Refinery.InsertVertex(ref pgPoints, ref pgPointCount, float3, ref num);
				int2 int2 = pgEdges[index];
				pgEdges[index] = new int2(int2.x, num);
				for (int i = pgEdgeCount; i > index + 1; i--)
				{
					pgEdges[i] = pgEdges[i - 1];
				}
				pgEdges[index + 1] = new int2(num, int2.y);
				pgEdgeCount++;
				return;
			}
			num = pgPointCount;
			int num2 = pgPointCount;
			pgPointCount = num2 + 1;
			pgPoints[num2] = float3;
			pgEdges[index] = new int2(math.max(@int.x, @int.y), num);
			num2 = pgEdgeCount;
			pgEdgeCount = num2 + 1;
			pgEdges[num2] = new int2(math.min(@int.x, @int.y), num);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003194 File Offset: 0x00001394
		internal static bool Condition(Allocator allocator, float factorArea, float targetArea, ref NativeArray<float2> pgPoints, ref int pgPointCount, ref NativeArray<int2> pgEdges, ref int pgEdgeCount, ref NativeArray<float2> vertices, ref int vertexCount, ref NativeArray<int> indices, ref int indexCount, ref float maxArea)
		{
			maxArea = 0f;
			float num = 0f;
			float num2 = 0f;
			bool flag = false;
			bool flag2 = true;
			int num3 = 0;
			int num4 = -1;
			int num5 = pgPointCount;
			Array<UEncroachingSegment> array = new Array<UEncroachingSegment>(num5, ModuleHandle.kMaxEdgeCount, allocator, NativeArrayOptions.UninitializedMemory);
			Array<UTriangle> array2 = new Array<UTriangle>(num5 * 4, ModuleHandle.kMaxTriangleCount, allocator, NativeArrayOptions.UninitializedMemory);
			ModuleHandle.BuildTriangles(vertices, vertexCount, indices, indexCount, ref array2, ref num3, ref maxArea, ref num2, ref num);
			factorArea = ((factorArea != 0f) ? math.clamp(factorArea, Refinery.kMinAreaFactor, Refinery.kMaxAreaFactor) : factorArea);
			float num6 = maxArea * factorArea;
			num6 = math.max(num6, targetArea);
			while (!flag && flag2)
			{
				for (int i = 0; i < num3; i++)
				{
					if (Refinery.RequiresRefining(array2[i], num6))
					{
						num4 = i;
						break;
					}
				}
				if (num4 != -1)
				{
					UTriangle utriangle = array2[num4];
					int num7 = 0;
					Refinery.FetchEncroachedSegments(pgPoints, pgPointCount, pgEdges, pgEdgeCount, ref array, ref num7, utriangle.c);
					if (num7 != 0)
					{
						for (int j = 0; j < num7; j++)
						{
							Refinery.SplitSegments(ref pgPoints, ref pgPointCount, ref pgEdges, ref pgEdgeCount, array[j]);
						}
					}
					else
					{
						float2 center = utriangle.c.center;
						int num8 = pgPointCount;
						pgPointCount = num8 + 1;
						pgPoints[num8] = center;
					}
					indexCount = 0;
					vertexCount = 0;
					flag2 = Tessellator.Tessellate(allocator, pgPoints, pgPointCount, pgEdges, pgEdgeCount, ref vertices, ref vertexCount, ref indices, ref indexCount);
					num7 = 0;
					num3 = 0;
					num4 = -1;
					if (flag2)
					{
						ModuleHandle.BuildTriangles(vertices, vertexCount, indices, indexCount, ref array2, ref num3, ref maxArea, ref num2, ref num);
					}
					if (pgPointCount - num5 > Refinery.kMaxSteinerCount)
					{
						break;
					}
				}
				else
				{
					flag = true;
				}
			}
			array2.Dispose();
			array.Dispose();
			return flag;
		}

		// Token: 0x0400000C RID: 12
		private static readonly float kMinAreaFactor = 0.0482f;

		// Token: 0x0400000D RID: 13
		private static readonly float kMaxAreaFactor = 0.482f;

		// Token: 0x0400000E RID: 14
		private static readonly int kMaxSteinerCount = 4084;
	}
}
