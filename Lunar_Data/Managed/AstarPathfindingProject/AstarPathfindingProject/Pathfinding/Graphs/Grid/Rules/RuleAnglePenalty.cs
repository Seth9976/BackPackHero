using System;
using Pathfinding.Jobs;
using Pathfinding.Util;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.Graphs.Grid.Rules
{
	// Token: 0x0200020B RID: 523
	[Preserve]
	public class RuleAnglePenalty : GridGraphRule
	{
		// Token: 0x06000CC5 RID: 3269 RVA: 0x0004FA14 File Offset: 0x0004DC14
		public override void Register(GridGraphRules rules)
		{
			if (!this.angleToPenalty.IsCreated)
			{
				this.angleToPenalty = new NativeArray<float>(32, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			}
			for (int i = 0; i < this.angleToPenalty.Length; i++)
			{
				this.angleToPenalty[i] = Mathf.Max(0f, this.curve.Evaluate(90f * (float)i / (float)(this.angleToPenalty.Length - 1)) * this.penaltyScale);
			}
			rules.AddJobSystemPass(GridGraphRule.Pass.BeforeConnections, delegate(GridGraphRules.Context context)
			{
				new RuleAnglePenalty.JobPenaltyAngle
				{
					angleToPenalty = this.angleToPenalty,
					up = context.data.up,
					nodeNormals = context.data.nodes.normals,
					penalty = context.data.nodes.penalties
				}.Schedule(context.tracker);
			});
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0004FAA5 File Offset: 0x0004DCA5
		public override void DisposeUnmanagedData()
		{
			if (this.angleToPenalty.IsCreated)
			{
				this.angleToPenalty.Dispose();
			}
		}

		// Token: 0x0400098A RID: 2442
		public float penaltyScale = 10000f;

		// Token: 0x0400098B RID: 2443
		public AnimationCurve curve = AnimationCurve.Linear(0f, 0f, 90f, 1f);

		// Token: 0x0400098C RID: 2444
		private NativeArray<float> angleToPenalty;

		// Token: 0x0200020C RID: 524
		[BurstCompile(FloatMode = FloatMode.Fast)]
		public struct JobPenaltyAngle : IJob
		{
			// Token: 0x06000CC9 RID: 3273 RVA: 0x0004FB64 File Offset: 0x0004DD64
			public void Execute()
			{
				float4 @float = new float4(this.up.x, this.up.y, this.up.z, 0f);
				for (int i = 0; i < this.penalty.Length; i++)
				{
					float4 float2 = this.nodeNormals[i];
					if (math.any(float2))
					{
						float num = math.acos(math.dot(float2, @float)) * (float)(this.angleToPenalty.Length - 1) / 3.1415927f;
						int num2 = (int)num;
						float num3 = this.angleToPenalty[math.max(num2, 0)];
						float num4 = this.angleToPenalty[math.min(num2 + 1, this.angleToPenalty.Length - 1)];
						ref NativeArray<uint> ptr = ref this.penalty;
						int num5 = i;
						ptr[num5] += (uint)math.lerp(num3, num4, num - (float)num2);
					}
				}
			}

			// Token: 0x0400098D RID: 2445
			public Vector3 up;

			// Token: 0x0400098E RID: 2446
			[ReadOnly]
			public NativeArray<float> angleToPenalty;

			// Token: 0x0400098F RID: 2447
			[ReadOnly]
			public NativeArray<float4> nodeNormals;

			// Token: 0x04000990 RID: 2448
			public NativeArray<uint> penalty;
		}
	}
}
