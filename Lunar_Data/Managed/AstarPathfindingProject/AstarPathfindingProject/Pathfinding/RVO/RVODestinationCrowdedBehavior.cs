using System;
using Pathfinding.Jobs;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x0200027D RID: 637
	[Serializable]
	public struct RVODestinationCrowdedBehavior
	{
		// Token: 0x06000F25 RID: 3877 RVA: 0x0005D03C File Offset: 0x0005B23C
		public void ReadJobResult(ref RVODestinationCrowdedBehavior.JobDensityCheck jobResult, int index)
		{
			bool flag = jobResult.outThresholdResult[index];
			this.progressAverage = jobResult.progressAverage[index];
			this.lastJobDensityResult = flag;
			this.shouldStopDelayTimer = Mathf.Lerp(this.shouldStopDelayTimer, (float)(flag ? 1 : 0), Time.deltaTime);
			flag = flag && this.shouldStopDelayTimer > 0.1f;
			this.lastShouldStopResult = flag;
			this.lastShouldStopDestination = jobResult.data[index].agentDestination;
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0005D0C4 File Offset: 0x0005B2C4
		public RVODestinationCrowdedBehavior(bool enabled, float densityFraction, bool returnAfterBeingPushedAway)
		{
			this.wasEnabled = enabled;
			this.enabled = enabled;
			this.densityThreshold = densityFraction;
			this.returnAfterBeingPushedAway = returnAfterBeingPushedAway;
			this.lastJobDensityResult = false;
			this.progressAverage = 0f;
			this.wasStopped = false;
			this.lastShouldStopDestination = new Vector3(float.NaN, float.NaN, float.NaN);
			this.reachedDestinationPoint = new Vector3(float.NaN, float.NaN, float.NaN);
			this.timer1 = 0f;
			this.shouldStopDelayTimer = 0f;
			this.reachedDestination = false;
			this.lastShouldStopResult = false;
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0005D160 File Offset: 0x0005B360
		public void ClearDestinationReached()
		{
			this.wasStopped = false;
			this.progressAverage = 1f;
			this.reachedDestination = false;
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x0005D17B File Offset: 0x0005B37B
		public void OnDestinationChanged(Vector3 newDestination, bool reachedDestination)
		{
			this.timer1 = float.PositiveInfinity;
			this.reachedDestination = reachedDestination;
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000F29 RID: 3881 RVA: 0x0005D18F File Offset: 0x0005B38F
		// (set) Token: 0x06000F2A RID: 3882 RVA: 0x0005D197 File Offset: 0x0005B397
		public bool reachedDestination { readonly get; private set; }

		// Token: 0x06000F2B RID: 3883 RVA: 0x0005D1A0 File Offset: 0x0005B3A0
		public void Update(bool rvoControllerEnabled, bool reachedDestination, ref bool isStopped, ref float rvoPriorityMultiplier, ref float rvoFlowFollowingStrength, Vector3 agentPosition)
		{
			if (!this.enabled || !rvoControllerEnabled)
			{
				if (this.wasEnabled)
				{
					this.wasEnabled = false;
					rvoPriorityMultiplier = 1f;
					rvoFlowFollowingStrength = 0f;
					this.timer1 = float.PositiveInfinity;
					this.progressAverage = 1f;
				}
				return;
			}
			this.wasEnabled = true;
			if (reachedDestination)
			{
				float sqrMagnitude = (agentPosition - this.reachedDestinationPoint).sqrMagnitude;
				if ((this.lastShouldStopDestination - this.reachedDestinationPoint).sqrMagnitude > sqrMagnitude)
				{
					this.reachedDestination = false;
				}
			}
			if (reachedDestination || this.lastShouldStopResult)
			{
				this.timer1 = 0f;
				this.reachedDestination = true;
				this.reachedDestinationPoint = this.lastShouldStopDestination;
				rvoPriorityMultiplier = Mathf.Lerp(rvoPriorityMultiplier, 0.1f, Time.deltaTime * 2f);
				rvoFlowFollowingStrength = Mathf.Lerp(rvoFlowFollowingStrength, 1f, Time.deltaTime * 4f);
				this.wasStopped |= math.abs(this.progressAverage) < 0.1f;
				isStopped |= this.wasStopped;
				return;
			}
			if (isStopped)
			{
				this.timer1 = 0f;
				this.reachedDestination = false;
				rvoPriorityMultiplier = Mathf.Lerp(rvoPriorityMultiplier, 0.1f, Time.deltaTime * 2f);
				rvoFlowFollowingStrength = Mathf.Lerp(rvoFlowFollowingStrength, 1f, Time.deltaTime * 4f);
				this.wasStopped |= math.abs(this.progressAverage) < 0.1f;
				return;
			}
			if (!this.reachedDestination)
			{
				rvoPriorityMultiplier = Mathf.Lerp(rvoPriorityMultiplier, 1f, Time.deltaTime * 4f);
				rvoFlowFollowingStrength = 0f;
				isStopped = false;
				this.wasStopped = false;
				return;
			}
			this.timer1 += Time.deltaTime;
			if (this.timer1 > 3f && this.returnAfterBeingPushedAway)
			{
				rvoPriorityMultiplier = Mathf.Lerp(rvoPriorityMultiplier, 0.5f, Time.deltaTime * 2f);
				rvoFlowFollowingStrength = 0f;
				isStopped = false;
				this.wasStopped = false;
				return;
			}
			rvoPriorityMultiplier = Mathf.Lerp(rvoPriorityMultiplier, 0.1f, Time.deltaTime * 2f);
			rvoFlowFollowingStrength = Mathf.Lerp(rvoFlowFollowingStrength, 1f, Time.deltaTime * 4f);
			this.wasStopped |= math.abs(this.progressAverage) < 0.1f;
			isStopped = this.wasStopped;
		}

		// Token: 0x04000B4C RID: 2892
		public bool enabled;

		// Token: 0x04000B4D RID: 2893
		[Range(0f, 1f)]
		public float densityThreshold;

		// Token: 0x04000B4E RID: 2894
		public bool returnAfterBeingPushedAway;

		// Token: 0x04000B4F RID: 2895
		public float progressAverage;

		// Token: 0x04000B50 RID: 2896
		private bool wasEnabled;

		// Token: 0x04000B51 RID: 2897
		private float timer1;

		// Token: 0x04000B52 RID: 2898
		private float shouldStopDelayTimer;

		// Token: 0x04000B53 RID: 2899
		private bool lastShouldStopResult;

		// Token: 0x04000B54 RID: 2900
		private Vector3 lastShouldStopDestination;

		// Token: 0x04000B55 RID: 2901
		private Vector3 reachedDestinationPoint;

		// Token: 0x04000B56 RID: 2902
		public bool lastJobDensityResult;

		// Token: 0x04000B57 RID: 2903
		private const float MaximumCirclePackingDensity = 0.9069f;

		// Token: 0x04000B59 RID: 2905
		private bool wasStopped;

		// Token: 0x04000B5A RID: 2906
		private const float DefaultPriority = 1f;

		// Token: 0x04000B5B RID: 2907
		private const float StoppedPriority = 0.1f;

		// Token: 0x04000B5C RID: 2908
		private const float MoveBackPriority = 0.5f;

		// Token: 0x0200027E RID: 638
		[BurstCompile(CompileSynchronously = false, FloatMode = FloatMode.Fast)]
		public struct JobDensityCheck : IJobParallelForBatched
		{
			// Token: 0x1700020A RID: 522
			// (get) Token: 0x06000F2C RID: 3884 RVA: 0x00018013 File Offset: 0x00016213
			public bool allowBoundsChecks
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000F2D RID: 3885 RVA: 0x0005D414 File Offset: 0x0005B614
			public JobDensityCheck(int size, float deltaTime)
			{
				SimulatorBurst simulator = RVOSimulator.active.GetSimulator();
				this.agentPosition = simulator.simulationData.position;
				this.agentTargetPoint = simulator.simulationData.targetPoint;
				this.agentRadius = simulator.simulationData.radius;
				this.agentDesiredSpeed = simulator.simulationData.desiredSpeed;
				this.agentOutputTargetPoint = simulator.outputData.targetPoint;
				this.agentOutputSpeed = simulator.outputData.speed;
				this.quadtree = simulator.quadtree;
				this.data = new NativeArray<RVODestinationCrowdedBehavior.JobDensityCheck.QueryData>(size, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
				this.outThresholdResult = new NativeArray<bool>(size, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
				this.progressAverage = new NativeArray<float>(size, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
				this.deltaTime = deltaTime;
			}

			// Token: 0x06000F2E RID: 3886 RVA: 0x0005D4CF File Offset: 0x0005B6CF
			public void Dispose()
			{
				this.data.Dispose();
				this.outThresholdResult.Dispose();
				this.progressAverage.Dispose();
			}

			// Token: 0x06000F2F RID: 3887 RVA: 0x0005D4F4 File Offset: 0x0005B6F4
			public void Set(int index, int rvoAgentIndex, float3 destination, float densityThreshold, float progressAverage)
			{
				this.data[index] = new RVODestinationCrowdedBehavior.JobDensityCheck.QueryData
				{
					agentDestination = destination,
					densityThreshold = densityThreshold,
					agentIndex = rvoAgentIndex
				};
				this.progressAverage[index] = progressAverage;
			}

			// Token: 0x06000F30 RID: 3888 RVA: 0x0005D540 File Offset: 0x0005B740
			void IJobParallelForBatched.Execute(int start, int count)
			{
				for (int i = start; i < start + count; i++)
				{
					this.Execute(i);
				}
			}

			// Token: 0x06000F31 RID: 3889 RVA: 0x0005D562 File Offset: 0x0005B762
			private float AgentDensityInCircle(float3 position, float radius)
			{
				return this.quadtree.QueryArea(position, radius) / (radius * radius * 3.1415927f);
			}

			// Token: 0x06000F32 RID: 3890 RVA: 0x0005D57C File Offset: 0x0005B77C
			private void Execute(int i)
			{
				RVODestinationCrowdedBehavior.JobDensityCheck.QueryData queryData = this.data[i];
				float3 @float = this.agentPosition[queryData.agentIndex];
				float num = this.agentRadius[queryData.agentIndex];
				float3 float2 = math.normalizesafe(this.agentTargetPoint[queryData.agentIndex] - @float, default(float3));
				float num2;
				if (this.agentDesiredSpeed[queryData.agentIndex] > 0.01f)
				{
					num2 = math.dot(float2, math.normalizesafe(this.agentOutputTargetPoint[queryData.agentIndex] - @float, default(float3)) * this.agentOutputSpeed[queryData.agentIndex]) / math.max(0.001f, math.min(this.agentDesiredSpeed[queryData.agentIndex], this.agentRadius[queryData.agentIndex]));
					num2 = math.clamp(num2, -1f, 1f);
				}
				else
				{
					num2 = 1f;
				}
				this.progressAverage[i] = math.lerp(this.progressAverage[i], num2, 2f * this.deltaTime);
				if (math.any(math.isinf(queryData.agentDestination)))
				{
					this.outThresholdResult[i] = true;
					return;
				}
				float num3 = math.length(queryData.agentDestination - @float);
				float num4 = num * 5f;
				if (num3 > num4 && this.AgentDensityInCircle(queryData.agentDestination, num4) < 0.9069f * queryData.densityThreshold)
				{
					this.outThresholdResult[i] = false;
					return;
				}
				this.outThresholdResult[i] = this.AgentDensityInCircle(queryData.agentDestination, num3) > 0.9069f * queryData.densityThreshold;
			}

			// Token: 0x04000B5D RID: 2909
			[ReadOnly]
			private RVOQuadtreeBurst quadtree;

			// Token: 0x04000B5E RID: 2910
			[ReadOnly]
			public NativeArray<RVODestinationCrowdedBehavior.JobDensityCheck.QueryData> data;

			// Token: 0x04000B5F RID: 2911
			[ReadOnly]
			public NativeArray<float3> agentPosition;

			// Token: 0x04000B60 RID: 2912
			[ReadOnly]
			private NativeArray<float3> agentTargetPoint;

			// Token: 0x04000B61 RID: 2913
			[ReadOnly]
			private NativeArray<float> agentRadius;

			// Token: 0x04000B62 RID: 2914
			[ReadOnly]
			private NativeArray<float> agentDesiredSpeed;

			// Token: 0x04000B63 RID: 2915
			[ReadOnly]
			private NativeArray<float3> agentOutputTargetPoint;

			// Token: 0x04000B64 RID: 2916
			[ReadOnly]
			private NativeArray<float> agentOutputSpeed;

			// Token: 0x04000B65 RID: 2917
			[WriteOnly]
			public NativeArray<bool> outThresholdResult;

			// Token: 0x04000B66 RID: 2918
			public NativeArray<float> progressAverage;

			// Token: 0x04000B67 RID: 2919
			public float deltaTime;

			// Token: 0x0200027F RID: 639
			public struct QueryData
			{
				// Token: 0x04000B68 RID: 2920
				public float3 agentDestination;

				// Token: 0x04000B69 RID: 2921
				public int agentIndex;

				// Token: 0x04000B6A RID: 2922
				public float densityThreshold;
			}
		}
	}
}
