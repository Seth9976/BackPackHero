using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000053 RID: 83
	public enum DynamicResUpscaleFilter : byte
	{
		// Token: 0x040001DF RID: 479
		[Obsolete("Bilinear upscale filter is considered obsolete and is not supported anymore, please use CatmullRom for a very cheap, but blurry filter.", false)]
		Bilinear,
		// Token: 0x040001E0 RID: 480
		CatmullRom,
		// Token: 0x040001E1 RID: 481
		[Obsolete("Lanczos upscale filter is considered obsolete and is not supported anymore, please use Contrast Adaptive Sharpening for very sharp filter or FidelityFX Super Resolution 1.0.", false)]
		Lanczos,
		// Token: 0x040001E2 RID: 482
		ContrastAdaptiveSharpen,
		// Token: 0x040001E3 RID: 483
		[InspectorName("FidelityFX Super Resolution 1.0")]
		EdgeAdaptiveScalingUpres,
		// Token: 0x040001E4 RID: 484
		[InspectorName("TAA Upscale")]
		TAAU
	}
}
