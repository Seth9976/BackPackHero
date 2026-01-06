using System;

namespace Pathfinding
{
	// Token: 0x02000092 RID: 146
	public class ABPathEndingCondition : PathEndingCondition
	{
		// Token: 0x06000707 RID: 1799 RVA: 0x0002A875 File Offset: 0x00028A75
		public ABPathEndingCondition(ABPath p)
		{
			if (p == null)
			{
				throw new ArgumentNullException("p");
			}
			this.abPath = p;
			this.path = p;
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0002A899 File Offset: 0x00028A99
		public override bool TargetFound(PathNode node)
		{
			return node.node == this.abPath.endNode;
		}

		// Token: 0x0400040C RID: 1036
		protected ABPath abPath;
	}
}
