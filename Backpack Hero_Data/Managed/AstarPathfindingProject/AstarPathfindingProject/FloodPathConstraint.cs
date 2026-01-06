using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200008C RID: 140
	public class FloodPathConstraint : NNConstraint
	{
		// Token: 0x060006D4 RID: 1748 RVA: 0x000290C5 File Offset: 0x000272C5
		public FloodPathConstraint(FloodPath path)
		{
			if (path == null)
			{
				Debug.LogWarning("FloodPathConstraint should not be used with a NULL path");
			}
			this.path = path;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x000290E1 File Offset: 0x000272E1
		public override bool Suitable(GraphNode node)
		{
			return base.Suitable(node) && this.path.HasPathTo(node);
		}

		// Token: 0x040003F2 RID: 1010
		private readonly FloodPath path;
	}
}
