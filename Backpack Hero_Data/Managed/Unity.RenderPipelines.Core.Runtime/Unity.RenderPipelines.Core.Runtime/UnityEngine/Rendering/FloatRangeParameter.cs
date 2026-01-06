using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000D1 RID: 209
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class FloatRangeParameter : VolumeParameter<Vector2>
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x0001DE8F File Offset: 0x0001C08F
		// (set) Token: 0x0600068E RID: 1678 RVA: 0x0001DE97 File Offset: 0x0001C097
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

		// Token: 0x0600068F RID: 1679 RVA: 0x0001DED1 File Offset: 0x0001C0D1
		public FloatRangeParameter(Vector2 value, float min, float max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001DEEC File Offset: 0x0001C0EC
		public override void Interp(Vector2 from, Vector2 to, float t)
		{
			this.m_Value.x = from.x + (to.x - from.x) * t;
			this.m_Value.y = from.y + (to.y - from.y) * t;
		}

		// Token: 0x040003B4 RID: 948
		[NonSerialized]
		public float min;

		// Token: 0x040003B5 RID: 949
		[NonSerialized]
		public float max;
	}
}
