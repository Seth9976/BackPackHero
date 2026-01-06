using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000DF RID: 223
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class RenderTextureParameter : VolumeParameter<RenderTexture>
	{
		// Token: 0x060006AB RID: 1707 RVA: 0x0001E35A File Offset: 0x0001C55A
		public RenderTextureParameter(RenderTexture value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001E364 File Offset: 0x0001C564
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
