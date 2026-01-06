using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x02000006 RID: 6
	internal struct NativeCustomSlice<T> where T : struct
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000025C8 File Offset: 0x000007C8
		public static NativeCustomSlice<T> Default()
		{
			return new NativeCustomSlice<T>
			{
				data = IntPtr.Zero,
				length = 0,
				stride = 0
			};
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000025FA File Offset: 0x000007FA
		public NativeCustomSlice(NativeSlice<T> nativeSlice)
		{
			this.data = new IntPtr(nativeSlice.GetUnsafeReadOnlyPtr<T>());
			this.length = nativeSlice.Length;
			this.stride = nativeSlice.Stride;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002627 File Offset: 0x00000827
		public NativeCustomSlice(NativeSlice<byte> slice, int length, int stride)
		{
			this.data = new IntPtr(slice.GetUnsafeReadOnlyPtr<byte>());
			this.length = length;
			this.stride = stride;
		}

		// Token: 0x17000006 RID: 6
		public T this[int index]
		{
			get
			{
				return UnsafeUtility.ReadArrayElementWithStride<T>(this.data.ToPointer(), index, this.stride);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002661 File Offset: 0x00000861
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x0400000B RID: 11
		[NativeDisableUnsafePtrRestriction]
		public IntPtr data;

		// Token: 0x0400000C RID: 12
		public int length;

		// Token: 0x0400000D RID: 13
		public int stride;
	}
}
