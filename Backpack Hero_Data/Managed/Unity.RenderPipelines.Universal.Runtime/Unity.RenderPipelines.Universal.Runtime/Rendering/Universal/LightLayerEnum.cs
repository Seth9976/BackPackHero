using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000D2 RID: 210
	[Flags]
	public enum LightLayerEnum
	{
		// Token: 0x040004E0 RID: 1248
		Nothing = 0,
		// Token: 0x040004E1 RID: 1249
		LightLayerDefault = 1,
		// Token: 0x040004E2 RID: 1250
		LightLayer1 = 2,
		// Token: 0x040004E3 RID: 1251
		LightLayer2 = 4,
		// Token: 0x040004E4 RID: 1252
		LightLayer3 = 8,
		// Token: 0x040004E5 RID: 1253
		LightLayer4 = 16,
		// Token: 0x040004E6 RID: 1254
		LightLayer5 = 32,
		// Token: 0x040004E7 RID: 1255
		LightLayer6 = 64,
		// Token: 0x040004E8 RID: 1256
		LightLayer7 = 128,
		// Token: 0x040004E9 RID: 1257
		Everything = 255
	}
}
