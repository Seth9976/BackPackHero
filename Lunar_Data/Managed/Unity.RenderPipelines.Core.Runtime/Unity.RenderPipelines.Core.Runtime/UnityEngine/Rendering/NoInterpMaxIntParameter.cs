using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000C6 RID: 198
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpMaxIntParameter : VolumeParameter<int>
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x0001DC9F File Offset: 0x0001BE9F
		// (set) Token: 0x06000670 RID: 1648 RVA: 0x0001DCA7 File Offset: 0x0001BEA7
		public override int value
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

		// Token: 0x06000671 RID: 1649 RVA: 0x0001DCBB File Offset: 0x0001BEBB
		public NoInterpMaxIntParameter(int value, int max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.max = max;
		}

		// Token: 0x040003A7 RID: 935
		[NonSerialized]
		public int max;
	}
}
