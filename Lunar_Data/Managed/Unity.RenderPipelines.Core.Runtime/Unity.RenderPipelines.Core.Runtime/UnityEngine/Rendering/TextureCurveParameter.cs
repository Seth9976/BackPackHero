using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000B2 RID: 178
	[Serializable]
	public class TextureCurveParameter : VolumeParameter<TextureCurve>
	{
		// Token: 0x06000602 RID: 1538 RVA: 0x0001C759 File Offset: 0x0001A959
		public TextureCurveParameter(TextureCurve value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001C763 File Offset: 0x0001A963
		public override void Release()
		{
			this.m_Value.Release();
		}
	}
}
