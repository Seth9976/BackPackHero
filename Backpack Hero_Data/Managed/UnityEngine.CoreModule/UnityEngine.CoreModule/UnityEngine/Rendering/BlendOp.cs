using System;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x020003AB RID: 939
	[NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h")]
	public enum BlendOp
	{
		// Token: 0x04000A90 RID: 2704
		Add,
		// Token: 0x04000A91 RID: 2705
		Subtract,
		// Token: 0x04000A92 RID: 2706
		ReverseSubtract,
		// Token: 0x04000A93 RID: 2707
		Min,
		// Token: 0x04000A94 RID: 2708
		Max,
		// Token: 0x04000A95 RID: 2709
		LogicalClear,
		// Token: 0x04000A96 RID: 2710
		LogicalSet,
		// Token: 0x04000A97 RID: 2711
		LogicalCopy,
		// Token: 0x04000A98 RID: 2712
		LogicalCopyInverted,
		// Token: 0x04000A99 RID: 2713
		LogicalNoop,
		// Token: 0x04000A9A RID: 2714
		LogicalInvert,
		// Token: 0x04000A9B RID: 2715
		LogicalAnd,
		// Token: 0x04000A9C RID: 2716
		LogicalNand,
		// Token: 0x04000A9D RID: 2717
		LogicalOr,
		// Token: 0x04000A9E RID: 2718
		LogicalNor,
		// Token: 0x04000A9F RID: 2719
		LogicalXor,
		// Token: 0x04000AA0 RID: 2720
		LogicalEquivalence,
		// Token: 0x04000AA1 RID: 2721
		LogicalAndReverse,
		// Token: 0x04000AA2 RID: 2722
		LogicalAndInverted,
		// Token: 0x04000AA3 RID: 2723
		LogicalOrReverse,
		// Token: 0x04000AA4 RID: 2724
		LogicalOrInverted,
		// Token: 0x04000AA5 RID: 2725
		Multiply,
		// Token: 0x04000AA6 RID: 2726
		Screen,
		// Token: 0x04000AA7 RID: 2727
		Overlay,
		// Token: 0x04000AA8 RID: 2728
		Darken,
		// Token: 0x04000AA9 RID: 2729
		Lighten,
		// Token: 0x04000AAA RID: 2730
		ColorDodge,
		// Token: 0x04000AAB RID: 2731
		ColorBurn,
		// Token: 0x04000AAC RID: 2732
		HardLight,
		// Token: 0x04000AAD RID: 2733
		SoftLight,
		// Token: 0x04000AAE RID: 2734
		Difference,
		// Token: 0x04000AAF RID: 2735
		Exclusion,
		// Token: 0x04000AB0 RID: 2736
		HSLHue,
		// Token: 0x04000AB1 RID: 2737
		HSLSaturation,
		// Token: 0x04000AB2 RID: 2738
		HSLColor,
		// Token: 0x04000AB3 RID: 2739
		HSLLuminosity
	}
}
