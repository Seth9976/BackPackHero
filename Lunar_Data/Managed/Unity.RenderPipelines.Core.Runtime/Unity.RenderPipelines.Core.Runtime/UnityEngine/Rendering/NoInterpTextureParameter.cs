using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000DC RID: 220
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpTextureParameter : VolumeParameter<Texture>
	{
		// Token: 0x060006A5 RID: 1701 RVA: 0x0001E2A6 File Offset: 0x0001C4A6
		public NoInterpTextureParameter(Texture value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0001E2B0 File Offset: 0x0001C4B0
		public override int GetHashCode()
		{
			int num = base.GetHashCode();
			if (this.value != null)
			{
				num = 23 * CoreUtils.GetTextureHash(this.value);
			}
			return num;
		}
	}
}
