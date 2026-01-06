using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x0200002A RID: 42
	[BurstCompile]
	internal struct PrepareDeformJob : IJob
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x00004B30 File Offset: 0x00002D30
		public void Execute()
		{
			for (int i = 0; i < this.batchDataSize; i++)
			{
				PerSkinJobData perSkinJobData = this.perSkinJobData[i];
				int num = 0;
				int j = perSkinJobData.bindPosesIndex.x;
				while (j < perSkinJobData.bindPosesIndex.y)
				{
					this.boneLookupData[j] = new int2(i, num);
					j++;
					num++;
				}
				int num2 = 0;
				int k = perSkinJobData.verticesIndex.x;
				while (k < perSkinJobData.verticesIndex.y)
				{
					this.vertexLookupData[k] = new int2(i, num2);
					k++;
					num2++;
				}
			}
		}

		// Token: 0x0400007C RID: 124
		[ReadOnly]
		public NativeArray<PerSkinJobData> perSkinJobData;

		// Token: 0x0400007D RID: 125
		[ReadOnly]
		public int batchDataSize;

		// Token: 0x0400007E RID: 126
		[WriteOnly]
		public NativeArray<int2> boneLookupData;

		// Token: 0x0400007F RID: 127
		[WriteOnly]
		public NativeArray<int2> vertexLookupData;
	}
}
