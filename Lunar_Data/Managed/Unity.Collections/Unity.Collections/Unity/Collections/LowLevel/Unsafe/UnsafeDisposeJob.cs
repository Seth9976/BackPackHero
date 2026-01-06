using System;
using Unity.Burst;
using Unity.Jobs;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000121 RID: 289
	[BurstCompile]
	internal struct UnsafeDisposeJob : IJob
	{
		// Token: 0x06000A9C RID: 2716 RVA: 0x0001FAB8 File Offset: 0x0001DCB8
		public void Execute()
		{
			AllocatorManager.Free(this.Allocator, this.Ptr);
		}

		// Token: 0x04000378 RID: 888
		[NativeDisableUnsafePtrRestriction]
		public unsafe void* Ptr;

		// Token: 0x04000379 RID: 889
		public AllocatorManager.AllocatorHandle Allocator;
	}
}
