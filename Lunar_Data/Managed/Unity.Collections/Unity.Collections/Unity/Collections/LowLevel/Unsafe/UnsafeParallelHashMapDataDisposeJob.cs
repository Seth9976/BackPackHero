using System;
using Unity.Burst;
using Unity.Jobs;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000132 RID: 306
	[BurstCompile]
	internal struct UnsafeParallelHashMapDataDisposeJob : IJob
	{
		// Token: 0x06000B24 RID: 2852 RVA: 0x00021098 File Offset: 0x0001F298
		public void Execute()
		{
			this.Data.Dispose();
		}

		// Token: 0x040003A9 RID: 937
		internal UnsafeParallelHashMapDataDispose Data;
	}
}
