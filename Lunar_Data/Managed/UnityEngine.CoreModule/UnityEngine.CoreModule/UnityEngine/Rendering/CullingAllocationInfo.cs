using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003F7 RID: 1015
	internal struct CullingAllocationInfo
	{
		// Token: 0x04000CC5 RID: 3269
		public unsafe VisibleLight* visibleLightsPtr;

		// Token: 0x04000CC6 RID: 3270
		public unsafe VisibleLight* visibleOffscreenVertexLightsPtr;

		// Token: 0x04000CC7 RID: 3271
		public unsafe VisibleReflectionProbe* visibleReflectionProbesPtr;

		// Token: 0x04000CC8 RID: 3272
		public int visibleLightCount;

		// Token: 0x04000CC9 RID: 3273
		public int visibleOffscreenVertexLightCount;

		// Token: 0x04000CCA RID: 3274
		public int visibleReflectionProbeCount;
	}
}
