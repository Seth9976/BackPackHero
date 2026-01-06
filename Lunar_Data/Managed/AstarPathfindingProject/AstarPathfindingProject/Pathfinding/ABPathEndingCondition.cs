using System;

namespace Pathfinding
{
	// Token: 0x02000135 RID: 309
	public class ABPathEndingCondition : PathEndingCondition
	{
		// Token: 0x06000957 RID: 2391 RVA: 0x000336F4 File Offset: 0x000318F4
		public ABPathEndingCondition(ABPath p)
		{
			if (p == null)
			{
				throw new ArgumentNullException("p");
			}
			this.abPath = p;
			this.path = p;
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00033718 File Offset: 0x00031918
		public override bool TargetFound(GraphNode node, uint H, uint G)
		{
			return node == this.abPath.endNode;
		}

		// Token: 0x0400066A RID: 1642
		protected ABPath abPath;
	}
}
