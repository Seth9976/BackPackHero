using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000E2 RID: 226
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpCubemapParameter : VolumeParameter<Cubemap>
	{
		// Token: 0x060006B1 RID: 1713 RVA: 0x0001E40E File Offset: 0x0001C60E
		public NoInterpCubemapParameter(Cubemap value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0001E418 File Offset: 0x0001C618
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
