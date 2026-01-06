using System;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x0200010C RID: 268
	public class DepthNormalOnlyPass : ScriptableRenderPass
	{
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x00034272 File Offset: 0x00032472
		// (set) Token: 0x0600083F RID: 2111 RVA: 0x0003427A File Offset: 0x0003247A
		internal RenderTextureDescriptor normalDescriptor { get; set; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x00034283 File Offset: 0x00032483
		// (set) Token: 0x06000841 RID: 2113 RVA: 0x0003428B File Offset: 0x0003248B
		internal RenderTextureDescriptor depthDescriptor { get; set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x00034294 File Offset: 0x00032494
		// (set) Token: 0x06000843 RID: 2115 RVA: 0x0003429C File Offset: 0x0003249C
		internal bool allocateDepth { get; set; } = true;

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x000342A5 File Offset: 0x000324A5
		// (set) Token: 0x06000845 RID: 2117 RVA: 0x000342AD File Offset: 0x000324AD
		internal bool allocateNormal { get; set; } = true;

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x000342B6 File Offset: 0x000324B6
		// (set) Token: 0x06000847 RID: 2119 RVA: 0x000342BE File Offset: 0x000324BE
		internal List<ShaderTagId> shaderTagIds { get; set; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x000342C7 File Offset: 0x000324C7
		// (set) Token: 0x06000849 RID: 2121 RVA: 0x000342CF File Offset: 0x000324CF
		private RenderTargetHandle depthHandle { get; set; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x000342D8 File Offset: 0x000324D8
		// (set) Token: 0x0600084B RID: 2123 RVA: 0x000342E0 File Offset: 0x000324E0
		private RenderTargetHandle normalHandle { get; set; }

		// Token: 0x0600084C RID: 2124 RVA: 0x000342EC File Offset: 0x000324EC
		public DepthNormalOnlyPass(RenderPassEvent evt, RenderQueueRange renderQueueRange, LayerMask layerMask)
		{
			base.profilingSampler = new ProfilingSampler("DepthNormalOnlyPass");
			this.m_FilteringSettings = new FilteringSettings(new RenderQueueRange?(renderQueueRange), layerMask, uint.MaxValue, 0);
			base.renderPassEvent = evt;
			base.useNativeRenderPass = false;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0003434C File Offset: 0x0003254C
		public void Setup(RenderTextureDescriptor baseDescriptor, RenderTargetHandle depthHandle, RenderTargetHandle normalHandle)
		{
			GraphicsFormat graphicsFormat;
			if (RenderingUtils.SupportsGraphicsFormat(GraphicsFormat.R8G8B8A8_SNorm, FormatUsage.Render))
			{
				graphicsFormat = GraphicsFormat.R8G8B8A8_SNorm;
			}
			else if (RenderingUtils.SupportsGraphicsFormat(GraphicsFormat.R16G16B16A16_SFloat, FormatUsage.Render))
			{
				graphicsFormat = GraphicsFormat.R16G16B16A16_SFloat;
			}
			else
			{
				graphicsFormat = GraphicsFormat.R32G32B32A32_SFloat;
			}
			this.depthHandle = depthHandle;
			this.m_RendererMSAASamples = baseDescriptor.msaaSamples;
			baseDescriptor.colorFormat = RenderTextureFormat.Depth;
			baseDescriptor.depthBufferBits = 32;
			baseDescriptor.msaaSamples = 1;
			this.depthDescriptor = baseDescriptor;
			this.normalHandle = normalHandle;
			baseDescriptor.graphicsFormat = graphicsFormat;
			baseDescriptor.depthBufferBits = 0;
			this.normalDescriptor = baseDescriptor;
			this.allocateDepth = true;
			this.allocateNormal = true;
			this.shaderTagIds = DepthNormalOnlyPass.k_DepthNormals;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x000343E8 File Offset: 0x000325E8
		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			if (this.allocateNormal)
			{
				RenderTextureDescriptor normalDescriptor = this.normalDescriptor;
				normalDescriptor.msaaSamples = (renderingData.cameraData.renderer.useDepthPriming ? this.m_RendererMSAASamples : 1);
				cmd.GetTemporaryRT(this.normalHandle.id, normalDescriptor, FilterMode.Point);
			}
			if (this.allocateDepth)
			{
				cmd.GetTemporaryRT(this.depthHandle.id, this.depthDescriptor, FilterMode.Point);
			}
			if (renderingData.cameraData.renderer.useDepthPriming && (renderingData.cameraData.renderType == CameraRenderType.Base || renderingData.cameraData.clearDepth))
			{
				base.ConfigureTarget(new RenderTargetIdentifier(this.normalHandle.Identifier(), 0, CubemapFace.Unknown, -1), new RenderTargetIdentifier(renderingData.cameraData.renderer.cameraDepthTarget, 0, CubemapFace.Unknown, -1));
			}
			else
			{
				base.ConfigureTarget(new RenderTargetIdentifier(this.normalHandle.Identifier(), 0, CubemapFace.Unknown, -1), new RenderTargetIdentifier(this.depthHandle.Identifier(), 0, CubemapFace.Unknown, -1));
			}
			base.ConfigureClear(ClearFlag.All, Color.black);
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00034500 File Offset: 0x00032700
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			using (new ProfilingScope(commandBuffer, ProfilingSampler.Get<URPProfileId>(URPProfileId.DepthNormalPrepass)))
			{
				context.ExecuteCommandBuffer(commandBuffer);
				commandBuffer.Clear();
				SortingCriteria defaultOpaqueSortFlags = renderingData.cameraData.defaultOpaqueSortFlags;
				DrawingSettings drawingSettings = base.CreateDrawingSettings(this.shaderTagIds, ref renderingData, defaultOpaqueSortFlags);
				drawingSettings.perObjectData = PerObjectData.None;
				context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref this.m_FilteringSettings);
			}
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0003459C File Offset: 0x0003279C
		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			if (cmd == null)
			{
				throw new ArgumentNullException("cmd");
			}
			if (this.depthHandle != RenderTargetHandle.CameraTarget)
			{
				if (this.allocateNormal)
				{
					cmd.ReleaseTemporaryRT(this.normalHandle.id);
				}
				if (this.allocateDepth)
				{
					cmd.ReleaseTemporaryRT(this.depthHandle.id);
				}
				this.normalHandle = RenderTargetHandle.CameraTarget;
				this.depthHandle = RenderTargetHandle.CameraTarget;
			}
		}

		// Token: 0x0400079B RID: 1947
		private FilteringSettings m_FilteringSettings;

		// Token: 0x0400079C RID: 1948
		private int m_RendererMSAASamples = 1;

		// Token: 0x0400079D RID: 1949
		private static readonly List<ShaderTagId> k_DepthNormals = new List<ShaderTagId>
		{
			new ShaderTagId("DepthNormals"),
			new ShaderTagId("DepthNormalsOnly")
		};
	}
}
