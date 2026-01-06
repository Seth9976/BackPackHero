using System;
using Pathfinding.Graphs.Grid;
using Unity.Burst;
using Unity.Jobs;

namespace Pathfinding.Jobs
{
	// Token: 0x02000170 RID: 368
	[BurstCompile]
	public struct SliceActionJob<T> : IJob where T : struct, GridIterationUtilities.ISliceAction
	{
		// Token: 0x06000A7D RID: 2685 RVA: 0x0003BB11 File Offset: 0x00039D11
		public void Execute()
		{
			GridIterationUtilities.ForEachCellIn3DSlice<T>(this.slice, ref this.action);
		}

		// Token: 0x04000719 RID: 1817
		public T action;

		// Token: 0x0400071A RID: 1818
		public Slice3D slice;
	}
}
