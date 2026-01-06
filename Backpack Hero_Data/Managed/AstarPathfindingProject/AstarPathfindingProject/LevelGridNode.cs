using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200005E RID: 94
	public class LevelGridNode : GridNodeBase
	{
		// Token: 0x060004CA RID: 1226 RVA: 0x0001A5DF File Offset: 0x000187DF
		public LevelGridNode(AstarPath astar)
			: base(astar)
		{
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001A5E8 File Offset: 0x000187E8
		public static LayerGridGraph GetGridGraph(uint graphIndex)
		{
			return LevelGridNode._gridGraphs[(int)graphIndex];
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001A5F4 File Offset: 0x000187F4
		public static void SetGridGraph(int graphIndex, LayerGridGraph graph)
		{
			GridNode.SetGridGraph(graphIndex, graph);
			if (LevelGridNode._gridGraphs.Length <= graphIndex)
			{
				LayerGridGraph[] array = new LayerGridGraph[graphIndex + 1];
				for (int i = 0; i < LevelGridNode._gridGraphs.Length; i++)
				{
					array[i] = LevelGridNode._gridGraphs[i];
				}
				LevelGridNode._gridGraphs = array;
			}
			LevelGridNode._gridGraphs[graphIndex] = graph;
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0001A645 File Offset: 0x00018845
		public void ResetAllGridConnections()
		{
			this.gridConnections = ulong.MaxValue;
			AstarPath.active.hierarchicalGraph.AddDirtyNode(this);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0001A65F File Offset: 0x0001885F
		public bool HasAnyGridConnections()
		{
			return this.gridConnections != ulong.MaxValue;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0001A66E File Offset: 0x0001886E
		public override bool HasConnectionsToAllEightNeighbours
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0001A671 File Offset: 0x00018871
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x0001A67C File Offset: 0x0001887C
		public int LayerCoordinateInGrid
		{
			get
			{
				return this.nodeInGridIndex >> 24;
			}
			set
			{
				this.nodeInGridIndex = (this.nodeInGridIndex & 16777215) | (value << 24);
			}
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0001A695 File Offset: 0x00018895
		public void SetPosition(Int3 position)
		{
			this.position = position;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0001A69E File Offset: 0x0001889E
		public override int GetGizmoHashCode()
		{
			return base.GetGizmoHashCode() ^ (int)(805306457UL * this.gridConnections);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0001A6B8 File Offset: 0x000188B8
		public override GridNodeBase GetNeighbourAlongDirection(int direction)
		{
			int connectionValue = this.GetConnectionValue(direction);
			if (connectionValue != 255)
			{
				LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
				return gridGraph.nodes[base.NodeInGridIndex + gridGraph.neighbourOffsets[direction] + gridGraph.lastScannedWidth * gridGraph.lastScannedDepth * connectionValue];
			}
			return null;
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0001A70C File Offset: 0x0001890C
		public override void ClearConnections(bool alsoReverse)
		{
			if (alsoReverse)
			{
				LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
				int[] neighbourOffsets = gridGraph.neighbourOffsets;
				GridNodeBase[] nodes = gridGraph.nodes;
				for (int i = 0; i < 4; i++)
				{
					int connectionValue = this.GetConnectionValue(i);
					if (connectionValue != 255)
					{
						LevelGridNode levelGridNode = nodes[base.NodeInGridIndex + neighbourOffsets[i] + gridGraph.lastScannedWidth * gridGraph.lastScannedDepth * connectionValue] as LevelGridNode;
						if (levelGridNode != null)
						{
							levelGridNode.SetConnectionValue((i + 2) % 4, 255);
						}
					}
				}
			}
			this.ResetAllGridConnections();
			base.ClearConnections(alsoReverse);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0001A79C File Offset: 0x0001899C
		public override void GetConnections(Action<GraphNode> action)
		{
			LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			GridNodeBase[] nodes = gridGraph.nodes;
			int nodeInGridIndex = base.NodeInGridIndex;
			for (int i = 0; i < 4; i++)
			{
				int connectionValue = this.GetConnectionValue(i);
				if (connectionValue != 255)
				{
					GraphNode graphNode = nodes[nodeInGridIndex + neighbourOffsets[i] + gridGraph.lastScannedWidth * gridGraph.lastScannedDepth * connectionValue];
					if (graphNode != null)
					{
						action(graphNode);
					}
				}
			}
			base.GetConnections(action);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0001A81B File Offset: 0x00018A1B
		[Obsolete("Use HasConnectionInDirection instead")]
		public bool GetConnection(int i)
		{
			return ((this.gridConnections >> i * 8) & 255UL) != 255UL;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001A83C File Offset: 0x00018A3C
		public override bool HasConnectionInDirection(int direction)
		{
			return ((this.gridConnections >> direction * 8) & 255UL) != 255UL;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0001A85D File Offset: 0x00018A5D
		public void SetConnectionValue(int dir, int value)
		{
			this.gridConnections = (this.gridConnections & ~(255UL << dir * 8)) | (ulong)((ulong)((long)value) << dir * 8);
			AstarPath.active.hierarchicalGraph.AddDirtyNode(this);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0001A894 File Offset: 0x00018A94
		public int GetConnectionValue(int dir)
		{
			return (int)((this.gridConnections >> dir * 8) & 255UL);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0001A8AC File Offset: 0x00018AAC
		public override void AddConnection(GraphNode node, uint cost)
		{
			LevelGridNode levelGridNode = node as LevelGridNode;
			if (levelGridNode != null && levelGridNode.GraphIndex == base.GraphIndex)
			{
				this.RemoveGridConnection(levelGridNode);
			}
			base.AddConnection(node, cost);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0001A8E0 File Offset: 0x00018AE0
		public override void RemoveConnection(GraphNode node)
		{
			base.RemoveConnection(node);
			LevelGridNode levelGridNode = node as LevelGridNode;
			if (levelGridNode != null && levelGridNode.GraphIndex == base.GraphIndex)
			{
				this.RemoveGridConnection(levelGridNode);
			}
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0001A914 File Offset: 0x00018B14
		protected void RemoveGridConnection(LevelGridNode node)
		{
			int nodeInGridIndex = base.NodeInGridIndex;
			LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
			for (int i = 0; i < 8; i++)
			{
				if (nodeInGridIndex + gridGraph.neighbourOffsets[i] == node.NodeInGridIndex && this.GetNeighbourAlongDirection(i) == node)
				{
					this.SetConnectionValue(i, 255);
					return;
				}
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0001A96C File Offset: 0x00018B6C
		public override bool GetPortal(GraphNode other, List<Vector3> left, List<Vector3> right, bool backwards)
		{
			if (backwards)
			{
				return true;
			}
			LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			GridNodeBase[] nodes = gridGraph.nodes;
			int nodeInGridIndex = base.NodeInGridIndex;
			for (int i = 0; i < 4; i++)
			{
				int connectionValue = this.GetConnectionValue(i);
				if (connectionValue != 255 && other == nodes[nodeInGridIndex + neighbourOffsets[i] + gridGraph.lastScannedWidth * gridGraph.lastScannedDepth * connectionValue])
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
			return false;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0001AA74 File Offset: 0x00018C74
		public override void UpdateRecursiveG(Path path, PathNode pathNode, PathHandler handler)
		{
			handler.heap.Add(pathNode);
			pathNode.UpdateG(path);
			LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			GridNodeBase[] nodes = gridGraph.nodes;
			int nodeInGridIndex = base.NodeInGridIndex;
			for (int i = 0; i < 4; i++)
			{
				int connectionValue = this.GetConnectionValue(i);
				if (connectionValue != 255)
				{
					GridNodeBase gridNodeBase = nodes[nodeInGridIndex + neighbourOffsets[i] + gridGraph.lastScannedWidth * gridGraph.lastScannedDepth * connectionValue];
					PathNode pathNode2 = handler.GetPathNode(gridNodeBase);
					if (pathNode2 != null && pathNode2.parent == pathNode && pathNode2.pathID == handler.PathID)
					{
						gridNodeBase.UpdateRecursiveG(path, pathNode2, handler);
					}
				}
			}
			base.UpdateRecursiveG(path, pathNode, handler);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001AB30 File Offset: 0x00018D30
		public override void Open(Path path, PathNode pathNode, PathHandler handler)
		{
			LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			uint[] neighbourCosts = gridGraph.neighbourCosts;
			GridNodeBase[] nodes = gridGraph.nodes;
			int nodeInGridIndex = base.NodeInGridIndex;
			for (int i = 0; i < 4; i++)
			{
				int connectionValue = this.GetConnectionValue(i);
				if (connectionValue != 255)
				{
					GraphNode graphNode = nodes[nodeInGridIndex + neighbourOffsets[i] + gridGraph.lastScannedWidth * gridGraph.lastScannedDepth * connectionValue];
					if (path.CanTraverse(graphNode))
					{
						PathNode pathNode2 = handler.GetPathNode(graphNode);
						if (pathNode2.pathID != handler.PathID)
						{
							pathNode2.parent = pathNode;
							pathNode2.pathID = handler.PathID;
							pathNode2.cost = neighbourCosts[i];
							pathNode2.H = path.CalculateHScore(graphNode);
							pathNode2.UpdateG(path);
							handler.heap.Add(pathNode2);
						}
						else
						{
							uint num = neighbourCosts[i];
							if (pathNode.G + num + path.GetTraversalCost(graphNode) < pathNode2.G)
							{
								pathNode2.cost = num;
								pathNode2.parent = pathNode;
								graphNode.UpdateRecursiveG(path, pathNode2, handler);
							}
						}
					}
				}
			}
			base.Open(path, pathNode, handler);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001AC60 File Offset: 0x00018E60
		public override Vector3 ClosestPointOnNode(Vector3 p)
		{
			LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
			p = gridGraph.transform.InverseTransform(p);
			int xcoordinateInGrid = base.XCoordinateInGrid;
			int zcoordinateInGrid = base.ZCoordinateInGrid;
			float y = gridGraph.transform.InverseTransform((Vector3)this.position).y;
			Vector3 vector = new Vector3(Mathf.Clamp(p.x, (float)xcoordinateInGrid, (float)xcoordinateInGrid + 1f), y, Mathf.Clamp(p.z, (float)zcoordinateInGrid, (float)zcoordinateInGrid + 1f));
			return gridGraph.transform.Transform(vector);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0001ACED File Offset: 0x00018EED
		public override void SerializeNode(GraphSerializationContext ctx)
		{
			base.SerializeNode(ctx);
			ctx.SerializeInt3(this.position);
			ctx.writer.Write(this.gridFlags);
			ctx.writer.Write(this.gridConnections);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0001AD24 File Offset: 0x00018F24
		public override void DeserializeNode(GraphSerializationContext ctx)
		{
			base.DeserializeNode(ctx);
			this.position = ctx.DeserializeInt3();
			this.gridFlags = ctx.reader.ReadUInt16();
			if (ctx.meta.version < AstarSerializer.V3_9_0)
			{
				this.gridConnections = (ulong)ctx.reader.ReadUInt32() | 18446744069414584320UL;
				return;
			}
			this.gridConnections = ctx.reader.ReadUInt64();
		}

		// Token: 0x040002DB RID: 731
		private static LayerGridGraph[] _gridGraphs = new LayerGridGraph[0];

		// Token: 0x040002DC RID: 732
		public ulong gridConnections;

		// Token: 0x040002DD RID: 733
		protected static LayerGridGraph[] gridGraphs;

		// Token: 0x040002DE RID: 734
		public const int NoConnection = 255;

		// Token: 0x040002DF RID: 735
		public const int ConnectionMask = 255;

		// Token: 0x040002E0 RID: 736
		private const int ConnectionStride = 8;

		// Token: 0x040002E1 RID: 737
		public const int MaxLayerCount = 255;
	}
}
