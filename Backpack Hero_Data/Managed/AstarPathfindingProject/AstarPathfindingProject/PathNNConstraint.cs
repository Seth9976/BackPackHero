using System;

namespace Pathfinding
{
	// Token: 0x02000013 RID: 19
	public class PathNNConstraint : NNConstraint
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600019B RID: 411 RVA: 0x000086D3 File Offset: 0x000068D3
		public new static PathNNConstraint Default
		{
			get
			{
				return new PathNNConstraint
				{
					constrainArea = true
				};
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000086E1 File Offset: 0x000068E1
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
