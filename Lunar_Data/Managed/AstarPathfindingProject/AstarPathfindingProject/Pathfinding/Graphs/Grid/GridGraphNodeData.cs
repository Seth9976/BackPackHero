using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Pathfinding.Graphs.Grid.Jobs;
using Pathfinding.Jobs;
using Pathfinding.Util;
using Unity.Collections;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.Graphs.Grid
{
	// Token: 0x020001FC RID: 508
	public struct GridGraphNodeData
	{
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x0004DD30 File Offset: 0x0004BF30
		public int layers
		{
			get
			{
				return this.bounds.size.y;
			}
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x0004DD44 File Offset: 0x0004BF44
		public void AllocateBuffers(JobDependencyTracker dependencyTracker)
		{
			if (dependencyTracker != null)
			{
				this.positions = dependencyTracker.NewNativeArray<Vector3>(this.numNodes, this.allocationMethod, NativeArrayOptions.UninitializedMemory);
				this.normals = dependencyTracker.NewNativeArray<float4>(this.numNodes, this.allocationMethod, NativeArrayOptions.UninitializedMemory);
				this.connections = dependencyTracker.NewNativeArray<ulong>(this.numNodes, this.allocationMethod, NativeArrayOptions.UninitializedMemory);
				this.penalties = dependencyTracker.NewNativeArray<uint>(this.numNodes, this.allocationMethod, NativeArrayOptions.UninitializedMemory);
				this.walkable = dependencyTracker.NewNativeArray<bool>(this.numNodes, this.allocationMethod, NativeArrayOptions.UninitializedMemory);
				this.walkableWithErosion = dependencyTracker.NewNativeArray<bool>(this.numNodes, this.allocationMethod, NativeArrayOptions.UninitializedMemory);
				this.tags = dependencyTracker.NewNativeArray<int>(this.numNodes, this.allocationMethod, NativeArrayOptions.ClearMemory);
				return;
			}
			this.positions = new NativeArray<Vector3>(this.numNodes, this.allocationMethod, NativeArrayOptions.UninitializedMemory);
			this.normals = new NativeArray<float4>(this.numNodes, this.allocationMethod, NativeArrayOptions.UninitializedMemory);
			this.connections = new NativeArray<ulong>(this.numNodes, this.allocationMethod, NativeArrayOptions.UninitializedMemory);
			this.penalties = new NativeArray<uint>(this.numNodes, this.allocationMethod, NativeArrayOptions.UninitializedMemory);
			this.walkable = new NativeArray<bool>(this.numNodes, this.allocationMethod, NativeArrayOptions.UninitializedMemory);
			this.walkableWithErosion = new NativeArray<bool>(this.numNodes, this.allocationMethod, NativeArrayOptions.UninitializedMemory);
			this.tags = new NativeArray<int>(this.numNodes, this.allocationMethod, NativeArrayOptions.ClearMemory);
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x0004DEB0 File Offset: 0x0004C0B0
		public void TrackBuffers(JobDependencyTracker dependencyTracker)
		{
			if (this.positions.IsCreated)
			{
				dependencyTracker.Track<Vector3>(this.positions, true);
			}
			if (this.normals.IsCreated)
			{
				dependencyTracker.Track<float4>(this.normals, true);
			}
			if (this.connections.IsCreated)
			{
				dependencyTracker.Track<ulong>(this.connections, true);
			}
			if (this.penalties.IsCreated)
			{
				dependencyTracker.Track<uint>(this.penalties, true);
			}
			if (this.walkable.IsCreated)
			{
				dependencyTracker.Track<bool>(this.walkable, true);
			}
			if (this.walkableWithErosion.IsCreated)
			{
				dependencyTracker.Track<bool>(this.walkableWithErosion, true);
			}
			if (this.tags.IsCreated)
			{
				dependencyTracker.Track<int>(this.tags, true);
			}
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x0004DF74 File Offset: 0x0004C174
		public void PersistBuffers(JobDependencyTracker dependencyTracker)
		{
			dependencyTracker.Persist<Vector3>(this.positions);
			dependencyTracker.Persist<float4>(this.normals);
			dependencyTracker.Persist<ulong>(this.connections);
			dependencyTracker.Persist<uint>(this.penalties);
			dependencyTracker.Persist<bool>(this.walkable);
			dependencyTracker.Persist<bool>(this.walkableWithErosion);
			dependencyTracker.Persist<int>(this.tags);
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x0004DFD8 File Offset: 0x0004C1D8
		public void Dispose()
		{
			this.bounds = default(IntBounds);
			this.numNodes = 0;
			if (this.positions.IsCreated)
			{
				this.positions.Dispose();
			}
			if (this.normals.IsCreated)
			{
				this.normals.Dispose();
			}
			if (this.connections.IsCreated)
			{
				this.connections.Dispose();
			}
			if (this.penalties.IsCreated)
			{
				this.penalties.Dispose();
			}
			if (this.walkable.IsCreated)
			{
				this.walkable.Dispose();
			}
			if (this.walkableWithErosion.IsCreated)
			{
				this.walkableWithErosion.Dispose();
			}
			if (this.tags.IsCreated)
			{
				this.tags.Dispose();
			}
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x0004E0A0 File Offset: 0x0004C2A0
		public unsafe JobHandle Rotate2D(int dx, int dz, JobHandle dependency)
		{
			int3 size = this.bounds.size;
			IntPtr intPtr;
			checked
			{
				intPtr = stackalloc byte[unchecked((UIntPtr)7) * (UIntPtr)sizeof(JobHandle)];
				*intPtr = this.positions.Rotate3D(size, dx, dz).Schedule(dependency);
			}
			*(intPtr + (IntPtr)sizeof(JobHandle)) = this.normals.Rotate3D(size, dx, dz).Schedule(dependency);
			*(intPtr + (IntPtr)2 * (IntPtr)sizeof(JobHandle)) = this.connections.Rotate3D(size, dx, dz).Schedule(dependency);
			*(intPtr + (IntPtr)3 * (IntPtr)sizeof(JobHandle)) = this.penalties.Rotate3D(size, dx, dz).Schedule(dependency);
			*(intPtr + (IntPtr)4 * (IntPtr)sizeof(JobHandle)) = this.walkable.Rotate3D(size, dx, dz).Schedule(dependency);
			*(intPtr + (IntPtr)5 * (IntPtr)sizeof(JobHandle)) = this.walkableWithErosion.Rotate3D(size, dx, dz).Schedule(dependency);
			*(intPtr + (IntPtr)6 * (IntPtr)sizeof(JobHandle)) = this.tags.Rotate3D(size, dx, dz).Schedule(dependency);
			return JobHandleUnsafeUtility.CombineDependencies(intPtr, 7);
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0004E1BC File Offset: 0x0004C3BC
		public void ResizeLayerCount(int layerCount, JobDependencyTracker dependencyTracker)
		{
			if (layerCount > this.layers)
			{
				GridGraphNodeData gridGraphNodeData = this;
				this.bounds.max.y = layerCount;
				this.numNodes = this.bounds.volume;
				this.AllocateBuffers(dependencyTracker);
				this.normals.MemSet(float4.zero).Schedule(dependencyTracker);
				this.walkable.MemSet(false).Schedule(dependencyTracker);
				this.walkableWithErosion.MemSet(false).Schedule(dependencyTracker);
				new JobCopyBuffers
				{
					input = gridGraphNodeData,
					output = this,
					copyPenaltyAndTags = true,
					bounds = gridGraphNodeData.bounds
				}.Schedule(dependencyTracker);
			}
			if (layerCount < this.layers)
			{
				throw new ArgumentException("Cannot reduce the number of layers");
			}
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x0004E290 File Offset: 0x0004C490
		public void ReadFromNodesForConnectionCalculations(GridNodeBase[] nodes, Slice3D slice, JobHandle nodesDependsOn, NativeArray<float4> graphNodeNormals, JobDependencyTracker dependencyTracker)
		{
			this.bounds = slice.slice;
			this.numNodes = slice.slice.volume;
			this.positions = new NativeArray<Vector3>(this.numNodes, this.allocationMethod, NativeArrayOptions.UninitializedMemory);
			this.normals = new NativeArray<float4>(this.numNodes, this.allocationMethod, NativeArrayOptions.UninitializedMemory);
			this.connections = new NativeArray<ulong>(this.numNodes, this.allocationMethod, NativeArrayOptions.UninitializedMemory);
			this.walkableWithErosion = new NativeArray<bool>(this.numNodes, this.allocationMethod, NativeArrayOptions.UninitializedMemory);
			GridGraphNodeData.LightReader lightReader = new GridGraphNodeData.LightReader
			{
				nodes = nodes,
				nodePositions = this.positions.AsUnsafeSpan<Vector3>(),
				nodeWalkable = this.walkableWithErosion.AsUnsafeSpan<bool>()
			};
			GridIterationUtilities.ForEachCellIn3DSlice<GridGraphNodeData.LightReader>(slice, ref lightReader);
			this.ReadNodeNormals(slice, graphNodeNormals, dependencyTracker);
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x0004E368 File Offset: 0x0004C568
		private void ReadNodeNormals(Slice3D slice, NativeArray<float4> graphNodeNormals, JobDependencyTracker dependencyTracker)
		{
			if (dependencyTracker != null)
			{
				this.normals.MemSet(float4.zero).Schedule(dependencyTracker);
				new JobCopyRectangle<float4>
				{
					input = graphNodeNormals,
					output = this.normals,
					inputSlice = slice,
					outputSlice = new Slice3D(this.bounds, slice.slice)
				}.Schedule(dependencyTracker);
				return;
			}
			this.normals.AsUnsafeSpan<float4>().FillZeros<float4>();
			JobCopyRectangle<float4>.Copy(graphNodeNormals, this.normals, slice, new Slice3D(this.bounds, slice.slice));
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x0004E404 File Offset: 0x0004C604
		public static GridGraphNodeData ReadFromNodes(GridNodeBase[] nodes, Slice3D slice, JobHandle nodesDependsOn, NativeArray<float4> graphNodeNormals, Allocator allocator, bool layeredDataLayout, JobDependencyTracker dependencyTracker)
		{
			GridGraphNodeData gridGraphNodeData = new GridGraphNodeData
			{
				allocationMethod = allocator,
				numNodes = slice.slice.volume,
				bounds = slice.slice,
				layeredDataLayout = layeredDataLayout
			};
			gridGraphNodeData.AllocateBuffers(dependencyTracker);
			GCHandle gchandle = GCHandle.Alloc(nodes);
			JobHandle jobHandle = new JobReadNodeData
			{
				nodesHandle = gchandle,
				nodePositions = gridGraphNodeData.positions,
				nodePenalties = gridGraphNodeData.penalties,
				nodeTags = gridGraphNodeData.tags,
				nodeConnections = gridGraphNodeData.connections,
				nodeWalkableWithErosion = gridGraphNodeData.walkableWithErosion,
				nodeWalkable = gridGraphNodeData.walkable,
				slice = slice
			}.ScheduleBatch(gridGraphNodeData.numNodes, math.max(2000, gridGraphNodeData.numNodes / 16), dependencyTracker, nodesDependsOn);
			dependencyTracker.DeferFree(gchandle, jobHandle);
			if (graphNodeNormals.IsCreated)
			{
				gridGraphNodeData.ReadNodeNormals(slice, graphNodeNormals, dependencyTracker);
			}
			return gridGraphNodeData;
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x0004E508 File Offset: 0x0004C708
		public GridGraphNodeData ReadFromNodesAndCopy(GridNodeBase[] nodes, Slice3D slice, JobHandle nodesDependsOn, NativeArray<float4> graphNodeNormals, bool copyPenaltyAndTags, JobDependencyTracker dependencyTracker)
		{
			GridGraphNodeData gridGraphNodeData = GridGraphNodeData.ReadFromNodes(nodes, slice, nodesDependsOn, graphNodeNormals, this.allocationMethod, this.layeredDataLayout, dependencyTracker);
			gridGraphNodeData.CopyFrom(this, copyPenaltyAndTags, dependencyTracker);
			return gridGraphNodeData;
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x0004E540 File Offset: 0x0004C740
		public void CopyFrom(GridGraphNodeData other, bool copyPenaltyAndTags, JobDependencyTracker dependencyTracker)
		{
			this.CopyFrom(other, IntBounds.Intersection(this.bounds, other.bounds), copyPenaltyAndTags, dependencyTracker);
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x0004E55C File Offset: 0x0004C75C
		public void CopyFrom(GridGraphNodeData other, IntBounds bounds, bool copyPenaltyAndTags, JobDependencyTracker dependencyTracker)
		{
			JobCopyBuffers jobCopyBuffers = new JobCopyBuffers
			{
				input = other,
				output = this,
				copyPenaltyAndTags = copyPenaltyAndTags,
				bounds = bounds
			};
			if (dependencyTracker != null)
			{
				jobCopyBuffers.Schedule(dependencyTracker);
				return;
			}
			jobCopyBuffers.Run<JobCopyBuffers>();
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x0004E5AC File Offset: 0x0004C7AC
		public JobHandle AssignToNodes(GridNodeBase[] nodes, int3 nodeArrayBounds, IntBounds writeMask, uint graphIndex, JobHandle nodesDependsOn, JobDependencyTracker dependencyTracker)
		{
			GCHandle gchandle = GCHandle.Alloc(nodes);
			JobHandle jobHandle = new JobWriteNodeData
			{
				nodesHandle = gchandle,
				graphIndex = graphIndex,
				nodePositions = this.positions,
				nodePenalties = this.penalties,
				nodeTags = this.tags,
				nodeConnections = this.connections,
				nodeWalkableWithErosion = this.walkableWithErosion,
				nodeWalkable = this.walkable,
				nodeArrayBounds = nodeArrayBounds,
				dataBounds = this.bounds,
				writeMask = writeMask
			}.ScheduleBatch(writeMask.volume, math.max(1000, writeMask.volume / 16), dependencyTracker, nodesDependsOn);
			dependencyTracker.DeferFree(gchandle, jobHandle);
			return jobHandle;
		}

		// Token: 0x04000960 RID: 2400
		public Allocator allocationMethod;

		// Token: 0x04000961 RID: 2401
		public int numNodes;

		// Token: 0x04000962 RID: 2402
		public IntBounds bounds;

		// Token: 0x04000963 RID: 2403
		public NativeArray<Vector3> positions;

		// Token: 0x04000964 RID: 2404
		public NativeArray<ulong> connections;

		// Token: 0x04000965 RID: 2405
		public NativeArray<uint> penalties;

		// Token: 0x04000966 RID: 2406
		public NativeArray<int> tags;

		// Token: 0x04000967 RID: 2407
		public NativeArray<float4> normals;

		// Token: 0x04000968 RID: 2408
		public NativeArray<bool> walkable;

		// Token: 0x04000969 RID: 2409
		public NativeArray<bool> walkableWithErosion;

		// Token: 0x0400096A RID: 2410
		public bool layeredDataLayout;

		// Token: 0x020001FD RID: 509
		private struct LightReader : GridIterationUtilities.ISliceAction
		{
			// Token: 0x06000C8B RID: 3211 RVA: 0x0004E674 File Offset: 0x0004C874
			[MethodImpl(256)]
			public unsafe void Execute(uint outerIdx, uint innerIdx)
			{
				if ((ulong)outerIdx < (ulong)((long)this.nodes.Length))
				{
					GridNodeBase gridNodeBase = this.nodes[(int)outerIdx];
					if (gridNodeBase != null)
					{
						*this.nodePositions[innerIdx] = (Vector3)gridNodeBase.position;
						*this.nodeWalkable[innerIdx] = gridNodeBase.Walkable;
						return;
					}
				}
				*this.nodePositions[innerIdx] = Vector3.zero;
				*this.nodeWalkable[innerIdx] = false;
			}

			// Token: 0x0400096B RID: 2411
			public GridNodeBase[] nodes;

			// Token: 0x0400096C RID: 2412
			public UnsafeSpan<Vector3> nodePositions;

			// Token: 0x0400096D RID: 2413
			public UnsafeSpan<bool> nodeWalkable;
		}
	}
}
