using System;
using System.ComponentModel;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000A2 RID: 162
	public enum RenderPassEvent
	{
		// Token: 0x040003DE RID: 990
		BeforeRendering,
		// Token: 0x040003DF RID: 991
		BeforeRenderingShadows = 50,
		// Token: 0x040003E0 RID: 992
		AfterRenderingShadows = 100,
		// Token: 0x040003E1 RID: 993
		BeforeRenderingPrePasses = 150,
		// Token: 0x040003E2 RID: 994
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Obsolete, to match the capital from 'Prepass' to 'PrePass' (UnityUpgradable) -> BeforeRenderingPrePasses")]
		BeforeRenderingPrepasses,
		// Token: 0x040003E3 RID: 995
		AfterRenderingPrePasses = 200,
		// Token: 0x040003E4 RID: 996
		BeforeRenderingGbuffer = 210,
		// Token: 0x040003E5 RID: 997
		AfterRenderingGbuffer = 220,
		// Token: 0x040003E6 RID: 998
		BeforeRenderingDeferredLights = 230,
		// Token: 0x040003E7 RID: 999
		AfterRenderingDeferredLights = 240,
		// Token: 0x040003E8 RID: 1000
		BeforeRenderingOpaques = 250,
		// Token: 0x040003E9 RID: 1001
		AfterRenderingOpaques = 300,
		// Token: 0x040003EA RID: 1002
		BeforeRenderingSkybox = 350,
		// Token: 0x040003EB RID: 1003
		AfterRenderingSkybox = 400,
		// Token: 0x040003EC RID: 1004
		BeforeRenderingTransparents = 450,
		// Token: 0x040003ED RID: 1005
		AfterRenderingTransparents = 500,
		// Token: 0x040003EE RID: 1006
		BeforeRenderingPostProcessing = 550,
		// Token: 0x040003EF RID: 1007
		AfterRenderingPostProcessing = 600,
		// Token: 0x040003F0 RID: 1008
		AfterRendering = 1000
	}
}
