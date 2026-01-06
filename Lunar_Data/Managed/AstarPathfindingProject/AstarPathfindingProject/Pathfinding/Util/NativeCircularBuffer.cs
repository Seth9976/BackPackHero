using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Pathfinding.Util
{
	// Token: 0x0200024D RID: 589
	public struct NativeCircularBuffer<[IsUnmanaged] T> : IReadOnlyList<T>, IEnumerable<T>, IEnumerable, IReadOnlyCollection<T> where T : struct, ValueType
	{
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x000576A9 File Offset: 0x000558A9
		public readonly int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000DDB RID: 3547 RVA: 0x000576B1 File Offset: 0x000558B1
		public readonly int AbsoluteStartIndex
		{
			get
			{
				return this.head;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x000576B9 File Offset: 0x000558B9
		public readonly int AbsoluteEndIndex
		{
			get
			{
				return this.head + this.length - 1;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x000576CA File Offset: 0x000558CA
		public unsafe readonly ref T First
		{
			[MethodImpl(256)]
			get
			{
				return ref this.data[(IntPtr)(this.head & this.capacityMask) * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x000576E8 File Offset: 0x000558E8
		public unsafe readonly ref T Last
		{
			[MethodImpl(256)]
			get
			{
				return ref this.data[(IntPtr)((this.head + this.length - 1) & this.capacityMask) * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x0005770F File Offset: 0x0005590F
		int IReadOnlyCollection<T>.Count
		{
			get
			{
				return this.Length;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x00057717 File Offset: 0x00055917
		public readonly bool IsCreated
		{
			get
			{
				return this.data != null;
			}
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x00057726 File Offset: 0x00055926
		public NativeCircularBuffer(AllocatorManager.AllocatorHandle allocator)
		{
			this.data = null;
			this.Allocator = allocator;
			this.capacityMask = -1;
			this.head = 0;
			this.length = 0;
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0005774C File Offset: 0x0005594C
		public NativeCircularBuffer(int initialCapacity, AllocatorManager.AllocatorHandle allocator)
		{
			initialCapacity = math.ceilpow2(initialCapacity);
			this.data = AllocatorManager.Allocate<T>(allocator, initialCapacity);
			this.capacityMask = initialCapacity - 1;
			this.Allocator = allocator;
			this.head = 0;
			this.length = 0;
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x00057781 File Offset: 0x00055981
		public NativeCircularBuffer(CircularBuffer<T> buffer, out ulong gcHandle)
		{
			this = new NativeCircularBuffer<T>(buffer.data, buffer.head, buffer.Length, out gcHandle);
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0005779D File Offset: 0x0005599D
		public unsafe NativeCircularBuffer(T[] data, int head, int length, out ulong gcHandle)
		{
			this.data = (T*)UnsafeUtility.PinGCArrayAndGetDataAddress(data, out gcHandle);
			this.capacityMask = data.Length - 1;
			this.head = head;
			this.length = length;
			this.Allocator = Unity.Collections.Allocator.None;
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x000577D2 File Offset: 0x000559D2
		public void Clear()
		{
			this.length = 0;
			this.head = 0;
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x000577E4 File Offset: 0x000559E4
		public void AddRange(List<T> items)
		{
			for (int i = 0; i < items.Count; i++)
			{
				this.PushEnd(items[i]);
			}
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x0005780F File Offset: 0x00055A0F
		[MethodImpl(256)]
		public void PushStart(T item)
		{
			if (this.length > this.capacityMask)
			{
				this.Grow();
			}
			this.length++;
			this.head--;
			this[0] = item;
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x00057849 File Offset: 0x00055A49
		[MethodImpl(256)]
		public void PushEnd(T item)
		{
			if (this.length > this.capacityMask)
			{
				this.Grow();
			}
			this.length++;
			this[this.length - 1] = item;
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0005787C File Offset: 0x00055A7C
		[MethodImpl(256)]
		public void Push(bool toStart, T item)
		{
			if (toStart)
			{
				this.PushStart(item);
				return;
			}
			this.PushEnd(item);
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00057890 File Offset: 0x00055A90
		public T PopStart()
		{
			T t = this[0];
			this.head++;
			this.length--;
			return t;
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x000578B5 File Offset: 0x00055AB5
		public T PopEnd()
		{
			T t = this[this.length - 1];
			this.length--;
			return t;
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x000578D3 File Offset: 0x00055AD3
		public T Pop(bool fromStart)
		{
			if (fromStart)
			{
				return this.PopStart();
			}
			return this.PopEnd();
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x000578E5 File Offset: 0x00055AE5
		public readonly T GetBoundaryValue(bool start)
		{
			if (!start)
			{
				return this.GetAbsolute(this.AbsoluteEndIndex);
			}
			return this.GetAbsolute(this.AbsoluteStartIndex);
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x00057903 File Offset: 0x00055B03
		public void TrimTo(int length)
		{
			this.length = math.min(this.length, length);
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x00057917 File Offset: 0x00055B17
		public void Splice(int startIndex, int toRemove, List<T> toInsert)
		{
			this.SpliceAbsolute(startIndex + this.head, toRemove, toInsert);
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x0005792C File Offset: 0x00055B2C
		public unsafe void SpliceAbsolute(int startIndex, int toRemove, List<T> toInsert)
		{
			this.SpliceUninitializedAbsolute(startIndex, toRemove, toInsert.Count);
			for (int i = 0; i < toInsert.Count; i++)
			{
				this.data[(IntPtr)((startIndex + i) & this.capacityMask) * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = toInsert[i];
			}
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0005797D File Offset: 0x00055B7D
		public void SpliceUninitialized(int startIndex, int toRemove, int toInsert)
		{
			this.SpliceUninitializedAbsolute(startIndex + this.head, toRemove, toInsert);
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00057990 File Offset: 0x00055B90
		public void SpliceUninitializedAbsolute(int startIndex, int toRemove, int toInsert)
		{
			int num = toInsert - toRemove;
			while (this.length + num > this.capacityMask + 1)
			{
				this.Grow();
			}
			this.MoveAbsolute(startIndex + toRemove, this.AbsoluteEndIndex, num);
			this.length += num;
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x000579DC File Offset: 0x00055BDC
		private unsafe void MoveAbsolute(int startIndex, int endIndex, int deltaIndex)
		{
			if (deltaIndex > 0)
			{
				for (int i = endIndex; i >= startIndex; i--)
				{
					this.data[(IntPtr)((i + deltaIndex) & this.capacityMask) * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = this.data[(IntPtr)(i & this.capacityMask) * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
				}
				return;
			}
			if (deltaIndex < 0)
			{
				for (int j = startIndex; j <= endIndex; j++)
				{
					this.data[(IntPtr)((j + deltaIndex) & this.capacityMask) * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = this.data[(IntPtr)(j & this.capacityMask) * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
				}
			}
		}

		// Token: 0x170001E4 RID: 484
		public unsafe T this[int index]
		{
			[MethodImpl(256)]
			readonly get
			{
				return this.data[(IntPtr)((index + this.head) & this.capacityMask) * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
			}
			[MethodImpl(256)]
			set
			{
				this.data[(IntPtr)((index + this.head) & this.capacityMask) * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = value;
			}
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x00057AC9 File Offset: 0x00055CC9
		[MethodImpl(256)]
		public unsafe readonly T GetAbsolute(int index)
		{
			return this.data[(IntPtr)(index & this.capacityMask) * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x00057AE8 File Offset: 0x00055CE8
		[MethodImpl(8)]
		private unsafe void Grow()
		{
			int num = this.capacityMask + 1;
			int num2 = math.max(4, num * 2);
			T* ptr = AllocatorManager.Allocate<T>(this.Allocator, num2);
			if (this.data != null)
			{
				int num3 = num - (this.head & this.capacityMask);
				UnsafeUtility.MemCpy((void*)(ptr + (IntPtr)(this.head & (num2 - 1)) * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)), (void*)(this.data + (IntPtr)(this.head & this.capacityMask) * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)), (long)(num3 * sizeof(T)));
				int num4 = this.length - num3;
				if (num4 > 0)
				{
					UnsafeUtility.MemCpy((void*)(ptr + (IntPtr)((this.head + num3) & (num2 - 1)) * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)), (void*)this.data, (long)(num4 * sizeof(T)));
				}
				AllocatorManager.Free<T>(this.Allocator, this.data, 1);
			}
			this.capacityMask = num2 - 1;
			this.data = ptr;
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x00057BCC File Offset: 0x00055DCC
		public void Dispose()
		{
			this.capacityMask = -1;
			this.length = 0;
			this.head = 0;
			AllocatorManager.Free<T>(this.Allocator, this.data, 1);
			this.data = null;
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00057BFD File Offset: 0x00055DFD
		public IEnumerator<T> GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.length; i = num + 1)
			{
				yield return this[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00057C11 File Offset: 0x00055E11
		IEnumerator IEnumerable.GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.length; i = num + 1)
			{
				yield return this[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x00057C28 File Offset: 0x00055E28
		public unsafe NativeCircularBuffer<T> Clone()
		{
			T* ptr = AllocatorManager.Allocate<T>(this.Allocator, this.capacityMask + 1);
			UnsafeUtility.MemCpy((void*)ptr, (void*)this.data, (long)(this.length * sizeof(T)));
			return new NativeCircularBuffer<T>
			{
				data = ptr,
				head = this.head,
				length = this.length,
				capacityMask = this.capacityMask,
				Allocator = this.Allocator
			};
		}

		// Token: 0x04000AC6 RID: 2758
		[NativeDisableUnsafePtrRestriction]
		internal unsafe T* data;

		// Token: 0x04000AC7 RID: 2759
		internal int head;

		// Token: 0x04000AC8 RID: 2760
		private int length;

		// Token: 0x04000AC9 RID: 2761
		private int capacityMask;

		// Token: 0x04000ACA RID: 2762
		public AllocatorManager.AllocatorHandle Allocator;
	}
}
