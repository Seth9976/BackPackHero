using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200000C RID: 12
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "C:\\Users\\jaspe\\Backpack Hero\\Library\\PackageCache\\com.unity.render-pipelines.universal@12.1.10\\ShaderLibrary\\Debug\\DebugViewEnums.cs")]
	[Flags]
	public enum DebugLightingFeatureFlags
	{
		// Token: 0x04000041 RID: 65
		None = 0,
		// Token: 0x04000042 RID: 66
		GlobalIllumination = 1,
		// Token: 0x04000043 RID: 67
		MainLight = 2,
		// Token: 0x04000044 RID: 68
		AdditionalLights = 4,
		// Token: 0x04000045 RID: 69
		VertexLighting = 8,
		// Token: 0x04000046 RID: 70
		Emission = 16,
		// Token: 0x04000047 RID: 71
		AmbientOcclusion = 32
	}
}
