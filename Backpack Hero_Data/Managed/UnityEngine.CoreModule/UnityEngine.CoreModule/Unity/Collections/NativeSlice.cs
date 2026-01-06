using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Internal;

namespace Unity.Collections
{
	// Token: 0x0200009A RID: 154
	[DebuggerDisplay("Length = {Length}")]
	[NativeContainerSupportsMinMaxWriteRestriction]
	[NativeContainer]
	[DebuggerTypeProxy(typeof(NativeSliceDebugView<>))]
	public struct NativeSlice<T> : IEnumerable<T>, IEnumerable, IEquatable<NativeSlice<T>> where T : struct
	{
		// Token: 0x060002B3 RID: 691 RVA: 0x0000514E File Offset: 0x0000334E
		public NativeSlice(NativeSlice<T> slice, int start)
		{
			this = new NativeSlice<T>(slice, start, slice.Length - start);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00005163 File Offset: 0x00003363
		public NativeSlice(NativeSlice<T> slice, int start, int length)
		{
			this.m_Stride = slice.m_Stride;
			this.m_Buffer = slice.m_Buffer + this.m_Stride * start;
			this.m_Length = length;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000518E File Offset: 0x0000338E
		public NativeSlice(NativeArray<T> array)
		{
			this = new NativeSlice<T>(array, 0, array.Length);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x000051A1 File Offset: 0x000033A1
		public NativeSlice(NativeArray<T> array, int start)
		{
			this = new NativeSlice<T>(array, start, array.Length - start);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x000051B8 File Offset: 0x000033B8
		public static implicit operator NativeSlice<T>(NativeArray<T> array)
		{
			return new NativeSlice<T>(array);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x000051D0 File Offset: 0x000033D0
		public unsafe NativeSlice(NativeArray<T> array, int start, int length)
		{
			this.m_Stride = UnsafeUtility.SizeOf<T>();
			byte* ptr = (byte*)array.m_Buffer + this.m_Stride * start;
			this.m_Buffer = ptr;
			this.m_Length = length;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00005208 File Offset: 0x00003408
		public NativeSlice<U> SliceConvert<U>() where U : struct
		{
			int num = UnsafeUtility.SizeOf<U>();
			NativeSlice<U> nativeSlice;
			nativeSlice.m_Buffer = this.m_Buffer;
			nativeSlice.m_Stride = num;
			nativeSlice.m_Length = this.m_Length * this.m_Stride / num;
			return nativeSlice;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000524C File Offset: 0x0000344C
		public NativeSlice<U> SliceWithStride<U>(int offset) where U : struct
		{
			NativeSlice<U> nativeSlice;
			nativeSlice.m_Buffer = this.m_Buffer + offset;
			nativeSlice.m_Stride = this.m_Stride;
			nativeSlice.m_Length = this.m_Length;
			return nativeSlice;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00005288 File Offset: 0x00003488
		public NativeSlice<U> SliceWithStride<U>() where U : struct
		{
			return this.SliceWithStride<U>(0);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00004557 File Offset: 0x00002757
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckReadIndex(int index)
		{
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00004557 File Offset: 0x00002757
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckWriteIndex(int index)
		{
		}

		// Token: 0x17000078 RID: 120
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

		// Token: 0x060002C0 RID: 704 RVA: 0x000052DF File Offset: 0x000034DF
		[WriteAccessRequired]
		public void CopyFrom(NativeSlice<T> slice)
		{
			UnsafeUtility.MemCpyStride(this.GetUnsafePtr<T>(), this.Stride, slice.GetUnsafeReadOnlyPtr<T>(), slice.Stride, UnsafeUtility.SizeOf<T>(), this.m_Length);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00005314 File Offset: 0x00003514
		[WriteAccessRequired]
		public unsafe void CopyFrom(T[] array)
		{
			GCHandle gchandle = GCHandle.Alloc(array, 3);
			IntPtr intPtr = gchandle.AddrOfPinnedObject();
			int num = UnsafeUtility.SizeOf<T>();
			UnsafeUtility.MemCpyStride(this.GetUnsafePtr<T>(), this.Stride, (void*)intPtr, num, num, this.m_Length);
			gchandle.Free();
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00005368 File Offset: 0x00003568
		public void CopyTo(NativeArray<T> array)
		{
			int num = UnsafeUtility.SizeOf<T>();
			UnsafeUtility.MemCpyStride(array.GetUnsafePtr<T>(), num, this.GetUnsafeReadOnlyPtr<T>(), this.Stride, num, this.m_Length);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x000053A4 File Offset: 0x000035A4
		public unsafe void CopyTo(T[] array)
		{
			GCHandle gchandle = GCHandle.Alloc(array, 3);
			IntPtr intPtr = gchandle.AddrOfPinnedObject();
			int num = UnsafeUtility.SizeOf<T>();
			UnsafeUtility.MemCpyStride((void*)intPtr, num, this.GetUnsafeReadOnlyPtr<T>(), this.Stride, num, this.m_Length);
			gchandle.Free();
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x000053F8 File Offset: 0x000035F8
		public T[] ToArray()
		{
			T[] array = new T[this.Length];
			this.CopyTo(array);
			return array;
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000541F File Offset: 0x0000361F
		public int Stride
		{
			get
			{
				return this.m_Stride;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00005428 File Offset: 0x00003628
		public int Length
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00005440 File Offset: 0x00003640
		public NativeSlice<T>.Enumerator GetEnumerator()
		{
			return new NativeSlice<T>.Enumerator(ref this);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00005458 File Offset: 0x00003658
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new NativeSlice<T>.Enumerator(ref this);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00005478 File Offset: 0x00003678
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00005498 File Offset: 0x00003698
		public bool Equals(NativeSlice<T> other)
		{
			return this.m_Buffer == other.m_Buffer && this.m_Stride == other.m_Stride && this.m_Length == other.m_Length;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000054D8 File Offset: 0x000036D8
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is NativeSlice<T> && this.Equals((NativeSlice<T>)obj);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00005510 File Offset: 0x00003710
		public override int GetHashCode()
		{
			int num = this.m_Buffer;
			num = (num * 397) ^ this.m_Stride;
			return (num * 397) ^ this.m_Length;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000554C File Offset: 0x0000374C
		public static bool operator ==(NativeSlice<T> left, NativeSlice<T> right)
		{
			return left.Equals(right);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00005568 File Offset: 0x00003768
		public static bool operator !=(NativeSlice<T> left, NativeSlice<T> right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000232 RID: 562
		[NativeDisableUnsafePtrRestriction]
		internal unsafe byte* m_Buffer;

		// Token: 0x04000233 RID: 563
		internal int m_Stride;

		// Token: 0x04000234 RID: 564
		internal int m_Length;

		// Token: 0x0200009B RID: 155
		[ExcludeFromDocs]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x060002CF RID: 719 RVA: 0x00005585 File Offset: 0x00003785
			public Enumerator(ref NativeSlice<T> array)
			{
				this.m_Array = array;
				this.m_Index = -1;
			}

			// Token: 0x060002D0 RID: 720 RVA: 0x00004557 File Offset: 0x00002757
			public void Dispose()
			{
			}

			// Token: 0x060002D1 RID: 721 RVA: 0x0000559C File Offset: 0x0000379C
			public bool MoveNext()
			{
				this.m_Index++;
				return this.m_Index < this.m_Array.Length;
			}

			// Token: 0x060002D2 RID: 722 RVA: 0x000055CF File Offset: 0x000037CF
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x1700007B RID: 123
			// (get) Token: 0x060002D3 RID: 723 RVA: 0x000055D9 File Offset: 0x000037D9
			public T Current
			{
				get
				{
					return this.m_Array[this.m_Index];
				}
			}

			// Token: 0x1700007C RID: 124
			// (get) Token: 0x060002D4 RID: 724 RVA: 0x000055EC File Offset: 0x000037EC
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000235 RID: 565
			private NativeSlice<T> m_Array;

			// Token: 0x04000236 RID: 566
			private int m_Index;
		}
	}
}
