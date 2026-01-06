using System;

namespace Pathfinding
{
	// Token: 0x02000026 RID: 38
	public class PathNNConstraint : NNConstraint
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00009B43 File Offset: 0x00007D43
		public new static PathNNConstraint Walkable
		{
			get
			{
				return new PathNNConstraint
				{
					constrainArea = true
				};
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00009B51 File Offset: 0x00007D51
		public virtual void SetStart(GraphNode node)
		{
			if (node != null)
			{
				this.area = (int)node.Area;
				return;
			}
			this.constrainArea = false;
		}
	}
}
