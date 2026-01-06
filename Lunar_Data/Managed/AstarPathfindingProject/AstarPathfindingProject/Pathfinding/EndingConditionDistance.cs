using System;

namespace Pathfinding
{
	// Token: 0x0200012C RID: 300
	public class EndingConditionDistance : PathEndingCondition
	{
		// Token: 0x06000917 RID: 2327 RVA: 0x0003225E File Offset: 0x0003045E
		public EndingConditionDistance(Path p, int maxGScore)
			: base(p)
		{
			this.maxGScore = maxGScore;
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00032276 File Offset: 0x00030476
		public override bool TargetFound(GraphNode node, uint H, uint G)
		{
			return G >= (uint)this.maxGScore;
		}

		// Token: 0x04000649 RID: 1609
		public int maxGScore = 100;
	}
}
