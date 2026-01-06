using System;
using Pathfinding.Graphs.Grid;
using Unity.Burst;
using Unity.Jobs;

namespace Pathfinding.Jobs
{
	// Token: 0x0200016F RID: 367
	[BurstCompile]
	public struct IndexActionJob<T> : IJob where T : struct, GridIterationUtilities.ISliceAction
	{
		// Token: 0x06000A7C RID: 2684 RVA: 0x0003BAE0 File Offset: 0x00039CE0
		public void Execute()
		{
			for (int i = 0; i < this.length; i++)
			{
				this.action.Execute((uint)i, (uint)i);
			}
		}

		// Token: 0x04000717 RID: 1815
		public T action;

		// Token: 0x04000718 RID: 1816
		public int length;
	}
}
