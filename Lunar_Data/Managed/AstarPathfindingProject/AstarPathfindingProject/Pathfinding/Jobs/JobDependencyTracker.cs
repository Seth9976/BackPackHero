using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Pathfinding.Util;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;

namespace Pathfinding.Jobs
{
	// Token: 0x0200017F RID: 383
	public class JobDependencyTracker : IAstarPooledObject
	{
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x0003C3A3 File Offset: 0x0003A5A3
		public bool forceLinearDependencies
		{
			get
			{
				if (this.linearDependencies == LinearDependencies.Check)
				{
					this.SetLinearDependencies(false);
				}
				return this.linearDependencies == LinearDependencies.Enabled;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x0003C3C0 File Offset: 0x0003A5C0
		public JobHandle AllWritesDependency
		{
			get
			{
				NativeArray<JobHandle> nativeArray = new NativeArray<JobHandle>(this.slots.Count, Allocator.Temp, NativeArrayOptions.ClearMemory);
				for (int i = 0; i < this.slots.Count; i++)
				{
					nativeArray[i] = this.slots[i].lastWrite.handle;
				}
				JobHandle jobHandle = JobHandle.CombineDependencies(nativeArray);
				nativeArray.Dispose();
				return jobHandle;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x0003C422 File Offset: 0x0003A622
		private bool supportsMultithreading
		{
			get
			{
				return JobsUtility.JobWorkerCount > 0;
			}
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0003C42C File Offset: 0x0003A62C
		public void SetLinearDependencies(bool linearDependencies)
		{
			if (!this.supportsMultithreading)
			{
				linearDependencies = true;
			}
			if (linearDependencies)
			{
				this.AllWritesDependency.Complete();
			}
			this.linearDependencies = (linearDependencies ? LinearDependencies.Enabled : LinearDependencies.Disabled);
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0003C464 File Offset: 0x0003A664
		public NativeArray<T> NewNativeArray<[IsUnmanaged] T>(int length, Allocator allocator, NativeArrayOptions options = NativeArrayOptions.ClearMemory) where T : struct, ValueType
		{
			NativeArray<T> nativeArray = new NativeArray<T>(length, allocator, options);
			this.Track<T>(nativeArray, options == NativeArrayOptions.ClearMemory);
			return nativeArray;
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0003C488 File Offset: 0x0003A688
		public void Track<[IsUnmanaged] T>(NativeArray<T> array, bool initialized = true) where T : struct, ValueType
		{
			this.slots.Add(new JobDependencyTracker.NativeArraySlot
			{
				hash = NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<T>(array),
				lastWrite = default(JobDependencyTracker.JobInstance),
				lastReads = ListPool<JobDependencyTracker.JobInstance>.Claim(),
				initialized = initialized
			});
			if (this.arena == null)
			{
				this.arena = new DisposeArena();
			}
			this.arena.Add<T>(array);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0003C4F7 File Offset: 0x0003A6F7
		public void Persist<[IsUnmanaged] T>(NativeArray<T> array) where T : struct, ValueType
		{
			if (this.arena == null)
			{
				return;
			}
			this.arena.Remove<T>(array);
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0003C510 File Offset: 0x0003A710
		public JobHandle ScheduleBatch(NativeArray<RaycastCommand> commands, NativeArray<RaycastHit> results, int minCommandsPerJob)
		{
			if (this.forceLinearDependencies)
			{
				RaycastCommand.ScheduleBatch(commands, results, minCommandsPerJob, default(JobHandle)).Complete();
				return default(JobHandle);
			}
			JobDependencyTracker.JobRaycastCommandDummy jobRaycastCommandDummy = new JobDependencyTracker.JobRaycastCommandDummy
			{
				commands = commands,
				results = results
			};
			JobHandle dependencies = JobDependencyAnalyzer<JobDependencyTracker.JobRaycastCommandDummy>.GetDependencies(ref jobRaycastCommandDummy, this);
			JobHandle jobHandle = RaycastCommand.ScheduleBatch(commands, results, minCommandsPerJob, dependencies);
			JobDependencyAnalyzer<JobDependencyTracker.JobRaycastCommandDummy>.Scheduled(ref jobRaycastCommandDummy, this, jobHandle);
			return jobHandle;
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0003C580 File Offset: 0x0003A780
		public void DeferFree(GCHandle handle, JobHandle dependsOn)
		{
			if (this.arena == null)
			{
				this.arena = new DisposeArena();
			}
			this.arena.Add(handle);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0003C5A4 File Offset: 0x0003A7A4
		internal void JobReadsFrom(JobHandle job, long nativeArrayHash, int jobHash)
		{
			for (int i = 0; i < this.slots.Count; i++)
			{
				JobDependencyTracker.NativeArraySlot nativeArraySlot = this.slots[i];
				if (nativeArraySlot.hash == nativeArrayHash)
				{
					nativeArraySlot.lastReads.Add(new JobDependencyTracker.JobInstance
					{
						handle = job,
						hash = jobHash
					});
					return;
				}
			}
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0003C604 File Offset: 0x0003A804
		internal void JobWritesTo(JobHandle job, long nativeArrayHash, int jobHash)
		{
			for (int i = 0; i < this.slots.Count; i++)
			{
				JobDependencyTracker.NativeArraySlot nativeArraySlot = this.slots[i];
				if (nativeArraySlot.hash == nativeArrayHash)
				{
					nativeArraySlot.lastWrite = new JobDependencyTracker.JobInstance
					{
						handle = job,
						hash = jobHash
					};
					nativeArraySlot.lastReads.Clear();
					nativeArraySlot.initialized = true;
					nativeArraySlot.hasWrite = true;
					this.slots[i] = nativeArraySlot;
					return;
				}
			}
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0003C688 File Offset: 0x0003A888
		private void Dispose()
		{
			for (int i = 0; i < this.slots.Count; i++)
			{
				ListPool<JobDependencyTracker.JobInstance>.Release(this.slots[i].lastReads);
			}
			this.slots.Clear();
			if (this.arena != null)
			{
				this.arena.DisposeAll();
			}
			this.linearDependencies = LinearDependencies.Check;
			if (this.dependenciesScratchBuffer.IsCreated)
			{
				this.dependenciesScratchBuffer.Dispose();
			}
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0003C700 File Offset: 0x0003A900
		public void ClearMemory()
		{
			this.AllWritesDependency.Complete();
			this.Dispose();
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0003C721 File Offset: 0x0003A921
		void IAstarPooledObject.OnEnterPool()
		{
			this.Dispose();
		}

		// Token: 0x0400073B RID: 1851
		internal List<JobDependencyTracker.NativeArraySlot> slots = ListPool<JobDependencyTracker.NativeArraySlot>.Claim();

		// Token: 0x0400073C RID: 1852
		private DisposeArena arena;

		// Token: 0x0400073D RID: 1853
		internal NativeArray<JobHandle> dependenciesScratchBuffer;

		// Token: 0x0400073E RID: 1854
		private LinearDependencies linearDependencies;

		// Token: 0x0400073F RID: 1855
		internal TimeSlice timeSlice = TimeSlice.Infinite;

		// Token: 0x02000180 RID: 384
		internal struct JobInstance
		{
			// Token: 0x04000740 RID: 1856
			public JobHandle handle;

			// Token: 0x04000741 RID: 1857
			public int hash;
		}

		// Token: 0x02000181 RID: 385
		internal struct NativeArraySlot
		{
			// Token: 0x04000742 RID: 1858
			public long hash;

			// Token: 0x04000743 RID: 1859
			public JobDependencyTracker.JobInstance lastWrite;

			// Token: 0x04000744 RID: 1860
			public List<JobDependencyTracker.JobInstance> lastReads;

			// Token: 0x04000745 RID: 1861
			public bool initialized;

			// Token: 0x04000746 RID: 1862
			public bool hasWrite;
		}

		// Token: 0x02000182 RID: 386
		private struct JobRaycastCommandDummy : IJob
		{
			// Token: 0x06000AB1 RID: 2737 RVA: 0x000033F6 File Offset: 0x000015F6
			public void Execute()
			{
			}

			// Token: 0x04000747 RID: 1863
			[ReadOnly]
			public NativeArray<RaycastCommand> commands;

			// Token: 0x04000748 RID: 1864
			[WriteOnly]
			public NativeArray<RaycastHit> results;
		}
	}
}
