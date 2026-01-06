using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000029 RID: 41
	internal class PixelPerfectBackgroundPass : ScriptableRenderPass
	{
		// Token: 0x0600017E RID: 382 RVA: 0x0000D7C1 File Offset: 0x0000B9C1
		public PixelPerfectBackgroundPass(RenderPassEvent evt)
		{
			base.renderPassEvent = evt;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000D7D0 File Offset: 0x0000B9D0
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			using (new ProfilingScope(commandBuffer, PixelPerfectBackgroundPass.m_ProfilingScope))
			{
				CoreUtils.SetRenderTarget(commandBuffer, BuiltinRenderTextureType.CameraTarget, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, ClearFlag.Color, Color.black);
			}
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x040000DB RID: 219
		private static readonly ProfilingSampler m_ProfilingScope = new ProfilingSampler("Pixel Perfect Background Pass");
	}
}
