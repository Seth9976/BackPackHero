using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200007C RID: 124
	public class NodeLink3Node : PointNode
	{
		// Token: 0x06000401 RID: 1025 RVA: 0x00014D29 File Offset: 0x00012F29
		public NodeLink3Node(AstarPath astar)
		{
			astar.InitializeNode(this);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00014D38 File Offset: 0x00012F38
		public override bool GetPortal(GraphNode other, out Vector3 left, out Vector3 right)
		{
			left = this.portalA;
			right = this.portalB;
			if (this.connections.Length < 2)
			{
				return false;
			}
			if (this.connections.Length != 2)
			{
				throw new Exception("Invalid NodeLink3Node. Expected 2 connections, found " + this.connections.Length.ToString());
			}
			return true;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00014D98 File Offset: 0x00012F98
		public GraphNode GetOther(GraphNode a)
		{
			if (this.connections.Length < 2)
			{
				return null;
			}
			if (this.connections.Length != 2)
			{
				throw new Exception("Invalid NodeLink3Node. Expected 2 connections, found " + this.connections.Length.ToString());
			}
			if (a != this.connections[0].node)
			{
				return (this.connections[0].node as NodeLink3Node).GetOtherInternal(this);
			}
			return (this.connections[1].node as NodeLink3Node).GetOtherInternal(this);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00014E2C File Offset: 0x0001302C
		private GraphNode GetOtherInternal(GraphNode a)
		{
			if (this.connections.Length < 2)
			{
				return null;
			}
			if (a != this.connections[0].node)
			{
				return this.connections[0].node;
			}
			return this.connections[1].node;
		}

		// Token: 0x040002A7 RID: 679
		public NodeLink3 link;

		// Token: 0x040002A8 RID: 680
		public Vector3 portalA;

		// Token: 0x040002A9 RID: 681
		public Vector3 portalB;
	}
}
