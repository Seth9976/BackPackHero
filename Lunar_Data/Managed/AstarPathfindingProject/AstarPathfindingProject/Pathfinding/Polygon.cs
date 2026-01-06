using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Pathfinding.Graphs.Navmesh;
using Pathfinding.Util;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200004D RID: 77
	[BurstCompile]
	public static class Polygon
	{
		// Token: 0x060002C1 RID: 705 RVA: 0x0000D30B File Offset: 0x0000B50B
		public static bool ContainsPointXZ(Vector3 a, Vector3 b, Vector3 c, Vector3 p)
		{
			return VectorMath.IsClockwiseMarginXZ(a, b, p) && VectorMath.IsClockwiseMarginXZ(b, c, p) && VectorMath.IsClockwiseMarginXZ(c, a, p);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000D32B File Offset: 0x0000B52B
		public static bool ContainsPointXZ(Int3 a, Int3 b, Int3 c, Int3 p)
		{
			return VectorMath.IsClockwiseOrColinearXZ(a, b, p) && VectorMath.IsClockwiseOrColinearXZ(b, c, p) && VectorMath.IsClockwiseOrColinearXZ(c, a, p);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000D34B File Offset: 0x0000B54B
		public static bool ContainsPoint(Int2 a, Int2 b, Int2 c, Int2 p)
		{
			return VectorMath.IsClockwiseOrColinear(a, b, p) && VectorMath.IsClockwiseOrColinear(b, c, p) && VectorMath.IsClockwiseOrColinear(c, a, p);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000D36C File Offset: 0x0000B56C
		public static bool ContainsPoint(Vector2[] polyPoints, Vector2 p)
		{
			int num = polyPoints.Length - 1;
			bool flag = false;
			int i = 0;
			while (i < polyPoints.Length)
			{
				if (((polyPoints[i].y <= p.y && p.y < polyPoints[num].y) || (polyPoints[num].y <= p.y && p.y < polyPoints[i].y)) && p.x < (polyPoints[num].x - polyPoints[i].x) * (p.y - polyPoints[i].y) / (polyPoints[num].y - polyPoints[i].y) + polyPoints[i].x)
				{
					flag = !flag;
				}
				num = i++;
			}
			return flag;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000D44C File Offset: 0x0000B64C
		public static bool ContainsPointXZ(Vector3[] polyPoints, Vector3 p)
		{
			int num = polyPoints.Length - 1;
			bool flag = false;
			int i = 0;
			while (i < polyPoints.Length)
			{
				if (((polyPoints[i].z <= p.z && p.z < polyPoints[num].z) || (polyPoints[num].z <= p.z && p.z < polyPoints[i].z)) && p.x < (polyPoints[num].x - polyPoints[i].x) * (p.z - polyPoints[i].z) / (polyPoints[num].z - polyPoints[i].z) + polyPoints[i].x)
				{
					flag = !flag;
				}
				num = i++;
			}
			return flag;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000D529 File Offset: 0x0000B729
		[BurstCompile]
		public static bool ContainsPoint(ref int3 aWorld, ref int3 bWorld, ref int3 cWorld, ref int3 pWorld, ref NativeMovementPlane movementPlane)
		{
			return Polygon.ContainsPoint_000002C6$BurstDirectCall.Invoke(ref aWorld, ref bWorld, ref cWorld, ref pWorld, ref movementPlane);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000D538 File Offset: 0x0000B738
		public static bool ContainsPoint(ref int3 aWorld, ref int3 bWorld, ref int3 cWorld, ref int3 pWorld, in float2x3 planeProjection)
		{
			int2x3 int2x = new int2x3(planeProjection * 1024f);
			int4 @int = new int4(aWorld.x, bWorld.x, cWorld.x, pWorld.x);
			int4 int2 = new int4(aWorld.y, bWorld.y, cWorld.y, pWorld.y);
			int4 int3 = new int4(aWorld.z, bWorld.z, cWorld.z, pWorld.z);
			int4 int4 = (@int * int2x.c0.x + int2 * int2x.c1.x + int3 * int2x.c2.x) / 1024;
			int4 int5 = (@int * int2x.c0.y + int2 * int2x.c1.y + int3 * int2x.c2.y) / 1024;
			int3 int6 = int4.yzx - int4.xyz;
			int3 int7 = int5.www - int5.xyz;
			int3 int8 = int4.www - int4.xyz;
			int3 int9 = int5.yzx - int5.xyz;
			long num = (long)int6.x * (long)int7.x - (long)int8.x * (long)int9.x;
			long num2 = (long)int6.y * (long)int7.y - (long)int8.y * (long)int9.y;
			long num3 = (long)int6.z * (long)int7.z - (long)int8.z * (long)int9.z;
			return ((num >= 0L) & (num2 >= 0L) & (num3 >= 0L)) | ((num <= 0L) & (num2 <= 0L) & (num3 <= 0L));
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000D740 File Offset: 0x0000B940
		public static int SampleYCoordinateInTriangle(Int3 p1, Int3 p2, Int3 p3, Int3 p)
		{
			double num = (double)(p2.z - p3.z) * (double)(p1.x - p3.x) + (double)(p3.x - p2.x) * (double)(p1.z - p3.z);
			double num2 = ((double)(p2.z - p3.z) * (double)(p.x - p3.x) + (double)(p3.x - p2.x) * (double)(p.z - p3.z)) / num;
			double num3 = ((double)(p3.z - p1.z) * (double)(p.x - p3.x) + (double)(p1.x - p3.x) * (double)(p.z - p3.z)) / num;
			return (int)Math.Round(num2 * (double)p1.y + num3 * (double)p2.y + (1.0 - num2 - num3) * (double)p3.y);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000D834 File Offset: 0x0000BA34
		public static Vector3[] ConvexHullXZ(Vector3[] points)
		{
			if (points.Length == 0)
			{
				return new Vector3[0];
			}
			List<Vector3> list = ListPool<Vector3>.Claim();
			int num = 0;
			for (int i = 1; i < points.Length; i++)
			{
				if (points[i].x < points[num].x)
				{
					num = i;
				}
			}
			int num2 = num;
			int num3 = 0;
			for (;;)
			{
				list.Add(points[num]);
				int num4 = 0;
				for (int j = 0; j < points.Length; j++)
				{
					if (num4 == num || !VectorMath.RightOrColinearXZ(points[num], points[num4], points[j]))
					{
						num4 = j;
					}
				}
				num = num4;
				num3++;
				if (num3 > 10000)
				{
					break;
				}
				if (num == num2)
				{
					goto IL_00AF;
				}
			}
			Debug.LogWarning("Infinite Loop in Convex Hull Calculation");
			IL_00AF:
			Vector3[] array = list.ToArray();
			ListPool<Vector3>.Release(list);
			return array;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000D900 File Offset: 0x0000BB00
		public static Vector2 ClosestPointOnTriangle(Vector2 a, Vector2 b, Vector2 c, Vector2 p)
		{
			Vector2 vector = b - a;
			Vector2 vector2 = c - a;
			Vector2 vector3 = p - a;
			float num = Vector2.Dot(vector, vector3);
			float num2 = Vector2.Dot(vector2, vector3);
			if (num <= 0f && num2 <= 0f)
			{
				return a;
			}
			Vector2 vector4 = p - b;
			float num3 = Vector2.Dot(vector, vector4);
			float num4 = Vector2.Dot(vector2, vector4);
			if (num3 >= 0f && num4 <= num3)
			{
				return b;
			}
			if (num >= 0f && num3 <= 0f && num * num4 - num3 * num2 <= 0f)
			{
				float num5 = num / (num - num3);
				return a + vector * num5;
			}
			Vector2 vector5 = p - c;
			float num6 = Vector2.Dot(vector, vector5);
			float num7 = Vector2.Dot(vector2, vector5);
			if (num7 >= 0f && num6 <= num7)
			{
				return c;
			}
			if (num2 >= 0f && num7 <= 0f && num6 * num2 - num * num7 <= 0f)
			{
				float num8 = num2 / (num2 - num7);
				return a + vector2 * num8;
			}
			if (num4 - num3 >= 0f && num6 - num7 >= 0f && num3 * num7 - num6 * num4 <= 0f)
			{
				float num9 = (num4 - num3) / (num4 - num3 + (num6 - num7));
				return b + (c - b) * num9;
			}
			return p;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000DA70 File Offset: 0x0000BC70
		public static Vector3 ClosestPointOnTriangleXZ(Vector3 a, Vector3 b, Vector3 c, Vector3 p)
		{
			Vector2 vector = new Vector2(b.x - a.x, b.z - a.z);
			Vector2 vector2 = new Vector2(c.x - a.x, c.z - a.z);
			Vector2 vector3 = new Vector2(p.x - a.x, p.z - a.z);
			float num = Vector2.Dot(vector, vector3);
			float num2 = Vector2.Dot(vector2, vector3);
			if (num <= 0f && num2 <= 0f)
			{
				return a;
			}
			Vector2 vector4 = new Vector2(p.x - b.x, p.z - b.z);
			float num3 = Vector2.Dot(vector, vector4);
			float num4 = Vector2.Dot(vector2, vector4);
			if (num3 >= 0f && num4 <= num3)
			{
				return b;
			}
			float num5 = num * num4 - num3 * num2;
			if (num >= 0f && num3 <= 0f && num5 <= 0f)
			{
				float num6 = num / (num - num3);
				return (1f - num6) * a + num6 * b;
			}
			Vector2 vector5 = new Vector2(p.x - c.x, p.z - c.z);
			float num7 = Vector2.Dot(vector, vector5);
			float num8 = Vector2.Dot(vector2, vector5);
			if (num8 >= 0f && num7 <= num8)
			{
				return c;
			}
			float num9 = num7 * num2 - num * num8;
			if (num2 >= 0f && num8 <= 0f && num9 <= 0f)
			{
				float num10 = num2 / (num2 - num8);
				return (1f - num10) * a + num10 * c;
			}
			float num11 = num3 * num8 - num7 * num4;
			if (num4 - num3 >= 0f && num7 - num8 >= 0f && num11 <= 0f)
			{
				float num12 = (num4 - num3) / (num4 - num3 + (num7 - num8));
				return b + (c - b) * num12;
			}
			float num13 = 1f / (num11 + num9 + num5);
			float num14 = num9 * num13;
			float num15 = num5 * num13;
			return new Vector3(p.x, (1f - num14 - num15) * a.y + num14 * b.y + num15 * c.y, p.z);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000DCD4 File Offset: 0x0000BED4
		public static float3 ClosestPointOnTriangle(float3 a, float3 b, float3 c, float3 p)
		{
			float3 @float;
			Polygon.ClosestPointOnTriangleByRef(in a, in b, in c, in p, out @float);
			return @float;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000DCF2 File Offset: 0x0000BEF2
		[BurstCompile]
		public static bool ClosestPointOnTriangleByRef(in float3 a, in float3 b, in float3 c, in float3 p, [NoAlias] out float3 output)
		{
			return Polygon.ClosestPointOnTriangleByRef_000002CD$BurstDirectCall.Invoke(in a, in b, in c, in p, out output);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000DD00 File Offset: 0x0000BF00
		public static float3 ClosestPointOnTriangleBarycentric(float2 a, float2 b, float2 c, float2 p)
		{
			float2 @float = b - a;
			float2 float2 = c - a;
			float2 float3 = p - a;
			float num = math.dot(@float, float3);
			float num2 = math.dot(float2, float3);
			if (num <= 0f && num2 <= 0f)
			{
				return new float3(1f, 0f, 0f);
			}
			float2 float4 = p - b;
			float num3 = math.dot(@float, float4);
			float num4 = math.dot(float2, float4);
			if (num3 >= 0f && num4 <= num3)
			{
				return new float3(0f, 1f, 0f);
			}
			float num5 = num * num4 - num3 * num2;
			if (num >= 0f && num3 <= 0f && num5 <= 0f)
			{
				float num6 = num / (num - num3);
				return new float3(1f - num6, num6, 0f);
			}
			float2 float5 = p - c;
			float num7 = math.dot(@float, float5);
			float num8 = math.dot(float2, float5);
			if (num8 >= 0f && num7 <= num8)
			{
				return new float3(0f, 0f, 1f);
			}
			float num9 = num7 * num2 - num * num8;
			if (num2 >= 0f && num8 <= 0f && num9 <= 0f)
			{
				float num10 = num2 / (num2 - num8);
				return new float3(1f - num10, 0f, num10);
			}
			float num11 = num3 * num8 - num7 * num4;
			if (num4 - num3 >= 0f && num7 - num8 >= 0f && num11 <= 0f)
			{
				float num12 = (num4 - num3) / (num4 - num3 + (num7 - num8));
				return new float3(0f, 1f - num12, num12);
			}
			float num13 = 1f / (num11 + num9 + num5);
			float num14 = num9 * num13;
			float num15 = num5 * num13;
			return new float3(1f - num14 - num15, num14, num15);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000DEEF File Offset: 0x0000C0EF
		[BurstCompile]
		public static void ClosestPointOnTriangleProjected(ref Int3 vi1, ref Int3 vi2, ref Int3 vi3, ref BBTree.ProjectionParams projection, ref float3 point, [NoAlias] out float3 closest, [NoAlias] out float sqrDist, [NoAlias] out float distAlongProjection)
		{
			Polygon.ClosestPointOnTriangleProjected_000002CF$BurstDirectCall.Invoke(ref vi1, ref vi2, ref vi3, ref projection, ref point, out closest, out sqrDist, out distAlongProjection);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000DF04 File Offset: 0x0000C104
		public static void CompressMesh(List<Int3> vertices, List<int> triangles, out Int3[] outVertices, out int[] outTriangles)
		{
			Dictionary<Int3, int> dictionary = Polygon.cached_Int3_int_dict;
			dictionary.Clear();
			int[] array = ArrayPool<int>.Claim(vertices.Count);
			int num = 0;
			for (int i = 0; i < vertices.Count; i++)
			{
				int num2;
				if (!dictionary.TryGetValue(vertices[i], out num2) && !dictionary.TryGetValue(vertices[i] + new Int3(0, 1, 0), out num2) && !dictionary.TryGetValue(vertices[i] + new Int3(0, -1, 0), out num2))
				{
					dictionary.Add(vertices[i], num);
					array[i] = num;
					vertices[num] = vertices[i];
					num++;
				}
				else
				{
					array[i] = num2;
				}
			}
			outTriangles = new int[triangles.Count];
			for (int j = 0; j < outTriangles.Length; j++)
			{
				outTriangles[j] = array[triangles[j]];
			}
			outVertices = new Int3[num];
			for (int k = 0; k < num; k++)
			{
				outVertices[k] = vertices[k];
			}
			ArrayPool<int>.Release(ref array, false);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000E018 File Offset: 0x0000C218
		public static void TraceContours(Dictionary<int, int> outline, HashSet<int> hasInEdge, Action<List<int>, bool> results)
		{
			List<int> list = ListPool<int>.Claim();
			List<int> list2 = ListPool<int>.Claim();
			list2.AddRange(outline.Keys);
			for (int i = 0; i <= 1; i++)
			{
				bool flag = i == 1;
				for (int j = 0; j < list2.Count; j++)
				{
					int num = list2[j];
					if (flag || !hasInEdge.Contains(num))
					{
						int num2 = num;
						list.Clear();
						list.Add(num2);
						while (outline.ContainsKey(num2))
						{
							int num3 = outline[num2];
							outline.Remove(num2);
							list.Add(num3);
							if (num3 == num)
							{
								break;
							}
							num2 = num3;
						}
						if (list.Count > 1)
						{
							results(list, flag);
						}
					}
				}
			}
			ListPool<int>.Release(ref list2);
			ListPool<int>.Release(ref list);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000E0E4 File Offset: 0x0000C2E4
		public static void Subdivide(List<Vector3> points, List<Vector3> result, int subSegments)
		{
			for (int i = 0; i < points.Count - 1; i++)
			{
				for (int j = 0; j < subSegments; j++)
				{
					result.Add(Vector3.Lerp(points[i], points[i + 1], (float)j / (float)subSegments));
				}
			}
			result.Add(points[points.Count - 1]);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000E150 File Offset: 0x0000C350
		[BurstCompile]
		[MethodImpl(256)]
		public static bool ContainsPoint$BurstManaged(ref int3 aWorld, ref int3 bWorld, ref int3 cWorld, ref int3 pWorld, ref NativeMovementPlane movementPlane)
		{
			float3x3 float3x = new float3x3(movementPlane.rotation.value);
			float2x3 float2x = math.transpose(new float3x2(float3x.c0, float3x.c2));
			return Polygon.ContainsPoint(ref aWorld, ref bWorld, ref cWorld, ref pWorld, in float2x);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000E198 File Offset: 0x0000C398
		[BurstCompile]
		[MethodImpl(256)]
		public static bool ClosestPointOnTriangleByRef$BurstManaged(in float3 a, in float3 b, in float3 c, in float3 p, [NoAlias] out float3 output)
		{
			float3 @float = b - a;
			float3 float2 = c - a;
			float3 float3 = p - a;
			float num = math.dot(@float, float3);
			float num2 = math.dot(float2, float3);
			if (num <= 0f && num2 <= 0f)
			{
				output = a;
				return false;
			}
			float3 float4 = p - b;
			float num3 = math.dot(@float, float4);
			float num4 = math.dot(float2, float4);
			if (num3 >= 0f && num4 <= num3)
			{
				output = b;
				return false;
			}
			float num5 = num * num4 - num3 * num2;
			if (num >= 0f && num3 <= 0f && num5 <= 0f)
			{
				float num6 = num / (num - num3);
				output = a + @float * num6;
				return false;
			}
			float3 float5 = p - c;
			float num7 = math.dot(@float, float5);
			float num8 = math.dot(float2, float5);
			if (num8 >= 0f && num7 <= num8)
			{
				output = c;
				return false;
			}
			float num9 = num7 * num2 - num * num8;
			if (num2 >= 0f && num8 <= 0f && num9 <= 0f)
			{
				float num10 = num2 / (num2 - num8);
				output = a + float2 * num10;
				return false;
			}
			float num11 = num3 * num8 - num7 * num4;
			if (num4 - num3 >= 0f && num7 - num8 >= 0f && num11 <= 0f)
			{
				float num12 = (num4 - num3) / (num4 - num3 + (num7 - num8));
				output = b + (c - b) * num12;
				return false;
			}
			float num13 = 1f / (num11 + num9 + num5);
			float num14 = num9 * num13;
			float num15 = num5 * num13;
			output = a + @float * num14 + float2 * num15;
			return true;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000E3E0 File Offset: 0x0000C5E0
		[BurstCompile]
		[MethodImpl(256)]
		public static void ClosestPointOnTriangleProjected$BurstManaged(ref Int3 vi1, ref Int3 vi2, ref Int3 vi3, ref BBTree.ProjectionParams projection, ref float3 point, [NoAlias] out float3 closest, [NoAlias] out float sqrDist, [NoAlias] out float distAlongProjection)
		{
			float3 @float = (float3)vi1;
			float3 float2 = (float3)vi2;
			float3 float3 = (float3)vi3;
			float2 float4 = math.mul(projection.planeProjection, @float);
			float2 float5 = math.mul(projection.planeProjection, float2);
			float2 float6 = math.mul(projection.planeProjection, float3);
			float2 float7 = math.mul(projection.planeProjection, point);
			float3 float8 = Polygon.ClosestPointOnTriangleBarycentric(float4, float5, float6, float7);
			closest = @float * float8.x + float2 * float8.y + float3 * float8.z;
			float2 float9 = float4 * float8.x + float5 * float8.y + float6 * float8.z;
			distAlongProjection = math.abs(math.dot(closest - point, projection.projectionAxis));
			float num = math.length(float9 - float7);
			if (num < 0.01f)
			{
				int3 @int = (int3)vi1;
				int3 int2 = (int3)vi2;
				int3 int3 = (int3)vi3;
				int3 int4 = (int3)((Int3)point);
				if (Polygon.ContainsPoint(ref @int, ref int2, ref int3, ref int4, in projection.planeProjection))
				{
					num = 0f;
				}
			}
			float num2 = num + distAlongProjection * projection.distanceScaleAlongProjectionAxis;
			sqrDist = num2 * num2;
		}

		// Token: 0x040001D5 RID: 469
		private static readonly Dictionary<Int3, int> cached_Int3_int_dict = new Dictionary<Int3, int>();

		// Token: 0x0200004E RID: 78
		// (Invoke) Token: 0x060002D8 RID: 728
		public delegate bool ContainsPoint_000002C6$PostfixBurstDelegate(ref int3 aWorld, ref int3 bWorld, ref int3 cWorld, ref int3 pWorld, ref NativeMovementPlane movementPlane);

		// Token: 0x0200004F RID: 79
		internal static class ContainsPoint_000002C6$BurstDirectCall
		{
			// Token: 0x060002DB RID: 731 RVA: 0x0000E571 File Offset: 0x0000C771
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (Polygon.ContainsPoint_000002C6$BurstDirectCall.Pointer == 0)
				{
					Polygon.ContainsPoint_000002C6$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(Polygon.ContainsPoint_000002C6$BurstDirectCall.DeferredCompilation, methodof(Polygon.ContainsPoint$BurstManaged(ref int3, ref int3, ref int3, ref int3, ref NativeMovementPlane)).MethodHandle, typeof(Polygon.ContainsPoint_000002C6$PostfixBurstDelegate).TypeHandle);
				}
				A_0 = Polygon.ContainsPoint_000002C6$BurstDirectCall.Pointer;
			}

			// Token: 0x060002DC RID: 732 RVA: 0x0000E5A0 File Offset: 0x0000C7A0
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				Polygon.ContainsPoint_000002C6$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x060002DD RID: 733 RVA: 0x0000E5B8 File Offset: 0x0000C7B8
			public static void Constructor()
			{
				Polygon.ContainsPoint_000002C6$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(Polygon.ContainsPoint(ref int3, ref int3, ref int3, ref int3, ref NativeMovementPlane)).MethodHandle);
			}

			// Token: 0x060002DE RID: 734 RVA: 0x000033F6 File Offset: 0x000015F6
			public static void Initialize()
			{
			}

			// Token: 0x060002DF RID: 735 RVA: 0x0000E5C9 File Offset: 0x0000C7C9
			// Note: this type is marked as 'beforefieldinit'.
			static ContainsPoint_000002C6$BurstDirectCall()
			{
				Polygon.ContainsPoint_000002C6$BurstDirectCall.Constructor();
			}

			// Token: 0x060002E0 RID: 736 RVA: 0x0000E5D0 File Offset: 0x0000C7D0
			public static bool Invoke(ref int3 aWorld, ref int3 bWorld, ref int3 cWorld, ref int3 pWorld, ref NativeMovementPlane movementPlane)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = Polygon.ContainsPoint_000002C6$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						return calli(System.Boolean(Unity.Mathematics.int3&,Unity.Mathematics.int3&,Unity.Mathematics.int3&,Unity.Mathematics.int3&,Pathfinding.Util.NativeMovementPlane&), ref aWorld, ref bWorld, ref cWorld, ref pWorld, ref movementPlane, functionPointer);
					}
				}
				return Polygon.ContainsPoint$BurstManaged(ref aWorld, ref bWorld, ref cWorld, ref pWorld, ref movementPlane);
			}

			// Token: 0x040001D6 RID: 470
			private static IntPtr Pointer;

			// Token: 0x040001D7 RID: 471
			private static IntPtr DeferredCompilation;
		}

		// Token: 0x02000050 RID: 80
		// (Invoke) Token: 0x060002E2 RID: 738
		public delegate bool ClosestPointOnTriangleByRef_000002CD$PostfixBurstDelegate(in float3 a, in float3 b, in float3 c, in float3 p, [NoAlias] out float3 output);

		// Token: 0x02000051 RID: 81
		internal static class ClosestPointOnTriangleByRef_000002CD$BurstDirectCall
		{
			// Token: 0x060002E5 RID: 741 RVA: 0x0000E60B File Offset: 0x0000C80B
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (Polygon.ClosestPointOnTriangleByRef_000002CD$BurstDirectCall.Pointer == 0)
				{
					Polygon.ClosestPointOnTriangleByRef_000002CD$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(Polygon.ClosestPointOnTriangleByRef_000002CD$BurstDirectCall.DeferredCompilation, methodof(Polygon.ClosestPointOnTriangleByRef$BurstManaged(ref float3, ref float3, ref float3, ref float3, ref float3)).MethodHandle, typeof(Polygon.ClosestPointOnTriangleByRef_000002CD$PostfixBurstDelegate).TypeHandle);
				}
				A_0 = Polygon.ClosestPointOnTriangleByRef_000002CD$BurstDirectCall.Pointer;
			}

			// Token: 0x060002E6 RID: 742 RVA: 0x0000E638 File Offset: 0x0000C838
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				Polygon.ClosestPointOnTriangleByRef_000002CD$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x060002E7 RID: 743 RVA: 0x0000E650 File Offset: 0x0000C850
			public static void Constructor()
			{
				Polygon.ClosestPointOnTriangleByRef_000002CD$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(Polygon.ClosestPointOnTriangleByRef(ref float3, ref float3, ref float3, ref float3, ref float3)).MethodHandle);
			}

			// Token: 0x060002E8 RID: 744 RVA: 0x000033F6 File Offset: 0x000015F6
			public static void Initialize()
			{
			}

			// Token: 0x060002E9 RID: 745 RVA: 0x0000E661 File Offset: 0x0000C861
			// Note: this type is marked as 'beforefieldinit'.
			static ClosestPointOnTriangleByRef_000002CD$BurstDirectCall()
			{
				Polygon.ClosestPointOnTriangleByRef_000002CD$BurstDirectCall.Constructor();
			}

			// Token: 0x060002EA RID: 746 RVA: 0x0000E668 File Offset: 0x0000C868
			public static bool Invoke(in float3 a, in float3 b, in float3 c, in float3 p, [NoAlias] out float3 output)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = Polygon.ClosestPointOnTriangleByRef_000002CD$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						return calli(System.Boolean(Unity.Mathematics.float3&,Unity.Mathematics.float3&,Unity.Mathematics.float3&,Unity.Mathematics.float3&,Unity.Mathematics.float3&), ref a, ref b, ref c, ref p, ref output, functionPointer);
					}
				}
				return Polygon.ClosestPointOnTriangleByRef$BurstManaged(in a, in b, in c, in p, out output);
			}

			// Token: 0x040001D8 RID: 472
			private static IntPtr Pointer;

			// Token: 0x040001D9 RID: 473
			private static IntPtr DeferredCompilation;
		}

		// Token: 0x02000052 RID: 82
		// (Invoke) Token: 0x060002EC RID: 748
		public delegate void ClosestPointOnTriangleProjected_000002CF$PostfixBurstDelegate(ref Int3 vi1, ref Int3 vi2, ref Int3 vi3, ref BBTree.ProjectionParams projection, ref float3 point, [NoAlias] out float3 closest, [NoAlias] out float sqrDist, [NoAlias] out float distAlongProjection);

		// Token: 0x02000053 RID: 83
		internal static class ClosestPointOnTriangleProjected_000002CF$BurstDirectCall
		{
			// Token: 0x060002EF RID: 751 RVA: 0x0000E6A3 File Offset: 0x0000C8A3
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (Polygon.ClosestPointOnTriangleProjected_000002CF$BurstDirectCall.Pointer == 0)
				{
					Polygon.ClosestPointOnTriangleProjected_000002CF$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(Polygon.ClosestPointOnTriangleProjected_000002CF$BurstDirectCall.DeferredCompilation, methodof(Polygon.ClosestPointOnTriangleProjected$BurstManaged(ref Int3, ref Int3, ref Int3, ref BBTree.ProjectionParams, ref float3, ref float3, ref float, ref float)).MethodHandle, typeof(Polygon.ClosestPointOnTriangleProjected_000002CF$PostfixBurstDelegate).TypeHandle);
				}
				A_0 = Polygon.ClosestPointOnTriangleProjected_000002CF$BurstDirectCall.Pointer;
			}

			// Token: 0x060002F0 RID: 752 RVA: 0x0000E6D0 File Offset: 0x0000C8D0
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				Polygon.ClosestPointOnTriangleProjected_000002CF$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x060002F1 RID: 753 RVA: 0x0000E6E8 File Offset: 0x0000C8E8
			public static void Constructor()
			{
				Polygon.ClosestPointOnTriangleProjected_000002CF$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(Polygon.ClosestPointOnTriangleProjected(ref Int3, ref Int3, ref Int3, ref BBTree.ProjectionParams, ref float3, ref float3, ref float, ref float)).MethodHandle);
			}

			// Token: 0x060002F2 RID: 754 RVA: 0x000033F6 File Offset: 0x000015F6
			public static void Initialize()
			{
			}

			// Token: 0x060002F3 RID: 755 RVA: 0x0000E6F9 File Offset: 0x0000C8F9
			// Note: this type is marked as 'beforefieldinit'.
			static ClosestPointOnTriangleProjected_000002CF$BurstDirectCall()
			{
				Polygon.ClosestPointOnTriangleProjected_000002CF$BurstDirectCall.Constructor();
			}

			// Token: 0x060002F4 RID: 756 RVA: 0x0000E700 File Offset: 0x0000C900
			public static void Invoke(ref Int3 vi1, ref Int3 vi2, ref Int3 vi3, ref BBTree.ProjectionParams projection, ref float3 point, [NoAlias] out float3 closest, [NoAlias] out float sqrDist, [NoAlias] out float distAlongProjection)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = Polygon.ClosestPointOnTriangleProjected_000002CF$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						calli(System.Void(Pathfinding.Int3&,Pathfinding.Int3&,Pathfinding.Int3&,Pathfinding.Graphs.Navmesh.BBTree/ProjectionParams&,Unity.Mathematics.float3&,Unity.Mathematics.float3&,System.Single&,System.Single&), ref vi1, ref vi2, ref vi3, ref projection, ref point, ref closest, ref sqrDist, ref distAlongProjection, functionPointer);
						return;
					}
				}
				Polygon.ClosestPointOnTriangleProjected$BurstManaged(ref vi1, ref vi2, ref vi3, ref projection, ref point, out closest, out sqrDist, out distAlongProjection);
			}

			// Token: 0x040001DA RID: 474
			private static IntPtr Pointer;

			// Token: 0x040001DB RID: 475
			private static IntPtr DeferredCompilation;
		}
	}
}
