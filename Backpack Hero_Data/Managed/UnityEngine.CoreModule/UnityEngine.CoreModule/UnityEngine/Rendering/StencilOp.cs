using System;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x020003AF RID: 943
	[NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h")]
	public enum StencilOp
	{
		// Token: 0x04000AC9 RID: 2761
		Keep,
		// Token: 0x04000ACA RID: 2762
		Zero,
		// Token: 0x04000ACB RID: 2763
		Replace,
		// Token: 0x04000ACC RID: 2764
		IncrementSaturate,
		// Token: 0x04000ACD RID: 2765
		DecrementSaturate,
		// Token: 0x04000ACE RID: 2766
		Invert,
		// Token: 0x04000ACF RID: 2767
		IncrementWrap,
		// Token: 0x04000AD0 RID: 2768
		DecrementWrap
	}
}
