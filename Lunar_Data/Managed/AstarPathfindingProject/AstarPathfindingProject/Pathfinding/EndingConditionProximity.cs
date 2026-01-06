using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000136 RID: 310
	public class EndingConditionProximity : ABPathEndingCondition
	{
		// Token: 0x06000959 RID: 2393 RVA: 0x00033728 File Offset: 0x00031928
		public EndingConditionProximity(ABPath p, float maxDistance)
			: base(p)
		{
			this.maxDistance = maxDistance;
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x00033744 File Offset: 0x00031944
		public override bool TargetFound(GraphNode node, uint H, uint G)
		{
			return ((Vector3)node.position - this.abPath.originalEndPoint).sqrMagnitude <= this.maxDistance * this.maxDistance;
		}

		// Token: 0x0400066B RID: 1643
		public float maxDistance = 10f;
	}
}
