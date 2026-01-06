using System;
using Unity.Burst;
using Unity.Jobs;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000139 RID: 313
	[BurstCompile]
	internal struct UnsafeParallelHashMapDisposeJob : IJob
	{
		// Token: 0x06000B5C RID: 2908 RVA: 0x00021E5E File Offset: 0x0002005E
		public void Execute()
		{
			UnsafeParallelHashMapData.DeallocateHashMap(this.Data, this.Allocator);
		}

		// Token: 0x040003B6 RID: 950
		[NativeDisableUnsafePtrRestriction]
		public unsafe UnsafeParallelHashMapData* Data;

		// Token: 0x040003B7 RID: 951
		public AllocatorManager.AllocatorHandle Allocator;
	}
}
