using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200009B RID: 155
	[VolumeComponentMenuForRenderPipeline("Post-processing/Vignette", new Type[] { typeof(UniversalRenderPipeline) })]
	[Serializable]
	public sealed class Vignette : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x0600050A RID: 1290 RVA: 0x0001D808 File Offset: 0x0001BA08
		public bool IsActive()
		{
			return this.intensity.value > 0f;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001D81C File Offset: 0x0001BA1C
		public bool IsTileCompatible()
		{
			return true;
		}

		// Token: 0x040003CD RID: 973
		[Tooltip("Vignette color.")]
		public ColorParameter color = new ColorParameter(Color.black, false, false, true, false);

		// Token: 0x040003CE RID: 974
		[Tooltip("Sets the vignette center point (screen center is [0.5,0.5]).")]
		public Vector2Parameter center = new Vector2Parameter(new Vector2(0.5f, 0.5f), false);

		// Token: 0x040003CF RID: 975
		[Tooltip("Amount of vignetting on screen.")]
		public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 1f, false);

		// Token: 0x040003D0 RID: 976
		[Tooltip("Smoothness of the vignette borders.")]
		public ClampedFloatParameter smoothness = new ClampedFloatParameter(0.2f, 0.01f, 1f, false);

		// Token: 0x040003D1 RID: 977
		[Tooltip("Should the vignette be perfectly round or be dependent on the current aspect ratio?")]
		public BoolParameter rounded = new BoolParameter(false, false);
	}
}
