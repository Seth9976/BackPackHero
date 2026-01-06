using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Pathfinding.Drawing;
using Pathfinding.Jobs;
using Pathfinding.RVO;
using Pathfinding.Util;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Profiling;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200006F RID: 111
	[BurstCompile]
	public class NavmeshEdges
	{
		// Token: 0x060003BE RID: 958 RVA: 0x00012D40 File Offset: 0x00010F40
		public void Dispose()
		{
			this.rwLock.WriteSync().Unlock();
			this.obstacleData.Dispose();
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00012D6B File Offset: 0x00010F6B
		private void Init()
		{
			this.obstacleData.Init(Allocator.Persistent);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00012D7C File Offset: 0x00010F7C
		public unsafe JobHandle RecalculateObstacles(NativeList<int> dirtyHierarchicalNodes, NativeReference<int> numHierarchicalNodes, JobHandle dependency)
		{
			this.Init();
			RWLock.WriteLockAsync writeLockAsync = this.rwLock.Write();
			JobHandle jobHandle = new NavmeshEdges.JobResizeObstacles
			{
				numHierarchicalNodes = numHierarchicalNodes,
				obstacles = this.obstacleData.obstacles
			}.Schedule(JobHandle.CombineDependencies(dependency, writeLockAsync.dependency));
			jobHandle = new NavmeshEdges.JobCalculateObstacles
			{
				hGraphGC = this.hierarchicalGraph.gcHandle,
				obstacleVertices = this.obstacleData.obstacleVertices,
				obstacleVertexGroups = this.obstacleData.obstacleVertexGroups,
				obstacles = this.obstacleData.obstacles.AsDeferredJobArray(),
				bounds = this.hierarchicalGraph.bounds.AsDeferredJobArray(),
				dirtyHierarchicalNodes = dirtyHierarchicalNodes,
				allocationLock = (SpinLock*)UnsafeUtility.AddressOf<SpinLock>(ref this.allocationLock)
			}.ScheduleBatch(32, 1, jobHandle);
			writeLockAsync.UnlockAfter(jobHandle);
			this.gizmoVersion++;
			return jobHandle;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00012E78 File Offset: 0x00011078
		public unsafe void OnDrawGizmos(DrawingData gizmos, RedrawScope redrawScope)
		{
			if (!this.obstacleData.obstacleVertices.IsCreated)
			{
				return;
			}
			NodeHasher nodeHasher = new NodeHasher(AstarPath.active);
			nodeHasher.Add<int>(12314127);
			nodeHasher.Add<int>(this.gizmoVersion);
			if (!gizmos.Draw(nodeHasher, redrawScope))
			{
				RWLock.LockSync lockSync = this.rwLock.ReadSync();
				try
				{
					using (CommandBuilder builder = gizmos.GetBuilder(nodeHasher, redrawScope, false))
					{
						for (int i = 1; i < this.obstacleData.obstacles.Length; i++)
						{
							UnmanagedObstacle unmanagedObstacle = this.obstacleData.obstacles[i];
							UnsafeSpan<float3> span = this.obstacleData.obstacleVertices.GetSpan(unmanagedObstacle.verticesAllocation);
							UnsafeSpan<ObstacleVertexGroup> span2 = this.obstacleData.obstacleVertexGroups.GetSpan(unmanagedObstacle.groupsAllocation);
							int num = 0;
							for (int j = 0; j < span2.Length; j++)
							{
								ObstacleVertexGroup obstacleVertexGroup = *span2[j];
								builder.PushLineWidth(2f, true);
								for (int k = 0; k < obstacleVertexGroup.vertexCount - 1; k++)
								{
									builder.ArrowRelativeSizeHead(*span[num + k], *span[num + k + 1], new float3(0f, 1f, 0f), 0.05f, Color.black);
								}
								if (obstacleVertexGroup.type == ObstacleType.Loop)
								{
									builder.Arrow(*span[num + obstacleVertexGroup.vertexCount - 1], *span[num], new float3(0f, 1f, 0f), 0.05f, Color.black);
								}
								builder.PopLineWidth();
								num += obstacleVertexGroup.vertexCount;
								builder.WireBox(0.5f * (obstacleVertexGroup.boundsMn + obstacleVertexGroup.boundsMx), obstacleVertexGroup.boundsMx - obstacleVertexGroup.boundsMn, Color.white);
							}
						}
					}
				}
				finally
				{
					lockSync.Unlock();
				}
			}
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x000130E0 File Offset: 0x000112E0
		public NavmeshEdges.NavmeshBorderData GetNavmeshEdgeData(out RWLock.CombinedReadLockAsync readLock)
		{
			this.Init();
			RWLock.ReadLockAsync readLockAsync = this.rwLock.Read();
			RWLock.ReadLockAsync readLockAsync2;
			HierarchicalGraph.HierarhicalNodeData hierarhicalNodeData = this.hierarchicalGraph.GetHierarhicalNodeData(out readLockAsync2);
			readLock = new RWLock.CombinedReadLockAsync(readLockAsync, readLockAsync2);
			return new NavmeshEdges.NavmeshBorderData
			{
				hierarhicalNodeData = hierarhicalNodeData,
				obstacleData = this.obstacleData
			};
		}

		// Token: 0x04000276 RID: 630
		public SimulatorBurst.ObstacleData obstacleData;

		// Token: 0x04000277 RID: 631
		private SpinLock allocationLock;

		// Token: 0x04000278 RID: 632
		private const int JobRecalculateObstaclesBatchCount = 32;

		// Token: 0x04000279 RID: 633
		private RWLock rwLock = new RWLock();

		// Token: 0x0400027A RID: 634
		public HierarchicalGraph hierarchicalGraph;

		// Token: 0x0400027B RID: 635
		private int gizmoVersion;

		// Token: 0x02000070 RID: 112
		[BurstCompile]
		private struct JobResizeObstacles : IJob
		{
			// Token: 0x060003C4 RID: 964 RVA: 0x0001314C File Offset: 0x0001134C
			public void Execute()
			{
				int length = this.obstacles.Length;
				int value = this.numHierarchicalNodes.Value;
				this.obstacles.Resize(value, NativeArrayOptions.UninitializedMemory);
				for (int i = length; i < this.obstacles.Length; i++)
				{
					this.obstacles[i] = new UnmanagedObstacle
					{
						verticesAllocation = -1,
						groupsAllocation = -1
					};
				}
				if (this.obstacles.Length > 0)
				{
					this.obstacles[0] = new UnmanagedObstacle
					{
						verticesAllocation = -2,
						groupsAllocation = -2
					};
				}
			}

			// Token: 0x0400027C RID: 636
			public NativeList<UnmanagedObstacle> obstacles;

			// Token: 0x0400027D RID: 637
			public NativeReference<int> numHierarchicalNodes;
		}

		// Token: 0x02000071 RID: 113
		private struct JobCalculateObstacles : IJobParallelForBatch
		{
			// Token: 0x060003C5 RID: 965 RVA: 0x000131EC File Offset: 0x000113EC
			public void Execute(int startIndex, int count)
			{
				HierarchicalGraph hierarchicalGraph = this.hGraphGC.Target as HierarchicalGraph;
				int num = (this.dirtyHierarchicalNodes.Length + 32 - 1) / 32;
				startIndex *= num;
				count *= num;
				int num2 = math.min(startIndex + count, this.dirtyHierarchicalNodes.Length);
				NativeList<RVOObstacleCache.ObstacleSegment> nativeList = new NativeList<RVOObstacleCache.ObstacleSegment>(Allocator.Temp);
				for (int i = startIndex; i < num2; i++)
				{
					nativeList.Clear();
					int num3 = this.dirtyHierarchicalNodes[i];
					this.CalculateBoundingBox(hierarchicalGraph, num3);
					this.CalculateObstacles(hierarchicalGraph, num3, this.obstacleVertexGroups, this.obstacleVertices, this.obstacles, nativeList);
				}
			}

			// Token: 0x060003C6 RID: 966 RVA: 0x00013294 File Offset: 0x00011494
			private void CalculateBoundingBox(HierarchicalGraph hGraph, int hierarchicalNode)
			{
				List<GraphNode> list = hGraph.children[hierarchicalNode];
				Bounds bounds = default(Bounds);
				if (list.Count != 0)
				{
					if (list[0] is TriangleMeshNode)
					{
						Int3 @int = new Int3(int.MaxValue, int.MaxValue, int.MaxValue);
						Int3 int2 = new Int3(int.MinValue, int.MinValue, int.MinValue);
						for (int i = 0; i < list.Count; i++)
						{
							Int3 int3;
							Int3 int4;
							Int3 int5;
							(list[i] as TriangleMeshNode).GetVertices(out int3, out int4, out int5);
							@int = Int3.Min(Int3.Min(Int3.Min(@int, int3), int4), int5);
							int2 = Int3.Max(Int3.Max(Int3.Max(int2, int3), int4), int5);
						}
						bounds.SetMinMax((Vector3)@int, (Vector3)int2);
					}
					else
					{
						Int3 int6 = new Int3(int.MaxValue, int.MaxValue, int.MaxValue);
						Int3 int7 = new Int3(int.MinValue, int.MinValue, int.MinValue);
						for (int j = 0; j < list.Count; j++)
						{
							GraphNode graphNode = list[j];
							int6 = Int3.Min(int6, graphNode.position);
							int7 = Int3.Max(int7, graphNode.position);
						}
						if (list[0] is GridNodeBase)
						{
							float num;
							if (list[0] is LevelGridNode)
							{
								num = LevelGridNode.GetGridGraph(list[0].GraphIndex).nodeSize;
							}
							else
							{
								num = GridNode.GetGridGraph(list[0].GraphIndex).nodeSize;
							}
							Vector3 vector = num * 0.70710677f * Vector3.one;
							bounds.SetMinMax((Vector3)int6 - vector, (Vector3)int7 + vector);
						}
						else
						{
							bounds.SetMinMax((Vector3)int6, (Vector3)int7);
						}
					}
				}
				this.bounds[hierarchicalNode] = bounds;
			}

			// Token: 0x060003C7 RID: 967 RVA: 0x00013484 File Offset: 0x00011684
			private unsafe void CalculateObstacles(HierarchicalGraph hGraph, int hierarchicalNode, SlabAllocator<ObstacleVertexGroup> obstacleVertexGroups, SlabAllocator<float3> obstacleVertices, NativeArray<UnmanagedObstacle> obstacles, NativeList<RVOObstacleCache.ObstacleSegment> edgesScratch)
			{
				RVOObstacleCache.CollectContours(hGraph.children[hierarchicalNode], edgesScratch);
				UnmanagedObstacle unmanagedObstacle = obstacles[hierarchicalNode];
				if (unmanagedObstacle.groupsAllocation != -1)
				{
					this.allocationLock->Lock();
					obstacleVertices.Free(unmanagedObstacle.verticesAllocation);
					obstacleVertexGroups.Free(unmanagedObstacle.groupsAllocation);
					this.allocationLock->Unlock();
				}
				List<GraphNode> list = hGraph.children[hierarchicalNode];
				bool flag = true;
				NativeMovementPlane nativeMovementPlane;
				if (list.Count > 0)
				{
					if (list[0] is GridNodeBase)
					{
						nativeMovementPlane = new NativeMovementPlane((list[0].Graph as GridGraph).transform.rotation);
					}
					else if (list[0] is TriangleMeshNode)
					{
						NavmeshBase navmeshBase = list[0].Graph as NavmeshBase;
						nativeMovementPlane = new NativeMovementPlane(navmeshBase.transform.rotation);
						flag = navmeshBase.RecalculateNormals;
					}
					else
					{
						nativeMovementPlane = new NativeMovementPlane(quaternion.identity);
						flag = false;
					}
				}
				else
				{
					nativeMovementPlane = default(NativeMovementPlane);
				}
				UnsafeSpan<RVOObstacleCache.ObstacleSegment> unsafeSpan = edgesScratch.AsUnsafeSpan<RVOObstacleCache.ObstacleSegment>();
				RVOObstacleCache.TraceContours(ref unsafeSpan, ref nativeMovementPlane, hierarchicalNode, (UnmanagedObstacle*)obstacles.GetUnsafePtr<UnmanagedObstacle>(), ref obstacleVertices, ref obstacleVertexGroups, UnsafeUtility.AsRef<SpinLock>((void*)this.allocationLock), flag);
			}

			// Token: 0x0400027E RID: 638
			public GCHandle hGraphGC;

			// Token: 0x0400027F RID: 639
			public SlabAllocator<float3> obstacleVertices;

			// Token: 0x04000280 RID: 640
			public SlabAllocator<ObstacleVertexGroup> obstacleVertexGroups;

			// Token: 0x04000281 RID: 641
			[NativeDisableParallelForRestriction]
			public NativeArray<UnmanagedObstacle> obstacles;

			// Token: 0x04000282 RID: 642
			[NativeDisableParallelForRestriction]
			public NativeArray<Bounds> bounds;

			// Token: 0x04000283 RID: 643
			[ReadOnly]
			public NativeList<int> dirtyHierarchicalNodes;

			// Token: 0x04000284 RID: 644
			[NativeDisableUnsafePtrRestriction]
			public unsafe SpinLock* allocationLock;

			// Token: 0x04000285 RID: 645
			private static readonly ProfilerMarker MarkerBBox = new ProfilerMarker("HierarchicalBBox");

			// Token: 0x04000286 RID: 646
			private static readonly ProfilerMarker MarkerObstacles = new ProfilerMarker("CalculateObstacles");

			// Token: 0x04000287 RID: 647
			private static readonly ProfilerMarker MarkerCollect = new ProfilerMarker("Collect");

			// Token: 0x04000288 RID: 648
			private static readonly ProfilerMarker MarkerTrace = new ProfilerMarker("Trace");
		}

		// Token: 0x02000072 RID: 114
		public struct NavmeshBorderData
		{
			// Token: 0x060003C9 RID: 969 RVA: 0x000135F4 File Offset: 0x000117F4
			public static NavmeshEdges.NavmeshBorderData CreateEmpty(Allocator allocator)
			{
				return new NavmeshEdges.NavmeshBorderData
				{
					hierarhicalNodeData = new HierarchicalGraph.HierarhicalNodeData
					{
						connectionAllocator = default(SlabAllocator<int>),
						connectionAllocations = new NativeList<int>(0, allocator),
						bounds = new NativeList<Bounds>(0, allocator)
					},
					obstacleData = new SimulatorBurst.ObstacleData
					{
						obstacleVertexGroups = default(SlabAllocator<ObstacleVertexGroup>),
						obstacleVertices = default(SlabAllocator<float3>),
						obstacles = new NativeList<UnmanagedObstacle>(0, allocator)
					}
				};
			}

			// Token: 0x060003CA RID: 970 RVA: 0x0001368C File Offset: 0x0001188C
			public void DisposeEmpty(JobHandle dependsOn)
			{
				if (this.hierarhicalNodeData.connectionAllocator.IsCreated)
				{
					throw new InvalidOperationException("NavmeshEdgeData was not empty");
				}
				this.hierarhicalNodeData.connectionAllocations.Dispose(dependsOn);
				this.hierarhicalNodeData.bounds.Dispose(dependsOn);
				this.obstacleData.obstacles.Dispose(dependsOn);
			}

			// Token: 0x060003CB RID: 971 RVA: 0x000136EC File Offset: 0x000118EC
			private unsafe static void GetHierarchicalNodesInRangeRec(int hierarchicalNode, Bounds bounds, SlabAllocator<int> connectionAllocator, [NoAlias] NativeList<int> connectionAllocations, NativeList<Bounds> nodeBounds, [NoAlias] NativeList<int> indices)
			{
				indices.Add(in hierarchicalNode);
				UnsafeSpan<int> span = connectionAllocator.GetSpan(connectionAllocations[hierarchicalNode]);
				for (int i = 0; i < span.Length; i++)
				{
					int num = *span[i];
					if (nodeBounds[num].Intersects(bounds) && !indices.Contains(num))
					{
						NavmeshEdges.NavmeshBorderData.GetHierarchicalNodesInRangeRec(num, bounds, connectionAllocator, connectionAllocations, nodeBounds, indices);
					}
				}
			}

			// Token: 0x060003CC RID: 972 RVA: 0x00013758 File Offset: 0x00011958
			private unsafe static void ConvertObstaclesToEdges(ref SimulatorBurst.ObstacleData obstacleData, NativeList<int> obstacleIndices, Bounds localBounds, NativeList<float2> edgeBuffer, NativeMovementPlane movementPlane)
			{
				Bounds bounds = movementPlane.ToWorld(localBounds);
				ToPlaneMatrix toPlaneMatrix = movementPlane.AsWorldToPlaneMatrix();
				float3 @float = bounds.min;
				float3 float2 = bounds.max;
				float3 float3 = localBounds.min;
				float3 float4 = localBounds.max;
				int num = 0;
				for (int i = 0; i < obstacleIndices.Length; i++)
				{
					UnmanagedObstacle unmanagedObstacle = obstacleData.obstacles[obstacleIndices[i]];
					num += obstacleData.obstacleVertices.GetSpan(unmanagedObstacle.verticesAllocation).Length;
				}
				edgeBuffer.ResizeUninitialized(num * 3);
				int num2 = 0;
				for (int j = 0; j < obstacleIndices.Length; j++)
				{
					UnmanagedObstacle unmanagedObstacle2 = obstacleData.obstacles[obstacleIndices[j]];
					if (unmanagedObstacle2.verticesAllocation != -1)
					{
						UnsafeSpan<float3> span = obstacleData.obstacleVertices.GetSpan(unmanagedObstacle2.verticesAllocation);
						UnsafeSpan<ObstacleVertexGroup> span2 = obstacleData.obstacleVertexGroups.GetSpan(unmanagedObstacle2.groupsAllocation);
						int num3 = 0;
						for (int k = 0; k < span2.Length; k++)
						{
							ObstacleVertexGroup obstacleVertexGroup = *span2[k];
							if (!math.all((obstacleVertexGroup.boundsMx >= @float) & (obstacleVertexGroup.boundsMn <= float2)))
							{
								num3 += obstacleVertexGroup.vertexCount;
							}
							else
							{
								for (int l = 0; l < obstacleVertexGroup.vertexCount - 1; l++)
								{
									float3 float5 = *span[num3 + l];
									float3 float6 = *span[num3 + l + 1];
									float3 float7 = math.min(float5, float6);
									if (math.all((math.max(float5, float6) >= @float) & (float7 <= float2)))
									{
										float3 float8 = toPlaneMatrix.ToXZPlane(float5);
										float3 float9 = toPlaneMatrix.ToXZPlane(float6);
										float7 = math.min(float8, float9);
										if (math.all((math.max(float8, float9) >= float3) & (float7 <= float4)))
										{
											edgeBuffer[num2++] = float8.xz;
											edgeBuffer[num2++] = float9.xz;
										}
									}
								}
								if (obstacleVertexGroup.type == ObstacleType.Loop)
								{
									float3 float10 = *span[num3 + obstacleVertexGroup.vertexCount - 1];
									float3 float11 = *span[num3];
									float3 float12 = math.min(float10, float11);
									if (math.all((math.max(float10, float11) >= @float) & (float12 <= float2)))
									{
										float3 float13 = toPlaneMatrix.ToXZPlane(float10);
										float3 float14 = toPlaneMatrix.ToXZPlane(float11);
										float12 = math.min(float13, float14);
										if (math.all((math.max(float13, float14) >= float3) & (float12 <= float4)))
										{
											edgeBuffer[num2++] = float13.xz;
											edgeBuffer[num2++] = float14.xz;
										}
									}
								}
								num3 += obstacleVertexGroup.vertexCount;
							}
						}
					}
				}
				edgeBuffer.Length = num2;
			}

			// Token: 0x060003CD RID: 973 RVA: 0x00013AA4 File Offset: 0x00011CA4
			public void GetObstaclesInRange(int hierarchicalNode, Bounds bounds, NativeList<int> obstacleIndexBuffer)
			{
				if (!this.obstacleData.obstacleVertices.IsCreated)
				{
					return;
				}
				NavmeshEdges.NavmeshBorderData.GetHierarchicalNodesInRangeRec(hierarchicalNode, bounds, this.hierarhicalNodeData.connectionAllocator, this.hierarhicalNodeData.connectionAllocations, this.hierarhicalNodeData.bounds, obstacleIndexBuffer);
			}

			// Token: 0x060003CE RID: 974 RVA: 0x00013AE4 File Offset: 0x00011CE4
			public void GetEdgesInRange(int hierarchicalNode, Bounds localBounds, NativeList<float2> edgeBuffer, NativeMovementPlane movementPlane)
			{
				if (!this.obstacleData.obstacleVertices.IsCreated)
				{
					return;
				}
				NativeList<int> nativeList = new NativeList<int>(8, Allocator.Temp);
				this.GetObstaclesInRange(hierarchicalNode, movementPlane.ToWorld(localBounds), nativeList);
				NavmeshEdges.NavmeshBorderData.ConvertObstaclesToEdges(ref this.obstacleData, nativeList, localBounds, edgeBuffer, movementPlane);
			}

			// Token: 0x04000289 RID: 649
			public HierarchicalGraph.HierarhicalNodeData hierarhicalNodeData;

			// Token: 0x0400028A RID: 650
			public SimulatorBurst.ObstacleData obstacleData;
		}
	}
}
