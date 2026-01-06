using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200008C RID: 140
	[VolumeComponentMenuForRenderPipeline("Post-processing/Film Grain", new Type[] { typeof(UniversalRenderPipeline) })]
	[Serializable]
	public sealed class FilmGrain : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x060004EE RID: 1262 RVA: 0x0001D2BC File Offset: 0x0001B4BC
		public bool IsActive()
		{
			return this.intensity.value > 0f && (this.type.value != FilmGrainLookup.Custom || this.texture.value != null);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001D2F4 File Offset: 0x0001B4F4
		public bool IsTileCompatible()
		{
			return true;
		}

		// Token: 0x040003A5 RID: 933
		[Tooltip("The type of grain to use. You can select a preset or provide your own texture by selecting Custom.")]
		public FilmGrainLookupParameter type = new FilmGrainLookupParameter(FilmGrainLookup.Thin1, false);

		// Token: 0x040003A6 RID: 934
		[Tooltip("Use the slider to set the strength of the Film Grain effect.")]
		public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 1f, false);

		// Token: 0x040003A7 RID: 935
		[Tooltip("Controls the noisiness response curve based on scene luminance. Higher values mean less noise in light areas.")]
		public ClampedFloatParameter response = new ClampedFloatParameter(0.8f, 0f, 1f, false);

		// Token: 0x040003A8 RID: 936
		[Tooltip("A tileable texture to use for the grain. The neutral value is 0.5 where no grain is applied.")]
		public NoInterpTextureParameter texture = new NoInterpTextureParameter(null, false);
	}
}
