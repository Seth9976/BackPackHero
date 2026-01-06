using System;

namespace Pathfinding
{
	// Token: 0x020000CB RID: 203
	public class LinkNode : PointNode
	{
		// Token: 0x0600066B RID: 1643 RVA: 0x00021CE8 File Offset: 0x0001FEE8
		public LinkNode()
		{
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00021CF0 File Offset: 0x0001FEF0
		public LinkNode(AstarPath active)
			: base(active)
		{
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00021CF9 File Offset: 0x0001FEF9
		public override void RemovePartialConnection(GraphNode node)
		{
			this.linkConcrete.staleConnections = true;
			AstarPath.active.offMeshLinks.DirtyNoSchedule(this.linkSource);
			base.RemovePartialConnection(node);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00021D24 File Offset: 0x0001FF24
		public unsafe override void Open(Path path, uint pathNodeIndex, uint gScore)
		{
			if (this.connections == null)
			{
				return;
			}
			PathHandler pathHandler = ((IPathInternals)path).PathHandler;
			PathNode pathNode = *pathHandler.pathNodes[pathNodeIndex];
			bool flag = !pathHandler.IsTemporaryNode(pathNode.parentIndex) && pathHandler.GetNode(pathNode.parentIndex).GraphIndex == base.GraphIndex;
			for (int i = 0; i < this.connections.Length; i++)
			{
				GraphNode node = this.connections[i].node;
				if (flag == (node.GraphIndex != base.GraphIndex) && path.CanTraverse(this, node))
				{
					if (node is PointNode)
					{
						path.OpenCandidateConnection(pathNodeIndex, node.NodeIndex, gScore, this.connections[i].cost, 0U, node.position);
					}
					else
					{
						node.OpenAtPoint(path, pathNodeIndex, this.position, gScore);
					}
				}
			}
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00021E0C File Offset: 0x0002000C
		public override void OpenAtPoint(Path path, uint pathNodeIndex, Int3 pos, uint gScore)
		{
			if (path.CanTraverse(this))
			{
				uint costMagnitude = (uint)(pos - this.position).costMagnitude;
				path.OpenCandidateConnection(pathNodeIndex, base.NodeIndex, gScore, costMagnitude, 0U, this.position);
			}
		}

		// Token: 0x0400045B RID: 1115
		public OffMeshLinks.OffMeshLinkSource linkSource;

		// Token: 0x0400045C RID: 1116
		public OffMeshLinks.OffMeshLinkConcrete linkConcrete;

		// Token: 0x0400045D RID: 1117
		public int nodeInGraphIndex;
	}
}
