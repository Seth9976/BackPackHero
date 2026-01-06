using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000D6 RID: 214
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpVector2Parameter : VolumeParameter<Vector2>
	{
		// Token: 0x0600069B RID: 1691 RVA: 0x0001E123 File Offset: 0x0001C323
		public NoInterpVector2Parameter(Vector2 value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
