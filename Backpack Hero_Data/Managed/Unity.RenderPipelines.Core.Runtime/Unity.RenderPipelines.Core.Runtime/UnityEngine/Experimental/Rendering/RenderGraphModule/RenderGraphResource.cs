using System;
using System.Diagnostics;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x02000038 RID: 56
	[DebuggerDisplay("Resource ({GetType().Name}:{GetName()})")]
	internal abstract class RenderGraphResource<DescType, ResType> : IRenderGraphResource where DescType : struct where ResType : class
	{
		// Token: 0x06000224 RID: 548 RVA: 0x0000BCAE File Offset: 0x00009EAE
		public override void Reset(IRenderGraphResourcePool pool)
		{
			base.Reset(pool);
			this.graphicsResource = default(ResType);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000BCC3 File Offset: 0x00009EC3
		public override bool IsCreated()
		{
			return this.graphicsResource != null;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000BCD3 File Offset: 0x00009ED3
		public override void ReleaseGraphicsResource()
		{
			this.graphicsResource = default(ResType);
		}

		// Token: 0x0400015C RID: 348
		public DescType desc;

		// Token: 0x0400015D RID: 349
		public ResType graphicsResource;
	}
}
