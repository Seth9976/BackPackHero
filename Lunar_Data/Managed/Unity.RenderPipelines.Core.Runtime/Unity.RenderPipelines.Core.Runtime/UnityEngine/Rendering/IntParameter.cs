using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000C1 RID: 193
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class IntParameter : VolumeParameter<int>
	{
		// Token: 0x06000663 RID: 1635 RVA: 0x0001DBF2 File Offset: 0x0001BDF2
		public IntParameter(int value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0001DBFC File Offset: 0x0001BDFC
		public sealed override void Interp(int from, int to, float t)
		{
			this.m_Value = (int)((float)from + (float)(to - from) * t);
		}
	}
}
