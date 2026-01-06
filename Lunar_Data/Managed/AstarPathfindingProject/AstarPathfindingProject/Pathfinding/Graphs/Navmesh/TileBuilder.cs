using System;
using System.Collections.Generic;
using Pathfinding.Graphs.Navmesh.Jobs;
using Pathfinding.Graphs.Navmesh.Voxelization.Burst;
using Pathfinding.Jobs;
using Pathfinding.Util;
using Unity.Collections;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pathfinding.Graphs.Navmesh
{
	// Token: 0x020001BC RID: 444
	public struct TileBuilder
	{
		// Token: 0x06000BB0 RID: 2992 RVA: 0x00042EE8 File Offset: 0x000410E8
		public TileBuilder(RecastGraph graph, TileLayout tileLayout, IntRect tileRect)
		{
			this.tileLayout = tileLayout;
			this.tileRect = tileRect;
			this.walkableClimb = Mathf.Min(graph.walkableClimb, graph.walkableHeight);
			this.terrainSampleSize = graph.terrainSampleSize;
			this.mask = graph.mask;
			this.tagMask = graph.tagMask;
			this.colliderRasterizeDetail = graph.colliderRasterizeDetail;
			this.rasterizeTerrain = graph.rasterizeTerrain;
			this.rasterizeMeshes = graph.rasterizeMeshes;
			this.rasterizeTrees = graph.rasterizeTrees;
			this.rasterizeColliders = graph.rasterizeColliders;
			this.dimensionMode = graph.dimensionMode;
			this.backgroundTraversability = graph.backgroundTraversability;
			this.tileBorderSizeInVoxels = graph.TileBorderSizeInVoxels;
			this.walkableHeight = graph.walkableHeight;
			this.maxSlope = graph.maxSlope;
			this.characterRadiusInVoxels = graph.CharacterRadiusInVoxels;
			this.minRegionSize = Mathf.RoundToInt(graph.minRegionSize);
			this.maxEdgeLength = graph.maxEdgeLength;
			this.contourMaxError = graph.contourMaxError;
			this.relevantGraphSurfaceMode = graph.relevantGraphSurfaceMode;
			this.scene = graph.active.gameObject.scene;
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000BB1 RID: 2993 RVA: 0x0004300D File Offset: 0x0004120D
		private int TileBorderSizeInVoxels
		{
			get
			{
				return this.characterRadiusInVoxels + 3;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x00043017 File Offset: 0x00041217
		private float TileBorderSizeInWorldUnits
		{
			get
			{
				return (float)this.TileBorderSizeInVoxels * this.tileLayout.cellSize;
			}
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0004302C File Offset: 0x0004122C
		public Bounds GetWorldSpaceBounds(float xzPadding = 0f)
		{
			Bounds tileBoundsInGraphSpace = this.tileLayout.GetTileBoundsInGraphSpace(this.tileRect.xmin, this.tileRect.ymin, this.tileRect.Width, this.tileRect.Height);
			tileBoundsInGraphSpace.Expand(new Vector3(2f * xzPadding, 0f, 2f * xzPadding));
			return this.tileLayout.transform.Transform(tileBoundsInGraphSpace);
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x000430A4 File Offset: 0x000412A4
		public RecastMeshGatherer.MeshCollection CollectMeshes(Bounds bounds)
		{
			RecastMeshGatherer recastMeshGatherer = new RecastMeshGatherer(this.scene, bounds, this.terrainSampleSize, this.mask, this.tagMask, this.tileLayout.cellSize / this.colliderRasterizeDetail);
			if (this.rasterizeMeshes && this.dimensionMode == RecastGraph.DimensionMode.Dimension3D)
			{
				recastMeshGatherer.CollectSceneMeshes();
			}
			recastMeshGatherer.CollectRecastMeshObjs();
			if (this.rasterizeTerrain && this.dimensionMode == RecastGraph.DimensionMode.Dimension3D)
			{
				float num = this.tileLayout.cellSize * (float)math.max(this.tileLayout.tileSizeInVoxels.x, this.tileLayout.tileSizeInVoxels.y);
				recastMeshGatherer.CollectTerrainMeshes(this.rasterizeTrees, num);
			}
			if (this.rasterizeColliders)
			{
				if (this.dimensionMode == RecastGraph.DimensionMode.Dimension3D)
				{
					recastMeshGatherer.CollectColliderMeshes();
				}
				else
				{
					recastMeshGatherer.Collect2DColliderMeshes();
				}
			}
			RecastMeshGatherer.MeshCollection meshCollection = recastMeshGatherer.Finalize();
			if (this.tileRect == new IntRect(0, 0, this.tileLayout.tileCount.x - 1, this.tileLayout.tileCount.y - 1) && meshCollection.meshes.Length == 0)
			{
				Debug.LogWarning("No rasterizable objects were found contained in the layers specified by the 'mask' variables");
			}
			return meshCollection;
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x000431C8 File Offset: 0x000413C8
		private TileBuilder.BucketMapping PutMeshesIntoTileBuckets(RecastMeshGatherer.MeshCollection meshCollection, IntRect tileBuckets)
		{
			int num = tileBuckets.Width * tileBuckets.Height;
			NativeList<int>[] array = new NativeList<int>[num];
			float tileBorderSizeInWorldUnits = this.TileBorderSizeInWorldUnits;
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new NativeList<int>(Allocator.Persistent);
			}
			Int2 @int = -tileBuckets.Min;
			IntRect intRect = new IntRect(0, 0, tileBuckets.Width - 1, tileBuckets.Height - 1);
			NativeArray<RasterizationMesh> meshes = meshCollection.meshes;
			for (int j = 0; j < meshes.Length; j++)
			{
				Bounds bounds = meshes[j].bounds;
				IntRect intRect2 = IntRect.Intersection(this.tileLayout.GetTouchingTiles(bounds, tileBorderSizeInWorldUnits).Offset(@int), intRect);
				for (int k = intRect2.ymin; k <= intRect2.ymax; k++)
				{
					for (int l = intRect2.xmin; l <= intRect2.xmax; l++)
					{
						array[l + k * tileBuckets.Width].Add(in j);
					}
				}
			}
			int num2 = 0;
			for (int m = 0; m < array.Length; m++)
			{
				num2 += array[m].Length;
			}
			NativeArray<int> nativeArray = new NativeArray<int>(num2, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			NativeArray<int> nativeArray2 = new NativeArray<int>(num, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			num2 = 0;
			for (int n = 0; n < array.Length; n++)
			{
				if (array[n].Length > 0)
				{
					NativeArray<int>.Copy(array[n].AsArray(), 0, nativeArray, num2, array[n].Length);
				}
				num2 += array[n].Length;
				nativeArray2[n] = num2;
				array[n].Dispose();
			}
			return new TileBuilder.BucketMapping
			{
				meshes = meshCollection.meshes,
				pointers = nativeArray,
				bucketRanges = nativeArray2
			};
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x000433C0 File Offset: 0x000415C0
		public Promise<TileBuilder.TileBuilderOutput> Schedule(DisposeArena arena)
		{
			int area = this.tileRect.Area;
			int width = this.tileRect.Width;
			int height = this.tileRect.Height;
			Bounds worldSpaceBounds = this.GetWorldSpaceBounds(this.TileBorderSizeInWorldUnits);
			if (this.dimensionMode == RecastGraph.DimensionMode.Dimension2D)
			{
				worldSpaceBounds.extents = new Vector3(worldSpaceBounds.extents.x, worldSpaceBounds.extents.y, float.PositiveInfinity);
			}
			RecastMeshGatherer.MeshCollection meshCollection = this.CollectMeshes(worldSpaceBounds);
			TileBuilder.BucketMapping bucketMapping = this.PutMeshesIntoTileBuckets(meshCollection, this.tileRect);
			NativeArray<TileMesh.TileMeshUnsafe> nativeArray = new NativeArray<TileMesh.TileMeshUnsafe>(area, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			int num = this.tileLayout.tileSizeInVoxels.x + this.tileBorderSizeInVoxels * 2;
			int num2 = this.tileLayout.tileSizeInVoxels.y + this.tileBorderSizeInVoxels * 2;
			float cellHeight = this.tileLayout.CellHeight;
			uint num3 = (uint)(this.walkableHeight / cellHeight);
			int num4 = Mathf.RoundToInt(this.walkableClimb / cellHeight);
			NativeArray<Bounds> nativeArray2 = new NativeArray<Bounds>(area, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					int num5 = j + i * width;
					Bounds tileBoundsInGraphSpace = this.tileLayout.GetTileBoundsInGraphSpace(this.tileRect.xmin + j, this.tileRect.ymin + i, 1, 1);
					tileBoundsInGraphSpace.Expand(new Vector3(1f, 0f, 1f) * this.TileBorderSizeInWorldUnits * 2f);
					nativeArray2[num5] = tileBoundsInGraphSpace;
				}
			}
			TileBuilderBurst[] array = new TileBuilderBurst[Mathf.Max(1, Mathf.Min(area, JobsUtility.JobWorkerCount + 1))];
			NativeReference<int> nativeReference = new NativeReference<int>(0, Allocator.Persistent);
			JobHandle jobHandle = default(JobHandle);
			NativeList<JobBuildRegions.RelevantGraphSurfaceInfo> nativeList = new NativeList<JobBuildRegions.RelevantGraphSurfaceInfo>(Allocator.Persistent);
			RelevantGraphSurface relevantGraphSurface = RelevantGraphSurface.Root;
			while (relevantGraphSurface != null)
			{
				JobBuildRegions.RelevantGraphSurfaceInfo relevantGraphSurfaceInfo = default(JobBuildRegions.RelevantGraphSurfaceInfo);
				relevantGraphSurfaceInfo.position = relevantGraphSurface.transform.position;
				relevantGraphSurfaceInfo.range = relevantGraphSurface.maxRange;
				nativeList.Add(in relevantGraphSurfaceInfo);
				relevantGraphSurface = relevantGraphSurface.Next;
			}
			int num6 = Mathf.CeilToInt(Mathf.Sqrt((float)area));
			int num7 = num6 * array.Length;
			int num8 = 2 * (area + num7 - 1) / num7;
			JobBuildTileMeshFromVoxels jobBuildTileMeshFromVoxels = new JobBuildTileMeshFromVoxels
			{
				tileBuilder = array[0],
				inputMeshes = bucketMapping,
				tileGraphSpaceBounds = nativeArray2,
				voxelWalkableClimb = num4,
				voxelWalkableHeight = num3,
				voxelToTileSpace = Matrix4x4.Scale(new Vector3(this.tileLayout.cellSize, cellHeight, this.tileLayout.cellSize)) * Matrix4x4.Translate(-new Vector3(1f, 0f, 1f) * (float)this.TileBorderSizeInVoxels),
				cellSize = this.tileLayout.cellSize,
				cellHeight = cellHeight,
				maxSlope = Mathf.Max(this.maxSlope, 0.0001f),
				dimensionMode = this.dimensionMode,
				backgroundTraversability = this.backgroundTraversability,
				graphToWorldSpace = this.tileLayout.transform.matrix,
				characterRadiusInVoxels = this.characterRadiusInVoxels,
				tileBorderSizeInVoxels = this.tileBorderSizeInVoxels,
				minRegionSize = this.minRegionSize,
				maxEdgeLength = this.maxEdgeLength,
				contourMaxError = this.contourMaxError,
				maxTiles = num6,
				relevantGraphSurfaces = nativeList.AsArray(),
				relevantGraphSurfaceMode = this.relevantGraphSurfaceMode
			};
			jobBuildTileMeshFromVoxels.SetOutputMeshes(nativeArray);
			jobBuildTileMeshFromVoxels.SetCounter(nativeReference);
			int num9 = (int)(this.tileLayout.boundsYSize / cellHeight);
			for (int k = 0; k < array.Length; k++)
			{
				jobBuildTileMeshFromVoxels.tileBuilder = (array[k] = new TileBuilderBurst(num, num2, (int)num3, num9));
				JobHandle jobHandle2 = default(JobHandle);
				for (int l = 0; l < num8; l++)
				{
					jobHandle2 = jobBuildTileMeshFromVoxels.Schedule(jobHandle2);
				}
				jobHandle = JobHandle.CombineDependencies(jobHandle, jobHandle2);
			}
			JobHandle.ScheduleBatchedJobs();
			arena.Add<Bounds>(nativeArray2);
			arena.Add<JobBuildRegions.RelevantGraphSurfaceInfo>(nativeList);
			arena.Add<int>(bucketMapping.bucketRanges);
			arena.Add<int>(bucketMapping.pointers);
			arena.Add<RecastMeshGatherer.MeshCollection>(meshCollection);
			for (int m = 0; m < array.Length; m++)
			{
				arena.Add<TileBuilderBurst>(array[m]);
			}
			return new Promise<TileBuilder.TileBuilderOutput>(jobHandle, new TileBuilder.TileBuilderOutput
			{
				tileMeshes = new TileMeshesUnsafe(nativeArray, this.tileRect, new Vector2(this.tileLayout.TileWorldSizeX, this.tileLayout.TileWorldSizeZ)),
				currentTileCounter = nativeReference
			});
		}

		// Token: 0x04000813 RID: 2067
		public float walkableClimb;

		// Token: 0x04000814 RID: 2068
		public int terrainSampleSize;

		// Token: 0x04000815 RID: 2069
		public LayerMask mask;

		// Token: 0x04000816 RID: 2070
		public List<string> tagMask;

		// Token: 0x04000817 RID: 2071
		public RecastGraph.RelevantGraphSurfaceMode relevantGraphSurfaceMode;

		// Token: 0x04000818 RID: 2072
		public float colliderRasterizeDetail;

		// Token: 0x04000819 RID: 2073
		public RecastGraph.DimensionMode dimensionMode;

		// Token: 0x0400081A RID: 2074
		public RecastGraph.BackgroundTraversability backgroundTraversability;

		// Token: 0x0400081B RID: 2075
		public bool rasterizeTerrain;

		// Token: 0x0400081C RID: 2076
		public bool rasterizeMeshes;

		// Token: 0x0400081D RID: 2077
		public bool rasterizeTrees;

		// Token: 0x0400081E RID: 2078
		public bool rasterizeColliders;

		// Token: 0x0400081F RID: 2079
		public int tileBorderSizeInVoxels;

		// Token: 0x04000820 RID: 2080
		public float walkableHeight;

		// Token: 0x04000821 RID: 2081
		public float maxSlope;

		// Token: 0x04000822 RID: 2082
		public int characterRadiusInVoxels;

		// Token: 0x04000823 RID: 2083
		public int minRegionSize;

		// Token: 0x04000824 RID: 2084
		public float maxEdgeLength;

		// Token: 0x04000825 RID: 2085
		public float contourMaxError;

		// Token: 0x04000826 RID: 2086
		public Scene scene;

		// Token: 0x04000827 RID: 2087
		public TileLayout tileLayout;

		// Token: 0x04000828 RID: 2088
		public IntRect tileRect;

		// Token: 0x020001BD RID: 445
		public class TileBuilderOutput : IProgress, IDisposable
		{
			// Token: 0x170001A6 RID: 422
			// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x00043888 File Offset: 0x00041A88
			public float Progress
			{
				get
				{
					int area = this.tileMeshes.tileRect.Area;
					int num = Mathf.Min(area, this.currentTileCounter.Value);
					if (area <= 0)
					{
						return 0f;
					}
					return (float)num / (float)area;
				}
			}

			// Token: 0x06000BB8 RID: 3000 RVA: 0x000438C7 File Offset: 0x00041AC7
			public void Dispose()
			{
				this.tileMeshes.Dispose();
				if (this.currentTileCounter.IsCreated)
				{
					this.currentTileCounter.Dispose();
				}
			}

			// Token: 0x04000829 RID: 2089
			public NativeReference<int> currentTileCounter;

			// Token: 0x0400082A RID: 2090
			public TileMeshesUnsafe tileMeshes;
		}

		// Token: 0x020001BE RID: 446
		public struct BucketMapping
		{
			// Token: 0x0400082B RID: 2091
			public NativeArray<RasterizationMesh> meshes;

			// Token: 0x0400082C RID: 2092
			public NativeArray<int> pointers;

			// Token: 0x0400082D RID: 2093
			public NativeArray<int> bucketRanges;
		}
	}
}
