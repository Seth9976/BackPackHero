using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Jobs.LowLevel.Unsafe
{
	// Token: 0x0200006B RID: 107
	[NativeHeader("Runtime/Jobs/JobSystem.h")]
	[NativeType(Header = "Runtime/Jobs/ScriptBindings/JobsBindings.h")]
	public static class JobsUtility
	{
		// Token: 0x0600018E RID: 398 RVA: 0x0000376C File Offset: 0x0000196C
		public unsafe static void GetJobRange(ref JobRanges ranges, int jobIndex, out int beginIndex, out int endIndex)
		{
			int* ptr = (int*)(void*)ranges.StartEndIndex;
			beginIndex = ptr[jobIndex * 2];
			endIndex = ptr[jobIndex * 2 + 1];
		}

		// Token: 0x0600018F RID: 399
		[NativeMethod(IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool GetWorkStealingRange(ref JobRanges ranges, int jobIndex, out int beginIndex, out int endIndex);

		// Token: 0x06000190 RID: 400 RVA: 0x000037A0 File Offset: 0x000019A0
		[FreeFunction("ScheduleManagedJob", ThrowsException = true, IsThreadSafe = true)]
		public static JobHandle Schedule(ref JobsUtility.JobScheduleParameters parameters)
		{
			JobHandle jobHandle;
			JobsUtility.Schedule_Injected(ref parameters, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000037B8 File Offset: 0x000019B8
		[FreeFunction("ScheduleManagedJobParallelFor", ThrowsException = true, IsThreadSafe = true)]
		public static JobHandle ScheduleParallelFor(ref JobsUtility.JobScheduleParameters parameters, int arrayLength, int innerloopBatchCount)
		{
			JobHandle jobHandle;
			JobsUtility.ScheduleParallelFor_Injected(ref parameters, arrayLength, innerloopBatchCount, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000037D0 File Offset: 0x000019D0
		[FreeFunction("ScheduleManagedJobParallelForDeferArraySize", ThrowsException = true, IsThreadSafe = true)]
		public unsafe static JobHandle ScheduleParallelForDeferArraySize(ref JobsUtility.JobScheduleParameters parameters, int innerloopBatchCount, void* listData, void* listDataAtomicSafetyHandle)
		{
			JobHandle jobHandle;
			JobsUtility.ScheduleParallelForDeferArraySize_Injected(ref parameters, innerloopBatchCount, listData, listDataAtomicSafetyHandle, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000037EC File Offset: 0x000019EC
		[FreeFunction("ScheduleManagedJobParallelForTransform", ThrowsException = true)]
		public static JobHandle ScheduleParallelForTransform(ref JobsUtility.JobScheduleParameters parameters, IntPtr transfromAccesssArray)
		{
			JobHandle jobHandle;
			JobsUtility.ScheduleParallelForTransform_Injected(ref parameters, transfromAccesssArray, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00003804 File Offset: 0x00001A04
		[FreeFunction("ScheduleManagedJobParallelForTransformReadOnly", ThrowsException = true)]
		public static JobHandle ScheduleParallelForTransformReadOnly(ref JobsUtility.JobScheduleParameters parameters, IntPtr transfromAccesssArray, int innerloopBatchCount)
		{
			JobHandle jobHandle;
			JobsUtility.ScheduleParallelForTransformReadOnly_Injected(ref parameters, transfromAccesssArray, innerloopBatchCount, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06000195 RID: 405
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[NativeMethod(IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(4096)]
		public unsafe static extern void PatchBufferMinMaxRanges(IntPtr bufferRangePatchData, void* jobdata, int startIndex, int rangeSize);

		// Token: 0x06000196 RID: 406
		[FreeFunction(ThrowsException = true, IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern IntPtr CreateJobReflectionData(Type wrapperJobType, Type userJobType, object managedJobFunction0, object managedJobFunction1, object managedJobFunction2);

		// Token: 0x06000197 RID: 407 RVA: 0x0000381C File Offset: 0x00001A1C
		[Obsolete("JobType is obsolete. The parameter should be removed. (UnityUpgradable) -> !1")]
		public static IntPtr CreateJobReflectionData(Type type, JobType jobType, object managedJobFunction0, object managedJobFunction1 = null, object managedJobFunction2 = null)
		{
			return JobsUtility.CreateJobReflectionData(type, type, managedJobFunction0, managedJobFunction1, managedJobFunction2);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000383C File Offset: 0x00001A3C
		public static IntPtr CreateJobReflectionData(Type type, object managedJobFunction0, object managedJobFunction1 = null, object managedJobFunction2 = null)
		{
			return JobsUtility.CreateJobReflectionData(type, type, managedJobFunction0, managedJobFunction1, managedJobFunction2);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00003858 File Offset: 0x00001A58
		[Obsolete("JobType is obsolete. The parameter should be removed. (UnityUpgradable) -> !2")]
		public static IntPtr CreateJobReflectionData(Type wrapperJobType, Type userJobType, JobType jobType, object managedJobFunction0)
		{
			return JobsUtility.CreateJobReflectionData(wrapperJobType, userJobType, managedJobFunction0, null, null);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00003874 File Offset: 0x00001A74
		public static IntPtr CreateJobReflectionData(Type wrapperJobType, Type userJobType, object managedJobFunction0)
		{
			return JobsUtility.CreateJobReflectionData(wrapperJobType, userJobType, managedJobFunction0, null, null);
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600019B RID: 411
		public static extern bool IsExecutingJob
		{
			[NativeMethod(IsFreeFunction = true, IsThreadSafe = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600019C RID: 412
		// (set) Token: 0x0600019D RID: 413
		public static extern bool JobDebuggerEnabled
		{
			[FreeFunction]
			[MethodImpl(4096)]
			get;
			[FreeFunction]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600019E RID: 414
		// (set) Token: 0x0600019F RID: 415
		public static extern bool JobCompilerEnabled
		{
			[FreeFunction]
			[MethodImpl(4096)]
			get;
			[FreeFunction]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060001A0 RID: 416
		[FreeFunction("JobSystem::GetJobQueueWorkerThreadCount")]
		[MethodImpl(4096)]
		private static extern int GetJobQueueWorkerThreadCount();

		// Token: 0x060001A1 RID: 417
		[FreeFunction("JobSystem::ForceSetJobQueueWorkerThreadCount")]
		[MethodImpl(4096)]
		private static extern void SetJobQueueMaximumActiveThreadCount(int count);

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001A2 RID: 418
		public static extern int JobWorkerMaximumCount
		{
			[FreeFunction("JobSystem::GetJobQueueMaximumThreadCount")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060001A3 RID: 419
		[FreeFunction("JobSystem::ResetJobQueueWorkerThreadCount")]
		[MethodImpl(4096)]
		public static extern void ResetJobWorkerCount();

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00003890 File Offset: 0x00001A90
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x000038A8 File Offset: 0x00001AA8
		public static int JobWorkerCount
		{
			get
			{
				return JobsUtility.GetJobQueueWorkerThreadCount();
			}
			set
			{
				bool flag = value < 0 || value > JobsUtility.JobWorkerMaximumCount;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("JobWorkerCount", string.Format("Invalid JobWorkerCount {0} must be in the range 0 -> {1}", value, JobsUtility.JobWorkerMaximumCount));
				}
				JobsUtility.SetJobQueueMaximumActiveThreadCount(value);
			}
		}

		// Token: 0x060001A6 RID: 422
		[FreeFunction("JobDebuggerGetSystemIdCellPtr")]
		[MethodImpl(4096)]
		internal static extern IntPtr GetSystemIdCellPtr();

		// Token: 0x060001A7 RID: 423
		[FreeFunction("JobDebuggerClearSystemIds")]
		[MethodImpl(4096)]
		internal static extern void ClearSystemIds();

		// Token: 0x060001A8 RID: 424
		[FreeFunction("JobDebuggerGetSystemIdMappings")]
		[MethodImpl(4096)]
		internal unsafe static extern int GetSystemIdMappings(JobHandle* handles, int* systemIds, int maxCount);

		// Token: 0x060001A9 RID: 425 RVA: 0x000038F8 File Offset: 0x00001AF8
		[RequiredByNativeCode]
		private static void InvokePanicFunction()
		{
			JobsUtility.PanicFunction_ panicFunction = JobsUtility.PanicFunction;
			bool flag = panicFunction == null;
			if (!flag)
			{
				panicFunction();
			}
		}

		// Token: 0x060001AA RID: 426
		[MethodImpl(4096)]
		private static extern void Schedule_Injected(ref JobsUtility.JobScheduleParameters parameters, out JobHandle ret);

		// Token: 0x060001AB RID: 427
		[MethodImpl(4096)]
		private static extern void ScheduleParallelFor_Injected(ref JobsUtility.JobScheduleParameters parameters, int arrayLength, int innerloopBatchCount, out JobHandle ret);

		// Token: 0x060001AC RID: 428
		[MethodImpl(4096)]
		private unsafe static extern void ScheduleParallelForDeferArraySize_Injected(ref JobsUtility.JobScheduleParameters parameters, int innerloopBatchCount, void* listData, void* listDataAtomicSafetyHandle, out JobHandle ret);

		// Token: 0x060001AD RID: 429
		[MethodImpl(4096)]
		private static extern void ScheduleParallelForTransform_Injected(ref JobsUtility.JobScheduleParameters parameters, IntPtr transfromAccesssArray, out JobHandle ret);

		// Token: 0x060001AE RID: 430
		[MethodImpl(4096)]
		private static extern void ScheduleParallelForTransformReadOnly_Injected(ref JobsUtility.JobScheduleParameters parameters, IntPtr transfromAccesssArray, int innerloopBatchCount, out JobHandle ret);

		// Token: 0x04000191 RID: 401
		public const int MaxJobThreadCount = 128;

		// Token: 0x04000192 RID: 402
		public const int CacheLineSize = 64;

		// Token: 0x04000193 RID: 403
		internal static JobsUtility.PanicFunction_ PanicFunction;

		// Token: 0x0200006C RID: 108
		public struct JobScheduleParameters
		{
			// Token: 0x060001AF RID: 431 RVA: 0x0000391D File Offset: 0x00001B1D
			public unsafe JobScheduleParameters(void* i_jobData, IntPtr i_reflectionData, JobHandle i_dependency, ScheduleMode i_scheduleMode)
			{
				this.Dependency = i_dependency;
				this.JobDataPtr = (IntPtr)i_jobData;
				this.ReflectionData = i_reflectionData;
				this.ScheduleMode = (int)i_scheduleMode;
			}

			// Token: 0x04000194 RID: 404
			public JobHandle Dependency;

			// Token: 0x04000195 RID: 405
			public int ScheduleMode;

			// Token: 0x04000196 RID: 406
			public IntPtr ReflectionData;

			// Token: 0x04000197 RID: 407
			public IntPtr JobDataPtr;
		}

		// Token: 0x0200006D RID: 109
		// (Invoke) Token: 0x060001B1 RID: 433
		internal delegate void PanicFunction_();
	}
}
