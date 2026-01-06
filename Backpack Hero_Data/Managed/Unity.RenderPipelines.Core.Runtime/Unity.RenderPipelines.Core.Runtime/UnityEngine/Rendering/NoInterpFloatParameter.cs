using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000CA RID: 202
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpFloatParameter : VolumeParameter<float>
	{
		// Token: 0x0600067A RID: 1658 RVA: 0x0001DD5B File Offset: 0x0001BF5B
		public NoInterpFloatParameter(float value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
