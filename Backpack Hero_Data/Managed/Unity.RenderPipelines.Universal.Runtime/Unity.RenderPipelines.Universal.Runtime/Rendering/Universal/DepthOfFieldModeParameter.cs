using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200008A RID: 138
	[Serializable]
	public sealed class DepthOfFieldModeParameter : VolumeParameter<DepthOfFieldMode>
	{
		// Token: 0x060004ED RID: 1261 RVA: 0x0001D2B2 File Offset: 0x0001B4B2
		public DepthOfFieldModeParameter(DepthOfFieldMode value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
