using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200000B RID: 11
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "C:\\Users\\jaspe\\Lunar Vampire Cards\\Library\\PackageCache\\com.unity.render-pipelines.universal@12.1.10\\ShaderLibrary\\Debug\\DebugViewEnums.cs")]
	public enum DebugLightingMode
	{
		// Token: 0x0400003A RID: 58
		None,
		// Token: 0x0400003B RID: 59
		ShadowCascades,
		// Token: 0x0400003C RID: 60
		LightingWithoutNormalMaps,
		// Token: 0x0400003D RID: 61
		LightingWithNormalMaps,
		// Token: 0x0400003E RID: 62
		Reflections,
		// Token: 0x0400003F RID: 63
		ReflectionsWithSmoothness
	}
}
