using System;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x020003AA RID: 938
	[NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h")]
	public enum BlendMode
	{
		// Token: 0x04000A84 RID: 2692
		Zero,
		// Token: 0x04000A85 RID: 2693
		One,
		// Token: 0x04000A86 RID: 2694
		DstColor,
		// Token: 0x04000A87 RID: 2695
		SrcColor,
		// Token: 0x04000A88 RID: 2696
		OneMinusDstColor,
		// Token: 0x04000A89 RID: 2697
		SrcAlpha,
		// Token: 0x04000A8A RID: 2698
		OneMinusSrcColor,
		// Token: 0x04000A8B RID: 2699
		DstAlpha,
		// Token: 0x04000A8C RID: 2700
		OneMinusDstAlpha,
		// Token: 0x04000A8D RID: 2701
		SrcAlphaSaturate,
		// Token: 0x04000A8E RID: 2702
		OneMinusSrcAlpha
	}
}
