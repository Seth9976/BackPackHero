using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200005E RID: 94
	internal class DecalForwardEmissivePass : ScriptableRenderPass
	{
		// Token: 0x06000365 RID: 869 RVA: 0x00014C08 File Offset: 0x00012E08
		public DecalForwardEmissivePass(DecalDrawFowardEmissiveSystem drawSystem)
		{
			base.renderPassEvent = RenderPassEvent.AfterRenderingOpaques;
			base.ConfigureInput(ScriptableRenderPassInput.Depth);
			this.m_DrawSystem = drawSystem;
			this.m_ProfilingSampler = new ProfilingSampler("Decal Forward Emissive Render");
			this.m_FilteringSettings = new FilteringSettings(new RenderQueueRange?(RenderQueueRange.opaque), -1, uint.MaxValue, 0);
			this.m_ShaderTagIdList = new List<ShaderTagId>();
			this.m_ShaderTagIdList.Add(new ShaderTagId("DecalMeshForwardEmissive"));
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00014C7C File Offset: 0x00012E7C
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			SortingCriteria defaultOpaqueSortFlags = renderingData.cameraData.defaultOpaqueSortFlags;
			DrawingSettings drawingSettings = base.CreateDrawingSettings(this.m_ShaderTagIdList, ref renderingData, defaultOpaqueSortFlags);
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			using (new ProfilingScope(commandBuffer, this.m_ProfilingSampler))
			{
				context.ExecuteCommandBuffer(commandBuffer);
				commandBuffer.Clear();
				this.m_DrawSystem.Execute(commandBuffer);
				context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref this.m_FilteringSettings);
			}
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x04000278 RID: 632
		private FilteringSettings m_FilteringSettings;

		// Token: 0x04000279 RID: 633
		private ProfilingSampler m_ProfilingSampler;

		// Token: 0x0400027A RID: 634
		private List<ShaderTagId> m_ShaderTagIdList;

		// Token: 0x0400027B RID: 635
		private DecalDrawFowardEmissiveSystem m_DrawSystem;
	}
}
