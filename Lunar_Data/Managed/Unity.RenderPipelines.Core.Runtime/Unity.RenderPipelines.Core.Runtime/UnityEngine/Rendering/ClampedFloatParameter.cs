using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000CF RID: 207
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class ClampedFloatParameter : FloatParameter
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x0001DE19 File Offset: 0x0001C019
		// (set) Token: 0x06000688 RID: 1672 RVA: 0x0001DE21 File Offset: 0x0001C021
		public override float value
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

		// Token: 0x06000689 RID: 1673 RVA: 0x0001DE3B File Offset: 0x0001C03B
		public ClampedFloatParameter(float value, float min, float max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x040003B0 RID: 944
		[NonSerialized]
		public float min;

		// Token: 0x040003B1 RID: 945
		[NonSerialized]
		public float max;
	}
}
