using System;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x02000096 RID: 150
	internal struct NativeArrayDisposeJob : IJob
	{
		// Token: 0x060002A8 RID: 680 RVA: 0x00005066 File Offset: 0x00003266
		public void Execute()
		{
			this.Data.Dispose();
		}

		// Token: 0x0400022F RID: 559
		internal NativeArrayDispose Data;
	}
}
