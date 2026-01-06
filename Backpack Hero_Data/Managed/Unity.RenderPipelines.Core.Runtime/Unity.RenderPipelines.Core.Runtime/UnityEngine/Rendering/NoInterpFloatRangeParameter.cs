using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000D2 RID: 210
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpFloatRangeParameter : VolumeParameter<Vector2>
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x0001DF3B File Offset: 0x0001C13B
		// (set) Token: 0x06000692 RID: 1682 RVA: 0x0001DF43 File Offset: 0x0001C143
		public override Vector2 value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value.x = Mathf.Max(value.x, this.min);
				this.m_Value.y = Mathf.Min(value.y, this.max);
			}
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001DF7D File Offset: 0x0001C17D
		public NoInterpFloatRangeParameter(Vector2 value, float min, float max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x040003B6 RID: 950
		[NonSerialized]
		public float min;

		// Token: 0x040003B7 RID: 951
		[NonSerialized]
		public float max;
	}
}
