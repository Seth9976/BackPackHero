using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200009C RID: 156
	[VolumeComponentMenuForRenderPipeline("Post-processing/White Balance", new Type[] { typeof(UniversalRenderPipeline) })]
	[Serializable]
	public sealed class WhiteBalance : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x0600050D RID: 1293 RVA: 0x0001D8A5 File Offset: 0x0001BAA5
		public bool IsActive()
		{
			return this.temperature.value != 0f || this.tint.value != 0f;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0001D8D0 File Offset: 0x0001BAD0
		public bool IsTileCompatible()
		{
			return true;
		}

		// Token: 0x040003D2 RID: 978
		[Tooltip("Sets the white balance to a custom color temperature.")]
		public ClampedFloatParameter temperature = new ClampedFloatParameter(0f, -100f, 100f, false);

		// Token: 0x040003D3 RID: 979
		[Tooltip("Sets the white balance to compensate for a green or magenta tint.")]
		public ClampedFloatParameter tint = new ClampedFloatParameter(0f, -100f, 100f, false);
	}
}
