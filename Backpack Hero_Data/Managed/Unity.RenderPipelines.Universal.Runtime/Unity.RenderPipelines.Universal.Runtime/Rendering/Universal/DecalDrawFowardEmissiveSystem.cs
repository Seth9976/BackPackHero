using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200005D RID: 93
	internal class DecalDrawFowardEmissiveSystem : DecalDrawSystem
	{
		// Token: 0x06000363 RID: 867 RVA: 0x00014BF1 File Offset: 0x00012DF1
		public DecalDrawFowardEmissiveSystem(DecalEntityManager entityManager)
			: base("DecalDrawFowardEmissiveSystem.Execute", entityManager)
		{
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00014BFF File Offset: 0x00012DFF
		protected override int GetPassIndex(DecalCachedChunk decalCachedChunk)
		{
			return decalCachedChunk.passIndexEmissive;
		}
	}
}
