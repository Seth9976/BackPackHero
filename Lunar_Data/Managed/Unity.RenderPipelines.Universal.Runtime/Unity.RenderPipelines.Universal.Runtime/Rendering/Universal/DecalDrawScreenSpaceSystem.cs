using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000075 RID: 117
	internal class DecalDrawScreenSpaceSystem : DecalDrawSystem
	{
		// Token: 0x060003F6 RID: 1014 RVA: 0x000179F8 File Offset: 0x00015BF8
		public DecalDrawScreenSpaceSystem(DecalEntityManager entityManager)
			: base("DecalDrawScreenSpaceSystem.Execute", entityManager)
		{
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00017A06 File Offset: 0x00015C06
		protected override int GetPassIndex(DecalCachedChunk decalCachedChunk)
		{
			return decalCachedChunk.passIndexScreenSpace;
		}
	}
}
