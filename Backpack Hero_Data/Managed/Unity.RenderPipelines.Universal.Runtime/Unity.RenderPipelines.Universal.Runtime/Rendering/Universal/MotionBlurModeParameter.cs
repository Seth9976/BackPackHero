using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000093 RID: 147
	[Serializable]
	public sealed class MotionBlurModeParameter : VolumeParameter<MotionBlurMode>
	{
		// Token: 0x060004FB RID: 1275 RVA: 0x0001D5BB File Offset: 0x0001B7BB
		public MotionBlurModeParameter(MotionBlurMode value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
