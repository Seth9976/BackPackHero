using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Pathfinding.Graphs.Navmesh;
using Pathfinding.Graphs.Navmesh.Jobs;
using Pathfinding.Serialization;
using Pathfinding.Util;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000DC RID: 220
	[JsonOptIn]
	[Preserve]
	public class NavMeshGraph : NavmeshBase, IUpdatableGraph
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x00025086 File Offset: 0x00023286
		public override float NavmeshCuttingCharacterRadius
		{
			get
			{
				return this.navmeshCuttingCharacterRadius;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x0002508E File Offset: 0x0002328E
		public override bool RecalculateNormals
		{
			get
			{
				return this.recalculateNormals;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x00025096 File Offset: 0x00023296
		public override float TileWorldSizeX
		{
			get
			{
				return this.forcedBoundsSize.x;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x000250A3 File Offset: 0x000232A3
		public override float TileWorldSizeZ
		{
			get
			{
				return this.forcedBoundsSize.z;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x000057A6 File Offset: 0x000039A6
		public override float MaxTileConnectionEdgeDistance
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x000250B0 File Offset: 0x000232B0
		public override bool IsInsideBounds(Vector3 point)
		{
			if (this.tiles == null || this.tiles.Length == 0 || this.sourceMesh == null)
			{
				return false;
			}
			Vector3 vector = this.transform.InverseTransform(point);
			Vector3 vector2 = this.sourceMesh.bounds.size * this.scale;
			return vector.x >= -0.0001f && vector.y >= -0.0001f && vector.z >= -0.0001f && vector.x <= vector2.x + 0.0001f && vector.y <= vector2.y + 0.0001f && vector.z <= vector2.z + 0.0001f;
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x00025174 File Offset: 0x00023374
		public override Bounds bounds
		{
			get
			{
				if (this.sourceMesh == null)
				{
					return default(Bounds);
				}
				float4x4 float4x = this.CalculateTransform().matrix;
				return new ToWorldMatrix(new float3x3(float4x.c0.xyz, float4x.c1.xyz, float4x.c2.xyz)).ToWorld(new Bounds(Vector3.zero, this.sourceMesh.bounds.size * this.scale));
			}
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00025208 File Offset: 0x00023408
		public override GraphTransform CalculateTransform()
		{
			return new GraphTransform(Matrix4x4.TRS(this.offset, Quaternion.Euler(this.rotation), Vector3.one) * Matrix4x4.TRS((this.sourceMesh != null) ? (this.sourceMesh.bounds.min * this.scale) : (this.cachedSourceMeshBoundsMin * this.scale), Quaternion.identity, Vector3.one));
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00025288 File Offset: 0x00023488
		IGraphUpdatePromise IUpdatableGraph.ScheduleGraphUpdates(List<GraphUpdateObject> graphUpdates)
		{
			return new NavMeshGraph.NavMeshGraphUpdatePromise
			{
				graph = this,
				graphUpdates = graphUpdates
			};
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x000252A0 File Offset: 0x000234A0
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

		// Token: 0x06000705 RID: 1797 RVA: 0x00025400 File Offset: 0x00023600
		protected override IGraphUpdatePromise ScanInternal(bool async)
		{
			return new NavMeshGraph.NavMeshGraphScanPromise
			{
				graph = this
			};
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0002540E File Offset: 0x0002360E
		protected override void PostDeserialization(GraphSerializationContext ctx)
		{
			if (ctx.meta.version < AstarSerializer.V4_3_74)
			{
				this.navmeshCuttingCharacterRadius = 0f;
			}
			base.PostDeserialization(ctx);
		}

		// Token: 0x04000495 RID: 1173
		[JsonMember]
		public Mesh sourceMesh;

		// Token: 0x04000496 RID: 1174
		[JsonMember]
		public Vector3 offset;

		// Token: 0x04000497 RID: 1175
		[JsonMember]
		public Vector3 rotation;

		// Token: 0x04000498 RID: 1176
		[JsonMember]
		public float scale = 1f;

		// Token: 0x04000499 RID: 1177
		[JsonMember]
		public bool recalculateNormals = true;

		// Token: 0x0400049A RID: 1178
		[JsonMember]
		private Vector3 cachedSourceMeshBoundsMin;

		// Token: 0x0400049B RID: 1179
		[JsonMember]
		public float navmeshCuttingCharacterRadius = 0.5f;

		// Token: 0x020000DD RID: 221
		private class NavMeshGraphUpdatePromise : IGraphUpdatePromise
		{
			// Token: 0x06000708 RID: 1800 RVA: 0x00025460 File Offset: 0x00023660
			public void Apply(IGraphUpdateContext ctx)
			{
				for (int i = 0; i < this.graphUpdates.Count; i++)
				{
					GraphUpdateObject graphUpdateObject = this.graphUpdates[i];
					NavMeshGraph.UpdateArea(graphUpdateObject, this.graph);
					ctx.DirtyBounds(graphUpdateObject.bounds);
				}
			}

			// Token: 0x0400049C RID: 1180
			public NavMeshGraph graph;

			// Token: 0x0400049D RID: 1181
			public List<GraphUpdateObject> graphUpdates;
		}

		// Token: 0x020000DE RID: 222
		private class NavMeshGraphScanPromise : IGraphUpdatePromise
		{
			// Token: 0x0600070A RID: 1802 RVA: 0x000254A8 File Offset: 0x000236A8
			public IEnumerator<JobHandle> Prepare()
			{
				Mesh sourceMesh = this.graph.sourceMesh;
				this.graph.cachedSourceMeshBoundsMin = ((sourceMesh != null) ? sourceMesh.bounds.min : Vector3.zero);
				this.transform = this.graph.CalculateTransform();
				if (sourceMesh == null)
				{
					this.emptyGraph = true;
					yield break;
				}
				if (!sourceMesh.isReadable)
				{
					Debug.LogError("The source mesh " + sourceMesh.name + " is not readable. Enable Read/Write in the mesh's import settings.", sourceMesh);
					this.emptyGraph = true;
					yield break;
				}
				Mesh.MeshDataArray meshDataArray = Mesh.AcquireReadOnlyMeshData(sourceMesh);
				NativeArray<Vector3> vertices;
				NativeArray<int> indices;
				MeshUtility.GetMeshData(meshDataArray, 0, out vertices, out indices);
				meshDataArray.Dispose();
				Matrix4x4 matrix4x = Matrix4x4.TRS(-sourceMesh.bounds.min * this.graph.scale, Quaternion.identity, Vector3.one * this.graph.scale);
				Promise<JobBuildTileMeshFromVertices.BuildNavmeshOutput> promise = JobBuildTileMeshFromVertices.Schedule(vertices, indices, matrix4x, this.graph.RecalculateNormals);
				this.forcedBoundsSize = sourceMesh.bounds.size * this.graph.scale;
				this.tileRect = new IntRect(0, 0, 0, 0);
				this.tiles = new NavmeshTile[this.tileRect.Area];
				GCHandle tilesGCHandle = GCHandle.Alloc(this.tiles);
				Vector2 vector = new Vector2(this.forcedBoundsSize.x, this.forcedBoundsSize.z);
				NativeArray<JobCalculateTriangleConnections.TileNodeConnectionsUnsafe> tileNodeConnections = new NativeArray<JobCalculateTriangleConnections.TileNodeConnectionsUnsafe>(this.tiles.Length, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				JobHandle jobHandle = new JobCalculateTriangleConnections
				{
					tileMeshes = promise.GetValue().tiles,
					nodeConnections = tileNodeConnections
				}.Schedule(promise.handle);
				JobHandle jobHandle2 = new JobCreateTiles
				{
					tileMeshes = promise.GetValue().tiles,
					tiles = tilesGCHandle,
					tileRect = this.tileRect,
					graphTileCount = new Int2(this.tileRect.Width, this.tileRect.Height),
					graphIndex = this.graph.graphIndex,
					initialPenalty = this.graph.initialPenalty,
					recalculateNormals = this.graph.recalculateNormals,
					graphToWorldSpace = this.transform.matrix,
					tileWorldSize = vector
				}.Schedule(promise.handle);
				JobHandle jobHandle3 = new JobWriteNodeConnections
				{
					tiles = tilesGCHandle,
					nodeConnections = tileNodeConnections
				}.Schedule(JobHandle.CombineDependencies(jobHandle2, jobHandle));
				yield return jobHandle3;
				promise.Complete().Dispose();
				tileNodeConnections.Dispose();
				vertices.Dispose();
				indices.Dispose();
				tilesGCHandle.Free();
				yield break;
			}

			// Token: 0x0600070B RID: 1803 RVA: 0x000254B8 File Offset: 0x000236B8
			public void Apply(IGraphUpdateContext ctx)
			{
				if (this.emptyGraph)
				{
					this.graph.forcedBoundsSize = Vector3.zero;
					this.graph.transform = this.transform;
					this.graph.tileZCount = (this.graph.tileXCount = 1);
					this.graph.tiles = new NavmeshTile[this.graph.tileZCount * this.graph.tileZCount];
					TriangleMeshNode.SetNavmeshHolder(AstarPath.active.data.GetGraphIndex(this.graph), this.graph);
					this.graph.FillWithEmptyTiles();
					return;
				}
				this.graph.DestroyAllNodes();
				for (int i = 0; i < this.tiles.Length; i++)
				{
					AstarPath active = AstarPath.active;
					GraphNode[] nodes = this.tiles[i].nodes;
					active.InitializeNodes(nodes);
				}
				this.graph.forcedBoundsSize = this.forcedBoundsSize;
				this.graph.transform = this.transform;
				this.graph.tileXCount = this.tileRect.Width;
				this.graph.tileZCount = this.tileRect.Height;
				this.graph.tiles = this.tiles;
				TriangleMeshNode.SetNavmeshHolder(this.graph.active.data.GetGraphIndex(this.graph), this.graph);
				this.graph.navmeshUpdateData.OnRecalculatedTiles(this.tiles);
				if (this.graph.OnRecalculatedTiles != null)
				{
					this.graph.OnRecalculatedTiles(this.tiles.Clone() as NavmeshTile[]);
				}
			}

			// Token: 0x0400049E RID: 1182
			public NavMeshGraph graph;

			// Token: 0x0400049F RID: 1183
			private bool emptyGraph;

			// Token: 0x040004A0 RID: 1184
			private GraphTransform transform;

			// Token: 0x040004A1 RID: 1185
			private NavmeshTile[] tiles;

			// Token: 0x040004A2 RID: 1186
			private Vector3 forcedBoundsSize;

			// Token: 0x040004A3 RID: 1187
			private IntRect tileRect;
		}
	}
}
