using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000C9 RID: 201
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class FloatParameter : VolumeParameter<float>
	{
		// Token: 0x06000678 RID: 1656 RVA: 0x0001DD42 File Offset: 0x0001BF42
		public FloatParameter(float value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001DD4C File Offset: 0x0001BF4C
		public sealed override void Interp(float from, float to, float t)
		{
			this.m_Value = from + (to - from) * t;
		}
	}
}
