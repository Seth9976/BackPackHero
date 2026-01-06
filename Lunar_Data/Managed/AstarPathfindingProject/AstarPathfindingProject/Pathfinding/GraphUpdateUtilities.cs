using System;
using System.Collections.Generic;
using Pathfinding.Util;

namespace Pathfinding
{
	// Token: 0x02000149 RID: 329
	public static class GraphUpdateUtilities
	{
		// Token: 0x060009BD RID: 2493 RVA: 0x00035C54 File Offset: 0x00033E54
		public static bool UpdateGraphsNoBlock(GraphUpdateObject guo, GraphNode node1, GraphNode node2, bool alwaysRevert = false)
		{
			List<GraphNode> list = ListPool<GraphNode>.Claim();
			list.Add(node1);
			list.Add(node2);
			bool flag = GraphUpdateUtilities.UpdateGraphsNoBlock(guo, list, alwaysRevert);
			ListPool<GraphNode>.Release(ref list);
			return flag;
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00035C84 File Offset: 0x00033E84
		public static bool UpdateGraphsNoBlock(GraphUpdateObject guo, List<GraphNode> nodes, bool alwaysRevert = false)
		{
			PathProcessor.GraphUpdateLock graphUpdateLock = AstarPath.active.PausePathfinding();
			bool flag;
			try
			{
				AstarPath.active.FlushGraphUpdates();
				for (int i = 0; i < nodes.Count; i++)
				{
					if (!nodes[i].Walkable)
					{
						return false;
					}
				}
				GraphSnapshot graphSnapshot = AstarPath.active.Snapshot(guo.bounds, guo.nnConstraint.graphMask);
				AstarPath.active.UpdateGraphs(guo);
				AstarPath.active.FlushGraphUpdates();
				flag = PathUtilities.IsPathPossible(nodes);
				if (!flag || alwaysRevert)
				{
					AstarPath.active.AddWorkItem(new Action<IWorkItemContext>(graphSnapshot.Restore));
					AstarPath.active.FlushWorkItems();
				}
				graphSnapshot.Dispose();
			}
			finally
			{
				graphUpdateLock.Release();
			}
			return flag;
		}
	}
}
