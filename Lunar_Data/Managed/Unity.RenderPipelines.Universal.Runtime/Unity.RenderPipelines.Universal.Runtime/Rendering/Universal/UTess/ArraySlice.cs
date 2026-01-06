using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.Rendering.Universal.UTess
{
	// Token: 0x02000119 RID: 281
	[DebuggerDisplay("Length = {Length}")]
	[DebuggerTypeProxy(typeof(ArraySliceDebugView<>))]
	internal struct ArraySlice<T> : IEquatable<ArraySlice<T>> where T : struct
	{
		// Token: 0x060008D0 RID: 2256 RVA: 0x00039598 File Offset: 0x00037798
		public unsafe ArraySlice(NativeArray<T> array, int start, int length)
		{
			this.m_Stride = UnsafeUtility.SizeOf<T>();
			byte* ptr = (byte*)array.GetUnsafePtr<T>() + this.m_Stride * start;
			this.m_Buffer = ptr;
			this.m_Length = length;
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x000395CE File Offset: 0x000377CE
		public bool Equals(ArraySlice<T> other)
		{
			return this.m_Buffer == other.m_Buffer && this.m_Stride == other.m_Stride && this.m_Length == other.m_Length;
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x000395FC File Offset: 0x000377FC
		public override bool Equals(object obj)
		{
			return obj != null && obj is ArraySlice<T> && this.Equals((ArraySlice<T>)obj);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00039619 File Offset: 0x00037819
		public override int GetHashCode()
		{
			return (((this.m_Buffer * 397) ^ this.m_Stride) * 397) ^ this.m_Length;
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0003963C File Offset: 0x0003783C
		public static bool operator ==(ArraySlice<T> left, ArraySlice<T> right)
		{
			return left.Equals(right);
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00039646 File Offset: 0x00037846
		public static bool operator !=(ArraySlice<T> left, ArraySlice<T> right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00039654 File Offset: 0x00037854
		public unsafe static ArraySlice<T> ConvertExistingDataToArraySlice(void* dataPointer, int stride, int length)
		{
			if (length < 0)
			{
				throw new ArgumentException(string.Format("Invalid length of '{0}'. It must be greater than 0.", length), "length");
			}
			if (stride < 0)
			{
				throw new ArgumentException(string.Format("Invalid stride '{0}'. It must be greater than 0.", stride), "stride");
			}
			return new ArraySlice<T>
			{
				m_Stride = stride,
				m_Buffer = (byte*)dataPointer,
				m_Length = length
			};
		}

		// Token: 0x17000219 RID: 537
		public unsafe T this[int index]
		{
			get
			{
				return UnsafeUtility.ReadArrayElementWithStride<T>((void*)this.m_Buffer, index, this.m_Stride);
			}
			[WriteAccessRequired]
			set
			{
				UnsafeUtility.WriteArrayElementWithStride<T>((void*)this.m_Buffer, index, this.m_Stride, value);
			}
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x000396E9 File Offset: 0x000378E9
		internal unsafe void* GetUnsafeReadOnlyPtr()
		{
			return (void*)this.m_Buffer;
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x000396F4 File Offset: 0x000378F4
		internal unsafe void CopyTo(T[] array)
		{
			GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			IntPtr intPtr = gchandle.AddrOfPinnedObject();
			int num = UnsafeUtility.SizeOf<T>();
			UnsafeUtility.MemCpyStride((void*)intPtr, num, this.GetUnsafeReadOnlyPtr(), this.Stride, num, this.m_Length);
			gchandle.Free();
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0003973C File Offset: 0x0003793C
		internal T[] ToArray()
		{
			T[] array = new T[this.Length];
			this.CopyTo(array);
			return array;
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x0003975D File Offset: 0x0003795D
		public int Stride
		{
			get
			{
				return this.m_Stride;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x00039765 File Offset: 0x00037965
		public int Length
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x04000817 RID: 2071
		[NativeDisableUnsafePtrRestriction]
		internal unsafe byte* m_Buffer;

		// Token: 0x04000818 RID: 2072
		internal int m_Stride;

		// Token: 0x04000819 RID: 2073
		internal int m_Length;
	}
}
