using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000085 RID: 133
	[VolumeComponentMenuForRenderPipeline("Post-processing/Color Adjustments", new Type[] { typeof(UniversalRenderPipeline) })]
	[Serializable]
	public sealed class ColorAdjustments : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x060004E0 RID: 1248 RVA: 0x0001CCE0 File Offset: 0x0001AEE0
		public bool IsActive()
		{
			return this.postExposure.value != 0f || this.contrast.value != 0f || this.colorFilter != Color.white || this.hueShift != 0f || this.saturation != 0f;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001CD47 File Offset: 0x0001AF47
		public bool IsTileCompatible()
		{
			return true;
		}

		// Token: 0x0400037B RID: 891
		[Tooltip("Adjusts the overall exposure of the scene in EV100. This is applied after HDR effect and right before tonemapping so it won't affect previous effects in the chain.")]
		public FloatParameter postExposure = new FloatParameter(0f, false);

		// Token: 0x0400037C RID: 892
		[Tooltip("Expands or shrinks the overall range of tonal values.")]
		public ClampedFloatParameter contrast = new ClampedFloatParameter(0f, -100f, 100f, false);

		// Token: 0x0400037D RID: 893
		[Tooltip("Tint the render by multiplying a color.")]
		public ColorParameter colorFilter = new ColorParameter(Color.white, true, false, true, false);

		// Token: 0x0400037E RID: 894
		[Tooltip("Shift the hue of all colors.")]
		public ClampedFloatParameter hueShift = new ClampedFloatParameter(0f, -180f, 180f, false);

		// Token: 0x0400037F RID: 895
		[Tooltip("Pushes the intensity of all colors.")]
		public ClampedFloatParameter saturation = new ClampedFloatParameter(0f, -100f, 100f, false);
	}
}
