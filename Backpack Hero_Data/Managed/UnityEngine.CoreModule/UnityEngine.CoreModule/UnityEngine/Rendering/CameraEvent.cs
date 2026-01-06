using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003B3 RID: 947
	public enum CameraEvent
	{
		// Token: 0x04000ADE RID: 2782
		BeforeDepthTexture,
		// Token: 0x04000ADF RID: 2783
		AfterDepthTexture,
		// Token: 0x04000AE0 RID: 2784
		BeforeDepthNormalsTexture,
		// Token: 0x04000AE1 RID: 2785
		AfterDepthNormalsTexture,
		// Token: 0x04000AE2 RID: 2786
		BeforeGBuffer,
		// Token: 0x04000AE3 RID: 2787
		AfterGBuffer,
		// Token: 0x04000AE4 RID: 2788
		BeforeLighting,
		// Token: 0x04000AE5 RID: 2789
		AfterLighting,
		// Token: 0x04000AE6 RID: 2790
		BeforeFinalPass,
		// Token: 0x04000AE7 RID: 2791
		AfterFinalPass,
		// Token: 0x04000AE8 RID: 2792
		BeforeForwardOpaque,
		// Token: 0x04000AE9 RID: 2793
		AfterForwardOpaque,
		// Token: 0x04000AEA RID: 2794
		BeforeImageEffectsOpaque,
		// Token: 0x04000AEB RID: 2795
		AfterImageEffectsOpaque,
		// Token: 0x04000AEC RID: 2796
		BeforeSkybox,
		// Token: 0x04000AED RID: 2797
		AfterSkybox,
		// Token: 0x04000AEE RID: 2798
		BeforeForwardAlpha,
		// Token: 0x04000AEF RID: 2799
		AfterForwardAlpha,
		// Token: 0x04000AF0 RID: 2800
		BeforeImageEffects,
		// Token: 0x04000AF1 RID: 2801
		AfterImageEffects,
		// Token: 0x04000AF2 RID: 2802
		AfterEverything,
		// Token: 0x04000AF3 RID: 2803
		BeforeReflections,
		// Token: 0x04000AF4 RID: 2804
		AfterReflections,
		// Token: 0x04000AF5 RID: 2805
		BeforeHaloAndLensFlares,
		// Token: 0x04000AF6 RID: 2806
		AfterHaloAndLensFlares
	}
}
