using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000C8 RID: 200
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpClampedIntParameter : VolumeParameter<int>
	{
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x0001DD07 File Offset: 0x0001BF07
		// (set) Token: 0x06000676 RID: 1654 RVA: 0x0001DD0F File Offset: 0x0001BF0F
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

		// Token: 0x06000677 RID: 1655 RVA: 0x0001DD29 File Offset: 0x0001BF29
		public NoInterpClampedIntParameter(int value, int min, int max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x040003AA RID: 938
		[NonSerialized]
		public int min;

		// Token: 0x040003AB RID: 939
		[NonSerialized]
		public int max;
	}
}
