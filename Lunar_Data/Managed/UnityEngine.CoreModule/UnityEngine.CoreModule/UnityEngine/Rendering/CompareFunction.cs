using System;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x020003AC RID: 940
	[NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h")]
	public enum CompareFunction
	{
		// Token: 0x04000AB5 RID: 2741
		Disabled,
		// Token: 0x04000AB6 RID: 2742
		Never,
		// Token: 0x04000AB7 RID: 2743
		Less,
		// Token: 0x04000AB8 RID: 2744
		Equal,
		// Token: 0x04000AB9 RID: 2745
		LessEqual,
		// Token: 0x04000ABA RID: 2746
		Greater,
		// Token: 0x04000ABB RID: 2747
		NotEqual,
		// Token: 0x04000ABC RID: 2748
		GreaterEqual,
		// Token: 0x04000ABD RID: 2749
		Always
	}
}
