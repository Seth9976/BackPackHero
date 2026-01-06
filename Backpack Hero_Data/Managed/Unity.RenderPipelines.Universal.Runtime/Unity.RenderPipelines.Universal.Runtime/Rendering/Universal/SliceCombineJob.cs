using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000C4 RID: 196
	[BurstCompile]
	internal struct SliceCombineJob : IJobFor
	{
		// Token: 0x060005B8 RID: 1464 RVA: 0x00020404 File Offset: 0x0001E604
		public void Execute(int idY)
		{
			int num = idY * this.wordsPerTile;
			int num2 = num * this.tileResolution.x;
			for (int i = 0; i < this.tileResolution.x; i++)
			{
				int num3 = i * this.wordsPerTile;
				int num4 = num2 + num3;
				for (int j = 0; j < this.wordsPerTile; j++)
				{
					this.lightMasks[num4 + j] = this.sliceLightMasksH[num + j] & this.sliceLightMasksV[num3 + j];
				}
			}
		}

		// Token: 0x04000494 RID: 1172
		public int2 tileResolution;

		// Token: 0x04000495 RID: 1173
		public int wordsPerTile;

		// Token: 0x04000496 RID: 1174
		[ReadOnly]
		public NativeArray<uint> sliceLightMasksH;

		// Token: 0x04000497 RID: 1175
		[ReadOnly]
		public NativeArray<uint> sliceLightMasksV;

		// Token: 0x04000498 RID: 1176
		[NativeDisableParallelForRestriction]
		public NativeArray<uint> lightMasks;
	}
}
