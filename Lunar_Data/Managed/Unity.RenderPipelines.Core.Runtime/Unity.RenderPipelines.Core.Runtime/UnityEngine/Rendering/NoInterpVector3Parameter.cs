using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000D8 RID: 216
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpVector3Parameter : VolumeParameter<Vector3>
	{
		// Token: 0x0600069E RID: 1694 RVA: 0x0001E1A8 File Offset: 0x0001C3A8
		public NoInterpVector3Parameter(Vector3 value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
