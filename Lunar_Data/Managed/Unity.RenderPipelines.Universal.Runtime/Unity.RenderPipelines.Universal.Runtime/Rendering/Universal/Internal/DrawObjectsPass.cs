using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x0200010E RID: 270
	public class DrawObjectsPass : ScriptableRenderPass
	{
		// Token: 0x06000860 RID: 2144 RVA: 0x00034904 File Offset: 0x00032B04
		public DrawObjectsPass(string profilerTag, ShaderTagId[] shaderTagIds, bool opaque, RenderPassEvent evt, RenderQueueRange renderQueueRange, LayerMask layerMask, StencilState stencilState, int stencilReference)
		{
			base.profilingSampler = new ProfilingSampler("DrawObjectsPass");
			this.m_ProfilerTag = profilerTag;
			this.m_ProfilingSampler = new ProfilingSampler(profilerTag);
			foreach (ShaderTagId shaderTagId in shaderTagIds)
			{
				this.m_ShaderTagIdList.Add(shaderTagId);
			}
			base.renderPassEvent = evt;
			this.m_FilteringSettings = new FilteringSettings(new RenderQueueRange?(renderQueueRange), layerMask, uint.MaxValue, 0);
			this.m_RenderStateBlock = new RenderStateBlock(RenderStateMask.Nothing);
			this.m_IsOpaque = opaque;
			if (stencilState.enabled)
			{
				this.m_RenderStateBlock.stencilReference = stencilReference;
				this.m_RenderStateBlock.mask = RenderStateMask.Stencil;
				this.m_RenderStateBlock.stencilState = stencilState;
			}
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x000349D0 File Offset: 0x00032BD0
		public DrawObjectsPass(string profilerTag, bool opaque, RenderPassEvent evt, RenderQueueRange renderQueueRange, LayerMask layerMask, StencilState stencilState, int stencilReference)
			: this(profilerTag, new ShaderTagId[]
			{
				new ShaderTagId("SRPDefaultUnlit"),
				new ShaderTagId("UniversalForward"),
				new ShaderTagId("UniversalForwardOnly")
			}, opaque, evt, renderQueueRange, layerMask, stencilState, stencilReference)
		{
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00034A27 File Offset: 0x00032C27
		internal DrawObjectsPass(URPProfileId profileId, bool opaque, RenderPassEvent evt, RenderQueueRange renderQueueRange, LayerMask layerMask, StencilState stencilState, int stencilReference)
			: this(profileId.GetType().Name, opaque, evt, renderQueueRange, layerMask, stencilState, stencilReference)
		{
			this.m_ProfilingSampler = ProfilingSampler.Get<URPProfileId>(profileId);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00034A58 File Offset: 0x00032C58
		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			if (renderingData.cameraData.renderer.useDepthPriming && this.m_IsOpaque && (renderingData.cameraData.renderType == CameraRenderType.Base || renderingData.cameraData.clearDepth))
			{
				this.m_RenderStateBlock.depthState = new DepthState(false, CompareFunction.Equal);
				this.m_RenderStateBlock.mask = this.m_RenderStateBlock.mask | RenderStateMask.Depth;
				return;
			}
			if (this.m_RenderStateBlock.depthState.compareFunction == CompareFunction.Equal)
			{
				this.m_RenderStateBlock.depthState = new DepthState(true, CompareFunction.LessEqual);
				this.m_RenderStateBlock.mask = this.m_RenderStateBlock.mask | RenderStateMask.Depth;
			}
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00034AFC File Offset: 0x00032CFC
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			using (new ProfilingScope(commandBuffer, this.m_ProfilingSampler))
			{
				Vector4 vector = new Vector4(0f, 0f, 0f, this.m_IsOpaque ? 1f : 0f);
				commandBuffer.SetGlobalVector(DrawObjectsPass.s_DrawObjectPassDataPropID, vector);
				float num = (renderingData.cameraData.IsCameraProjectionMatrixFlipped() ? (-1f) : 1f);
				Vector4 vector2 = ((num < 0f) ? new Vector4(num, 1f, -1f, 1f) : new Vector4(num, 0f, 1f, 1f));
				commandBuffer.SetGlobalVector(ShaderPropertyId.scaleBiasRt, vector2);
				context.ExecuteCommandBuffer(commandBuffer);
				commandBuffer.Clear();
				SortingCriteria sortingCriteria = (this.m_IsOpaque ? renderingData.cameraData.defaultOpaqueSortFlags : SortingCriteria.CommonTransparent);
				if (renderingData.cameraData.renderer.useDepthPriming && this.m_IsOpaque && (renderingData.cameraData.renderType == CameraRenderType.Base || renderingData.cameraData.clearDepth))
				{
					sortingCriteria = SortingCriteria.SortingLayer | SortingCriteria.RenderQueue | SortingCriteria.OptimizeStateChanges | SortingCriteria.CanvasOrder;
				}
				FilteringSettings filteringSettings = this.m_FilteringSettings;
				DrawingSettings drawingSettings = base.CreateDrawingSettings(this.m_ShaderTagIdList, ref renderingData, sortingCriteria);
				DebugHandler activeDebugHandler = base.GetActiveDebugHandler(renderingData);
				if (activeDebugHandler != null)
				{
					activeDebugHandler.DrawWithDebugRenderState(context, commandBuffer, ref renderingData, ref drawingSettings, ref filteringSettings, ref this.m_RenderStateBlock, delegate(ScriptableRenderContext ctx, ref RenderingData data, ref DrawingSettings ds, ref FilteringSettings fs, ref RenderStateBlock rsb)
					{
						ctx.DrawRenderers(data.cullResults, ref ds, ref fs, ref rsb);
					});
				}
				else
				{
					context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref filteringSettings, ref this.m_RenderStateBlock);
				}
			}
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x040007A4 RID: 1956
		private FilteringSettings m_FilteringSettings;

		// Token: 0x040007A5 RID: 1957
		private RenderStateBlock m_RenderStateBlock;

		// Token: 0x040007A6 RID: 1958
		private List<ShaderTagId> m_ShaderTagIdList = new List<ShaderTagId>();

		// Token: 0x040007A7 RID: 1959
		private string m_ProfilerTag;

		// Token: 0x040007A8 RID: 1960
		private ProfilingSampler m_ProfilingSampler;

		// Token: 0x040007A9 RID: 1961
		private bool m_IsOpaque;

		// Token: 0x040007AA RID: 1962
		private bool m_UseDepthPriming;

		// Token: 0x040007AB RID: 1963
		private static readonly int s_DrawObjectPassDataPropID = Shader.PropertyToID("_DrawObjectPassData");
	}
}
