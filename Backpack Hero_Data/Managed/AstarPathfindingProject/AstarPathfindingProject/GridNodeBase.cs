using System;
using Pathfinding.Serialization;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000064 RID: 100
	public abstract class GridNodeBase : GraphNode
	{
		// Token: 0x06000549 RID: 1353 RVA: 0x0001E286 File Offset: 0x0001C486
		protected GridNodeBase(AstarPath astar)
			: base(astar)
		{
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x0001E28F File Offset: 0x0001C48F
		// (set) Token: 0x0600054B RID: 1355 RVA: 0x0001E29D File Offset: 0x0001C49D
		public int NodeInGridIndex
		{
			get
			{
				return this.nodeInGridIndex & 16777215;
			}
			set
			{
				this.nodeInGridIndex = (this.nodeInGridIndex & -16777216) | value;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x0001E2B3 File Offset: 0x0001C4B3
		public int XCoordinateInGrid
		{
			get
			{
				return this.NodeInGridIndex % GridNode.GetGridGraph(base.GraphIndex).width;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x0001E2CC File Offset: 0x0001C4CC
		public int ZCoordinateInGrid
		{
			get
			{
				return this.NodeInGridIndex / GridNode.GetGridGraph(base.GraphIndex).width;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x0001E2E5 File Offset: 0x0001C4E5
		// (set) Token: 0x0600054F RID: 1359 RVA: 0x0001E2F6 File Offset: 0x0001C4F6
		public bool WalkableErosion
		{
			get
			{
				return (this.gridFlags & 256) > 0;
			}
			set
			{
				this.gridFlags = (ushort)(((int)this.gridFlags & -257) | (value ? 256 : 0));
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x0001E317 File Offset: 0x0001C517
		// (set) Token: 0x06000551 RID: 1361 RVA: 0x0001E328 File Offset: 0x0001C528
		public bool TmpWalkable
		{
			get
			{
				return (this.gridFlags & 512) > 0;
			}
			set
			{
				this.gridFlags = (ushort)(((int)this.gridFlags & -513) | (value ? 512 : 0));
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000552 RID: 1362
		public abstract bool HasConnectionsToAllEightNeighbours { get; }

		// Token: 0x06000553 RID: 1363 RVA: 0x0001E34C File Offset: 0x0001C54C
		public override float SurfaceArea()
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			return gridGraph.nodeSize * gridGraph.nodeSize;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0001E374 File Offset: 0x0001C574
		public override Vector3 RandomPointOnSurface()
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			Vector3 vector = gridGraph.transform.InverseTransform((Vector3)this.position);
			return gridGraph.transform.Transform(vector + new Vector3(Random.value - 0.5f, 0f, Random.value - 0.5f));
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0001E3D4 File Offset: 0x0001C5D4
		public Vector2 NormalizePoint(Vector3 worldPoint)
		{
			Vector3 vector = GridNode.GetGridGraph(base.GraphIndex).transform.InverseTransform(worldPoint);
			return new Vector2(vector.x - (float)this.XCoordinateInGrid, vector.z - (float)this.ZCoordinateInGrid);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0001E41C File Offset: 0x0001C61C
		public Vector3 UnNormalizePoint(Vector2 normalizedPointOnSurface)
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			return (Vector3)this.position + gridGraph.transform.TransformVector(new Vector3(normalizedPointOnSurface.x - 0.5f, 0f, normalizedPointOnSurface.y - 0.5f));
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0001E474 File Offset: 0x0001C674
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
			return num ^ (int)(109 * this.gridFlags);
		}

		// Token: 0x06000558 RID: 1368
		public abstract GridNodeBase GetNeighbourAlongDirection(int direction);

		// Token: 0x06000559 RID: 1369 RVA: 0x0001E4CD File Offset: 0x0001C6CD
		public virtual bool HasConnectionInDirection(int direction)
		{
			return this.GetNeighbourAlongDirection(direction) != null;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0001E4DC File Offset: 0x0001C6DC
		public override bool ContainsConnection(GraphNode node)
		{
			if (this.connections != null)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					if (this.connections[i].node == node)
					{
						return true;
					}
				}
			}
			for (int j = 0; j < 8; j++)
			{
				if (node == this.GetNeighbourAlongDirection(j))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0001E534 File Offset: 0x0001C734
		public void ClearCustomConnections(bool alsoReverse)
		{
			if (this.connections != null)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					this.connections[i].node.RemoveConnection(this);
				}
			}
			this.connections = null;
			AstarPath.active.hierarchicalGraph.AddDirtyNode(this);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0001E58A File Offset: 0x0001C78A
		public override void ClearConnections(bool alsoReverse)
		{
			this.ClearCustomConnections(alsoReverse);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0001E594 File Offset: 0x0001C794
		public override void GetConnections(Action<GraphNode> action)
		{
			if (this.connections != null)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					action(this.connections[i].node);
				}
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0001E5D4 File Offset: 0x0001C7D4
		public override void UpdateRecursiveG(Path path, PathNode pathNode, PathHandler handler)
		{
			ushort pathID = handler.PathID;
			if (this.connections != null)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					GraphNode node = this.connections[i].node;
					PathNode pathNode2 = handler.GetPathNode(node);
					if (pathNode2.parent == pathNode && pathNode2.pathID == pathID)
					{
						node.UpdateRecursiveG(path, pathNode2, handler);
					}
				}
			}
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0001E638 File Offset: 0x0001C838
		public override void Open(Path path, PathNode pathNode, PathHandler handler)
		{
			ushort pathID = handler.PathID;
			if (this.connections != null)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					GraphNode node = this.connections[i].node;
					if (path.CanTraverse(node))
					{
						PathNode pathNode2 = handler.GetPathNode(node);
						uint cost = this.connections[i].cost;
						if (pathNode2.pathID != pathID)
						{
							pathNode2.parent = pathNode;
							pathNode2.pathID = pathID;
							pathNode2.cost = cost;
							pathNode2.H = path.CalculateHScore(node);
							pathNode2.UpdateG(path);
							handler.heap.Add(pathNode2);
						}
						else if (pathNode.G + cost + path.GetTraversalCost(node) < pathNode2.G)
						{
							pathNode2.cost = cost;
							pathNode2.parent = pathNode;
							node.UpdateRecursiveG(path, pathNode2, handler);
						}
					}
				}
			}
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0001E71C File Offset: 0x0001C91C
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
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0001E7DC File Offset: 0x0001C9DC
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

		// Token: 0x06000562 RID: 1378 RVA: 0x0001E894 File Offset: 0x0001CA94
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

		// Token: 0x06000563 RID: 1379 RVA: 0x0001E910 File Offset: 0x0001CB10
		public override void DeserializeReferences(GraphSerializationContext ctx)
		{
			if (ctx.meta.version < AstarSerializer.V3_8_3)
			{
				return;
			}
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

		// Token: 0x04000306 RID: 774
		private const int GridFlagsWalkableErosionOffset = 8;

		// Token: 0x04000307 RID: 775
		private const int GridFlagsWalkableErosionMask = 256;

		// Token: 0x04000308 RID: 776
		private const int GridFlagsWalkableTmpOffset = 9;

		// Token: 0x04000309 RID: 777
		private const int GridFlagsWalkableTmpMask = 512;

		// Token: 0x0400030A RID: 778
		protected const int NodeInGridIndexLayerOffset = 24;

		// Token: 0x0400030B RID: 779
		protected const int NodeInGridIndexMask = 16777215;

		// Token: 0x0400030C RID: 780
		protected int nodeInGridIndex;

		// Token: 0x0400030D RID: 781
		protected ushort gridFlags;

		// Token: 0x0400030E RID: 782
		public Connection[] connections;
	}
}
