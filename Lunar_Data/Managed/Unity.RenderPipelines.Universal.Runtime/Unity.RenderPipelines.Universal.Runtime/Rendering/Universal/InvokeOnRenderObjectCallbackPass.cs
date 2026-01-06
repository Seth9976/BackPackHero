using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200009F RID: 159
	internal class InvokeOnRenderObjectCallbackPass : ScriptableRenderPass
	{
		// Token: 0x06000516 RID: 1302 RVA: 0x0001DB0F File Offset: 0x0001BD0F
		public InvokeOnRenderObjectCallbackPass(RenderPassEvent evt)
		{
			base.profilingSampler = new ProfilingSampler("InvokeOnRenderObjectCallbackPass");
			base.renderPassEvent = evt;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0001DB2E File Offset: 0x0001BD2E
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			context.InvokeOnRenderObjectCallback();
		}
	}
}
