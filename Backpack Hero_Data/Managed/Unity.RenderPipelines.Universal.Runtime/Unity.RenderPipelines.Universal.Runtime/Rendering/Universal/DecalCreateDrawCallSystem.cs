using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000067 RID: 103
	internal class DecalCreateDrawCallSystem
	{
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060003AE RID: 942 RVA: 0x000155C8 File Offset: 0x000137C8
		// (set) Token: 0x060003AF RID: 943 RVA: 0x000155D0 File Offset: 0x000137D0
		public float maxDrawDistance
		{
			get
			{
				return this.m_MaxDrawDistance;
			}
			set
			{
				this.m_MaxDrawDistance = value;
			}
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x000155D9 File Offset: 0x000137D9
		public DecalCreateDrawCallSystem(DecalEntityManager entityManager, float maxDrawDistance)
		{
			this.m_EntityManager = entityManager;
			this.m_Sampler = new ProfilingSampler("DecalCreateDrawCallSystem.Execute");
			this.m_MaxDrawDistance = maxDrawDistance;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00015600 File Offset: 0x00013800
		public void Execute()
		{
			using (new ProfilingScope(null, this.m_Sampler))
			{
				for (int i = 0; i < this.m_EntityManager.chunkCount; i++)
				{
					this.Execute(this.m_EntityManager.cachedChunks[i], this.m_EntityManager.culledChunks[i], this.m_EntityManager.drawCallChunks[i], this.m_EntityManager.cachedChunks[i].count);
				}
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x000156A0 File Offset: 0x000138A0
		private void Execute(DecalCachedChunk cachedChunk, DecalCulledChunk culledChunk, DecalDrawCallChunk drawCallChunk, int count)
		{
			if (count == 0)
			{
				return;
			}
			JobHandle jobHandle = new DecalCreateDrawCallSystem.DrawCallJob
			{
				decalToWorlds = cachedChunk.decalToWorlds,
				normalToWorlds = cachedChunk.normalToWorlds,
				sizeOffsets = cachedChunk.sizeOffsets,
				drawDistances = cachedChunk.drawDistances,
				angleFades = cachedChunk.angleFades,
				uvScaleBiases = cachedChunk.uvScaleBias,
				layerMasks = cachedChunk.layerMasks,
				sceneLayerMasks = cachedChunk.sceneLayerMasks,
				fadeFactors = cachedChunk.fadeFactors,
				boundingSpheres = cachedChunk.boundingSpheres,
				cameraPosition = culledChunk.cameraPosition,
				sceneCullingMask = culledChunk.sceneCullingMask,
				cullingMask = culledChunk.cullingMask,
				visibleDecalIndices = culledChunk.visibleDecalIndices,
				visibleDecalCount = culledChunk.visibleDecalCount,
				maxDrawDistance = this.m_MaxDrawDistance,
				decalToWorldsDraw = drawCallChunk.decalToWorlds,
				normalToDecalsDraw = drawCallChunk.normalToDecals,
				subCalls = drawCallChunk.subCalls,
				subCallCount = drawCallChunk.subCallCounts
			}.Schedule(cachedChunk.currentJobHandle);
			drawCallChunk.currentJobHandle = jobHandle;
			cachedChunk.currentJobHandle = jobHandle;
		}

		// Token: 0x040002A7 RID: 679
		private DecalEntityManager m_EntityManager;

		// Token: 0x040002A8 RID: 680
		private ProfilingSampler m_Sampler;

		// Token: 0x040002A9 RID: 681
		private float m_MaxDrawDistance;

		// Token: 0x02000166 RID: 358
		[BurstCompile]
		private struct DrawCallJob : IJob
		{
			// Token: 0x0600098F RID: 2447 RVA: 0x0003FBE0 File Offset: 0x0003DDE0
			public void Execute()
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				for (int i = 0; i < this.visibleDecalCount; i++)
				{
					int num4 = this.visibleDecalIndices[i];
					int num5 = 1 << this.layerMasks[num4];
					if ((this.cullingMask & num5) != 0)
					{
						BoundingSphere boundingSphere = this.boundingSpheres[num4];
						float2 @float = this.drawDistances[num4];
						float magnitude = (this.cameraPosition - boundingSphere.position).magnitude;
						float num6 = math.min(@float.x, this.maxDrawDistance) + boundingSphere.radius;
						if (magnitude <= num6)
						{
							this.decalToWorldsDraw[num2] = this.decalToWorlds[num4];
							float num7 = this.fadeFactors[num4];
							float2 float2 = this.angleFades[num4];
							float4 float3 = this.uvScaleBiases[num4];
							float4x4 float4x = this.normalToWorlds[num4];
							float num8 = num7 * math.clamp((num6 - magnitude) / (num6 * (1f - @float.y)), 0f, 1f);
							float4x.c0.w = float3.x;
							float4x.c1.w = float3.y;
							float4x.c2.w = float3.z;
							float4x.c3 = new float4(num8 * 1f, float2.x, float2.y, float3.w);
							this.normalToDecalsDraw[num2] = float4x;
							num2++;
							if (num2 - num3 >= 250)
							{
								this.subCalls[num++] = new DecalSubDrawCall
								{
									start = num3,
									end = num2
								};
								num3 = num2;
							}
						}
					}
				}
				if (num2 - num3 != 0)
				{
					this.subCalls[num++] = new DecalSubDrawCall
					{
						start = num3,
						end = num2
					};
				}
				this.subCallCount[0] = num;
			}

			// Token: 0x04000927 RID: 2343
			[ReadOnly]
			public NativeArray<float4x4> decalToWorlds;

			// Token: 0x04000928 RID: 2344
			[ReadOnly]
			public NativeArray<float4x4> normalToWorlds;

			// Token: 0x04000929 RID: 2345
			[ReadOnly]
			public NativeArray<float4x4> sizeOffsets;

			// Token: 0x0400092A RID: 2346
			[ReadOnly]
			public NativeArray<float2> drawDistances;

			// Token: 0x0400092B RID: 2347
			[ReadOnly]
			public NativeArray<float2> angleFades;

			// Token: 0x0400092C RID: 2348
			[ReadOnly]
			public NativeArray<float4> uvScaleBiases;

			// Token: 0x0400092D RID: 2349
			[ReadOnly]
			public NativeArray<int> layerMasks;

			// Token: 0x0400092E RID: 2350
			[ReadOnly]
			public NativeArray<ulong> sceneLayerMasks;

			// Token: 0x0400092F RID: 2351
			[ReadOnly]
			public NativeArray<float> fadeFactors;

			// Token: 0x04000930 RID: 2352
			[ReadOnly]
			public NativeArray<BoundingSphere> boundingSpheres;

			// Token: 0x04000931 RID: 2353
			public Vector3 cameraPosition;

			// Token: 0x04000932 RID: 2354
			public ulong sceneCullingMask;

			// Token: 0x04000933 RID: 2355
			public int cullingMask;

			// Token: 0x04000934 RID: 2356
			[ReadOnly]
			public NativeArray<int> visibleDecalIndices;

			// Token: 0x04000935 RID: 2357
			public int visibleDecalCount;

			// Token: 0x04000936 RID: 2358
			public float maxDrawDistance;

			// Token: 0x04000937 RID: 2359
			[WriteOnly]
			public NativeArray<float4x4> decalToWorldsDraw;

			// Token: 0x04000938 RID: 2360
			[WriteOnly]
			public NativeArray<float4x4> normalToDecalsDraw;

			// Token: 0x04000939 RID: 2361
			[WriteOnly]
			public NativeArray<DecalSubDrawCall> subCalls;

			// Token: 0x0400093A RID: 2362
			[WriteOnly]
			public NativeArray<int> subCallCount;
		}
	}
}
