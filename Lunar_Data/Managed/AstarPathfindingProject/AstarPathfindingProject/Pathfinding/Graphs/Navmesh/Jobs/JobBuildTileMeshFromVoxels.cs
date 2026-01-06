using System;
using System.Threading;
using Pathfinding.Graphs.Navmesh.Voxelization.Burst;
using Pathfinding.Util;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Profiling;
using UnityEngine;

namespace Pathfinding.Graphs.Navmesh.Jobs
{
	// Token: 0x020001EE RID: 494
	[BurstCompile(CompileSynchronously = true)]
	public struct JobBuildTileMeshFromVoxels : IJob
	{
		// Token: 0x06000C60 RID: 3168 RVA: 0x0004C911 File Offset: 0x0004AB11
		public unsafe void SetOutputMeshes(NativeArray<TileMesh.TileMeshUnsafe> arr)
		{
			this.outputMeshes = (TileMesh.TileMeshUnsafe*)arr.GetUnsafeReadOnlyPtr<TileMesh.TileMeshUnsafe>();
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0004C91F File Offset: 0x0004AB1F
		public unsafe void SetCounter(NativeReference<int> counter)
		{
			this.currentTileCounter = (int*)counter.GetUnsafePtr<int>();
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0004C930 File Offset: 0x0004AB30
		public unsafe void Execute()
		{
			for (int i = 0; i < this.maxTiles; i++)
			{
				int num = Interlocked.Increment(UnsafeUtility.AsRef<int>((void*)this.currentTileCounter)) - 1;
				if (num >= this.tileGraphSpaceBounds.Length)
				{
					return;
				}
				this.tileBuilder.linkedVoxelField.ResetLinkedVoxelSpans();
				if (this.dimensionMode == RecastGraph.DimensionMode.Dimension2D && this.backgroundTraversability == RecastGraph.BackgroundTraversability.Walkable)
				{
					this.tileBuilder.linkedVoxelField.SetWalkableBackground();
				}
				int num2 = ((num > 0) ? this.inputMeshes.bucketRanges[num - 1] : 0);
				int num3 = this.inputMeshes.bucketRanges[num];
				JobVoxelize jobVoxelize = default(JobVoxelize);
				jobVoxelize.inputMeshes = this.inputMeshes.meshes;
				jobVoxelize.bucket = this.inputMeshes.pointers.GetSubArray(num2, num3 - num2);
				jobVoxelize.voxelWalkableClimb = this.voxelWalkableClimb;
				jobVoxelize.voxelWalkableHeight = this.voxelWalkableHeight;
				jobVoxelize.cellSize = this.cellSize;
				jobVoxelize.cellHeight = this.cellHeight;
				jobVoxelize.maxSlope = this.maxSlope;
				jobVoxelize.graphTransform = this.graphToWorldSpace;
				jobVoxelize.graphSpaceBounds = this.tileGraphSpaceBounds[num];
				jobVoxelize.voxelArea = this.tileBuilder.linkedVoxelField;
				jobVoxelize.Execute();
				JobFilterLedges jobFilterLedges = default(JobFilterLedges);
				jobFilterLedges.field = this.tileBuilder.linkedVoxelField;
				jobFilterLedges.voxelWalkableClimb = this.voxelWalkableClimb;
				jobFilterLedges.voxelWalkableHeight = this.voxelWalkableHeight;
				jobFilterLedges.cellSize = this.cellSize;
				jobFilterLedges.cellHeight = this.cellHeight;
				jobFilterLedges.Execute();
				JobFilterLowHeightSpans jobFilterLowHeightSpans = default(JobFilterLowHeightSpans);
				jobFilterLowHeightSpans.field = this.tileBuilder.linkedVoxelField;
				jobFilterLowHeightSpans.voxelWalkableHeight = this.voxelWalkableHeight;
				jobFilterLowHeightSpans.Execute();
				JobBuildCompactField jobBuildCompactField = default(JobBuildCompactField);
				jobBuildCompactField.input = this.tileBuilder.linkedVoxelField;
				jobBuildCompactField.output = this.tileBuilder.compactVoxelField;
				jobBuildCompactField.Execute();
				JobBuildConnections jobBuildConnections = default(JobBuildConnections);
				jobBuildConnections.field = this.tileBuilder.compactVoxelField;
				jobBuildConnections.voxelWalkableHeight = (int)this.voxelWalkableHeight;
				jobBuildConnections.voxelWalkableClimb = this.voxelWalkableClimb;
				jobBuildConnections.Execute();
				JobErodeWalkableArea jobErodeWalkableArea = default(JobErodeWalkableArea);
				jobErodeWalkableArea.field = this.tileBuilder.compactVoxelField;
				jobErodeWalkableArea.radius = this.characterRadiusInVoxels;
				jobErodeWalkableArea.Execute();
				JobBuildDistanceField jobBuildDistanceField = default(JobBuildDistanceField);
				jobBuildDistanceField.field = this.tileBuilder.compactVoxelField;
				jobBuildDistanceField.output = this.tileBuilder.distanceField;
				jobBuildDistanceField.Execute();
				JobBuildRegions jobBuildRegions = default(JobBuildRegions);
				jobBuildRegions.field = this.tileBuilder.compactVoxelField;
				jobBuildRegions.distanceField = this.tileBuilder.distanceField;
				jobBuildRegions.borderSize = this.tileBorderSizeInVoxels;
				jobBuildRegions.minRegionSize = Mathf.RoundToInt((float)this.minRegionSize);
				jobBuildRegions.srcQue = this.tileBuilder.tmpQueue1;
				jobBuildRegions.dstQue = this.tileBuilder.tmpQueue2;
				jobBuildRegions.relevantGraphSurfaces = this.relevantGraphSurfaces;
				jobBuildRegions.relevantGraphSurfaceMode = this.relevantGraphSurfaceMode;
				jobBuildRegions.cellSize = this.cellSize;
				jobBuildRegions.cellHeight = this.cellHeight;
				jobBuildRegions.graphTransform = this.graphToWorldSpace;
				jobBuildRegions.graphSpaceBounds = this.tileGraphSpaceBounds[num];
				jobBuildRegions.Execute();
				JobBuildContours jobBuildContours = default(JobBuildContours);
				jobBuildContours.field = this.tileBuilder.compactVoxelField;
				jobBuildContours.maxError = this.contourMaxError;
				jobBuildContours.maxEdgeLength = this.maxEdgeLength;
				jobBuildContours.buildFlags = 5;
				jobBuildContours.cellSize = this.cellSize;
				jobBuildContours.outputContours = this.tileBuilder.contours;
				jobBuildContours.outputVerts = this.tileBuilder.contourVertices;
				jobBuildContours.Execute();
				JobBuildMesh jobBuildMesh = default(JobBuildMesh);
				jobBuildMesh.contours = this.tileBuilder.contours;
				jobBuildMesh.contourVertices = this.tileBuilder.contourVertices;
				jobBuildMesh.mesh = this.tileBuilder.voxelMesh;
				jobBuildMesh.field = this.tileBuilder.compactVoxelField;
				jobBuildMesh.Execute();
				TileMesh.TileMeshUnsafe* ptr = this.outputMeshes + num;
				*ptr = new TileMesh.TileMeshUnsafe
				{
					verticesInTileSpace = new UnsafeAppendBuffer(0, 4, Allocator.Persistent),
					triangles = new UnsafeAppendBuffer(0, 4, Allocator.Persistent),
					tags = new UnsafeAppendBuffer(0, 4, Allocator.Persistent)
				};
				JobConvertAreasToTags jobConvertAreasToTags = default(JobConvertAreasToTags);
				jobConvertAreasToTags.areas = this.tileBuilder.voxelMesh.areas;
				jobConvertAreasToTags.Execute();
				MeshUtility.JobRemoveDuplicateVertices jobRemoveDuplicateVertices = default(MeshUtility.JobRemoveDuplicateVertices);
				jobRemoveDuplicateVertices.vertices = this.tileBuilder.voxelMesh.verts.AsArray();
				jobRemoveDuplicateVertices.triangles = this.tileBuilder.voxelMesh.tris.AsArray();
				jobRemoveDuplicateVertices.tags = this.tileBuilder.voxelMesh.areas.AsArray();
				jobRemoveDuplicateVertices.outputTags = &ptr->tags;
				jobRemoveDuplicateVertices.outputVertices = &ptr->verticesInTileSpace;
				jobRemoveDuplicateVertices.outputTriangles = &ptr->triangles;
				jobRemoveDuplicateVertices.Execute();
				JobTransformTileCoordinates jobTransformTileCoordinates = default(JobTransformTileCoordinates);
				jobTransformTileCoordinates.vertices = &ptr->verticesInTileSpace;
				jobTransformTileCoordinates.matrix = this.voxelToTileSpace;
				jobTransformTileCoordinates.Execute();
			}
		}

		// Token: 0x04000907 RID: 2311
		public TileBuilderBurst tileBuilder;

		// Token: 0x04000908 RID: 2312
		[ReadOnly]
		public TileBuilder.BucketMapping inputMeshes;

		// Token: 0x04000909 RID: 2313
		[ReadOnly]
		public NativeArray<Bounds> tileGraphSpaceBounds;

		// Token: 0x0400090A RID: 2314
		public Matrix4x4 voxelToTileSpace;

		// Token: 0x0400090B RID: 2315
		[NativeDisableUnsafePtrRestriction]
		public unsafe TileMesh.TileMeshUnsafe* outputMeshes;

		// Token: 0x0400090C RID: 2316
		public int maxTiles;

		// Token: 0x0400090D RID: 2317
		public int voxelWalkableClimb;

		// Token: 0x0400090E RID: 2318
		public uint voxelWalkableHeight;

		// Token: 0x0400090F RID: 2319
		public float cellSize;

		// Token: 0x04000910 RID: 2320
		public float cellHeight;

		// Token: 0x04000911 RID: 2321
		public float maxSlope;

		// Token: 0x04000912 RID: 2322
		public RecastGraph.DimensionMode dimensionMode;

		// Token: 0x04000913 RID: 2323
		public RecastGraph.BackgroundTraversability backgroundTraversability;

		// Token: 0x04000914 RID: 2324
		public Matrix4x4 graphToWorldSpace;

		// Token: 0x04000915 RID: 2325
		public int characterRadiusInVoxels;

		// Token: 0x04000916 RID: 2326
		public int tileBorderSizeInVoxels;

		// Token: 0x04000917 RID: 2327
		public int minRegionSize;

		// Token: 0x04000918 RID: 2328
		public float maxEdgeLength;

		// Token: 0x04000919 RID: 2329
		public float contourMaxError;

		// Token: 0x0400091A RID: 2330
		[ReadOnly]
		public NativeArray<JobBuildRegions.RelevantGraphSurfaceInfo> relevantGraphSurfaces;

		// Token: 0x0400091B RID: 2331
		public RecastGraph.RelevantGraphSurfaceMode relevantGraphSurfaceMode;

		// Token: 0x0400091C RID: 2332
		[NativeDisableUnsafePtrRestriction]
		public unsafe int* currentTileCounter;

		// Token: 0x0400091D RID: 2333
		private static readonly ProfilerMarker MarkerVoxelize = new ProfilerMarker("Voxelize");

		// Token: 0x0400091E RID: 2334
		private static readonly ProfilerMarker MarkerFilterLedges = new ProfilerMarker("FilterLedges");

		// Token: 0x0400091F RID: 2335
		private static readonly ProfilerMarker MarkerFilterLowHeightSpans = new ProfilerMarker("FilterLowHeightSpans");

		// Token: 0x04000920 RID: 2336
		private static readonly ProfilerMarker MarkerBuildCompactField = new ProfilerMarker("BuildCompactField");

		// Token: 0x04000921 RID: 2337
		private static readonly ProfilerMarker MarkerBuildConnections = new ProfilerMarker("BuildConnections");

		// Token: 0x04000922 RID: 2338
		private static readonly ProfilerMarker MarkerErodeWalkableArea = new ProfilerMarker("ErodeWalkableArea");

		// Token: 0x04000923 RID: 2339
		private static readonly ProfilerMarker MarkerBuildDistanceField = new ProfilerMarker("BuildDistanceField");

		// Token: 0x04000924 RID: 2340
		private static readonly ProfilerMarker MarkerBuildRegions = new ProfilerMarker("BuildRegions");

		// Token: 0x04000925 RID: 2341
		private static readonly ProfilerMarker MarkerBuildContours = new ProfilerMarker("BuildContours");

		// Token: 0x04000926 RID: 2342
		private static readonly ProfilerMarker MarkerBuildMesh = new ProfilerMarker("BuildMesh");

		// Token: 0x04000927 RID: 2343
		private static readonly ProfilerMarker MarkerConvertAreasToTags = new ProfilerMarker("ConvertAreasToTags");

		// Token: 0x04000928 RID: 2344
		private static readonly ProfilerMarker MarkerRemoveDuplicateVertices = new ProfilerMarker("RemoveDuplicateVertices");

		// Token: 0x04000929 RID: 2345
		private static readonly ProfilerMarker MarkerTransformTileCoordinates = new ProfilerMarker("TransformTileCoordinates");
	}
}
