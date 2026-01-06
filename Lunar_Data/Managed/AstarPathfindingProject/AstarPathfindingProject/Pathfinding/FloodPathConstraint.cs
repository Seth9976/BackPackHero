using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200012F RID: 303
	public class FloodPathConstraint : NNConstraint
	{
		// Token: 0x06000929 RID: 2345 RVA: 0x0003258C File Offset: 0x0003078C
		public FloodPathConstraint(FloodPath path)
		{
			if (path == null)
			{
				Debug.LogWarning("FloodPathConstraint should not be used with a NULL path");
			}
			this.path = path;
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x000325A8 File Offset: 0x000307A8
		public override bool Suitable(GraphNode node)
		{
			return base.Suitable(node) && this.path.HasPathTo(node);
		}

		// Token: 0x04000651 RID: 1617
		private readonly FloodPath path;
	}
}
