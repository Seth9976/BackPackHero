using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.Jobs
{
	// Token: 0x02000167 RID: 359
	public struct JobRaycastAll
	{
		// Token: 0x06000A62 RID: 2658 RVA: 0x0003B424 File Offset: 0x00039624
		public JobRaycastAll(NativeArray<RaycastCommand> commands, NativeArray<RaycastHit> results, PhysicsScene physicsScene, int maxHits, Allocator allocator, JobDependencyTracker dependencyTracker, float minStep = 0.0001f)
		{
			if (maxHits <= 0)
			{
				throw new ArgumentException("maxHits should be greater than zero");
			}
			if (results.Length < commands.Length * maxHits)
			{
				throw new ArgumentException("Results array length does not match maxHits count");
			}
			if (minStep < 0f)
			{
				throw new ArgumentException("minStep should be more or equal to zero");
			}
			this.results = results;
			this.maxHits = maxHits;
			this.minStep = minStep;
			this.commands = commands;
			this.physicsScene = physicsScene;
			this.semiResults = dependencyTracker.NewNativeArray<RaycastHit>(maxHits * commands.Length, allocator, NativeArrayOptions.ClearMemory);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0003B4B4 File Offset: 0x000396B4
		public JobHandle Schedule(JobHandle dependency)
		{
			for (int i = 0; i < this.maxHits; i++)
			{
				NativeArray<RaycastHit> subArray = this.semiResults.GetSubArray(i * this.commands.Length, this.commands.Length);
				dependency = RaycastCommand.ScheduleBatch(this.commands, subArray, 128, dependency);
				if (i < this.maxHits - 1)
				{
					dependency = new JobRaycastAll.JobCreateCommands
					{
						commands = this.commands,
						raycastHits = subArray,
						minStep = this.minStep,
						physicsScene = this.physicsScene
					}.Schedule(this.commands.Length, 256, dependency);
				}
			}
			return new JobRaycastAll.JobCombineResults
			{
				semiResults = this.semiResults,
				maxHits = this.maxHits,
				results = this.results
			}.Schedule(dependency);
		}

		// Token: 0x04000700 RID: 1792
		private int maxHits;

		// Token: 0x04000701 RID: 1793
		public readonly float minStep;

		// Token: 0x04000702 RID: 1794
		private NativeArray<RaycastHit> results;

		// Token: 0x04000703 RID: 1795
		private NativeArray<RaycastHit> semiResults;

		// Token: 0x04000704 RID: 1796
		private NativeArray<RaycastCommand> commands;

		// Token: 0x04000705 RID: 1797
		public PhysicsScene physicsScene;

		// Token: 0x02000168 RID: 360
		[BurstCompile]
		private struct JobCreateCommands : IJobParallelFor
		{
			// Token: 0x06000A64 RID: 2660 RVA: 0x0003B5A4 File Offset: 0x000397A4
			public void Execute(int index)
			{
				RaycastHit raycastHit = this.raycastHits[index];
				if (raycastHit.normal != default(Vector3))
				{
					RaycastCommand raycastCommand = this.commands[index];
					Vector3 vector = raycastHit.point + raycastCommand.direction.normalized * this.minStep;
					float num = raycastCommand.distance - (vector - raycastCommand.from).magnitude;
					this.commands[index] = new RaycastCommand(vector, raycastCommand.direction, num, raycastCommand.layerMask, 1);
					return;
				}
				this.commands[index] = new RaycastCommand(Vector3.zero, Vector3.up, 1f, 0, 1);
			}

			// Token: 0x04000706 RID: 1798
			public NativeArray<RaycastCommand> commands;

			// Token: 0x04000707 RID: 1799
			[ReadOnly]
			public NativeArray<RaycastHit> raycastHits;

			// Token: 0x04000708 RID: 1800
			public float minStep;

			// Token: 0x04000709 RID: 1801
			public PhysicsScene physicsScene;
		}

		// Token: 0x02000169 RID: 361
		[BurstCompile]
		private struct JobCombineResults : IJob
		{
			// Token: 0x06000A65 RID: 2661 RVA: 0x0003B670 File Offset: 0x00039870
			public void Execute()
			{
				int num = this.semiResults.Length / this.maxHits;
				for (int i = 0; i < num; i++)
				{
					int num2 = 0;
					for (int j = this.maxHits - 1; j >= 0; j--)
					{
						if (math.any(this.semiResults[i + j * num].normal))
						{
							this.results[i + num2] = this.semiResults[i + j * num];
							num2 += num;
						}
					}
				}
			}

			// Token: 0x0400070A RID: 1802
			public int maxHits;

			// Token: 0x0400070B RID: 1803
			[ReadOnly]
			public NativeArray<RaycastHit> semiResults;

			// Token: 0x0400070C RID: 1804
			public NativeArray<RaycastHit> results;
		}
	}
}
