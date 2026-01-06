using System;

namespace Pathfinding
{
	// Token: 0x02000091 RID: 145
	public abstract class PathEndingCondition
	{
		// Token: 0x06000704 RID: 1796 RVA: 0x0002A850 File Offset: 0x00028A50
		protected PathEndingCondition()
		{
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0002A858 File Offset: 0x00028A58
		public PathEndingCondition(Path p)
		{
			if (p == null)
			{
				throw new ArgumentNullException("p");
			}
			this.path = p;
		}

		// Token: 0x06000706 RID: 1798
		public abstract bool TargetFound(PathNode node);

		// Token: 0x0400040B RID: 1035
		protected Path path;
	}
}
