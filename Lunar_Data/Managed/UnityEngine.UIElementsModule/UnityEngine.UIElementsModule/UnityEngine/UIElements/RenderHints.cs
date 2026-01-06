using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000052 RID: 82
	[Flags]
	internal enum RenderHints
	{
		// Token: 0x040000F5 RID: 245
		None = 0,
		// Token: 0x040000F6 RID: 246
		GroupTransform = 1,
		// Token: 0x040000F7 RID: 247
		BoneTransform = 2,
		// Token: 0x040000F8 RID: 248
		ClipWithScissors = 4,
		// Token: 0x040000F9 RID: 249
		MaskContainer = 8,
		// Token: 0x040000FA RID: 250
		DynamicColor = 16,
		// Token: 0x040000FB RID: 251
		DirtyOffset = 5,
		// Token: 0x040000FC RID: 252
		DirtyGroupTransform = 32,
		// Token: 0x040000FD RID: 253
		DirtyBoneTransform = 64,
		// Token: 0x040000FE RID: 254
		DirtyClipWithScissors = 128,
		// Token: 0x040000FF RID: 255
		DirtyMaskContainer = 256,
		// Token: 0x04000100 RID: 256
		DirtyAll = 480
	}
}
