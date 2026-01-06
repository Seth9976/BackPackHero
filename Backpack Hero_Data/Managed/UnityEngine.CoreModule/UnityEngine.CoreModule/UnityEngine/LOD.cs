using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000198 RID: 408
	[UsedByNativeCode]
	public struct LOD
	{
		// Token: 0x06000EF4 RID: 3828 RVA: 0x00012DF9 File Offset: 0x00010FF9
		public LOD(float screenRelativeTransitionHeight, Renderer[] renderers)
		{
			this.screenRelativeTransitionHeight = screenRelativeTransitionHeight;
			this.fadeTransitionWidth = 0f;
			this.renderers = renderers;
		}

		// Token: 0x040005A1 RID: 1441
		public float screenRelativeTransitionHeight;

		// Token: 0x040005A2 RID: 1442
		public float fadeTransitionWidth;

		// Token: 0x040005A3 RID: 1443
		public Renderer[] renderers;
	}
}
