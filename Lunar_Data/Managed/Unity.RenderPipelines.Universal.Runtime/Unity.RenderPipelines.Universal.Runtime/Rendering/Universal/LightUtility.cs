using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine.Rendering.Universal.UTess;
using UnityEngine.U2D;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000027 RID: 39
	internal static class LightUtility
	{
		// Token: 0x0600016F RID: 367 RVA: 0x0000C6EC File Offset: 0x0000A8EC
		public static bool CheckForChange(Light2D.LightType a, ref Light2D.LightType b)
		{
			bool flag = a != b;
			b = a;
			return flag;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000C6F9 File Offset: 0x0000A8F9
		public static bool CheckForChange(int a, ref int b)
		{
			bool flag = a != b;
			b = a;
			return flag;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000C706 File Offset: 0x0000A906
		public static bool CheckForChange(float a, ref float b)
		{
			bool flag = a != b;
			b = a;
			return flag;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000C713 File Offset: 0x0000A913
		public static bool CheckForChange(bool a, ref bool b)
		{
			bool flag = a != b;
			b = a;
			return flag;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000C720 File Offset: 0x0000A920
		private static bool TestPivot(List<IntPoint> path, int activePoint, long lastPoint)
		{
			for (int i = activePoint; i < path.Count; i++)
			{
				if (path[i].N > lastPoint)
				{
					return true;
				}
			}
			return path[activePoint].N == -1L;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000C760 File Offset: 0x0000A960
		private static List<IntPoint> DegeneratePivots(List<IntPoint> path, List<IntPoint> inPath, ref int interiorStart)
		{
			List<IntPoint> list = new List<IntPoint>();
			long num = path[0].N;
			long num2 = path[0].N;
			for (int i = 1; i < path.Count; i++)
			{
				if (path[i].N != -1L)
				{
					num = Math.Min(num, path[i].N);
					num2 = Math.Max(num2, path[i].N);
				}
			}
			for (long num3 = 0L; num3 < num; num3 += 1L)
			{
				IntPoint intPoint = path[(int)num];
				intPoint.N = num3;
				list.Add(intPoint);
			}
			list.AddRange(path.GetRange(0, path.Count));
			interiorStart = list.Count;
			for (long num4 = num2 + 1L; num4 < (long)inPath.Count; num4 += 1L)
			{
				IntPoint intPoint2 = inPath[(int)num4];
				intPoint2.N = num4;
				list.Add(intPoint2);
			}
			return list;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000C854 File Offset: 0x0000AA54
		private static List<IntPoint> SortPivots(List<IntPoint> outPath, List<IntPoint> inPath)
		{
			List<IntPoint> list = new List<IntPoint>();
			IntPoint intPoint = outPath[0];
			long num = outPath[0].N;
			int num2 = 0;
			bool flag = true;
			for (int i = 1; i < outPath.Count; i++)
			{
				if (num > outPath[i].N && flag && outPath[i].N != -1L)
				{
					num = outPath[i].N;
					num2 = i;
					flag = false;
				}
				else if (outPath[i].N >= num)
				{
					num = outPath[i].N;
					flag = true;
				}
			}
			list.AddRange(outPath.GetRange(num2, outPath.Count - num2));
			list.AddRange(outPath.GetRange(0, num2));
			return list;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000C914 File Offset: 0x0000AB14
		private static List<IntPoint> FixPivots(List<IntPoint> outPath, List<IntPoint> inPath, ref int interiorStart)
		{
			List<IntPoint> list = LightUtility.SortPivots(outPath, inPath);
			long num = list[0].N;
			for (int i = 1; i < list.Count; i++)
			{
				int num2 = ((i == list.Count - 1) ? 0 : (i + 1));
				IntPoint intPoint = list[i - 1];
				IntPoint intPoint2 = list[i];
				IntPoint intPoint3 = list[num2];
				if (intPoint.N > intPoint2.N && LightUtility.TestPivot(list, i, num))
				{
					if (intPoint.N == intPoint3.N)
					{
						intPoint2.N = intPoint.N;
					}
					else
					{
						intPoint2.N = ((num + 1L < (long)inPath.Count) ? (num + 1L) : 0L);
					}
					intPoint2.D = 3L;
					list[i] = intPoint2;
				}
				num = list[i].N;
			}
			int j = 1;
			while (j < list.Count - 1)
			{
				IntPoint intPoint4 = list[j - 1];
				IntPoint intPoint5 = list[j];
				IntPoint intPoint6 = list[j + 1];
				if (intPoint5.N - intPoint4.N > 1L)
				{
					if (intPoint5.N == intPoint6.N)
					{
						IntPoint intPoint7 = intPoint5;
						intPoint7.N -= 1L;
						list[j] = intPoint7;
					}
					else
					{
						IntPoint intPoint8 = intPoint5;
						intPoint8.N -= 1L;
						list.Insert(j, intPoint8);
					}
				}
				else
				{
					j++;
				}
			}
			return LightUtility.DegeneratePivots(list, inPath, ref interiorStart);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000CAA0 File Offset: 0x0000ACA0
		internal static List<Vector2> GetOutlinePath(Vector3[] shapePath, float offsetDistance)
		{
			List<IntPoint> list = new List<IntPoint>();
			List<Vector2> list2 = new List<Vector2>();
			for (int i = 0; i < shapePath.Length; i++)
			{
				Vector2 vector = new Vector2(shapePath[i].x, shapePath[i].y) * 10000f;
				list.Add(new IntPoint((long)vector.x, (long)vector.y));
			}
			List<List<IntPoint>> list3 = new List<List<IntPoint>>();
			ClipperOffset clipperOffset = new ClipperOffset(24.0);
			clipperOffset.AddPath(list, JoinType.jtRound, EndType.etClosedPolygon);
			clipperOffset.Execute(ref list3, (double)(10000f * offsetDistance), list.Count);
			if (list3.Count > 0)
			{
				int num = 0;
				List<IntPoint> list4 = list3[0];
				list4 = LightUtility.FixPivots(list4, list, ref num);
				for (int j = 0; j < list4.Count; j++)
				{
					list2.Add(new Vector2((float)list4[j].X / 10000f, (float)list4[j].Y / 10000f));
				}
			}
			return list2;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000CBAC File Offset: 0x0000ADAC
		private static void TransferToMesh(NativeArray<LightUtility.LightMeshVertex> vertices, int vertexCount, NativeArray<ushort> indices, int indexCount, Light2D light)
		{
			Mesh lightMesh = light.lightMesh;
			lightMesh.SetVertexBufferParams(vertexCount, LightUtility.LightMeshVertex.VertexLayout);
			lightMesh.SetVertexBufferData<LightUtility.LightMeshVertex>(vertices, 0, 0, vertexCount, 0, MeshUpdateFlags.Default);
			lightMesh.SetIndices<ushort>(indices, 0, indexCount, MeshTopology.Triangles, 0, true, 0);
			light.vertices = new LightUtility.LightMeshVertex[vertexCount];
			NativeArray<LightUtility.LightMeshVertex>.Copy(vertices, light.vertices, vertexCount);
			light.indices = new ushort[indexCount];
			NativeArray<ushort>.Copy(indices, light.indices, indexCount);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000CC1C File Offset: 0x0000AE1C
		public static Bounds GenerateShapeMesh(Light2D light, Vector3[] shapePath, float falloffDistance)
		{
			Color color = new Color(0f, 0f, 0f, 1f);
			Color color2 = new Color(0f, 0f, 0f, 0f);
			int num = shapePath.Length;
			NativeArray<int2> nativeArray = new NativeArray<int2>(num, Allocator.Temp, NativeArrayOptions.ClearMemory);
			NativeArray<float2> nativeArray2 = new NativeArray<float2>(num, Allocator.Temp, NativeArrayOptions.ClearMemory);
			for (int i = 0; i < num; i++)
			{
				int num2 = i + 1;
				if (num2 == num)
				{
					num2 = 0;
				}
				int2 @int = new int2(i, num2);
				nativeArray[i] = @int;
				int x = @int.x;
				nativeArray2[x] = new float2(shapePath[x].x, shapePath[x].y);
			}
			NativeArray<int> nativeArray3 = new NativeArray<int>(nativeArray.Length * 8, Allocator.Temp, NativeArrayOptions.ClearMemory);
			NativeArray<float2> nativeArray4 = new NativeArray<float2>(nativeArray.Length * 8, Allocator.Temp, NativeArrayOptions.ClearMemory);
			NativeArray<int2> nativeArray5 = new NativeArray<int2>(nativeArray.Length * 8, Allocator.Temp, NativeArrayOptions.ClearMemory);
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			ModuleHandle.Tessellate(Allocator.Temp, nativeArray2, nativeArray, ref nativeArray4, ref num3, ref nativeArray3, ref num4, ref nativeArray5, ref num5);
			int num6 = shapePath.Length;
			List<IntPoint> list = new List<IntPoint>();
			for (int j = 0; j < num6; j++)
			{
				Vector2 vector = new Vector2(shapePath[j].x, shapePath[j].y) * 10000f;
				list.Add(new IntPoint((long)vector.x + (long)Random.Range(-100, 100), (long)vector.y + (long)Random.Range(-100, 100))
				{
					N = (long)j,
					D = -1L
				});
			}
			int num7 = num6 - 1;
			int num8 = 0;
			List<List<IntPoint>> list2 = new List<List<IntPoint>>();
			ClipperOffset clipperOffset = new ClipperOffset(24.0);
			clipperOffset.AddPath(list, JoinType.jtRound, EndType.etClosedPolygon);
			clipperOffset.Execute(ref list2, (double)(10000f * falloffDistance), list.Count);
			if (list2.Count > 0)
			{
				List<IntPoint> list3 = list2[0];
				long num9 = (long)num6;
				for (int k = 0; k < list3.Count; k++)
				{
					num9 = ((list3[k].N != -1L) ? Math.Min(num9, list3[k].N) : num9);
				}
				bool flag = num9 == 0L;
				list3 = LightUtility.FixPivots(list3, list, ref num8);
				int num10 = num3 + list3.Count + num6;
				int num11 = num4 + list3.Count * 6 + 6;
				NativeArray<LightUtility.LightMeshVertex> nativeArray6 = new NativeArray<LightUtility.LightMeshVertex>(num10, Allocator.Temp, NativeArrayOptions.ClearMemory);
				NativeArray<ushort> nativeArray7 = new NativeArray<ushort>(num11, Allocator.Temp, NativeArrayOptions.ClearMemory);
				for (int l = 0; l < num4; l++)
				{
					nativeArray7[l] = (ushort)nativeArray3[l];
				}
				for (int m = 0; m < num3; m++)
				{
					int num12 = m;
					LightUtility.LightMeshVertex lightMeshVertex = new LightUtility.LightMeshVertex
					{
						position = new float3(nativeArray4[m].x, nativeArray4[m].y, 0f),
						color = color
					};
					nativeArray6[num12] = lightMeshVertex;
				}
				int num13 = num3;
				int num14 = num4;
				ushort[] array = new ushort[num6];
				for (int n = 0; n < num6; n++)
				{
					int num15 = num13++;
					LightUtility.LightMeshVertex lightMeshVertex = new LightUtility.LightMeshVertex
					{
						position = new float3(shapePath[n].x, shapePath[n].y, 0f),
						color = color
					};
					nativeArray6[num15] = lightMeshVertex;
					array[n] = (ushort)(num13 - 1);
				}
				ushort num16 = (ushort)num13;
				ushort num17 = num16;
				long num18 = ((list3[0].N == -1L) ? 0L : list3[0].N);
				for (int num19 = 0; num19 < list3.Count; num19++)
				{
					IntPoint intPoint = list3[num19];
					float2 @float = new float2((float)intPoint.X / 10000f, (float)intPoint.Y / 10000f);
					long num20 = ((intPoint.N == -1L) ? 0L : intPoint.N);
					int num21 = num13++;
					LightUtility.LightMeshVertex lightMeshVertex = new LightUtility.LightMeshVertex
					{
						position = new float3(@float.x, @float.y, 0f),
						color = ((num8 > num19) ? color2 : color)
					};
					nativeArray6[num21] = lightMeshVertex;
					if (num18 != num20)
					{
						nativeArray7[num14++] = array[(int)(checked((IntPtr)num18))];
						nativeArray7[num14++] = array[(int)(checked((IntPtr)num20))];
						nativeArray7[num14++] = (ushort)(num13 - 1);
					}
					nativeArray7[num14++] = array[(int)(checked((IntPtr)num18))];
					nativeArray7[num14++] = num16;
					num16 = (nativeArray7[num14++] = (ushort)(num13 - 1));
					num18 = num20;
				}
				nativeArray7[num14++] = num17;
				nativeArray7[num14++] = array[(int)(checked((IntPtr)num9))];
				nativeArray7[num14++] = (flag ? array[num7] : num16);
				nativeArray7[num14++] = (flag ? num17 : num16);
				nativeArray7[num14++] = (flag ? num16 : array[(int)(checked((IntPtr)num9))]);
				if (flag)
				{
					float num22 = 0.001f;
					ushort num23 = array[num7];
					bool flag2 = MathF.Abs(nativeArray6[(int)num23].position.x - nativeArray6[(int)nativeArray7[num14 - 1]].position.x) > num22 || MathF.Abs(nativeArray6[(int)num23].position.y - nativeArray6[(int)nativeArray7[num14 - 1]].position.y) > num22;
					bool flag3 = MathF.Abs(nativeArray6[(int)num23].position.x - nativeArray6[(int)nativeArray7[num14 - 2]].position.x) > num22 || MathF.Abs(nativeArray6[(int)num23].position.y - nativeArray6[(int)nativeArray7[num14 - 2]].position.y) > num22;
					if (!flag2 || !flag3)
					{
						num23 = (ushort)(num8 + num6 + num3 - 1);
					}
					nativeArray7[num14++] = num23;
				}
				else
				{
					nativeArray7[num14++] = array[(int)(checked((IntPtr)(unchecked(num9 - 1L))))];
				}
				LightUtility.TransferToMesh(nativeArray6, num13, nativeArray7, num14, light);
			}
			return light.lightMesh.GetSubMesh(0).bounds;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000D2D8 File Offset: 0x0000B4D8
		public static Bounds GenerateParametricMesh(Light2D light, float radius, float falloffDistance, float angle, int sides)
		{
			float num = 1.5707964f + 0.017453292f * angle;
			if (sides < 3)
			{
				radius = 0.70710677f * radius;
				sides = 4;
			}
			if (sides == 4)
			{
				num = 0.7853982f + 0.017453292f * angle;
			}
			int num2 = 1 + 2 * sides;
			int num3 = 9 * sides;
			NativeArray<LightUtility.LightMeshVertex> nativeArray = new NativeArray<LightUtility.LightMeshVertex>(num2, Allocator.Temp, NativeArrayOptions.ClearMemory);
			NativeArray<ushort> nativeArray2 = new NativeArray<ushort>(num3, Allocator.Temp, NativeArrayOptions.ClearMemory);
			ushort num4 = (ushort)(2 * sides);
			Mesh lightMesh = light.lightMesh;
			Color color = new Color(0f, 0f, 0f, 1f);
			nativeArray[(int)num4] = new LightUtility.LightMeshVertex
			{
				position = float3.zero,
				color = color
			};
			float num5 = 6.2831855f / (float)sides;
			float3 @float = new float3(float.MaxValue, float.MaxValue, 0f);
			float3 float2 = new float3(float.MinValue, float.MinValue, 0f);
			for (int i = 0; i < sides; i++)
			{
				float num6 = (float)(i + 1) * num5;
				float3 float3 = new float3(math.cos(num6 + num), math.sin(num6 + num), 0f);
				float3 float4 = radius * float3;
				int num7 = (2 * i + 2) % (2 * sides);
				nativeArray[num7] = new LightUtility.LightMeshVertex
				{
					position = float4,
					color = new Color(float3.x, float3.y, 0f, 0f)
				};
				nativeArray[num7 + 1] = new LightUtility.LightMeshVertex
				{
					position = float4,
					color = color
				};
				int num8 = 9 * i;
				nativeArray2[num8] = (ushort)(num7 + 1);
				nativeArray2[num8 + 1] = (ushort)(2 * i + 1);
				nativeArray2[num8 + 2] = num4;
				nativeArray2[num8 + 3] = (ushort)num7;
				nativeArray2[num8 + 4] = (ushort)(2 * i);
				nativeArray2[num8 + 5] = (ushort)(2 * i + 1);
				nativeArray2[num8 + 6] = (ushort)(num7 + 1);
				nativeArray2[num8 + 7] = (ushort)num7;
				nativeArray2[num8 + 8] = (ushort)(2 * i + 1);
				@float = math.min(@float, float4 + float3 * falloffDistance);
				float2 = math.max(float2, float4 + float3 * falloffDistance);
			}
			lightMesh.SetVertexBufferParams(num2, LightUtility.LightMeshVertex.VertexLayout);
			lightMesh.SetVertexBufferData<LightUtility.LightMeshVertex>(nativeArray, 0, 0, num2, 0, MeshUpdateFlags.Default);
			lightMesh.SetIndices<ushort>(nativeArray2, MeshTopology.Triangles, 0, false, 0);
			light.vertices = new LightUtility.LightMeshVertex[num2];
			NativeArray<LightUtility.LightMeshVertex>.Copy(nativeArray, light.vertices, num2);
			light.indices = new ushort[num3];
			NativeArray<ushort>.Copy(nativeArray2, light.indices, num3);
			return new Bounds
			{
				min = @float,
				max = float2
			};
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000D5D4 File Offset: 0x0000B7D4
		public static Bounds GenerateSpriteMesh(Light2D light, Sprite sprite)
		{
			Mesh lightMesh = light.lightMesh;
			if (sprite == null)
			{
				lightMesh.Clear();
				return new Bounds(Vector3.zero, Vector3.zero);
			}
			Vector2[] uv = sprite.uv;
			NativeSlice<Vector3> vertexAttribute = sprite.GetVertexAttribute(VertexAttribute.Position);
			NativeSlice<Vector2> vertexAttribute2 = sprite.GetVertexAttribute(VertexAttribute.TexCoord0);
			NativeArray<ushort> indices = sprite.GetIndices();
			0.5f * (sprite.bounds.min + sprite.bounds.max);
			NativeArray<LightUtility.LightMeshVertex> nativeArray = new NativeArray<LightUtility.LightMeshVertex>(indices.Length, Allocator.Temp, NativeArrayOptions.ClearMemory);
			Color color = new Color(0f, 0f, 0f, 1f);
			for (int i = 0; i < vertexAttribute.Length; i++)
			{
				nativeArray[i] = new LightUtility.LightMeshVertex
				{
					position = new Vector3(vertexAttribute[i].x, vertexAttribute[i].y, 0f),
					color = color,
					uv = vertexAttribute2[i]
				};
			}
			lightMesh.SetVertexBufferParams(nativeArray.Length, LightUtility.LightMeshVertex.VertexLayout);
			lightMesh.SetVertexBufferData<LightUtility.LightMeshVertex>(nativeArray, 0, 0, nativeArray.Length, 0, MeshUpdateFlags.Default);
			lightMesh.SetIndices<ushort>(indices, MeshTopology.Triangles, 0, true, 0);
			light.vertices = new LightUtility.LightMeshVertex[nativeArray.Length];
			NativeArray<LightUtility.LightMeshVertex>.Copy(nativeArray, light.vertices, nativeArray.Length);
			light.indices = new ushort[indices.Length];
			NativeArray<ushort>.Copy(indices, light.indices, indices.Length);
			return lightMesh.GetSubMesh(0).bounds;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000D778 File Offset: 0x0000B978
		public static int GetShapePathHash(Vector3[] path)
		{
			int num = -2128831035;
			if (path != null)
			{
				foreach (Vector3 vector in path)
				{
					num = (num * 16777619) ^ vector.GetHashCode();
				}
			}
			else
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x02000144 RID: 324
		private enum PivotType
		{
			// Token: 0x040008AF RID: 2223
			PivotBase,
			// Token: 0x040008B0 RID: 2224
			PivotCurve,
			// Token: 0x040008B1 RID: 2225
			PivotIntersect,
			// Token: 0x040008B2 RID: 2226
			PivotSkip,
			// Token: 0x040008B3 RID: 2227
			PivotClip
		}

		// Token: 0x02000145 RID: 325
		[Serializable]
		internal struct LightMeshVertex
		{
			// Token: 0x040008B4 RID: 2228
			public Vector3 position;

			// Token: 0x040008B5 RID: 2229
			public Color color;

			// Token: 0x040008B6 RID: 2230
			public Vector2 uv;

			// Token: 0x040008B7 RID: 2231
			public static readonly VertexAttributeDescriptor[] VertexLayout = new VertexAttributeDescriptor[]
			{
				new VertexAttributeDescriptor(VertexAttribute.Position, VertexAttributeFormat.Float32, 3, 0),
				new VertexAttributeDescriptor(VertexAttribute.Color, VertexAttributeFormat.Float32, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord0, VertexAttributeFormat.Float32, 2, 0)
			};
		}
	}
}
