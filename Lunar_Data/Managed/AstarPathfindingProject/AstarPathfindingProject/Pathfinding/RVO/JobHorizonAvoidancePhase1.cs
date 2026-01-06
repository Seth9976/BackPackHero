using System;
using Pathfinding.Drawing;
using Pathfinding.Jobs;
using Pathfinding.Util;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x02000281 RID: 641
	[BurstCompile(FloatMode = FloatMode.Fast)]
	public struct JobHorizonAvoidancePhase1 : IJobParallelForBatched
	{
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000F34 RID: 3892 RVA: 0x00016F22 File Offset: 0x00015122
		public bool allowBoundsChecks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x0005D958 File Offset: 0x0005BB58
		private static void Sort<T>(NativeSlice<T> arr, NativeSlice<float> keys) where T : struct
		{
			bool flag = true;
			while (flag)
			{
				flag = false;
				for (int i = 0; i < arr.Length - 1; i++)
				{
					if (keys[i] > keys[i + 1])
					{
						float num = keys[i];
						T t = arr[i];
						keys[i] = keys[i + 1];
						keys[i + 1] = num;
						arr[i] = arr[i + 1];
						arr[i + 1] = t;
						flag = true;
					}
				}
			}
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0005D9E4 File Offset: 0x0005BBE4
		public static float DeltaAngle(float current, float target)
		{
			float num = Mathf.Repeat(target - current, 6.2831855f);
			if (num > 3.1415927f)
			{
				num -= 6.2831855f;
			}
			return num;
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x0005DA10 File Offset: 0x0005BC10
		public void Execute(int startIndex, int count)
		{
			NativeArray<float> nativeArray = new NativeArray<float>(100, Allocator.Temp, NativeArrayOptions.ClearMemory);
			NativeArray<int> nativeArray2 = new NativeArray<int>(100, Allocator.Temp, NativeArrayOptions.ClearMemory);
			for (int i = startIndex; i < startIndex + count; i++)
			{
				if (this.agentData.version[i].Valid)
				{
					if (this.agentData.locked[i] || this.agentData.manuallyControlled[i])
					{
						this.horizonAgentData.horizonSide[i] = 0;
						this.horizonAgentData.horizonMinAngle[i] = 0f;
						this.horizonAgentData.horizonMaxAngle[i] = 0f;
					}
					else
					{
						float num = math.atan2(this.desiredTargetPointInVelocitySpace[i].y, this.desiredTargetPointInVelocitySpace[i].x);
						int num2 = 0;
						int num3 = 0;
						float num4 = this.agentData.radius[i];
						float3 @float = this.agentData.position[i];
						NativeMovementPlane nativeMovementPlane = this.agentData.movementPlane[i];
						NativeSlice<int> nativeSlice = this.neighbours.Slice(i * 50, 50);
						int num5 = 0;
						while (num5 < nativeSlice.Length && nativeSlice[num5] != -1)
						{
							int num6 = nativeSlice[num5];
							if (this.agentData.locked[num6] || this.agentData.manuallyControlled[num6])
							{
								float2 float2 = nativeMovementPlane.ToPlane(this.agentData.position[num6] - @float);
								float num7 = math.length(float2);
								float num8 = math.atan2(float2.y, float2.x) - num;
								float num9 = this.agentData.radius[num6];
								float num10;
								if (num7 < num4 + num9)
								{
									num10 = 1.5393804f;
								}
								else
								{
									num10 = math.asin((num4 + num9) / num7) + 0.017453292f;
								}
								float num11 = JobHorizonAvoidancePhase1.DeltaAngle(0f, num8 - num10);
								float num12 = num11 + JobHorizonAvoidancePhase1.DeltaAngle(num11, num8 + num10);
								if (num11 < 0f && num12 > 0f)
								{
									num3++;
								}
								nativeArray[num2] = num11;
								nativeArray2[num2] = 1;
								num2++;
								nativeArray[num2] = num12;
								nativeArray2[num2] = -1;
								num2++;
							}
							num5++;
						}
						if (num3 == 0)
						{
							this.horizonAgentData.horizonSide[i] = 0;
							this.horizonAgentData.horizonMinAngle[i] = 0f;
							this.horizonAgentData.horizonMaxAngle[i] = 0f;
						}
						else
						{
							JobHorizonAvoidancePhase1.Sort<int>(nativeArray2.Slice(0, num2), nativeArray.Slice(0, num2));
							int num13 = 0;
							while (num13 < num2 && nativeArray[num13] <= 0f)
							{
								num13++;
							}
							int num14 = num3;
							int j;
							for (j = num13; j < num2; j++)
							{
								num14 += nativeArray2[j];
								if (num14 == 0)
								{
									break;
								}
							}
							float num15 = ((j == num2) ? 3.1415927f : nativeArray[j]);
							num14 = num3;
							for (j = num13 - 1; j >= 0; j--)
							{
								num14 -= nativeArray2[j];
								if (num14 == 0)
								{
									break;
								}
							}
							float num16 = ((j == -1) ? (-3.1415927f) : nativeArray[j]);
							if (this.horizonAgentData.horizonSide[i] == 0)
							{
								this.horizonAgentData.horizonSide[i] = 2;
							}
							this.horizonAgentData.horizonMinAngle[i] = num16 + num;
							this.horizonAgentData.horizonMaxAngle[i] = num15 + num;
						}
					}
				}
			}
		}

		// Token: 0x04000B70 RID: 2928
		[ReadOnly]
		public SimulatorBurst.AgentData agentData;

		// Token: 0x04000B71 RID: 2929
		[ReadOnly]
		public NativeArray<float2> desiredTargetPointInVelocitySpace;

		// Token: 0x04000B72 RID: 2930
		[ReadOnly]
		public NativeArray<int> neighbours;

		// Token: 0x04000B73 RID: 2931
		public SimulatorBurst.HorizonAgentData horizonAgentData;

		// Token: 0x04000B74 RID: 2932
		public CommandBuilder draw;
	}
}
