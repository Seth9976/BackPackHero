using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000B1 RID: 177
	[Serializable]
	internal class ScreenSpaceAmbientOcclusionSettings
	{
		// Token: 0x04000433 RID: 1075
		[SerializeField]
		internal bool Downsample;

		// Token: 0x04000434 RID: 1076
		[SerializeField]
		internal bool AfterOpaque;

		// Token: 0x04000435 RID: 1077
		[SerializeField]
		internal ScreenSpaceAmbientOcclusionSettings.DepthSource Source = ScreenSpaceAmbientOcclusionSettings.DepthSource.DepthNormals;

		// Token: 0x04000436 RID: 1078
		[SerializeField]
		internal ScreenSpaceAmbientOcclusionSettings.NormalQuality NormalSamples = ScreenSpaceAmbientOcclusionSettings.NormalQuality.Medium;

		// Token: 0x04000437 RID: 1079
		[SerializeField]
		internal float Intensity = 3f;

		// Token: 0x04000438 RID: 1080
		[SerializeField]
		internal float DirectLightingStrength = 0.25f;

		// Token: 0x04000439 RID: 1081
		[SerializeField]
		internal float Radius = 0.035f;

		// Token: 0x0400043A RID: 1082
		[SerializeField]
		internal int SampleCount = 4;

		// Token: 0x0200017C RID: 380
		internal enum DepthSource
		{
			// Token: 0x040009AE RID: 2478
			Depth,
			// Token: 0x040009AF RID: 2479
			DepthNormals
		}

		// Token: 0x0200017D RID: 381
		internal enum NormalQuality
		{
			// Token: 0x040009B1 RID: 2481
			Low,
			// Token: 0x040009B2 RID: 2482
			Medium,
			// Token: 0x040009B3 RID: 2483
			High
		}
	}
}
