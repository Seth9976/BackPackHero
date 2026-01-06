using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000D4 RID: 212
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpColorParameter : VolumeParameter<Color>
	{
		// Token: 0x06000697 RID: 1687 RVA: 0x0001E079 File Offset: 0x0001C279
		public NoInterpColorParameter(Color value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001E091 File Offset: 0x0001C291
		public NoInterpColorParameter(Color value, bool hdr, bool showAlpha, bool showEyeDropper, bool overrideState = false)
			: base(value, overrideState)
		{
			this.hdr = hdr;
			this.showAlpha = showAlpha;
			this.showEyeDropper = showEyeDropper;
			this.overrideState = overrideState;
		}

		// Token: 0x040003BB RID: 955
		public bool hdr;

		// Token: 0x040003BC RID: 956
		[NonSerialized]
		public bool showAlpha = true;

		// Token: 0x040003BD RID: 957
		[NonSerialized]
		public bool showEyeDropper = true;
	}
}
