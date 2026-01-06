using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000093 RID: 147
	public class EndingConditionProximity : ABPathEndingCondition
	{
		// Token: 0x06000709 RID: 1801 RVA: 0x0002A8AE File Offset: 0x00028AAE
		public EndingConditionProximity(ABPath p, float maxDistance)
			: base(p)
		{
			this.maxDistance = maxDistance;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0002A8CC File Offset: 0x00028ACC
		public override bool TargetFound(PathNode node)
		{
			return ((Vector3)node.node.position - this.abPath.originalEndPoint).sqrMagnitude <= this.maxDistance * this.maxDistance;
		}

		// Token: 0x0400040D RID: 1037
		public float maxDistance = 10f;
	}
}
