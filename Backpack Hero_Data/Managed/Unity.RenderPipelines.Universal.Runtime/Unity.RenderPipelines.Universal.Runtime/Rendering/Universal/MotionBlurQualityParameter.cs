using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000094 RID: 148
	[Serializable]
	public sealed class MotionBlurQualityParameter : VolumeParameter<MotionBlurQuality>
	{
		// Token: 0x060004FC RID: 1276 RVA: 0x0001D5C5 File Offset: 0x0001B7C5
		public MotionBlurQualityParameter(MotionBlurQuality value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
