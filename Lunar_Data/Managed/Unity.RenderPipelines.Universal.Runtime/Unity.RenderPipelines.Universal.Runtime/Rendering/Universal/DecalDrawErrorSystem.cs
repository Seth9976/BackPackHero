using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200005F RID: 95
	internal class DecalDrawErrorSystem : DecalDrawSystem
	{
		// Token: 0x06000367 RID: 871 RVA: 0x00014D18 File Offset: 0x00012F18
		public DecalDrawErrorSystem(DecalEntityManager entityManager, DecalTechnique technique)
			: base("DecalDrawErrorSystem.Execute", entityManager)
		{
			this.m_Technique = technique;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00014D30 File Offset: 0x00012F30
		protected override int GetPassIndex(DecalCachedChunk decalCachedChunk)
		{
			switch (this.m_Technique)
			{
			case DecalTechnique.Invalid:
				return 0;
			case DecalTechnique.DBuffer:
				if (decalCachedChunk.passIndexDBuffer != -1 || decalCachedChunk.passIndexEmissive != -1)
				{
					return -1;
				}
				return 0;
			case DecalTechnique.ScreenSpace:
				if (decalCachedChunk.passIndexScreenSpace != -1)
				{
					return -1;
				}
				return 0;
			case DecalTechnique.GBuffer:
				if (decalCachedChunk.passIndexGBuffer != -1)
				{
					return -1;
				}
				return 0;
			default:
				return 0;
			}
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00014D8F File Offset: 0x00012F8F
		protected override Material GetMaterial(DecalEntityChunk decalEntityChunk)
		{
			return this.m_EntityManager.errorMaterial;
		}

		// Token: 0x0400027C RID: 636
		private DecalTechnique m_Technique;
	}
}
