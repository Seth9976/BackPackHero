using System;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;

namespace Pathfinding.Jobs
{
	// Token: 0x02000176 RID: 374
	public static class JobParallelForBatchedExtensions
	{
		// Token: 0x06000A85 RID: 2693 RVA: 0x0003BE70 File Offset: 0x0003A070
		public static JobHandle ScheduleBatch<T>(this T jobData, int arrayLength, int minIndicesPerJobCount, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParallelForBatched
		{
			ScheduleMode scheduleMode = ScheduleMode.Batched;
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), JobParallelForBatchedExtensions.ParallelForBatchJobStruct<T>.Initialize(), dependsOn, scheduleMode);
			return JobsUtility.ScheduleParallelFor(ref jobScheduleParameters, arrayLength, minIndicesPerJobCount);
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0003BEA0 File Offset: 0x0003A0A0
		public static void RunBatch<T>(this T jobData, int arrayLength) where T : struct, IJobParallelForBatched
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), JobParallelForBatchedExtensions.ParallelForBatchJobStruct<T>.Initialize(), default(JobHandle), ScheduleMode.Run);
			JobsUtility.ScheduleParallelFor(ref jobScheduleParameters, arrayLength, arrayLength);
		}

		// Token: 0x02000177 RID: 375
		internal struct ParallelForBatchJobStruct<T> where T : struct, IJobParallelForBatched
		{
			// Token: 0x06000A87 RID: 2695 RVA: 0x0003BED4 File Offset: 0x0003A0D4
			public static IntPtr Initialize()
			{
				if (JobParallelForBatchedExtensions.ParallelForBatchJobStruct<T>.jobReflectionData == IntPtr.Zero)
				{
					JobParallelForBatchedExtensions.ParallelForBatchJobStruct<T>.jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), new JobParallelForBatchedExtensions.ParallelForBatchJobStruct<T>.ExecuteJobFunction(JobParallelForBatchedExtensions.ParallelForBatchJobStruct<T>.Execute), null, null);
				}
				return JobParallelForBatchedExtensions.ParallelForBatchJobStruct<T>.jobReflectionData;
			}

			// Token: 0x06000A88 RID: 2696 RVA: 0x0003BF10 File Offset: 0x0003A110
			public static void Execute(ref T jobData, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
			{
				int num;
				int num2;
				while (JobsUtility.GetWorkStealingRange(ref ranges, jobIndex, out num, out num2))
				{
					jobData.Execute(num, num2 - num);
				}
			}

			// Token: 0x04000729 RID: 1833
			public static IntPtr jobReflectionData;

			// Token: 0x02000178 RID: 376
			// (Invoke) Token: 0x06000A8A RID: 2698
			public delegate void ExecuteJobFunction(ref T data, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);
		}
	}
}
