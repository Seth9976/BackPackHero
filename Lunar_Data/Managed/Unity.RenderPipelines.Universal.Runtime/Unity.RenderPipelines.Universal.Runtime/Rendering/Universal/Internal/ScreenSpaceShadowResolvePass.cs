using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x02000114 RID: 276
	public class ScreenSpaceShadowResolvePass : ScriptableRenderPass
	{
		// Token: 0x060008AC RID: 2220 RVA: 0x00038E88 File Offset: 0x00037088
		public ScreenSpaceShadowResolvePass(RenderPassEvent evt, Material screenspaceShadowsMaterial)
		{
			base.profilingSampler = new ProfilingSampler("ScreenSpaceShadowResolvePass");
			this.m_ScreenSpaceShadowsMaterial = screenspaceShadowsMaterial;
			this.m_ScreenSpaceShadowmap.Init("_ScreenSpaceShadowmapTexture");
			base.renderPassEvent = evt;
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00038EBE File Offset: 0x000370BE
		public void Setup(RenderTextureDescriptor baseDescriptor)
		{
			this.m_RenderTextureDescriptor = baseDescriptor;
			this.m_RenderTextureDescriptor.depthBufferBits = 0;
			this.m_RenderTextureDescriptor.msaaSamples = 1;
			this.m_RenderTextureDescriptor.graphicsFormat = (RenderingUtils.SupportsGraphicsFormat(GraphicsFormat.R8_UNorm, FormatUsage.Blend) ? GraphicsFormat.R8_UNorm : GraphicsFormat.B8G8R8A8_UNorm);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00038EF8 File Offset: 0x000370F8
		public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
		{
			cmd.GetTemporaryRT(this.m_ScreenSpaceShadowmap.id, this.m_RenderTextureDescriptor, FilterMode.Bilinear);
			RenderTargetIdentifier renderTargetIdentifier = this.m_ScreenSpaceShadowmap.Identifier();
			base.ConfigureTarget(renderTargetIdentifier);
			base.ConfigureClear(ClearFlag.All, Color.white);
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00038F3C File Offset: 0x0003713C
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			if (this.m_ScreenSpaceShadowsMaterial == null)
			{
				Debug.LogErrorFormat("Missing {0}. {1} render pass will not execute. Check for missing reference in the renderer resources.", new object[]
				{
					this.m_ScreenSpaceShadowsMaterial,
					base.GetType().Name
				});
				return;
			}
			if (renderingData.lightData.mainLightIndex == -1)
			{
				return;
			}
			Camera camera = renderingData.cameraData.camera;
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			using (new ProfilingScope(commandBuffer, ProfilingSampler.Get<URPProfileId>(URPProfileId.ResolveShadows)))
			{
				if (!renderingData.cameraData.xr.enabled)
				{
					commandBuffer.SetViewProjectionMatrices(Matrix4x4.identity, Matrix4x4.identity);
					commandBuffer.DrawMesh(RenderingUtils.fullscreenMesh, Matrix4x4.identity, this.m_ScreenSpaceShadowsMaterial);
					commandBuffer.SetViewProjectionMatrices(camera.worldToCameraMatrix, camera.projectionMatrix);
				}
				else
				{
					RenderTargetIdentifier renderTargetIdentifier = this.m_ScreenSpaceShadowmap.Identifier();
					base.Blit(commandBuffer, renderTargetIdentifier, renderTargetIdentifier, this.m_ScreenSpaceShadowsMaterial, 0);
				}
			}
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00039048 File Offset: 0x00037248
		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			if (cmd == null)
			{
				throw new ArgumentNullException("cmd");
			}
			cmd.ReleaseTemporaryRT(this.m_ScreenSpaceShadowmap.id);
		}

		// Token: 0x040007FE RID: 2046
		private Material m_ScreenSpaceShadowsMaterial;

		// Token: 0x040007FF RID: 2047
		private RenderTargetHandle m_ScreenSpaceShadowmap;

		// Token: 0x04000800 RID: 2048
		private RenderTextureDescriptor m_RenderTextureDescriptor;
	}
}
