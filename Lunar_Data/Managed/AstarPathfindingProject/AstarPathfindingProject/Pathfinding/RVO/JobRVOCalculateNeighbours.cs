using System;
using Pathfinding.Jobs;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;

namespace Pathfinding.RVO
{
	// Token: 0x02000284 RID: 644
	[BurstCompile(CompileSynchronously = false, FloatMode = FloatMode.Fast)]
	public struct JobRVOCalculateNeighbours<MovementPlaneWrapper> : IJobParallelForBatched where MovementPlaneWrapper : struct, IMovementPlaneWrapper
	{
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x00018013 File Offset: 0x00016213
		public bool allowBoundsChecks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x0005E1C4 File Offset: 0x0005C3C4
		public void Execute(int startIndex, int count)
		{
			NativeArray<float> nativeArray = new NativeArray<float>(50, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			for (int i = startIndex; i < startIndex + count; i++)
			{
				if (this.agentData.version[i].Valid)
				{
					this.CalculateNeighbours(i, this.outNeighbours, nativeArray);
				}
			}
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x0005E214 File Offset: 0x0005C414
		private void CalculateNeighbours(int agentIndex, NativeArray<int> neighbours, NativeArray<float> neighbourDistances)
		{
			int num = math.min(50, this.agentData.maxNeighbours[agentIndex]);
			int num2 = agentIndex * 50;
			this.quadtree.QueryKNearest(new RVOQuadtreeBurst.QuadtreeQuery
			{
				position = this.agentData.position[agentIndex],
				speed = this.agentData.maxSpeed[agentIndex],
				agentRadius = this.agentData.radius[agentIndex],
				timeHorizon = this.agentData.agentTimeHorizon[agentIndex],
				outputStartIndex = num2,
				maxCount = num,
				result = neighbours,
				resultDistances = neighbourDistances
			});
			int num3 = 0;
			while (num3 < num && math.isfinite(neighbourDistances[num3]))
			{
				num3++;
			}
			this.output.numNeighbours[agentIndex] = num3;
			MovementPlaneWrapper movementPlaneWrapper = default(MovementPlaneWrapper);
			movementPlaneWrapper.Set(this.agentData.movementPlane[agentIndex]);
			float num4;
			movementPlaneWrapper.ToPlane(this.agentData.position[agentIndex], out num4);
			for (int i = 0; i < num3; i++)
			{
				int num5 = neighbours[num2 + i];
				float num6;
				movementPlaneWrapper.ToPlane(this.agentData.position[num5], out num6);
				float num7 = math.min(num4 + this.agentData.height[agentIndex], num6 + this.agentData.height[num5]);
				float num8 = math.max(num4, num6);
				if ((num7 < num8) | (num5 == agentIndex) | ((this.agentData.collidesWith[agentIndex] & this.agentData.layer[num5]) == (RVOLayer)0))
				{
					num3--;
					neighbours[num2 + i] = neighbours[num2 + num3];
					i--;
				}
			}
			if (num3 < 50)
			{
				neighbours[num2 + num3] = -1;
			}
		}

		// Token: 0x04000B81 RID: 2945
		[ReadOnly]
		public SimulatorBurst.AgentData agentData;

		// Token: 0x04000B82 RID: 2946
		[ReadOnly]
		public RVOQuadtreeBurst quadtree;

		// Token: 0x04000B83 RID: 2947
		public NativeArray<int> outNeighbours;

		// Token: 0x04000B84 RID: 2948
		[WriteOnly]
		public SimulatorBurst.AgentOutputData output;
	}
}
