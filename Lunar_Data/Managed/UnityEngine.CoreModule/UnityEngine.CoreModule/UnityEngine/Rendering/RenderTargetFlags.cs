using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003C1 RID: 961
	[Flags]
	public enum RenderTargetFlags
	{
		// Token: 0x04000B7A RID: 2938
		None = 0,
		// Token: 0x04000B7B RID: 2939
		ReadOnlyDepth = 1,
		// Token: 0x04000B7C RID: 2940
		ReadOnlyStencil = 2,
		// Token: 0x04000B7D RID: 2941
		ReadOnlyDepthStencil = 3
	}
}
