using System;
using Pathfinding.Serialization;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000092 RID: 146
	public abstract class MeshNode : GraphNode
	{
		// Token: 0x060004B0 RID: 1200
		public abstract Int3 GetVertex(int i);

		// Token: 0x060004B1 RID: 1201
		public abstract int GetVertexCount();

		// Token: 0x060004B2 RID: 1202
		public abstract Vector3 ClosestPointOnNodeXZ(Vector3 p);

		// Token: 0x060004B3 RID: 1203 RVA: 0x000172BC File Offset: 0x000154BC
		public override void ClearConnections(bool alsoReverse = true)
		{
			if (alsoReverse && this.connections != null)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					this.connections[i].node.RemovePartialConnection(this);
				}
			}
			ArrayPool<Connection>.Release(ref this.connections, true);
			AstarPath.active.hierarchicalGraph.AddDirtyNode(this);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001731C File Offset: 0x0001551C
		public override void GetConnections<T>(GraphNode.GetConnectionsWithData<T> action, ref T data, int connectionFilter = 32)
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

		// Token: 0x060004B5 RID: 1205 RVA: 0x00017374 File Offset: 0x00015574
		public override bool ContainsOutgoingConnection(GraphNode node)
		{
			if (this.connections != null)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					if (this.connections[i].node == node && this.connections[i].isOutgoing)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x000173C6 File Offset: 0x000155C6
		public override void AddPartialConnection(GraphNode node, uint cost, bool isOutgoing, bool isIncoming)
		{
			this.AddPartialConnection(node, cost, Connection.PackShapeEdgeInfo(isOutgoing, isIncoming));
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x000173D8 File Offset: 0x000155D8
		public void AddPartialConnection(GraphNode node, uint cost, byte shapeEdgeInfo)
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
						this.connections[i].shapeEdgeInfo = shapeEdgeInfo;
						return;
					}
				}
			}
			int num = ((this.connections != null) ? this.connections.Length : 0);
			Connection[] array = ArrayPool<Connection>.ClaimWithExactLength(num + 1);
			for (int j = 0; j < num; j++)
			{
				array[j] = this.connections[j];
			}
			array[num] = new Connection(node, cost, shapeEdgeInfo);
			if (this.connections != null)
			{
				ArrayPool<Connection>.Release(ref this.connections, true);
			}
			this.connections = array;
			AstarPath.active.hierarchicalGraph.AddDirtyNode(this);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000174B8 File Offset: 0x000156B8
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
					Connection[] array = ArrayPool<Connection>.ClaimWithExactLength(num - 1);
					for (int j = 0; j < i; j++)
					{
						array[j] = this.connections[j];
					}
					for (int k = i + 1; k < num; k++)
					{
						array[k - 1] = this.connections[k];
					}
					if (this.connections != null)
					{
						ArrayPool<Connection>.Release(ref this.connections, true);
					}
					this.connections = array;
					AstarPath.active.hierarchicalGraph.AddDirtyNode(this);
					return;
				}
			}
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00017584 File Offset: 0x00015784
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

		// Token: 0x060004BA RID: 1210 RVA: 0x000175D1 File Offset: 0x000157D1
		public override void SerializeReferences(GraphSerializationContext ctx)
		{
			ctx.SerializeConnections(this.connections, true);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x000175E0 File Offset: 0x000157E0
		public override void DeserializeReferences(GraphSerializationContext ctx)
		{
			this.connections = ctx.DeserializeConnections(true);
		}

		// Token: 0x0400031B RID: 795
		public Connection[] connections;
	}
}
