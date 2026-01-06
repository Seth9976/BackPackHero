using System;
using Unity.Collections;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x02000110 RID: 272
	internal class GBufferPass : ScriptableRenderPass
	{
		// Token: 0x06000869 RID: 2153 RVA: 0x00034FA0 File Offset: 0x000331A0
		public GBufferPass(RenderPassEvent evt, RenderQueueRange renderQueueRange, LayerMask layerMask, StencilState stencilState, int stencilReference, DeferredLights deferredLights)
		{
			base.profilingSampler = new ProfilingSampler("GBufferPass");
			base.renderPassEvent = evt;
			this.m_DeferredLights = deferredLights;
			this.m_FilteringSettings = new FilteringSettings(new RenderQueueRange?(renderQueueRange), layerMask, uint.MaxValue, 0);
			this.m_RenderStateBlock = new RenderStateBlock(RenderStateMask.Nothing);
			this.m_RenderStateBlock.stencilState = stencilState;
			this.m_RenderStateBlock.stencilReference = stencilReference;
			this.m_RenderStateBlock.mask = RenderStateMask.Stencil;
			this.m_ShaderTagValues = new ShaderTagId[4];
			this.m_ShaderTagValues[0] = GBufferPass.s_ShaderTagLit;
			this.m_ShaderTagValues[1] = GBufferPass.s_ShaderTagSimpleLit;
			this.m_ShaderTagValues[2] = GBufferPass.s_ShaderTagUnlit;
			this.m_ShaderTagValues[3] = default(ShaderTagId);
			this.m_RenderStateBlocks = new RenderStateBlock[4];
			this.m_RenderStateBlocks[0] = DeferredLights.OverwriteStencil(this.m_RenderStateBlock, 96, 32);
			this.m_RenderStateBlocks[1] = DeferredLights.OverwriteStencil(this.m_RenderStateBlock, 96, 64);
			this.m_RenderStateBlocks[2] = DeferredLights.OverwriteStencil(this.m_RenderStateBlock, 96, 0);
			this.m_RenderStateBlocks[3] = this.m_RenderStateBlocks[0];
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x000350F4 File Offset: 0x000332F4
		public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
		{
			RenderTargetHandle[] gbufferAttachments = this.m_DeferredLights.GbufferAttachments;
			if (cmd != null)
			{
				for (int i = 0; i < gbufferAttachments.Length; i++)
				{
					if (i != this.m_DeferredLights.GBufferLightingIndex)
					{
						if (i == this.m_DeferredLights.GBufferNormalSmoothnessIndex && this.m_DeferredLights.HasNormalPrepass)
						{
							if (this.m_DeferredLights.UseRenderPass)
							{
								this.m_DeferredLights.DeferredInputIsTransient[i] = false;
							}
						}
						else if (!this.m_DeferredLights.UseRenderPass || i == this.m_DeferredLights.GBufferShadowMask || i == this.m_DeferredLights.GBufferRenderingLayers || i == this.m_DeferredLights.GbufferDepthIndex || this.m_DeferredLights.HasDepthPrepass)
						{
							RenderTextureDescriptor renderTextureDescriptor = cameraTextureDescriptor;
							renderTextureDescriptor.depthBufferBits = 0;
							renderTextureDescriptor.stencilFormat = GraphicsFormat.None;
							renderTextureDescriptor.graphicsFormat = this.m_DeferredLights.GetGBufferFormat(i);
							cmd.GetTemporaryRT(this.m_DeferredLights.GbufferAttachments[i].id, renderTextureDescriptor);
						}
					}
				}
			}
			base.ConfigureTarget(this.m_DeferredLights.GbufferAttachmentIdentifiers, this.m_DeferredLights.DepthAttachmentIdentifier, this.m_DeferredLights.GbufferFormats);
			base.ConfigureClear(ClearFlag.None, Color.black);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00035230 File Offset: 0x00033430
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			using (new ProfilingScope(commandBuffer, this.m_ProfilingSampler))
			{
				context.ExecuteCommandBuffer(commandBuffer);
				commandBuffer.Clear();
				if (this.m_DeferredLights.IsOverlay)
				{
					this.m_DeferredLights.ClearStencilPartial(commandBuffer);
					context.ExecuteCommandBuffer(commandBuffer);
					commandBuffer.Clear();
				}
				ShaderTagId shaderTagId = GBufferPass.s_ShaderTagUniversalGBuffer;
				DrawingSettings drawingSettings = base.CreateDrawingSettings(shaderTagId, ref renderingData, renderingData.cameraData.defaultOpaqueSortFlags);
				ShaderTagId shaderTagId2 = GBufferPass.s_ShaderTagUniversalMaterialType;
				NativeArray<ShaderTagId> nativeArray = new NativeArray<ShaderTagId>(this.m_ShaderTagValues, Allocator.Temp);
				NativeArray<RenderStateBlock> nativeArray2 = new NativeArray<RenderStateBlock>(this.m_RenderStateBlocks, Allocator.Temp);
				context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref this.m_FilteringSettings, shaderTagId2, false, nativeArray, nativeArray2);
				nativeArray.Dispose();
				nativeArray2.Dispose();
				commandBuffer.SetGlobalTexture(GBufferPass.s_CameraNormalsTextureID, this.m_DeferredLights.GbufferAttachmentIdentifiers[this.m_DeferredLights.GBufferNormalSmoothnessIndex]);
			}
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00035348 File Offset: 0x00033548
		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			RenderTargetHandle[] gbufferAttachments = this.m_DeferredLights.GbufferAttachments;
			for (int i = 0; i < gbufferAttachments.Length; i++)
			{
				if (i != this.m_DeferredLights.GBufferLightingIndex && (i != this.m_DeferredLights.GBufferNormalSmoothnessIndex || !this.m_DeferredLights.HasNormalPrepass))
				{
					cmd.ReleaseTemporaryRT(gbufferAttachments[i].id);
				}
			}
		}

		// Token: 0x040007AE RID: 1966
		private static readonly int s_CameraNormalsTextureID = Shader.PropertyToID("_CameraNormalsTexture");

		// Token: 0x040007AF RID: 1967
		private static ShaderTagId s_ShaderTagLit = new ShaderTagId("Lit");

		// Token: 0x040007B0 RID: 1968
		private static ShaderTagId s_ShaderTagSimpleLit = new ShaderTagId("SimpleLit");

		// Token: 0x040007B1 RID: 1969
		private static ShaderTagId s_ShaderTagUnlit = new ShaderTagId("Unlit");

		// Token: 0x040007B2 RID: 1970
		private static ShaderTagId s_ShaderTagUniversalGBuffer = new ShaderTagId("UniversalGBuffer");

		// Token: 0x040007B3 RID: 1971
		private static ShaderTagId s_ShaderTagUniversalMaterialType = new ShaderTagId("UniversalMaterialType");

		// Token: 0x040007B4 RID: 1972
		private ProfilingSampler m_ProfilingSampler = new ProfilingSampler("Render GBuffer");

		// Token: 0x040007B5 RID: 1973
		private DeferredLights m_DeferredLights;

		// Token: 0x040007B6 RID: 1974
		private ShaderTagId[] m_ShaderTagValues;

		// Token: 0x040007B7 RID: 1975
		private RenderStateBlock[] m_RenderStateBlocks;

		// Token: 0x040007B8 RID: 1976
		private FilteringSettings m_FilteringSettings;

		// Token: 0x040007B9 RID: 1977
		private RenderStateBlock m_RenderStateBlock;
	}
}
