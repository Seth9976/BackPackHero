using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003A6 RID: 934
	public enum RenderQueue
	{
		// Token: 0x04000A70 RID: 2672
		Background = 1000,
		// Token: 0x04000A71 RID: 2673
		Geometry = 2000,
		// Token: 0x04000A72 RID: 2674
		AlphaTest = 2450,
		// Token: 0x04000A73 RID: 2675
		GeometryLast = 2500,
		// Token: 0x04000A74 RID: 2676
		Transparent = 3000,
		// Token: 0x04000A75 RID: 2677
		Overlay = 4000
	}
}
