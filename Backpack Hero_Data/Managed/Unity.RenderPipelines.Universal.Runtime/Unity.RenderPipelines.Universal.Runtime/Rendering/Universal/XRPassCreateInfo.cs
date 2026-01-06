using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000EB RID: 235
	internal struct XRPassCreateInfo
	{
		// Token: 0x0400066C RID: 1644
		public int multipassId;

		// Token: 0x0400066D RID: 1645
		public int cullingPassId;

		// Token: 0x0400066E RID: 1646
		public RenderTexture renderTarget;

		// Token: 0x0400066F RID: 1647
		public RenderTextureDescriptor renderTargetDesc;

		// Token: 0x04000670 RID: 1648
		public bool renderTargetIsRenderTexture;

		// Token: 0x04000671 RID: 1649
		public ScriptableCullingParameters cullingParameters;

		// Token: 0x04000672 RID: 1650
		public XRPass.CustomMirrorView customMirrorView;
	}
}
