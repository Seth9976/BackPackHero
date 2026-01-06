using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000CC RID: 204
	public enum AntialiasingMode
	{
		// Token: 0x040004B9 RID: 1209
		[InspectorName("No Anti-aliasing")]
		None,
		// Token: 0x040004BA RID: 1210
		[InspectorName("Fast Approximate Anti-aliasing (FXAA)")]
		FastApproximateAntialiasing,
		// Token: 0x040004BB RID: 1211
		[InspectorName("Subpixel Morphological Anti-aliasing (SMAA)")]
		SubpixelMorphologicalAntiAliasing
	}
}
