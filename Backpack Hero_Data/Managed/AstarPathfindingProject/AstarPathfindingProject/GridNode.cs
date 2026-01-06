using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000063 RID: 99
	public class GridNode : GridNodeBase
	{
		// Token: 0x0600052E RID: 1326 RVA: 0x0001DA9C File Offset: 0x0001BC9C
		public GridNode(AstarPath astar)
			: base(astar)
		{
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0001DAA5 File Offset: 0x0001BCA5
		public static GridGraph GetGridGraph(uint graphIndex)
		{
			return GridNode._gridGraphs[(int)graphIndex];
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001DAB0 File Offset: 0x0001BCB0
		public static void SetGridGraph(int graphIndex, GridGraph graph)
		{
			if (GridNode._gridGraphs.Length <= graphIndex)
			{
				GridGraph[] array = new GridGraph[graphIndex + 1];
				for (int i = 0; i < GridNode._gridGraphs.Length; i++)
				{
					array[i] = GridNode._gridGraphs[i];
				}
				GridNode._gridGraphs = array;
			}
			GridNode._gridGraphs[graphIndex] = graph;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0001DAFA File Offset: 0x0001BCFA
		public static void ClearGridGraph(int graphIndex, GridGraph graph)
		{
			if (graphIndex < GridNode._gridGraphs.Length && GridNode._gridGraphs[graphIndex] == graph)
			{
				GridNode._gridGraphs[graphIndex] = null;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0001DB18 File Offset: 0x0001BD18
		// (set) Token: 0x06000533 RID: 1331 RVA: 0x0001DB20 File Offset: 0x0001BD20
		internal ushort InternalGridFlags
		{
			get
			{
				return this.gridFlags;
			}
			set
			{
				this.gridFlags = value;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0001DB29 File Offset: 0x0001BD29
		public override bool HasConnectionsToAllEightNeighbours
		{
			get
			{
				return (this.InternalGridFlags & 255) == 255;
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0001DB3E File Offset: 0x0001BD3E
		public override bool HasConnectionInDirection(int dir)
		{
			return ((this.gridFlags >> dir) & 1) != 0;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0001DB50 File Offset: 0x0001BD50
		[Obsolete("Use HasConnectionInDirection")]
		public bool GetConnectionInternal(int dir)
		{
			return this.HasConnectionInDirection(dir);
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0001DB59 File Offset: 0x0001BD59
		public void SetConnectionInternal(int dir, bool value)
		{
			this.gridFlags = (ushort)(((int)this.gridFlags & ~(1 << dir)) | ((value ? 1 : 0) << (dir & 31)));
			AstarPath.active.hierarchicalGraph.AddDirtyNode(this);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0001DB8D File Offset: 0x0001BD8D
		public void SetAllConnectionInternal(int connections)
		{
			this.gridFlags = (ushort)(((int)this.gridFlags & -256) | connections);
			AstarPath.active.hierarchicalGraph.AddDirtyNode(this);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0001DBB4 File Offset: 0x0001BDB4
		public void ResetConnectionsInternal()
		{
			this.gridFlags = (ushort)((int)this.gridFlags & -256);
			AstarPath.active.hierarchicalGraph.AddDirtyNode(this);
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x0001DBD9 File Offset: 0x0001BDD9
		// (set) Token: 0x0600053B RID: 1339 RVA: 0x0001DBEA File Offset: 0x0001BDEA
		public bool EdgeNode
		{
			get
			{
				return (this.gridFlags & 1024) > 0;
			}
			set
			{
				this.gridFlags = (ushort)(((int)this.gridFlags & -1025) | (value ? 1024 : 0));
			}
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0001DC0C File Offset: 0x0001BE0C
		public override GridNodeBase GetNeighbourAlongDirection(int direction)
		{
			if (this.HasConnectionInDirection(direction))
			{
				GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
				return gridGraph.nodes[base.NodeInGridIndex + gridGraph.neighbourOffsets[direction]];
			}
			return null;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0001DC48 File Offset: 0x0001BE48
		public override void ClearConnections(bool alsoReverse)
		{
			if (alsoReverse)
			{
				for (int i = 0; i < 8; i++)
				{
					GridNode gridNode = this.GetNeighbourAlongDirection(i) as GridNode;
					if (gridNode != null)
					{
						gridNode.SetConnectionInternal((i < 4) ? ((i + 2) % 4) : ((i - 2) % 4 + 4), false);
					}
				}
			}
			this.ResetConnectionsInternal();
			base.ClearConnections(alsoReverse);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0001DC9C File Offset: 0x0001BE9C
		public override void GetConnections(Action<GraphNode> action)
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			GridNodeBase[] nodes = gridGraph.nodes;
			for (int i = 0; i < 8; i++)
			{
				if (this.HasConnectionInDirection(i))
				{
					GridNodeBase gridNodeBase = nodes[base.NodeInGridIndex + neighbourOffsets[i]];
					if (gridNodeBase != null)
					{
						action(gridNodeBase);
					}
				}
			}
			base.GetConnections(action);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0001DCF4 File Offset: 0x0001BEF4
		public override Vector3 ClosestPointOnNode(Vector3 p)
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			p = gridGraph.transform.InverseTransform(p);
			int num = base.NodeInGridIndex % gridGraph.width;
			int num2 = base.NodeInGridIndex / gridGraph.width;
			float y = gridGraph.transform.InverseTransform((Vector3)this.position).y;
			Vector3 vector = new Vector3(Mathf.Clamp(p.x, (float)num, (float)num + 1f), y, Mathf.Clamp(p.z, (float)num2, (float)num2 + 1f));
			return gridGraph.transform.Transform(vector);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0001DD94 File Offset: 0x0001BF94
		public override bool GetPortal(GraphNode other, List<Vector3> left, List<Vector3> right, bool backwards)
		{
			if (backwards)
			{
				return true;
			}
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			GridNodeBase[] nodes = gridGraph.nodes;
			for (int i = 0; i < 4; i++)
			{
				if (this.HasConnectionInDirection(i) && other == nodes[base.NodeInGridIndex + neighbourOffsets[i]])
				{
					Vector3 vector = (Vector3)(this.position + other.position) * 0.5f;
					Vector3 vector2 = Vector3.Cross(gridGraph.collision.up, (Vector3)(other.position - this.position));
					vector2.Normalize();
					vector2 *= gridGraph.nodeSize * 0.5f;
					left.Add(vector - vector2);
					right.Add(vector + vector2);
					return true;
				}
			}
			for (int j = 4; j < 8; j++)
			{
				if (this.HasConnectionInDirection(j) && other == nodes[base.NodeInGridIndex + neighbourOffsets[j]])
				{
					bool flag = false;
					bool flag2 = false;
					if (this.HasConnectionInDirection(j - 4))
					{
						GridNodeBase gridNodeBase = nodes[base.NodeInGridIndex + neighbourOffsets[j - 4]];
						if (gridNodeBase.Walkable && gridNodeBase.HasConnectionInDirection((j - 4 + 1) % 4))
						{
							flag = true;
						}
					}
					if (this.HasConnectionInDirection((j - 4 + 1) % 4))
					{
						GridNodeBase gridNodeBase2 = nodes[base.NodeInGridIndex + neighbourOffsets[(j - 4 + 1) % 4]];
						if (gridNodeBase2.Walkable && gridNodeBase2.HasConnectionInDirection(j - 4))
						{
							flag2 = true;
						}
					}
					Vector3 vector3 = (Vector3)(this.position + other.position) * 0.5f;
					Vector3 vector4 = Vector3.Cross(gridGraph.collision.up, (Vector3)(other.position - this.position));
					vector4.Normalize();
					vector4 *= gridGraph.nodeSize * 1.4142f;
					left.Add(vector3 - (flag2 ? vector4 : Vector3.zero));
					right.Add(vector3 + (flag ? vector4 : Vector3.zero));
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0001DFC8 File Offset: 0x0001C1C8
		public override void UpdateRecursiveG(Path path, PathNode pathNode, PathHandler handler)
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			GridNodeBase[] nodes = gridGraph.nodes;
			pathNode.UpdateG(path);
			handler.heap.Add(pathNode);
			ushort pathID = handler.PathID;
			int nodeInGridIndex = base.NodeInGridIndex;
			for (int i = 0; i < 8; i++)
			{
				if (this.HasConnectionInDirection(i))
				{
					GridNodeBase gridNodeBase = nodes[nodeInGridIndex + neighbourOffsets[i]];
					PathNode pathNode2 = handler.GetPathNode(gridNodeBase);
					if (pathNode2.parent == pathNode && pathNode2.pathID == pathID)
					{
						gridNodeBase.UpdateRecursiveG(path, pathNode2, handler);
					}
				}
			}
			base.UpdateRecursiveG(path, pathNode, handler);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0001E064 File Offset: 0x0001C264
		public override void Open(Path path, PathNode pathNode, PathHandler handler)
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			ushort pathID = handler.PathID;
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			uint[] neighbourCosts = gridGraph.neighbourCosts;
			GridNodeBase[] nodes = gridGraph.nodes;
			int nodeInGridIndex = base.NodeInGridIndex;
			for (int i = 0; i < 8; i++)
			{
				if (this.HasConnectionInDirection(i))
				{
					GridNodeBase gridNodeBase = nodes[nodeInGridIndex + neighbourOffsets[i]];
					if (path.CanTraverse(gridNodeBase))
					{
						PathNode pathNode2 = handler.GetPathNode(gridNodeBase);
						uint num = neighbourCosts[i];
						if (pathNode2.pathID != pathID)
						{
							pathNode2.parent = pathNode;
							pathNode2.pathID = pathID;
							pathNode2.cost = num;
							pathNode2.H = path.CalculateHScore(gridNodeBase);
							pathNode2.UpdateG(path);
							handler.heap.Add(pathNode2);
						}
						else if (pathNode.G + num + path.GetTraversalCost(gridNodeBase) < pathNode2.G)
						{
							pathNode2.cost = num;
							pathNode2.parent = pathNode;
							gridNodeBase.UpdateRecursiveG(path, pathNode2, handler);
						}
					}
				}
			}
			base.Open(path, pathNode, handler);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001E173 File Offset: 0x0001C373
		public override void SerializeNode(GraphSerializationContext ctx)
		{
			base.SerializeNode(ctx);
			ctx.SerializeInt3(this.position);
			ctx.writer.Write(this.gridFlags);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0001E199 File Offset: 0x0001C399
		public override void DeserializeNode(GraphSerializationContext ctx)
		{
			base.DeserializeNode(ctx);
			this.position = ctx.DeserializeInt3();
			this.gridFlags = ctx.reader.ReadUInt16();
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0001E1C0 File Offset: 0x0001C3C0
		public override void AddConnection(GraphNode node, uint cost)
		{
			GridNode gridNode = node as GridNode;
			if (gridNode != null && gridNode.GraphIndex == base.GraphIndex)
			{
				this.RemoveGridConnection(gridNode);
			}
			base.AddConnection(node, cost);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0001E1F4 File Offset: 0x0001C3F4
		public override void RemoveConnection(GraphNode node)
		{
			base.RemoveConnection(node);
			GridNode gridNode = node as GridNode;
			if (gridNode != null && gridNode.GraphIndex == base.GraphIndex)
			{
				this.RemoveGridConnection(gridNode);
			}
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0001E228 File Offset: 0x0001C428
		protected void RemoveGridConnection(GridNode node)
		{
			int nodeInGridIndex = base.NodeInGridIndex;
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			for (int i = 0; i < 8; i++)
			{
				if (nodeInGridIndex + gridGraph.neighbourOffsets[i] == node.NodeInGridIndex && this.GetNeighbourAlongDirection(i) == node)
				{
					this.SetConnectionInternal(i, false);
					return;
				}
			}
		}

		// Token: 0x04000300 RID: 768
		private static GridGraph[] _gridGraphs = new GridGraph[0];

		// Token: 0x04000301 RID: 769
		private const int GridFlagsConnectionOffset = 0;

		// Token: 0x04000302 RID: 770
		private const int GridFlagsConnectionBit0 = 1;

		// Token: 0x04000303 RID: 771
		private const int GridFlagsConnectionMask = 255;

		// Token: 0x04000304 RID: 772
		private const int GridFlagsEdgeNodeOffset = 10;

		// Token: 0x04000305 RID: 773
		private const int GridFlagsEdgeNodeMask = 1024;
	}
}
