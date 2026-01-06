using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000DE RID: 222
	public struct RenderingData
	{
		// Token: 0x04000567 RID: 1383
		public CullingResults cullResults;

		// Token: 0x04000568 RID: 1384
		public CameraData cameraData;

		// Token: 0x04000569 RID: 1385
		public LightData lightData;

		// Token: 0x0400056A RID: 1386
		public ShadowData shadowData;

		// Token: 0x0400056B RID: 1387
		public PostProcessingData postProcessingData;

		// Token: 0x0400056C RID: 1388
		public bool supportsDynamicBatching;

		// Token: 0x0400056D RID: 1389
		public PerObjectData perObjectData;

		// Token: 0x0400056E RID: 1390
		public bool postProcessingEnabled;
	}
}
