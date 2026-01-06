using System;
using System.Runtime.CompilerServices;
using Pathfinding.Serialization;
using Pathfinding.Util;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Profiling;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000E6 RID: 230
	[BurstCompile]
	public sealed class TriangleMeshNode : MeshNode
	{
		// Token: 0x0600078D RID: 1933 RVA: 0x0002771D File Offset: 0x0002591D
		public TriangleMeshNode()
		{
			base.HierarchicalNodeIndex = 0;
			base.NodeIndex = 268435454U;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00027737 File Offset: 0x00025937
		public TriangleMeshNode(AstarPath astar)
		{
			astar.InitializeNode(this);
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x00027746 File Offset: 0x00025946
		internal override int PathNodeVariants
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00027749 File Offset: 0x00025949
		[MethodImpl(256)]
		public static INavmeshHolder GetNavmeshHolder(uint graphIndex)
		{
			return TriangleMeshNode._navmeshHolders[(int)graphIndex];
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x00027752 File Offset: 0x00025952
		public int TileIndex
		{
			get
			{
				return (this.v0 >> 12) & 524287;
			}
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00027764 File Offset: 0x00025964
		public static void SetNavmeshHolder(int graphIndex, INavmeshHolder graph)
		{
			object obj = TriangleMeshNode.lockObject;
			lock (obj)
			{
				if (graphIndex >= TriangleMeshNode._navmeshHolders.Length)
				{
					INavmeshHolder[] array = new INavmeshHolder[graphIndex + 1];
					TriangleMeshNode._navmeshHolders.CopyTo(array, 0);
					TriangleMeshNode._navmeshHolders = array;
				}
				TriangleMeshNode._navmeshHolders[graphIndex] = graph;
			}
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x000277CC File Offset: 0x000259CC
		public static void ClearNavmeshHolder(int graphIndex, INavmeshHolder graph)
		{
			object obj = TriangleMeshNode.lockObject;
			lock (obj)
			{
				if (graphIndex < TriangleMeshNode._navmeshHolders.Length && TriangleMeshNode._navmeshHolders[graphIndex] == graph)
				{
					TriangleMeshNode._navmeshHolders[graphIndex] = null;
				}
			}
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00027824 File Offset: 0x00025A24
		public void UpdatePositionFromVertices()
		{
			Int3 @int;
			Int3 int2;
			Int3 int3;
			this.GetVertices(out @int, out int2, out int3);
			this.position = (@int + int2 + int3) * 0.333333f;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0002785A File Offset: 0x00025A5A
		[MethodImpl(256)]
		public int GetVertexIndex(int i)
		{
			if (i == 0)
			{
				return this.v0;
			}
			if (i != 1)
			{
				return this.v2;
			}
			return this.v1;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00027877 File Offset: 0x00025A77
		public int GetVertexArrayIndex(int i)
		{
			return TriangleMeshNode.GetNavmeshHolder(base.GraphIndex).GetVertexArrayIndex((i == 0) ? this.v0 : ((i == 1) ? this.v1 : this.v2));
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x000278A8 File Offset: 0x00025AA8
		public void GetVertices(out Int3 v0, out Int3 v1, out Int3 v2)
		{
			INavmeshHolder navmeshHolder = TriangleMeshNode.GetNavmeshHolder(base.GraphIndex);
			v0 = navmeshHolder.GetVertex(this.v0);
			v1 = navmeshHolder.GetVertex(this.v1);
			v2 = navmeshHolder.GetVertex(this.v2);
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x000278F8 File Offset: 0x00025AF8
		public void GetVerticesInGraphSpace(out Int3 v0, out Int3 v1, out Int3 v2)
		{
			INavmeshHolder navmeshHolder = TriangleMeshNode.GetNavmeshHolder(base.GraphIndex);
			v0 = navmeshHolder.GetVertexInGraphSpace(this.v0);
			v1 = navmeshHolder.GetVertexInGraphSpace(this.v1);
			v2 = navmeshHolder.GetVertexInGraphSpace(this.v2);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00027947 File Offset: 0x00025B47
		[MethodImpl(256)]
		public override Int3 GetVertex(int i)
		{
			return TriangleMeshNode.GetNavmeshHolder(base.GraphIndex).GetVertex(this.GetVertexIndex(i));
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00027960 File Offset: 0x00025B60
		public Int3 GetVertexInGraphSpace(int i)
		{
			return TriangleMeshNode.GetNavmeshHolder(base.GraphIndex).GetVertexInGraphSpace(this.GetVertexIndex(i));
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00027746 File Offset: 0x00025946
		public override int GetVertexCount()
		{
			return 3;
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0002797C File Offset: 0x00025B7C
		public Vector3 ProjectOnSurface(Vector3 point)
		{
			Int3 @int;
			Int3 int2;
			Int3 int3;
			this.GetVertices(out @int, out int2, out int3);
			Vector3 vector = (Vector3)@int;
			Vector3 vector2 = (Vector3)int2;
			Vector3 vector3 = (Vector3)int3;
			Vector3 normalized = Vector3.Cross(vector2 - vector, vector3 - vector).normalized;
			return point - normalized * Vector3.Dot(normalized, point - vector);
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x000279E4 File Offset: 0x00025BE4
		public override Vector3 ClosestPointOnNode(Vector3 p)
		{
			Int3 @int;
			Int3 int2;
			Int3 int3;
			this.GetVertices(out @int, out int2, out int3);
			return Polygon.ClosestPointOnTriangle((Vector3)@int, (Vector3)int2, (Vector3)int3, p);
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00027A30 File Offset: 0x00025C30
		internal Int3 ClosestPointOnNodeXZInGraphSpace(Vector3 p)
		{
			Int3 @int;
			Int3 int2;
			Int3 int3;
			this.GetVerticesInGraphSpace(out @int, out int2, out int3);
			p = TriangleMeshNode.GetNavmeshHolder(base.GraphIndex).transform.InverseTransform(p);
			Int3 int4 = (Int3)Polygon.ClosestPointOnTriangleXZ((Vector3)@int, (Vector3)int2, (Vector3)int3, p);
			if (this.ContainsPointInGraphSpace(int4))
			{
				return int4;
			}
			for (int i = -1; i <= 1; i++)
			{
				for (int j = -1; j <= 1; j++)
				{
					if (i != 0 || j != 0)
					{
						Int3 int5 = new Int3(int4.x + i, int4.y, int4.z + j);
						if (this.ContainsPointInGraphSpace(int5))
						{
							return int5;
						}
					}
				}
			}
			long sqrMagnitudeLong = (@int - int4).sqrMagnitudeLong;
			long sqrMagnitudeLong2 = (int2 - int4).sqrMagnitudeLong;
			long sqrMagnitudeLong3 = (int3 - int4).sqrMagnitudeLong;
			if (sqrMagnitudeLong >= sqrMagnitudeLong2)
			{
				if (sqrMagnitudeLong2 >= sqrMagnitudeLong3)
				{
					return int3;
				}
				return int2;
			}
			else
			{
				if (sqrMagnitudeLong >= sqrMagnitudeLong3)
				{
					return int3;
				}
				return @int;
			}
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00027B30 File Offset: 0x00025D30
		public override Vector3 ClosestPointOnNodeXZ(Vector3 p)
		{
			Int3 @int;
			Int3 int2;
			Int3 int3;
			this.GetVertices(out @int, out int2, out int3);
			return Polygon.ClosestPointOnTriangleXZ((Vector3)@int, (Vector3)int2, (Vector3)int3, p);
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00027B61 File Offset: 0x00025D61
		public override bool ContainsPoint(Vector3 p)
		{
			return this.ContainsPointInGraphSpace((Int3)TriangleMeshNode.GetNavmeshHolder(base.GraphIndex).transform.InverseTransform(p));
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00027B84 File Offset: 0x00025D84
		public bool ContainsPoint(Vector3 p, NativeMovementPlane movementPlane)
		{
			Int3 @int;
			Int3 int2;
			Int3 int3;
			this.GetVertices(out @int, out int2, out int3);
			int3 int4 = (int3)@int;
			int3 int5 = (int3)int2;
			int3 int6 = (int3)int3;
			int3 int7 = (int3)((Int3)p);
			return Polygon.ContainsPoint(ref int4, ref int5, ref int6, ref int7, ref movementPlane);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00027BD0 File Offset: 0x00025DD0
		public override bool ContainsPointInGraphSpace(Int3 p)
		{
			Int3 @int;
			Int3 int2;
			Int3 int3;
			this.GetVerticesInGraphSpace(out @int, out int2, out int3);
			return (long)(int2.x - @int.x) * (long)(p.z - @int.z) - (long)(p.x - @int.x) * (long)(int2.z - @int.z) <= 0L && (long)(int3.x - int2.x) * (long)(p.z - int2.z) - (long)(p.x - int2.x) * (long)(int3.z - int2.z) <= 0L && (long)(@int.x - int3.x) * (long)(p.z - int3.z) - (long)(p.x - int3.x) * (long)(@int.z - int3.z) <= 0L;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00027CB0 File Offset: 0x00025EB0
		public override Int3 DecodeVariantPosition(uint pathNodeIndex, uint fractionAlongEdge)
		{
			int num = (int)(pathNodeIndex - base.NodeIndex);
			Int3 vertex = this.GetVertex(num);
			Int3 vertex2 = this.GetVertex((num + 1) % 3);
			Int3 @int;
			TriangleMeshNode.InterpolateEdge(ref vertex, ref vertex2, fractionAlongEdge, out @int);
			return @int;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00027CE7 File Offset: 0x00025EE7
		[BurstCompile(FloatMode = FloatMode.Fast)]
		private static void InterpolateEdge(ref Int3 p1, ref Int3 p2, uint fractionAlongEdge, out Int3 pos)
		{
			TriangleMeshNode.InterpolateEdge_00000757$BurstDirectCall.Invoke(ref p1, ref p2, fractionAlongEdge, out pos);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00027CF2 File Offset: 0x00025EF2
		public override void OpenAtPoint(Path path, uint pathNodeIndex, Int3 point, uint gScore)
		{
			this.OpenAtPoint(path, pathNodeIndex, point, -1, gScore);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00027D00 File Offset: 0x00025F00
		public override void Open(Path path, uint pathNodeIndex, uint gScore)
		{
			PathHandler pathHandler = ((IPathInternals)path).PathHandler;
			int num = (int)(pathNodeIndex - base.NodeIndex);
			this.OpenAtPoint(path, pathNodeIndex, this.DecodeVariantPosition(pathNodeIndex, pathHandler.pathNodes[pathNodeIndex].fractionAlongEdge), num, gScore);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00027D40 File Offset: 0x00025F40
		private unsafe void OpenAtPoint(Path path, uint pathNodeIndex, Int3 pos, int edge, uint gScore)
		{
			PathHandler pathHandler = ((IPathInternals)path).PathHandler;
			PathNode pathNode = *pathHandler.pathNodes[pathNodeIndex];
			if (pathNode.flag1)
			{
				path.OpenCandidateConnectionsToEndNode(pos, pathNodeIndex, base.NodeIndex, gScore);
			}
			int num = 0;
			bool flag = pathNode.parentIndex >= base.NodeIndex && pathNode.parentIndex < base.NodeIndex + 3U;
			if (this.connections != null)
			{
				for (int i = this.connections.Length - 1; i >= 0; i--)
				{
					Connection connection = this.connections[i];
					if (connection.isOutgoing)
					{
						GraphNode node = connection.node;
						if (connection.isEdgeShared)
						{
							int adjacentShapeEdge = connection.adjacentShapeEdge;
							uint num2 = node.NodeIndex + (uint)adjacentShapeEdge;
							if (num2 != pathNode.parentIndex)
							{
								if (connection.shapeEdge == edge)
								{
									if (path.CanTraverse(this, node))
									{
										TriangleMeshNode triangleMeshNode = node as TriangleMeshNode;
										if (path.ShouldConsiderPathNode(num2))
										{
											if (connection.edgesAreIdentical)
											{
												uint traversalCost = path.GetTraversalCost(node);
												ref PathNode ptr = ref pathHandler.pathNodes[num2];
												ptr.pathID = path.pathID;
												ptr.heapIndex = ushort.MaxValue;
												ptr.parentIndex = pathNodeIndex;
												ptr.fractionAlongEdge = PathNode.ReverseFractionAlongEdge(pathNode.fractionAlongEdge);
												path.OnVisitNode(num2, uint.MaxValue, gScore + traversalCost);
												pathHandler.LogVisitedNode(num2, uint.MaxValue, gScore + traversalCost);
												triangleMeshNode.OpenAtPoint(path, num2, pos, adjacentShapeEdge, gScore + traversalCost);
											}
											else
											{
												this.OpenSingleEdge(path, pathNodeIndex, triangleMeshNode, adjacentShapeEdge, pos, gScore);
											}
										}
									}
								}
								else if (!flag && (num & (1 << connection.shapeEdge)) == 0)
								{
									num |= 1 << connection.shapeEdge;
									this.OpenSingleEdge(path, pathNodeIndex, this, connection.shapeEdge, pos, gScore);
								}
							}
						}
						else if (!flag && path.CanTraverse(this, node) && path.ShouldConsiderPathNode(node.NodeIndex))
						{
							uint costMagnitude = (uint)(node.position - pos).costMagnitude;
							if (edge != -1)
							{
								path.OpenCandidateConnection(pathNodeIndex, node.NodeIndex, gScore, costMagnitude, 0U, node.position);
							}
							else
							{
								uint num3 = pathHandler.AddTemporaryNode(new TemporaryNode
								{
									associatedNode = base.NodeIndex,
									position = pos,
									targetIndex = 0,
									type = TemporaryNodeType.Ignore
								});
								ref PathNode ptr2 = ref pathHandler.pathNodes[num3];
								ptr2.pathID = path.pathID;
								ptr2.parentIndex = pathNodeIndex;
								path.OpenCandidateConnection(num3, node.NodeIndex, gScore, costMagnitude, 0U, node.position);
							}
						}
					}
				}
			}
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00027FFC File Offset: 0x000261FC
		private void OpenSingleEdge(Path path, uint pathNodeIndex, TriangleMeshNode other, int sharedEdgeOnOtherNode, Int3 pos, uint gScore)
		{
			uint num = other.NodeIndex + (uint)sharedEdgeOnOtherNode;
			if (!path.ShouldConsiderPathNode(num))
			{
				return;
			}
			Int3 vertex = other.GetVertex(sharedEdgeOnOtherNode);
			Int3 vertex2 = other.GetVertex((sharedEdgeOnOtherNode + 1) % 3);
			PathHandler pathHandler = ((IPathInternals)path).PathHandler;
			uint traversalCost = path.GetTraversalCost(other);
			uint num2 = gScore + traversalCost;
			TriangleMeshNode.OpenSingleEdgeBurst(ref vertex, ref vertex2, ref pos, path.pathID, pathNodeIndex, num, other.NodeIndex, num2, ref pathHandler.pathNodes, ref pathHandler.heap, path.heuristicObjectiveInternal);
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00028078 File Offset: 0x00026278
		[BurstCompile]
		private static void OpenSingleEdgeBurst(ref Int3 s1, ref Int3 s2, ref Int3 pos, ushort pathID, uint pathNodeIndex, uint candidatePathNodeIndex, uint candidateNodeIndex, uint candidateG, ref UnsafeSpan<PathNode> pathNodes, ref BinaryHeap heap, ref HeuristicObjective heuristicObjective)
		{
			TriangleMeshNode.OpenSingleEdgeBurst_0000075C$BurstDirectCall.Invoke(ref s1, ref s2, ref pos, pathID, pathNodeIndex, candidatePathNodeIndex, candidateNodeIndex, candidateG, ref pathNodes, ref heap, ref heuristicObjective);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0002809C File Offset: 0x0002629C
		[BurstCompile]
		private static void CalculateBestEdgePosition(ref Int3 s1, ref Int3 s2, ref Int3 pos, out int3 closestPointAlongEdge, out uint quantizedFractionAlongEdge, out uint cost)
		{
			TriangleMeshNode.CalculateBestEdgePosition_0000075D$BurstDirectCall.Invoke(ref s1, ref s2, ref pos, out closestPointAlongEdge, out quantizedFractionAlongEdge, out cost);
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x000280AC File Offset: 0x000262AC
		public int SharedEdge(GraphNode other)
		{
			int num = -1;
			if (this.connections != null)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					if (this.connections[i].node == other && this.connections[i].isEdgeShared)
					{
						num = this.connections[i].shapeEdge;
					}
				}
			}
			return num;
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00028110 File Offset: 0x00026310
		public override bool GetPortal(GraphNode toNode, out Vector3 left, out Vector3 right)
		{
			int num;
			int num2;
			return this.GetPortal(toNode, out left, out right, out num, out num2);
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0002812C File Offset: 0x0002632C
		public bool GetPortalInGraphSpace(TriangleMeshNode toNode, out Int3 a, out Int3 b, out int aIndex, out int bIndex)
		{
			aIndex = -1;
			bIndex = -1;
			a = Int3.zero;
			b = Int3.zero;
			if (toNode.GraphIndex != base.GraphIndex)
			{
				return false;
			}
			int num = -1;
			int num2 = -1;
			if (this.connections != null)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					if (this.connections[i].node == toNode && this.connections[i].isEdgeShared)
					{
						num = this.connections[i].shapeEdge;
						num2 = this.connections[i].adjacentShapeEdge;
					}
				}
			}
			if (num == -1)
			{
				return false;
			}
			aIndex = num;
			bIndex = (num + 1) % 3;
			INavmeshHolder navmeshHolder = TriangleMeshNode.GetNavmeshHolder(base.GraphIndex);
			a = navmeshHolder.GetVertexInGraphSpace(this.GetVertexIndex(aIndex));
			b = navmeshHolder.GetVertexInGraphSpace(this.GetVertexIndex(bIndex));
			int tileIndex = this.TileIndex;
			int tileIndex2 = toNode.TileIndex;
			if (tileIndex != tileIndex2)
			{
				Int3 vertexInGraphSpace = toNode.GetVertexInGraphSpace(num2);
				Int3 vertexInGraphSpace2 = toNode.GetVertexInGraphSpace((num2 + 1) % 3);
				int num3;
				int num4;
				navmeshHolder.GetTileCoordinates(tileIndex, out num3, out num4);
				int num5;
				int num6;
				navmeshHolder.GetTileCoordinates(tileIndex2, out num5, out num6);
				int num7 = ((num3 == num5) ? 0 : 2);
				int num8 = Mathf.Min(vertexInGraphSpace[num7], vertexInGraphSpace2[num7]);
				int num9 = Mathf.Max(vertexInGraphSpace[num7], vertexInGraphSpace2[num7]);
				a[num7] = Mathf.Clamp(a[num7], num8, num9);
				b[num7] = Mathf.Clamp(b[num7], num8, num9);
			}
			return true;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x000282D8 File Offset: 0x000264D8
		public bool GetPortal(GraphNode toNode, out Vector3 left, out Vector3 right, out int aIndex, out int bIndex)
		{
			TriangleMeshNode triangleMeshNode = toNode as TriangleMeshNode;
			Int3 @int;
			Int3 int2;
			if (triangleMeshNode != null && this.GetPortalInGraphSpace(triangleMeshNode, out @int, out int2, out aIndex, out bIndex))
			{
				INavmeshHolder navmeshHolder = TriangleMeshNode.GetNavmeshHolder(base.GraphIndex);
				left = navmeshHolder.transform.Transform((Vector3)@int);
				right = navmeshHolder.transform.Transform((Vector3)int2);
				return true;
			}
			aIndex = -1;
			bIndex = -1;
			left = Vector3.zero;
			right = Vector3.zero;
			return false;
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0002835C File Offset: 0x0002655C
		public override float SurfaceArea()
		{
			INavmeshHolder navmeshHolder = TriangleMeshNode.GetNavmeshHolder(base.GraphIndex);
			return (float)Math.Abs(VectorMath.SignedTriangleAreaTimes2XZ(navmeshHolder.GetVertex(this.v0), navmeshHolder.GetVertex(this.v1), navmeshHolder.GetVertex(this.v2))) * 0.5f;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x000283AC File Offset: 0x000265AC
		public override Vector3 RandomPointOnSurface()
		{
			float2 @float;
			do
			{
				@float = AstarMath.ThreadSafeRandomFloat2();
			}
			while (@float.x + @float.y > 1f);
			Int3 @int;
			Int3 int2;
			Int3 int3;
			this.GetVertices(out @int, out int2, out int3);
			return (Vector3)(int2 - @int) * @float.x + (Vector3)(int3 - @int) * @float.y + (Vector3)@int;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0002841D File Offset: 0x0002661D
		public override void SerializeNode(GraphSerializationContext ctx)
		{
			base.SerializeNode(ctx);
			ctx.writer.Write(this.v0);
			ctx.writer.Write(this.v1);
			ctx.writer.Write(this.v2);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00028459 File Offset: 0x00026659
		public override void DeserializeNode(GraphSerializationContext ctx)
		{
			base.DeserializeNode(ctx);
			this.v0 = ctx.reader.ReadInt32();
			this.v1 = ctx.reader.ReadInt32();
			this.v2 = ctx.reader.ReadInt32();
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x000284E8 File Offset: 0x000266E8
		[BurstCompile(FloatMode = FloatMode.Fast)]
		[MethodImpl(256)]
		public static void InterpolateEdge$BurstManaged(ref Int3 p1, ref Int3 p2, uint fractionAlongEdge, out Int3 pos)
		{
			int3 @int = (int3)math.lerp((int3)p1, (int3)p2, PathNode.UnQuantizeFractionAlongEdge(fractionAlongEdge));
			pos = new Int3(@int.x, @int.y, @int.z);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00028544 File Offset: 0x00026744
		[BurstCompile]
		[MethodImpl(256)]
		public static void OpenSingleEdgeBurst$BurstManaged(ref Int3 s1, ref Int3 s2, ref Int3 pos, ushort pathID, uint pathNodeIndex, uint candidatePathNodeIndex, uint candidateNodeIndex, uint candidateG, ref UnsafeSpan<PathNode> pathNodes, ref BinaryHeap heap, ref HeuristicObjective heuristicObjective)
		{
			int3 @int;
			uint num;
			uint num2;
			TriangleMeshNode.CalculateBestEdgePosition(ref s1, ref s2, ref pos, out @int, out num, out num2);
			candidateG += num2;
			Path.OpenCandidateParams openCandidateParams = new Path.OpenCandidateParams
			{
				pathID = pathID,
				parentPathNode = pathNodeIndex,
				targetPathNode = candidatePathNodeIndex,
				targetNodeIndex = candidateNodeIndex,
				candidateG = candidateG,
				fractionAlongEdge = num,
				targetNodePosition = @int,
				pathNodes = pathNodes
			};
			Path.OpenCandidateConnectionBurst(ref openCandidateParams, ref heap, ref heuristicObjective);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x000285C8 File Offset: 0x000267C8
		[BurstCompile]
		[MethodImpl(256)]
		public static void CalculateBestEdgePosition$BurstManaged(ref Int3 s1, ref Int3 s2, ref Int3 pos, out int3 closestPointAlongEdge, out uint quantizedFractionAlongEdge, out uint cost)
		{
			float3 @float = (int3)s1;
			float3 float2 = (int3)s2;
			int3 @int = (int3)pos;
			float num = math.clamp(VectorMath.ClosestPointOnLineFactor(@float, float2, @int), 0f, 1f);
			quantizedFractionAlongEdge = PathNode.QuantizeFractionAlongEdge(num);
			num = PathNode.UnQuantizeFractionAlongEdge(quantizedFractionAlongEdge);
			float3 float3 = math.lerp(@float, float2, num);
			closestPointAlongEdge = (int3)float3;
			int3 int2 = @int - closestPointAlongEdge;
			cost = (uint)new Int3(int2.x, int2.y, int2.z).costMagnitude;
		}

		// Token: 0x040004D2 RID: 1234
		public const bool InaccuratePathSearch = false;

		// Token: 0x040004D3 RID: 1235
		public int v0;

		// Token: 0x040004D4 RID: 1236
		public int v1;

		// Token: 0x040004D5 RID: 1237
		public int v2;

		// Token: 0x040004D6 RID: 1238
		private static INavmeshHolder[] _navmeshHolders = new INavmeshHolder[0];

		// Token: 0x040004D7 RID: 1239
		private static readonly object lockObject = new object();

		// Token: 0x040004D8 RID: 1240
		public static readonly ProfilerMarker MarkerDecode = new ProfilerMarker("Decode");

		// Token: 0x040004D9 RID: 1241
		public static readonly ProfilerMarker MarkerGetVertices = new ProfilerMarker("GetVertex");

		// Token: 0x040004DA RID: 1242
		public static readonly ProfilerMarker MarkerClosest = new ProfilerMarker("MarkerClosest");

		// Token: 0x020000E7 RID: 231
		// (Invoke) Token: 0x060007B8 RID: 1976
		public delegate void InterpolateEdge_00000757$PostfixBurstDelegate(ref Int3 p1, ref Int3 p2, uint fractionAlongEdge, out Int3 pos);

		// Token: 0x020000E8 RID: 232
		internal static class InterpolateEdge_00000757$BurstDirectCall
		{
			// Token: 0x060007BB RID: 1979 RVA: 0x00028679 File Offset: 0x00026879
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (TriangleMeshNode.InterpolateEdge_00000757$BurstDirectCall.Pointer == 0)
				{
					TriangleMeshNode.InterpolateEdge_00000757$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(TriangleMeshNode.InterpolateEdge_00000757$BurstDirectCall.DeferredCompilation, methodof(TriangleMeshNode.InterpolateEdge$BurstManaged(ref Int3, ref Int3, uint, ref Int3)).MethodHandle, typeof(TriangleMeshNode.InterpolateEdge_00000757$PostfixBurstDelegate).TypeHandle);
				}
				A_0 = TriangleMeshNode.InterpolateEdge_00000757$BurstDirectCall.Pointer;
			}

			// Token: 0x060007BC RID: 1980 RVA: 0x000286A8 File Offset: 0x000268A8
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				TriangleMeshNode.InterpolateEdge_00000757$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x060007BD RID: 1981 RVA: 0x000286C0 File Offset: 0x000268C0
			public static void Constructor()
			{
				TriangleMeshNode.InterpolateEdge_00000757$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(TriangleMeshNode.InterpolateEdge(ref Int3, ref Int3, uint, ref Int3)).MethodHandle);
			}

			// Token: 0x060007BE RID: 1982 RVA: 0x000033F6 File Offset: 0x000015F6
			public static void Initialize()
			{
			}

			// Token: 0x060007BF RID: 1983 RVA: 0x000286D1 File Offset: 0x000268D1
			// Note: this type is marked as 'beforefieldinit'.
			static InterpolateEdge_00000757$BurstDirectCall()
			{
				TriangleMeshNode.InterpolateEdge_00000757$BurstDirectCall.Constructor();
			}

			// Token: 0x060007C0 RID: 1984 RVA: 0x000286D8 File Offset: 0x000268D8
			public static void Invoke(ref Int3 p1, ref Int3 p2, uint fractionAlongEdge, out Int3 pos)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = TriangleMeshNode.InterpolateEdge_00000757$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						calli(System.Void(Pathfinding.Int3&,Pathfinding.Int3&,System.UInt32,Pathfinding.Int3&), ref p1, ref p2, fractionAlongEdge, ref pos, functionPointer);
						return;
					}
				}
				TriangleMeshNode.InterpolateEdge$BurstManaged(ref p1, ref p2, fractionAlongEdge, out pos);
			}

			// Token: 0x040004DB RID: 1243
			private static IntPtr Pointer;

			// Token: 0x040004DC RID: 1244
			private static IntPtr DeferredCompilation;
		}

		// Token: 0x020000E9 RID: 233
		// (Invoke) Token: 0x060007C2 RID: 1986
		public delegate void OpenSingleEdgeBurst_0000075C$PostfixBurstDelegate(ref Int3 s1, ref Int3 s2, ref Int3 pos, ushort pathID, uint pathNodeIndex, uint candidatePathNodeIndex, uint candidateNodeIndex, uint candidateG, ref UnsafeSpan<PathNode> pathNodes, ref BinaryHeap heap, ref HeuristicObjective heuristicObjective);

		// Token: 0x020000EA RID: 234
		internal static class OpenSingleEdgeBurst_0000075C$BurstDirectCall
		{
			// Token: 0x060007C5 RID: 1989 RVA: 0x0002870F File Offset: 0x0002690F
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (TriangleMeshNode.OpenSingleEdgeBurst_0000075C$BurstDirectCall.Pointer == 0)
				{
					TriangleMeshNode.OpenSingleEdgeBurst_0000075C$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(TriangleMeshNode.OpenSingleEdgeBurst_0000075C$BurstDirectCall.DeferredCompilation, methodof(TriangleMeshNode.OpenSingleEdgeBurst$BurstManaged(ref Int3, ref Int3, ref Int3, ushort, uint, uint, uint, uint, ref UnsafeSpan<PathNode>, ref BinaryHeap, ref HeuristicObjective)).MethodHandle, typeof(TriangleMeshNode.OpenSingleEdgeBurst_0000075C$PostfixBurstDelegate).TypeHandle);
				}
				A_0 = TriangleMeshNode.OpenSingleEdgeBurst_0000075C$BurstDirectCall.Pointer;
			}

			// Token: 0x060007C6 RID: 1990 RVA: 0x0002873C File Offset: 0x0002693C
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				TriangleMeshNode.OpenSingleEdgeBurst_0000075C$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x060007C7 RID: 1991 RVA: 0x00028754 File Offset: 0x00026954
			public static void Constructor()
			{
				TriangleMeshNode.OpenSingleEdgeBurst_0000075C$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(TriangleMeshNode.OpenSingleEdgeBurst(ref Int3, ref Int3, ref Int3, ushort, uint, uint, uint, uint, ref UnsafeSpan<PathNode>, ref BinaryHeap, ref HeuristicObjective)).MethodHandle);
			}

			// Token: 0x060007C8 RID: 1992 RVA: 0x000033F6 File Offset: 0x000015F6
			public static void Initialize()
			{
			}

			// Token: 0x060007C9 RID: 1993 RVA: 0x00028765 File Offset: 0x00026965
			// Note: this type is marked as 'beforefieldinit'.
			static OpenSingleEdgeBurst_0000075C$BurstDirectCall()
			{
				TriangleMeshNode.OpenSingleEdgeBurst_0000075C$BurstDirectCall.Constructor();
			}

			// Token: 0x060007CA RID: 1994 RVA: 0x0002876C File Offset: 0x0002696C
			public static void Invoke(ref Int3 s1, ref Int3 s2, ref Int3 pos, ushort pathID, uint pathNodeIndex, uint candidatePathNodeIndex, uint candidateNodeIndex, uint candidateG, ref UnsafeSpan<PathNode> pathNodes, ref BinaryHeap heap, ref HeuristicObjective heuristicObjective)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = TriangleMeshNode.OpenSingleEdgeBurst_0000075C$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						calli(System.Void(Pathfinding.Int3&,Pathfinding.Int3&,Pathfinding.Int3&,System.UInt16,System.UInt32,System.UInt32,System.UInt32,System.UInt32,Pathfinding.Util.UnsafeSpan`1<Pathfinding.PathNode>&,Pathfinding.BinaryHeap&,Pathfinding.HeuristicObjective&), ref s1, ref s2, ref pos, pathID, pathNodeIndex, candidatePathNodeIndex, candidateNodeIndex, candidateG, ref pathNodes, ref heap, ref heuristicObjective, functionPointer);
						return;
					}
				}
				TriangleMeshNode.OpenSingleEdgeBurst$BurstManaged(ref s1, ref s2, ref pos, pathID, pathNodeIndex, candidatePathNodeIndex, candidateNodeIndex, candidateG, ref pathNodes, ref heap, ref heuristicObjective);
			}

			// Token: 0x040004DD RID: 1245
			private static IntPtr Pointer;

			// Token: 0x040004DE RID: 1246
			private static IntPtr DeferredCompilation;
		}

		// Token: 0x020000EB RID: 235
		// (Invoke) Token: 0x060007CC RID: 1996
		public delegate void CalculateBestEdgePosition_0000075D$PostfixBurstDelegate(ref Int3 s1, ref Int3 s2, ref Int3 pos, out int3 closestPointAlongEdge, out uint quantizedFractionAlongEdge, out uint cost);

		// Token: 0x020000EC RID: 236
		internal static class CalculateBestEdgePosition_0000075D$BurstDirectCall
		{
			// Token: 0x060007CF RID: 1999 RVA: 0x000287BF File Offset: 0x000269BF
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (TriangleMeshNode.CalculateBestEdgePosition_0000075D$BurstDirectCall.Pointer == 0)
				{
					TriangleMeshNode.CalculateBestEdgePosition_0000075D$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(TriangleMeshNode.CalculateBestEdgePosition_0000075D$BurstDirectCall.DeferredCompilation, methodof(TriangleMeshNode.CalculateBestEdgePosition$BurstManaged(ref Int3, ref Int3, ref Int3, ref int3, ref uint, ref uint)).MethodHandle, typeof(TriangleMeshNode.CalculateBestEdgePosition_0000075D$PostfixBurstDelegate).TypeHandle);
				}
				A_0 = TriangleMeshNode.CalculateBestEdgePosition_0000075D$BurstDirectCall.Pointer;
			}

			// Token: 0x060007D0 RID: 2000 RVA: 0x000287EC File Offset: 0x000269EC
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				TriangleMeshNode.CalculateBestEdgePosition_0000075D$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x060007D1 RID: 2001 RVA: 0x00028804 File Offset: 0x00026A04
			public static void Constructor()
			{
				TriangleMeshNode.CalculateBestEdgePosition_0000075D$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(TriangleMeshNode.CalculateBestEdgePosition(ref Int3, ref Int3, ref Int3, ref int3, ref uint, ref uint)).MethodHandle);
			}

			// Token: 0x060007D2 RID: 2002 RVA: 0x000033F6 File Offset: 0x000015F6
			public static void Initialize()
			{
			}

			// Token: 0x060007D3 RID: 2003 RVA: 0x00028815 File Offset: 0x00026A15
			// Note: this type is marked as 'beforefieldinit'.
			static CalculateBestEdgePosition_0000075D$BurstDirectCall()
			{
				TriangleMeshNode.CalculateBestEdgePosition_0000075D$BurstDirectCall.Constructor();
			}

			// Token: 0x060007D4 RID: 2004 RVA: 0x0002881C File Offset: 0x00026A1C
			public static void Invoke(ref Int3 s1, ref Int3 s2, ref Int3 pos, out int3 closestPointAlongEdge, out uint quantizedFractionAlongEdge, out uint cost)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = TriangleMeshNode.CalculateBestEdgePosition_0000075D$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						calli(System.Void(Pathfinding.Int3&,Pathfinding.Int3&,Pathfinding.Int3&,Unity.Mathematics.int3&,System.UInt32&,System.UInt32&), ref s1, ref s2, ref pos, ref closestPointAlongEdge, ref quantizedFractionAlongEdge, ref cost, functionPointer);
						return;
					}
				}
				TriangleMeshNode.CalculateBestEdgePosition$BurstManaged(ref s1, ref s2, ref pos, out closestPointAlongEdge, out quantizedFractionAlongEdge, out cost);
			}

			// Token: 0x040004DF RID: 1247
			private static IntPtr Pointer;

			// Token: 0x040004E0 RID: 1248
			private static IntPtr DeferredCompilation;
		}
	}
}
