using System;

namespace Pathfinding.Util
{
	// Token: 0x0200025E RID: 606
	public struct PathPartWithLinkInfo
	{
		// Token: 0x06000E6C RID: 3692 RVA: 0x0005A2AD File Offset: 0x000584AD
		public PathPartWithLinkInfo(int startIndex, int endIndex, OffMeshLinks.OffMeshLinkTracer linkInfo = default(OffMeshLinks.OffMeshLinkTracer))
		{
			this.startIndex = startIndex;
			this.endIndex = endIndex;
			this.linkInfo = linkInfo;
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000E6D RID: 3693 RVA: 0x0005A2C4 File Offset: 0x000584C4
		public Funnel.PartType type
		{
			get
			{
				if (this.linkInfo.link == null)
				{
					return Funnel.PartType.NodeSequence;
				}
				return Funnel.PartType.OffMeshLink;
			}
		}

		// Token: 0x04000AEE RID: 2798
		public int startIndex;

		// Token: 0x04000AEF RID: 2799
		public int endIndex;

		// Token: 0x04000AF0 RID: 2800
		public OffMeshLinks.OffMeshLinkTracer linkInfo;
	}
}
