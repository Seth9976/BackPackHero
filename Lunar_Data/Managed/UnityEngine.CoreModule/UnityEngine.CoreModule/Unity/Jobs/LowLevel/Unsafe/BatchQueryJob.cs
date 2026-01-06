using System;
using Unity.Collections;

namespace Unity.Jobs.LowLevel.Unsafe
{
	// Token: 0x02000064 RID: 100
	public struct BatchQueryJob<CommandT, ResultT> where CommandT : struct where ResultT : struct
	{
		// Token: 0x06000189 RID: 393 RVA: 0x000036E2 File Offset: 0x000018E2
		public BatchQueryJob(NativeArray<CommandT> commands, NativeArray<ResultT> results)
		{
			this.commands = commands;
			this.results = results;
		}

		// Token: 0x0400017F RID: 383
		[ReadOnly]
		internal NativeArray<CommandT> commands;

		// Token: 0x04000180 RID: 384
		internal NativeArray<ResultT> results;
	}
}
