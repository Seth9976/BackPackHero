using System;

namespace Pathfinding.Voxels
{
	// Token: 0x020000AD RID: 173
	internal struct Int3PolygonClipper
	{
		// Token: 0x060007BB RID: 1979 RVA: 0x0003318D File Offset: 0x0003138D
		public void Init()
		{
			if (this.clipPolygonCache == null)
			{
				this.clipPolygonCache = new float[21];
				this.clipPolygonIntCache = new int[21];
			}
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x000331B4 File Offset: 0x000313B4
		public int ClipPolygon(Int3[] vIn, int n, Int3[] vOut, int multi, int offset, int axis)
		{
			this.Init();
			int[] array = this.clipPolygonIntCache;
			for (int i = 0; i < n; i++)
			{
				array[i] = multi * vIn[i][axis] + offset;
			}
			int num = 0;
			int j = 0;
			int num2 = n - 1;
			while (j < n)
			{
				bool flag = array[num2] >= 0;
				bool flag2 = array[j] >= 0;
				if (flag != flag2)
				{
					double num3 = (double)array[num2] / (double)(array[num2] - array[j]);
					vOut[num] = vIn[num2] + (vIn[j] - vIn[num2]) * num3;
					num++;
				}
				if (flag2)
				{
					vOut[num] = vIn[j];
					num++;
				}
				num2 = j;
				j++;
			}
			return num;
		}

		// Token: 0x0400049A RID: 1178
		private float[] clipPolygonCache;

		// Token: 0x0400049B RID: 1179
		private int[] clipPolygonIntCache;
	}
}
