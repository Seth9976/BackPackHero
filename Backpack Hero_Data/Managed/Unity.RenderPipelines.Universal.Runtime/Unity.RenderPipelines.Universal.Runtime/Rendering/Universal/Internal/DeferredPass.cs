using System;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x0200010B RID: 267
	internal class DeferredPass : ScriptableRenderPass
	{
		// Token: 0x0600083A RID: 2106 RVA: 0x000341C2 File Offset: 0x000323C2
		public DeferredPass(RenderPassEvent evt, DeferredLights deferredLights)
		{
			base.profilingSampler = new ProfilingSampler("DeferredPass");
			base.renderPassEvent = evt;
			this.m_DeferredLights = deferredLights;
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x000341E8 File Offset: 0x000323E8
		public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescripor)
		{
			RenderTargetIdentifier renderTargetIdentifier = this.m_DeferredLights.GbufferAttachmentIdentifiers[this.m_DeferredLights.GBufferLightingIndex];
			RenderTargetIdentifier depthAttachmentIdentifier = this.m_DeferredLights.DepthAttachmentIdentifier;
			if (this.m_DeferredLights.UseRenderPass)
			{
				base.ConfigureInputAttachments(this.m_DeferredLights.DeferredInputAttachments, this.m_DeferredLights.DeferredInputIsTransient);
			}
			base.ConfigureTarget(renderTargetIdentifier, depthAttachmentIdentifier, cameraTextureDescripor.graphicsFormat);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00034255 File Offset: 0x00032455
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			this.m_DeferredLights.ExecuteDeferredPass(context, ref renderingData);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00034264 File Offset: 0x00032464
		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			this.m_DeferredLights.OnCameraCleanup(cmd);
		}

		// Token: 0x04000793 RID: 1939
		private DeferredLights m_DeferredLights;
	}
}
