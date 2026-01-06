using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000092 RID: 146
	[VolumeComponentMenuForRenderPipeline("Post-processing/Motion Blur", new Type[] { typeof(UniversalRenderPipeline) })]
	[Serializable]
	public sealed class MotionBlur : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x060004F8 RID: 1272 RVA: 0x0001D52E File Offset: 0x0001B72E
		public bool IsActive()
		{
			return this.intensity.value > 0f && this.mode.value == MotionBlurMode.CameraOnly;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0001D552 File Offset: 0x0001B752
		public bool IsTileCompatible()
		{
			return false;
		}

		// Token: 0x040003B8 RID: 952
		[Tooltip("The motion blur technique to use. If you don't need object motion blur, CameraOnly will result in better performance.")]
		public MotionBlurModeParameter mode = new MotionBlurModeParameter(MotionBlurMode.CameraOnly, false);

		// Token: 0x040003B9 RID: 953
		[Tooltip("The quality of the effect. Lower presets will result in better performance at the expense of visual quality.")]
		public MotionBlurQualityParameter quality = new MotionBlurQualityParameter(MotionBlurQuality.Low, false);

		// Token: 0x040003BA RID: 954
		[Tooltip("The strength of the motion blur filter. Acts as a multiplier for velocities.")]
		public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 1f, false);

		// Token: 0x040003BB RID: 955
		[Tooltip("Sets the maximum length, as a fraction of the screen's full resolution, that the velocity resulting from Camera rotation can have. Lower values will improve performance.")]
		public ClampedFloatParameter clamp = new ClampedFloatParameter(0.05f, 0f, 0.2f, false);
	}
}
