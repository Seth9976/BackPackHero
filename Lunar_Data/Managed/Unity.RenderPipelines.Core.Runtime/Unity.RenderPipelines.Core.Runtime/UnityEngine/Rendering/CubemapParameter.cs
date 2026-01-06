using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000E1 RID: 225
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class CubemapParameter : VolumeParameter<Texture>
	{
		// Token: 0x060006AF RID: 1711 RVA: 0x0001E3D2 File Offset: 0x0001C5D2
		public CubemapParameter(Texture value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0001E3DC File Offset: 0x0001C5DC
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
