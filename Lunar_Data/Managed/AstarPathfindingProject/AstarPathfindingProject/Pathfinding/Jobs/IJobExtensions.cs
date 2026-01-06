using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Jobs;

namespace Pathfinding.Jobs
{
	// Token: 0x02000185 RID: 389
	public static class IJobExtensions
	{
		// Token: 0x06000AB6 RID: 2742 RVA: 0x0003C7B0 File Offset: 0x0003A9B0
		public static JobHandle Schedule<T>(this T data, JobDependencyTracker tracker) where T : struct, IJob
		{
			if (tracker.forceLinearDependencies)
			{
				data.Run<T>();
				return default(JobHandle);
			}
			JobHandle jobHandle = data.Schedule(JobDependencyAnalyzer<T>.GetDependencies(ref data, tracker));
			JobDependencyAnalyzer<T>.Scheduled(ref data, tracker, jobHandle);
			return jobHandle;
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0003C7F0 File Offset: 0x0003A9F0
		public static JobHandle ScheduleBatch<T>(this T data, int arrayLength, int minIndicesPerJobCount, JobDependencyTracker tracker, JobHandle additionalDependency = default(JobHandle)) where T : struct, IJobParallelForBatched
		{
			if (tracker.forceLinearDependencies)
			{
				additionalDependency.Complete();
				data.RunBatch(arrayLength);
				return default(JobHandle);
			}
			JobHandle jobHandle = data.ScheduleBatch(arrayLength, minIndicesPerJobCount, JobDependencyAnalyzer<T>.GetDependencies(ref data, tracker, additionalDependency));
			JobDependencyAnalyzer<T>.Scheduled(ref data, tracker, jobHandle);
			return jobHandle;
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0003C83C File Offset: 0x0003AA3C
		public static JobHandle ScheduleManaged<T>(this T data, JobHandle dependsOn) where T : struct, IJob
		{
			return new IJobExtensions.ManagedJob
			{
				handle = GCHandle.Alloc(data)
			}.Schedule(dependsOn);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0003C86C File Offset: 0x0003AA6C
		public static JobHandle ScheduleManaged(this Action data, JobHandle dependsOn)
		{
			return new IJobExtensions.ManagedActionJob
			{
				handle = GCHandle.Alloc(data)
			}.Schedule(dependsOn);
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0003C898 File Offset: 0x0003AA98
		public static JobHandle GetDependencies<T>(this T data, JobDependencyTracker tracker) where T : struct, IJob
		{
			if (tracker.forceLinearDependencies)
			{
				return default(JobHandle);
			}
			return JobDependencyAnalyzer<T>.GetDependencies(ref data, tracker);
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0003C8BF File Offset: 0x0003AABF
		public static IEnumerator<JobHandle> ExecuteMainThreadJob<T>(this T data, JobDependencyTracker tracker) where T : struct, IJobTimeSliced
		{
			if (tracker.forceLinearDependencies)
			{
				data.Execute();
				yield break;
			}
			JobHandle dependencies = JobDependencyAnalyzer<T>.GetDependencies(ref data, tracker);
			yield return dependencies;
			while (!data.Execute(tracker.timeSlice))
			{
				JobHandle jobHandle = default(JobHandle);
			}
			yield break;
			yield break;
		}

		// Token: 0x02000186 RID: 390
		private struct ManagedJob : IJob
		{
			// Token: 0x06000ABC RID: 2748 RVA: 0x0003C8D5 File Offset: 0x0003AAD5
			public void Execute()
			{
				((IJob)this.handle.Target).Execute();
				this.handle.Free();
			}

			// Token: 0x0400074B RID: 1867
			public GCHandle handle;
		}

		// Token: 0x02000187 RID: 391
		private struct ManagedActionJob : IJob
		{
			// Token: 0x06000ABD RID: 2749 RVA: 0x0003C8F7 File Offset: 0x0003AAF7
			public void Execute()
			{
				((Action)this.handle.Target)();
				this.handle.Free();
			}

			// Token: 0x0400074C RID: 1868
			public GCHandle handle;
		}
	}
}
