using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000C0 RID: 192
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class LayerMaskParameter : VolumeParameter<LayerMask>
	{
		// Token: 0x06000662 RID: 1634 RVA: 0x0001DBE8 File Offset: 0x0001BDE8
		public LayerMaskParameter(LayerMask value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
