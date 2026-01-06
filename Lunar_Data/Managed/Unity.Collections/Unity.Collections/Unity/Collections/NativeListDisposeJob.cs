using System;
using Unity.Burst;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000B5 RID: 181
	[BurstCompile]
	[BurstCompatible]
	internal struct NativeListDisposeJob : IJob
	{
		// Token: 0x06000704 RID: 1796 RVA: 0x00016066 File Offset: 0x00014266
		public void Execute()
		{
			this.Data.Dispose();
		}

		// Token: 0x04000283 RID: 643
		internal NativeListDispose Data;
	}
}
