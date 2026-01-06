using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000046 RID: 70
	public enum RendererType
	{
		// Token: 0x040001C5 RID: 453
		Custom,
		// Token: 0x040001C6 RID: 454
		UniversalRenderer,
		// Token: 0x040001C7 RID: 455
		_2DRenderer,
		// Token: 0x040001C8 RID: 456
		[Obsolete("ForwardRenderer has been renamed (UnityUpgradable) -> UniversalRenderer", true)]
		ForwardRenderer = 1
	}
}
