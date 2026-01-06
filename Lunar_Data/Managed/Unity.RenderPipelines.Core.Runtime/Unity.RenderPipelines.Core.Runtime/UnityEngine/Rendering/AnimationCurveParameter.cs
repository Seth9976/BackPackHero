using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000E4 RID: 228
	[Serializable]
	public class AnimationCurveParameter : VolumeParameter<AnimationCurve>
	{
		// Token: 0x060006BC RID: 1724 RVA: 0x0001E5D4 File Offset: 0x0001C7D4
		public AnimationCurveParameter(AnimationCurve value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
