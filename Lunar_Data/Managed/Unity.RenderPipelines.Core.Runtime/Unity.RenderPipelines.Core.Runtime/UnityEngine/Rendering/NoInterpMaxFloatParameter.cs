using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000CE RID: 206
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpMaxFloatParameter : VolumeParameter<float>
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x0001DDEC File Offset: 0x0001BFEC
		// (set) Token: 0x06000685 RID: 1669 RVA: 0x0001DDF4 File Offset: 0x0001BFF4
		public override float value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = Mathf.Min(value, this.max);
			}
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001DE08 File Offset: 0x0001C008
		public NoInterpMaxFloatParameter(float value, float max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.max = max;
		}

		// Token: 0x040003AF RID: 943
		[NonSerialized]
		public float max;
	}
}
