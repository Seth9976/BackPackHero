using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000411 RID: 1041
	[Flags]
	public enum SortingCriteria
	{
		// Token: 0x04000D40 RID: 3392
		None = 0,
		// Token: 0x04000D41 RID: 3393
		SortingLayer = 1,
		// Token: 0x04000D42 RID: 3394
		RenderQueue = 2,
		// Token: 0x04000D43 RID: 3395
		BackToFront = 4,
		// Token: 0x04000D44 RID: 3396
		QuantizedFrontToBack = 8,
		// Token: 0x04000D45 RID: 3397
		OptimizeStateChanges = 16,
		// Token: 0x04000D46 RID: 3398
		CanvasOrder = 32,
		// Token: 0x04000D47 RID: 3399
		RendererPriority = 64,
		// Token: 0x04000D48 RID: 3400
		CommonOpaque = 59,
		// Token: 0x04000D49 RID: 3401
		CommonTransparent = 23
	}
}
