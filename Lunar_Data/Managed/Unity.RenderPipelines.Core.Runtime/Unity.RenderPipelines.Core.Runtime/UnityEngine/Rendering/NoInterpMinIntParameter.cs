using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000C4 RID: 196
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpMinIntParameter : VolumeParameter<int>
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x0001DC45 File Offset: 0x0001BE45
		// (set) Token: 0x0600066A RID: 1642 RVA: 0x0001DC4D File Offset: 0x0001BE4D
		public override int value
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

		// Token: 0x0600066B RID: 1643 RVA: 0x0001DC61 File Offset: 0x0001BE61
		public NoInterpMinIntParameter(int value, int min, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
		}

		// Token: 0x040003A5 RID: 933
		[NonSerialized]
		public int min;
	}
}
