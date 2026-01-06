using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000060 RID: 96
	internal class DecalPreviewPass : ScriptableRenderPass
	{
		// Token: 0x0600036A RID: 874 RVA: 0x00014D9C File Offset: 0x00012F9C
		public DecalPreviewPass()
		{
			base.renderPassEvent = RenderPassEvent.AfterRenderingOpaques;
			base.ConfigureInput(ScriptableRenderPassInput.Depth);
			this.m_ProfilingSampler = new ProfilingSampler("Decal Preview Render");
			this.m_FilteringSettings = new FilteringSettings(new RenderQueueRange?(RenderQueueRange.opaque), -1, uint.MaxValue, 0);
			this.m_ShaderTagIdList = new List<ShaderTagId>();
			this.m_ShaderTagIdList.Add(new ShaderTagId("DecalScreenSpaceMesh"));
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00014E0C File Offset: 0x0001300C
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			SortingCriteria defaultOpaqueSortFlags = renderingData.cameraData.defaultOpaqueSortFlags;
			DrawingSettings drawingSettings = base.CreateDrawingSettings(this.m_ShaderTagIdList, ref renderingData, defaultOpaqueSortFlags);
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			using (new ProfilingScope(commandBuffer, this.m_ProfilingSampler))
			{
				context.ExecuteCommandBuffer(commandBuffer);
				commandBuffer.Clear();
				context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref this.m_FilteringSettings);
			}
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x0400027D RID: 637
		private FilteringSettings m_FilteringSettings;

		// Token: 0x0400027E RID: 638
		private List<ShaderTagId> m_ShaderTagIdList;

		// Token: 0x0400027F RID: 639
		private ProfilingSampler m_ProfilingSampler;
	}
}
