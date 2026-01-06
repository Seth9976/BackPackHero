using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace Pathfinding.RVO
{
	// Token: 0x02000280 RID: 640
	[BurstCompile(CompileSynchronously = false, FloatMode = FloatMode.Fast)]
	public struct JobRVOPreprocess : IJob
	{
		// Token: 0x06000F33 RID: 3891 RVA: 0x0005D750 File Offset: 0x0005B950
		public void Execute()
		{
			for (int i = this.startIndex; i < this.endIndex; i++)
			{
				if (this.agentData.version[i].Valid)
				{
					if (this.agentData.locked[i] & !this.agentData.manuallyControlled[i])
					{
						this.temporaryAgentData.desiredTargetPointInVelocitySpace[i] = float2.zero;
						this.temporaryAgentData.desiredVelocity[i] = float3.zero;
						this.temporaryAgentData.currentVelocity[i] = float3.zero;
					}
					else
					{
						float2 @float = this.agentData.movementPlane[i].ToPlane(this.agentData.targetPoint[i] - this.agentData.position[i]);
						this.temporaryAgentData.desiredTargetPointInVelocitySpace[i] = @float;
						float3 float2 = math.normalizesafe(this.previousOutput.targetPoint[i] - this.agentData.position[i], default(float3)) * this.previousOutput.speed[i];
						this.temporaryAgentData.desiredVelocity[i] = this.agentData.movementPlane[i].ToWorld(math.normalizesafe(@float, default(float2)) * this.agentData.desiredSpeed[i], 0f);
						float3 float3 = math.normalizesafe(this.agentData.collisionNormal[i], default(float3));
						float num = math.dot(float2, float3);
						float2 -= math.min(0f, num) * float3;
						this.temporaryAgentData.currentVelocity[i] = float2;
					}
				}
			}
		}

		// Token: 0x04000B6B RID: 2923
		[ReadOnly]
		public SimulatorBurst.AgentData agentData;

		// Token: 0x04000B6C RID: 2924
		[ReadOnly]
		public SimulatorBurst.AgentOutputData previousOutput;

		// Token: 0x04000B6D RID: 2925
		[WriteOnly]
		public SimulatorBurst.TemporaryAgentData temporaryAgentData;

		// Token: 0x04000B6E RID: 2926
		public int startIndex;

		// Token: 0x04000B6F RID: 2927
		public int endIndex;
	}
}
