using System;
using Pathfinding.Util;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.Graphs.Navmesh.Voxelization.Burst
{
	// Token: 0x020001DE RID: 478
	[BurstCompile(CompileSynchronously = true)]
	public struct JobVoxelize : IJob
	{
		// Token: 0x06000C44 RID: 3140 RVA: 0x00049F74 File Offset: 0x00048174
		public unsafe void Execute()
		{
			Matrix4x4 matrix4x = Matrix4x4.TRS(this.graphSpaceBounds.min, Quaternion.identity, Vector3.one) * Matrix4x4.Scale(new Vector3(this.cellSize, this.cellHeight, this.cellSize));
			Matrix4x4 inverse = (this.graphTransform * matrix4x * Matrix4x4.Translate(new Vector3(0.5f, 0f, 0.5f))).inverse;
			float num = math.cos(math.atan(this.cellSize / this.cellHeight * math.tan(this.maxSlope * 0.017453292f)));
			VoxelPolygonClipper voxelPolygonClipper = default(VoxelPolygonClipper);
			VoxelPolygonClipper voxelPolygonClipper2 = default(VoxelPolygonClipper);
			VoxelPolygonClipper voxelPolygonClipper3 = default(VoxelPolygonClipper);
			VoxelPolygonClipper voxelPolygonClipper4 = default(VoxelPolygonClipper);
			VoxelPolygonClipper voxelPolygonClipper5 = default(VoxelPolygonClipper);
			int num2 = 0;
			for (int i = 0; i < this.bucket.Length; i++)
			{
				num2 = math.max(this.inputMeshes[this.bucket[i]].vertices.Length, num2);
			}
			NativeArray<float3> nativeArray = new NativeArray<float3>(num2, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			int width = this.voxelArea.width;
			int depth = this.voxelArea.depth;
			for (int j = 0; j < this.bucket.Length; j++)
			{
				RasterizationMesh rasterizationMesh = this.inputMeshes[this.bucket[j]];
				bool flag = VectorMath.ReversesFaceOrientations(rasterizationMesh.matrix);
				UnsafeSpan<float3> vertices = rasterizationMesh.vertices;
				UnsafeSpan<int> triangles = rasterizationMesh.triangles;
				float4x4 float4x = inverse * rasterizationMesh.matrix;
				for (int k = 0; k < vertices.Length; k++)
				{
					nativeArray[k] = math.transform(float4x, *vertices[k]);
				}
				int num3 = rasterizationMesh.area;
				if (rasterizationMesh.areaIsTag)
				{
					num3 |= 16384;
				}
				IntRect intRect = default(IntRect);
				for (int l = 0; l < triangles.Length; l += 3)
				{
					float3 @float = nativeArray[*triangles[l]];
					float3 float2 = nativeArray[*triangles[l + 1]];
					float3 float3 = nativeArray[*triangles[l + 2]];
					if (flag)
					{
						float3 float4 = @float;
						@float = float3;
						float3 = float4;
					}
					int num4 = (int)math.min(math.min(@float.x, float2.x), float3.x);
					int num5 = (int)math.min(math.min(@float.z, float2.z), float3.z);
					int num6 = (int)math.ceil(math.max(math.max(@float.x, float2.x), float3.x));
					int num7 = (int)math.ceil(math.max(math.max(@float.z, float2.z), float3.z));
					num4 = math.clamp(num4, 0, width - 1);
					num6 = math.clamp(num6, 0, width - 1);
					num5 = math.clamp(num5, 0, depth - 1);
					num7 = math.clamp(num7, 0, depth - 1);
					if (num4 < width && num5 < depth && num6 > 0 && num7 > 0)
					{
						if (l == 0)
						{
							intRect = new IntRect(num4, num5, num4, num5);
						}
						intRect.xmin = math.min(intRect.xmin, num4);
						intRect.xmax = math.max(intRect.xmax, num6);
						intRect.ymin = math.min(intRect.ymin, num5);
						intRect.ymax = math.max(intRect.ymax, num7);
						float num8 = math.normalizesafe(math.cross(float2 - @float, float3 - @float), default(float3)).y;
						if (rasterizationMesh.doubleSided)
						{
							num8 = math.abs(num8);
						}
						int num9 = ((num8 < num) ? 0 : (1 + num3));
						voxelPolygonClipper[0] = @float;
						voxelPolygonClipper[1] = float2;
						voxelPolygonClipper[2] = float3;
						voxelPolygonClipper.n = 3;
						for (int m = num4; m <= num6; m++)
						{
							voxelPolygonClipper.ClipPolygonAlongX(ref voxelPolygonClipper2, 1f, (float)(-(float)m) + 0.5f);
							if (voxelPolygonClipper2.n >= 3)
							{
								voxelPolygonClipper2.ClipPolygonAlongX(ref voxelPolygonClipper3, -1f, (float)m + 0.5f);
								if (voxelPolygonClipper3.n >= 3)
								{
									float num11;
									float num10 = (num11 = voxelPolygonClipper3.z.FixedElementField);
									for (int n = 1; n < voxelPolygonClipper3.n; n++)
									{
										float num12 = *((ref voxelPolygonClipper3.z.FixedElementField) + (IntPtr)n * 4);
										num11 = math.min(num11, num12);
										num10 = math.max(num10, num12);
									}
									int num13 = math.clamp((int)math.round(num11), 0, depth - 1);
									int num14 = math.clamp((int)math.round(num10), 0, depth - 1);
									for (int num15 = num13; num15 <= num14; num15++)
									{
										voxelPolygonClipper3.ClipPolygonAlongZWithYZ(ref voxelPolygonClipper4, 1f, (float)(-(float)num15) + 0.5f);
										if (voxelPolygonClipper4.n >= 3)
										{
											voxelPolygonClipper4.ClipPolygonAlongZWithY(ref voxelPolygonClipper5, -1f, (float)num15 + 0.5f);
											if (voxelPolygonClipper5.n >= 3)
											{
												if (rasterizationMesh.flatten)
												{
													this.voxelArea.AddFlattenedSpan(num15 * width + m, num9);
												}
												else
												{
													float num17;
													float num16 = (num17 = voxelPolygonClipper5.y.FixedElementField);
													for (int num18 = 1; num18 < voxelPolygonClipper5.n; num18++)
													{
														float num19 = *((ref voxelPolygonClipper5.y.FixedElementField) + (IntPtr)num18 * 4);
														num17 = math.min(num17, num19);
														num16 = math.max(num16, num19);
													}
													int num20 = (int)math.ceil(num16);
													int num21 = (int)num17;
													num20 = math.max(num21 + 1, num20);
													this.voxelArea.AddLinkedSpan(num15 * width + m, num21, num20, num9, this.voxelWalkableClimb, j);
												}
											}
										}
									}
								}
							}
						}
					}
				}
				if (rasterizationMesh.solid)
				{
					for (int num22 = intRect.ymin; num22 <= intRect.ymax; num22++)
					{
						for (int num23 = intRect.xmin; num23 <= intRect.xmax; num23++)
						{
							this.voxelArea.ResolveSolid(num22 * this.voxelArea.width + num23, j, this.voxelWalkableClimb);
						}
					}
				}
			}
		}

		// Token: 0x040008B9 RID: 2233
		[ReadOnly]
		public NativeArray<RasterizationMesh> inputMeshes;

		// Token: 0x040008BA RID: 2234
		[ReadOnly]
		public NativeArray<int> bucket;

		// Token: 0x040008BB RID: 2235
		public int voxelWalkableClimb;

		// Token: 0x040008BC RID: 2236
		public uint voxelWalkableHeight;

		// Token: 0x040008BD RID: 2237
		public float cellSize;

		// Token: 0x040008BE RID: 2238
		public float cellHeight;

		// Token: 0x040008BF RID: 2239
		public float maxSlope;

		// Token: 0x040008C0 RID: 2240
		public Matrix4x4 graphTransform;

		// Token: 0x040008C1 RID: 2241
		public Bounds graphSpaceBounds;

		// Token: 0x040008C2 RID: 2242
		public LinkedVoxelField voxelArea;
	}
}
