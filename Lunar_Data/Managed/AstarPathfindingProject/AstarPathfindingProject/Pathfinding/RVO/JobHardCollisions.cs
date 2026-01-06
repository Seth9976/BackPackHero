using System;
using Pathfinding.Jobs;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;

namespace Pathfinding.RVO
{
	// Token: 0x02000283 RID: 643
	[BurstCompile(FloatMode = FloatMode.Fast)]
	public struct JobHardCollisions<MovementPlaneWrapper> : IJobParallelForBatched where MovementPlaneWrapper : struct, IMovementPlaneWrapper
	{
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000F3A RID: 3898 RVA: 0x00018013 File Offset: 0x00016213
		public bool allowBoundsChecks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x0005DFC4 File Offset: 0x0005C1C4
		public void Execute(int startIndex, int count)
		{
			if (!this.enabled)
			{
				for (int i = startIndex; i < startIndex + count; i++)
				{
					this.collisionVelocityOffsets[i] = float2.zero;
				}
				return;
			}
			for (int j = startIndex; j < startIndex + count; j++)
			{
				if (!this.agentData.version[j].Valid || this.agentData.locked[j])
				{
					this.collisionVelocityOffsets[j] = float2.zero;
				}
				else
				{
					NativeSlice<int> nativeSlice = this.neighbours.Slice(j * 50, 50);
					float num = this.agentData.radius[j];
					float2 @float = float2.zero;
					float num2 = 0f;
					float3 float2 = this.agentData.position[j];
					MovementPlaneWrapper movementPlaneWrapper = new MovementPlaneWrapper();
					movementPlaneWrapper.Set(this.agentData.movementPlane[j]);
					int num3 = 0;
					while (num3 < nativeSlice.Length && nativeSlice[num3] != -1)
					{
						int num4 = nativeSlice[num3];
						float2 float3 = movementPlaneWrapper.ToPlane(float2 - this.agentData.position[num4]);
						float num5 = math.lengthsq(float3);
						float num6 = this.agentData.radius[num4] + num;
						if (num5 < num6 * num6 && num5 > 1E-08f)
						{
							float num7 = math.sqrt(num5);
							float2 float4 = float3 * (1f / num7);
							float num8 = num6 - num7;
							float2 float5 = float4 * num8 * num8;
							@float += float5;
							num2 += num8;
						}
						num3++;
					}
					float2 float6 = @float * (1f / (0.0001f + num2));
					float6 *= 0.4f / this.deltaTime;
					this.collisionVelocityOffsets[j] = float6;
				}
			}
		}

		// Token: 0x04000B7B RID: 2939
		[ReadOnly]
		public SimulatorBurst.AgentData agentData;

		// Token: 0x04000B7C RID: 2940
		[ReadOnly]
		public NativeArray<int> neighbours;

		// Token: 0x04000B7D RID: 2941
		[WriteOnly]
		public NativeArray<float2> collisionVelocityOffsets;

		// Token: 0x04000B7E RID: 2942
		public float deltaTime;

		// Token: 0x04000B7F RID: 2943
		public bool enabled;

		// Token: 0x04000B80 RID: 2944
		private const float CollisionStrength = 0.8f;
	}
}
