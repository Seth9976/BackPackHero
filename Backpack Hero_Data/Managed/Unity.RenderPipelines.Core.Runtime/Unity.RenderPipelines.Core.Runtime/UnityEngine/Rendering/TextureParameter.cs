using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000DB RID: 219
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class TextureParameter : VolumeParameter<Texture>
	{
		// Token: 0x060006A2 RID: 1698 RVA: 0x0001E257 File Offset: 0x0001C457
		public TextureParameter(Texture value, bool overrideState = false)
			: this(value, TextureDimension.Any, overrideState)
		{
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001E262 File Offset: 0x0001C462
		public TextureParameter(Texture value, TextureDimension dimension, bool overrideState = false)
			: base(value, overrideState)
		{
			this.dimension = dimension;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001E274 File Offset: 0x0001C474
		public override int GetHashCode()
		{
			int num = base.GetHashCode();
			if (this.value != null)
			{
				num = 23 * CoreUtils.GetTextureHash(this.value);
			}
			return num;
		}

		// Token: 0x040003BE RID: 958
		public TextureDimension dimension;
	}
}
