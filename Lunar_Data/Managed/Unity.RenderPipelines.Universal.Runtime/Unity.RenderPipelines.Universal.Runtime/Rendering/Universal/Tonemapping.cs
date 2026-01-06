using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000099 RID: 153
	[VolumeComponentMenuForRenderPipeline("Post-processing/Tonemapping", new Type[] { typeof(UniversalRenderPipeline) })]
	[Serializable]
	public sealed class Tonemapping : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x06000506 RID: 1286 RVA: 0x0001D7D6 File Offset: 0x0001B9D6
		public bool IsActive()
		{
			return this.mode.value > TonemappingMode.None;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0001D7E6 File Offset: 0x0001B9E6
		public bool IsTileCompatible()
		{
			return true;
		}

		// Token: 0x040003CC RID: 972
		[Tooltip("Select a tonemapping algorithm to use for the color grading process.")]
		public TonemappingModeParameter mode = new TonemappingModeParameter(TonemappingMode.None, false);
	}
}
