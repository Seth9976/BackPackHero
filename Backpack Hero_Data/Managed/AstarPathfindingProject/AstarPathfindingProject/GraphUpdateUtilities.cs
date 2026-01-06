using System;
using System.Collections.Generic;
using Pathfinding.Util;

namespace Pathfinding
{
	// Token: 0x02000099 RID: 153
	public static class GraphUpdateUtilities
	{
		// Token: 0x06000739 RID: 1849 RVA: 0x0002C188 File Offset: 0x0002A388
		public static bool UpdateGraphsNoBlock(GraphUpdateObject guo, GraphNode node1, GraphNode node2, bool alwaysRevert = false)
		{
			List<GraphNode> list = ListPool<GraphNode>.Claim();
			list.Add(node1);
			list.Add(node2);
			bool flag = GraphUpdateUtilities.UpdateGraphsNoBlock(guo, list, alwaysRevert);
			ListPool<GraphNode>.Release(ref list);
			return flag;
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0002C1B8 File Offset: 0x0002A3B8
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
				guo.trackChangedNodes = true;
				AstarPath.active.UpdateGraphs(guo);
				AstarPath.active.FlushGraphUpdates();
				flag = PathUtilities.IsPathPossible(nodes);
				if (!flag || alwaysRevert)
				{
					guo.RevertFromBackup();
					AstarPath.active.hierarchicalGraph.RecalculateIfNecessary();
				}
			}
			finally
			{
				graphUpdateLock.Release();
			}
			guo.trackChangedNodes = false;
			return flag;
		}
	}
}
