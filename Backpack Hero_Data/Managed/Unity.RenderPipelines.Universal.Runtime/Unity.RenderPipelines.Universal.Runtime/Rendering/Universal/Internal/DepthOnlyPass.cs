using System;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x0200010D RID: 269
	public class DepthOnlyPass : ScriptableRenderPass
	{
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x00034643 File Offset: 0x00032843
		// (set) Token: 0x06000853 RID: 2131 RVA: 0x0003464B File Offset: 0x0003284B
		private RenderTargetHandle depthAttachmentHandle { get; set; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x00034654 File Offset: 0x00032854
		// (set) Token: 0x06000855 RID: 2133 RVA: 0x0003465C File Offset: 0x0003285C
		internal RenderTextureDescriptor descriptor { get; set; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x00034665 File Offset: 0x00032865
		// (set) Token: 0x06000857 RID: 2135 RVA: 0x0003466D File Offset: 0x0003286D
		internal bool allocateDepth { get; set; } = true;

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x00034676 File Offset: 0x00032876
		// (set) Token: 0x06000859 RID: 2137 RVA: 0x0003467E File Offset: 0x0003287E
		internal ShaderTagId shaderTagId { get; set; } = DepthOnlyPass.k_ShaderTagId;

		// Token: 0x0600085A RID: 2138 RVA: 0x00034688 File Offset: 0x00032888
		public DepthOnlyPass(RenderPassEvent evt, RenderQueueRange renderQueueRange, LayerMask layerMask)
		{
			base.profilingSampler = new ProfilingSampler("DepthOnlyPass");
			this.m_FilteringSettings = new FilteringSettings(new RenderQueueRange?(renderQueueRange), layerMask, uint.MaxValue, 0);
			base.renderPassEvent = evt;
			base.useNativeRenderPass = false;
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x000346E4 File Offset: 0x000328E4
		public void Setup(RenderTextureDescriptor baseDescriptor, RenderTargetHandle depthAttachmentHandle)
		{
			this.depthAttachmentHandle = depthAttachmentHandle;
			baseDescriptor.colorFormat = RenderTextureFormat.Depth;
			baseDescriptor.depthBufferBits = 32;
			baseDescriptor.msaaSamples = 1;
			this.descriptor = baseDescriptor;
			this.allocateDepth = true;
			this.shaderTagId = DepthOnlyPass.k_ShaderTagId;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00034720 File Offset: 0x00032920
		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			if (this.allocateDepth)
			{
				cmd.GetTemporaryRT(this.depthAttachmentHandle.id, this.descriptor, FilterMode.Point);
			}
			RenderTextureDescriptor cameraTargetDescriptor = renderingData.cameraData.cameraTargetDescriptor;
			if (renderingData.cameraData.renderer.useDepthPriming && (renderingData.cameraData.renderType == CameraRenderType.Base || renderingData.cameraData.clearDepth))
			{
				base.ConfigureTarget(renderingData.cameraData.renderer.cameraDepthTarget, this.descriptor.depthStencilFormat, cameraTargetDescriptor.width, cameraTargetDescriptor.height, 1, true);
			}
			else
			{
				base.ConfigureTarget(new RenderTargetIdentifier(this.depthAttachmentHandle.Identifier(), 0, CubemapFace.Unknown, -1), this.descriptor.depthStencilFormat, cameraTargetDescriptor.width, cameraTargetDescriptor.height, 1, true);
			}
			base.ConfigureClear(ClearFlag.Depth, Color.black);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00034804 File Offset: 0x00032A04
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			using (new ProfilingScope(commandBuffer, ProfilingSampler.Get<URPProfileId>(URPProfileId.DepthPrepass)))
			{
				context.ExecuteCommandBuffer(commandBuffer);
				commandBuffer.Clear();
				SortingCriteria defaultOpaqueSortFlags = renderingData.cameraData.defaultOpaqueSortFlags;
				DrawingSettings drawingSettings = base.CreateDrawingSettings(this.shaderTagId, ref renderingData, defaultOpaqueSortFlags);
				drawingSettings.perObjectData = PerObjectData.None;
				context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref this.m_FilteringSettings);
			}
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0003489C File Offset: 0x00032A9C
		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			if (cmd == null)
			{
				throw new ArgumentNullException("cmd");
			}
			if (this.depthAttachmentHandle != RenderTargetHandle.CameraTarget)
			{
				if (this.allocateDepth)
				{
					cmd.ReleaseTemporaryRT(this.depthAttachmentHandle.id);
				}
				this.depthAttachmentHandle = RenderTargetHandle.CameraTarget;
			}
		}

		// Token: 0x0400079E RID: 1950
		private static readonly ShaderTagId k_ShaderTagId = new ShaderTagId("DepthOnly");

		// Token: 0x040007A3 RID: 1955
		private FilteringSettings m_FilteringSettings;
	}
}
