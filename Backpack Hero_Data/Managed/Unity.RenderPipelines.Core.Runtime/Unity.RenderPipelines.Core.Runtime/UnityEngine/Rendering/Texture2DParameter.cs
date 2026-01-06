using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000DD RID: 221
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class Texture2DParameter : VolumeParameter<Texture>
	{
		// Token: 0x060006A7 RID: 1703 RVA: 0x0001E2E2 File Offset: 0x0001C4E2
		public Texture2DParameter(Texture value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0001E2EC File Offset: 0x0001C4EC
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
