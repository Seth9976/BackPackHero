using System;
using Unity.Collections;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000068 RID: 104
	internal abstract class DecalDrawSystem
	{
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x000157D9 File Offset: 0x000139D9
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x000157E1 File Offset: 0x000139E1
		public Material overrideMaterial { get; set; }

		// Token: 0x060003B5 RID: 949 RVA: 0x000157EA File Offset: 0x000139EA
		public DecalDrawSystem(string sampler, DecalEntityManager entityManager)
		{
			this.m_EntityManager = entityManager;
			this.m_WorldToDecals = new Matrix4x4[250];
			this.m_NormalToDecals = new Matrix4x4[250];
			this.m_Sampler = new ProfilingSampler(sampler);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00015828 File Offset: 0x00013A28
		public void Execute(CommandBuffer cmd)
		{
			using (new ProfilingScope(cmd, this.m_Sampler))
			{
				for (int i = 0; i < this.m_EntityManager.chunkCount; i++)
				{
					this.Execute(cmd, this.m_EntityManager.entityChunks[i], this.m_EntityManager.cachedChunks[i], this.m_EntityManager.drawCallChunks[i], this.m_EntityManager.entityChunks[i].count);
				}
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x000158CC File Offset: 0x00013ACC
		protected virtual Material GetMaterial(DecalEntityChunk decalEntityChunk)
		{
			return decalEntityChunk.material;
		}

		// Token: 0x060003B8 RID: 952
		protected abstract int GetPassIndex(DecalCachedChunk decalCachedChunk);

		// Token: 0x060003B9 RID: 953 RVA: 0x000158D4 File Offset: 0x00013AD4
		private void Execute(CommandBuffer cmd, DecalEntityChunk decalEntityChunk, DecalCachedChunk decalCachedChunk, DecalDrawCallChunk decalDrawCallChunk, int count)
		{
			decalCachedChunk.currentJobHandle.Complete();
			decalDrawCallChunk.currentJobHandle.Complete();
			Material material = this.GetMaterial(decalEntityChunk);
			int passIndex = this.GetPassIndex(decalCachedChunk);
			if (count == 0 || passIndex == -1 || material == null)
			{
				return;
			}
			if (SystemInfo.supportsInstancing && material.enableInstancing)
			{
				this.DrawInstanced(cmd, decalEntityChunk, decalCachedChunk, decalDrawCallChunk, passIndex);
				return;
			}
			this.Draw(cmd, decalEntityChunk, decalCachedChunk, decalDrawCallChunk, passIndex);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00015948 File Offset: 0x00013B48
		private void Draw(CommandBuffer cmd, DecalEntityChunk decalEntityChunk, DecalCachedChunk decalCachedChunk, DecalDrawCallChunk decalDrawCallChunk, int passIndex)
		{
			Mesh decalProjectorMesh = this.m_EntityManager.decalProjectorMesh;
			Material material = this.GetMaterial(decalEntityChunk);
			decalCachedChunk.propertyBlock.SetVector("unity_LightData", new Vector4(1f, 1f, 1f, 0f));
			int subCallCount = decalDrawCallChunk.subCallCount;
			for (int i = 0; i < subCallCount; i++)
			{
				DecalSubDrawCall decalSubDrawCall = decalDrawCallChunk.subCalls[i];
				for (int j = decalSubDrawCall.start; j < decalSubDrawCall.end; j++)
				{
					decalCachedChunk.propertyBlock.SetMatrix("_NormalToWorld", decalDrawCallChunk.normalToDecals[j]);
					cmd.DrawMesh(decalProjectorMesh, decalDrawCallChunk.decalToWorlds[j], material, 0, passIndex, decalCachedChunk.propertyBlock);
				}
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00015A18 File Offset: 0x00013C18
		private void DrawInstanced(CommandBuffer cmd, DecalEntityChunk decalEntityChunk, DecalCachedChunk decalCachedChunk, DecalDrawCallChunk decalDrawCallChunk, int passIndex)
		{
			Mesh decalProjectorMesh = this.m_EntityManager.decalProjectorMesh;
			Material material = this.GetMaterial(decalEntityChunk);
			decalCachedChunk.propertyBlock.SetVector("unity_LightData", new Vector4(1f, 1f, 1f, 0f));
			int subCallCount = decalDrawCallChunk.subCallCount;
			for (int i = 0; i < subCallCount; i++)
			{
				DecalSubDrawCall decalSubDrawCall = decalDrawCallChunk.subCalls[i];
				NativeArray<Matrix4x4>.Copy(decalDrawCallChunk.decalToWorlds.Reinterpret<Matrix4x4>(), decalSubDrawCall.start, this.m_WorldToDecals, 0, decalSubDrawCall.count);
				NativeArray<Matrix4x4>.Copy(decalDrawCallChunk.normalToDecals.Reinterpret<Matrix4x4>(), decalSubDrawCall.start, this.m_NormalToDecals, 0, decalSubDrawCall.count);
				decalCachedChunk.propertyBlock.SetMatrixArray("_NormalToWorld", this.m_NormalToDecals);
				cmd.DrawMeshInstanced(decalProjectorMesh, 0, material, passIndex, this.m_WorldToDecals, decalSubDrawCall.end - decalSubDrawCall.start, decalCachedChunk.propertyBlock);
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00015B14 File Offset: 0x00013D14
		public void Execute(in CameraData cameraData)
		{
			using (new ProfilingScope(null, this.m_Sampler))
			{
				for (int i = 0; i < this.m_EntityManager.chunkCount; i++)
				{
					this.Execute(in cameraData, this.m_EntityManager.entityChunks[i], this.m_EntityManager.cachedChunks[i], this.m_EntityManager.drawCallChunks[i], this.m_EntityManager.entityChunks[i].count);
				}
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00015BB8 File Offset: 0x00013DB8
		private void Execute(in CameraData cameraData, DecalEntityChunk decalEntityChunk, DecalCachedChunk decalCachedChunk, DecalDrawCallChunk decalDrawCallChunk, int count)
		{
			decalCachedChunk.currentJobHandle.Complete();
			decalDrawCallChunk.currentJobHandle.Complete();
			Material material = this.GetMaterial(decalEntityChunk);
			int passIndex = this.GetPassIndex(decalCachedChunk);
			if (count == 0 || passIndex == -1 || material == null)
			{
				return;
			}
			if (SystemInfo.supportsInstancing && material.enableInstancing)
			{
				this.DrawInstanced(in cameraData, decalEntityChunk, decalCachedChunk, decalDrawCallChunk);
				return;
			}
			this.Draw(in cameraData, decalEntityChunk, decalCachedChunk, decalDrawCallChunk);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00015C2C File Offset: 0x00013E2C
		private void Draw(in CameraData cameraData, DecalEntityChunk decalEntityChunk, DecalCachedChunk decalCachedChunk, DecalDrawCallChunk decalDrawCallChunk)
		{
			Mesh decalProjectorMesh = this.m_EntityManager.decalProjectorMesh;
			Material material = this.GetMaterial(decalEntityChunk);
			int subCallCount = decalDrawCallChunk.subCallCount;
			for (int i = 0; i < subCallCount; i++)
			{
				DecalSubDrawCall decalSubDrawCall = decalDrawCallChunk.subCalls[i];
				for (int j = decalSubDrawCall.start; j < decalSubDrawCall.end; j++)
				{
					decalCachedChunk.propertyBlock.SetMatrix("_NormalToWorld", decalDrawCallChunk.normalToDecals[j]);
					Graphics.DrawMesh(decalProjectorMesh, decalCachedChunk.decalToWorlds[j], material, decalCachedChunk.layerMasks[j], cameraData.camera, 0, decalCachedChunk.propertyBlock);
				}
			}
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00015CE8 File Offset: 0x00013EE8
		private void DrawInstanced(in CameraData cameraData, DecalEntityChunk decalEntityChunk, DecalCachedChunk decalCachedChunk, DecalDrawCallChunk decalDrawCallChunk)
		{
			Mesh decalProjectorMesh = this.m_EntityManager.decalProjectorMesh;
			Material material = this.GetMaterial(decalEntityChunk);
			decalCachedChunk.propertyBlock.SetVector("unity_LightData", new Vector4(1f, 1f, 1f, 0f));
			int subCallCount = decalDrawCallChunk.subCallCount;
			for (int i = 0; i < subCallCount; i++)
			{
				DecalSubDrawCall decalSubDrawCall = decalDrawCallChunk.subCalls[i];
				NativeArray<Matrix4x4>.Copy(decalDrawCallChunk.decalToWorlds.Reinterpret<Matrix4x4>(), decalSubDrawCall.start, this.m_WorldToDecals, 0, decalSubDrawCall.count);
				NativeArray<Matrix4x4>.Copy(decalDrawCallChunk.normalToDecals.Reinterpret<Matrix4x4>(), decalSubDrawCall.start, this.m_NormalToDecals, 0, decalSubDrawCall.count);
				decalCachedChunk.propertyBlock.SetMatrixArray("_NormalToWorld", this.m_NormalToDecals);
				Graphics.DrawMeshInstanced(decalProjectorMesh, 0, material, this.m_WorldToDecals, decalSubDrawCall.count, decalCachedChunk.propertyBlock, ShadowCastingMode.On, true, 0, cameraData.camera);
			}
		}

		// Token: 0x040002AA RID: 682
		protected DecalEntityManager m_EntityManager;

		// Token: 0x040002AB RID: 683
		private Matrix4x4[] m_WorldToDecals;

		// Token: 0x040002AC RID: 684
		private Matrix4x4[] m_NormalToDecals;

		// Token: 0x040002AD RID: 685
		private ProfilingSampler m_Sampler;
	}
}
