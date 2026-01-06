using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000C9 RID: 201
	[BurstCompile]
	internal struct ZBinningJob : IJobFor
	{
		// Token: 0x060005C0 RID: 1472 RVA: 0x00020814 File Offset: 0x0001EA14
		public void Execute(int index)
		{
			int num = 64 * index;
			int num2 = math.min(num + 64, this.bins.Length) - 1;
			for (int i = num; i <= num2; i++)
			{
				this.bins[i] = new ZBin
				{
					minIndex = ushort.MaxValue,
					maxIndex = ushort.MaxValue
				};
			}
			for (int j = 0; j < this.minMaxZs.Length; j++)
			{
				ushort num3 = (ushort)j;
				LightMinMaxZ lightMinMaxZ = this.minMaxZs[j];
				int num4 = math.max((int)(math.sqrt(lightMinMaxZ.minZ) * this.zFactor) - this.binOffset, num);
				int num5 = math.min((int)(math.sqrt(lightMinMaxZ.maxZ) * this.zFactor) - this.binOffset, num2);
				for (int k = num4; k <= num5; k++)
				{
					ZBin zbin = this.bins[k];
					zbin.minIndex = Math.Min(zbin.minIndex, num3);
					zbin.maxIndex = num3;
					this.bins[k] = zbin;
				}
			}
		}

		// Token: 0x040004AC RID: 1196
		public const int batchCount = 64;

		// Token: 0x040004AD RID: 1197
		[NativeDisableParallelForRestriction]
		public NativeArray<ZBin> bins;

		// Token: 0x040004AE RID: 1198
		[ReadOnly]
		public NativeArray<LightMinMaxZ> minMaxZs;

		// Token: 0x040004AF RID: 1199
		public int binOffset;

		// Token: 0x040004B0 RID: 1200
		public float zFactor;
	}
}
