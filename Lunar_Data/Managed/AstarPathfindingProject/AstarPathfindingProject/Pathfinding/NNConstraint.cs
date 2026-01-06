using System;

namespace Pathfinding
{
	// Token: 0x02000025 RID: 37
	public class NNConstraint
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00009A1B File Offset: 0x00007C1B
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x00009A3E File Offset: 0x00007C3E
		[Obsolete("Use distanceMetric = DistanceMetric.ClosestAsSeenFromAbove() instead")]
		public bool distanceXZ
		{
			get
			{
				return this.distanceMetric.isProjectedDistance && this.distanceMetric.distanceScaleAlongProjectionDirection == 0f;
			}
			set
			{
				if (value)
				{
					this.distanceMetric = DistanceMetric.ClosestAsSeenFromAbove();
					return;
				}
				this.distanceMetric = DistanceMetric.Euclidean;
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00009A5A File Offset: 0x00007C5A
		public virtual bool SuitableGraph(int graphIndex, NavGraph graph)
		{
			return this.graphMask.Contains(graphIndex);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00009A68 File Offset: 0x00007C68
		public virtual bool Suitable(GraphNode node)
		{
			return (!this.constrainWalkability || node.Walkable == this.walkable) && (!this.constrainArea || this.area < 0 || (ulong)node.Area == (ulong)((long)this.area)) && (!this.constrainTags || ((this.tags >> (int)node.Tag) & 1) != 0);
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00009ACF File Offset: 0x00007CCF
		[Obsolete("Use NNConstraint.Walkable instead. It is equivalent, but the name is more descriptive")]
		public static NNConstraint Default
		{
			get
			{
				return new NNConstraint();
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00009ACF File Offset: 0x00007CCF
		public static NNConstraint Walkable
		{
			get
			{
				return new NNConstraint();
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00009AD6 File Offset: 0x00007CD6
		public static NNConstraint None
		{
			get
			{
				return new NNConstraint
				{
					constrainWalkability = false,
					constrainArea = false,
					constrainTags = false,
					constrainDistance = false,
					graphMask = -1
				};
			}
		}

		// Token: 0x04000132 RID: 306
		public GraphMask graphMask = -1;

		// Token: 0x04000133 RID: 307
		public bool constrainArea;

		// Token: 0x04000134 RID: 308
		public int area = -1;

		// Token: 0x04000135 RID: 309
		public DistanceMetric distanceMetric;

		// Token: 0x04000136 RID: 310
		public bool constrainWalkability = true;

		// Token: 0x04000137 RID: 311
		public bool walkable = true;

		// Token: 0x04000138 RID: 312
		public bool constrainTags = true;

		// Token: 0x04000139 RID: 313
		public int tags = -1;

		// Token: 0x0400013A RID: 314
		public bool constrainDistance = true;
	}
}
