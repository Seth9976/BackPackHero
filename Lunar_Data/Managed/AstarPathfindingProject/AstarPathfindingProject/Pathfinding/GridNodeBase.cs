using System;
using System.Runtime.CompilerServices;
using Pathfinding.Serialization;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000E2 RID: 226
	public abstract class GridNodeBase : GraphNode
	{
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x0002635A File Offset: 0x0002455A
		// (set) Token: 0x06000735 RID: 1845 RVA: 0x00026368 File Offset: 0x00024568
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

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x0002637E File Offset: 0x0002457E
		public int XCoordinateInGrid
		{
			get
			{
				return this.NodeInGridIndex % GridNode.GetGridGraph(base.GraphIndex).width;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x00026397 File Offset: 0x00024597
		public int ZCoordinateInGrid
		{
			get
			{
				return this.NodeInGridIndex / GridNode.GetGridGraph(base.GraphIndex).width;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x000263B0 File Offset: 0x000245B0
		public Int2 CoordinatesInGrid
		{
			[MethodImpl(256)]
			get
			{
				int width = GridNode.GetGridGraph(base.GraphIndex).width;
				int num = this.NodeInGridIndex;
				int num2 = num / width;
				return new Int2(num - num2 * width, num2);
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x000263E2 File Offset: 0x000245E2
		// (set) Token: 0x0600073A RID: 1850 RVA: 0x000263F3 File Offset: 0x000245F3
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

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x00026414 File Offset: 0x00024614
		// (set) Token: 0x0600073C RID: 1852 RVA: 0x00026425 File Offset: 0x00024625
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

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600073D RID: 1853
		public abstract bool HasConnectionsToAllEightNeighbours { get; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600073E RID: 1854
		public abstract bool HasConnectionsToAllAxisAlignedNeighbours { get; }

		// Token: 0x0600073F RID: 1855 RVA: 0x00026446 File Offset: 0x00024646
		public static int OppositeConnectionDirection(int dir)
		{
			if (dir >= 4)
			{
				return (dir - 2) % 4 + 4;
			}
			return (dir + 2) % 4;
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00026459 File Offset: 0x00024659
		public static int OffsetToConnectionDirection(int dx, int dz)
		{
			dx++;
			dz++;
			if (dx > 2 || dz > 2)
			{
				return -1;
			}
			return GridNodeBase.offsetToDirection[3 * dz + dx];
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0002647C File Offset: 0x0002467C
		public Vector3 ProjectOnSurface(Vector3 point)
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			Vector3 vector = (Vector3)this.position;
			Vector3 vector2 = gridGraph.transform.WorldUpAtGraphPosition(vector);
			return point - vector2 * Vector3.Dot(vector2, point - vector);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x000264C8 File Offset: 0x000246C8
		public override Vector3 ClosestPointOnNode(Vector3 p)
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			Vector3 vector = (Vector3)this.position;
			Vector3 vector2 = gridGraph.transform.InverseTransformVector(p - vector);
			vector2.y = 0f;
			vector2.x = Mathf.Clamp(vector2.x, -0.5f, 0.5f);
			vector2.z = Mathf.Clamp(vector2.z, -0.5f, 0.5f);
			return vector + gridGraph.transform.TransformVector(vector2);
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00026558 File Offset: 0x00024758
		public override bool ContainsPoint(Vector3 point)
		{
			GridGraph gridGraph = base.Graph as GridGraph;
			return this.ContainsPointInGraphSpace((Int3)gridGraph.transform.InverseTransform(point));
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00026588 File Offset: 0x00024788
		public override bool ContainsPointInGraphSpace(Int3 point)
		{
			int num = this.XCoordinateInGrid * 1000;
			int num2 = this.ZCoordinateInGrid * 1000;
			return point.x >= num && point.x <= num + 1000 && point.z >= num2 && point.z <= num2 + 1000;
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x000265E4 File Offset: 0x000247E4
		public override float SurfaceArea()
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			return gridGraph.nodeSize * gridGraph.nodeSize;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0002660C File Offset: 0x0002480C
		public override Vector3 RandomPointOnSurface()
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			Vector3 vector = gridGraph.transform.InverseTransform((Vector3)this.position);
			float2 @float = AstarMath.ThreadSafeRandomFloat2();
			return gridGraph.transform.Transform(vector + new Vector3(@float.x - 0.5f, 0f, @float.y - 0.5f));
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00026674 File Offset: 0x00024874
		public Vector2 NormalizePoint(Vector3 worldPoint)
		{
			Vector3 vector = GridNode.GetGridGraph(base.GraphIndex).transform.InverseTransform(worldPoint);
			return new Vector2(vector.x - (float)this.XCoordinateInGrid, vector.z - (float)this.ZCoordinateInGrid);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x000266BC File Offset: 0x000248BC
		public Vector3 UnNormalizePoint(Vector2 normalizedPointOnSurface)
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			return (Vector3)this.position + gridGraph.transform.TransformVector(new Vector3(normalizedPointOnSurface.x - 0.5f, 0f, normalizedPointOnSurface.y - 0.5f));
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00026714 File Offset: 0x00024914
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

		// Token: 0x0600074A RID: 1866
		public abstract GridNodeBase GetNeighbourAlongDirection(int direction);

		// Token: 0x0600074B RID: 1867 RVA: 0x0002676D File Offset: 0x0002496D
		public virtual bool HasConnectionInDirection(int direction)
		{
			return this.GetNeighbourAlongDirection(direction) != null;
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600074C RID: 1868
		public abstract bool HasAnyGridConnections { get; }

		// Token: 0x0600074D RID: 1869 RVA: 0x0002677C File Offset: 0x0002497C
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
			for (int j = 0; j < 8; j++)
			{
				if (node == this.GetNeighbourAlongDirection(j))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600074E RID: 1870
		public abstract void ResetConnectionsInternal();

		// Token: 0x0600074F RID: 1871 RVA: 0x000267E6 File Offset: 0x000249E6
		public override void OpenAtPoint(Path path, uint pathNodeIndex, Int3 pos, uint gScore)
		{
			path.OpenCandidateConnectionsToEndNode(pos, pathNodeIndex, pathNodeIndex, gScore);
			path.OpenCandidateConnection(pathNodeIndex, base.NodeIndex, gScore, 0U, 0U, this.position);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0002680C File Offset: 0x00024A0C
		public override void Open(Path path, uint pathNodeIndex, uint gScore)
		{
			path.OpenCandidateConnectionsToEndNode(this.position, pathNodeIndex, pathNodeIndex, gScore);
			if (this.connections != null)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					GraphNode node = this.connections[i].node;
					if (this.connections[i].isOutgoing && path.CanTraverse(this, node))
					{
						path.OpenCandidateConnection(pathNodeIndex, node.NodeIndex, gScore, this.connections[i].cost, 0U, node.position);
					}
				}
			}
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00026898 File Offset: 0x00024A98
		public void ClearCustomConnections(bool alsoReverse)
		{
			if (this.connections != null)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					this.connections[i].node.RemovePartialConnection(this);
				}
				this.connections = null;
				AstarPath.active.hierarchicalGraph.AddDirtyNode(this);
			}
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x000268EE File Offset: 0x00024AEE
		public override void ClearConnections(bool alsoReverse)
		{
			this.ClearCustomConnections(alsoReverse);
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x000268F8 File Offset: 0x00024AF8
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

		// Token: 0x06000754 RID: 1876 RVA: 0x00026950 File Offset: 0x00024B50
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
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00026A28 File Offset: 0x00024C28
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

		// Token: 0x06000756 RID: 1878 RVA: 0x00026ADD File Offset: 0x00024CDD
		public override void SerializeReferences(GraphSerializationContext ctx)
		{
			ctx.SerializeConnections(this.connections, true);
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00026AEC File Offset: 0x00024CEC
		public override void DeserializeReferences(GraphSerializationContext ctx)
		{
			if (ctx.meta.version < AstarSerializer.V3_8_3)
			{
				return;
			}
			this.connections = ctx.DeserializeConnections(ctx.meta.version >= AstarSerializer.V4_3_85);
		}

		// Token: 0x040004BB RID: 1211
		private const int GridFlagsWalkableErosionOffset = 8;

		// Token: 0x040004BC RID: 1212
		private const int GridFlagsWalkableErosionMask = 256;

		// Token: 0x040004BD RID: 1213
		private const int GridFlagsWalkableTmpOffset = 9;

		// Token: 0x040004BE RID: 1214
		private const int GridFlagsWalkableTmpMask = 512;

		// Token: 0x040004BF RID: 1215
		public const int NodeInGridIndexLayerOffset = 24;

		// Token: 0x040004C0 RID: 1216
		protected const int NodeInGridIndexMask = 16777215;

		// Token: 0x040004C1 RID: 1217
		protected int nodeInGridIndex;

		// Token: 0x040004C2 RID: 1218
		protected ushort gridFlags;

		// Token: 0x040004C3 RID: 1219
		public Connection[] connections;

		// Token: 0x040004C4 RID: 1220
		internal static readonly int[] offsetToDirection = new int[] { 7, 0, 4, 3, -1, 1, 6, 2, 5 };
	}
}
