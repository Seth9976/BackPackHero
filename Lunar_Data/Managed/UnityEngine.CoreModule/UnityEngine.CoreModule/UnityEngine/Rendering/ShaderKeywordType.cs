using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000421 RID: 1057
	[UsedByNativeCode]
	[NativeHeader("Runtime/Shaders/Keywords/KeywordSpaceScriptBindings.h")]
	public enum ShaderKeywordType
	{
		// Token: 0x04000DA6 RID: 3494
		None,
		// Token: 0x04000DA7 RID: 3495
		BuiltinDefault = 2,
		// Token: 0x04000DA8 RID: 3496
		[Obsolete("Shader keyword type BuiltinExtra is no longer used. Use BuiltinDefault instead. (UnityUpgradable) -> BuiltinDefault")]
		BuiltinExtra = 6,
		// Token: 0x04000DA9 RID: 3497
		[Obsolete("Shader keyword type BuiltinAutoStripped is no longer used. Use BuiltinDefault instead. (UnityUpgradable) -> BuiltinDefault")]
		BuiltinAutoStripped = 10,
		// Token: 0x04000DAA RID: 3498
		UserDefined = 16,
		// Token: 0x04000DAB RID: 3499
		Plugin = 32
	}
}
