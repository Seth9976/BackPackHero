using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000089 RID: 137
	[VolumeComponentMenuForRenderPipeline("Post-processing/Depth Of Field", new Type[] { typeof(UniversalRenderPipeline) })]
	[Serializable]
	public sealed class DepthOfField : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x060004EA RID: 1258 RVA: 0x0001D176 File Offset: 0x0001B376
		public bool IsActive()
		{
			return this.mode.value != DepthOfFieldMode.Off && SystemInfo.graphicsShaderLevel >= 35 && (this.mode.value != DepthOfFieldMode.Gaussian || SystemInfo.supportedRenderTargetCount > 1);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0001D1A8 File Offset: 0x0001B3A8
		public bool IsTileCompatible()
		{
			return false;
		}

		// Token: 0x0400038E RID: 910
		[Tooltip("Use \"Gaussian\" for a faster but non physical depth of field; \"Bokeh\" for a more realistic but slower depth of field.")]
		public DepthOfFieldModeParameter mode = new DepthOfFieldModeParameter(DepthOfFieldMode.Off, false);

		// Token: 0x0400038F RID: 911
		[Tooltip("The distance at which the blurring will start.")]
		public MinFloatParameter gaussianStart = new MinFloatParameter(10f, 0f, false);

		// Token: 0x04000390 RID: 912
		[Tooltip("The distance at which the blurring will reach its maximum radius.")]
		public MinFloatParameter gaussianEnd = new MinFloatParameter(30f, 0f, false);

		// Token: 0x04000391 RID: 913
		[Tooltip("The maximum radius of the gaussian blur. Values above 1 may show under-sampling artifacts.")]
		public ClampedFloatParameter gaussianMaxRadius = new ClampedFloatParameter(1f, 0.5f, 1.5f, false);

		// Token: 0x04000392 RID: 914
		[Tooltip("Use higher quality sampling to reduce flickering and improve the overall blur smoothness.")]
		public BoolParameter highQualitySampling = new BoolParameter(false, false);

		// Token: 0x04000393 RID: 915
		[Tooltip("The distance to the point of focus.")]
		public MinFloatParameter focusDistance = new MinFloatParameter(10f, 0.1f, false);

		// Token: 0x04000394 RID: 916
		[Tooltip("The ratio of aperture (known as f-stop or f-number). The smaller the value is, the shallower the depth of field is.")]
		public ClampedFloatParameter aperture = new ClampedFloatParameter(5.6f, 1f, 32f, false);

		// Token: 0x04000395 RID: 917
		[Tooltip("The distance between the lens and the film. The larger the value is, the shallower the depth of field is.")]
		public ClampedFloatParameter focalLength = new ClampedFloatParameter(50f, 1f, 300f, false);

		// Token: 0x04000396 RID: 918
		[Tooltip("The number of aperture blades.")]
		public ClampedIntParameter bladeCount = new ClampedIntParameter(5, 3, 9, false);

		// Token: 0x04000397 RID: 919
		[Tooltip("The curvature of aperture blades. The smaller the value is, the more visible aperture blades are. A value of 1 will make the bokeh perfectly circular.")]
		public ClampedFloatParameter bladeCurvature = new ClampedFloatParameter(1f, 0f, 1f, false);

		// Token: 0x04000398 RID: 920
		[Tooltip("The rotation of aperture blades in degrees.")]
		public ClampedFloatParameter bladeRotation = new ClampedFloatParameter(0f, -180f, 180f, false);
	}
}
