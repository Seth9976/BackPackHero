using System;
using Pathfinding.Serialization;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000E3 RID: 227
	public class LevelGridNode : GridNodeBase
	{
		// Token: 0x0600075A RID: 1882 RVA: 0x00025BFE File Offset: 0x00023DFE
		public LevelGridNode()
		{
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00025C06 File Offset: 0x00023E06
		public LevelGridNode(AstarPath astar)
		{
			astar.InitializeNode(this);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00026B40 File Offset: 0x00024D40
		public static LayerGridGraph GetGridGraph(uint graphIndex)
		{
			return LevelGridNode._gridGraphs[(int)graphIndex];
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00026B4C File Offset: 0x00024D4C
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

		// Token: 0x0600075E RID: 1886 RVA: 0x00026B9D File Offset: 0x00024D9D
		public static void ClearGridGraph(int graphIndex, LayerGridGraph graph)
		{
			if (graphIndex < LevelGridNode._gridGraphs.Length && LevelGridNode._gridGraphs[graphIndex] == graph)
			{
				LevelGridNode._gridGraphs[graphIndex] = null;
			}
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00026BBB File Offset: 0x00024DBB
		public override void ResetConnectionsInternal()
		{
			this.gridConnections = uint.MaxValue;
			AstarPath.active.hierarchicalGraph.AddDirtyNode(this);
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x00026BD4 File Offset: 0x00024DD4
		public override bool HasAnyGridConnections
		{
			get
			{
				return this.gridConnections != uint.MaxValue;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x00026BE4 File Offset: 0x00024DE4
		public override bool HasConnectionsToAllEightNeighbours
		{
			get
			{
				for (int i = 0; i < 8; i++)
				{
					if (!this.HasConnectionInDirection(i))
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x00026C09 File Offset: 0x00024E09
		public override bool HasConnectionsToAllAxisAlignedNeighbours
		{
			get
			{
				return (this.gridConnections & 65535U) == 65535U;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x00026C1E File Offset: 0x00024E1E
		// (set) Token: 0x06000764 RID: 1892 RVA: 0x00026C29 File Offset: 0x00024E29
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

		// Token: 0x06000765 RID: 1893 RVA: 0x00026C42 File Offset: 0x00024E42
		public void SetPosition(Int3 position)
		{
			this.position = position;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00026C4B File Offset: 0x00024E4B
		public override int GetGizmoHashCode()
		{
			return base.GetGizmoHashCode() ^ (int)((805306457UL * (ulong)this.gridConnections) ^ (402653189UL * (ulong)this.gridConnections));
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00026C74 File Offset: 0x00024E74
		public override GridNodeBase GetNeighbourAlongDirection(int direction)
		{
			int connectionValue = this.GetConnectionValue(direction);
			if (connectionValue != 15)
			{
				LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
				return gridGraph.nodes[base.NodeInGridIndex + gridGraph.neighbourOffsets[direction] + gridGraph.lastScannedWidth * gridGraph.lastScannedDepth * connectionValue];
			}
			return null;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00026CC4 File Offset: 0x00024EC4
		public override void ClearConnections(bool alsoReverse)
		{
			if (alsoReverse)
			{
				LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
				int[] neighbourOffsets = gridGraph.neighbourOffsets;
				GridNodeBase[] nodes = gridGraph.nodes;
				for (int i = 0; i < 8; i++)
				{
					int connectionValue = this.GetConnectionValue(i);
					if (connectionValue != 15)
					{
						LevelGridNode levelGridNode = nodes[base.NodeInGridIndex + neighbourOffsets[i] + gridGraph.lastScannedWidth * gridGraph.lastScannedDepth * connectionValue] as LevelGridNode;
						if (levelGridNode != null)
						{
							levelGridNode.SetConnectionValue((i + 2) % 4, 15);
						}
					}
				}
			}
			this.ResetConnectionsInternal();
			base.ClearConnections(alsoReverse);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00026D4C File Offset: 0x00024F4C
		public override void GetConnections<T>(GraphNode.GetConnectionsWithData<T> action, ref T data, int connectionFilter)
		{
			if ((connectionFilter & 48) == 0)
			{
				return;
			}
			LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			GridNodeBase[] nodes = gridGraph.nodes;
			int nodeInGridIndex = base.NodeInGridIndex;
			for (int i = 0; i < 8; i++)
			{
				int connectionValue = this.GetConnectionValue(i);
				if (connectionValue != 15)
				{
					GridNodeBase gridNodeBase = nodes[nodeInGridIndex + neighbourOffsets[i] + gridGraph.lastScannedWidth * gridGraph.lastScannedDepth * connectionValue];
					if (gridNodeBase != null)
					{
						action(gridNodeBase, ref data);
					}
				}
			}
			base.GetConnections<T>(action, ref data, connectionFilter);
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00026DD2 File Offset: 0x00024FD2
		[Obsolete("Use HasConnectionInDirection instead")]
		public bool GetConnection(int i)
		{
			return ((this.gridConnections >> i * 4) & 15U) != 15U;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00026DD2 File Offset: 0x00024FD2
		public override bool HasConnectionInDirection(int direction)
		{
			return ((this.gridConnections >> direction * 4) & 15U) != 15U;
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00026DEB File Offset: 0x00024FEB
		public void SetConnectionValue(int dir, int value)
		{
			this.gridConnections = (this.gridConnections & ~(15U << dir * 4)) | (uint)((uint)value << dir * 4);
			AstarPath.active.hierarchicalGraph.AddDirtyNode(this);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00026E1D File Offset: 0x0002501D
		public void SetAllConnectionInternal(ulong value)
		{
			this.gridConnections = (uint)value;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00026E27 File Offset: 0x00025027
		public int GetConnectionValue(int dir)
		{
			return (int)((this.gridConnections >> dir * 4) & 15U);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00026E3C File Offset: 0x0002503C
		public override void AddPartialConnection(GraphNode node, uint cost, bool isOutgoing, bool isIncoming)
		{
			LevelGridNode levelGridNode = node as LevelGridNode;
			if (levelGridNode != null && levelGridNode.GraphIndex == base.GraphIndex)
			{
				this.RemoveGridConnection(levelGridNode);
			}
			base.AddPartialConnection(node, cost, isOutgoing, isIncoming);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00026E74 File Offset: 0x00025074
		public override void RemovePartialConnection(GraphNode node)
		{
			base.RemovePartialConnection(node);
			LevelGridNode levelGridNode = node as LevelGridNode;
			if (levelGridNode != null && levelGridNode.GraphIndex == base.GraphIndex)
			{
				this.RemoveGridConnection(levelGridNode);
			}
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00026EA8 File Offset: 0x000250A8
		protected void RemoveGridConnection(LevelGridNode node)
		{
			int nodeInGridIndex = base.NodeInGridIndex;
			LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
			for (int i = 0; i < 8; i++)
			{
				if (nodeInGridIndex + gridGraph.neighbourOffsets[i] == node.NodeInGridIndex && this.GetNeighbourAlongDirection(i) == node)
				{
					this.SetConnectionValue(i, 15);
					return;
				}
			}
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00026EFC File Offset: 0x000250FC
		public override bool GetPortal(GraphNode other, out Vector3 left, out Vector3 right)
		{
			LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			GridNodeBase[] nodes = gridGraph.nodes;
			int nodeInGridIndex = base.NodeInGridIndex;
			for (int i = 0; i < 8; i++)
			{
				int connectionValue = this.GetConnectionValue(i);
				if (connectionValue != 15 && other == nodes[nodeInGridIndex + neighbourOffsets[i] + gridGraph.lastScannedWidth * gridGraph.lastScannedDepth * connectionValue])
				{
					Vector3 vector = (Vector3)(this.position + other.position) * 0.5f;
					Vector3 vector2 = Vector3.Cross(gridGraph.collision.up, (Vector3)(other.position - this.position));
					vector2.Normalize();
					vector2 *= gridGraph.nodeSize * 0.5f;
					left = vector - vector2;
					right = vector + vector2;
					return true;
				}
			}
			left = Vector3.zero;
			right = Vector3.zero;
			return false;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00027010 File Offset: 0x00025210
		public override void Open(Path path, uint pathNodeIndex, uint gScore)
		{
			LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			uint[] neighbourCosts = gridGraph.neighbourCosts;
			GridNodeBase[] nodes = gridGraph.nodes;
			int nodeInGridIndex = base.NodeInGridIndex;
			int num = 255;
			for (int i = 0; i < 8; i++)
			{
				if (i == 4 && (path.traversalProvider == null || path.traversalProvider.filterDiagonalGridConnections))
				{
					num = GridNode.FilterDiagonalConnections(num, gridGraph.neighbours, gridGraph.cutCorners);
				}
				int connectionValue = this.GetConnectionValue(i);
				if (connectionValue != 15 && ((num >> i) & 1) != 0)
				{
					GraphNode graphNode = nodes[nodeInGridIndex + neighbourOffsets[i] + gridGraph.lastScannedWidth * gridGraph.lastScannedDepth * connectionValue];
					if (!path.CanTraverse(this, graphNode))
					{
						num &= ~(1 << i);
					}
					else
					{
						path.OpenCandidateConnection(pathNodeIndex, graphNode.NodeIndex, gScore, neighbourCosts[i], 0U, graphNode.position);
					}
				}
				else
				{
					num &= ~(1 << i);
				}
			}
			base.Open(path, pathNodeIndex, gScore);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00027118 File Offset: 0x00025318
		public override void SerializeNode(GraphSerializationContext ctx)
		{
			base.SerializeNode(ctx);
			ctx.SerializeInt3(this.position);
			ctx.writer.Write(this.gridFlags);
			ulong num = 0UL;
			for (int i = 0; i < 8; i++)
			{
				num |= (ulong)((ulong)((long)this.GetConnectionValue(i)) << i * 8);
			}
			ctx.writer.Write(num);
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00027178 File Offset: 0x00025378
		public override void DeserializeNode(GraphSerializationContext ctx)
		{
			base.DeserializeNode(ctx);
			this.position = ctx.DeserializeInt3();
			this.gridFlags = ctx.reader.ReadUInt16();
			if (ctx.meta.version < AstarSerializer.V4_3_12)
			{
				ulong num;
				if (ctx.meta.version < AstarSerializer.V3_9_0)
				{
					num = (ulong)ctx.reader.ReadUInt32() | 1085102592318504960UL;
				}
				else
				{
					num = ctx.reader.ReadUInt64();
				}
				this.gridConnections = 0U;
				for (int i = 0; i < 8; i++)
				{
					ulong num2 = (num >> i * 8) & 255UL;
					if ((num2 & 15UL) != num2)
					{
						num2 = 15UL;
					}
					this.SetConnectionValue(i, (int)num2);
				}
				return;
			}
			ulong num3 = ctx.reader.ReadUInt64();
			uint num4 = 0U;
			if (ctx.meta.version < AstarSerializer.V4_3_83)
			{
				num4 = (uint)num3;
			}
			else
			{
				for (int j = 0; j < 8; j++)
				{
					num4 |= ((uint)(num3 >> j * 8) & 15U) << 4 * j;
				}
			}
			this.gridConnections = num4;
		}

		// Token: 0x040004C5 RID: 1221
		private static LayerGridGraph[] _gridGraphs = new LayerGridGraph[0];

		// Token: 0x040004C6 RID: 1222
		public uint gridConnections;

		// Token: 0x040004C7 RID: 1223
		protected static LayerGridGraph[] gridGraphs;

		// Token: 0x040004C8 RID: 1224
		private const int MaxNeighbours = 8;

		// Token: 0x040004C9 RID: 1225
		public const int ConnectionMask = 15;

		// Token: 0x040004CA RID: 1226
		public const int ConnectionStride = 4;

		// Token: 0x040004CB RID: 1227
		public const int AxisAlignedConnectionsMask = 65535;

		// Token: 0x040004CC RID: 1228
		public const uint AllConnectionsMask = 4294967295U;

		// Token: 0x040004CD RID: 1229
		public const int NoConnection = 15;

		// Token: 0x040004CE RID: 1230
		internal const ulong DiagonalConnectionsMask = 4294901760UL;

		// Token: 0x040004CF RID: 1231
		public const int MaxLayerCount = 15;
	}
}
