using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000042 RID: 66
	[Flags]
	public enum ClearFlag
	{
		// Token: 0x040001A2 RID: 418
		None = 0,
		// Token: 0x040001A3 RID: 419
		Color = 1,
		// Token: 0x040001A4 RID: 420
		Depth = 2,
		// Token: 0x040001A5 RID: 421
		Stencil = 4,
		// Token: 0x040001A6 RID: 422
		DepthStencil = 6,
		// Token: 0x040001A7 RID: 423
		ColorStencil = 5,
		// Token: 0x040001A8 RID: 424
		All = 7
	}
}
