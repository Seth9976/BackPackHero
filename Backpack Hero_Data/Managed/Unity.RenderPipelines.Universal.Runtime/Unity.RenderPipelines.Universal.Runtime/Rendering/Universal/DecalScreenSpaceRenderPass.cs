using System;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal.Internal;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000076 RID: 118
	internal class DecalScreenSpaceRenderPass : ScriptableRenderPass
	{
		// Token: 0x060003F8 RID: 1016 RVA: 0x00017A10 File Offset: 0x00015C10
		public DecalScreenSpaceRenderPass(DecalScreenSpaceSettings settings, DecalDrawScreenSpaceSystem drawSystem)
		{
			base.renderPassEvent = RenderPassEvent.AfterRenderingSkybox;
			base.ConfigureInput(ScriptableRenderPassInput.Depth);
			this.m_DrawSystem = drawSystem;
			this.m_Settings = settings;
			this.m_ProfilingSampler = new ProfilingSampler("Decal Screen Space Render");
			this.m_FilteringSettings = new FilteringSettings(new RenderQueueRange?(RenderQueueRange.opaque), -1, uint.MaxValue, 0);
			this.m_ShaderTagIdList = new List<ShaderTagId>();
			if (this.m_DrawSystem == null)
			{
				this.m_ShaderTagIdList.Add(new ShaderTagId("DecalScreenSpaceProjector"));
				return;
			}
			this.m_ShaderTagIdList.Add(new ShaderTagId("DecalScreenSpaceMesh"));
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00017AAC File Offset: 0x00015CAC
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			SortingCriteria sortingCriteria = SortingCriteria.CommonTransparent;
			DrawingSettings drawingSettings = base.CreateDrawingSettings(this.m_ShaderTagIdList, ref renderingData, sortingCriteria);
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			using (new ProfilingScope(commandBuffer, this.m_ProfilingSampler))
			{
				context.ExecuteCommandBuffer(commandBuffer);
				commandBuffer.Clear();
				RenderingUtils.SetScaleBiasRt(commandBuffer, in renderingData);
				NormalReconstruction.SetupProperties(commandBuffer, in renderingData.cameraData);
				CoreUtils.SetKeyword(commandBuffer, "_DECAL_NORMAL_BLEND_LOW", this.m_Settings.normalBlend == DecalNormalBlend.Low);
				CoreUtils.SetKeyword(commandBuffer, "_DECAL_NORMAL_BLEND_MEDIUM", this.m_Settings.normalBlend == DecalNormalBlend.Medium);
				CoreUtils.SetKeyword(commandBuffer, "_DECAL_NORMAL_BLEND_HIGH", this.m_Settings.normalBlend == DecalNormalBlend.High);
				context.ExecuteCommandBuffer(commandBuffer);
				commandBuffer.Clear();
				DecalDrawScreenSpaceSystem drawSystem = this.m_DrawSystem;
				if (drawSystem != null)
				{
					drawSystem.Execute(commandBuffer);
				}
				context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref this.m_FilteringSettings);
			}
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00017BB0 File Offset: 0x00015DB0
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

		// Token: 0x040002F6 RID: 758
		private FilteringSettings m_FilteringSettings;

		// Token: 0x040002F7 RID: 759
		private ProfilingSampler m_ProfilingSampler;

		// Token: 0x040002F8 RID: 760
		private List<ShaderTagId> m_ShaderTagIdList;

		// Token: 0x040002F9 RID: 761
		private DecalDrawScreenSpaceSystem m_DrawSystem;

		// Token: 0x040002FA RID: 762
		private DecalScreenSpaceSettings m_Settings;
	}
}
