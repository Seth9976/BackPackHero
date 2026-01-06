using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.Jobs
{
	// Token: 0x02000172 RID: 370
	[BurstCompile]
	public struct JobMaxHitCount : IJob
	{
		// Token: 0x06000A7F RID: 2687 RVA: 0x0003BB58 File Offset: 0x00039D58
		public void Execute()
		{
			int i;
			for (i = 0; i < this.maxHits; i++)
			{
				int num = i * this.layerStride;
				bool flag = false;
				for (int j = num; j < num + this.layerStride; j++)
				{
					if (math.any(this.hits[j].normal))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					break;
				}
			}
			this.maxHitCount[0] = math.max(1, i);
		}

		// Token: 0x0400071D RID: 1821
		[ReadOnly]
		public NativeArray<RaycastHit> hits;

		// Token: 0x0400071E RID: 1822
		public int maxHits;

		// Token: 0x0400071F RID: 1823
		public int layerStride;

		// Token: 0x04000720 RID: 1824
		[WriteOnly]
		public NativeArray<int> maxHitCount;
	}
}
