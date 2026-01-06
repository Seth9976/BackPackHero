using System;
using Pathfinding.Util;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;

namespace Pathfinding.Graphs.Navmesh.Jobs
{
	// Token: 0x020001EA RID: 490
	[BurstCompile(FloatMode = FloatMode.Default)]
	public struct JobBuildTileMeshFromVertices : IJob
	{
		// Token: 0x06000C59 RID: 3161 RVA: 0x0004C53C File Offset: 0x0004A73C
		public static Promise<JobBuildTileMeshFromVertices.BuildNavmeshOutput> Schedule(NativeArray<Vector3> vertices, NativeArray<int> indices, Matrix4x4 meshToGraph, bool recalculateNormals)
		{
			if (vertices.Length > 4095)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Too many vertices in the navmesh graph. Provided ",
					vertices.Length.ToString(),
					", but the maximum number of vertices per tile is ",
					4095.ToString(),
					". You can raise this limit by enabling ASTAR_RECAST_LARGER_TILES in the A* Inspector Optimizations tab"
				}));
			}
			NativeArray<TileMesh.TileMeshUnsafe> nativeArray = new NativeArray<TileMesh.TileMeshUnsafe>(1, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			return new Promise<JobBuildTileMeshFromVertices.BuildNavmeshOutput>(new JobBuildTileMeshFromVertices
			{
				vertices = vertices,
				indices = indices,
				meshToGraph = meshToGraph,
				outputBuffers = nativeArray,
				recalculateNormals = recalculateNormals
			}.Schedule(default(JobHandle)), new JobBuildTileMeshFromVertices.BuildNavmeshOutput
			{
				tiles = nativeArray
			});
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0004C604 File Offset: 0x0004A804
		public unsafe void Execute()
		{
			NativeArray<Int3> nativeArray = new NativeArray<Int3>(this.vertices.Length, Allocator.Temp, NativeArrayOptions.ClearMemory);
			NativeArray<int> nativeArray2 = new NativeArray<int>(this.indices.Length / 3, Allocator.Temp, NativeArrayOptions.ClearMemory);
			JobBuildTileMeshFromVertices.JobTransformTileCoordinates jobTransformTileCoordinates = default(JobBuildTileMeshFromVertices.JobTransformTileCoordinates);
			jobTransformTileCoordinates.vertices = this.vertices;
			jobTransformTileCoordinates.outputVertices = nativeArray;
			jobTransformTileCoordinates.matrix = this.meshToGraph;
			jobTransformTileCoordinates.Execute();
			TileMesh.TileMeshUnsafe* unsafePtr = (TileMesh.TileMeshUnsafe*)this.outputBuffers.GetUnsafePtr<TileMesh.TileMeshUnsafe>();
			UnsafeAppendBuffer* ptr = &unsafePtr->verticesInTileSpace;
			UnsafeAppendBuffer* ptr2 = &unsafePtr->triangles;
			UnsafeAppendBuffer* ptr3 = &unsafePtr->tags;
			*ptr = new UnsafeAppendBuffer(0, 4, Allocator.Persistent);
			*ptr2 = new UnsafeAppendBuffer(0, 4, Allocator.Persistent);
			*ptr3 = new UnsafeAppendBuffer(0, 4, Allocator.Persistent);
			MeshUtility.JobRemoveDuplicateVertices jobRemoveDuplicateVertices = default(MeshUtility.JobRemoveDuplicateVertices);
			jobRemoveDuplicateVertices.vertices = nativeArray;
			jobRemoveDuplicateVertices.triangles = this.indices;
			jobRemoveDuplicateVertices.tags = nativeArray2;
			jobRemoveDuplicateVertices.outputVertices = ptr;
			jobRemoveDuplicateVertices.outputTriangles = ptr2;
			jobRemoveDuplicateVertices.outputTags = ptr3;
			jobRemoveDuplicateVertices.Execute();
			if (this.recalculateNormals)
			{
				UnsafeSpan<Int3> unsafeSpan = (*ptr).AsUnsafeSpan<Int3>();
				UnsafeSpan<int> unsafeSpan2 = (*ptr2).AsUnsafeSpan<int>();
				MeshUtility.MakeTrianglesClockwise(ref unsafeSpan, ref unsafeSpan2);
			}
			nativeArray.Dispose();
		}

		// Token: 0x040008F6 RID: 2294
		public NativeArray<Vector3> vertices;

		// Token: 0x040008F7 RID: 2295
		public NativeArray<int> indices;

		// Token: 0x040008F8 RID: 2296
		public Matrix4x4 meshToGraph;

		// Token: 0x040008F9 RID: 2297
		public NativeArray<TileMesh.TileMeshUnsafe> outputBuffers;

		// Token: 0x040008FA RID: 2298
		public bool recalculateNormals;

		// Token: 0x020001EB RID: 491
		[BurstCompile(FloatMode = FloatMode.Fast)]
		public struct JobTransformTileCoordinates : IJob
		{
			// Token: 0x06000C5B RID: 3163 RVA: 0x0004C74C File Offset: 0x0004A94C
			public void Execute()
			{
				for (int i = 0; i < this.vertices.Length; i++)
				{
					this.outputVertices[i] = (Int3)this.matrix.MultiplyPoint3x4(this.vertices[i]);
				}
			}

			// Token: 0x040008FB RID: 2299
			public NativeArray<Vector3> vertices;

			// Token: 0x040008FC RID: 2300
			public NativeArray<Int3> outputVertices;

			// Token: 0x040008FD RID: 2301
			public Matrix4x4 matrix;
		}

		// Token: 0x020001EC RID: 492
		public struct BuildNavmeshOutput : IProgress, IDisposable
		{
			// Token: 0x170001B4 RID: 436
			// (get) Token: 0x06000C5C RID: 3164 RVA: 0x000057A6 File Offset: 0x000039A6
			public float Progress
			{
				get
				{
					return 0f;
				}
			}

			// Token: 0x06000C5D RID: 3165 RVA: 0x0004C798 File Offset: 0x0004A998
			public void Dispose()
			{
				for (int i = 0; i < this.tiles.Length; i++)
				{
					this.tiles[i].Dispose();
				}
				this.tiles.Dispose();
			}

			// Token: 0x040008FE RID: 2302
			public NativeArray<TileMesh.TileMeshUnsafe> tiles;
		}
	}
}
