using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003D7 RID: 983
	[Flags]
	public enum RTClearFlags
	{
		// Token: 0x04000BFF RID: 3071
		None = 0,
		// Token: 0x04000C00 RID: 3072
		Color = 1,
		// Token: 0x04000C01 RID: 3073
		Depth = 2,
		// Token: 0x04000C02 RID: 3074
		Stencil = 4,
		// Token: 0x04000C03 RID: 3075
		All = 7,
		// Token: 0x04000C04 RID: 3076
		DepthStencil = 6,
		// Token: 0x04000C05 RID: 3077
		ColorDepth = 3,
		// Token: 0x04000C06 RID: 3078
		ColorStencil = 5
	}
}
