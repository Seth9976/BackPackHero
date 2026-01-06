using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000082 RID: 130
	[VolumeComponentMenuForRenderPipeline("Post-processing/Bloom", new Type[] { typeof(UniversalRenderPipeline) })]
	[Serializable]
	public sealed class Bloom : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x060004D7 RID: 1239 RVA: 0x0001CA05 File Offset: 0x0001AC05
		public bool IsActive()
		{
			return this.intensity.value > 0f;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001CA19 File Offset: 0x0001AC19
		public bool IsTileCompatible()
		{
			return false;
		}

		// Token: 0x04000368 RID: 872
		[Header("Bloom")]
		[Tooltip("Filters out pixels under this level of brightness. Value is in gamma-space.")]
		public MinFloatParameter threshold = new MinFloatParameter(0.9f, 0f, false);

		// Token: 0x04000369 RID: 873
		[Tooltip("Strength of the bloom filter.")]
		public MinFloatParameter intensity = new MinFloatParameter(0f, 0f, false);

		// Token: 0x0400036A RID: 874
		[Tooltip("Set the radius of the bloom effect")]
		public ClampedFloatParameter scatter = new ClampedFloatParameter(0.7f, 0f, 1f, false);

		// Token: 0x0400036B RID: 875
		[Tooltip("Set the maximum intensity that Unity uses to calculate Bloom. If pixels in your Scene are more intense than this, URP renders them at their current intensity, but uses this intensity value for the purposes of Bloom calculations.")]
		public MinFloatParameter clamp = new MinFloatParameter(65472f, 0f, false);

		// Token: 0x0400036C RID: 876
		[Tooltip("Use the color picker to select a color for the Bloom effect to tint to.")]
		public ColorParameter tint = new ColorParameter(Color.white, false, false, true, false);

		// Token: 0x0400036D RID: 877
		[Tooltip("Use bicubic sampling instead of bilinear sampling for the upsampling passes. This is slightly more expensive but helps getting smoother visuals.")]
		public BoolParameter highQualityFiltering = new BoolParameter(false, false);

		// Token: 0x0400036E RID: 878
		[Tooltip("The number of final iterations to skip in the effect processing sequence.")]
		public ClampedIntParameter skipIterations = new ClampedIntParameter(1, 0, 16, false);

		// Token: 0x0400036F RID: 879
		[Header("Lens Dirt")]
		[Tooltip("Dirtiness texture to add smudges or dust to the bloom effect.")]
		public TextureParameter dirtTexture = new TextureParameter(null, false);

		// Token: 0x04000370 RID: 880
		[Tooltip("Amount of dirtiness.")]
		public MinFloatParameter dirtIntensity = new MinFloatParameter(0f, 0f, false);
	}
}
