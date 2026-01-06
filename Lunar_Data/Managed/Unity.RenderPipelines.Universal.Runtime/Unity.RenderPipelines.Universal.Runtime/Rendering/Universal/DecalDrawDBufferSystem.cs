using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200005B RID: 91
	internal class DecalDrawDBufferSystem : DecalDrawSystem
	{
		// Token: 0x06000350 RID: 848 RVA: 0x00014698 File Offset: 0x00012898
		public DecalDrawDBufferSystem(DecalEntityManager entityManager)
			: base("DecalDrawIntoDBufferSystem.Execute", entityManager)
		{
		}

		// Token: 0x06000351 RID: 849 RVA: 0x000146A6 File Offset: 0x000128A6
		protected override int GetPassIndex(DecalCachedChunk decalCachedChunk)
		{
			return decalCachedChunk.passIndexDBuffer;
		}
	}
}
