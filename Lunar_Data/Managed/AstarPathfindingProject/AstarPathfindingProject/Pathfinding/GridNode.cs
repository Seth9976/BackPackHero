using System;
using Pathfinding.Serialization;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000E1 RID: 225
	public class GridNode : GridNodeBase
	{
		// Token: 0x06000715 RID: 1813 RVA: 0x00025BFE File Offset: 0x00023DFE
		public GridNode()
		{
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00025C06 File Offset: 0x00023E06
		public GridNode(AstarPath astar)
		{
			astar.InitializeNode(this);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00025C15 File Offset: 0x00023E15
		public static GridGraph GetGridGraph(uint graphIndex)
		{
			return GridNode._gridGraphs[(int)graphIndex];
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00025C20 File Offset: 0x00023E20
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

		// Token: 0x06000719 RID: 1817 RVA: 0x00025C6A File Offset: 0x00023E6A
		public static void ClearGridGraph(int graphIndex, GridGraph graph)
		{
			if (graphIndex < GridNode._gridGraphs.Length && GridNode._gridGraphs[graphIndex] == graph)
			{
				GridNode._gridGraphs[graphIndex] = null;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x00025C88 File Offset: 0x00023E88
		// (set) Token: 0x0600071B RID: 1819 RVA: 0x00025C90 File Offset: 0x00023E90
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

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x00025C99 File Offset: 0x00023E99
		public override bool HasConnectionsToAllEightNeighbours
		{
			get
			{
				return (this.InternalGridFlags & 255) == 255;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x00025CAE File Offset: 0x00023EAE
		public override bool HasConnectionsToAllAxisAlignedNeighbours
		{
			get
			{
				return (this.InternalGridFlags & 15) == 15;
			}
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00025CBD File Offset: 0x00023EBD
		public override bool HasConnectionInDirection(int dir)
		{
			return ((this.gridFlags >> dir) & 1) != 0;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00025CCF File Offset: 0x00023ECF
		[Obsolete("Use HasConnectionInDirection")]
		public bool GetConnectionInternal(int dir)
		{
			return this.HasConnectionInDirection(dir);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00025CD8 File Offset: 0x00023ED8
		public void SetConnection(int dir, bool value)
		{
			this.SetConnectionInternal(dir, value);
			GridNode.GetGridGraph(base.GraphIndex).nodeDataRef.connections[base.NodeInGridIndex] = (ulong)((long)this.GetAllConnectionInternal());
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00025D09 File Offset: 0x00023F09
		public void SetConnectionInternal(int dir, bool value)
		{
			this.gridFlags = (ushort)(((int)this.gridFlags & ~(1 << dir)) | ((value ? 1 : 0) << (dir & 31)));
			AstarPath.active.hierarchicalGraph.AddDirtyNode(this);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00025D3D File Offset: 0x00023F3D
		public void SetAllConnectionInternal(int connections)
		{
			this.gridFlags = (ushort)(((int)this.gridFlags & -256) | connections);
			AstarPath.active.hierarchicalGraph.AddDirtyNode(this);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00025D64 File Offset: 0x00023F64
		public int GetAllConnectionInternal()
		{
			return (int)(this.gridFlags & 255);
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x00025D72 File Offset: 0x00023F72
		public override bool HasAnyGridConnections
		{
			get
			{
				return this.GetAllConnectionInternal() != 0;
			}
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00025D7D File Offset: 0x00023F7D
		public override void ResetConnectionsInternal()
		{
			this.gridFlags = (ushort)((int)this.gridFlags & -256);
			AstarPath.active.hierarchicalGraph.AddDirtyNode(this);
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x00025DA2 File Offset: 0x00023FA2
		// (set) Token: 0x06000727 RID: 1831 RVA: 0x00025DB3 File Offset: 0x00023FB3
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

		// Token: 0x06000728 RID: 1832 RVA: 0x00025DD4 File Offset: 0x00023FD4
		public override GridNodeBase GetNeighbourAlongDirection(int direction)
		{
			if (this.HasConnectionInDirection(direction))
			{
				GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
				return gridGraph.nodes[base.NodeInGridIndex + gridGraph.neighbourOffsets[direction]];
			}
			return null;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00025E10 File Offset: 0x00024010
		public override void ClearConnections(bool alsoReverse)
		{
			if (alsoReverse)
			{
				for (int i = 0; i < 8; i++)
				{
					GridNode gridNode = this.GetNeighbourAlongDirection(i) as GridNode;
					if (gridNode != null)
					{
						gridNode.SetConnectionInternal(GridNodeBase.OppositeConnectionDirection(i), false);
					}
				}
			}
			this.ResetConnectionsInternal();
			base.ClearConnections(alsoReverse);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00025E58 File Offset: 0x00024058
		public override void GetConnections<T>(GraphNode.GetConnectionsWithData<T> action, ref T data, int connectionFilter)
		{
			if ((connectionFilter & 48) == 0)
			{
				return;
			}
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			GridNodeBase[] nodes = gridGraph.nodes;
			for (int i = 0; i < 8; i++)
			{
				if (((this.gridFlags >> i) & 1) != 0)
				{
					GridNodeBase gridNodeBase = nodes[base.NodeInGridIndex + neighbourOffsets[i]];
					if (gridNodeBase != null)
					{
						action(gridNodeBase, ref data);
					}
				}
			}
			base.GetConnections<T>(action, ref data, connectionFilter);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00025EC0 File Offset: 0x000240C0
		public override bool GetPortal(GraphNode other, out Vector3 left, out Vector3 right)
		{
			if (other.GraphIndex != base.GraphIndex)
			{
				left = (right = Vector3.zero);
				return false;
			}
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			Int2 @int = (other as GridNode).CoordinatesInGrid - base.CoordinatesInGrid;
			int num = GridNodeBase.OffsetToConnectionDirection(@int.x, @int.y);
			if (num == -1 || !this.HasConnectionInDirection(num))
			{
				left = (right = Vector3.zero);
				return false;
			}
			if (num < 4)
			{
				Vector3 vector = (Vector3)(this.position + other.position) * 0.5f;
				Vector3 vector2 = Vector3.Cross(gridGraph.collision.up, (Vector3)(other.position - this.position));
				vector2.Normalize();
				vector2 *= gridGraph.nodeSize * 0.5f;
				left = vector - vector2;
				right = vector + vector2;
			}
			else
			{
				bool flag = false;
				bool flag2 = false;
				if (this.HasConnectionInDirection(num - 4))
				{
					GridNodeBase gridNodeBase = gridGraph.nodes[base.NodeInGridIndex + gridGraph.neighbourOffsets[num - 4]];
					if (gridNodeBase.Walkable && gridNodeBase.HasConnectionInDirection((num - 4 + 1) % 4))
					{
						flag = true;
					}
				}
				if (this.HasConnectionInDirection((num - 4 + 1) % 4))
				{
					GridNodeBase gridNodeBase2 = gridGraph.nodes[base.NodeInGridIndex + gridGraph.neighbourOffsets[(num - 4 + 1) % 4]];
					if (gridNodeBase2.Walkable && gridNodeBase2.HasConnectionInDirection(num - 4))
					{
						flag2 = true;
					}
				}
				Vector3 vector3 = (Vector3)(this.position + other.position) * 0.5f;
				Vector3 vector4 = Vector3.Cross(gridGraph.collision.up, (Vector3)(other.position - this.position));
				vector4.Normalize();
				vector4 *= gridGraph.nodeSize * 1.4142f;
				left = vector3 - (flag2 ? vector4 : Vector3.zero);
				right = vector3 + (flag ? vector4 : Vector3.zero);
			}
			return true;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00026100 File Offset: 0x00024300
		public static int FilterDiagonalConnections(int conns, NumNeighbours neighbours, bool cutCorners)
		{
			switch (neighbours)
			{
			case NumNeighbours.Four:
				return conns & 15;
			case NumNeighbours.Six:
				return conns & 175;
			}
			if (cutCorners)
			{
				int num = conns & 15;
				int num2 = (num | ((num >> 1) | (num << 3))) << 4;
				num2 &= conns;
				return num | num2;
			}
			int num3 = conns & 15;
			int num4 = (num3 & ((num3 >> 1) | (num3 << 3))) << 4;
			num4 &= conns;
			return num3 | num4;
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00026164 File Offset: 0x00024364
		public override void Open(Path path, uint pathNodeIndex, uint gScore)
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			uint[] neighbourCosts = gridGraph.neighbourCosts;
			GridNodeBase[] nodes = gridGraph.nodes;
			int nodeInGridIndex = base.NodeInGridIndex;
			int num = (int)(this.gridFlags & 255);
			for (int i = 0; i < 8; i++)
			{
				if (i == 4 && (path.traversalProvider == null || path.traversalProvider.filterDiagonalGridConnections))
				{
					num = GridNode.FilterDiagonalConnections(num, gridGraph.neighbours, gridGraph.cutCorners);
				}
				if (((num >> i) & 1) != 0)
				{
					GridNodeBase gridNodeBase = nodes[nodeInGridIndex + neighbourOffsets[i]];
					if (path.CanTraverse(this, gridNodeBase))
					{
						path.OpenCandidateConnection(pathNodeIndex, gridNodeBase.NodeIndex, gScore, neighbourCosts[i], 0U, gridNodeBase.position);
					}
					else
					{
						num &= ~(1 << i);
					}
				}
			}
			base.Open(path, pathNodeIndex, gScore);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00026242 File Offset: 0x00024442
		public override void SerializeNode(GraphSerializationContext ctx)
		{
			base.SerializeNode(ctx);
			ctx.SerializeInt3(this.position);
			ctx.writer.Write(this.gridFlags);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00026268 File Offset: 0x00024468
		public override void DeserializeNode(GraphSerializationContext ctx)
		{
			base.DeserializeNode(ctx);
			this.position = ctx.DeserializeInt3();
			this.gridFlags = ctx.reader.ReadUInt16();
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00026290 File Offset: 0x00024490
		public override void AddPartialConnection(GraphNode node, uint cost, bool isOutgoing, bool isIncoming)
		{
			GridNode gridNode = node as GridNode;
			if (gridNode != null && gridNode.GraphIndex == base.GraphIndex)
			{
				this.RemoveGridConnection(gridNode);
			}
			base.AddPartialConnection(node, cost, isOutgoing, isIncoming);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x000262C8 File Offset: 0x000244C8
		public override void RemovePartialConnection(GraphNode node)
		{
			base.RemovePartialConnection(node);
			GridNode gridNode = node as GridNode;
			if (gridNode != null && gridNode.GraphIndex == base.GraphIndex)
			{
				this.RemoveGridConnection(gridNode);
			}
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x000262FC File Offset: 0x000244FC
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

		// Token: 0x040004B4 RID: 1204
		private static GridGraph[] _gridGraphs = new GridGraph[0];

		// Token: 0x040004B5 RID: 1205
		private const int GridFlagsConnectionOffset = 0;

		// Token: 0x040004B6 RID: 1206
		private const int GridFlagsConnectionBit0 = 1;

		// Token: 0x040004B7 RID: 1207
		private const int GridFlagsConnectionMask = 255;

		// Token: 0x040004B8 RID: 1208
		private const int GridFlagsAxisAlignedConnectionMask = 15;

		// Token: 0x040004B9 RID: 1209
		private const int GridFlagsEdgeNodeOffset = 10;

		// Token: 0x040004BA RID: 1210
		private const int GridFlagsEdgeNodeMask = 1024;
	}
}
