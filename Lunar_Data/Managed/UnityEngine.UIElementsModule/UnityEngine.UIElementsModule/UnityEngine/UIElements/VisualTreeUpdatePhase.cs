using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000FC RID: 252
	internal enum VisualTreeUpdatePhase
	{
		// Token: 0x04000338 RID: 824
		ViewData,
		// Token: 0x04000339 RID: 825
		Bindings,
		// Token: 0x0400033A RID: 826
		Animation,
		// Token: 0x0400033B RID: 827
		Styles,
		// Token: 0x0400033C RID: 828
		Layout,
		// Token: 0x0400033D RID: 829
		TransformClip,
		// Token: 0x0400033E RID: 830
		Repaint,
		// Token: 0x0400033F RID: 831
		Count
	}
}
