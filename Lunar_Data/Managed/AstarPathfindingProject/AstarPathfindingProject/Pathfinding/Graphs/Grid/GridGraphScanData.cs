using System;
using System.Collections.Generic;
using Pathfinding.Graphs.Grid.Jobs;
using Pathfinding.Jobs;
using Pathfinding.Util;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.Graphs.Grid
{
	// Token: 0x020001FE RID: 510
	public struct GridGraphScanData
	{
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x0004E6EE File Offset: 0x0004C8EE
		[Obsolete("Use nodes.bounds or heightHitsBounds depending on if you are using the heightHits array or not")]
		public IntBounds bounds
		{
			get
			{
				return this.nodes.bounds;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x0004E6FB File Offset: 0x0004C8FB
		[Obsolete("Use nodes.layeredDataLayout instead")]
		public bool layeredDataLayout
		{
			get
			{
				return this.nodes.layeredDataLayout;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x0004E708 File Offset: 0x0004C908
		[Obsolete("Use nodes.positions instead")]
		public NativeArray<Vector3> nodePositions
		{
			get
			{
				return this.nodes.positions;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x0004E715 File Offset: 0x0004C915
		[Obsolete("Use nodes.connections instead")]
		public NativeArray<ulong> nodeConnections
		{
			get
			{
				return this.nodes.connections;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x0004E722 File Offset: 0x0004C922
		[Obsolete("Use nodes.penalties instead")]
		public NativeArray<uint> nodePenalties
		{
			get
			{
				return this.nodes.penalties;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x0004E72F File Offset: 0x0004C92F
		[Obsolete("Use nodes.tags instead")]
		public NativeArray<int> nodeTags
		{
			get
			{
				return this.nodes.tags;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x0004E73C File Offset: 0x0004C93C
		[Obsolete("Use nodes.normals instead")]
		public NativeArray<float4> nodeNormals
		{
			get
			{
				return this.nodes.normals;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x0004E749 File Offset: 0x0004C949
		[Obsolete("Use nodes.walkable instead")]
		public NativeArray<bool> nodeWalkable
		{
			get
			{
				return this.nodes.walkable;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x0004E756 File Offset: 0x0004C956
		[Obsolete("Use nodes.walkableWithErosion instead")]
		public NativeArray<bool> nodeWalkableWithErosion
		{
			get
			{
				return this.nodes.walkableWithErosion;
			}
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x0004E763 File Offset: 0x0004C963
		public void SetDefaultPenalties(uint initialPenalty)
		{
			this.nodes.penalties.MemSet(initialPenalty).Schedule(this.dependencyTracker);
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0004E784 File Offset: 0x0004C984
		public void SetDefaultNodePositions(GraphTransform transform)
		{
			new JobNodeGridLayout
			{
				graphToWorld = transform.matrix,
				bounds = this.nodes.bounds,
				nodePositions = this.nodes.positions
			}.Schedule(this.dependencyTracker);
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x0004E7D8 File Offset: 0x0004C9D8
		public JobHandle HeightCheck(GraphCollision collision, int maxHits, IntBounds recalculationBounds, NativeArray<int> outLayerCount, float characterHeight, Allocator allocator)
		{
			int num = recalculationBounds.size.x * recalculationBounds.size.z;
			NativeArray<RaycastCommand> nativeArray = this.dependencyTracker.NewNativeArray<RaycastCommand>(num, allocator, NativeArrayOptions.ClearMemory);
			this.heightHits = this.dependencyTracker.NewNativeArray<RaycastHit>(num * maxHits, allocator, NativeArrayOptions.ClearMemory);
			this.heightHitsBounds = recalculationBounds;
			JobHandle jobHandle = new JobPrepareGridRaycast
			{
				graphToWorld = this.transform.matrix,
				bounds = recalculationBounds,
				physicsScene = Physics.defaultPhysicsScene,
				raycastOffset = this.up * collision.fromHeight,
				raycastDirection = -this.up * (collision.fromHeight + 0.01f),
				raycastMask = collision.heightMask,
				raycastCommands = nativeArray
			}.Schedule(this.dependencyTracker);
			if (maxHits > 1)
			{
				float num2 = characterHeight * 0.5f;
				JobHandle jobHandle2 = new JobRaycastAll(nativeArray, this.heightHits, Physics.defaultPhysicsScene, maxHits, allocator, this.dependencyTracker, num2).Schedule(jobHandle);
				return new JobMaxHitCount
				{
					hits = this.heightHits,
					maxHits = maxHits,
					layerStride = num,
					maxHitCount = outLayerCount
				}.Schedule(jobHandle2);
			}
			this.dependencyTracker.ScheduleBatch(nativeArray, this.heightHits, 2048);
			outLayerCount[0] = 1;
			return default(JobHandle);
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x0004E958 File Offset: 0x0004CB58
		public void CopyHits(IntBounds recalculationBounds)
		{
			this.nodes.normals.MemSet(float4.zero).Schedule(this.dependencyTracker);
			new JobCopyHits
			{
				hits = this.heightHits,
				points = this.nodes.positions,
				normals = this.nodes.normals,
				slice = new Slice3D(this.nodes.bounds, recalculationBounds)
			}.Schedule(this.dependencyTracker);
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x0004E9E4 File Offset: 0x0004CBE4
		public void CalculateWalkabilityFromHeightData(bool useRaycastNormal, bool unwalkableWhenNoGround, float maxSlope, float characterHeight)
		{
			new JobNodeWalkability
			{
				useRaycastNormal = useRaycastNormal,
				unwalkableWhenNoGround = unwalkableWhenNoGround,
				maxSlope = maxSlope,
				up = this.up,
				nodeNormals = this.nodes.normals,
				nodeWalkable = this.nodes.walkable,
				nodePositions = this.nodes.positions.Reinterpret<float3>(),
				characterHeight = characterHeight,
				layerStride = this.nodes.bounds.size.x * this.nodes.bounds.size.z
			}.Schedule(this.dependencyTracker);
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x0004EAA4 File Offset: 0x0004CCA4
		public IEnumerator<JobHandle> CollisionCheck(GraphCollision collision, IntBounds calculationBounds)
		{
			if (collision.type == ColliderType.Ray && !collision.use2D)
			{
				NativeArray<bool> nativeArray = this.dependencyTracker.NewNativeArray<bool>(this.nodes.numNodes, this.nodes.allocationMethod, NativeArrayOptions.UninitializedMemory);
				collision.JobCollisionRay(this.nodes.positions, nativeArray, this.up, this.nodes.allocationMethod, this.dependencyTracker);
				this.nodes.walkable.BitwiseAndWith(nativeArray).WithLength(this.nodes.numNodes).Schedule(this.dependencyTracker);
				return null;
			}
			return new JobCheckCollisions
			{
				nodePositions = this.nodes.positions,
				collisionResult = this.nodes.walkable,
				collision = collision
			}.ExecuteMainThreadJob(this.dependencyTracker);
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x0004EB80 File Offset: 0x0004CD80
		public void Connections(float maxStepHeight, bool maxStepUsesSlope, IntBounds calculationBounds, NumNeighbours neighbours, bool cutCorners, bool use2D, bool useErodedWalkability, float characterHeight)
		{
			JobCalculateGridConnections jobCalculateGridConnections = new JobCalculateGridConnections
			{
				maxStepHeight = maxStepHeight,
				maxStepUsesSlope = maxStepUsesSlope,
				up = this.up,
				bounds = calculationBounds.Offset(-this.nodes.bounds.min),
				arrayBounds = this.nodes.bounds.size,
				neighbours = neighbours,
				use2D = use2D,
				cutCorners = cutCorners,
				nodeWalkable = (useErodedWalkability ? this.nodes.walkableWithErosion : this.nodes.walkable).AsUnsafeSpanNoChecks<bool>(),
				nodePositions = this.nodes.positions.AsUnsafeSpanNoChecks<Vector3>(),
				nodeNormals = this.nodes.normals.AsUnsafeSpanNoChecks<float4>(),
				nodeConnections = this.nodes.connections.AsUnsafeSpanNoChecks<ulong>(),
				characterHeight = characterHeight,
				layeredDataLayout = this.nodes.layeredDataLayout
			};
			if (this.dependencyTracker != null)
			{
				jobCalculateGridConnections.ScheduleBatch(calculationBounds.size.z, 20, this.dependencyTracker, default(JobHandle));
			}
			else
			{
				jobCalculateGridConnections.RunBatch(calculationBounds.size.z);
			}
			if (this.nodes.layeredDataLayout)
			{
				JobFilterDiagonalConnections jobFilterDiagonalConnections = new JobFilterDiagonalConnections
				{
					slice = new Slice3D(this.nodes.bounds, calculationBounds),
					neighbours = neighbours,
					cutCorners = cutCorners,
					nodeConnections = this.nodes.connections.AsUnsafeSpanNoChecks<ulong>()
				};
				if (this.dependencyTracker != null)
				{
					jobFilterDiagonalConnections.ScheduleBatch(calculationBounds.size.z, 20, this.dependencyTracker, default(JobHandle));
					return;
				}
				jobFilterDiagonalConnections.RunBatch(calculationBounds.size.z);
			}
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x0004ED6C File Offset: 0x0004CF6C
		public void Erosion(NumNeighbours neighbours, int erodeIterations, IntBounds erosionWriteMask, bool erosionUsesTags, int erosionStartTag, int erosionTagsPrecedenceMask)
		{
			if (!this.nodes.layeredDataLayout)
			{
				new JobErosion<FlatGridAdjacencyMapper>
				{
					bounds = this.nodes.bounds,
					writeMask = erosionWriteMask,
					neighbours = neighbours,
					nodeConnections = this.nodes.connections,
					erosion = erodeIterations,
					nodeWalkable = this.nodes.walkable,
					outNodeWalkable = this.nodes.walkableWithErosion,
					nodeTags = this.nodes.tags,
					erosionUsesTags = erosionUsesTags,
					erosionStartTag = erosionStartTag,
					erosionTagsPrecedenceMask = erosionTagsPrecedenceMask
				}.Schedule(this.dependencyTracker);
				return;
			}
			new JobErosion<LayeredGridAdjacencyMapper>
			{
				bounds = this.nodes.bounds,
				writeMask = erosionWriteMask,
				neighbours = neighbours,
				nodeConnections = this.nodes.connections,
				erosion = erodeIterations,
				nodeWalkable = this.nodes.walkable,
				outNodeWalkable = this.nodes.walkableWithErosion,
				nodeTags = this.nodes.tags,
				erosionUsesTags = erosionUsesTags,
				erosionStartTag = erosionStartTag,
				erosionTagsPrecedenceMask = erosionTagsPrecedenceMask
			}.Schedule(this.dependencyTracker);
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x0004EED0 File Offset: 0x0004D0D0
		public unsafe void AssignNodeConnections(GridNodeBase[] nodes, int3 nodeArrayBounds, IntBounds writeBounds)
		{
			IntBounds bounds = this.nodes.bounds;
			int3 @int = writeBounds.min - bounds.min;
			UnsafeSpan<ulong> unsafeSpan = this.nodes.connections.AsUnsafeReadOnlySpan<ulong>();
			for (int i = 0; i < writeBounds.size.y; i++)
			{
				int num = (i + writeBounds.min.y) * nodeArrayBounds.x * nodeArrayBounds.z;
				for (int j = 0; j < writeBounds.size.z; j++)
				{
					int num2 = num + (j + writeBounds.min.z) * nodeArrayBounds.x + writeBounds.min.x;
					int num3 = (i + @int.y) * bounds.size.x * bounds.size.z + (j + @int.z) * bounds.size.x + @int.x;
					for (int k = 0; k < writeBounds.size.x; k++)
					{
						GridNodeBase gridNodeBase = nodes[num2 + k];
						int num4 = num3 + k;
						ulong num5 = *unsafeSpan[num4];
						if (gridNodeBase != null)
						{
							LevelGridNode levelGridNode = gridNodeBase as LevelGridNode;
							if (levelGridNode != null)
							{
								levelGridNode.SetAllConnectionInternal(num5);
							}
							else
							{
								(gridNodeBase as GridNode).SetAllConnectionInternal((int)num5);
							}
						}
					}
				}
			}
		}

		// Token: 0x0400096E RID: 2414
		public JobDependencyTracker dependencyTracker;

		// Token: 0x0400096F RID: 2415
		public Vector3 up;

		// Token: 0x04000970 RID: 2416
		public GraphTransform transform;

		// Token: 0x04000971 RID: 2417
		public GridGraphNodeData nodes;

		// Token: 0x04000972 RID: 2418
		public NativeArray<RaycastHit> heightHits;

		// Token: 0x04000973 RID: 2419
		public IntBounds heightHitsBounds;
	}
}
