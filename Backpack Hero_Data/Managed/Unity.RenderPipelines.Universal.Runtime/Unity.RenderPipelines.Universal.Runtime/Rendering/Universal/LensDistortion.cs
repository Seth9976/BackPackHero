using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200008E RID: 142
	[VolumeComponentMenuForRenderPipeline("Post-processing/Lens Distortion", new Type[] { typeof(UniversalRenderPipeline) })]
	[Serializable]
	public sealed class LensDistortion : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x060004F2 RID: 1266 RVA: 0x0001D368 File Offset: 0x0001B568
		public bool IsActive()
		{
			return Mathf.Abs(this.intensity.value) > 0f && (this.xMultiplier.value > 0f || this.yMultiplier.value > 0f);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001D3B4 File Offset: 0x0001B5B4
		public bool IsTileCompatible()
		{
			return false;
		}

		// Token: 0x040003A9 RID: 937
		[Tooltip("Total distortion amount.")]
		public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, -1f, 1f, false);

		// Token: 0x040003AA RID: 938
		[Tooltip("Intensity multiplier on X axis. Set it to 0 to disable distortion on this axis.")]
		public ClampedFloatParameter xMultiplier = new ClampedFloatParameter(1f, 0f, 1f, false);

		// Token: 0x040003AB RID: 939
		[Tooltip("Intensity multiplier on Y axis. Set it to 0 to disable distortion on this axis.")]
		public ClampedFloatParameter yMultiplier = new ClampedFloatParameter(1f, 0f, 1f, false);

		// Token: 0x040003AC RID: 940
		[Tooltip("Distortion center point. 0.5,0.5 is center of the screen")]
		public Vector2Parameter center = new Vector2Parameter(new Vector2(0.5f, 0.5f), false);

		// Token: 0x040003AD RID: 941
		[Tooltip("Controls global screen scaling for the distortion effect. Use this to hide screen borders when using high \"Intensity.\"")]
		public ClampedFloatParameter scale = new ClampedFloatParameter(1f, 0.01f, 5f, false);
	}
}
