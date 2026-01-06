using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000CB RID: 203
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class MinFloatParameter : FloatParameter
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x0001DD65 File Offset: 0x0001BF65
		// (set) Token: 0x0600067C RID: 1660 RVA: 0x0001DD6D File Offset: 0x0001BF6D
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

		// Token: 0x0600067D RID: 1661 RVA: 0x0001DD81 File Offset: 0x0001BF81
		public MinFloatParameter(float value, float min, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
		}

		// Token: 0x040003AC RID: 940
		[NonSerialized]
		public float min;
	}
}
