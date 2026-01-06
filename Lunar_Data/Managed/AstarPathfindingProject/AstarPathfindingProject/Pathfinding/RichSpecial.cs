using System;

namespace Pathfinding
{
	// Token: 0x0200001E RID: 30
	public class RichSpecial : RichPathPart
	{
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00008F90 File Offset: 0x00007190
		public FakeTransform first
		{
			get
			{
				return new FakeTransform
				{
					position = this.nodeLink.relativeStart,
					rotation = (this.nodeLink.isReverse ? this.nodeLink.link.end.rotation : this.nodeLink.link.start.rotation)
				};
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00008FF8 File Offset: 0x000071F8
		public FakeTransform second
		{
			get
			{
				return new FakeTransform
				{
					position = this.nodeLink.relativeEnd,
					rotation = (this.nodeLink.isReverse ? this.nodeLink.link.start.rotation : this.nodeLink.link.end.rotation)
				};
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00009060 File Offset: 0x00007260
		public bool reverse
		{
			get
			{
				return this.nodeLink.isReverse;
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000906D File Offset: 0x0000726D
		public override void OnEnterPool()
		{
			this.nodeLink = default(OffMeshLinks.OffMeshLinkTracer);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000907B File Offset: 0x0000727B
		public RichSpecial Initialize(OffMeshLinks.OffMeshLinkTracer nodeLink)
		{
			this.nodeLink = nodeLink;
			return this;
		}

		// Token: 0x04000104 RID: 260
		public OffMeshLinks.OffMeshLinkTracer nodeLink;
	}
}
