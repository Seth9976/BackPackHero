using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003F2 RID: 1010
	[Flags]
	public enum CullingOptions
	{
		// Token: 0x04000C9C RID: 3228
		None = 0,
		// Token: 0x04000C9D RID: 3229
		ForceEvenIfCameraIsNotActive = 1,
		// Token: 0x04000C9E RID: 3230
		OcclusionCull = 2,
		// Token: 0x04000C9F RID: 3231
		NeedsLighting = 4,
		// Token: 0x04000CA0 RID: 3232
		NeedsReflectionProbes = 8,
		// Token: 0x04000CA1 RID: 3233
		Stereo = 16,
		// Token: 0x04000CA2 RID: 3234
		DisablePerObjectCulling = 32,
		// Token: 0x04000CA3 RID: 3235
		ShadowCasters = 64
	}
}
