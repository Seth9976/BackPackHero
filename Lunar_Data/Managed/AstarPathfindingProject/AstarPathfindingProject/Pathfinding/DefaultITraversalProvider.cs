using System;

namespace Pathfinding
{
	// Token: 0x020000A6 RID: 166
	public static class DefaultITraversalProvider
	{
		// Token: 0x06000525 RID: 1317 RVA: 0x000197C8 File Offset: 0x000179C8
		public static bool CanTraverse(Path path, GraphNode node)
		{
			return node.Walkable && (path == null || ((path.enabledTags >> (int)node.Tag) & 1) != 0);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x000197EE File Offset: 0x000179EE
		public static uint GetTraversalCost(Path path, GraphNode node)
		{
			return node.Penalty + ((path != null) ? path.GetTagPenalty((int)node.Tag) : 0U);
		}
	}
}
