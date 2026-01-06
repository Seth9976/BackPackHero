using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Pathfinding.Graphs.Navmesh;
using Pathfinding.Graphs.Navmesh.Jobs;
using Pathfinding.Jobs;
using Pathfinding.Serialization;
using Pathfinding.Util;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000F8 RID: 248
	[JsonOptIn]
	[Preserve]
	public class RecastGraph : NavmeshBase, IUpdatableGraph
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x0002A4EB File Offset: 0x000286EB
		public override float NavmeshCuttingCharacterRadius
		{
			get
			{
				return this.characterRadius;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x00016F22 File Offset: 0x00015122
		public override bool RecalculateNormals
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x0002A4F3 File Offset: 0x000286F3
		public override float TileWorldSizeX
		{
			get
			{
				return (float)this.tileSizeX * this.cellSize;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x0002A503 File Offset: 0x00028703
		public override float TileWorldSizeZ
		{
			get
			{
				return (float)this.tileSizeZ * this.cellSize;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x0002A513 File Offset: 0x00028713
		public override float MaxTileConnectionEdgeDistance
		{
			get
			{
				return this.walkableClimb;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x0002A51C File Offset: 0x0002871C
		public override Bounds bounds
		{
			get
			{
				float4x4 float4x = this.CalculateTransform().matrix;
				Bounds bounds = new ToWorldMatrix(new float3x3(float4x.c0.xyz, float4x.c1.xyz, float4x.c2.xyz)).ToWorld(new Bounds(Vector3.zero, this.forcedBoundsSize));
				bounds.center += this.forcedBoundsCenter;
				return bounds;
			}
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0002A59C File Offset: 0x0002879C
		public override bool IsInsideBounds(Vector3 point)
		{
			if (this.tiles == null || this.tiles.Length == 0)
			{
				return false;
			}
			float3 @float = this.transform.InverseTransform(point);
			if (this.dimensionMode == RecastGraph.DimensionMode.Dimension2D)
			{
				return @float.x >= 0f && @float.z >= 0f && @float.x <= this.forcedBoundsSize.x && @float.z <= this.forcedBoundsSize.z;
			}
			return math.all(@float >= 0f) && math.all(@float <= this.forcedBoundsSize);
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0002A648 File Offset: 0x00028848
		public void SnapForceBoundsToScene()
		{
			DisposeArena disposeArena = new DisposeArena();
			RecastMeshGatherer.MeshCollection meshCollection = new TileBuilder(this, new TileLayout(this), default(IntRect)).CollectMeshes(new Bounds(Vector3.zero, new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity)));
			if (meshCollection.meshes.Length > 0)
			{
				ToWorldMatrix toWorldMatrix = new ToWorldMatrix(new float3x3(Quaternion.Inverse(Quaternion.Euler(this.rotation))));
				Bounds bounds = toWorldMatrix.ToWorld(meshCollection.meshes[0].bounds);
				for (int i = 1; i < meshCollection.meshes.Length; i++)
				{
					bounds.Encapsulate(toWorldMatrix.ToWorld(meshCollection.meshes[i].bounds));
				}
				this.forcedBoundsCenter = Quaternion.Euler(this.rotation) * bounds.center;
				this.forcedBoundsSize = bounds.size;
			}
			disposeArena.Add<RecastMeshGatherer.MeshCollection>(meshCollection);
			disposeArena.DisposeAll();
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0002A758 File Offset: 0x00028958
		IGraphUpdatePromise IUpdatableGraph.ScheduleGraphUpdates(List<GraphUpdateObject> graphUpdates)
		{
			return new RecastGraph.RecastGraphUpdatePromise(this, graphUpdates);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0002A761 File Offset: 0x00028961
		public IGraphUpdatePromise TranslateInDirection(int dx, int dz)
		{
			return new RecastGraph.RecastMovePromise(this, new Int2(dx, dz));
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0002A770 File Offset: 0x00028970
		protected override IGraphUpdatePromise ScanInternal(bool async)
		{
			return new RecastGraph.RecastGraphScanPromise
			{
				graph = this
			};
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0002A77E File Offset: 0x0002897E
		public override GraphTransform CalculateTransform()
		{
			return RecastGraph.CalculateTransform(new Bounds(this.forcedBoundsCenter, this.forcedBoundsSize), Quaternion.Euler(this.rotation));
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0002A7A1 File Offset: 0x000289A1
		public static GraphTransform CalculateTransform(Bounds bounds, Quaternion rotation)
		{
			return new GraphTransform(Matrix4x4.TRS(bounds.center, rotation, Vector3.one) * Matrix4x4.TRS(-bounds.extents, Quaternion.identity, Vector3.one));
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0002A7DC File Offset: 0x000289DC
		private void AllocateTilesArray(TileLayout info)
		{
			this.tileXCount = info.tileCount.x;
			this.tileZCount = info.tileCount.y;
			this.tileSizeX = info.tileSizeInVoxels.x;
			this.tileSizeZ = info.tileSizeInVoxels.y;
			this.tiles = new NavmeshTile[info.tileCount.x * info.tileCount.y];
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x0002A84F File Offset: 0x00028A4F
		internal int CharacterRadiusInVoxels
		{
			get
			{
				return Mathf.CeilToInt(this.characterRadius / this.cellSize - 0.1f);
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x0002A869 File Offset: 0x00028A69
		internal int TileBorderSizeInVoxels
		{
			get
			{
				return this.CharacterRadiusInVoxels + 3;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x0002A873 File Offset: 0x00028A73
		internal float TileBorderSizeInWorldUnits
		{
			get
			{
				return (float)this.TileBorderSizeInVoxels * this.cellSize;
			}
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0002A884 File Offset: 0x00028A84
		public unsafe virtual void Resize(IntRect newTileBounds)
		{
			base.AssertSafeToUpdateGraph();
			if (!newTileBounds.IsValid())
			{
				throw new ArgumentException("Invalid tile bounds");
			}
			if (newTileBounds == new IntRect(0, 0, this.tileXCount - 1, this.tileZCount - 1))
			{
				return;
			}
			if (newTileBounds.Area == 0)
			{
				throw new ArgumentException("Tile count must at least 1x1");
			}
			base.StartBatchTileUpdate();
			NavmeshTile[] array = new NavmeshTile[newTileBounds.Area];
			for (int i = 0; i < this.tileZCount; i++)
			{
				for (int j = 0; j < this.tileXCount; j++)
				{
					if (newTileBounds.Contains(j, i))
					{
						NavmeshTile navmeshTile = this.tiles[j + i * this.tileXCount];
						array[j - newTileBounds.xmin + (i - newTileBounds.ymin) * newTileBounds.Width] = navmeshTile;
					}
					else
					{
						base.ClearTile(j, i);
						base.DirtyBounds(base.GetTileBounds(j, i, 1, 1));
					}
				}
			}
			this.forcedBoundsSize = new Vector3((float)newTileBounds.Width * this.TileWorldSizeX, this.forcedBoundsSize.y, (float)newTileBounds.Height * this.TileWorldSizeZ);
			this.forcedBoundsCenter = this.transform.Transform(new Vector3((float)(newTileBounds.xmin + newTileBounds.xmax + 1) * 0.5f * this.TileWorldSizeX, this.forcedBoundsSize.y * 0.5f, (float)(newTileBounds.ymin + newTileBounds.ymax + 1) * 0.5f * this.TileWorldSizeZ));
			this.transform = this.CalculateTransform();
			Int3 @int = -(Int3)new Vector3(this.TileWorldSizeX * (float)newTileBounds.xmin, 0f, this.TileWorldSizeZ * (float)newTileBounds.ymin);
			for (int k = 0; k < newTileBounds.Height; k++)
			{
				for (int l = 0; l < newTileBounds.Width; l++)
				{
					int num = l + k * newTileBounds.Width;
					NavmeshTile navmeshTile2 = array[num];
					if (navmeshTile2 == null)
					{
						array[num] = base.NewEmptyTile(l, k);
					}
					else
					{
						navmeshTile2.x = l;
						navmeshTile2.z = k;
						for (int m = 0; m < navmeshTile2.nodes.Length; m++)
						{
							TriangleMeshNode triangleMeshNode = navmeshTile2.nodes[m];
							triangleMeshNode.v0 = (triangleMeshNode.v0 & 4095) | (num << 12);
							triangleMeshNode.v1 = (triangleMeshNode.v1 & 4095) | (num << 12);
							triangleMeshNode.v2 = (triangleMeshNode.v2 & 4095) | (num << 12);
						}
						for (int n = 0; n < navmeshTile2.vertsInGraphSpace.Length; n++)
						{
							*navmeshTile2.vertsInGraphSpace[n] += @int;
						}
						navmeshTile2.vertsInGraphSpace.CopyTo(navmeshTile2.verts);
						this.transform.Transform(navmeshTile2.verts);
						navmeshTile2.bbTree.Dispose();
						navmeshTile2.bbTree = new BBTree(navmeshTile2.tris, navmeshTile2.vertsInGraphSpace);
					}
				}
			}
			this.tiles = array;
			this.tileXCount = newTileBounds.Width;
			this.tileZCount = newTileBounds.Height;
			base.EndBatchTileUpdate();
			this.navmeshUpdateData.OnResized(newTileBounds);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0002ABD4 File Offset: 0x00028DD4
		public void EnsureInitialized()
		{
			base.AssertSafeToUpdateGraph();
			if (this.tiles == null)
			{
				TriangleMeshNode.SetNavmeshHolder(AstarPath.active.data.GetGraphIndex(this), this);
				this.transform = this.CalculateTransform();
				this.AllocateTilesArray(new TileLayout(this));
				base.FillWithEmptyTiles();
			}
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0002AC24 File Offset: 0x00028E24
		public void ReplaceTiles(TileMeshes tileMeshes, float yOffset = 0f)
		{
			base.AssertSafeToUpdateGraph();
			this.EnsureInitialized();
			if (tileMeshes.tileWorldSize.x != this.TileWorldSizeX || tileMeshes.tileWorldSize.y != this.TileWorldSizeZ)
			{
				string[] array = new string[7];
				array[0] = "Loaded tile size does not match this graph's tile size.\nThe source tiles have a world-space tile size of ";
				int num = 1;
				Vector2 tileWorldSize = tileMeshes.tileWorldSize;
				array[num] = tileWorldSize.ToString();
				array[2] = " while this graph's tile size is (";
				array[3] = this.TileWorldSizeX.ToString();
				array[4] = ",";
				array[5] = this.TileWorldSizeZ.ToString();
				array[6] = ").\nFor a recast graph, the world-space tile size is defined as the cell size * the tile size in voxels";
				throw new Exception(string.Concat(array));
			}
			int width = tileMeshes.tileRect.Width;
			int height = tileMeshes.tileRect.Height;
			IntRect intRect = IntRect.Union(new IntRect(0, 0, this.tileXCount - 1, this.tileZCount - 1), tileMeshes.tileRect);
			this.Resize(intRect);
			tileMeshes.tileRect = tileMeshes.tileRect.Offset(-intRect.Min);
			base.StartBatchTileUpdate();
			NavmeshTile[] array2 = new NavmeshTile[width * height];
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					TileMesh tileMesh = tileMeshes.tileMeshes[j + i * width];
					Int3 @int = (Int3)new Vector3(0f, yOffset, 0f);
					for (int k = 0; k < tileMesh.verticesInTileSpace.Length; k++)
					{
						tileMesh.verticesInTileSpace[k] += @int;
					}
					Int2 int2 = new Int2(j, i) + tileMeshes.tileRect.Min;
					base.ReplaceTile(int2.x, int2.y, tileMesh.verticesInTileSpace, tileMesh.triangles);
					array2[j + i * width] = base.GetTile(int2.x, int2.y);
				}
			}
			base.EndBatchTileUpdate();
			this.navmeshUpdateData.OnRecalculatedTiles(array2);
			if (this.OnRecalculatedTiles != null)
			{
				this.OnRecalculatedTiles(array2);
			}
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0002AE54 File Offset: 0x00029054
		protected override void PostDeserialization(GraphSerializationContext ctx)
		{
			base.PostDeserialization(ctx);
			if (ctx.meta.version < AstarSerializer.V4_3_80)
			{
				this.colliderRasterizeDetail = 2f * this.cellSize * this.colliderRasterizeDetail * this.colliderRasterizeDetail / 9.869605f;
			}
		}

		// Token: 0x0400051B RID: 1307
		[JsonMember]
		public float characterRadius = 1.5f;

		// Token: 0x0400051C RID: 1308
		[JsonMember]
		public float contourMaxError = 2f;

		// Token: 0x0400051D RID: 1309
		[JsonMember]
		public float cellSize = 0.5f;

		// Token: 0x0400051E RID: 1310
		[JsonMember]
		public float walkableHeight = 2f;

		// Token: 0x0400051F RID: 1311
		[JsonMember]
		public float walkableClimb = 0.5f;

		// Token: 0x04000520 RID: 1312
		[JsonMember]
		public float maxSlope = 30f;

		// Token: 0x04000521 RID: 1313
		[JsonMember]
		public float maxEdgeLength = 20f;

		// Token: 0x04000522 RID: 1314
		[JsonMember]
		public float minRegionSize = 3f;

		// Token: 0x04000523 RID: 1315
		[JsonMember]
		public int editorTileSize = 128;

		// Token: 0x04000524 RID: 1316
		[JsonMember]
		public int tileSizeX = 128;

		// Token: 0x04000525 RID: 1317
		[JsonMember]
		public int tileSizeZ = 128;

		// Token: 0x04000526 RID: 1318
		[JsonMember]
		public bool useTiles = true;

		// Token: 0x04000527 RID: 1319
		public bool scanEmptyGraph;

		// Token: 0x04000528 RID: 1320
		[JsonMember]
		public RecastGraph.DimensionMode dimensionMode = RecastGraph.DimensionMode.Dimension3D;

		// Token: 0x04000529 RID: 1321
		[JsonMember]
		public RecastGraph.BackgroundTraversability backgroundTraversability;

		// Token: 0x0400052A RID: 1322
		[JsonMember]
		public RecastGraph.RelevantGraphSurfaceMode relevantGraphSurfaceMode;

		// Token: 0x0400052B RID: 1323
		[JsonMember]
		public bool rasterizeColliders;

		// Token: 0x0400052C RID: 1324
		[JsonMember]
		public bool rasterizeMeshes = true;

		// Token: 0x0400052D RID: 1325
		[JsonMember]
		public bool rasterizeTerrain = true;

		// Token: 0x0400052E RID: 1326
		[JsonMember]
		public bool rasterizeTrees = true;

		// Token: 0x0400052F RID: 1327
		[JsonMember]
		public float colliderRasterizeDetail = 1f;

		// Token: 0x04000530 RID: 1328
		[JsonMember]
		public LayerMask mask = -1;

		// Token: 0x04000531 RID: 1329
		[JsonMember]
		public List<string> tagMask = new List<string>();

		// Token: 0x04000532 RID: 1330
		[JsonMember]
		public int terrainSampleSize = 3;

		// Token: 0x04000533 RID: 1331
		[JsonMember]
		public Vector3 rotation;

		// Token: 0x04000534 RID: 1332
		[JsonMember]
		public Vector3 forcedBoundsCenter;

		// Token: 0x04000535 RID: 1333
		private DisposeArena pendingGraphUpdateArena = new DisposeArena();

		// Token: 0x020000F9 RID: 249
		public enum RelevantGraphSurfaceMode
		{
			// Token: 0x04000537 RID: 1335
			DoNotRequire,
			// Token: 0x04000538 RID: 1336
			OnlyForCompletelyInsideTile,
			// Token: 0x04000539 RID: 1337
			RequireForAll
		}

		// Token: 0x020000FA RID: 250
		public enum DimensionMode
		{
			// Token: 0x0400053B RID: 1339
			Dimension2D,
			// Token: 0x0400053C RID: 1340
			Dimension3D
		}

		// Token: 0x020000FB RID: 251
		public enum BackgroundTraversability
		{
			// Token: 0x0400053E RID: 1342
			Walkable,
			// Token: 0x0400053F RID: 1343
			Unwalkable
		}

		// Token: 0x020000FC RID: 252
		private class RecastGraphUpdatePromise : IGraphUpdatePromise
		{
			// Token: 0x06000833 RID: 2099 RVA: 0x0002AF8C File Offset: 0x0002918C
			public RecastGraphUpdatePromise(RecastGraph graph, List<GraphUpdateObject> graphUpdates)
			{
				this.promises = ListPool<ValueTuple<Promise<TileBuilder.TileBuilderOutput>, Promise<JobBuildNodes.BuildNodeTilesOutput>>>.Claim();
				this.graph = graph;
				this.graphHash = RecastGraph.RecastGraphUpdatePromise.HashSettings(graph);
				List<ValueTuple<IntRect, GraphUpdateObject>> list = ListPool<ValueTuple<IntRect, GraphUpdateObject>>.Claim();
				for (int i = graphUpdates.Count - 1; i >= 0; i--)
				{
					GraphUpdateObject graphUpdateObject = graphUpdates[i];
					if (graphUpdateObject.updatePhysics)
					{
						graphUpdates.RemoveAt(i);
						IntRect touchingTiles = graph.GetTouchingTiles(graphUpdateObject.bounds, graph.TileBorderSizeInWorldUnits);
						if (touchingTiles.IsValid())
						{
							list.Add(new ValueTuple<IntRect, GraphUpdateObject>(touchingTiles, graphUpdateObject));
						}
					}
				}
				this.graphUpdates = graphUpdates;
				if (list.Count > 1)
				{
					list.Sort((ValueTuple<IntRect, GraphUpdateObject> a, ValueTuple<IntRect, GraphUpdateObject> b) => b.Item1.Area.CompareTo(a.Item1.Area));
				}
				int j = 0;
				while (j < list.Count)
				{
					IntRect item = list[j].Item1;
					if (list.Count <= 1)
					{
						goto IL_0136;
					}
					bool flag = false;
					for (int k = item.ymin; k <= item.ymax; k++)
					{
						for (int l = item.xmin; l <= item.xmax; l++)
						{
							NavmeshTile tile = graph.GetTile(l, k);
							flag |= !tile.flag;
							tile.flag = true;
						}
					}
					if (flag)
					{
						goto IL_0136;
					}
					IL_0188:
					j++;
					continue;
					IL_0136:
					TileLayout tileLayout = new TileLayout(graph);
					Promise<TileBuilder.TileBuilderOutput> promise = RecastBuilder.BuildTileMeshes(graph, tileLayout, item).Schedule(graph.pendingGraphUpdateArena);
					Promise<JobBuildNodes.BuildNodeTilesOutput> promise2 = RecastBuilder.BuildNodeTiles(graph, tileLayout).Schedule(graph.pendingGraphUpdateArena, promise);
					this.promises.Add(new ValueTuple<Promise<TileBuilder.TileBuilderOutput>, Promise<JobBuildNodes.BuildNodeTilesOutput>>(promise, promise2));
					goto IL_0188;
				}
				if (list.Count > 1)
				{
					for (int m = 0; m < list.Count; m++)
					{
						IntRect item2 = list[m].Item1;
						for (int n = item2.ymin; n <= item2.ymax; n++)
						{
							for (int num = item2.xmin; num <= item2.xmax; num++)
							{
								graph.GetTile(num, n).flag = false;
							}
						}
					}
				}
				ListPool<ValueTuple<IntRect, GraphUpdateObject>>.Release(ref list);
			}

			// Token: 0x06000834 RID: 2100 RVA: 0x0002B1B0 File Offset: 0x000293B0
			public IEnumerator<JobHandle> Prepare()
			{
				int num;
				for (int i = 0; i < this.promises.Count; i = num + 1)
				{
					yield return this.promises[i].Item2.handle;
					yield return this.promises[i].Item1.handle;
					num = i;
				}
				yield break;
			}

			// Token: 0x06000835 RID: 2101 RVA: 0x0002B1C0 File Offset: 0x000293C0
			private static int HashSettings(RecastGraph graph)
			{
				return (((graph.tileXCount * 31) ^ graph.tileZCount) * 31) ^ (graph.TileWorldSizeX.GetHashCode() * 31) ^ graph.TileWorldSizeZ.GetHashCode();
			}

			// Token: 0x06000836 RID: 2102 RVA: 0x0002B204 File Offset: 0x00029404
			public void Apply(IGraphUpdateContext ctx)
			{
				if (RecastGraph.RecastGraphUpdatePromise.HashSettings(this.graph) != this.graphHash)
				{
					throw new InvalidOperationException("Recast graph changed while a graph update was in progress. This is not allowed. Use AstarPath.active.AddWorkItem if you need to update graphs.");
				}
				for (int i = 0; i < this.promises.Count; i++)
				{
					Promise<TileBuilder.TileBuilderOutput> item = this.promises[i].Item1;
					Promise<JobBuildNodes.BuildNodeTilesOutput> item2 = this.promises[i].Item2;
					JobBuildNodes.BuildNodeTilesOutput buildNodeTilesOutput = item2.Complete();
					IntRect tileRect = buildNodeTilesOutput.dependency.tileMeshes.tileRect;
					NavmeshTile[] tiles = buildNodeTilesOutput.tiles;
					item.Dispose();
					item2.Dispose();
					for (int j = 0; j < tiles.Length; j++)
					{
						AstarPath active = AstarPath.active;
						GraphNode[] nodes = tiles[j].nodes;
						active.InitializeNodes(nodes);
					}
					this.graph.StartBatchTileUpdate();
					for (int k = 0; k < tileRect.Height; k++)
					{
						for (int l = 0; l < tileRect.Width; l++)
						{
							int num = (k + tileRect.ymin) * this.graph.tileXCount + (l + tileRect.xmin);
							this.graph.ClearTile(l + tileRect.xmin, k + tileRect.ymin);
							NavmeshTile navmeshTile = tiles[k * tileRect.Width + l];
							navmeshTile.graph = this.graph;
							this.graph.tiles[num] = navmeshTile;
						}
					}
					this.graph.EndBatchTileUpdate();
					JobHandle jobHandle = default(JobHandle);
					GCHandle gchandle = GCHandle.Alloc(this.graph.tiles);
					IntRect intRect = new IntRect(0, 0, this.graph.tileXCount - 1, this.graph.tileZCount - 1);
					for (int m = tileRect.ymin; m <= tileRect.ymax; m++)
					{
						for (int n = tileRect.xmin; n <= tileRect.xmax; n++)
						{
							int num2 = m * intRect.Width + n;
							JobHandle jobHandle2 = default(JobHandle);
							for (int num3 = 0; num3 < 4; num3++)
							{
								int num4 = n + GridGraph.neighbourXOffsets[num3];
								int num5 = m + GridGraph.neighbourZOffsets[num3];
								if (intRect.Contains(num4, num5) && !tileRect.Contains(num4, num5))
								{
									int num6 = num5 * intRect.Width + num4;
									JobHandle jobHandle3 = new JobConnectTiles
									{
										tiles = gchandle,
										tileIndex1 = num2,
										tileIndex2 = num6,
										tileWorldSizeX = this.graph.TileWorldSizeX,
										tileWorldSizeZ = this.graph.TileWorldSizeZ,
										maxTileConnectionEdgeDistance = this.graph.MaxTileConnectionEdgeDistance
									}.Schedule(jobHandle2);
									jobHandle2 = JobHandle.CombineDependencies(jobHandle2, jobHandle3);
								}
							}
							jobHandle = JobHandle.CombineDependencies(jobHandle, jobHandle2);
						}
					}
					jobHandle.Complete();
					gchandle.Free();
					this.graph.navmeshUpdateData.OnRecalculatedTiles(tiles);
					if (this.graph.OnRecalculatedTiles != null)
					{
						this.graph.OnRecalculatedTiles(tiles);
					}
					ctx.DirtyBounds(this.graph.GetTileBounds(tileRect));
				}
				this.graph.pendingGraphUpdateArena.DisposeAll();
				if (this.graphUpdates != null)
				{
					for (int num7 = 0; num7 < this.graphUpdates.Count; num7++)
					{
						GraphUpdateObject graphUpdateObject = this.graphUpdates[num7];
						IntRect touchingTiles = this.graph.GetTouchingTiles(graphUpdateObject.bounds, this.graph.TileBorderSizeInWorldUnits);
						if (touchingTiles.IsValid())
						{
							for (int num8 = touchingTiles.ymin; num8 <= touchingTiles.ymax; num8++)
							{
								for (int num9 = touchingTiles.xmin; num9 <= touchingTiles.xmax; num9++)
								{
									NavmeshTile navmeshTile2 = this.graph.tiles[num8 * this.graph.tileXCount + num9];
									NavMeshGraph.UpdateArea(graphUpdateObject, navmeshTile2);
								}
							}
							ctx.DirtyBounds(this.graph.GetTileBounds(touchingTiles));
						}
					}
				}
			}

			// Token: 0x04000540 RID: 1344
			public List<ValueTuple<Promise<TileBuilder.TileBuilderOutput>, Promise<JobBuildNodes.BuildNodeTilesOutput>>> promises;

			// Token: 0x04000541 RID: 1345
			public List<GraphUpdateObject> graphUpdates;

			// Token: 0x04000542 RID: 1346
			public RecastGraph graph;

			// Token: 0x04000543 RID: 1347
			private int graphHash;
		}

		// Token: 0x020000FF RID: 255
		private class RecastGraphScanPromise : IGraphUpdatePromise
		{
			// Token: 0x17000157 RID: 343
			// (get) Token: 0x06000840 RID: 2112 RVA: 0x0002B741 File Offset: 0x00029941
			public float Progress
			{
				get
				{
					if (this.progressSource == null)
					{
						return 1f;
					}
					return this.progressSource.Progress;
				}
			}

			// Token: 0x06000841 RID: 2113 RVA: 0x0002B75C File Offset: 0x0002995C
			public IEnumerator<JobHandle> Prepare()
			{
				TriangleMeshNode.SetNavmeshHolder(AstarPath.active.data.GetGraphIndex(this.graph), this.graph);
				if (!Application.isPlaying)
				{
					RelevantGraphSurface.FindAllGraphSurfaces();
				}
				RelevantGraphSurface.UpdateAllPositions();
				this.tileLayout = new TileLayout(this.graph);
				if (this.graph.scanEmptyGraph || this.tileLayout.tileCount.x * this.tileLayout.tileCount.y <= 0)
				{
					this.emptyGraph = true;
					yield break;
				}
				DisposeArena arena = new DisposeArena();
				Promise<TileBuilder.TileBuilderOutput> tileMeshesPromise = RecastBuilder.BuildTileMeshes(this.graph, this.tileLayout, new IntRect(0, 0, this.tileLayout.tileCount.x - 1, this.tileLayout.tileCount.y - 1)).Schedule(arena);
				Promise<JobBuildNodes.BuildNodeTilesOutput> tilesPromise = RecastBuilder.BuildNodeTiles(this.graph, this.tileLayout).Schedule(arena, tileMeshesPromise);
				this.progressSource = tilesPromise;
				yield return tilesPromise.handle;
				this.progressSource = null;
				JobBuildNodes.BuildNodeTilesOutput buildNodeTilesOutput = tilesPromise.Complete();
				TileBuilder.TileBuilderOutput tileBuilderOutput = tileMeshesPromise.Complete();
				this.tiles = buildNodeTilesOutput.tiles;
				tileBuilderOutput.Dispose();
				buildNodeTilesOutput.Dispose();
				arena.DisposeAll();
				yield break;
			}

			// Token: 0x06000842 RID: 2114 RVA: 0x0002B76C File Offset: 0x0002996C
			public void Apply(IGraphUpdateContext ctx)
			{
				this.graph.DestroyAllNodes();
				if (this.emptyGraph)
				{
					this.graph.AllocateTilesArray(this.tileLayout);
					this.graph.FillWithEmptyTiles();
					return;
				}
				for (int i = 0; i < this.tiles.Length; i++)
				{
					AstarPath active = AstarPath.active;
					GraphNode[] nodes = this.tiles[i].nodes;
					active.InitializeNodes(nodes);
				}
				this.graph.transform = this.tileLayout.transform;
				this.graph.tileXCount = this.tileLayout.tileCount.x;
				this.graph.tileZCount = this.tileLayout.tileCount.y;
				this.graph.tileSizeX = this.tileLayout.tileSizeInVoxels.x;
				this.graph.tileSizeZ = this.tileLayout.tileSizeInVoxels.y;
				this.graph.tiles = this.tiles;
				for (int j = 0; j < this.tiles.Length; j++)
				{
					this.tiles[j].graph = this.graph;
				}
				this.graph.navmeshUpdateData.OnRecalculatedTiles(this.graph.tiles);
				if (this.graph.OnRecalculatedTiles != null)
				{
					this.graph.OnRecalculatedTiles(this.graph.tiles.Clone() as NavmeshTile[]);
				}
			}

			// Token: 0x0400054A RID: 1354
			public RecastGraph graph;

			// Token: 0x0400054B RID: 1355
			private TileLayout tileLayout;

			// Token: 0x0400054C RID: 1356
			private bool emptyGraph;

			// Token: 0x0400054D RID: 1357
			private NavmeshTile[] tiles;

			// Token: 0x0400054E RID: 1358
			private IProgress progressSource;
		}

		// Token: 0x02000101 RID: 257
		private class RecastMovePromise : IGraphUpdatePromise
		{
			// Token: 0x0600084A RID: 2122 RVA: 0x0002BAA5 File Offset: 0x00029CA5
			public RecastMovePromise(RecastGraph graph, Int2 delta)
			{
				this.graph = graph;
				this.delta = delta;
				if (delta.x != 0 && delta.y != 0)
				{
					throw new ArgumentException("Only translation in a single direction is supported. delta.x == 0 || delta.y == 0 must hold.");
				}
			}

			// Token: 0x0600084B RID: 2123 RVA: 0x0002BAD6 File Offset: 0x00029CD6
			public IEnumerator<JobHandle> Prepare()
			{
				if (this.delta.x == 0 && this.delta.y == 0)
				{
					yield break;
				}
				IntRect originalTileRect = new IntRect(0, 0, this.graph.tileXCount - 1, this.graph.tileZCount - 1);
				this.newTileRect = originalTileRect.Offset(this.delta);
				IntRect createdTiles = IntRect.Exclude(this.newTileRect, originalTileRect);
				DisposeArena disposeArena = new DisposeArena();
				TileBuilder tileBuilder = RecastBuilder.BuildTileMeshes(this.graph, new TileLayout(this.graph), createdTiles);
				tileBuilder.scene = this.graph.active.gameObject.scene;
				Promise<TileBuilder.TileBuilderOutput> pendingPromise = tileBuilder.Schedule(disposeArena);
				yield return pendingPromise.handle;
				TileBuilder.TileBuilderOutput value = pendingPromise.GetValue();
				this.tileMeshes = value.tileMeshes.ToManaged();
				pendingPromise.Dispose();
				disposeArena.DisposeAll();
				this.tileMeshes.tileRect = createdTiles.Offset(originalTileRect.Min - this.newTileRect.Min);
				yield break;
			}

			// Token: 0x0600084C RID: 2124 RVA: 0x0002BAE8 File Offset: 0x00029CE8
			public void Apply(IGraphUpdateContext ctx)
			{
				if (this.delta.x == 0 && this.delta.y == 0)
				{
					return;
				}
				this.graph.Resize(this.newTileRect);
				this.graph.ReplaceTiles(this.tileMeshes, 0f);
			}

			// Token: 0x04000555 RID: 1365
			private RecastGraph graph;

			// Token: 0x04000556 RID: 1366
			private TileMeshes tileMeshes;

			// Token: 0x04000557 RID: 1367
			private Int2 delta;

			// Token: 0x04000558 RID: 1368
			private IntRect newTileRect;
		}
	}
}
