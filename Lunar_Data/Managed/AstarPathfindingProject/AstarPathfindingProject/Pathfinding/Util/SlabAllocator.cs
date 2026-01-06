using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Burst.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Pathfinding.Util
{
	// Token: 0x02000250 RID: 592
	public struct SlabAllocator<[IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000E08 RID: 3592 RVA: 0x00057DD9 File Offset: 0x00055FD9
		public bool IsCreated
		{
			get
			{
				return this.data != null;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000E09 RID: 3593 RVA: 0x00057DE8 File Offset: 0x00055FE8
		public unsafe int ByteSize
		{
			get
			{
				return this.data->mem.Length;
			}
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x00057DFA File Offset: 0x00055FFA
		public unsafe SlabAllocator(int initialCapacityBytes, AllocatorManager.AllocatorHandle allocator)
		{
			this.data = AllocatorManager.Allocate<SlabAllocator<T>.AllocatorData>(allocator, 1);
			this.data->mem = new UnsafeList<byte>(initialCapacityBytes, allocator, NativeArrayOptions.UninitializedMemory);
			this.Clear();
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00057E24 File Offset: 0x00056024
		public unsafe void Clear()
		{
			this.CheckDisposed();
			this.data->mem.Clear();
			for (int i = 0; i < 11; i++)
			{
				*((ref this.data->freeHeads.FixedElementField) + (IntPtr)i * 4) = -1;
			}
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x00057E6C File Offset: 0x0005606C
		public unsafe UnsafeSpan<T> GetSpan(int allocatedIndex)
		{
			this.CheckDisposed();
			if (allocatedIndex == -1)
			{
				return new UnsafeSpan<T>(null, 0);
			}
			byte* ptr = this.data->mem.Ptr + allocatedIndex;
			SlabAllocator<T>.Header* ptr2 = (SlabAllocator<T>.Header*)(ptr - sizeof(SlabAllocator<T>.Header));
			uint num = ptr2->length & 1073741823U;
			return new UnsafeSpan<T>((void*)ptr, (int)num);
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x00057EBA File Offset: 0x000560BA
		public SlabAllocator<T>.List GetList(int allocatedIndex)
		{
			return new SlabAllocator<T>.List(this, allocatedIndex);
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00057EC8 File Offset: 0x000560C8
		public unsafe void Realloc(ref int allocatedIndex, int nElements)
		{
			this.CheckDisposed();
			if (allocatedIndex == -1)
			{
				allocatedIndex = this.Allocate(nElements);
				return;
			}
			SlabAllocator<T>.Header* ptr = (SlabAllocator<T>.Header*)(this.data->mem.Ptr + allocatedIndex - sizeof(SlabAllocator<T>.Header));
			uint num = ptr->length & 1073741823U;
			int num2 = SlabAllocator<T>.ElementsToSizeIndex((int)num);
			int num3 = SlabAllocator<T>.ElementsToSizeIndex(nElements);
			if (num2 == num3)
			{
				ptr->length = (uint)(nElements | 1073741824 | int.MinValue);
				return;
			}
			int num4 = this.Allocate(nElements);
			UnsafeSpan<T> span = this.GetSpan(allocatedIndex);
			UnsafeSpan<T> span2 = this.GetSpan(num4);
			span.Slice(0, math.min((int)num, nElements)).CopyTo(span2);
			this.Free(allocatedIndex);
			allocatedIndex = num4;
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x00057F76 File Offset: 0x00056176
		internal static int SizeIndexToElements(int sizeIndex)
		{
			return 1 << sizeIndex;
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x00057F7E File Offset: 0x0005617E
		internal static int ElementsToSizeIndex(int nElements)
		{
			if (nElements < 0)
			{
				throw new Exception("SlabAllocator cannot allocate less than 1 element");
			}
			if (nElements == 0)
			{
				return 0;
			}
			int num = CollectionHelper.Log2Ceil(nElements);
			if (num > 10)
			{
				throw new Exception("SlabAllocator cannot allocate more than 2^(MaxAllocationSizeIndex-1) elements");
			}
			return num;
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x00057FAC File Offset: 0x000561AC
		public unsafe int Allocate(List<T> values)
		{
			int num = this.Allocate(values.Count);
			UnsafeSpan<T> span = this.GetSpan(num);
			for (int i = 0; i < span.Length; i++)
			{
				*span[i] = values[i];
			}
			return num;
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x00057FF8 File Offset: 0x000561F8
		public int Allocate(NativeList<T> values)
		{
			int num = this.Allocate(values.Length);
			this.GetSpan(num).CopyFrom(values.AsArray());
			return num;
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x00058028 File Offset: 0x00056228
		public unsafe int Allocate(int nElements)
		{
			this.CheckDisposed();
			if (nElements == 0)
			{
				return -1;
			}
			int num = SlabAllocator<T>.ElementsToSizeIndex(nElements);
			int num2 = *((ref this.data->freeHeads.FixedElementField) + (IntPtr)num * 4);
			if (num2 != -1)
			{
				byte* ptr = this.data->mem.Ptr;
				*((ref this.data->freeHeads.FixedElementField) + (IntPtr)num * 4) = ((SlabAllocator<T>.NextBlock*)(ptr + num2))->next;
				*(SlabAllocator<T>.Header*)(ptr + num2 - sizeof(SlabAllocator<T>.Header)) = new SlabAllocator<T>.Header
				{
					length = (uint)(nElements | int.MinValue | 1073741824)
				};
				return num2;
			}
			int length = this.data->mem.Length;
			int num3 = length + sizeof(SlabAllocator<T>.Header) + SlabAllocator<T>.SizeIndexToElements(num) * sizeof(T);
			if (Hint.Unlikely(num3 > this.data->mem.Capacity))
			{
				this.data->mem.SetCapacity(math.max(this.data->mem.Capacity * 2, num3));
			}
			this.data->mem.m_length = num3;
			*(SlabAllocator<T>.Header*)(this.data->mem.Ptr + length) = new SlabAllocator<T>.Header
			{
				length = (uint)(nElements | int.MinValue | 1073741824)
			};
			return length + sizeof(SlabAllocator<T>.Header);
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x0005817C File Offset: 0x0005637C
		public unsafe void Free(int allocatedIndex)
		{
			this.CheckDisposed();
			if (allocatedIndex == -1)
			{
				return;
			}
			byte* ptr = this.data->mem.Ptr;
			SlabAllocator<T>.Header* ptr2 = (SlabAllocator<T>.Header*)(ptr + allocatedIndex - sizeof(SlabAllocator<T>.Header));
			int num = SlabAllocator<T>.ElementsToSizeIndex((int)(ptr2->length & 1073741823U));
			*(SlabAllocator<T>.NextBlock*)(ptr + allocatedIndex) = new SlabAllocator<T>.NextBlock
			{
				next = *((ref this.data->freeHeads.FixedElementField) + (IntPtr)num * 4)
			};
			*((ref this.data->freeHeads.FixedElementField) + (IntPtr)num * 4) = allocatedIndex;
			ptr2->length &= 1073741823U;
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00058214 File Offset: 0x00056414
		public unsafe void CopyTo(SlabAllocator<T> other)
		{
			this.CheckDisposed();
			other.CheckDisposed();
			other.data->mem.CopyFrom(this.data->mem);
			for (int i = 0; i < 11; i++)
			{
				*((ref other.data->freeHeads.FixedElementField) + (IntPtr)i * 4) = *((ref this.data->freeHeads.FixedElementField) + (IntPtr)i * 4);
			}
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x000033F6 File Offset: 0x000015F6
		[MethodImpl(256)]
		private void CheckDisposed()
		{
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00058284 File Offset: 0x00056484
		public unsafe void Dispose()
		{
			if (this.data == null)
			{
				return;
			}
			AllocatorManager.AllocatorHandle allocator = this.data->mem.Allocator;
			this.data->mem.Dispose();
			AllocatorManager.Free<SlabAllocator<T>.AllocatorData>(allocator, this.data, 1);
			this.data = null;
		}

		// Token: 0x04000AD3 RID: 2771
		public const int MaxAllocationSizeIndex = 10;

		// Token: 0x04000AD4 RID: 2772
		private const uint UsedBit = 2147483648U;

		// Token: 0x04000AD5 RID: 2773
		private const uint AllocatedBit = 1073741824U;

		// Token: 0x04000AD6 RID: 2774
		private const uint LengthMask = 1073741823U;

		// Token: 0x04000AD7 RID: 2775
		public const int InvalidAllocation = -2;

		// Token: 0x04000AD8 RID: 2776
		public const int ZeroLengthArray = -1;

		// Token: 0x04000AD9 RID: 2777
		[NativeDisableUnsafePtrRestriction]
		private unsafe SlabAllocator<T>.AllocatorData* data;

		// Token: 0x02000251 RID: 593
		private struct AllocatorData
		{
			// Token: 0x04000ADA RID: 2778
			public UnsafeList<byte> mem;

			// Token: 0x04000ADB RID: 2779
			[FixedBuffer(typeof(int), 11)]
			public SlabAllocator<T>.AllocatorData.<freeHeads>e__FixedBuffer freeHeads;

			// Token: 0x02000252 RID: 594
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(0, Size = 44)]
			public struct <freeHeads>e__FixedBuffer
			{
				// Token: 0x04000ADC RID: 2780
				public int FixedElementField;
			}
		}

		// Token: 0x02000253 RID: 595
		private struct Header
		{
			// Token: 0x04000ADD RID: 2781
			public uint length;
		}

		// Token: 0x02000254 RID: 596
		private struct NextBlock
		{
			// Token: 0x04000ADE RID: 2782
			public int next;
		}

		// Token: 0x02000255 RID: 597
		public ref struct List
		{
			// Token: 0x06000E18 RID: 3608 RVA: 0x000582D0 File Offset: 0x000564D0
			public List(SlabAllocator<T> allocator, int allocationIndex)
			{
				this.span = allocator.GetSpan(allocationIndex);
				this.allocator = allocator;
				this.allocationIndex = allocationIndex;
			}

			// Token: 0x06000E19 RID: 3609 RVA: 0x000582F0 File Offset: 0x000564F0
			public unsafe void Add(T value)
			{
				this.allocator.Realloc(ref this.allocationIndex, this.span.Length + 1);
				this.span = this.allocator.GetSpan(this.allocationIndex);
				*this.span[this.span.Length - 1] = value;
			}

			// Token: 0x06000E1A RID: 3610 RVA: 0x00058350 File Offset: 0x00056550
			public void RemoveAt(int index)
			{
				this.span.Slice(index + 1).CopyTo(this.span.Slice(index, this.span.Length - index - 1));
				this.allocator.Realloc(ref this.allocationIndex, this.span.Length - 1);
				this.span = this.allocator.GetSpan(this.allocationIndex);
			}

			// Token: 0x06000E1B RID: 3611 RVA: 0x000583C3 File Offset: 0x000565C3
			public void Clear()
			{
				this.allocator.Realloc(ref this.allocationIndex, 0);
				this.span = this.allocator.GetSpan(this.allocationIndex);
			}

			// Token: 0x170001EB RID: 491
			// (get) Token: 0x06000E1C RID: 3612 RVA: 0x000583EE File Offset: 0x000565EE
			public int Length
			{
				get
				{
					return this.span.Length;
				}
			}

			// Token: 0x170001EC RID: 492
			public ref T this[int index]
			{
				[MethodImpl(256)]
				get
				{
					return this.span[index];
				}
			}

			// Token: 0x04000ADF RID: 2783
			public UnsafeSpan<T> span;

			// Token: 0x04000AE0 RID: 2784
			private SlabAllocator<T> allocator;

			// Token: 0x04000AE1 RID: 2785
			public int allocationIndex;
		}
	}
}
