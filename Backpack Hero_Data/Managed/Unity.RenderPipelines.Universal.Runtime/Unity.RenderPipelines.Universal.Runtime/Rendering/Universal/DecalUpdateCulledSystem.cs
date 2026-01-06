using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000070 RID: 112
	internal class DecalUpdateCulledSystem
	{
		// Token: 0x060003E2 RID: 994 RVA: 0x00017424 File Offset: 0x00015624
		public DecalUpdateCulledSystem(DecalEntityManager entityManager)
		{
			this.m_EntityManager = entityManager;
			this.m_Sampler = new ProfilingSampler("DecalUpdateCulledSystem.Execute");
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00017444 File Offset: 0x00015644
		public void Execute()
		{
			using (new ProfilingScope(null, this.m_Sampler))
			{
				for (int i = 0; i < this.m_EntityManager.chunkCount; i++)
				{
					this.Execute(this.m_EntityManager.culledChunks[i], this.m_EntityManager.culledChunks[i].count);
				}
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x000174C4 File Offset: 0x000156C4
		private void Execute(DecalCulledChunk culledChunk, int count)
		{
			if (count == 0)
			{
				return;
			}
			culledChunk.currentJobHandle.Complete();
			CullingGroup cullingGroups = culledChunk.cullingGroups;
			culledChunk.visibleDecalCount = cullingGroups.QueryIndices(true, culledChunk.visibleDecalIndexArray, 0);
			culledChunk.visibleDecalIndices.CopyFrom(culledChunk.visibleDecalIndexArray);
		}

		// Token: 0x040002E2 RID: 738
		private DecalEntityManager m_EntityManager;

		// Token: 0x040002E3 RID: 739
		private ProfilingSampler m_Sampler;
	}
}
