using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200004A RID: 74
	public enum UpscalingFilterSelection
	{
		// Token: 0x040001D5 RID: 469
		[InspectorName("Automatic")]
		Auto,
		// Token: 0x040001D6 RID: 470
		[InspectorName("Bilinear")]
		Linear,
		// Token: 0x040001D7 RID: 471
		[InspectorName("Nearest-Neighbor")]
		Point,
		// Token: 0x040001D8 RID: 472
		[InspectorName("FidelityFX Super Resolution 1.0")]
		FSR
	}
}
