using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000A3 RID: 163
	internal class TransparentSettingsPass : ScriptableRenderPass
	{
		// Token: 0x0600051A RID: 1306 RVA: 0x0001DB37 File Offset: 0x0001BD37
		public TransparentSettingsPass(RenderPassEvent evt, bool shadowReceiveSupported)
		{
			base.profilingSampler = new ProfilingSampler("TransparentSettingsPass");
			base.renderPassEvent = evt;
			this.m_shouldReceiveShadows = shadowReceiveSupported;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0001DB5D File Offset: 0x0001BD5D
		public bool Setup(ref RenderingData renderingData)
		{
			return !this.m_shouldReceiveShadows;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001DB68 File Offset: 0x0001BD68
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			using (new ProfilingScope(commandBuffer, TransparentSettingsPass.m_ProfilingSampler))
			{
				CoreUtils.SetKeyword(commandBuffer, "_MAIN_LIGHT_SHADOWS", this.m_shouldReceiveShadows);
				CoreUtils.SetKeyword(commandBuffer, "_MAIN_LIGHT_SHADOWS_CASCADE", this.m_shouldReceiveShadows);
				CoreUtils.SetKeyword(commandBuffer, "_ADDITIONAL_LIGHT_SHADOWS", this.m_shouldReceiveShadows);
			}
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x040003F1 RID: 1009
		private bool m_shouldReceiveShadows;

		// Token: 0x040003F2 RID: 1010
		private const string m_ProfilerTag = "Transparent Settings Pass";

		// Token: 0x040003F3 RID: 1011
		private static readonly ProfilingSampler m_ProfilingSampler = new ProfilingSampler("Transparent Settings Pass");
	}
}
