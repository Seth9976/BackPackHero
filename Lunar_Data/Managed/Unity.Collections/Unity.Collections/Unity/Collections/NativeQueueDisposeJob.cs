using System;
using Unity.Burst;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000D0 RID: 208
	[BurstCompile]
	internal struct NativeQueueDisposeJob : IJob
	{
		// Token: 0x060007BB RID: 1979 RVA: 0x00017CA9 File Offset: 0x00015EA9
		public void Execute()
		{
			this.Data.Dispose();
		}

		// Token: 0x040002B1 RID: 689
		public NativeQueueDispose Data;
	}
}
