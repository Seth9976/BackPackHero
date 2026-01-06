using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200030F RID: 783
	[Flags]
	internal enum RenderDataDirtyTypes
	{
		// Token: 0x04000B4D RID: 2893
		None = 0,
		// Token: 0x04000B4E RID: 2894
		Transform = 1,
		// Token: 0x04000B4F RID: 2895
		ClipRectSize = 2,
		// Token: 0x04000B50 RID: 2896
		Clipping = 4,
		// Token: 0x04000B51 RID: 2897
		ClippingHierarchy = 8,
		// Token: 0x04000B52 RID: 2898
		Visuals = 16,
		// Token: 0x04000B53 RID: 2899
		VisualsHierarchy = 32,
		// Token: 0x04000B54 RID: 2900
		Opacity = 64,
		// Token: 0x04000B55 RID: 2901
		OpacityHierarchy = 128,
		// Token: 0x04000B56 RID: 2902
		Color = 256
	}
}
