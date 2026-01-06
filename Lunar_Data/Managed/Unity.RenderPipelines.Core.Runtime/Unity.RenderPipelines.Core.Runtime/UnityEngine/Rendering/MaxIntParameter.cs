using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000C5 RID: 197
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class MaxIntParameter : IntParameter
	{
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x0001DC72 File Offset: 0x0001BE72
		// (set) Token: 0x0600066D RID: 1645 RVA: 0x0001DC7A File Offset: 0x0001BE7A
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

		// Token: 0x0600066E RID: 1646 RVA: 0x0001DC8E File Offset: 0x0001BE8E
		public MaxIntParameter(int value, int max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.max = max;
		}

		// Token: 0x040003A6 RID: 934
		[NonSerialized]
		public int max;
	}
}
