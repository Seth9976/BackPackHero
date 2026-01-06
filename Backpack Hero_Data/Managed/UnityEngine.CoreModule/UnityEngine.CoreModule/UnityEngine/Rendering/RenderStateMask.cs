using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000408 RID: 1032
	[Flags]
	public enum RenderStateMask
	{
		// Token: 0x04000D1E RID: 3358
		Nothing = 0,
		// Token: 0x04000D1F RID: 3359
		Blend = 1,
		// Token: 0x04000D20 RID: 3360
		Raster = 2,
		// Token: 0x04000D21 RID: 3361
		Depth = 4,
		// Token: 0x04000D22 RID: 3362
		Stencil = 8,
		// Token: 0x04000D23 RID: 3363
		Everything = 15
	}
}
