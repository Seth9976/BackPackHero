using System;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal.Internal;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000074 RID: 116
	internal class DecalGBufferRenderPass : ScriptableRenderPass
	{
		// Token: 0x060003F1 RID: 1009 RVA: 0x00017764 File Offset: 0x00015964
		public DecalGBufferRenderPass(DecalScreenSpaceSettings settings, DecalDrawGBufferSystem drawSystem)
		{
			base.renderPassEvent = RenderPassEvent.AfterRenderingGbuffer;
			this.m_DrawSystem = drawSystem;
			this.m_Settings = settings;
			this.m_ProfilingSampler = new ProfilingSampler("Decal GBuffer Render");
			this.m_FilteringSettings = new FilteringSettings(new RenderQueueRange?(RenderQueueRange.opaque), -1, uint.MaxValue, 0);
			this.m_ShaderTagIdList = new List<ShaderTagId>();
			if (drawSystem == null)
			{
				this.m_ShaderTagIdList.Add(new ShaderTagId("DecalGBufferProjector"));
				return;
			}
			this.m_ShaderTagIdList.Add(new ShaderTagId("DecalGBufferMesh"));
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x000177F1 File Offset: 0x000159F1
		internal void Setup(DeferredLights deferredLights)
		{
			this.m_DeferredLights = deferredLights;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x000177FC File Offset: 0x000159FC
		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			if (this.m_DeferredLights != null && this.m_DeferredLights.UseRenderPass)
			{
				if (this.m_GbufferAttachments == null)
				{
					this.m_GbufferAttachments = new RenderTargetIdentifier[]
					{
						this.m_DeferredLights.GbufferAttachmentIdentifiers[0],
						this.m_DeferredLights.GbufferAttachmentIdentifiers[1],
						this.m_DeferredLights.GbufferAttachmentIdentifiers[2],
						this.m_DeferredLights.GbufferAttachmentIdentifiers[3]
					};
				}
			}
			else
			{
				this.m_GbufferAttachments = this.m_DeferredLights.GbufferAttachmentIdentifiers;
			}
			base.ConfigureTarget(this.m_GbufferAttachments, this.m_DeferredLights.DepthAttachmentIdentifier);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x000178C0 File Offset: 0x00015AC0
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			SortingCriteria defaultOpaqueSortFlags = renderingData.cameraData.defaultOpaqueSortFlags;
			DrawingSettings drawingSettings = base.CreateDrawingSettings(this.m_ShaderTagIdList, ref renderingData, defaultOpaqueSortFlags);
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			using (new ProfilingScope(commandBuffer, this.m_ProfilingSampler))
			{
				context.ExecuteCommandBuffer(commandBuffer);
				commandBuffer.Clear();
				NormalReconstruction.SetupProperties(commandBuffer, in renderingData.cameraData);
				CoreUtils.SetKeyword(commandBuffer, "_DECAL_NORMAL_BLEND_LOW", this.m_Settings.normalBlend == DecalNormalBlend.Low);
				CoreUtils.SetKeyword(commandBuffer, "_DECAL_NORMAL_BLEND_MEDIUM", this.m_Settings.normalBlend == DecalNormalBlend.Medium);
				CoreUtils.SetKeyword(commandBuffer, "_DECAL_NORMAL_BLEND_HIGH", this.m_Settings.normalBlend == DecalNormalBlend.High);
				context.ExecuteCommandBuffer(commandBuffer);
				commandBuffer.Clear();
				DecalDrawGBufferSystem drawSystem = this.m_DrawSystem;
				if (drawSystem != null)
				{
					drawSystem.Execute(commandBuffer);
				}
				context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref this.m_FilteringSettings);
			}
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x000179C4 File Offset: 0x00015BC4
		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			if (cmd == null)
			{
				throw new ArgumentNullException("cmd");
			}
			CoreUtils.SetKeyword(cmd, "_DECAL_NORMAL_BLEND_LOW", false);
			CoreUtils.SetKeyword(cmd, "_DECAL_NORMAL_BLEND_MEDIUM", false);
			CoreUtils.SetKeyword(cmd, "_DECAL_NORMAL_BLEND_HIGH", false);
		}

		// Token: 0x040002EF RID: 751
		private FilteringSettings m_FilteringSettings;

		// Token: 0x040002F0 RID: 752
		private ProfilingSampler m_ProfilingSampler;

		// Token: 0x040002F1 RID: 753
		private List<ShaderTagId> m_ShaderTagIdList;

		// Token: 0x040002F2 RID: 754
		private DecalDrawGBufferSystem m_DrawSystem;

		// Token: 0x040002F3 RID: 755
		private DecalScreenSpaceSettings m_Settings;

		// Token: 0x040002F4 RID: 756
		private DeferredLights m_DeferredLights;

		// Token: 0x040002F5 RID: 757
		private RenderTargetIdentifier[] m_GbufferAttachments;
	}
}
