using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000D9 RID: 217
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class Vector4Parameter : VolumeParameter<Vector4>
	{
		// Token: 0x0600069F RID: 1695 RVA: 0x0001E1B2 File Offset: 0x0001C3B2
		public Vector4Parameter(Vector4 value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001E1BC File Offset: 0x0001C3BC
		public override void Interp(Vector4 from, Vector4 to, float t)
		{
			this.m_Value.x = from.x + (to.x - from.x) * t;
			this.m_Value.y = from.y + (to.y - from.y) * t;
			this.m_Value.z = from.z + (to.z - from.z) * t;
			this.m_Value.w = from.w + (to.w - from.w) * t;
		}
	}
}
