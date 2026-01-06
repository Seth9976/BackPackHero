using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200009D RID: 157
	internal class CapturePass : ScriptableRenderPass
	{
		// Token: 0x06000510 RID: 1296 RVA: 0x0001D911 File Offset: 0x0001BB11
		public CapturePass(RenderPassEvent evt)
		{
			base.profilingSampler = new ProfilingSampler("CapturePass");
			base.renderPassEvent = evt;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0001D930 File Offset: 0x0001BB30
		public void Setup(RenderTargetHandle colorHandle)
		{
			this.m_CameraColorHandle = colorHandle;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0001D93C File Offset: 0x0001BB3C
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			using (new ProfilingScope(commandBuffer, CapturePass.m_ProfilingSampler))
			{
				RenderTargetIdentifier renderTargetIdentifier = this.m_CameraColorHandle.Identifier();
				IEnumerator<Action<RenderTargetIdentifier, CommandBuffer>> captureActions = renderingData.cameraData.captureActions;
				captureActions.Reset();
				while (captureActions.MoveNext())
				{
					Action<RenderTargetIdentifier, CommandBuffer> action = captureActions.Current;
					action(renderTargetIdentifier, commandBuffer);
				}
			}
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x040003D4 RID: 980
		private RenderTargetHandle m_CameraColorHandle;

		// Token: 0x040003D5 RID: 981
		private const string m_ProfilerTag = "Capture Pass";

		// Token: 0x040003D6 RID: 982
		private static readonly ProfilingSampler m_ProfilingSampler = new ProfilingSampler("Capture Pass");
	}
}
