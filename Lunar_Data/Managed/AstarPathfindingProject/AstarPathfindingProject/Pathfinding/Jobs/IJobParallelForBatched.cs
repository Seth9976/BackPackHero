using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace Pathfinding.Jobs
{
	// Token: 0x02000175 RID: 373
	[JobProducerType(typeof(JobParallelForBatchedExtensions.ParallelForBatchJobStruct<>))]
	public interface IJobParallelForBatched
	{
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000A83 RID: 2691
		bool allowBoundsChecks { get; }

		// Token: 0x06000A84 RID: 2692
		void Execute(int startIndex, int count);
	}
}
