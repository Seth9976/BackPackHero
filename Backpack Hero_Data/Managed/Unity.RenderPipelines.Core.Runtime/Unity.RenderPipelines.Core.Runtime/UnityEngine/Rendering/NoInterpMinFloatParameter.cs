using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000CC RID: 204
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpMinFloatParameter : VolumeParameter<float>
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x0001DD92 File Offset: 0x0001BF92
		// (set) Token: 0x0600067F RID: 1663 RVA: 0x0001DD9A File Offset: 0x0001BF9A
		public override float value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = Mathf.Max(value, this.min);
			}
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001DDAE File Offset: 0x0001BFAE
		public NoInterpMinFloatParameter(float value, float min, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
		}

		// Token: 0x040003AD RID: 941
		[NonSerialized]
		public float min;
	}
}
