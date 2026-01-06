using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000C3 RID: 195
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class MinIntParameter : IntParameter
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x0001DC18 File Offset: 0x0001BE18
		// (set) Token: 0x06000667 RID: 1639 RVA: 0x0001DC20 File Offset: 0x0001BE20
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

		// Token: 0x06000668 RID: 1640 RVA: 0x0001DC34 File Offset: 0x0001BE34
		public MinIntParameter(int value, int min, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
		}

		// Token: 0x040003A4 RID: 932
		[NonSerialized]
		public int min;
	}
}
