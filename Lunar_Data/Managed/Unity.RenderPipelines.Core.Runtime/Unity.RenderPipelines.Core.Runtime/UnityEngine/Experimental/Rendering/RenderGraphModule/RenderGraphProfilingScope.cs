using System;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x02000023 RID: 35
	public struct RenderGraphProfilingScope : IDisposable
	{
		// Token: 0x0600013B RID: 315 RVA: 0x00009748 File Offset: 0x00007948
		public RenderGraphProfilingScope(RenderGraph renderGraph, ProfilingSampler sampler)
		{
			this.m_RenderGraph = renderGraph;
			this.m_Sampler = sampler;
			this.m_Disposed = false;
			renderGraph.BeginProfilingSampler(sampler);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00009766 File Offset: 0x00007966
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000976F File Offset: 0x0000796F
		private void Dispose(bool disposing)
		{
			if (this.m_Disposed)
			{
				return;
			}
			if (disposing)
			{
				this.m_RenderGraph.EndProfilingSampler(this.m_Sampler);
			}
			this.m_Disposed = true;
		}

		// Token: 0x040000F9 RID: 249
		private bool m_Disposed;

		// Token: 0x040000FA RID: 250
		private ProfilingSampler m_Sampler;

		// Token: 0x040000FB RID: 251
		private RenderGraph m_RenderGraph;
	}
}
