using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000425 RID: 1061
	[Flags]
	public enum ShaderPropertyFlags
	{
		// Token: 0x04000DBD RID: 3517
		None = 0,
		// Token: 0x04000DBE RID: 3518
		HideInInspector = 1,
		// Token: 0x04000DBF RID: 3519
		PerRendererData = 2,
		// Token: 0x04000DC0 RID: 3520
		NoScaleOffset = 4,
		// Token: 0x04000DC1 RID: 3521
		Normal = 8,
		// Token: 0x04000DC2 RID: 3522
		HDR = 16,
		// Token: 0x04000DC3 RID: 3523
		Gamma = 32,
		// Token: 0x04000DC4 RID: 3524
		NonModifiableTextureData = 64,
		// Token: 0x04000DC5 RID: 3525
		MainTexture = 128,
		// Token: 0x04000DC6 RID: 3526
		MainColor = 256
	}
}
