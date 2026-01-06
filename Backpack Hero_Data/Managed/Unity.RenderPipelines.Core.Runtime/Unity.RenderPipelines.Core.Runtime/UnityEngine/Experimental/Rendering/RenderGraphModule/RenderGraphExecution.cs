using System;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x0200001E RID: 30
	public struct RenderGraphExecution : IDisposable
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x00007006 File Offset: 0x00005206
		internal RenderGraphExecution(RenderGraph renderGraph)
		{
			this.renderGraph = renderGraph;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000700F File Offset: 0x0000520F
		public void Dispose()
		{
			this.renderGraph.Execute();
		}

		// Token: 0x040000CF RID: 207
		private RenderGraph renderGraph;
	}
}
