using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000CD RID: 205
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class MaxFloatParameter : FloatParameter
	{
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x0001DDBF File Offset: 0x0001BFBF
		// (set) Token: 0x06000682 RID: 1666 RVA: 0x0001DDC7 File Offset: 0x0001BFC7
		public override float value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = Mathf.Min(value, this.max);
			}
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001DDDB File Offset: 0x0001BFDB
		public MaxFloatParameter(float value, float max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.max = max;
		}

		// Token: 0x040003AE RID: 942
		[NonSerialized]
		public float max;
	}
}
