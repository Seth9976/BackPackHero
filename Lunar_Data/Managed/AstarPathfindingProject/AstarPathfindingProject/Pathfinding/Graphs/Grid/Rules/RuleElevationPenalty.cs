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
	// Token: 0x0200020D RID: 525
	[Preserve]
	public class RuleElevationPenalty : GridGraphRule
	{
		// Token: 0x06000CCA RID: 3274 RVA: 0x0004FC60 File Offset: 0x0004DE60
		public override void Register(GridGraphRules rules)
		{
			if (!this.elevationToPenalty.IsCreated)
			{
				this.elevationToPenalty = new NativeArray<float>(64, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			}
			for (int i = 0; i < this.elevationToPenalty.Length; i++)
			{
				this.elevationToPenalty[i] = Mathf.Max(0f, this.penaltyScale * this.curve.Evaluate((float)i * 1f / (float)(this.elevationToPenalty.Length - 1)));
			}
			Vector2 clampedElevationRange = new Vector2(math.max(0f, this.elevationRange.x), math.max(1f, this.elevationRange.y));
			rules.AddJobSystemPass(GridGraphRule.Pass.BeforeConnections, delegate(GridGraphRules.Context context)
			{
				Matrix4x4 matrix4x = Matrix4x4.Scale(new Vector3(1f, 1f / (clampedElevationRange.y - clampedElevationRange.x), 1f)) * Matrix4x4.Translate(new Vector3(0f, -clampedElevationRange.x, 0f));
				new RuleElevationPenalty.JobElevationPenalty
				{
					elevationToPenalty = this.elevationToPenalty,
					nodePositions = context.data.nodes.positions,
					worldToGraph = matrix4x * context.data.transform.matrix.inverse,
					penalty = context.data.nodes.penalties
				}.Schedule(context.tracker);
			});
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0004FD33 File Offset: 0x0004DF33
		public override void DisposeUnmanagedData()
		{
			if (this.elevationToPenalty.IsCreated)
			{
				this.elevationToPenalty.Dispose();
			}
		}

		// Token: 0x04000991 RID: 2449
		public float penaltyScale = 10000f;

		// Token: 0x04000992 RID: 2450
		public Vector2 elevationRange = new Vector2(0f, 100f);

		// Token: 0x04000993 RID: 2451
		public AnimationCurve curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

		// Token: 0x04000994 RID: 2452
		private NativeArray<float> elevationToPenalty;

		// Token: 0x0200020E RID: 526
		[BurstCompile(FloatMode = FloatMode.Fast)]
		public struct JobElevationPenalty : IJob
		{
			// Token: 0x06000CCD RID: 3277 RVA: 0x0004FDA4 File Offset: 0x0004DFA4
			public void Execute()
			{
				for (int i = 0; i < this.penalty.Length; i++)
				{
					float num = math.clamp(this.worldToGraph.MultiplyPoint3x4(this.nodePositions[i]).y, 0f, 1f) * (float)(this.elevationToPenalty.Length - 1);
					int num2 = (int)num;
					float num3 = this.elevationToPenalty[num2];
					float num4 = this.elevationToPenalty[math.min(num2 + 1, this.elevationToPenalty.Length - 1)];
					ref NativeArray<uint> ptr = ref this.penalty;
					int num5 = i;
					ptr[num5] += (uint)math.lerp(num3, num4, num - (float)num2);
				}
			}

			// Token: 0x04000995 RID: 2453
			[ReadOnly]
			public NativeArray<float> elevationToPenalty;

			// Token: 0x04000996 RID: 2454
			[ReadOnly]
			public NativeArray<Vector3> nodePositions;

			// Token: 0x04000997 RID: 2455
			public Matrix4x4 worldToGraph;

			// Token: 0x04000998 RID: 2456
			public NativeArray<uint> penalty;
		}
	}
}
