using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.U2D.Common.UTess
{
	// Token: 0x02000004 RID: 4
	[DebuggerDisplay("Length = {Length}")]
	[DebuggerTypeProxy(typeof(ArraySliceDebugView<>))]
	internal struct ArraySlice<T> : IEquatable<ArraySlice<T>> where T : struct
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000021E0 File Offset: 0x000003E0
		public unsafe ArraySlice(NativeArray<T> array, int start, int length)
		{
			this.m_Stride = UnsafeUtility.SizeOf<T>();
			byte* ptr = (byte*)array.GetUnsafePtr<T>() + this.m_Stride * start;
			this.m_Buffer = ptr;
			this.m_Length = length;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002218 File Offset: 0x00000418
		public unsafe ArraySlice(Array<T> array, int start, int length)
		{
			this.m_Stride = UnsafeUtility.SizeOf<T>();
			byte* ptr = (byte*)array.UnsafePtr + this.m_Stride * start;
			this.m_Buffer = ptr;
			this.m_Length = length;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000224F File Offset: 0x0000044F
		public bool Equals(ArraySlice<T> other)
		{
			return this.m_Buffer == other.m_Buffer && this.m_Stride == other.m_Stride && this.m_Length == other.m_Length;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000227D File Offset: 0x0000047D
		public override bool Equals(object obj)
		{
			return obj != null && obj is ArraySlice<T> && this.Equals((ArraySlice<T>)obj);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000229A File Offset: 0x0000049A
		public override int GetHashCode()
		{
			return (((this.m_Buffer * 397) ^ this.m_Stride) * 397) ^ this.m_Length;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022BD File Offset: 0x000004BD
		public static bool operator ==(ArraySlice<T> left, ArraySlice<T> right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022C7 File Offset: 0x000004C7
		public static bool operator !=(ArraySlice<T> left, ArraySlice<T> right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022D4 File Offset: 0x000004D4
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

		// Token: 0x17000007 RID: 7
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

		// Token: 0x06000017 RID: 23 RVA: 0x00002369 File Offset: 0x00000569
		internal unsafe void* GetUnsafeReadOnlyPtr()
		{
			return (void*)this.m_Buffer;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002374 File Offset: 0x00000574
		internal unsafe void CopyTo(T[] array)
		{
			GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			IntPtr intPtr = gchandle.AddrOfPinnedObject();
			int num = UnsafeUtility.SizeOf<T>();
			UnsafeUtility.MemCpyStride((void*)intPtr, num, this.GetUnsafeReadOnlyPtr(), this.Stride, num, this.m_Length);
			gchandle.Free();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023BC File Offset: 0x000005BC
		internal T[] ToArray()
		{
			T[] array = new T[this.Length];
			this.CopyTo(array);
			return array;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000023DD File Offset: 0x000005DD
		public int Stride
		{
			get
			{
				return this.m_Stride;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000023E5 File Offset: 0x000005E5
		public int Length
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x04000006 RID: 6
		[NativeDisableUnsafePtrRestriction]
		internal unsafe byte* m_Buffer;

		// Token: 0x04000007 RID: 7
		internal int m_Stride;

		// Token: 0x04000008 RID: 8
		internal int m_Length;
	}
}
