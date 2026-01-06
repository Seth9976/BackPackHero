using System;
using Pathfinding.Jobs;
using Pathfinding.Util;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.Graphs.Grid.Jobs
{
	// Token: 0x0200021B RID: 539
	[BurstCompile(FloatMode = FloatMode.Fast, CompileSynchronously = true)]
	public struct JobCalculateGridConnections : IJobParallelForBatched
	{
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x00018013 File Offset: 0x00016213
		public bool allowBoundsChecks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x00050990 File Offset: 0x0004EB90
		public static bool IsValidConnection(float4 nodePosA, float4 nodeNormalA, bool nodeWalkableB, float4 nodePosB, float4 nodeNormalB, bool maxStepUsesSlope, float maxStepHeight, float4 up)
		{
			if (!nodeWalkableB)
			{
				return false;
			}
			if (!maxStepUsesSlope)
			{
				return math.abs(math.dot(up, nodePosB - nodePosA)) <= maxStepHeight;
			}
			float4 @float = nodePosB - nodePosA;
			float num = math.dot(@float, up);
			if (math.abs(num) <= maxStepHeight)
			{
				return true;
			}
			float4 float2 = (@float - num * up) * 0.5f;
			float num2 = math.dot(nodeNormalA, up);
			float num3 = -math.dot(nodeNormalA - num2 * up, float2);
			num2 = math.dot(nodeNormalB, up);
			float num4 = math.dot(nodeNormalB - num2 * up, float2);
			return math.abs(num + num4 - num3) <= maxStepHeight;
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x00050A4B File Offset: 0x0004EC4B
		public void Execute(int start, int count)
		{
			if (this.layeredDataLayout)
			{
				this.ExecuteLayered(start, count);
				return;
			}
			this.ExecuteFlat(start, count);
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x00050A68 File Offset: 0x0004EC68
		public unsafe void ExecuteFlat(int start, int count)
		{
			if (this.maxStepHeight <= 0f || this.use2D)
			{
				this.maxStepHeight = float.PositiveInfinity;
			}
			float4 @float = new float4(this.up.x, this.up.y, this.up.z, 0f);
			NativeArray<int> nativeArray = new NativeArray<int>(8, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			for (int i = 0; i < 8; i++)
			{
				nativeArray[i] = GridGraph.neighbourZOffsets[i] * this.arrayBounds.x + GridGraph.neighbourXOffsets[i];
			}
			UnsafeSpan<float3> unsafeSpan = this.nodePositions.Reinterpret<float3>();
			start += this.bounds.min.z;
			for (int j = start; j < start + count; j++)
			{
				int num = 255;
				if (j == 0)
				{
					num &= -146;
				}
				if (j == this.arrayBounds.z - 1)
				{
					num &= -101;
				}
				for (int k = this.bounds.min.x; k < this.bounds.max.x; k++)
				{
					int num2 = j * this.arrayBounds.x + k;
					if (!(*this.nodeWalkable[num2]))
					{
						*this.nodeConnections[num2] = 0UL;
					}
					else
					{
						int num3 = num;
						if (k == 0)
						{
							num3 &= -201;
						}
						if (k == this.arrayBounds.x - 1)
						{
							num3 &= -51;
						}
						float4 float2 = new float4(*unsafeSpan[num2], 0f);
						float4 float3 = *this.nodeNormals[num2];
						for (int l = 0; l < 8; l++)
						{
							int num4 = num2 + nativeArray[l];
							if ((num3 & (1 << l)) != 0 && !JobCalculateGridConnections.IsValidConnection(float2, float3, *this.nodeWalkable[num4], new float4(*unsafeSpan[num4], 0f), *this.nodeNormals[num4], this.maxStepUsesSlope, this.maxStepHeight, @float))
							{
								num3 &= ~(1 << l);
							}
						}
						*this.nodeConnections[num2] = (ulong)((long)GridNode.FilterDiagonalConnections(num3, this.neighbours, this.cutCorners));
					}
				}
			}
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x00050CD0 File Offset: 0x0004EED0
		public unsafe void ExecuteLayered(int start, int count)
		{
			if (this.maxStepHeight <= 0f || this.use2D)
			{
				this.maxStepHeight = float.PositiveInfinity;
			}
			float4 @float = new float4(this.up.x, this.up.y, this.up.z, 0f);
			NativeArray<int> nativeArray = new NativeArray<int>(8, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			for (int i = 0; i < 8; i++)
			{
				nativeArray[i] = GridGraph.neighbourZOffsets[i] * this.arrayBounds.x + GridGraph.neighbourXOffsets[i];
			}
			int num = this.arrayBounds.z * this.arrayBounds.x;
			start += this.bounds.min.z;
			for (int j = this.bounds.min.y; j < this.bounds.max.y; j++)
			{
				for (int k = start; k < start + count; k++)
				{
					for (int l = this.bounds.min.x; l < this.bounds.max.x; l++)
					{
						ulong num2 = 0UL;
						int num3 = k * this.arrayBounds.x + l;
						int num4 = num3 + j * num;
						float4 float2 = new float4(*this.nodePositions[num4], 0f);
						float4 float3 = *this.nodeNormals[num4];
						if (*this.nodeWalkable[num4])
						{
							float num5 = math.dot(@float, float2);
							float num6;
							if (j == this.arrayBounds.y - 1 || !math.any(*this.nodeNormals[num4 + num]))
							{
								num6 = float.PositiveInfinity;
							}
							else
							{
								float4 float4 = new float4(*this.nodePositions[num4 + num], 0f);
								num6 = math.max(0f, math.dot(@float, float4) - num5);
							}
							for (int m = 0; m < 8; m++)
							{
								int num7 = l + GridGraph.neighbourXOffsets[m];
								int num8 = k + GridGraph.neighbourZOffsets[m];
								int num9 = 15;
								if (num7 >= 0 && num8 >= 0 && num7 < this.arrayBounds.x && num8 < this.arrayBounds.z)
								{
									int num10 = num3 + nativeArray[m];
									for (int n = 0; n < this.arrayBounds.y; n++)
									{
										int num11 = num10 + n * num;
										float4 float5 = new float4(*this.nodePositions[num11], 0f);
										float num12 = math.dot(@float, float5);
										float num13;
										if (n == this.arrayBounds.y - 1 || !math.any(*this.nodeNormals[num11 + num]))
										{
											num13 = float.PositiveInfinity;
										}
										else
										{
											float4 float6 = new float4(*this.nodePositions[num11 + num], 0f);
											num13 = math.max(0f, math.dot(@float, float6) - num12);
										}
										float num14 = math.max(num12, num5);
										if (math.min(num12 + num13, num5 + num6) - num14 >= this.characterHeight && JobCalculateGridConnections.IsValidConnection(float2, float3, *this.nodeWalkable[num11], new float4(*this.nodePositions[num11], 0f), *this.nodeNormals[num11], this.maxStepUsesSlope, this.maxStepHeight, @float))
										{
											num9 = n;
										}
									}
								}
								num2 |= (ulong)((ulong)((long)num9) << 4 * m);
							}
						}
						else
						{
							num2 = (ulong)(-1);
						}
						*this.nodeConnections[num4] = num2;
					}
				}
			}
		}

		// Token: 0x040009D0 RID: 2512
		public float maxStepHeight;

		// Token: 0x040009D1 RID: 2513
		public Vector3 up;

		// Token: 0x040009D2 RID: 2514
		public IntBounds bounds;

		// Token: 0x040009D3 RID: 2515
		public int3 arrayBounds;

		// Token: 0x040009D4 RID: 2516
		public NumNeighbours neighbours;

		// Token: 0x040009D5 RID: 2517
		public bool use2D;

		// Token: 0x040009D6 RID: 2518
		public bool cutCorners;

		// Token: 0x040009D7 RID: 2519
		public bool maxStepUsesSlope;

		// Token: 0x040009D8 RID: 2520
		public float characterHeight;

		// Token: 0x040009D9 RID: 2521
		public bool layeredDataLayout;

		// Token: 0x040009DA RID: 2522
		[ReadOnly]
		public UnsafeSpan<bool> nodeWalkable;

		// Token: 0x040009DB RID: 2523
		[ReadOnly]
		public UnsafeSpan<float4> nodeNormals;

		// Token: 0x040009DC RID: 2524
		[ReadOnly]
		public UnsafeSpan<Vector3> nodePositions;

		// Token: 0x040009DD RID: 2525
		[WriteOnly]
		public UnsafeSpan<ulong> nodeConnections;
	}
}
