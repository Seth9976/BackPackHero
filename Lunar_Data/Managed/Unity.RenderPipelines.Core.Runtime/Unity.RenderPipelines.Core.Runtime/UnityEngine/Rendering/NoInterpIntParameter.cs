using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000C2 RID: 194
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpIntParameter : VolumeParameter<int>
	{
		// Token: 0x06000665 RID: 1637 RVA: 0x0001DC0E File Offset: 0x0001BE0E
		public NoInterpIntParameter(int value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
