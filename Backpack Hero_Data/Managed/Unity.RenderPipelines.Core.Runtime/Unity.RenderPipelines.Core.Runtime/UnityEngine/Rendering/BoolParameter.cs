using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000BF RID: 191
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class BoolParameter : VolumeParameter<bool>
	{
		// Token: 0x06000661 RID: 1633 RVA: 0x0001DBDE File Offset: 0x0001BDDE
		public BoolParameter(bool value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
