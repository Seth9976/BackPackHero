using System;
using System.Diagnostics;

namespace Pathfinding.Jobs
{
	// Token: 0x02000183 RID: 387
	public struct TimeSlice
	{
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x0003C747 File Offset: 0x0003A947
		public bool expired
		{
			get
			{
				return Stopwatch.GetTimestamp() > this.endTick;
			}
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0003C758 File Offset: 0x0003A958
		public static TimeSlice MillisFromNow(float millis)
		{
			return new TimeSlice
			{
				endTick = Stopwatch.GetTimestamp() + (long)(millis * 10000f)
			};
		}

		// Token: 0x04000749 RID: 1865
		public long endTick;

		// Token: 0x0400074A RID: 1866
		public static readonly TimeSlice Infinite = new TimeSlice
		{
			endTick = long.MaxValue
		};
	}
}
