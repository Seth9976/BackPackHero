using System;
using System.Diagnostics;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x0200002A RID: 42
	[DebuggerDisplay("RenderPass: {name} (Index:{index} Async:{enableAsyncCompute})")]
	internal sealed class RenderGraphPass<PassData> : RenderGraphPass where PassData : class, new()
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x0000A4E4 File Offset: 0x000086E4
		public override void Execute(RenderGraphContext renderGraphContext)
		{
			base.GetExecuteDelegate<PassData>()(this.data, renderGraphContext);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000A4F8 File Offset: 0x000086F8
		public void Initialize(int passIndex, PassData passData, string passName, ProfilingSampler sampler)
		{
			base.Clear();
			base.index = passIndex;
			this.data = passData;
			base.name = passName;
			base.customSampler = sampler;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000A51D File Offset: 0x0000871D
		public override void Release(RenderGraphObjectPool pool)
		{
			base.Clear();
			pool.Release<PassData>(this.data);
			this.data = default(PassData);
			this.renderFunc = null;
			pool.Release<RenderGraphPass<PassData>>(this);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000A54B File Offset: 0x0000874B
		public override bool HasRenderFunc()
		{
			return this.renderFunc != null;
		}

		// Token: 0x04000127 RID: 295
		internal PassData data;

		// Token: 0x04000128 RID: 296
		internal RenderFunc<PassData> renderFunc;
	}
}
