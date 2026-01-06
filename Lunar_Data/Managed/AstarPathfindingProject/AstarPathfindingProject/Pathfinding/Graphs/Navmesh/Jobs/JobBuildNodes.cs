using System;
using System.Runtime.InteropServices;
using Pathfinding.Jobs;
using Pathfinding.Util;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Pathfinding.Graphs.Navmesh.Jobs
{
	// Token: 0x020001E8 RID: 488
	public struct JobBuildNodes
	{
		// Token: 0x06000C54 RID: 3156 RVA: 0x0004C34C File Offset: 0x0004A54C
		internal JobBuildNodes(RecastGraph graph, TileLayout tileLayout)
		{
			this.astar = graph.active;
			this.tileLayout = tileLayout;
			this.graphIndex = graph.graphIndex;
			this.initialPenalty = graph.initialPenalty;
			this.recalculateNormals = graph.RecalculateNormals;
			this.maxTileConnectionEdgeDistance = graph.MaxTileConnectionEdgeDistance;
			this.graphToWorldSpace = tileLayout.transform.matrix;
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0004C3B0 File Offset: 0x0004A5B0
		public Promise<JobBuildNodes.BuildNodeTilesOutput> Schedule(DisposeArena arena, Promise<TileBuilder.TileBuilderOutput> dependency)
		{
			TileBuilder.TileBuilderOutput value = dependency.GetValue();
			IntRect tileRect = value.tileMeshes.tileRect;
			NavmeshTile[] array = new NavmeshTile[tileRect.Area];
			GCHandle gchandle = GCHandle.Alloc(array);
			NativeArray<JobCalculateTriangleConnections.TileNodeConnectionsUnsafe> nativeArray = new NativeArray<JobCalculateTriangleConnections.TileNodeConnectionsUnsafe>(tileRect.Area, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			JobHandle jobHandle = new JobCalculateTriangleConnections
			{
				tileMeshes = value.tileMeshes.tileMeshes,
				nodeConnections = nativeArray
			}.Schedule(dependency.handle);
			Vector2 vector = new Vector2(this.tileLayout.TileWorldSizeX, this.tileLayout.TileWorldSizeZ);
			JobHandle jobHandle2 = new JobCreateTiles
			{
				tileMeshes = value.tileMeshes.tileMeshes,
				tiles = gchandle,
				tileRect = tileRect,
				graphTileCount = this.tileLayout.tileCount,
				graphIndex = this.graphIndex,
				initialPenalty = this.initialPenalty,
				recalculateNormals = this.recalculateNormals,
				graphToWorldSpace = this.graphToWorldSpace,
				tileWorldSize = vector
			}.Schedule(dependency.handle);
			JobHandle jobHandle3 = new JobWriteNodeConnections
			{
				nodeConnections = nativeArray,
				tiles = gchandle
			}.Schedule(JobHandle.CombineDependencies(jobHandle, jobHandle2));
			JobHandle jobHandle4 = JobConnectTiles.ScheduleBatch(gchandle, jobHandle3, tileRect, vector, this.maxTileConnectionEdgeDistance);
			arena.Add(gchandle);
			arena.Add<JobCalculateTriangleConnections.TileNodeConnectionsUnsafe>(nativeArray);
			return new Promise<JobBuildNodes.BuildNodeTilesOutput>(jobHandle4, new JobBuildNodes.BuildNodeTilesOutput
			{
				dependency = value,
				tiles = array
			});
		}

		// Token: 0x040008ED RID: 2285
		private AstarPath astar;

		// Token: 0x040008EE RID: 2286
		private uint graphIndex;

		// Token: 0x040008EF RID: 2287
		public uint initialPenalty;

		// Token: 0x040008F0 RID: 2288
		public bool recalculateNormals;

		// Token: 0x040008F1 RID: 2289
		public float maxTileConnectionEdgeDistance;

		// Token: 0x040008F2 RID: 2290
		private Matrix4x4 graphToWorldSpace;

		// Token: 0x040008F3 RID: 2291
		private TileLayout tileLayout;

		// Token: 0x020001E9 RID: 489
		public class BuildNodeTilesOutput : IProgress, IDisposable
		{
			// Token: 0x170001B3 RID: 435
			// (get) Token: 0x06000C56 RID: 3158 RVA: 0x0004C52F File Offset: 0x0004A72F
			public float Progress
			{
				get
				{
					return this.dependency.Progress;
				}
			}

			// Token: 0x06000C57 RID: 3159 RVA: 0x000033F6 File Offset: 0x000015F6
			public void Dispose()
			{
			}

			// Token: 0x040008F4 RID: 2292
			public TileBuilder.TileBuilderOutput dependency;

			// Token: 0x040008F5 RID: 2293
			public NavmeshTile[] tiles;
		}
	}
}
