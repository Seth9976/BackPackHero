using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Pathfinding.Util
{
	// Token: 0x02000257 RID: 599
	public readonly struct UnsafeSpan<[IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x00058431 File Offset: 0x00056631
		public int Length
		{
			get
			{
				return (int)this.length;
			}
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x00058439 File Offset: 0x00056639
		[MethodImpl(256)]
		public unsafe UnsafeSpan(void* ptr, int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (length > 0 && ptr == null)
			{
				throw new ArgumentNullException();
			}
			this.ptr = (T*)ptr;
			this.length = (uint)length;
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00058462 File Offset: 0x00056662
		public unsafe UnsafeSpan(T[] data, out ulong gcHandle)
		{
			this.ptr = (T*)UnsafeUtility.PinGCArrayAndGetDataAddress(data, out gcHandle);
			this.length = (uint)data.Length;
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x0005847A File Offset: 0x0005667A
		public UnsafeSpan(Allocator allocator, int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (length > 0)
			{
				this.ptr = AllocatorManager.Allocate<T>(allocator, length);
			}
			else
			{
				this.ptr = null;
			}
			this.length = (uint)length;
		}

		// Token: 0x170001EE RID: 494
		public unsafe ref T this[int index]
		{
			[MethodImpl(256)]
			get
			{
				if (index >= (int)this.length)
				{
					throw new IndexOutOfRangeException();
				}
				Hint.Assume(this.ptr != null);
				return ref this.ptr[(IntPtr)index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
			}
		}

		// Token: 0x170001EF RID: 495
		public unsafe ref T this[uint index]
		{
			[MethodImpl(256)]
			get
			{
				if (index >= this.length)
				{
					throw new IndexOutOfRangeException();
				}
				Hint.Assume(this.ptr != null);
				Hint.Assume(this.ptr + (ulong)index * (ulong)((long)sizeof(T)) / (ulong)sizeof(T) != null);
				return ref this.ptr[(ulong)index * (ulong)((long)sizeof(T)) / (ulong)sizeof(T)];
			}
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x0005853E File Offset: 0x0005673E
		[MethodImpl(256)]
		public unsafe UnsafeSpan<U> Reinterpret<[IsUnmanaged] U>() where U : struct, ValueType
		{
			if (sizeof(T) != sizeof(U))
			{
				throw new InvalidOperationException("Cannot reinterpret span because the size of the types do not match");
			}
			return new UnsafeSpan<U>((void*)this.ptr, (int)this.length);
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x0005856A File Offset: 0x0005676A
		public unsafe UnsafeSpan<T> Slice(int start, int length)
		{
			if (start < 0 || length < 0 || (long)(start + length) > (long)((ulong)this.length))
			{
				throw new ArgumentOutOfRangeException();
			}
			return new UnsafeSpan<T>((void*)(this.ptr + (IntPtr)start * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)), length);
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x0005859D File Offset: 0x0005679D
		public UnsafeSpan<T> Slice(int start)
		{
			return this.Slice(start, (int)(this.length - (uint)start));
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x000585B0 File Offset: 0x000567B0
		public unsafe void Move(int startIndex, int toIndex, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (startIndex < 0 || (long)(startIndex + count) > (long)((ulong)this.length))
			{
				throw new ArgumentOutOfRangeException();
			}
			if (toIndex < 0 || (long)(toIndex + count) > (long)((ulong)this.length))
			{
				throw new ArgumentOutOfRangeException();
			}
			if (count == 0)
			{
				return;
			}
			UnsafeUtility.MemMove((void*)(this.ptr + (IntPtr)toIndex * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)), (void*)(this.ptr + (IntPtr)startIndex * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)), (long)sizeof(T) * (long)count);
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00058628 File Offset: 0x00056828
		public unsafe void CopyTo(UnsafeSpan<T> other)
		{
			if (other.length < this.length)
			{
				throw new ArgumentException();
			}
			if (this.length > 0U)
			{
				UnsafeUtility.MemCpy((void*)other.ptr, (void*)this.ptr, (long)sizeof(T) * (long)((ulong)this.length));
			}
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x00058668 File Offset: 0x00056868
		public unsafe void CopyTo(List<T> buffer)
		{
			if (buffer.Capacity < buffer.Count + this.Length)
			{
				buffer.Capacity = buffer.Count + this.Length;
			}
			for (int i = 0; i < this.Length; i++)
			{
				buffer.Add(*this[i]);
			}
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x000586C0 File Offset: 0x000568C0
		public UnsafeSpan<T> Clone(Allocator allocator)
		{
			UnsafeSpan<T> unsafeSpan = new UnsafeSpan<T>(allocator, (int)this.length);
			this.CopyTo(unsafeSpan);
			return unsafeSpan;
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x000586E4 File Offset: 0x000568E4
		public unsafe T[] ToArray()
		{
			T[] array = new T[this.length];
			if (this.length > 0U)
			{
				T[] array2;
				T* ptr;
				if ((array2 = array) == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				UnsafeUtility.MemCpy((void*)ptr, (void*)this.ptr, (long)sizeof(T) * (long)((ulong)this.length));
				array2 = null;
			}
			return array;
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0005873C File Offset: 0x0005693C
		public void Free(Allocator allocator)
		{
			if (this.length > 0U)
			{
				AllocatorManager.Free<T>(allocator, this.ptr, (int)this.length);
			}
		}

		// Token: 0x04000AE2 RID: 2786
		[NativeDisableUnsafePtrRestriction]
		internal unsafe readonly T* ptr;

		// Token: 0x04000AE3 RID: 2787
		internal readonly uint length;
	}
}
