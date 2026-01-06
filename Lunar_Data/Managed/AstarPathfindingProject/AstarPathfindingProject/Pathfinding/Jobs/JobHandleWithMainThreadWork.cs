using System;
using System.Collections.Generic;
using Unity.Jobs;

namespace Pathfinding.Jobs
{
	// Token: 0x0200017C RID: 380
	public struct JobHandleWithMainThreadWork<T> where T : struct
	{
		// Token: 0x06000A97 RID: 2711 RVA: 0x0003C179 File Offset: 0x0003A379
		public JobHandleWithMainThreadWork(IEnumerator<ValueTuple<JobHandle, T>> handles, JobDependencyTracker tracker)
		{
			this.coroutine = handles;
			this.tracker = tracker;
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0003C18C File Offset: 0x0003A38C
		public void Complete()
		{
			this.tracker.timeSlice = TimeSlice.Infinite;
			while (this.coroutine.MoveNext())
			{
				ValueTuple<JobHandle, T> valueTuple = this.coroutine.Current;
				valueTuple.Item1.Complete();
			}
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0003C1D0 File Offset: 0x0003A3D0
		public IEnumerable<T?> CompleteTimeSliced(float maxMillisPerStep)
		{
			this.tracker.timeSlice = TimeSlice.MillisFromNow(maxMillisPerStep);
			while (this.coroutine.MoveNext())
			{
				ValueTuple<JobHandle, T> valueTuple;
				if (maxMillisPerStep < float.PositiveInfinity)
				{
					for (;;)
					{
						valueTuple = this.coroutine.Current;
						if (valueTuple.Item1.IsCompleted)
						{
							break;
						}
						T? t = null;
						this.tracker.timeSlice = TimeSlice.MillisFromNow(maxMillisPerStep);
					}
				}
				valueTuple = this.coroutine.Current;
				valueTuple.Item1.Complete();
				yield return new T?(this.coroutine.Current.Item2);
				this.tracker.timeSlice = TimeSlice.MillisFromNow(maxMillisPerStep);
			}
			yield break;
			yield break;
		}

		// Token: 0x0400072E RID: 1838
		private JobDependencyTracker tracker;

		// Token: 0x0400072F RID: 1839
		private IEnumerator<ValueTuple<JobHandle, T>> coroutine;
	}
}
