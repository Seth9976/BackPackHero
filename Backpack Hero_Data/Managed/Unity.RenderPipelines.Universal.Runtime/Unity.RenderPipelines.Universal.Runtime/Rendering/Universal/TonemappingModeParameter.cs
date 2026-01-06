using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200009A RID: 154
	[Serializable]
	public sealed class TonemappingModeParameter : VolumeParameter<TonemappingMode>
	{
		// Token: 0x06000509 RID: 1289 RVA: 0x0001D7FE File Offset: 0x0001B9FE
		public TonemappingModeParameter(TonemappingMode value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
