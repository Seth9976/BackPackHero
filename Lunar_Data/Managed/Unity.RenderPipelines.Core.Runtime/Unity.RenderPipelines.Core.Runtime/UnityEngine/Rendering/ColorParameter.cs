using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000D3 RID: 211
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class ColorParameter : VolumeParameter<Color>
	{
		// Token: 0x06000694 RID: 1684 RVA: 0x0001DF96 File Offset: 0x0001C196
		public ColorParameter(Color value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0001DFAE File Offset: 0x0001C1AE
		public ColorParameter(Color value, bool hdr, bool showAlpha, bool showEyeDropper, bool overrideState = false)
			: base(value, overrideState)
		{
			this.hdr = hdr;
			this.showAlpha = showAlpha;
			this.showEyeDropper = showEyeDropper;
			this.overrideState = overrideState;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001DFE8 File Offset: 0x0001C1E8
		public override void Interp(Color from, Color to, float t)
		{
			this.m_Value.r = from.r + (to.r - from.r) * t;
			this.m_Value.g = from.g + (to.g - from.g) * t;
			this.m_Value.b = from.b + (to.b - from.b) * t;
			this.m_Value.a = from.a + (to.a - from.a) * t;
		}

		// Token: 0x040003B8 RID: 952
		[NonSerialized]
		public bool hdr;

		// Token: 0x040003B9 RID: 953
		[NonSerialized]
		public bool showAlpha = true;

		// Token: 0x040003BA RID: 954
		[NonSerialized]
		public bool showEyeDropper = true;
	}
}
