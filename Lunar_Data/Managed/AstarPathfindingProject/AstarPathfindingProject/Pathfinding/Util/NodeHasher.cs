using System;
using Pathfinding.Drawing;

namespace Pathfinding.Util
{
	// Token: 0x02000275 RID: 629
	public struct NodeHasher
	{
		// Token: 0x06000EFD RID: 3837 RVA: 0x0005C7A0 File Offset: 0x0005A9A0
		public NodeHasher(AstarPath active)
		{
			this.hasher = default(DrawingData.Hasher);
			this.debugData = active.debugPathData;
			this.includePathSearchInfo = this.debugData != null && (active.debugMode == GraphDebugMode.F || active.debugMode == GraphDebugMode.G || active.debugMode == GraphDebugMode.H || active.showSearchTree);
			this.includeAreaInfo = active.debugMode == GraphDebugMode.Areas;
			this.includeHierarchicalNodeInfo = active.debugMode == GraphDebugMode.HierarchicalNode;
			this.hasher.Add<GraphDebugMode>(active.debugMode);
			this.hasher.Add<float>(active.debugFloor);
			this.hasher.Add<float>(active.debugRoof);
			this.hasher.Add<int>(AstarColor.ColorHash());
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x0005C85C File Offset: 0x0005AA5C
		public unsafe void HashNode(GraphNode node)
		{
			this.hasher.Add<int>(node.GetGizmoHashCode());
			if (this.includeAreaInfo)
			{
				this.hasher.Add<int>((int)node.Area);
			}
			if (this.includeHierarchicalNodeInfo)
			{
				this.hasher.Add<int>(node.HierarchicalNodeIndex);
			}
			if (this.includePathSearchInfo)
			{
				PathNode pathNode = *this.debugData.pathNodes[node.NodeIndex];
				this.hasher.Add<ushort>(pathNode.pathID);
				this.hasher.Add<bool>(pathNode.pathID == this.debugData.PathID);
			}
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x0005C8FF File Offset: 0x0005AAFF
		public void Add<T>(T v)
		{
			this.hasher.Add<T>(v);
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0005C90D File Offset: 0x0005AB0D
		public static implicit operator DrawingData.Hasher(NodeHasher hasher)
		{
			return hasher.hasher;
		}

		// Token: 0x04000B31 RID: 2865
		private readonly bool includePathSearchInfo;

		// Token: 0x04000B32 RID: 2866
		private readonly bool includeAreaInfo;

		// Token: 0x04000B33 RID: 2867
		private readonly bool includeHierarchicalNodeInfo;

		// Token: 0x04000B34 RID: 2868
		private readonly PathHandler debugData;

		// Token: 0x04000B35 RID: 2869
		public DrawingData.Hasher hasher;
	}
}
