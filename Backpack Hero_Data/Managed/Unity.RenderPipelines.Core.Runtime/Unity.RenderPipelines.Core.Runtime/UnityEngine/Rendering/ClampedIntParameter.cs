using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000C7 RID: 199
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class ClampedIntParameter : IntParameter
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x0001DCCC File Offset: 0x0001BECC
		// (set) Token: 0x06000673 RID: 1651 RVA: 0x0001DCD4 File Offset: 0x0001BED4
		public override int value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = Mathf.Clamp(value, this.min, this.max);
			}
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0001DCEE File Offset: 0x0001BEEE
		public ClampedIntParameter(int value, int min, int max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x040003A8 RID: 936
		[NonSerialized]
		public int min;

		// Token: 0x040003A9 RID: 937
		[NonSerialized]
		public int max;
	}
}
