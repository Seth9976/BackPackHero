using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine.Jobs;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200006F RID: 111
	internal class DecalUpdateCachedSystem
	{
		// Token: 0x060003DF RID: 991 RVA: 0x000171E9 File Offset: 0x000153E9
		public DecalUpdateCachedSystem(DecalEntityManager entityManager)
		{
			this.m_EntityManager = entityManager;
			this.m_Sampler = new ProfilingSampler("DecalUpdateCachedSystem.Execute");
			this.m_SamplerJob = new ProfilingSampler("DecalUpdateCachedSystem.ExecuteJob");
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00017218 File Offset: 0x00015418
		public void Execute()
		{
			using (new ProfilingScope(null, this.m_Sampler))
			{
				for (int i = 0; i < this.m_EntityManager.chunkCount; i++)
				{
					this.Execute(this.m_EntityManager.entityChunks[i], this.m_EntityManager.cachedChunks[i], this.m_EntityManager.entityChunks[i].count);
				}
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x000172A8 File Offset: 0x000154A8
		private void Execute(DecalEntityChunk entityChunk, DecalCachedChunk cachedChunk, int count)
		{
			if (count == 0)
			{
				return;
			}
			cachedChunk.currentJobHandle.Complete();
			Material material = entityChunk.material;
			if (material.HasProperty("_DrawOrder"))
			{
				cachedChunk.drawOrder = material.GetInt("_DrawOrder");
			}
			if (!cachedChunk.isCreated)
			{
				int num = material.FindPass("DBufferProjector");
				cachedChunk.passIndexDBuffer = num;
				int num2 = material.FindPass("DecalProjectorForwardEmissive");
				cachedChunk.passIndexEmissive = num2;
				int num3 = material.FindPass("DecalScreenSpaceProjector");
				cachedChunk.passIndexScreenSpace = num3;
				int num4 = material.FindPass("DecalGBufferProjector");
				cachedChunk.passIndexGBuffer = num4;
				cachedChunk.isCreated = true;
			}
			using (new ProfilingScope(null, this.m_SamplerJob))
			{
				JobHandle jobHandle = new DecalUpdateCachedSystem.UpdateTransformsJob
				{
					positions = cachedChunk.positions,
					rotations = cachedChunk.rotation,
					scales = cachedChunk.scales,
					dirty = cachedChunk.dirty,
					scaleModes = cachedChunk.scaleModes,
					sizeOffsets = cachedChunk.sizeOffsets,
					decalToWorlds = cachedChunk.decalToWorlds,
					normalToWorlds = cachedChunk.normalToWorlds,
					boundingSpheres = cachedChunk.boundingSpheres,
					minDistance = float.Epsilon
				}.Schedule(entityChunk.transformAccessArray, default(JobHandle));
				cachedChunk.currentJobHandle = jobHandle;
			}
		}

		// Token: 0x040002DF RID: 735
		private DecalEntityManager m_EntityManager;

		// Token: 0x040002E0 RID: 736
		private ProfilingSampler m_Sampler;

		// Token: 0x040002E1 RID: 737
		private ProfilingSampler m_SamplerJob;

		// Token: 0x0200016A RID: 362
		[BurstCompile]
		public struct UpdateTransformsJob : IJobParallelForTransform
		{
			// Token: 0x06000993 RID: 2451 RVA: 0x0003FEA1 File Offset: 0x0003E0A1
			private float DistanceBetweenQuaternions(quaternion a, quaternion b)
			{
				return math.distancesq(a.value, b.value);
			}

			// Token: 0x06000994 RID: 2452 RVA: 0x0003FEB4 File Offset: 0x0003E0B4
			public void Execute(int index, TransformAccess transform)
			{
				bool flag = math.distancesq(transform.position, this.positions[index]) > this.minDistance;
				if (flag)
				{
					this.positions[index] = transform.position;
				}
				bool flag2 = this.DistanceBetweenQuaternions(transform.rotation, this.rotations[index]) > this.minDistance;
				if (flag2)
				{
					this.rotations[index] = transform.rotation;
				}
				bool flag3 = math.distancesq(transform.localScale, this.scales[index]) > this.minDistance;
				if (flag3)
				{
					this.scales[index] = transform.localScale;
				}
				if (!flag && !flag2 && !flag3 && !this.dirty[index])
				{
					return;
				}
				float4x4 float4x;
				if (this.scaleModes[index] == DecalScaleMode.InheritFromHierarchy)
				{
					float4x = transform.localToWorldMatrix;
					float4x = math.mul(float4x, new float4x4(DecalUpdateCachedSystem.UpdateTransformsJob.k_MinusYtoZRotation, float3.zero));
				}
				else
				{
					quaternion quaternion = math.mul(transform.rotation, DecalUpdateCachedSystem.UpdateTransformsJob.k_MinusYtoZRotation);
					float4x = float4x4.TRS(this.positions[index], quaternion, new float3(1f, 1f, 1f));
				}
				float4x4 float4x2 = float4x;
				float4 c = float4x2.c1;
				float4x2.c1 = float4x2.c2;
				float4x2.c2 = c;
				this.normalToWorlds[index] = float4x2;
				float4x4 float4x3 = this.sizeOffsets[index];
				float4x4 float4x4 = math.mul(float4x, float4x3);
				this.decalToWorlds[index] = float4x4;
				this.boundingSpheres[index] = this.GetDecalProjectBoundingSphere(float4x4);
				this.dirty[index] = false;
			}

			// Token: 0x06000995 RID: 2453 RVA: 0x00040088 File Offset: 0x0003E288
			private BoundingSphere GetDecalProjectBoundingSphere(Matrix4x4 decalToWorld)
			{
				float4 @float = new float4(-0.5f, -0.5f, -0.5f, 1f);
				float4 float2 = new float4(0.5f, 0.5f, 0.5f, 1f);
				@float = math.mul(decalToWorld, @float);
				float2 = math.mul(decalToWorld, float2);
				float3 xyz = ((float2 + @float) / 2f).xyz;
				float num = math.length(float2 - @float) / 2f;
				return new BoundingSphere
				{
					position = xyz,
					radius = num
				};
			}

			// Token: 0x04000946 RID: 2374
			private static readonly quaternion k_MinusYtoZRotation = quaternion.EulerXYZ(-1.5707964f, 0f, 0f);

			// Token: 0x04000947 RID: 2375
			public NativeArray<float3> positions;

			// Token: 0x04000948 RID: 2376
			public NativeArray<quaternion> rotations;

			// Token: 0x04000949 RID: 2377
			public NativeArray<float3> scales;

			// Token: 0x0400094A RID: 2378
			public NativeArray<bool> dirty;

			// Token: 0x0400094B RID: 2379
			[ReadOnly]
			public NativeArray<DecalScaleMode> scaleModes;

			// Token: 0x0400094C RID: 2380
			[ReadOnly]
			public NativeArray<float4x4> sizeOffsets;

			// Token: 0x0400094D RID: 2381
			[WriteOnly]
			public NativeArray<float4x4> decalToWorlds;

			// Token: 0x0400094E RID: 2382
			[WriteOnly]
			public NativeArray<float4x4> normalToWorlds;

			// Token: 0x0400094F RID: 2383
			[WriteOnly]
			public NativeArray<BoundingSphere> boundingSpheres;

			// Token: 0x04000950 RID: 2384
			public float minDistance;
		}
	}
}
