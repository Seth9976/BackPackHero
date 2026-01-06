using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000D5 RID: 213
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class Vector2Parameter : VolumeParameter<Vector2>
	{
		// Token: 0x06000699 RID: 1689 RVA: 0x0001E0C8 File Offset: 0x0001C2C8
		public Vector2Parameter(Vector2 value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001E0D4 File Offset: 0x0001C2D4
		public override void Interp(Vector2 from, Vector2 to, float t)
		{
			this.m_Value.x = from.x + (to.x - from.x) * t;
			this.m_Value.y = from.y + (to.y - from.y) * t;
		}
	}
}
