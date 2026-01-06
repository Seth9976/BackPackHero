using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003B6 RID: 950
	[Flags]
	public enum ShadowMapPass
	{
		// Token: 0x04000B01 RID: 2817
		PointlightPositiveX = 1,
		// Token: 0x04000B02 RID: 2818
		PointlightNegativeX = 2,
		// Token: 0x04000B03 RID: 2819
		PointlightPositiveY = 4,
		// Token: 0x04000B04 RID: 2820
		PointlightNegativeY = 8,
		// Token: 0x04000B05 RID: 2821
		PointlightPositiveZ = 16,
		// Token: 0x04000B06 RID: 2822
		PointlightNegativeZ = 32,
		// Token: 0x04000B07 RID: 2823
		DirectionalCascade0 = 64,
		// Token: 0x04000B08 RID: 2824
		DirectionalCascade1 = 128,
		// Token: 0x04000B09 RID: 2825
		DirectionalCascade2 = 256,
		// Token: 0x04000B0A RID: 2826
		DirectionalCascade3 = 512,
		// Token: 0x04000B0B RID: 2827
		Spotlight = 1024,
		// Token: 0x04000B0C RID: 2828
		Pointlight = 63,
		// Token: 0x04000B0D RID: 2829
		Directional = 960,
		// Token: 0x04000B0E RID: 2830
		All = 2047
	}
}
