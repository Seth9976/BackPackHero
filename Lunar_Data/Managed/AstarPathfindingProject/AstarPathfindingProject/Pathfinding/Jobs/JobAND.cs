using System;
using Pathfinding.Graphs.Grid;
using Unity.Collections;

namespace Pathfinding.Jobs
{
	// Token: 0x02000171 RID: 369
	public struct JobAND : GridIterationUtilities.ISliceAction
	{
		// Token: 0x06000A7E RID: 2686 RVA: 0x0003BB24 File Offset: 0x00039D24
		public void Execute(uint outerIdx, uint innerIdx)
		{
			ref NativeArray<bool> ptr = ref this.result;
			ptr[(int)outerIdx] = ptr[(int)outerIdx] & this.data[(int)outerIdx];
		}

		// Token: 0x0400071B RID: 1819
		public NativeArray<bool> result;

		// Token: 0x0400071C RID: 1820
		[ReadOnly]
		public NativeArray<bool> data;
	}
}
