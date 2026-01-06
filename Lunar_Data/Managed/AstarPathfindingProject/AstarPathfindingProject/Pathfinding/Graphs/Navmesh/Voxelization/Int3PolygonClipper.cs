using System;

namespace Pathfinding.Graphs.Navmesh.Voxelization
{
	// Token: 0x020001CE RID: 462
	internal struct Int3PolygonClipper
	{
		// Token: 0x06000C0C RID: 3084 RVA: 0x00046C4C File Offset: 0x00044E4C
		public void Init()
		{
			if (this.clipPolygonCache == null)
			{
				this.clipPolygonCache = new float[21];
				this.clipPolygonIntCache = new int[21];
			}
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00046C70 File Offset: 0x00044E70
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

		// Token: 0x04000874 RID: 2164
		private float[] clipPolygonCache;

		// Token: 0x04000875 RID: 2165
		private int[] clipPolygonIntCache;
	}
}
