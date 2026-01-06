using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200006D RID: 109
	internal class DecalSkipCulledSystem
	{
		// Token: 0x060003D7 RID: 983 RVA: 0x00016DE0 File Offset: 0x00014FE0
		public DecalSkipCulledSystem(DecalEntityManager entityManager)
		{
			this.m_EntityManager = entityManager;
			this.m_Sampler = new ProfilingSampler("DecalSkipCulledSystem.Execute");
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00016E00 File Offset: 0x00015000
		public void Execute(Camera camera)
		{
			using (new ProfilingScope(null, this.m_Sampler))
			{
				this.m_Camera = camera;
				for (int i = 0; i < this.m_EntityManager.chunkCount; i++)
				{
					this.Execute(this.m_EntityManager.culledChunks[i], this.m_EntityManager.culledChunks[i].count);
				}
			}
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00016E88 File Offset: 0x00015088
		private void Execute(DecalCulledChunk culledChunk, int count)
		{
			if (count == 0)
			{
				return;
			}
			culledChunk.currentJobHandle.Complete();
			for (int i = 0; i < count; i++)
			{
				culledChunk.visibleDecalIndices[i] = i;
			}
			culledChunk.visibleDecalCount = count;
			culledChunk.cameraPosition = this.m_Camera.transform.position;
			culledChunk.cullingMask = this.m_Camera.cullingMask;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00016EEE File Offset: 0x000150EE
		internal static ulong GetSceneCullingMaskFromCamera(Camera camera)
		{
			return 0UL;
		}

		// Token: 0x040002C5 RID: 709
		private DecalEntityManager m_EntityManager;

		// Token: 0x040002C6 RID: 710
		private ProfilingSampler m_Sampler;

		// Token: 0x040002C7 RID: 711
		private Camera m_Camera;
	}
}
