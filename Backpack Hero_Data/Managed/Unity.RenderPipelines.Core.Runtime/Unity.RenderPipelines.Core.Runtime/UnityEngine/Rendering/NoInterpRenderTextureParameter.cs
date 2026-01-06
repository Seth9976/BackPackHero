using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000E0 RID: 224
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpRenderTextureParameter : VolumeParameter<RenderTexture>
	{
		// Token: 0x060006AD RID: 1709 RVA: 0x0001E396 File Offset: 0x0001C596
		public NoInterpRenderTextureParameter(RenderTexture value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0001E3A0 File Offset: 0x0001C5A0
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
