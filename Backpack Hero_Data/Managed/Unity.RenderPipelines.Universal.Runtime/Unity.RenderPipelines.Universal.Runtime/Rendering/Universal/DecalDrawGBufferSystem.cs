using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000073 RID: 115
	internal class DecalDrawGBufferSystem : DecalDrawSystem
	{
		// Token: 0x060003EF RID: 1007 RVA: 0x0001774D File Offset: 0x0001594D
		public DecalDrawGBufferSystem(DecalEntityManager entityManager)
			: base("DecalDrawGBufferSystem.Execute", entityManager)
		{
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0001775B File Offset: 0x0001595B
		protected override int GetPassIndex(DecalCachedChunk decalCachedChunk)
		{
			return decalCachedChunk.passIndexGBuffer;
		}
	}
}
