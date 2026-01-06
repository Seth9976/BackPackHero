using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000A4 RID: 164
	public class XROcclusionMeshPass : ScriptableRenderPass
	{
		// Token: 0x0600051E RID: 1310 RVA: 0x0001DBFD File Offset: 0x0001BDFD
		public XROcclusionMeshPass(RenderPassEvent evt)
		{
			base.profilingSampler = new ProfilingSampler("XROcclusionMeshPass");
			base.renderPassEvent = evt;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0001DC1C File Offset: 0x0001BE1C
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			if (!renderingData.cameraData.xr.enabled)
			{
				return;
			}
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			renderingData.cameraData.xr.RenderOcclusionMesh(commandBuffer);
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}
	}
}
