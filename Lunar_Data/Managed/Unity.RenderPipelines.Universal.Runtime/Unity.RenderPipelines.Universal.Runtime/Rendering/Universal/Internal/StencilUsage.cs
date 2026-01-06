using System;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x02000118 RID: 280
	internal enum StencilUsage
	{
		// Token: 0x04000811 RID: 2065
		UserMask = 15,
		// Token: 0x04000812 RID: 2066
		StencilLight,
		// Token: 0x04000813 RID: 2067
		MaterialMask = 96,
		// Token: 0x04000814 RID: 2068
		MaterialUnlit = 0,
		// Token: 0x04000815 RID: 2069
		MaterialLit = 32,
		// Token: 0x04000816 RID: 2070
		MaterialSimpleLit = 64
	}
}
