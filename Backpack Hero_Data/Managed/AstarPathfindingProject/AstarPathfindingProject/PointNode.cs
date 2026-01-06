using System;
using Pathfinding.Serialization;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000065 RID: 101
	public class PointNode : GraphNode
	{
		// Token: 0x06000564 RID: 1380 RVA: 0x0001E98C File Offset: 0x0001CB8C
		public void SetPosition(Int3 value)
		{
			this.position = value;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0001E995 File Offset: 0x0001CB95
		public PointNode(AstarPath astar)
			: base(astar)
		{
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0001E99E File Offset: 0x0001CB9E
		public override Vector3 ClosestPointOnNode(Vector3 p)
		{
			return (Vector3)this.position;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0001E9AC File Offset: 0x0001CBAC
		public override void GetConnections(Action<GraphNode> action)
		{
			if (this.connections == null)
			{
				return;
			}
			for (int i = 0; i < this.connections.Length; i++)
			{
				action(this.connections[i].node);
			}
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0001E9EC File Offset: 0x0001CBEC
		public override void ClearConnections(bool alsoReverse)
		{
			if (alsoReverse && this.connections != null)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					this.connections[i].node.RemoveConnection(this);
				}
			}
			this.connections = null;
			AstarPath.active.hierarchicalGraph.AddDirtyNode(this);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001EA48 File Offset: 0x0001CC48
		public override void UpdateRecursiveG(Path path, PathNode pathNode, PathHandler handler)
		{
			pathNode.UpdateG(path);
			handler.heap.Add(pathNode);
			for (int i = 0; i < this.connections.Length; i++)
			{
				GraphNode node = this.connections[i].node;
				PathNode pathNode2 = handler.GetPathNode(node);
				if (pathNode2.parent == pathNode && pathNode2.pathID == handler.PathID)
				{
					node.UpdateRecursiveG(path, pathNode2, handler);
				}
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0001EAB8 File Offset: 0x0001CCB8
		public override bool ContainsConnection(GraphNode node)
		{
			if (this.connections == null)
			{
				return false;
			}
			for (int i = 0; i < this.connections.Length; i++)
			{
				if (this.connections[i].node == node)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0001EAFC File Offset: 0x0001CCFC
		public override void AddConnection(GraphNode node, uint cost)
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
			array[num] = new Connection(node, cost, byte.MaxValue);
			this.connections = array;
			AstarPath.active.hierarchicalGraph.AddDirtyNode(this);
			(base.Graph as PointGraph).RegisterConnectionLength((node.position - this.position).sqrMagnitudeLong);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0001EBE4 File Offset: 0x0001CDE4
		public override void RemoveConnection(GraphNode node)
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

		// Token: 0x0600056D RID: 1389 RVA: 0x0001EC9C File Offset: 0x0001CE9C
		public override void Open(Path path, PathNode pathNode, PathHandler handler)
		{
			if (this.connections == null)
			{
				return;
			}
			for (int i = 0; i < this.connections.Length; i++)
			{
				GraphNode node = this.connections[i].node;
				if (path.CanTraverse(node))
				{
					PathNode pathNode2 = handler.GetPathNode(node);
					if (pathNode2.pathID != handler.PathID)
					{
						pathNode2.parent = pathNode;
						pathNode2.pathID = handler.PathID;
						pathNode2.cost = this.connections[i].cost;
						pathNode2.H = path.CalculateHScore(node);
						pathNode2.UpdateG(path);
						handler.heap.Add(pathNode2);
					}
					else
					{
						uint cost = this.connections[i].cost;
						if (pathNode.G + cost + path.GetTraversalCost(node) < pathNode2.G)
						{
							pathNode2.cost = cost;
							pathNode2.parent = pathNode;
							node.UpdateRecursiveG(path, pathNode2, handler);
						}
					}
				}
			}
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0001ED8C File Offset: 0x0001CF8C
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

		// Token: 0x0600056F RID: 1391 RVA: 0x0001EDD9 File Offset: 0x0001CFD9
		public override void SerializeNode(GraphSerializationContext ctx)
		{
			base.SerializeNode(ctx);
			ctx.SerializeInt3(this.position);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0001EDEE File Offset: 0x0001CFEE
		public override void DeserializeNode(GraphSerializationContext ctx)
		{
			base.DeserializeNode(ctx);
			this.position = ctx.DeserializeInt3();
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0001EE04 File Offset: 0x0001D004
		public override void SerializeReferences(GraphSerializationContext ctx)
		{
			if (this.connections == null)
			{
				ctx.writer.Write(-1);
				return;
			}
			ctx.writer.Write(this.connections.Length);
			for (int i = 0; i < this.connections.Length; i++)
			{
				ctx.SerializeNodeReference(this.connections[i].node);
				ctx.writer.Write(this.connections[i].cost);
			}
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0001EE80 File Offset: 0x0001D080
		public override void DeserializeReferences(GraphSerializationContext ctx)
		{
			int num = ctx.reader.ReadInt32();
			if (num == -1)
			{
				this.connections = null;
				return;
			}
			this.connections = new Connection[num];
			for (int i = 0; i < num; i++)
			{
				this.connections[i] = new Connection(ctx.DeserializeNodeReference(), ctx.reader.ReadUInt32(), byte.MaxValue);
			}
		}

		// Token: 0x0400030F RID: 783
		public Connection[] connections;

		// Token: 0x04000310 RID: 784
		public GameObject gameObject;
	}
}
