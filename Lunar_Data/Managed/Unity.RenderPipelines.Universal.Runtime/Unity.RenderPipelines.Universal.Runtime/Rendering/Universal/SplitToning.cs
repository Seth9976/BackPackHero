using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000097 RID: 151
	[VolumeComponentMenuForRenderPipeline("Post-processing/Split Toning", new Type[] { typeof(UniversalRenderPipeline) })]
	[Serializable]
	public sealed class SplitToning : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x06000503 RID: 1283 RVA: 0x0001D756 File Offset: 0x0001B956
		public bool IsActive()
		{
			return this.shadows != Color.grey || this.highlights != Color.grey;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0001D77C File Offset: 0x0001B97C
		public bool IsTileCompatible()
		{
			return true;
		}

		// Token: 0x040003C5 RID: 965
		[Tooltip("The color to use for shadows.")]
		public ColorParameter shadows = new ColorParameter(Color.grey, false, false, true, false);

		// Token: 0x040003C6 RID: 966
		[Tooltip("The color to use for highlights.")]
		public ColorParameter highlights = new ColorParameter(Color.grey, false, false, true, false);

		// Token: 0x040003C7 RID: 967
		[Tooltip("Balance between the colors in the highlights and shadows.")]
		public ClampedFloatParameter balance = new ClampedFloatParameter(0f, -100f, 100f, false);
	}
}
