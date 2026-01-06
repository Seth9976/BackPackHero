using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000095 RID: 149
	[VolumeComponentMenuForRenderPipeline("Post-processing/Panini Projection", new Type[] { typeof(UniversalRenderPipeline) })]
	[Serializable]
	public sealed class PaniniProjection : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x060004FD RID: 1277 RVA: 0x0001D5CF File Offset: 0x0001B7CF
		public bool IsActive()
		{
			return this.distance.value > 0f;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001D5E3 File Offset: 0x0001B7E3
		public bool IsTileCompatible()
		{
			return false;
		}

		// Token: 0x040003BC RID: 956
		[Tooltip("Panini projection distance.")]
		public ClampedFloatParameter distance = new ClampedFloatParameter(0f, 0f, 1f, false);

		// Token: 0x040003BD RID: 957
		[Tooltip("Panini projection crop to fit.")]
		public ClampedFloatParameter cropToFit = new ClampedFloatParameter(1f, 0f, 1f, false);
	}
}
