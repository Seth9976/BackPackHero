using System;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.Jobs
{
	// Token: 0x02000280 RID: 640
	public static class IJobParallelForTransformExtensions
	{
		// Token: 0x06001BD2 RID: 7122 RVA: 0x0002CBE8 File Offset: 0x0002ADE8
		public static JobHandle Schedule<T>(this T jobData, TransformAccessArray transforms, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParallelForTransform
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.Initialize(), dependsOn, ScheduleMode.Batched);
			return JobsUtility.ScheduleParallelForTransform(ref jobScheduleParameters, transforms.GetTransformAccessArrayForSchedule());
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x0002CC20 File Offset: 0x0002AE20
		public static JobHandle ScheduleReadOnly<T>(this T jobData, TransformAccessArray transforms, int batchSize, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParallelForTransform
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.Initialize(), dependsOn, ScheduleMode.Batched);
			return JobsUtility.ScheduleParallelForTransformReadOnly(ref jobScheduleParameters, transforms.GetTransformAccessArrayForSchedule(), batchSize);
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x0002CC58 File Offset: 0x0002AE58
		public static void RunReadOnly<T>(this T jobData, TransformAccessArray transforms) where T : struct, IJobParallelForTransform
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.Initialize(), default(JobHandle), ScheduleMode.Run);
			JobsUtility.ScheduleParallelForTransformReadOnly(ref jobScheduleParameters, transforms.GetTransformAccessArrayForSchedule(), transforms.length);
		}

		// Token: 0x02000281 RID: 641
		internal struct TransformParallelForLoopStruct<T> where T : struct, IJobParallelForTransform
		{
			// Token: 0x06001BD5 RID: 7125 RVA: 0x0002CC9C File Offset: 0x0002AE9C
			public static IntPtr Initialize()
			{
				bool flag = IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.jobReflectionData == IntPtr.Zero;
				if (flag)
				{
					IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), new IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.ExecuteJobFunction(IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.Execute), null, null);
				}
				return IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.jobReflectionData;
			}

			// Token: 0x06001BD6 RID: 7126 RVA: 0x0002CCE8 File Offset: 0x0002AEE8
			public unsafe static void Execute(ref T jobData, IntPtr jobData2, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
			{
				IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.TransformJobData transformJobData;
				UnsafeUtility.CopyPtrToStructure<IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.TransformJobData>((void*)jobData2, out transformJobData);
				int* ptr = (int*)(void*)TransformAccessArray.GetSortedToUserIndex(transformJobData.TransformAccessArray);
				TransformAccess* ptr2 = (TransformAccess*)(void*)TransformAccessArray.GetSortedTransformAccess(transformJobData.TransformAccessArray);
				bool flag = transformJobData.IsReadOnly == 1;
				if (flag)
				{
					for (;;)
					{
						int num;
						int num2;
						bool flag2 = !JobsUtility.GetWorkStealingRange(ref ranges, jobIndex, out num, out num2);
						if (flag2)
						{
							break;
						}
						int num3 = num2;
						for (int i = num; i < num3; i++)
						{
							int num4 = i;
							int num5 = ptr[num4];
							TransformAccess transformAccess = ptr2[num4];
							jobData.Execute(num5, transformAccess);
						}
					}
				}
				else
				{
					int num6;
					int num7;
					JobsUtility.GetJobRange(ref ranges, jobIndex, out num6, out num7);
					for (int j = num6; j < num7; j++)
					{
						int num8 = j;
						int num9 = ptr[num8];
						TransformAccess transformAccess2 = ptr2[num8];
						jobData.Execute(num9, transformAccess2);
					}
				}
			}

			// Token: 0x0400091C RID: 2332
			public static IntPtr jobReflectionData;

			// Token: 0x02000282 RID: 642
			private struct TransformJobData
			{
				// Token: 0x0400091D RID: 2333
				public IntPtr TransformAccessArray;

				// Token: 0x0400091E RID: 2334
				public int IsReadOnly;
			}

			// Token: 0x02000283 RID: 643
			// (Invoke) Token: 0x06001BD8 RID: 7128
			public delegate void ExecuteJobFunction(ref T jobData, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);
		}
	}
}
