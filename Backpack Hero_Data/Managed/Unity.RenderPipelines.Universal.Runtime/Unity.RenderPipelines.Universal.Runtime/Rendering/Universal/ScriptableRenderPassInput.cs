using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000A1 RID: 161
	[Flags]
	public enum ScriptableRenderPassInput
	{
		// Token: 0x040003D8 RID: 984
		None = 0,
		// Token: 0x040003D9 RID: 985
		Depth = 1,
		// Token: 0x040003DA RID: 986
		Normal = 2,
		// Token: 0x040003DB RID: 987
		Color = 4,
		// Token: 0x040003DC RID: 988
		Motion = 8
	}
}
