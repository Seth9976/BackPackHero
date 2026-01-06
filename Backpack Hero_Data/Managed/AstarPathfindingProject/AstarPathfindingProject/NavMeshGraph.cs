using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000062 RID: 98
	[JsonOptIn]
	[Preserve]
	public class NavMeshGraph : NavmeshBase, IUpdatableGraph
	{
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x0001D7F4 File Offset: 0x0001B9F4
		protected override bool RecalculateNormals
		{
			get
			{
				return this.recalculateNormals;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x0001D7FC File Offset: 0x0001B9FC
		public override float TileWorldSizeX
		{
			get
			{
				return this.forcedBoundsSize.x;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x0001D809 File Offset: 0x0001BA09
		public override float TileWorldSizeZ
		{
			get
			{
				return this.forcedBoundsSize.z;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0001D816 File Offset: 0x0001BA16
		protected override float MaxTileConnectionEdgeDistance
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0001D820 File Offset: 0x0001BA20
		public override GraphTransform CalculateTransform()
		{
			return new GraphTransform(Matrix4x4.TRS(this.offset, Quaternion.Euler(this.rotation), Vector3.one) * Matrix4x4.TRS((this.sourceMesh != null) ? (this.sourceMesh.bounds.min * this.scale) : (this.cachedSourceMeshBoundsMin * this.scale), Quaternion.identity, Vector3.one));
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0001D8A0 File Offset: 0x0001BAA0
		GraphUpdateThreading IUpdatableGraph.CanUpdateAsync(GraphUpdateObject o)
		{
			return GraphUpdateThreading.UnityThread;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0001D8A3 File Offset: 0x0001BAA3
		void IUpdatableGraph.UpdateAreaInit(GraphUpdateObject o)
		{
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0001D8A5 File Offset: 0x0001BAA5
		void IUpdatableGraph.UpdateAreaPost(GraphUpdateObject o)
		{
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0001D8A7 File Offset: 0x0001BAA7
		void IUpdatableGraph.UpdateArea(GraphUpdateObject o)
		{
			NavMeshGraph.UpdateArea(o, this);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0001D8B0 File Offset: 0x0001BAB0
		public static void UpdateArea(GraphUpdateObject o, INavmeshHolder graph)
		{
			Bounds bounds = graph.transform.InverseTransform(o.bounds);
			IntRect irect = new IntRect(Mathf.FloorToInt(bounds.min.x * 1000f), Mathf.FloorToInt(bounds.min.z * 1000f), Mathf.CeilToInt(bounds.max.x * 1000f), Mathf.CeilToInt(bounds.max.z * 1000f));
			Int3 a = new Int3(irect.xmin, 0, irect.ymin);
			Int3 b = new Int3(irect.xmin, 0, irect.ymax);
			Int3 c = new Int3(irect.xmax, 0, irect.ymin);
			Int3 d = new Int3(irect.xmax, 0, irect.ymax);
			int ymin = ((Int3)bounds.min).y;
			int ymax = ((Int3)bounds.max).y;
			graph.GetNodes(delegate(GraphNode _node)
			{
				TriangleMeshNode triangleMeshNode = _node as TriangleMeshNode;
				bool flag = false;
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				for (int i = 0; i < 3; i++)
				{
					Int3 vertexInGraphSpace = triangleMeshNode.GetVertexInGraphSpace(i);
					if (irect.Contains(vertexInGraphSpace.x, vertexInGraphSpace.z))
					{
						flag = true;
						break;
					}
					if (vertexInGraphSpace.x < irect.xmin)
					{
						num++;
					}
					if (vertexInGraphSpace.x > irect.xmax)
					{
						num2++;
					}
					if (vertexInGraphSpace.z < irect.ymin)
					{
						num3++;
					}
					if (vertexInGraphSpace.z > irect.ymax)
					{
						num4++;
					}
				}
				if (!flag && (num == 3 || num2 == 3 || num3 == 3 || num4 == 3))
				{
					return;
				}
				for (int j = 0; j < 3; j++)
				{
					int num5 = ((j > 1) ? 0 : (j + 1));
					Int3 vertexInGraphSpace2 = triangleMeshNode.GetVertexInGraphSpace(j);
					Int3 vertexInGraphSpace3 = triangleMeshNode.GetVertexInGraphSpace(num5);
					if (VectorMath.SegmentsIntersectXZ(a, b, vertexInGraphSpace2, vertexInGraphSpace3))
					{
						flag = true;
						break;
					}
					if (VectorMath.SegmentsIntersectXZ(a, c, vertexInGraphSpace2, vertexInGraphSpace3))
					{
						flag = true;
						break;
					}
					if (VectorMath.SegmentsIntersectXZ(c, d, vertexInGraphSpace2, vertexInGraphSpace3))
					{
						flag = true;
						break;
					}
					if (VectorMath.SegmentsIntersectXZ(d, b, vertexInGraphSpace2, vertexInGraphSpace3))
					{
						flag = true;
						break;
					}
				}
				if (flag || triangleMeshNode.ContainsPointInGraphSpace(a) || triangleMeshNode.ContainsPointInGraphSpace(b) || triangleMeshNode.ContainsPointInGraphSpace(c) || triangleMeshNode.ContainsPointInGraphSpace(d))
				{
					flag = true;
				}
				if (!flag)
				{
					return;
				}
				int num6 = 0;
				int num7 = 0;
				for (int k = 0; k < 3; k++)
				{
					Int3 vertexInGraphSpace4 = triangleMeshNode.GetVertexInGraphSpace(k);
					if (vertexInGraphSpace4.y < ymin)
					{
						num7++;
					}
					if (vertexInGraphSpace4.y > ymax)
					{
						num6++;
					}
				}
				if (num7 == 3 || num6 == 3)
				{
					return;
				}
				o.WillUpdateNode(triangleMeshNode);
				o.Apply(triangleMeshNode);
			});
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0001DA10 File Offset: 0x0001BC10
		protected override IEnumerable<Progress> ScanInternal()
		{
			this.cachedSourceMeshBoundsMin = ((this.sourceMesh != null) ? this.sourceMesh.bounds.min : Vector3.zero);
			this.transform = this.CalculateTransform();
			this.tileZCount = (this.tileXCount = 1);
			this.tiles = new NavmeshTile[this.tileZCount * this.tileXCount];
			TriangleMeshNode.SetNavmeshHolder(AstarPath.active.data.GetGraphIndex(this), this);
			if (this.sourceMesh == null)
			{
				base.FillWithEmptyTiles();
				yield break;
			}
			yield return new Progress(0f, "Transforming Vertices");
			this.forcedBoundsSize = this.sourceMesh.bounds.size * this.scale;
			Vector3[] vertices = this.sourceMesh.vertices;
			List<Int3> intVertices = ListPool<Int3>.Claim(vertices.Length);
			Matrix4x4 matrix4x = Matrix4x4.TRS(-this.sourceMesh.bounds.min * this.scale, Quaternion.identity, Vector3.one * this.scale);
			for (int i = 0; i < vertices.Length; i++)
			{
				intVertices.Add((Int3)matrix4x.MultiplyPoint3x4(vertices[i]));
			}
			yield return new Progress(0.1f, "Compressing Vertices");
			Int3[] compressedVertices = null;
			int[] compressedTriangles = null;
			Polygon.CompressMesh(intVertices, new List<int>(this.sourceMesh.triangles), out compressedVertices, out compressedTriangles);
			ListPool<Int3>.Release(ref intVertices);
			yield return new Progress(0.2f, "Building Nodes");
			base.ReplaceTile(0, 0, compressedVertices, compressedTriangles);
			this.navmeshUpdateData.OnRecalculatedTiles(this.tiles);
			if (this.OnRecalculatedTiles != null)
			{
				this.OnRecalculatedTiles(this.tiles.Clone() as NavmeshTile[]);
			}
			yield break;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0001DA20 File Offset: 0x0001BC20
		protected override void DeserializeSettingsCompatibility(GraphSerializationContext ctx)
		{
			base.DeserializeSettingsCompatibility(ctx);
			this.sourceMesh = ctx.DeserializeUnityObject() as Mesh;
			this.offset = ctx.DeserializeVector3();
			this.rotation = ctx.DeserializeVector3();
			this.scale = ctx.reader.ReadSingle();
			this.nearestSearchOnlyXZ = !ctx.reader.ReadBoolean();
		}

		// Token: 0x040002FA RID: 762
		[JsonMember]
		public Mesh sourceMesh;

		// Token: 0x040002FB RID: 763
		[JsonMember]
		public Vector3 offset;

		// Token: 0x040002FC RID: 764
		[JsonMember]
		public Vector3 rotation;

		// Token: 0x040002FD RID: 765
		[JsonMember]
		public float scale = 1f;

		// Token: 0x040002FE RID: 766
		[JsonMember]
		public bool recalculateNormals = true;

		// Token: 0x040002FF RID: 767
		[JsonMember]
		private Vector3 cachedSourceMeshBoundsMin;
	}
}
