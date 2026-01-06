using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000067 RID: 103
	public class TriangleMeshNode : MeshNode
	{
		// Token: 0x06000577 RID: 1399 RVA: 0x0001EEE4 File Offset: 0x0001D0E4
		public TriangleMeshNode(AstarPath astar)
			: base(astar)
		{
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0001EEED File Offset: 0x0001D0ED
		public static INavmeshHolder GetNavmeshHolder(uint graphIndex)
		{
			return TriangleMeshNode._navmeshHolders[(int)graphIndex];
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0001EEF8 File Offset: 0x0001D0F8
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

		// Token: 0x0600057A RID: 1402 RVA: 0x0001EF60 File Offset: 0x0001D160
		public void UpdatePositionFromVertices()
		{
			Int3 @int;
			Int3 int2;
			Int3 int3;
			this.GetVertices(out @int, out int2, out int3);
			this.position = (@int + int2 + int3) * 0.333333f;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0001EF96 File Offset: 0x0001D196
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

		// Token: 0x0600057C RID: 1404 RVA: 0x0001EFB3 File Offset: 0x0001D1B3
		public int GetVertexArrayIndex(int i)
		{
			return TriangleMeshNode.GetNavmeshHolder(base.GraphIndex).GetVertexArrayIndex((i == 0) ? this.v0 : ((i == 1) ? this.v1 : this.v2));
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0001EFE4 File Offset: 0x0001D1E4
		public void GetVertices(out Int3 v0, out Int3 v1, out Int3 v2)
		{
			INavmeshHolder navmeshHolder = TriangleMeshNode.GetNavmeshHolder(base.GraphIndex);
			v0 = navmeshHolder.GetVertex(this.v0);
			v1 = navmeshHolder.GetVertex(this.v1);
			v2 = navmeshHolder.GetVertex(this.v2);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0001F034 File Offset: 0x0001D234
		public void GetVerticesInGraphSpace(out Int3 v0, out Int3 v1, out Int3 v2)
		{
			INavmeshHolder navmeshHolder = TriangleMeshNode.GetNavmeshHolder(base.GraphIndex);
			v0 = navmeshHolder.GetVertexInGraphSpace(this.v0);
			v1 = navmeshHolder.GetVertexInGraphSpace(this.v1);
			v2 = navmeshHolder.GetVertexInGraphSpace(this.v2);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0001F083 File Offset: 0x0001D283
		public override Int3 GetVertex(int i)
		{
			return TriangleMeshNode.GetNavmeshHolder(base.GraphIndex).GetVertex(this.GetVertexIndex(i));
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0001F09C File Offset: 0x0001D29C
		public Int3 GetVertexInGraphSpace(int i)
		{
			return TriangleMeshNode.GetNavmeshHolder(base.GraphIndex).GetVertexInGraphSpace(this.GetVertexIndex(i));
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0001F0B5 File Offset: 0x0001D2B5
		public override int GetVertexCount()
		{
			return 3;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0001F0B8 File Offset: 0x0001D2B8
		public override Vector3 ClosestPointOnNode(Vector3 p)
		{
			Int3 @int;
			Int3 int2;
			Int3 int3;
			this.GetVertices(out @int, out int2, out int3);
			return Polygon.ClosestPointOnTriangle((Vector3)@int, (Vector3)int2, (Vector3)int3, p);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0001F0EC File Offset: 0x0001D2EC
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

		// Token: 0x06000584 RID: 1412 RVA: 0x0001F1EC File Offset: 0x0001D3EC
		public override Vector3 ClosestPointOnNodeXZ(Vector3 p)
		{
			Int3 @int;
			Int3 int2;
			Int3 int3;
			this.GetVertices(out @int, out int2, out int3);
			return Polygon.ClosestPointOnTriangleXZ((Vector3)@int, (Vector3)int2, (Vector3)int3, p);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0001F21D File Offset: 0x0001D41D
		public override bool ContainsPoint(Vector3 p)
		{
			return this.ContainsPointInGraphSpace((Int3)TriangleMeshNode.GetNavmeshHolder(base.GraphIndex).transform.InverseTransform(p));
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0001F240 File Offset: 0x0001D440
		public override bool ContainsPointInGraphSpace(Int3 p)
		{
			Int3 @int;
			Int3 int2;
			Int3 int3;
			this.GetVerticesInGraphSpace(out @int, out int2, out int3);
			return (long)(int2.x - @int.x) * (long)(p.z - @int.z) - (long)(p.x - @int.x) * (long)(int2.z - @int.z) <= 0L && (long)(int3.x - int2.x) * (long)(p.z - int2.z) - (long)(p.x - int2.x) * (long)(int3.z - int2.z) <= 0L && (long)(@int.x - int3.x) * (long)(p.z - int3.z) - (long)(p.x - int3.x) * (long)(@int.z - int3.z) <= 0L;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0001F320 File Offset: 0x0001D520
		public override void UpdateRecursiveG(Path path, PathNode pathNode, PathHandler handler)
		{
			pathNode.UpdateG(path);
			handler.heap.Add(pathNode);
			if (this.connections == null)
			{
				return;
			}
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

		// Token: 0x06000588 RID: 1416 RVA: 0x0001F398 File Offset: 0x0001D598
		public override void Open(Path path, PathNode pathNode, PathHandler handler)
		{
			if (this.connections == null)
			{
				return;
			}
			bool flag = pathNode.flag2;
			for (int i = this.connections.Length - 1; i >= 0; i--)
			{
				Connection connection = this.connections[i];
				GraphNode node = connection.node;
				if (path.CanTraverse(connection.node))
				{
					PathNode pathNode2 = handler.GetPathNode(connection.node);
					if (pathNode2 != pathNode.parent)
					{
						uint num = connection.cost;
						if (flag || pathNode2.flag2)
						{
							num = path.GetConnectionSpecialCost(this, connection.node, num);
						}
						if (pathNode2.pathID != handler.PathID)
						{
							pathNode2.node = connection.node;
							pathNode2.parent = pathNode;
							pathNode2.pathID = handler.PathID;
							pathNode2.cost = num;
							pathNode2.H = path.CalculateHScore(node);
							pathNode2.UpdateG(path);
							handler.heap.Add(pathNode2);
						}
						else if (pathNode.G + num + path.GetTraversalCost(node) < pathNode2.G)
						{
							pathNode2.cost = num;
							pathNode2.parent = pathNode;
							node.UpdateRecursiveG(path, pathNode2, handler);
						}
					}
				}
			}
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0001F4CC File Offset: 0x0001D6CC
		public int SharedEdge(GraphNode other)
		{
			int num = -1;
			if (this.connections != null)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					if (this.connections[i].node == other)
					{
						num = (int)this.connections[i].shapeEdge;
					}
				}
			}
			return num;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0001F520 File Offset: 0x0001D720
		public override bool GetPortal(GraphNode toNode, List<Vector3> left, List<Vector3> right, bool backwards)
		{
			int num;
			int num2;
			return this.GetPortal(toNode, left, right, backwards, out num, out num2);
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0001F53C File Offset: 0x0001D73C
		public bool GetPortal(GraphNode toNode, List<Vector3> left, List<Vector3> right, bool backwards, out int aIndex, out int bIndex)
		{
			aIndex = -1;
			bIndex = -1;
			if (backwards || toNode.GraphIndex != base.GraphIndex)
			{
				return false;
			}
			TriangleMeshNode triangleMeshNode = toNode as TriangleMeshNode;
			int num = this.SharedEdge(triangleMeshNode);
			if (num == 255)
			{
				return false;
			}
			if (num == -1)
			{
				if (this.connections != null)
				{
					for (int i = 0; i < this.connections.Length; i++)
					{
						if (this.connections[i].node.GraphIndex != base.GraphIndex)
						{
							NodeLink3Node nodeLink3Node = this.connections[i].node as NodeLink3Node;
							if (nodeLink3Node != null && nodeLink3Node.GetOther(this) == triangleMeshNode)
							{
								nodeLink3Node.GetPortal(triangleMeshNode, left, right, false);
								return true;
							}
						}
					}
				}
				return false;
			}
			aIndex = num;
			bIndex = (num + 1) % this.GetVertexCount();
			Int3 vertex = this.GetVertex(num);
			Int3 vertex2 = this.GetVertex((num + 1) % this.GetVertexCount());
			int num2 = (this.GetVertexIndex(0) >> 12) & 524287;
			int num3 = (triangleMeshNode.GetVertexIndex(0) >> 12) & 524287;
			if (num2 != num3)
			{
				INavmeshHolder navmeshHolder = TriangleMeshNode.GetNavmeshHolder(base.GraphIndex);
				int num4;
				int num5;
				navmeshHolder.GetTileCoordinates(num2, out num4, out num5);
				int num6;
				int num7;
				navmeshHolder.GetTileCoordinates(num3, out num6, out num7);
				int num8;
				if (Math.Abs(num4 - num6) == 1)
				{
					num8 = 2;
				}
				else
				{
					if (Math.Abs(num5 - num7) != 1)
					{
						return false;
					}
					num8 = 0;
				}
				int num9 = triangleMeshNode.SharedEdge(this);
				if (num9 == 255)
				{
					throw new Exception("Connection used edge in one direction, but not in the other direction. Has the wrong overload of AddConnection been used?");
				}
				if (num9 != -1)
				{
					int num10 = Math.Min(vertex[num8], vertex2[num8]);
					int num11 = Math.Max(vertex[num8], vertex2[num8]);
					Int3 vertex3 = triangleMeshNode.GetVertex(num9);
					Int3 vertex4 = triangleMeshNode.GetVertex((num9 + 1) % triangleMeshNode.GetVertexCount());
					num10 = Math.Max(num10, Math.Min(vertex3[num8], vertex4[num8]));
					num11 = Math.Min(num11, Math.Max(vertex3[num8], vertex4[num8]));
					if (vertex[num8] < vertex2[num8])
					{
						vertex[num8] = num10;
						vertex2[num8] = num11;
					}
					else
					{
						vertex[num8] = num11;
						vertex2[num8] = num10;
					}
				}
			}
			if (left != null)
			{
				left.Add((Vector3)vertex);
				right.Add((Vector3)vertex2);
			}
			return true;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0001F7AC File Offset: 0x0001D9AC
		public override float SurfaceArea()
		{
			INavmeshHolder navmeshHolder = TriangleMeshNode.GetNavmeshHolder(base.GraphIndex);
			return (float)Math.Abs(VectorMath.SignedTriangleAreaTimes2XZ(navmeshHolder.GetVertex(this.v0), navmeshHolder.GetVertex(this.v1), navmeshHolder.GetVertex(this.v2))) * 0.5f;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0001F7FC File Offset: 0x0001D9FC
		public override Vector3 RandomPointOnSurface()
		{
			float value;
			float value2;
			do
			{
				value = Random.value;
				value2 = Random.value;
			}
			while (value + value2 > 1f);
			INavmeshHolder navmeshHolder = TriangleMeshNode.GetNavmeshHolder(base.GraphIndex);
			return (Vector3)(navmeshHolder.GetVertex(this.v1) - navmeshHolder.GetVertex(this.v0)) * value + (Vector3)(navmeshHolder.GetVertex(this.v2) - navmeshHolder.GetVertex(this.v0)) * value2 + (Vector3)navmeshHolder.GetVertex(this.v0);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0001F896 File Offset: 0x0001DA96
		public override void SerializeNode(GraphSerializationContext ctx)
		{
			base.SerializeNode(ctx);
			ctx.writer.Write(this.v0);
			ctx.writer.Write(this.v1);
			ctx.writer.Write(this.v2);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0001F8D2 File Offset: 0x0001DAD2
		public override void DeserializeNode(GraphSerializationContext ctx)
		{
			base.DeserializeNode(ctx);
			this.v0 = ctx.reader.ReadInt32();
			this.v1 = ctx.reader.ReadInt32();
			this.v2 = ctx.reader.ReadInt32();
		}

		// Token: 0x04000311 RID: 785
		public int v0;

		// Token: 0x04000312 RID: 786
		public int v1;

		// Token: 0x04000313 RID: 787
		public int v2;

		// Token: 0x04000314 RID: 788
		protected static INavmeshHolder[] _navmeshHolders = new INavmeshHolder[0];

		// Token: 0x04000315 RID: 789
		protected static readonly object lockObject = new object();
	}
}
