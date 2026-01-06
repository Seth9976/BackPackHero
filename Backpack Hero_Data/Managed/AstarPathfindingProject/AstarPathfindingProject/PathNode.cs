using System;

namespace Pathfinding
{
	// Token: 0x02000052 RID: 82
	public class PathNode
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x000148BA File Offset: 0x00012ABA
		// (set) Token: 0x06000405 RID: 1029 RVA: 0x000148C8 File Offset: 0x00012AC8
		public uint cost
		{
			get
			{
				return this.flags & 268435455U;
			}
			set
			{
				this.flags = (this.flags & 4026531840U) | value;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x000148DE File Offset: 0x00012ADE
		// (set) Token: 0x06000407 RID: 1031 RVA: 0x000148EF File Offset: 0x00012AEF
		public bool flag1
		{
			get
			{
				return (this.flags & 268435456U) > 0U;
			}
			set
			{
				this.flags = (this.flags & 4026531839U) | (value ? 268435456U : 0U);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x0001490F File Offset: 0x00012B0F
		// (set) Token: 0x06000409 RID: 1033 RVA: 0x00014920 File Offset: 0x00012B20
		public bool flag2
		{
			get
			{
				return (this.flags & 536870912U) > 0U;
			}
			set
			{
				this.flags = (this.flags & 3758096383U) | (value ? 536870912U : 0U);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x00014940 File Offset: 0x00012B40
		// (set) Token: 0x0600040B RID: 1035 RVA: 0x00014948 File Offset: 0x00012B48
		public uint G
		{
			get
			{
				return this.g;
			}
			set
			{
				this.g = value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x00014951 File Offset: 0x00012B51
		// (set) Token: 0x0600040D RID: 1037 RVA: 0x00014959 File Offset: 0x00012B59
		public uint H
		{
			get
			{
				return this.h;
			}
			set
			{
				this.h = value;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x00014962 File Offset: 0x00012B62
		public uint F
		{
			get
			{
				return this.g + this.h;
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00014971 File Offset: 0x00012B71
		public void UpdateG(Path path)
		{
			this.g = this.parent.g + this.cost + path.GetTraversalCost(this.node);
		}

		// Token: 0x04000263 RID: 611
		public GraphNode node;

		// Token: 0x04000264 RID: 612
		public PathNode parent;

		// Token: 0x04000265 RID: 613
		public ushort pathID;

		// Token: 0x04000266 RID: 614
		public ushort heapIndex = ushort.MaxValue;

		// Token: 0x04000267 RID: 615
		private uint flags;

		// Token: 0x04000268 RID: 616
		private const uint CostMask = 268435455U;

		// Token: 0x04000269 RID: 617
		private const int Flag1Offset = 28;

		// Token: 0x0400026A RID: 618
		private const uint Flag1Mask = 268435456U;

		// Token: 0x0400026B RID: 619
		private const int Flag2Offset = 29;

		// Token: 0x0400026C RID: 620
		private const uint Flag2Mask = 536870912U;

		// Token: 0x0400026D RID: 621
		private uint g;

		// Token: 0x0400026E RID: 622
		private uint h;
	}
}
