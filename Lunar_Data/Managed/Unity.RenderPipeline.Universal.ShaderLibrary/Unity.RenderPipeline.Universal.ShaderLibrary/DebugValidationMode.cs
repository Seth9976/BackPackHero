using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000009 RID: 9
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "C:\\Users\\jaspe\\Lunar Vampire Cards\\Library\\PackageCache\\com.unity.render-pipelines.universal@12.1.10\\ShaderLibrary\\Debug\\DebugViewEnums.cs")]
	public enum DebugValidationMode
	{
		// Token: 0x04000030 RID: 48
		None,
		// Token: 0x04000031 RID: 49
		[InspectorName("Highlight NaN, Inf and Negative Values")]
		HighlightNanInfNegative,
		// Token: 0x04000032 RID: 50
		[InspectorName("Highlight Values Outside Range")]
		HighlightOutsideOfRange
	}
}
