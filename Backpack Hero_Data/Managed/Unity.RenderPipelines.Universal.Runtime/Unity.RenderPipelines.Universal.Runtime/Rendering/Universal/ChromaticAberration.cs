using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000084 RID: 132
	[VolumeComponentMenuForRenderPipeline("Post-processing/Chromatic Aberration", new Type[] { typeof(UniversalRenderPipeline) })]
	[Serializable]
	public sealed class ChromaticAberration : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x060004DD RID: 1245 RVA: 0x0001CCA6 File Offset: 0x0001AEA6
		public bool IsActive()
		{
			return this.intensity.value > 0f;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0001CCBA File Offset: 0x0001AEBA
		public bool IsTileCompatible()
		{
			return false;
		}

		// Token: 0x0400037A RID: 890
		[Tooltip("Use the slider to set the strength of the Chromatic Aberration effect.")]
		public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 1f, false);
	}
}
