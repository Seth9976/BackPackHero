using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000DA RID: 218
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpVector4Parameter : VolumeParameter<Vector4>
	{
		// Token: 0x060006A1 RID: 1697 RVA: 0x0001E24D File Offset: 0x0001C44D
		public NoInterpVector4Parameter(Vector4 value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
