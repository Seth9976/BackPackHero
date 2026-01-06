using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000C2 RID: 194
	[BurstCompile]
	internal struct RadixSortJob : IJob
	{
		// Token: 0x060005B6 RID: 1462 RVA: 0x00020228 File Offset: 0x0001E428
		public void Execute()
		{
			NativeArray<int> nativeArray = new NativeArray<int>(256, Allocator.Temp, NativeArrayOptions.ClearMemory);
			int num = this.indices.Length / 2;
			for (int i = 0; i < num; i++)
			{
				this.indices[i] = i;
			}
			for (int j = 0; j < 4; j++)
			{
				for (int k = 0; k < 256; k++)
				{
					nativeArray[k] = 0;
				}
				int num2;
				int num3;
				if (j % 2 == 0)
				{
					num2 = 0;
					num3 = num;
				}
				else
				{
					num2 = num;
					num3 = 0;
				}
				for (int l = 0; l < num; l++)
				{
					uint num4 = (this.keys[num2 + l] >> 8 * j) & 255U;
					int num5 = (int)num4;
					int num6 = nativeArray[num5];
					nativeArray[num5] = num6 + 1;
				}
				for (int m = 1; m < 256; m++)
				{
					ref NativeArray<int> ptr = ref nativeArray;
					int num6 = m;
					ptr[num6] += nativeArray[m - 1];
				}
				for (int n = num - 1; n >= 0; n--)
				{
					uint num7 = this.keys[num2 + n];
					uint num8 = (num7 >> 8 * j) & 255U;
					int num9 = nativeArray[(int)num8] - 1;
					int num6 = (int)num8;
					int num5 = nativeArray[num6];
					nativeArray[num6] = num5 - 1;
					this.keys[num3 + num9] = num7;
					this.indices[num3 + num9] = this.indices[num2 + n];
				}
			}
			nativeArray.Dispose();
		}

		// Token: 0x0400048F RID: 1167
		public NativeArray<uint> keys;

		// Token: 0x04000490 RID: 1168
		public NativeArray<int> indices;
	}
}
