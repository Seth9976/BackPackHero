using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000D7 RID: 215
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class Vector3Parameter : VolumeParameter<Vector3>
	{
		// Token: 0x0600069C RID: 1692 RVA: 0x0001E12D File Offset: 0x0001C32D
		public Vector3Parameter(Vector3 value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0001E138 File Offset: 0x0001C338
		public override void Interp(Vector3 from, Vector3 to, float t)
		{
			this.m_Value.x = from.x + (to.x - from.x) * t;
			this.m_Value.y = from.y + (to.y - from.y) * t;
			this.m_Value.z = from.z + (to.z - from.z) * t;
		}
	}
}
