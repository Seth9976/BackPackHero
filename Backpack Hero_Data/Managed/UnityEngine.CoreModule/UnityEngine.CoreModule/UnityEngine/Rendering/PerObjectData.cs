using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003FF RID: 1023
	[Flags]
	public enum PerObjectData
	{
		// Token: 0x04000CEB RID: 3307
		None = 0,
		// Token: 0x04000CEC RID: 3308
		LightProbe = 1,
		// Token: 0x04000CED RID: 3309
		ReflectionProbes = 2,
		// Token: 0x04000CEE RID: 3310
		LightProbeProxyVolume = 4,
		// Token: 0x04000CEF RID: 3311
		Lightmaps = 8,
		// Token: 0x04000CF0 RID: 3312
		LightData = 16,
		// Token: 0x04000CF1 RID: 3313
		MotionVectors = 32,
		// Token: 0x04000CF2 RID: 3314
		LightIndices = 64,
		// Token: 0x04000CF3 RID: 3315
		ReflectionProbeData = 128,
		// Token: 0x04000CF4 RID: 3316
		OcclusionProbe = 256,
		// Token: 0x04000CF5 RID: 3317
		OcclusionProbeProxyVolume = 512,
		// Token: 0x04000CF6 RID: 3318
		ShadowMask = 1024
	}
}
