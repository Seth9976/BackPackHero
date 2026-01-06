using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000177 RID: 375
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/ColorGamut.h")]
	public enum ColorGamut
	{
		// Token: 0x040004C4 RID: 1220
		sRGB,
		// Token: 0x040004C5 RID: 1221
		Rec709,
		// Token: 0x040004C6 RID: 1222
		Rec2020,
		// Token: 0x040004C7 RID: 1223
		DisplayP3,
		// Token: 0x040004C8 RID: 1224
		HDR10,
		// Token: 0x040004C9 RID: 1225
		DolbyHDR
	}
}
