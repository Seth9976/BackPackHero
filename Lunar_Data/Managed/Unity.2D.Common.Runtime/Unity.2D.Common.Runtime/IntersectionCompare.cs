using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Mathematics;

namespace UnityEngine.U2D.Common.UTess
{
	// Token: 0x02000014 RID: 20
	internal struct IntersectionCompare : IComparer<int2>
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00005588 File Offset: 0x00003788
		public unsafe int Compare(int2 a, int2 b)
		{
			int2 @int = this.edges[a.x];
			int2 int2 = this.edges[a.y];
			int2 int3 = this.edges[b.x];
			int2 int4 = this.edges[b.y];
			this.xvasort.FixedElementField = this.points[@int.x].x;
			*((ref this.xvasort.FixedElementField) + 8) = this.points[@int.y].x;
			*((ref this.xvasort.FixedElementField) + (IntPtr)2 * 8) = this.points[int2.x].x;
			*((ref this.xvasort.FixedElementField) + (IntPtr)3 * 8) = this.points[int2.y].x;
			this.xvbsort.FixedElementField = this.points[int3.x].x;
			*((ref this.xvbsort.FixedElementField) + 8) = this.points[int3.y].x;
			*((ref this.xvbsort.FixedElementField) + (IntPtr)2 * 8) = this.points[int4.x].x;
			*((ref this.xvbsort.FixedElementField) + (IntPtr)3 * 8) = this.points[int4.y].x;
			fixed (double* ptr = &this.xvasort.FixedElementField)
			{
				ModuleHandle.InsertionSort<double, XCompare>((void*)ptr, 0, 3, default(XCompare));
			}
			fixed (double* ptr = &this.xvbsort.FixedElementField)
			{
				ModuleHandle.InsertionSort<double, XCompare>((void*)ptr, 0, 3, default(XCompare));
			}
			int i = 0;
			while (i < 4)
			{
				if (*((ref this.xvasort.FixedElementField) + (IntPtr)i * 8) - *((ref this.xvbsort.FixedElementField) + (IntPtr)i * 8) != 0.0)
				{
					if (*((ref this.xvasort.FixedElementField) + (IntPtr)i * 8) >= *((ref this.xvbsort.FixedElementField) + (IntPtr)i * 8))
					{
						return 1;
					}
					return -1;
				}
				else
				{
					i++;
				}
			}
			if (this.points[@int.x].y >= this.points[@int.x].y)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x0400003E RID: 62
		public Array<double2> points;

		// Token: 0x0400003F RID: 63
		public Array<int2> edges;

		// Token: 0x04000040 RID: 64
		[FixedBuffer(typeof(double), 4)]
		public IntersectionCompare.<xvasort>e__FixedBuffer xvasort;

		// Token: 0x04000041 RID: 65
		[FixedBuffer(typeof(double), 4)]
		public IntersectionCompare.<xvbsort>e__FixedBuffer xvbsort;

		// Token: 0x02000022 RID: 34
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 32)]
		public struct <xvasort>e__FixedBuffer
		{
			// Token: 0x0400004C RID: 76
			public double FixedElementField;
		}

		// Token: 0x02000023 RID: 35
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 32)]
		public struct <xvbsort>e__FixedBuffer
		{
			// Token: 0x0400004D RID: 77
			public double FixedElementField;
		}
	}
}
