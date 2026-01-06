using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Burst;
using Unity.Burst.CompilerServices;
using UnityEngine;

namespace Pathfinding.Graphs.Navmesh.Voxelization
{
	// Token: 0x020001CF RID: 463
	internal struct VoxelPolygonClipper
	{
		// Token: 0x170001B2 RID: 434
		public unsafe Vector3 this[int i]
		{
			set
			{
				*((ref this.x.FixedElementField) + (IntPtr)i * 4) = value.x;
				*((ref this.y.FixedElementField) + (IntPtr)i * 4) = value.y;
				*((ref this.z.FixedElementField) + (IntPtr)i * 4) = value.z;
			}
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00046D8C File Offset: 0x00044F8C
		public unsafe void ClipPolygonAlongX([NoAlias] ref VoxelPolygonClipper result, float multi, float offset)
		{
			int num = 0;
			float num2 = multi * *((ref this.x.FixedElementField) + (IntPtr)(this.n - 1) * 4) + offset;
			int i = 0;
			int num3 = this.n - 1;
			while (i < this.n)
			{
				float num4 = multi * *((ref this.x.FixedElementField) + (IntPtr)i * 4) + offset;
				bool flag = num2 >= 0f;
				bool flag2 = num4 >= 0f;
				if (flag != flag2)
				{
					float num5 = num2 / (num2 - num4);
					*((ref result.x.FixedElementField) + (IntPtr)num * 4) = *((ref this.x.FixedElementField) + (IntPtr)num3 * 4) + (*((ref this.x.FixedElementField) + (IntPtr)i * 4) - *((ref this.x.FixedElementField) + (IntPtr)num3 * 4)) * num5;
					*((ref result.y.FixedElementField) + (IntPtr)num * 4) = *((ref this.y.FixedElementField) + (IntPtr)num3 * 4) + (*((ref this.y.FixedElementField) + (IntPtr)i * 4) - *((ref this.y.FixedElementField) + (IntPtr)num3 * 4)) * num5;
					*((ref result.z.FixedElementField) + (IntPtr)num * 4) = *((ref this.z.FixedElementField) + (IntPtr)num3 * 4) + (*((ref this.z.FixedElementField) + (IntPtr)i * 4) - *((ref this.z.FixedElementField) + (IntPtr)num3 * 4)) * num5;
					num++;
				}
				if (flag2)
				{
					*((ref result.x.FixedElementField) + (IntPtr)num * 4) = *((ref this.x.FixedElementField) + (IntPtr)i * 4);
					*((ref result.y.FixedElementField) + (IntPtr)num * 4) = *((ref this.y.FixedElementField) + (IntPtr)i * 4);
					*((ref result.z.FixedElementField) + (IntPtr)num * 4) = *((ref this.z.FixedElementField) + (IntPtr)i * 4);
					num++;
				}
				num2 = num4;
				num3 = i;
				i++;
			}
			result.n = num;
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x00046F70 File Offset: 0x00045170
		public unsafe void ClipPolygonAlongZWithYZ([NoAlias] ref VoxelPolygonClipper result, float multi, float offset)
		{
			int num = 0;
			Hint.Assume(this.n >= 0);
			Hint.Assume(this.n <= 8);
			float num2 = multi * *((ref this.z.FixedElementField) + (IntPtr)(this.n - 1) * 4) + offset;
			int i = 0;
			int num3 = this.n - 1;
			while (i < this.n)
			{
				float num4 = multi * *((ref this.z.FixedElementField) + (IntPtr)i * 4) + offset;
				bool flag = num2 >= 0f;
				bool flag2 = num4 >= 0f;
				if (flag != flag2)
				{
					float num5 = num2 / (num2 - num4);
					*((ref result.y.FixedElementField) + (IntPtr)num * 4) = *((ref this.y.FixedElementField) + (IntPtr)num3 * 4) + (*((ref this.y.FixedElementField) + (IntPtr)i * 4) - *((ref this.y.FixedElementField) + (IntPtr)num3 * 4)) * num5;
					*((ref result.z.FixedElementField) + (IntPtr)num * 4) = *((ref this.z.FixedElementField) + (IntPtr)num3 * 4) + (*((ref this.z.FixedElementField) + (IntPtr)i * 4) - *((ref this.z.FixedElementField) + (IntPtr)num3 * 4)) * num5;
					num++;
				}
				if (flag2)
				{
					*((ref result.y.FixedElementField) + (IntPtr)num * 4) = *((ref this.y.FixedElementField) + (IntPtr)i * 4);
					*((ref result.z.FixedElementField) + (IntPtr)num * 4) = *((ref this.z.FixedElementField) + (IntPtr)i * 4);
					num++;
				}
				num2 = num4;
				num3 = i;
				i++;
			}
			result.n = num;
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0004710C File Offset: 0x0004530C
		public unsafe void ClipPolygonAlongZWithY([NoAlias] ref VoxelPolygonClipper result, float multi, float offset)
		{
			int num = 0;
			Hint.Assume(this.n >= 3);
			Hint.Assume(this.n <= 8);
			float num2 = multi * *((ref this.z.FixedElementField) + (IntPtr)(this.n - 1) * 4) + offset;
			int i = 0;
			int num3 = this.n - 1;
			while (i < this.n)
			{
				float num4 = multi * *((ref this.z.FixedElementField) + (IntPtr)i * 4) + offset;
				bool flag = num2 >= 0f;
				bool flag2 = num4 >= 0f;
				if (flag != flag2)
				{
					float num5 = num2 / (num2 - num4);
					*((ref result.y.FixedElementField) + (IntPtr)num * 4) = *((ref this.y.FixedElementField) + (IntPtr)num3 * 4) + (*((ref this.y.FixedElementField) + (IntPtr)i * 4) - *((ref this.y.FixedElementField) + (IntPtr)num3 * 4)) * num5;
					num++;
				}
				if (flag2)
				{
					*((ref result.y.FixedElementField) + (IntPtr)num * 4) = *((ref this.y.FixedElementField) + (IntPtr)i * 4);
					num++;
				}
				num2 = num4;
				num3 = i;
				i++;
			}
			result.n = num;
		}

		// Token: 0x04000876 RID: 2166
		[FixedBuffer(typeof(float), 8)]
		public VoxelPolygonClipper.<x>e__FixedBuffer x;

		// Token: 0x04000877 RID: 2167
		[FixedBuffer(typeof(float), 8)]
		public VoxelPolygonClipper.<y>e__FixedBuffer y;

		// Token: 0x04000878 RID: 2168
		[FixedBuffer(typeof(float), 8)]
		public VoxelPolygonClipper.<z>e__FixedBuffer z;

		// Token: 0x04000879 RID: 2169
		public int n;

		// Token: 0x020001D0 RID: 464
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(0, Size = 32)]
		public struct <x>e__FixedBuffer
		{
			// Token: 0x0400087A RID: 2170
			public float FixedElementField;
		}

		// Token: 0x020001D1 RID: 465
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(0, Size = 32)]
		public struct <y>e__FixedBuffer
		{
			// Token: 0x0400087B RID: 2171
			public float FixedElementField;
		}

		// Token: 0x020001D2 RID: 466
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(0, Size = 32)]
		public struct <z>e__FixedBuffer
		{
			// Token: 0x0400087C RID: 2172
			public float FixedElementField;
		}
	}
}
