using System;
using Pathfinding.ECS.RVO;
using Pathfinding.Jobs;
using Pathfinding.Util;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;

namespace Pathfinding.RVO
{
	// Token: 0x02000282 RID: 642
	[BurstCompile(FloatMode = FloatMode.Fast)]
	public struct JobHorizonAvoidancePhase2 : IJobParallelForBatched
	{
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000F38 RID: 3896 RVA: 0x00018013 File Offset: 0x00016213
		public bool allowBoundsChecks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x0005DDF4 File Offset: 0x0005BFF4
		public void Execute(int startIndex, int count)
		{
			for (int i = startIndex; i < startIndex + count; i++)
			{
				if (this.versions[i].Valid && this.horizonAgentData.horizonSide[i] != 0)
				{
					if (this.horizonAgentData.horizonSide[i] == 2)
					{
						float num = 0f;
						NativeSlice<int> nativeSlice = this.neighbours.Slice(i * 50, 50);
						int num2 = 0;
						while (num2 < nativeSlice.Length && nativeSlice[num2] != -1)
						{
							int num3 = nativeSlice[num2];
							float num4 = -(this.horizonAgentData.horizonMinAngle[num3] + this.horizonAgentData.horizonMaxAngle[num3]);
							num += num4;
							num2++;
						}
						float num5 = -(this.horizonAgentData.horizonMinAngle[i] + this.horizonAgentData.horizonMaxAngle[i]);
						num += num5;
						this.horizonAgentData.horizonSide[i] = ((num < 0f) ? (-1) : 1);
					}
					float2 @float;
					math.sincos((this.horizonAgentData.horizonSide[i] < 0) ? this.horizonAgentData.horizonMinAngle[i] : this.horizonAgentData.horizonMaxAngle[i], out @float.y, out @float.x);
					this.desiredVelocity[i] = this.movementPlane[i].ToWorld(math.length(this.desiredVelocity[i]) * @float, 0f);
					this.desiredTargetPointInVelocitySpace[i] = math.length(this.desiredTargetPointInVelocitySpace[i]) * @float;
				}
			}
		}

		// Token: 0x04000B75 RID: 2933
		[ReadOnly]
		public NativeArray<int> neighbours;

		// Token: 0x04000B76 RID: 2934
		[ReadOnly]
		public NativeArray<AgentIndex> versions;

		// Token: 0x04000B77 RID: 2935
		public NativeArray<float3> desiredVelocity;

		// Token: 0x04000B78 RID: 2936
		public NativeArray<float2> desiredTargetPointInVelocitySpace;

		// Token: 0x04000B79 RID: 2937
		[ReadOnly]
		public NativeArray<NativeMovementPlane> movementPlane;

		// Token: 0x04000B7A RID: 2938
		public SimulatorBurst.HorizonAgentData horizonAgentData;
	}
}
