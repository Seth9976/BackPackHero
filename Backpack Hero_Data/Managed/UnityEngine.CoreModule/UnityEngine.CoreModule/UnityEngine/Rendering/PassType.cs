using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003B8 RID: 952
	public enum PassType
	{
		// Token: 0x04000B28 RID: 2856
		Normal,
		// Token: 0x04000B29 RID: 2857
		Vertex,
		// Token: 0x04000B2A RID: 2858
		VertexLM,
		// Token: 0x04000B2B RID: 2859
		[Obsolete("VertexLMRGBM PassType is obsolete. Please use VertexLM PassType together with DecodeLightmap shader function.")]
		VertexLMRGBM,
		// Token: 0x04000B2C RID: 2860
		ForwardBase,
		// Token: 0x04000B2D RID: 2861
		ForwardAdd,
		// Token: 0x04000B2E RID: 2862
		LightPrePassBase,
		// Token: 0x04000B2F RID: 2863
		LightPrePassFinal,
		// Token: 0x04000B30 RID: 2864
		ShadowCaster,
		// Token: 0x04000B31 RID: 2865
		Deferred = 10,
		// Token: 0x04000B32 RID: 2866
		Meta,
		// Token: 0x04000B33 RID: 2867
		MotionVectors,
		// Token: 0x04000B34 RID: 2868
		ScriptableRenderPipeline,
		// Token: 0x04000B35 RID: 2869
		ScriptableRenderPipelineDefaultUnlit,
		// Token: 0x04000B36 RID: 2870
		GrabPass
	}
}
