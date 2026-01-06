using System;

namespace Pathfinding
{
	// Token: 0x02000089 RID: 137
	public class EndingConditionDistance : PathEndingCondition
	{
		// Token: 0x060006C3 RID: 1731 RVA: 0x00028CF9 File Offset: 0x00026EF9
		public EndingConditionDistance(Path p, int maxGScore)
			: base(p)
		{
			this.maxGScore = maxGScore;
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00028D11 File Offset: 0x00026F11
		public override bool TargetFound(PathNode node)
		{
			return (ulong)node.G >= (ulong)((long)this.maxGScore);
		}

		// Token: 0x040003EC RID: 1004
		public int maxGScore = 100;
	}
}
