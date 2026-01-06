using System;
using System.Collections.Generic;
using Pathfinding.Recast;
using Pathfinding.Serialization;
using Pathfinding.Util;
using Pathfinding.Voxels;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000069 RID: 105
	[JsonOptIn]
	[Preserve]
	public class RecastGraph : NavmeshBase, IUpdatableGraph
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x000205B2 File Offset: 0x0001E7B2
		protected override bool RecalculateNormals
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x000205B5 File Offset: 0x0001E7B5
		public override float TileWorldSizeX
		{
			get
			{
				return (float)this.tileSizeX * this.cellSize;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x000205C5 File Offset: 0x0001E7C5
		public override float TileWorldSizeZ
		{
			get
			{
				return (float)this.tileSizeZ * this.cellSize;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x000205D5 File Offset: 0x0001E7D5
		protected override float MaxTileConnectionEdgeDistance
		{
			get
			{
				return this.walkableClimb;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x000205DD File Offset: 0x0001E7DD
		[Obsolete("Obsolete since this is not accurate when the graph is rotated (rotation was not supported when this property was created)")]
		public Bounds forcedBounds
		{
			get
			{
				return new Bounds(this.forcedBoundsCenter, this.forcedBoundsSize);
			}
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x000205F0 File Offset: 0x0001E7F0
		[Obsolete("Use node.ClosestPointOnNode instead")]
		public Vector3 ClosestPointOnNode(TriangleMeshNode node, Vector3 pos)
		{
			return node.ClosestPointOnNode(pos);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x000205F9 File Offset: 0x0001E7F9
		[Obsolete("Use node.ContainsPoint instead")]
		public bool ContainsPoint(TriangleMeshNode node, Vector3 pos)
		{
			return node.ContainsPoint((Int3)pos);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00020608 File Offset: 0x0001E808
		public void SnapForceBoundsToScene()
		{
			List<RasterizationMesh> list = this.CollectMeshes(new Bounds(Vector3.zero, new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity)));
			if (list.Count == 0)
			{
				return;
			}
			Bounds bounds = list[0].bounds;
			for (int i = 1; i < list.Count; i++)
			{
				bounds.Encapsulate(list[i].bounds);
				list[i].Pool();
			}
			this.forcedBoundsCenter = bounds.center;
			this.forcedBoundsSize = bounds.size;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0002069A File Offset: 0x0001E89A
		GraphUpdateThreading IUpdatableGraph.CanUpdateAsync(GraphUpdateObject o)
		{
			if (!o.updatePhysics)
			{
				return GraphUpdateThreading.SeparateThread;
			}
			return (GraphUpdateThreading)7;
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x000206A8 File Offset: 0x0001E8A8
		void IUpdatableGraph.UpdateAreaInit(GraphUpdateObject o)
		{
			if (!o.updatePhysics)
			{
				return;
			}
			RelevantGraphSurface.UpdateAllPositions();
			IntRect touchingTiles = base.GetTouchingTiles(o.bounds, this.TileBorderSizeInWorldUnits);
			Bounds tileBounds = base.GetTileBounds(touchingTiles);
			tileBounds.Expand(new Vector3(1f, 0f, 1f) * this.TileBorderSizeInWorldUnits * 2f);
			List<RasterizationMesh> list = this.CollectMeshes(tileBounds);
			if (this.globalVox == null)
			{
				this.globalVox = new Voxelize(this.CellHeight, this.cellSize, this.walkableClimb, this.walkableHeight, this.maxSlope, this.maxEdgeLength);
			}
			this.globalVox.inputMeshes = list;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0002075C File Offset: 0x0001E95C
		void IUpdatableGraph.UpdateArea(GraphUpdateObject guo)
		{
			IntRect touchingTiles = base.GetTouchingTiles(guo.bounds, this.TileBorderSizeInWorldUnits);
			if (!guo.updatePhysics)
			{
				for (int i = touchingTiles.ymin; i <= touchingTiles.ymax; i++)
				{
					for (int j = touchingTiles.xmin; j <= touchingTiles.xmax; j++)
					{
						NavmeshTile navmeshTile = this.tiles[i * this.tileXCount + j];
						NavMeshGraph.UpdateArea(guo, navmeshTile);
					}
				}
				return;
			}
			Voxelize voxelize = this.globalVox;
			if (voxelize == null)
			{
				throw new InvalidOperationException("No Voxelizer object. UpdateAreaInit should have been called before this function.");
			}
			for (int k = touchingTiles.xmin; k <= touchingTiles.xmax; k++)
			{
				for (int l = touchingTiles.ymin; l <= touchingTiles.ymax; l++)
				{
					this.stagingTiles.Add(this.BuildTileMesh(voxelize, k, l, 0));
				}
			}
			uint graphIndex = (uint)AstarPath.active.data.GetGraphIndex(this);
			for (int m = 0; m < this.stagingTiles.Count; m++)
			{
				GraphNode[] nodes = this.stagingTiles[m].nodes;
				GraphNode[] array = nodes;
				for (int n = 0; n < array.Length; n++)
				{
					array[n].GraphIndex = graphIndex;
				}
			}
			for (int num = 0; num < voxelize.inputMeshes.Count; num++)
			{
				voxelize.inputMeshes[num].Pool();
			}
			ListPool<RasterizationMesh>.Release(ref voxelize.inputMeshes);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x000208CC File Offset: 0x0001EACC
		void IUpdatableGraph.UpdateAreaPost(GraphUpdateObject guo)
		{
			for (int i = 0; i < this.stagingTiles.Count; i++)
			{
				NavmeshTile navmeshTile = this.stagingTiles[i];
				int num = navmeshTile.x + navmeshTile.z * this.tileXCount;
				NavmeshTile navmeshTile2 = this.tiles[num];
				for (int j = 0; j < navmeshTile2.nodes.Length; j++)
				{
					navmeshTile2.nodes[j].Destroy();
				}
				this.tiles[num] = navmeshTile;
			}
			for (int k = 0; k < this.stagingTiles.Count; k++)
			{
				NavmeshTile navmeshTile3 = this.stagingTiles[k];
				base.ConnectTileWithNeighbours(navmeshTile3, false);
			}
			NavmeshTile[] array = this.stagingTiles.ToArray();
			this.navmeshUpdateData.OnRecalculatedTiles(array);
			if (this.OnRecalculatedTiles != null)
			{
				this.OnRecalculatedTiles(array);
			}
			this.stagingTiles.Clear();
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x000209B4 File Offset: 0x0001EBB4
		protected override IEnumerable<Progress> ScanInternal()
		{
			TriangleMeshNode.SetNavmeshHolder(AstarPath.active.data.GetGraphIndex(this), this);
			if (!Application.isPlaying)
			{
				RelevantGraphSurface.FindAllGraphSurfaces();
			}
			RelevantGraphSurface.UpdateAllPositions();
			foreach (Progress progress in this.ScanAllTiles())
			{
				yield return progress;
			}
			IEnumerator<Progress> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x000209C4 File Offset: 0x0001EBC4
		public override GraphTransform CalculateTransform()
		{
			return new GraphTransform(Matrix4x4.TRS(this.forcedBoundsCenter, Quaternion.Euler(this.rotation), Vector3.one) * Matrix4x4.TRS(-this.forcedBoundsSize * 0.5f, Quaternion.identity, Vector3.one));
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00020A1C File Offset: 0x0001EC1C
		private void InitializeTileInfo()
		{
			int num = (int)(this.forcedBoundsSize.x / this.cellSize + 0.5f);
			int num2 = (int)(this.forcedBoundsSize.z / this.cellSize + 0.5f);
			if (!this.useTiles)
			{
				this.tileSizeX = num;
				this.tileSizeZ = num2;
			}
			else
			{
				this.tileSizeX = this.editorTileSize;
				this.tileSizeZ = this.editorTileSize;
			}
			this.tileXCount = (num + this.tileSizeX - 1) / this.tileSizeX;
			this.tileZCount = (num2 + this.tileSizeZ - 1) / this.tileSizeZ;
			if (this.tileXCount * this.tileZCount > 524288)
			{
				throw new Exception(string.Concat(new string[]
				{
					"Too many tiles (",
					(this.tileXCount * this.tileZCount).ToString(),
					") maximum is ",
					524288.ToString(),
					"\nTry disabling ASTAR_RECAST_LARGER_TILES under the 'Optimizations' tab in the A* inspector."
				}));
			}
			this.tiles = new NavmeshTile[this.tileXCount * this.tileZCount];
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00020B38 File Offset: 0x0001ED38
		private List<RasterizationMesh>[] PutMeshesIntoTileBuckets(List<RasterizationMesh> meshes)
		{
			List<RasterizationMesh>[] array = new List<RasterizationMesh>[this.tiles.Length];
			Vector3 vector = new Vector3(1f, 0f, 1f) * this.TileBorderSizeInWorldUnits * 2f;
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = ListPool<RasterizationMesh>.Claim();
			}
			for (int j = 0; j < meshes.Count; j++)
			{
				RasterizationMesh rasterizationMesh = meshes[j];
				Bounds bounds = rasterizationMesh.bounds;
				bounds.Expand(vector);
				IntRect touchingTiles = base.GetTouchingTiles(bounds, 0f);
				for (int k = touchingTiles.ymin; k <= touchingTiles.ymax; k++)
				{
					for (int l = touchingTiles.xmin; l <= touchingTiles.xmax; l++)
					{
						array[l + k * this.tileXCount].Add(rasterizationMesh);
					}
				}
			}
			return array;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00020C1E File Offset: 0x0001EE1E
		protected IEnumerable<Progress> ScanAllTiles()
		{
			RecastGraph.<>c__DisplayClass50_0 CS$<>8__locals1 = new RecastGraph.<>c__DisplayClass50_0();
			CS$<>8__locals1.<>4__this = this;
			this.transform = this.CalculateTransform();
			this.InitializeTileInfo();
			if (this.scanEmptyGraph)
			{
				base.FillWithEmptyTiles();
				yield break;
			}
			this.walkableClimb = Mathf.Min(this.walkableClimb, this.walkableHeight);
			yield return new Progress(0f, "Finding Meshes");
			Bounds bounds = this.transform.Transform(new Bounds(this.forcedBoundsSize * 0.5f, this.forcedBoundsSize));
			List<RasterizationMesh> meshes = this.CollectMeshes(bounds);
			CS$<>8__locals1.buckets = this.PutMeshesIntoTileBuckets(meshes);
			Queue<Int2> tileQueue = new Queue<Int2>();
			for (int i = 0; i < this.tileZCount; i++)
			{
				for (int j = 0; j < this.tileXCount; j++)
				{
					tileQueue.Enqueue(new Int2(j, i));
				}
			}
			ParallelWorkQueue<Int2> parallelWorkQueue = new ParallelWorkQueue<Int2>(tileQueue);
			CS$<>8__locals1.voxelizers = new Voxelize[parallelWorkQueue.threadCount];
			for (int k = 0; k < CS$<>8__locals1.voxelizers.Length; k++)
			{
				CS$<>8__locals1.voxelizers[k] = new Voxelize(this.CellHeight, this.cellSize, this.walkableClimb, this.walkableHeight, this.maxSlope, this.maxEdgeLength);
			}
			parallelWorkQueue.action = delegate(Int2 tile, int threadIndex)
			{
				CS$<>8__locals1.voxelizers[threadIndex].inputMeshes = CS$<>8__locals1.buckets[tile.x + tile.y * CS$<>8__locals1.<>4__this.tileXCount];
				CS$<>8__locals1.<>4__this.tiles[tile.x + tile.y * CS$<>8__locals1.<>4__this.tileXCount] = CS$<>8__locals1.<>4__this.BuildTileMesh(CS$<>8__locals1.voxelizers[threadIndex], tile.x, tile.y, threadIndex);
			};
			int timeoutMillis = (Application.isPlaying ? 1 : 200);
			foreach (int num in parallelWorkQueue.Run(timeoutMillis))
			{
				yield return new Progress(Mathf.Lerp(0.1f, 0.9f, (float)num / (float)this.tiles.Length), "Calculated Tiles: " + num.ToString() + "/" + this.tiles.Length.ToString());
			}
			IEnumerator<int> enumerator = null;
			yield return new Progress(0.9f, "Assigning Graph Indices");
			CS$<>8__locals1.graphIndex = (uint)AstarPath.active.data.GetGraphIndex(this);
			this.GetNodes(delegate(GraphNode node)
			{
				node.GraphIndex = CS$<>8__locals1.graphIndex;
			});
			int num3;
			for (int coordinateSum = 0; coordinateSum <= 1; coordinateSum = num3 + 1)
			{
				RecastGraph.<>c__DisplayClass50_1 CS$<>8__locals2 = new RecastGraph.<>c__DisplayClass50_1();
				CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
				CS$<>8__locals2.direction = 0;
				while (CS$<>8__locals2.direction <= 1)
				{
					for (int l = 0; l < this.tiles.Length; l++)
					{
						if ((this.tiles[l].x + this.tiles[l].z) % 2 == coordinateSum)
						{
							tileQueue.Enqueue(new Int2(this.tiles[l].x, this.tiles[l].z));
						}
					}
					parallelWorkQueue = new ParallelWorkQueue<Int2>(tileQueue);
					parallelWorkQueue.action = delegate(Int2 tile, int threadIndex)
					{
						if (CS$<>8__locals2.direction == 0 && tile.x < CS$<>8__locals2.CS$<>8__locals1.<>4__this.tileXCount - 1)
						{
							CS$<>8__locals2.CS$<>8__locals1.<>4__this.ConnectTiles(CS$<>8__locals2.CS$<>8__locals1.<>4__this.tiles[tile.x + tile.y * CS$<>8__locals2.CS$<>8__locals1.<>4__this.tileXCount], CS$<>8__locals2.CS$<>8__locals1.<>4__this.tiles[tile.x + 1 + tile.y * CS$<>8__locals2.CS$<>8__locals1.<>4__this.tileXCount]);
						}
						if (CS$<>8__locals2.direction == 1 && tile.y < CS$<>8__locals2.CS$<>8__locals1.<>4__this.tileZCount - 1)
						{
							CS$<>8__locals2.CS$<>8__locals1.<>4__this.ConnectTiles(CS$<>8__locals2.CS$<>8__locals1.<>4__this.tiles[tile.x + tile.y * CS$<>8__locals2.CS$<>8__locals1.<>4__this.tileXCount], CS$<>8__locals2.CS$<>8__locals1.<>4__this.tiles[tile.x + (tile.y + 1) * CS$<>8__locals2.CS$<>8__locals1.<>4__this.tileXCount]);
						}
					};
					int numTilesInQueue = tileQueue.Count;
					foreach (int num2 in parallelWorkQueue.Run(timeoutMillis))
					{
						yield return new Progress(0.95f, string.Concat(new string[]
						{
							"Connected Tiles ",
							(numTilesInQueue - num2).ToString(),
							"/",
							numTilesInQueue.ToString(),
							" (Phase ",
							(CS$<>8__locals2.direction + 1 + 2 * coordinateSum).ToString(),
							" of 4)"
						}));
					}
					enumerator = null;
					num3 = CS$<>8__locals2.direction;
					CS$<>8__locals2.direction = num3 + 1;
				}
				CS$<>8__locals2 = null;
				num3 = coordinateSum;
			}
			for (int m = 0; m < meshes.Count; m++)
			{
				meshes[m].Pool();
			}
			ListPool<RasterizationMesh>.Release(ref meshes);
			this.navmeshUpdateData.OnRecalculatedTiles(this.tiles);
			if (this.OnRecalculatedTiles != null)
			{
				this.OnRecalculatedTiles(this.tiles.Clone() as NavmeshTile[]);
			}
			yield break;
			yield break;
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00020C30 File Offset: 0x0001EE30
		private List<RasterizationMesh> CollectMeshes(Bounds bounds)
		{
			List<RasterizationMesh> list = ListPool<RasterizationMesh>.Claim();
			RecastMeshGatherer recastMeshGatherer = new RecastMeshGatherer(bounds, this.terrainSampleSize, this.mask, this.tagMask, this.colliderRasterizeDetail);
			if (this.rasterizeMeshes)
			{
				recastMeshGatherer.CollectSceneMeshes(list);
			}
			recastMeshGatherer.CollectRecastMeshObjs(list);
			if (this.rasterizeTerrain)
			{
				float num = this.cellSize * (float)Math.Max(this.tileSizeX, this.tileSizeZ);
				recastMeshGatherer.CollectTerrainMeshes(this.rasterizeTrees, num, list);
			}
			if (this.rasterizeColliders)
			{
				recastMeshGatherer.CollectColliderMeshes(list);
			}
			if (list.Count == 0)
			{
				Debug.LogWarning("No MeshFilters were found contained in the layers specified by the 'mask' variables");
			}
			return list;
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x00020CCA File Offset: 0x0001EECA
		private float CellHeight
		{
			get
			{
				return Mathf.Max(this.forcedBoundsSize.y / 64000f, 0.001f);
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x00020CE7 File Offset: 0x0001EEE7
		private int CharacterRadiusInVoxels
		{
			get
			{
				return Mathf.CeilToInt(this.characterRadius / this.cellSize - 0.1f);
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x00020D01 File Offset: 0x0001EF01
		private int TileBorderSizeInVoxels
		{
			get
			{
				return this.CharacterRadiusInVoxels + 3;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x00020D0B File Offset: 0x0001EF0B
		private float TileBorderSizeInWorldUnits
		{
			get
			{
				return (float)this.TileBorderSizeInVoxels * this.cellSize;
			}
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00020D1C File Offset: 0x0001EF1C
		private Bounds CalculateTileBoundsWithBorder(int x, int z)
		{
			Bounds bounds = default(Bounds);
			bounds.SetMinMax(new Vector3((float)x * this.TileWorldSizeX, 0f, (float)z * this.TileWorldSizeZ), new Vector3((float)(x + 1) * this.TileWorldSizeX, this.forcedBoundsSize.y, (float)(z + 1) * this.TileWorldSizeZ));
			bounds.Expand(new Vector3(1f, 0f, 1f) * this.TileBorderSizeInWorldUnits * 2f);
			return bounds;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00020DAC File Offset: 0x0001EFAC
		protected NavmeshTile BuildTileMesh(Voxelize vox, int x, int z, int threadIndex = 0)
		{
			vox.borderSize = this.TileBorderSizeInVoxels;
			vox.forcedBounds = this.CalculateTileBoundsWithBorder(x, z);
			vox.width = this.tileSizeX + vox.borderSize * 2;
			vox.depth = this.tileSizeZ + vox.borderSize * 2;
			if (!this.useTiles && this.relevantGraphSurfaceMode == RecastGraph.RelevantGraphSurfaceMode.OnlyForCompletelyInsideTile)
			{
				vox.relevantGraphSurfaceMode = RecastGraph.RelevantGraphSurfaceMode.RequireForAll;
			}
			else
			{
				vox.relevantGraphSurfaceMode = this.relevantGraphSurfaceMode;
			}
			vox.minRegionSize = Mathf.RoundToInt(this.minRegionSize / (this.cellSize * this.cellSize));
			vox.Init();
			vox.VoxelizeInput(this.transform, this.CalculateTileBoundsWithBorder(x, z));
			vox.FilterLedges(vox.voxelWalkableHeight, vox.voxelWalkableClimb, vox.cellSize, vox.cellHeight);
			vox.FilterLowHeightSpans(vox.voxelWalkableHeight, vox.cellSize, vox.cellHeight);
			vox.BuildCompactField();
			vox.BuildVoxelConnections();
			vox.ErodeWalkableArea(this.CharacterRadiusInVoxels);
			vox.BuildDistanceField();
			vox.BuildRegions();
			VoxelContourSet voxelContourSet = new VoxelContourSet();
			vox.BuildContours(this.contourMaxError, 1, voxelContourSet, 5);
			VoxelMesh voxelMesh;
			vox.BuildPolyMesh(voxelContourSet, 3, out voxelMesh);
			for (int i = 0; i < voxelMesh.verts.Length; i++)
			{
				voxelMesh.verts[i] *= 1000;
			}
			vox.transformVoxel2Graph.Transform(voxelMesh.verts);
			return this.CreateTile(vox, voxelMesh, x, z, threadIndex);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00020F28 File Offset: 0x0001F128
		private NavmeshTile CreateTile(Voxelize vox, VoxelMesh mesh, int x, int z, int threadIndex)
		{
			if (mesh.tris == null)
			{
				throw new ArgumentNullException("mesh.tris");
			}
			if (mesh.verts == null)
			{
				throw new ArgumentNullException("mesh.verts");
			}
			if (mesh.tris.Length % 3 != 0)
			{
				throw new ArgumentException("Indices array's length must be a multiple of 3 (mesh.tris)");
			}
			if (mesh.verts.Length >= 4095)
			{
				if (this.tileXCount * this.tileZCount == 1)
				{
					throw new ArgumentException("Too many vertices per tile (more than " + 4095.ToString() + ").\n<b>Try enabling tiling in the recast graph settings.</b>\n");
				}
				throw new ArgumentException("Too many vertices per tile (more than " + 4095.ToString() + ").\n<b>Try reducing tile size or enabling ASTAR_RECAST_LARGER_TILES under the 'Optimizations' tab in the A* Inspector</b>");
			}
			else
			{
				NavmeshTile navmeshTile = new NavmeshTile
				{
					x = x,
					z = z,
					w = 1,
					d = 1,
					tris = mesh.tris,
					bbTree = new BBTree(),
					graph = this
				};
				navmeshTile.vertsInGraphSpace = Utility.RemoveDuplicateVertices(mesh.verts, navmeshTile.tris);
				navmeshTile.verts = (Int3[])navmeshTile.vertsInGraphSpace.Clone();
				this.transform.Transform(navmeshTile.verts);
				uint num = (uint)(this.active.data.graphs.Length + threadIndex);
				if (num > 255U)
				{
					throw new Exception("Graph limit reached. Multithreaded recast calculations cannot be done because a few scratch graph indices are required.");
				}
				TriangleMeshNode.SetNavmeshHolder((int)num, navmeshTile);
				navmeshTile.nodes = new TriangleMeshNode[navmeshTile.tris.Length / 3];
				AstarPath active = this.active;
				lock (active)
				{
					base.CreateNodes(navmeshTile.nodes, navmeshTile.tris, x + z * this.tileXCount, num);
				}
				navmeshTile.bbTree.RebuildFrom(navmeshTile.nodes);
				NavmeshBase.CreateNodeConnections(navmeshTile.nodes);
				TriangleMeshNode.SetNavmeshHolder((int)num, null);
				return navmeshTile;
			}
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0002110C File Offset: 0x0001F30C
		protected override void DeserializeSettingsCompatibility(GraphSerializationContext ctx)
		{
			base.DeserializeSettingsCompatibility(ctx);
			this.characterRadius = ctx.reader.ReadSingle();
			this.contourMaxError = ctx.reader.ReadSingle();
			this.cellSize = ctx.reader.ReadSingle();
			ctx.reader.ReadSingle();
			this.walkableHeight = ctx.reader.ReadSingle();
			this.maxSlope = ctx.reader.ReadSingle();
			this.maxEdgeLength = ctx.reader.ReadSingle();
			this.editorTileSize = ctx.reader.ReadInt32();
			this.tileSizeX = ctx.reader.ReadInt32();
			this.nearestSearchOnlyXZ = ctx.reader.ReadBoolean();
			this.useTiles = ctx.reader.ReadBoolean();
			this.relevantGraphSurfaceMode = (RecastGraph.RelevantGraphSurfaceMode)ctx.reader.ReadInt32();
			this.rasterizeColliders = ctx.reader.ReadBoolean();
			this.rasterizeMeshes = ctx.reader.ReadBoolean();
			this.rasterizeTerrain = ctx.reader.ReadBoolean();
			this.rasterizeTrees = ctx.reader.ReadBoolean();
			this.colliderRasterizeDetail = ctx.reader.ReadSingle();
			this.forcedBoundsCenter = ctx.DeserializeVector3();
			this.forcedBoundsSize = ctx.DeserializeVector3();
			this.mask = ctx.reader.ReadInt32();
			int num = ctx.reader.ReadInt32();
			this.tagMask = new List<string>(num);
			for (int i = 0; i < num; i++)
			{
				this.tagMask.Add(ctx.reader.ReadString());
			}
			this.showMeshOutline = ctx.reader.ReadBoolean();
			this.showNodeConnections = ctx.reader.ReadBoolean();
			this.terrainSampleSize = ctx.reader.ReadInt32();
			this.walkableClimb = ctx.DeserializeFloat(this.walkableClimb);
			this.minRegionSize = ctx.DeserializeFloat(this.minRegionSize);
			this.tileSizeZ = ctx.DeserializeInt(this.tileSizeX);
			this.showMeshSurface = ctx.reader.ReadBoolean();
		}

		// Token: 0x04000326 RID: 806
		[JsonMember]
		public float characterRadius = 1.5f;

		// Token: 0x04000327 RID: 807
		[JsonMember]
		public float contourMaxError = 2f;

		// Token: 0x04000328 RID: 808
		[JsonMember]
		public float cellSize = 0.5f;

		// Token: 0x04000329 RID: 809
		[JsonMember]
		public float walkableHeight = 2f;

		// Token: 0x0400032A RID: 810
		[JsonMember]
		public float walkableClimb = 0.5f;

		// Token: 0x0400032B RID: 811
		[JsonMember]
		public float maxSlope = 30f;

		// Token: 0x0400032C RID: 812
		[JsonMember]
		public float maxEdgeLength = 20f;

		// Token: 0x0400032D RID: 813
		[JsonMember]
		public float minRegionSize = 3f;

		// Token: 0x0400032E RID: 814
		[JsonMember]
		public int editorTileSize = 128;

		// Token: 0x0400032F RID: 815
		[JsonMember]
		public int tileSizeX = 128;

		// Token: 0x04000330 RID: 816
		[JsonMember]
		public int tileSizeZ = 128;

		// Token: 0x04000331 RID: 817
		[JsonMember]
		public bool useTiles = true;

		// Token: 0x04000332 RID: 818
		public bool scanEmptyGraph;

		// Token: 0x04000333 RID: 819
		[JsonMember]
		public RecastGraph.RelevantGraphSurfaceMode relevantGraphSurfaceMode;

		// Token: 0x04000334 RID: 820
		[JsonMember]
		public bool rasterizeColliders;

		// Token: 0x04000335 RID: 821
		[JsonMember]
		public bool rasterizeMeshes = true;

		// Token: 0x04000336 RID: 822
		[JsonMember]
		public bool rasterizeTerrain = true;

		// Token: 0x04000337 RID: 823
		[JsonMember]
		public bool rasterizeTrees = true;

		// Token: 0x04000338 RID: 824
		[JsonMember]
		public float colliderRasterizeDetail = 10f;

		// Token: 0x04000339 RID: 825
		[JsonMember]
		public LayerMask mask = -1;

		// Token: 0x0400033A RID: 826
		[JsonMember]
		public List<string> tagMask = new List<string>();

		// Token: 0x0400033B RID: 827
		[JsonMember]
		public int terrainSampleSize = 3;

		// Token: 0x0400033C RID: 828
		[JsonMember]
		public Vector3 rotation;

		// Token: 0x0400033D RID: 829
		[JsonMember]
		public Vector3 forcedBoundsCenter;

		// Token: 0x0400033E RID: 830
		private Voxelize globalVox;

		// Token: 0x0400033F RID: 831
		public const int BorderVertexMask = 1;

		// Token: 0x04000340 RID: 832
		public const int BorderVertexOffset = 31;

		// Token: 0x04000341 RID: 833
		private List<NavmeshTile> stagingTiles = new List<NavmeshTile>();

		// Token: 0x02000123 RID: 291
		public enum RelevantGraphSurfaceMode
		{
			// Token: 0x040006C9 RID: 1737
			DoNotRequire,
			// Token: 0x040006CA RID: 1738
			OnlyForCompletelyInsideTile,
			// Token: 0x040006CB RID: 1739
			RequireForAll
		}
	}
}
