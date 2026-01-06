using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003D1 RID: 977
	[Flags]
	public enum CopyTextureSupport
	{
		// Token: 0x04000BE4 RID: 3044
		None = 0,
		// Token: 0x04000BE5 RID: 3045
		Basic = 1,
		// Token: 0x04000BE6 RID: 3046
		Copy3D = 2,
		// Token: 0x04000BE7 RID: 3047
		DifferentTypes = 4,
		// Token: 0x04000BE8 RID: 3048
		TextureToRT = 8,
		// Token: 0x04000BE9 RID: 3049
		RTToTexture = 16
	}
}
