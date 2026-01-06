using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace Pathfinding.Jobs
{
	// Token: 0x0200016E RID: 366
	[BurstCompile]
	public struct JobCopy<T> : IJob where T : struct
	{
		// Token: 0x06000A7B RID: 2683 RVA: 0x0003BACD File Offset: 0x00039CCD
		public void Execute()
		{
			this.from.CopyTo(this.to);
		}

		// Token: 0x04000715 RID: 1813
		[ReadOnly]
		public NativeArray<T> from;

		// Token: 0x04000716 RID: 1814
		[WriteOnly]
		public NativeArray<T> to;
	}
}
