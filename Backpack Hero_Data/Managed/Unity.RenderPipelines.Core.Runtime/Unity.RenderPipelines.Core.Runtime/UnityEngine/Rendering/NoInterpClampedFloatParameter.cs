using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000D0 RID: 208
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpClampedFloatParameter : VolumeParameter<float>
	{
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x0001DE54 File Offset: 0x0001C054
		// (set) Token: 0x0600068B RID: 1675 RVA: 0x0001DE5C File Offset: 0x0001C05C
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

		// Token: 0x0600068C RID: 1676 RVA: 0x0001DE76 File Offset: 0x0001C076
		public NoInterpClampedFloatParameter(float value, float min, float max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x040003B2 RID: 946
		[NonSerialized]
		public float min;

		// Token: 0x040003B3 RID: 947
		[NonSerialized]
		public float max;
	}
}
