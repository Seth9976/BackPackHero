using System;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x02000109 RID: 265
	public class CopyColorPass : ScriptableRenderPass
	{
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x00033A4E File Offset: 0x00031C4E
		// (set) Token: 0x06000825 RID: 2085 RVA: 0x00033A56 File Offset: 0x00031C56
		private RenderTargetIdentifier source { get; set; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x00033A5F File Offset: 0x00031C5F
		// (set) Token: 0x06000827 RID: 2087 RVA: 0x00033A67 File Offset: 0x00031C67
		private RenderTargetHandle destination { get; set; }

		// Token: 0x06000828 RID: 2088 RVA: 0x00033A70 File Offset: 0x00031C70
		public CopyColorPass(RenderPassEvent evt, Material samplingMaterial, Material copyColorMaterial = null)
		{
			base.profilingSampler = new ProfilingSampler("CopyColorPass");
			this.m_SamplingMaterial = samplingMaterial;
			this.m_CopyColorMaterial = copyColorMaterial;
			this.m_SampleOffsetShaderHandle = Shader.PropertyToID("_SampleOffset");
			base.renderPassEvent = evt;
			this.m_DownsamplingMethod = Downsampling.None;
			base.useNativeRenderPass = false;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00033AC6 File Offset: 0x00031CC6
		public void Setup(RenderTargetIdentifier source, RenderTargetHandle destination, Downsampling downsampling)
		{
			this.source = source;
			this.destination = destination;
			this.m_DownsamplingMethod = downsampling;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00033AE0 File Offset: 0x00031CE0
		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			RenderTextureDescriptor cameraTargetDescriptor = renderingData.cameraData.cameraTargetDescriptor;
			cameraTargetDescriptor.msaaSamples = 1;
			cameraTargetDescriptor.depthBufferBits = 0;
			if (this.m_DownsamplingMethod == Downsampling._2xBilinear)
			{
				cameraTargetDescriptor.width /= 2;
				cameraTargetDescriptor.height /= 2;
			}
			else if (this.m_DownsamplingMethod == Downsampling._4xBox || this.m_DownsamplingMethod == Downsampling._4xBilinear)
			{
				cameraTargetDescriptor.width /= 4;
				cameraTargetDescriptor.height /= 4;
			}
			cmd.GetTemporaryRT(this.destination.id, cameraTargetDescriptor, (this.m_DownsamplingMethod == Downsampling.None) ? FilterMode.Point : FilterMode.Bilinear);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00033B84 File Offset: 0x00031D84
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			if (this.m_SamplingMaterial == null)
			{
				Debug.LogErrorFormat("Missing {0}. {1} render pass will not execute. Check for missing reference in the renderer resources.", new object[]
				{
					this.m_SamplingMaterial,
					base.GetType().Name
				});
				return;
			}
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			if (this.source == renderingData.cameraData.renderer.GetCameraColorFrontBuffer(commandBuffer))
			{
				this.source = renderingData.cameraData.renderer.cameraColorTarget;
			}
			using (new ProfilingScope(commandBuffer, ProfilingSampler.Get<URPProfileId>(URPProfileId.CopyColor)))
			{
				RenderTargetIdentifier renderTargetIdentifier = this.destination.Identifier();
				ScriptableRenderer.SetRenderTarget(commandBuffer, renderTargetIdentifier, BuiltinRenderTextureType.CameraTarget, base.clearFlag, base.clearColor);
				bool enabled = renderingData.cameraData.xr.enabled;
				switch (this.m_DownsamplingMethod)
				{
				case Downsampling.None:
					RenderingUtils.Blit(commandBuffer, this.source, renderTargetIdentifier, this.m_CopyColorMaterial, 0, enabled, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
					break;
				case Downsampling._2xBilinear:
					RenderingUtils.Blit(commandBuffer, this.source, renderTargetIdentifier, this.m_CopyColorMaterial, 0, enabled, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
					break;
				case Downsampling._4xBox:
					this.m_SamplingMaterial.SetFloat(this.m_SampleOffsetShaderHandle, 2f);
					RenderingUtils.Blit(commandBuffer, this.source, renderTargetIdentifier, this.m_SamplingMaterial, 0, enabled, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
					break;
				case Downsampling._4xBilinear:
					RenderingUtils.Blit(commandBuffer, this.source, renderTargetIdentifier, this.m_CopyColorMaterial, 0, enabled, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
					break;
				}
			}
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00033D1C File Offset: 0x00031F1C
		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			if (cmd == null)
			{
				throw new ArgumentNullException("cmd");
			}
			if (this.destination != RenderTargetHandle.CameraTarget)
			{
				cmd.ReleaseTemporaryRT(this.destination.id);
				this.destination = RenderTargetHandle.CameraTarget;
			}
		}

		// Token: 0x04000788 RID: 1928
		private int m_SampleOffsetShaderHandle;

		// Token: 0x04000789 RID: 1929
		private Material m_SamplingMaterial;

		// Token: 0x0400078A RID: 1930
		private Downsampling m_DownsamplingMethod;

		// Token: 0x0400078B RID: 1931
		private Material m_CopyColorMaterial;
	}
}
