using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace Pathfinding.Graphs.Navmesh.Jobs
{
	// Token: 0x020001F2 RID: 498
	[BurstCompile]
	public struct JobConvertAreasToTags : IJob
	{
		// Token: 0x06000C67 RID: 3175 RVA: 0x0004D2E4 File Offset: 0x0004B4E4
		public void Execute()
		{
			for (int i = 0; i < this.areas.Length; i++)
			{
				int num = this.areas[i];
				this.areas[i] = (((num & 16384) != 0) ? ((num & 16383) - 1) : 0);
			}
		}

		// Token: 0x04000934 RID: 2356
		public NativeList<int> areas;
	}
}
