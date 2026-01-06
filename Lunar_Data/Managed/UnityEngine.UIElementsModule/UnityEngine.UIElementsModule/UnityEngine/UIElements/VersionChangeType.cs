using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000050 RID: 80
	[Flags]
	internal enum VersionChangeType
	{
		// Token: 0x040000DE RID: 222
		Bindings = 1,
		// Token: 0x040000DF RID: 223
		ViewData = 2,
		// Token: 0x040000E0 RID: 224
		Hierarchy = 4,
		// Token: 0x040000E1 RID: 225
		Layout = 8,
		// Token: 0x040000E2 RID: 226
		StyleSheet = 16,
		// Token: 0x040000E3 RID: 227
		Styles = 32,
		// Token: 0x040000E4 RID: 228
		Overflow = 64,
		// Token: 0x040000E5 RID: 229
		BorderRadius = 128,
		// Token: 0x040000E6 RID: 230
		BorderWidth = 256,
		// Token: 0x040000E7 RID: 231
		Transform = 512,
		// Token: 0x040000E8 RID: 232
		Size = 1024,
		// Token: 0x040000E9 RID: 233
		Repaint = 2048,
		// Token: 0x040000EA RID: 234
		Opacity = 4096,
		// Token: 0x040000EB RID: 235
		Color = 8192,
		// Token: 0x040000EC RID: 236
		RenderHints = 16384,
		// Token: 0x040000ED RID: 237
		TransitionProperty = 32768
	}
}
