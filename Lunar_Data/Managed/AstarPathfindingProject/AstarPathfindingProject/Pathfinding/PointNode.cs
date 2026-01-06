using System;
using Pathfinding.Serialization;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000E4 RID: 228
	public class PointNode : GraphNode
	{
		// Token: 0x06000777 RID: 1911 RVA: 0x00026C42 File Offset: 0x00024E42
		public void SetPosition(Int3 value)
		{
			this.position = value;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x000175EF File Offset: 0x000157EF
		public PointNode()
		{
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x000272A0 File Offset: 0x000254A0
		public PointNode(AstarPath astar)
		{
			astar.InitializeNode(this);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x000171D3 File Offset: 0x000153D3
		public override Vector3 ClosestPointOnNode(Vector3 p)
		{
			return (Vector3)this.position;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00018013 File Offset: 0x00016213
		public override bool ContainsPoint(Vector3 point)
		{
			return false;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00018013 File Offset: 0x00016213
		public override bool ContainsPointInGraphSpace(Int3 point)
		{
			return false;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x000272B0 File Offset: 0x000254B0
		public override void GetConnections<T>(GraphNode.GetConnectionsWithData<T> action, ref T data, int connectionFilter)
		{
			if (this.connections == null)
			{
				return;
			}
			for (int i = 0; i < this.connections.Length; i++)
			{
				if (((int)this.connections[i].shapeEdgeInfo & connectionFilter) != 0)
				{
					action(this.connections[i].node, ref data);
				}
			}
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00027308 File Offset: 0x00025508
		public override void ClearConnections(bool alsoReverse)
		{
			if (alsoReverse && this.connections != null)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					this.connections[i].node.RemovePartialConnection(this);
				}
			}
			this.connections = null;
			AstarPath.active.hierarchicalGraph.AddDirtyNode(this);
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00027364 File Offset: 0x00025564
		public override bool ContainsOutgoingConnection(GraphNode node)
		{
			if (this.connections == null)
			{
				return false;
			}
			for (int i = 0; i < this.connections.Length; i++)
			{
				if (this.connections[i].node == node && this.connections[i].isOutgoing)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x000273B8 File Offset: 0x000255B8
		public override void AddPartialConnection(GraphNode node, uint cost, bool isOutgoing, bool isIncoming)
		{
			if (node == null)
			{
				throw new ArgumentNullException();
			}
			if (this.connections != null)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					if (this.connections[i].node == node)
					{
						this.connections[i].cost = cost;
						this.connections[i].shapeEdgeInfo = Connection.PackShapeEdgeInfo(isOutgoing, isIncoming);
						return;
					}
				}
			}
			int num = ((this.connections != null) ? this.connections.Length : 0);
			Connection[] array = new Connection[num + 1];
			for (int j = 0; j < num; j++)
			{
				array[j] = this.connections[j];
			}
			array[num] = new Connection(node, cost, isOutgoing, isIncoming);
			this.connections = array;
			AstarPath.active.hierarchicalGraph.AddDirtyNode(this);
			PointGraph pointGraph = base.Graph as PointGraph;
			if (pointGraph != null)
			{
				pointGraph.RegisterConnectionLength((node.position - this.position).sqrMagnitudeLong);
			}
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x000274C4 File Offset: 0x000256C4
		public override void RemovePartialConnection(GraphNode node)
		{
			if (this.connections == null)
			{
				return;
			}
			for (int i = 0; i < this.connections.Length; i++)
			{
				if (this.connections[i].node == node)
				{
					int num = this.connections.Length;
					Connection[] array = new Connection[num - 1];
					for (int j = 0; j < i; j++)
					{
						array[j] = this.connections[j];
					}
					for (int k = i + 1; k < num; k++)
					{
						array[k - 1] = this.connections[k];
					}
					this.connections = array;
					AstarPath.active.hierarchicalGraph.AddDirtyNode(this);
					return;
				}
			}
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0002757C File Offset: 0x0002577C
		public override void Open(Path path, uint pathNodeIndex, uint gScore)
		{
			path.OpenCandidateConnectionsToEndNode(this.position, pathNodeIndex, pathNodeIndex, gScore);
			if (this.connections == null)
			{
				return;
			}
			for (int i = 0; i < this.connections.Length; i++)
			{
				GraphNode node = this.connections[i].node;
				if (this.connections[i].isOutgoing && path.CanTraverse(this, node))
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

		// Token: 0x06000783 RID: 1923 RVA: 0x00027624 File Offset: 0x00025824
		public override void OpenAtPoint(Path path, uint pathNodeIndex, Int3 pos, uint gScore)
		{
			if (path.CanTraverse(this))
			{
				path.OpenCandidateConnectionsToEndNode(pos, pathNodeIndex, pathNodeIndex, gScore);
				uint costMagnitude = (uint)(pos - this.position).costMagnitude;
				path.OpenCandidateConnection(pathNodeIndex, base.NodeIndex, gScore, costMagnitude, 0U, this.position);
			}
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00027674 File Offset: 0x00025874
		public override int GetGizmoHashCode()
		{
			int num = base.GetGizmoHashCode();
			if (this.connections != null)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					num ^= 17 * this.connections[i].GetHashCode();
				}
			}
			return num;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x000276C1 File Offset: 0x000258C1
		public override void SerializeNode(GraphSerializationContext ctx)
		{
			base.SerializeNode(ctx);
			ctx.SerializeInt3(this.position);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x000276D6 File Offset: 0x000258D6
		public override void DeserializeNode(GraphSerializationContext ctx)
		{
			base.DeserializeNode(ctx);
			this.position = ctx.DeserializeInt3();
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x000276EB File Offset: 0x000258EB
		public override void SerializeReferences(GraphSerializationContext ctx)
		{
			ctx.SerializeConnections(this.connections, true);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x000276FA File Offset: 0x000258FA
		public override void DeserializeReferences(GraphSerializationContext ctx)
		{
			this.connections = ctx.DeserializeConnections(ctx.meta.version >= AstarSerializer.V4_3_85);
		}

		// Token: 0x040004D0 RID: 1232
		public Connection[] connections;

		// Token: 0x040004D1 RID: 1233
		public GameObject gameObject;
	}
}
