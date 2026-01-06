using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Pathfinding.Jobs;
using Pathfinding.Util;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Profiling;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x0200029F RID: 671
	[BurstCompile]
	public static class RVOObstacleCache
	{
		// Token: 0x06000FF4 RID: 4084 RVA: 0x00062E30 File Offset: 0x00061030
		private static ulong HashKey(GraphNode sourceNode, int traversableTags, SimpleMovementPlane movementPlane)
		{
			return ((((((((((ulong)sourceNode.NodeIndex * 786433UL) ^ (ulong)((long)traversableTags)) * 786433UL) ^ (ulong)(movementPlane.rotation.x * 4f)) * 786433UL) ^ (ulong)(movementPlane.rotation.y * 4f)) * 786433UL) ^ (ulong)(movementPlane.rotation.z * 4f)) * 786433UL) ^ (ulong)(movementPlane.rotation.w * 4f);
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x00062EB8 File Offset: 0x000610B8
		public unsafe static void CollectContours(List<GraphNode> nodes, NativeList<RVOObstacleCache.ObstacleSegment> obstacles)
		{
			if (nodes.Count == 0)
			{
				return;
			}
			if (nodes[0] is TriangleMeshNode)
			{
				for (int i = 0; i < nodes.Count; i++)
				{
					TriangleMeshNode triangleMeshNode = nodes[i] as TriangleMeshNode;
					int num = 0;
					if (triangleMeshNode.connections != null)
					{
						for (int j = 0; j < triangleMeshNode.connections.Length; j++)
						{
							Connection connection = triangleMeshNode.connections[j];
							if (connection.isEdgeShared)
							{
								num |= 1 << connection.shapeEdge;
							}
						}
					}
					Int3 @int;
					Int3 int2;
					Int3 int3;
					triangleMeshNode.GetVertices(out @int, out int2, out int3);
					for (int k = 0; k < 3; k++)
					{
						if ((num & (1 << k)) == 0)
						{
							Int3 int4;
							Int3 int5;
							switch (k)
							{
							case 0:
								int4 = @int;
								int5 = int2;
								break;
							case 1:
								int4 = int2;
								int5 = int3;
								break;
							case 2:
								goto IL_00C0;
							default:
								goto IL_00C0;
							}
							IL_00C7:
							int hashCode = int4.GetHashCode();
							int hashCode2 = int5.GetHashCode();
							RVOObstacleCache.ObstacleSegment obstacleSegment = default(RVOObstacleCache.ObstacleSegment);
							obstacleSegment.vertex1 = (Vector3)int4;
							obstacleSegment.vertex2 = (Vector3)int5;
							obstacleSegment.vertex1LinkId = hashCode;
							obstacleSegment.vertex2LinkId = hashCode2;
							obstacles.Add(in obstacleSegment);
							goto IL_012E;
							IL_00C0:
							int4 = int3;
							int5 = @int;
							goto IL_00C7;
						}
						IL_012E:;
					}
				}
				return;
			}
			if (nodes[0] is GridNodeBase)
			{
				GridGraph gridGraph;
				if (nodes[0] is LevelGridNode)
				{
					gridGraph = LevelGridNode.GetGridGraph(nodes[0].GraphIndex);
				}
				else
				{
					gridGraph = GridNode.GetGridGraph(nodes[0].GraphIndex);
				}
				Vector3* ptr;
				checked
				{
					ptr = stackalloc Vector3[unchecked((UIntPtr)4) * (UIntPtr)sizeof(Vector3)];
				}
				for (int l = 0; l < 4; l++)
				{
					int num2 = (l + 1) % 4;
					ptr[l] = gridGraph.transform.TransformVector(0.5f * new Vector3((float)(GridGraph.neighbourXOffsets[l] + GridGraph.neighbourXOffsets[num2]), 0f, (float)(GridGraph.neighbourZOffsets[l] + GridGraph.neighbourZOffsets[num2])));
				}
				for (int m = 0; m < nodes.Count; m++)
				{
					GridNodeBase gridNodeBase = nodes[m] as GridNodeBase;
					if (!gridNodeBase.HasConnectionsToAllAxisAlignedNeighbours)
					{
						for (int n = 0; n < 4; n++)
						{
							if (!gridNodeBase.HasConnectionInDirection(n))
							{
								int num3 = (n + 1) % 4;
								int num4 = (n - 1 + 4) % 4;
								GridNodeBase neighbourAlongDirection = gridNodeBase.GetNeighbourAlongDirection(num3);
								GridNodeBase neighbourAlongDirection2 = gridNodeBase.GetNeighbourAlongDirection(num4);
								uint num5;
								if (neighbourAlongDirection != null)
								{
									GridNodeBase neighbourAlongDirection3 = neighbourAlongDirection.GetNeighbourAlongDirection(n);
									if (neighbourAlongDirection3 != null)
									{
										uint nodeIndex = gridNodeBase.NodeIndex;
										uint nodeIndex2 = neighbourAlongDirection.NodeIndex;
										uint nodeIndex3 = neighbourAlongDirection3.NodeIndex;
										if (nodeIndex > nodeIndex2)
										{
											Memory.Swap<uint>(ref nodeIndex, ref nodeIndex2);
										}
										if (nodeIndex2 > nodeIndex3)
										{
											Memory.Swap<uint>(ref nodeIndex2, ref nodeIndex3);
										}
										if (nodeIndex > nodeIndex2)
										{
											Memory.Swap<uint>(ref nodeIndex, ref nodeIndex2);
										}
										num5 = math.hash(new uint3(nodeIndex, nodeIndex2, nodeIndex3));
									}
									else
									{
										uint nodeIndex4 = gridNodeBase.NodeIndex;
										uint nodeIndex5 = neighbourAlongDirection.NodeIndex;
										if (nodeIndex4 > nodeIndex5)
										{
											Memory.Swap<uint>(ref nodeIndex4, ref nodeIndex5);
										}
										num5 = math.hash(new uint3(nodeIndex4, nodeIndex5, (uint)n));
									}
								}
								else
								{
									int num6 = n + 4;
									num5 = math.hash(new uint2(gridNodeBase.NodeIndex, (uint)num6));
								}
								uint num7;
								if (neighbourAlongDirection2 != null)
								{
									GridNodeBase neighbourAlongDirection4 = neighbourAlongDirection2.GetNeighbourAlongDirection(n);
									if (neighbourAlongDirection4 != null)
									{
										uint nodeIndex6 = gridNodeBase.NodeIndex;
										uint nodeIndex7 = neighbourAlongDirection2.NodeIndex;
										uint nodeIndex8 = neighbourAlongDirection4.NodeIndex;
										if (nodeIndex6 > nodeIndex7)
										{
											Memory.Swap<uint>(ref nodeIndex6, ref nodeIndex7);
										}
										if (nodeIndex7 > nodeIndex8)
										{
											Memory.Swap<uint>(ref nodeIndex7, ref nodeIndex8);
										}
										if (nodeIndex6 > nodeIndex7)
										{
											Memory.Swap<uint>(ref nodeIndex6, ref nodeIndex7);
										}
										num7 = math.hash(new uint3(nodeIndex6, nodeIndex7, nodeIndex8));
									}
									else
									{
										uint nodeIndex9 = gridNodeBase.NodeIndex;
										uint nodeIndex10 = neighbourAlongDirection2.NodeIndex;
										if (nodeIndex9 > nodeIndex10)
										{
											Memory.Swap<uint>(ref nodeIndex9, ref nodeIndex10);
										}
										num7 = math.hash(new uint3(nodeIndex9, nodeIndex10, (uint)n));
									}
								}
								else
								{
									int num8 = num4 + 4;
									num7 = math.hash(new uint2(gridNodeBase.NodeIndex, (uint)num8));
								}
								Vector3 vector = (Vector3)gridNodeBase.position;
								RVOObstacleCache.ObstacleSegment obstacleSegment = default(RVOObstacleCache.ObstacleSegment);
								obstacleSegment.vertex1 = vector + ptr[n];
								obstacleSegment.vertex2 = vector + ptr[num4];
								obstacleSegment.vertex1LinkId = (int)num5;
								obstacleSegment.vertex2LinkId = (int)num7;
								obstacles.Add(in obstacleSegment);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x00063355 File Offset: 0x00061555
		[BurstCompile]
		public unsafe static void TraceContours(ref UnsafeSpan<RVOObstacleCache.ObstacleSegment> obstaclesSpan, ref NativeMovementPlane movementPlane, int obstacleId, UnmanagedObstacle* outputObstacles, ref SlabAllocator<float3> verticesAllocator, ref SlabAllocator<ObstacleVertexGroup> obstaclesAllocator, ref SpinLock spinLock, bool simplifyObstacles)
		{
			RVOObstacleCache.TraceContours_00000EED$BurstDirectCall.Invoke(ref obstaclesSpan, ref movementPlane, obstacleId, outputObstacles, ref verticesAllocator, ref obstaclesAllocator, ref spinLock, simplifyObstacles);
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x0006337C File Offset: 0x0006157C
		[BurstCompile]
		[MethodImpl(256)]
		public unsafe static void TraceContours$BurstManaged(ref UnsafeSpan<RVOObstacleCache.ObstacleSegment> obstaclesSpan, ref NativeMovementPlane movementPlane, int obstacleId, UnmanagedObstacle* outputObstacles, ref SlabAllocator<float3> verticesAllocator, ref SlabAllocator<ObstacleVertexGroup> obstaclesAllocator, ref SpinLock spinLock, bool simplifyObstacles)
		{
			UnsafeSpan<RVOObstacleCache.ObstacleSegment> unsafeSpan = obstaclesSpan;
			if (unsafeSpan.Length == 0)
			{
				outputObstacles[obstacleId] = new UnmanagedObstacle
				{
					verticesAllocation = -1,
					groupsAllocation = -1
				};
				return;
			}
			NativeParallelHashMap<int, int> nativeParallelHashMap = new NativeParallelHashMap<int, int>(unsafeSpan.Length, Allocator.Temp);
			NativeArray<byte> nativeArray = new NativeArray<byte>(unsafeSpan.Length, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			for (int i = 0; i < unsafeSpan.Length; i++)
			{
				if (nativeParallelHashMap.TryAdd(unsafeSpan[i].vertex1LinkId, i))
				{
					nativeArray[i] = 2;
				}
				else
				{
					nativeArray[i] = 0;
				}
			}
			for (int j = 0; j < unsafeSpan.Length; j++)
			{
				int num;
				if (nativeParallelHashMap.TryGetValue(unsafeSpan[j].vertex2LinkId, out num) && nativeArray[num] > 0)
				{
					nativeArray[num] = 1;
				}
			}
			NativeList<ObstacleVertexGroup> nativeList = new NativeList<ObstacleVertexGroup>(16, Allocator.Temp);
			NativeList<float3> nativeList2 = new NativeList<float3>(16, Allocator.Temp);
			ToPlaneMatrix toPlaneMatrix = movementPlane.AsWorldToPlaneMatrix();
			for (int k = 0; k <= 1; k++)
			{
				int num2 = ((k == 1) ? 1 : 2);
				for (int l = 0; l < unsafeSpan.Length; l++)
				{
					if ((int)nativeArray[l] >= num2)
					{
						int length = nativeList2.Length;
						nativeList2.Add(in unsafeSpan[l].vertex1);
						float3 @float = unsafeSpan[l].vertex1;
						float3 float2 = unsafeSpan[l].vertex2;
						int num3 = l;
						ObstacleType obstacleType = ObstacleType.Chain;
						float3 float3 = @float;
						float3 float4 = @float;
						while (nativeArray[num3] != 0)
						{
							nativeArray[num3] = 0;
							int num4;
							float3 float5;
							if (nativeParallelHashMap.TryGetValue(unsafeSpan[num3].vertex2LinkId, out num4))
							{
								float5 = 0.5f * (unsafeSpan[num3].vertex2 + unsafeSpan[num4].vertex1);
							}
							else
							{
								float5 = unsafeSpan[num3].vertex2;
								num4 = -1;
							}
							float3 float6 = @float;
							float3 float7 = float5;
							float3 float8 = float2;
							float2 float9 = toPlaneMatrix.ToPlane(float7 - float6);
							float2 float10 = toPlaneMatrix.ToPlane(float8 - float6);
							if (math.abs(VectorMath.Determinant(float9, float10)) >= 0.01f || !simplifyObstacles)
							{
								nativeList2.Add(in float2);
								float3 = math.min(float3, float2);
								float4 = math.max(float4, float2);
								@float = float8;
							}
							if (num4 == l)
							{
								nativeList2[length] = float5;
								obstacleType = ObstacleType.Loop;
								break;
							}
							if (num4 == -1)
							{
								nativeList2.Add(in float5);
								float3 = math.min(float3, float5);
								float4 = math.max(float4, float5);
								break;
							}
							num3 = num4;
							float2 = float5;
						}
						ObstacleVertexGroup obstacleVertexGroup = default(ObstacleVertexGroup);
						obstacleVertexGroup.type = obstacleType;
						obstacleVertexGroup.vertexCount = nativeList2.Length - length;
						obstacleVertexGroup.boundsMn = float3;
						obstacleVertexGroup.boundsMx = float4;
						nativeList.Add(in obstacleVertexGroup);
					}
				}
			}
			int num5;
			int num6;
			if (nativeList.Length > 0)
			{
				spinLock.Lock();
				num5 = obstaclesAllocator.Allocate(nativeList);
				num6 = verticesAllocator.Allocate(nativeList2);
				spinLock.Unlock();
			}
			else
			{
				num5 = -1;
				num6 = -1;
			}
			outputObstacles[obstacleId] = new UnmanagedObstacle
			{
				verticesAllocation = num6,
				groupsAllocation = num5
			};
		}

		// Token: 0x04000C1D RID: 3101
		private static readonly ProfilerMarker MarkerAllocate = new ProfilerMarker("Allocate");

		// Token: 0x020002A0 RID: 672
		public struct ObstacleSegment
		{
			// Token: 0x04000C1E RID: 3102
			public float3 vertex1;

			// Token: 0x04000C1F RID: 3103
			public float3 vertex2;

			// Token: 0x04000C20 RID: 3104
			public int vertex1LinkId;

			// Token: 0x04000C21 RID: 3105
			public int vertex2LinkId;
		}

		// Token: 0x020002A1 RID: 673
		// (Invoke) Token: 0x06000FFA RID: 4090
		public unsafe delegate void TraceContours_00000EED$PostfixBurstDelegate(ref UnsafeSpan<RVOObstacleCache.ObstacleSegment> obstaclesSpan, ref NativeMovementPlane movementPlane, int obstacleId, UnmanagedObstacle* outputObstacles, ref SlabAllocator<float3> verticesAllocator, ref SlabAllocator<ObstacleVertexGroup> obstaclesAllocator, ref SpinLock spinLock, bool simplifyObstacles);

		// Token: 0x020002A2 RID: 674
		internal static class TraceContours_00000EED$BurstDirectCall
		{
			// Token: 0x06000FFD RID: 4093 RVA: 0x000636F5 File Offset: 0x000618F5
			[BurstDiscard]
			private unsafe static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (RVOObstacleCache.TraceContours_00000EED$BurstDirectCall.Pointer == 0)
				{
					RVOObstacleCache.TraceContours_00000EED$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(RVOObstacleCache.TraceContours_00000EED$BurstDirectCall.DeferredCompilation, methodof(RVOObstacleCache.TraceContours$BurstManaged(ref UnsafeSpan<RVOObstacleCache.ObstacleSegment>, ref NativeMovementPlane, int, UnmanagedObstacle*, ref SlabAllocator<float3>, ref SlabAllocator<ObstacleVertexGroup>, ref SpinLock, bool)).MethodHandle, typeof(RVOObstacleCache.TraceContours_00000EED$PostfixBurstDelegate).TypeHandle);
				}
				A_0 = RVOObstacleCache.TraceContours_00000EED$BurstDirectCall.Pointer;
			}

			// Token: 0x06000FFE RID: 4094 RVA: 0x00063724 File Offset: 0x00061924
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				RVOObstacleCache.TraceContours_00000EED$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x06000FFF RID: 4095 RVA: 0x0006373C File Offset: 0x0006193C
			public unsafe static void Constructor()
			{
				RVOObstacleCache.TraceContours_00000EED$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(RVOObstacleCache.TraceContours(ref UnsafeSpan<RVOObstacleCache.ObstacleSegment>, ref NativeMovementPlane, int, UnmanagedObstacle*, ref SlabAllocator<float3>, ref SlabAllocator<ObstacleVertexGroup>, ref SpinLock, bool)).MethodHandle);
			}

			// Token: 0x06001000 RID: 4096 RVA: 0x000033F6 File Offset: 0x000015F6
			public static void Initialize()
			{
			}

			// Token: 0x06001001 RID: 4097 RVA: 0x0006374D File Offset: 0x0006194D
			// Note: this type is marked as 'beforefieldinit'.
			static TraceContours_00000EED$BurstDirectCall()
			{
				RVOObstacleCache.TraceContours_00000EED$BurstDirectCall.Constructor();
			}

			// Token: 0x06001002 RID: 4098 RVA: 0x00063754 File Offset: 0x00061954
			public unsafe static void Invoke(ref UnsafeSpan<RVOObstacleCache.ObstacleSegment> obstaclesSpan, ref NativeMovementPlane movementPlane, int obstacleId, UnmanagedObstacle* outputObstacles, ref SlabAllocator<float3> verticesAllocator, ref SlabAllocator<ObstacleVertexGroup> obstaclesAllocator, ref SpinLock spinLock, bool simplifyObstacles)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = RVOObstacleCache.TraceContours_00000EED$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						calli(System.Void(Pathfinding.Util.UnsafeSpan`1<Pathfinding.RVO.RVOObstacleCache/ObstacleSegment>&,Pathfinding.Util.NativeMovementPlane&,System.Int32,Pathfinding.RVO.UnmanagedObstacle*,Pathfinding.Util.SlabAllocator`1<Unity.Mathematics.float3>&,Pathfinding.Util.SlabAllocator`1<Pathfinding.RVO.ObstacleVertexGroup>&,Pathfinding.Jobs.SpinLock&,System.Boolean), ref obstaclesSpan, ref movementPlane, obstacleId, outputObstacles, ref verticesAllocator, ref obstaclesAllocator, ref spinLock, simplifyObstacles, functionPointer);
						return;
					}
				}
				RVOObstacleCache.TraceContours$BurstManaged(ref obstaclesSpan, ref movementPlane, obstacleId, outputObstacles, ref verticesAllocator, ref obstaclesAllocator, ref spinLock, simplifyObstacles);
			}

			// Token: 0x04000C22 RID: 3106
			private static IntPtr Pointer;

			// Token: 0x04000C23 RID: 3107
			private static IntPtr DeferredCompilation;
		}
	}
}
