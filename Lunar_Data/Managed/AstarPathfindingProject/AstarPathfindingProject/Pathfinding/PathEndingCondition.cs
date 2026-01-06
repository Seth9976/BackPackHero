using System;

namespace Pathfinding
{
	// Token: 0x02000134 RID: 308
	public abstract class PathEndingCondition
	{
		// Token: 0x06000954 RID: 2388 RVA: 0x000033F8 File Offset: 0x000015F8
		protected PathEndingCondition()
		{
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x000336D7 File Offset: 0x000318D7
		public PathEndingCondition(Path p)
		{
			if (p == null)
			{
				throw new ArgumentNullException("p");
			}
			this.path = p;
		}

		// Token: 0x06000956 RID: 2390
		public abstract bool TargetFound(GraphNode node, uint H, uint G);

		// Token: 0x04000669 RID: 1641
		protected Path path;
	}
}
