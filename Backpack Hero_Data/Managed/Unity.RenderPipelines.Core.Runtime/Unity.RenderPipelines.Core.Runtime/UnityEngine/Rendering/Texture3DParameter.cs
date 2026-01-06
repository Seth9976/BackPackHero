using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000DE RID: 222
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class Texture3DParameter : VolumeParameter<Texture>
	{
		// Token: 0x060006A9 RID: 1705 RVA: 0x0001E31E File Offset: 0x0001C51E
		public Texture3DParameter(Texture value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0001E328 File Offset: 0x0001C528
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
