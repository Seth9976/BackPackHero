using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000096 RID: 150
	[VolumeComponentMenuForRenderPipeline("Post-processing/Shadows, Midtones, Highlights", new Type[] { typeof(UniversalRenderPipeline) })]
	[Serializable]
	public sealed class ShadowsMidtonesHighlights : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x06000500 RID: 1280 RVA: 0x0001D624 File Offset: 0x0001B824
		public bool IsActive()
		{
			Vector4 vector = new Vector4(1f, 1f, 1f, 0f);
			return this.shadows != vector || this.midtones != vector || this.highlights != vector;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0001D676 File Offset: 0x0001B876
		public bool IsTileCompatible()
		{
			return true;
		}

		// Token: 0x040003BE RID: 958
		[Tooltip("Use this to control and apply a hue to the shadows.")]
		public Vector4Parameter shadows = new Vector4Parameter(new Vector4(1f, 1f, 1f, 0f), false);

		// Token: 0x040003BF RID: 959
		[Tooltip("Use this to control and apply a hue to the midtones.")]
		public Vector4Parameter midtones = new Vector4Parameter(new Vector4(1f, 1f, 1f, 0f), false);

		// Token: 0x040003C0 RID: 960
		[Tooltip("Use this to control and apply a hue to the highlights.")]
		public Vector4Parameter highlights = new Vector4Parameter(new Vector4(1f, 1f, 1f, 0f), false);

		// Token: 0x040003C1 RID: 961
		[Header("Shadow Limits")]
		[Tooltip("Start point of the transition between shadows and midtones.")]
		public MinFloatParameter shadowsStart = new MinFloatParameter(0f, 0f, false);

		// Token: 0x040003C2 RID: 962
		[Tooltip("End point of the transition between shadows and midtones.")]
		public MinFloatParameter shadowsEnd = new MinFloatParameter(0.3f, 0f, false);

		// Token: 0x040003C3 RID: 963
		[Header("Highlight Limits")]
		[Tooltip("Start point of the transition between midtones and highlights.")]
		public MinFloatParameter highlightsStart = new MinFloatParameter(0.55f, 0f, false);

		// Token: 0x040003C4 RID: 964
		[Tooltip("End point of the transition between midtones and highlights.")]
		public MinFloatParameter highlightsEnd = new MinFloatParameter(1f, 0f, false);
	}
}
