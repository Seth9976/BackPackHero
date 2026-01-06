using System;
using UnityEngine;

namespace Pathfinding.Voxels
{
	// Token: 0x020000AC RID: 172
	internal struct VoxelPolygonClipper
	{
		// Token: 0x060007B6 RID: 1974 RVA: 0x00032E3C File Offset: 0x0003103C
		public VoxelPolygonClipper(int capacity)
		{
			this.x = new float[capacity];
			this.y = new float[capacity];
			this.z = new float[capacity];
			this.n = 0;
		}

		// Token: 0x17000118 RID: 280
		public Vector3 this[int i]
		{
			set
			{
				this.x[i] = value.x;
				this.y[i] = value.y;
				this.z[i] = value.z;
			}
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00032E98 File Offset: 0x00031098
		public void ClipPolygonAlongX(ref VoxelPolygonClipper result, float multi, float offset)
		{
			int num = 0;
			float num2 = multi * this.x[this.n - 1] + offset;
			int i = 0;
			int num3 = this.n - 1;
			while (i < this.n)
			{
				float num4 = multi * this.x[i] + offset;
				bool flag = num2 >= 0f;
				bool flag2 = num4 >= 0f;
				if (flag != flag2)
				{
					float num5 = num2 / (num2 - num4);
					result.x[num] = this.x[num3] + (this.x[i] - this.x[num3]) * num5;
					result.y[num] = this.y[num3] + (this.y[i] - this.y[num3]) * num5;
					result.z[num] = this.z[num3] + (this.z[i] - this.z[num3]) * num5;
					num++;
				}
				if (flag2)
				{
					result.x[num] = this.x[i];
					result.y[num] = this.y[i];
					result.z[num] = this.z[i];
					num++;
				}
				num2 = num4;
				num3 = i;
				i++;
			}
			result.n = num;
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00032FD0 File Offset: 0x000311D0
		public void ClipPolygonAlongZWithYZ(ref VoxelPolygonClipper result, float multi, float offset)
		{
			int num = 0;
			float num2 = multi * this.z[this.n - 1] + offset;
			int i = 0;
			int num3 = this.n - 1;
			while (i < this.n)
			{
				float num4 = multi * this.z[i] + offset;
				bool flag = num2 >= 0f;
				bool flag2 = num4 >= 0f;
				if (flag != flag2)
				{
					float num5 = num2 / (num2 - num4);
					result.y[num] = this.y[num3] + (this.y[i] - this.y[num3]) * num5;
					result.z[num] = this.z[num3] + (this.z[i] - this.z[num3]) * num5;
					num++;
				}
				if (flag2)
				{
					result.y[num] = this.y[i];
					result.z[num] = this.z[i];
					num++;
				}
				num2 = num4;
				num3 = i;
				i++;
			}
			result.n = num;
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x000330CC File Offset: 0x000312CC
		public void ClipPolygonAlongZWithY(ref VoxelPolygonClipper result, float multi, float offset)
		{
			int num = 0;
			float num2 = multi * this.z[this.n - 1] + offset;
			int i = 0;
			int num3 = this.n - 1;
			while (i < this.n)
			{
				float num4 = multi * this.z[i] + offset;
				bool flag = num2 >= 0f;
				bool flag2 = num4 >= 0f;
				if (flag != flag2)
				{
					float num5 = num2 / (num2 - num4);
					result.y[num] = this.y[num3] + (this.y[i] - this.y[num3]) * num5;
					num++;
				}
				if (flag2)
				{
					result.y[num] = this.y[i];
					num++;
				}
				num2 = num4;
				num3 = i;
				i++;
			}
			result.n = num;
		}

		// Token: 0x04000496 RID: 1174
		public float[] x;

		// Token: 0x04000497 RID: 1175
		public float[] y;

		// Token: 0x04000498 RID: 1176
		public float[] z;

		// Token: 0x04000499 RID: 1177
		public int n;
	}
}
