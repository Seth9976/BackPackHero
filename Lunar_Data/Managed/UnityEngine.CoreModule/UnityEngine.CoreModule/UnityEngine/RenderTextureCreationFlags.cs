using System;

namespace UnityEngine
{
	// Token: 0x02000180 RID: 384
	[Flags]
	public enum RenderTextureCreationFlags
	{
		// Token: 0x04000551 RID: 1361
		MipMap = 1,
		// Token: 0x04000552 RID: 1362
		AutoGenerateMips = 2,
		// Token: 0x04000553 RID: 1363
		SRGB = 4,
		// Token: 0x04000554 RID: 1364
		EyeTexture = 8,
		// Token: 0x04000555 RID: 1365
		EnableRandomWrite = 16,
		// Token: 0x04000556 RID: 1366
		CreatedFromScript = 32,
		// Token: 0x04000557 RID: 1367
		AllowVerticalFlip = 128,
		// Token: 0x04000558 RID: 1368
		NoResolvedColorSurface = 256,
		// Token: 0x04000559 RID: 1369
		DynamicallyScalable = 1024,
		// Token: 0x0400055A RID: 1370
		BindMS = 2048
	}
}
