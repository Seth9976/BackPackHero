using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Jobs;
using Unity.Mathematics;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000123 RID: 291
	[DebuggerDisplay("Length = {Length}, Capacity = {Capacity}, IsCreated = {IsCreated}, IsEmpty = {IsEmpty}")]
	[DebuggerTypeProxy(typeof(UnsafeListTDebugView<>))]
	[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
	public struct UnsafeList<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : INativeDisposable, IDisposable, INativeList<T>, IIndexable<T>, IEnumerable<T>, IEnumerable where T : struct, ValueType
	{
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x0001FACB File Offset: 0x0001DCCB
		// (set) Token: 0x06000A9E RID: 2718 RVA: 0x0001FAD8 File Offset: 0x0001DCD8
		public int Length
		{
			get
			{
				return CollectionHelper.AssumePositive(this.m_length);
			}
			set
			{
				if (value > this.Capacity)
				{
					this.Resize(value, NativeArrayOptions.UninitializedMemory);
					return;
				}
				this.m_length = value;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x0001FAF3 File Offset: 0x0001DCF3
		// (set) Token: 0x06000AA0 RID: 2720 RVA: 0x0001FB00 File Offset: 0x0001DD00
		public int Capacity
		{
			get
			{
				return CollectionHelper.AssumePositive(this.m_capacity);
			}
			set
			{
				this.SetCapacity(value);
			}
		}

		// Token: 0x17000129 RID: 297
		public unsafe T this[int index]
		{
			get
			{
				return this.Ptr[(IntPtr)CollectionHelper.AssumePositive(index) * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
			}
			set
			{
				this.Ptr[(IntPtr)CollectionHelper.AssumePositive(index) * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = value;
			}
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0001FB42 File Offset: 0x0001DD42
		public unsafe ref T ElementAt(int index)
		{
			return ref this.Ptr[(IntPtr)CollectionHelper.AssumePositive(index) * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0001FB59 File Offset: 0x0001DD59
		public unsafe UnsafeList(T* ptr, int length)
		{
			this = default(UnsafeList<T>);
			this.Ptr = ptr;
			this.m_length = length;
			this.m_capacity = 0;
			this.Allocator = AllocatorManager.None;
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0001FB84 File Offset: 0x0001DD84
		public unsafe UnsafeList(int initialCapacity, AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory)
		{
			this = default(UnsafeList<T>);
			this.Ptr = null;
			this.m_length = 0;
			this.m_capacity = 0;
			this.Allocator = allocator;
			if (initialCapacity != 0)
			{
				this.SetCapacity(initialCapacity);
			}
			if (options == NativeArrayOptions.ClearMemory && this.Ptr != null)
			{
				int num = sizeof(T);
				UnsafeUtility.MemClear((void*)this.Ptr, (long)(this.Capacity * num));
			}
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0001FBE8 File Offset: 0x0001DDE8
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(AllocatorManager.AllocatorHandle) })]
		internal void Initialize<[global::System.Runtime.CompilerServices.IsUnmanaged] U>(int initialCapacity, ref U allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			this.Ptr = null;
			this.m_length = 0;
			this.m_capacity = 0;
			this.Allocator = AllocatorManager.None;
			this.Initialize<U>(initialCapacity, ref allocator, options);
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0001FC14 File Offset: 0x0001DE14
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(AllocatorManager.AllocatorHandle) })]
		internal static UnsafeList<T> New<[global::System.Runtime.CompilerServices.IsUnmanaged] U>(int initialCapacity, ref U allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			UnsafeList<T> unsafeList = default(UnsafeList<T>);
			unsafeList.Initialize<U>(initialCapacity, ref allocator, options);
			return unsafeList;
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0001FC34 File Offset: 0x0001DE34
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(AllocatorManager.AllocatorHandle) })]
		internal unsafe static UnsafeList<T>* Create<[global::System.Runtime.CompilerServices.IsUnmanaged] U>(int initialCapacity, ref U allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			UnsafeList<T>* ptr = (ref allocator).Allocate(default(UnsafeList<T>), 1);
			UnsafeUtility.MemClear((void*)ptr, (long)sizeof(UnsafeList<T>));
			ptr->Allocator = allocator.Handle;
			if (initialCapacity != 0)
			{
				ptr->SetCapacity<U>(ref allocator, initialCapacity);
			}
			if (options == NativeArrayOptions.ClearMemory && ptr->Ptr != null)
			{
				int num = sizeof(T);
				UnsafeUtility.MemClear((void*)ptr->Ptr, (long)(ptr->Capacity * num));
			}
			return ptr;
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0001FCA6 File Offset: 0x0001DEA6
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(AllocatorManager.AllocatorHandle) })]
		internal unsafe static void Destroy<[global::System.Runtime.CompilerServices.IsUnmanaged] U>(UnsafeList<T>* listData, ref U allocator) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			listData->Dispose<U>(ref allocator);
			(ref allocator).Free((void*)listData, sizeof(UnsafeList<T>), UnsafeUtility.AlignOf<UnsafeList<T>>(), 1);
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0001FCC2 File Offset: 0x0001DEC2
		public unsafe static UnsafeList<T>* Create(int initialCapacity, AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory)
		{
			UnsafeList<T>* ptr = AllocatorManager.Allocate<UnsafeList<T>>(allocator, 1);
			*ptr = new UnsafeList<T>(initialCapacity, allocator, options);
			return ptr;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0001FCD9 File Offset: 0x0001DED9
		public unsafe static void Destroy(UnsafeList<T>* listData)
		{
			AllocatorManager.AllocatorHandle allocator = listData->Allocator;
			listData->Dispose();
			AllocatorManager.Free<UnsafeList<T>>(allocator, listData, 1);
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x0001FCEE File Offset: 0x0001DEEE
		public bool IsEmpty
		{
			get
			{
				return !this.IsCreated || this.m_length == 0;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x0001FD03 File Offset: 0x0001DF03
		public bool IsCreated
		{
			get
			{
				return this.Ptr != null;
			}
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0001FD12 File Offset: 0x0001DF12
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(AllocatorManager.AllocatorHandle) })]
		internal void Dispose<[global::System.Runtime.CompilerServices.IsUnmanaged] U>(ref U allocator) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			(ref allocator).Free(this.Ptr, this.m_length);
			this.Ptr = null;
			this.m_length = 0;
			this.m_capacity = 0;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0001FD3C File Offset: 0x0001DF3C
		public void Dispose()
		{
			if (CollectionHelper.ShouldDeallocate(this.Allocator))
			{
				AllocatorManager.Free<T>(this.Allocator, this.Ptr, 1);
				this.Allocator = AllocatorManager.Invalid;
			}
			this.Ptr = null;
			this.m_length = 0;
			this.m_capacity = 0;
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0001FD8C File Offset: 0x0001DF8C
		[NotBurstCompatible]
		public unsafe JobHandle Dispose(JobHandle inputDeps)
		{
			if (CollectionHelper.ShouldDeallocate(this.Allocator))
			{
				JobHandle jobHandle = new UnsafeDisposeJob
				{
					Ptr = (void*)this.Ptr,
					Allocator = this.Allocator
				}.Schedule(inputDeps);
				this.Ptr = null;
				this.Allocator = AllocatorManager.Invalid;
				return jobHandle;
			}
			this.Ptr = null;
			return inputDeps;
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0001FDEC File Offset: 0x0001DFEC
		public void Clear()
		{
			this.m_length = 0;
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0001FDF8 File Offset: 0x0001DFF8
		public unsafe void Resize(int length, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory)
		{
			int num = this.m_length;
			if (length > this.Capacity)
			{
				this.SetCapacity(length);
			}
			this.m_length = length;
			if (options == NativeArrayOptions.ClearMemory && num < length)
			{
				int num2 = length - num;
				byte* ptr = (byte*)this.Ptr;
				int num3 = sizeof(T);
				UnsafeUtility.MemClear((void*)(ptr + num * num3), (long)(num2 * num3));
			}
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0001FE4C File Offset: 0x0001E04C
		private unsafe void Realloc<[global::System.Runtime.CompilerServices.IsUnmanaged] U>(ref U allocator, int newCapacity) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			T* ptr = null;
			int num = UnsafeUtility.AlignOf<T>();
			int num2 = sizeof(T);
			if (newCapacity > 0)
			{
				ptr = (T*)(ref allocator).Allocate(num2, num, newCapacity);
				if (this.m_capacity > 0)
				{
					int num3 = math.min(newCapacity, this.Capacity) * num2;
					UnsafeUtility.MemCpy((void*)ptr, (void*)this.Ptr, (long)num3);
				}
			}
			(ref allocator).Free(this.Ptr, this.Capacity);
			this.Ptr = ptr;
			this.m_capacity = newCapacity;
			this.m_length = math.min(this.m_length, newCapacity);
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0001FECF File Offset: 0x0001E0CF
		private void Realloc(int capacity)
		{
			this.Realloc<AllocatorManager.AllocatorHandle>(ref this.Allocator, capacity);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0001FEE0 File Offset: 0x0001E0E0
		private void SetCapacity<[global::System.Runtime.CompilerServices.IsUnmanaged] U>(ref U allocator, int capacity) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			int num = sizeof(T);
			int num2 = math.max(capacity, 64 / num);
			num2 = math.ceilpow2(num2);
			if (num2 == this.Capacity)
			{
				return;
			}
			this.Realloc<U>(ref allocator, num2);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0001FF18 File Offset: 0x0001E118
		public void SetCapacity(int capacity)
		{
			this.SetCapacity<AllocatorManager.AllocatorHandle>(ref this.Allocator, capacity);
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0001FF27 File Offset: 0x0001E127
		public void TrimExcess()
		{
			if (this.Capacity != this.m_length)
			{
				this.Realloc(this.m_length);
			}
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0001FF43 File Offset: 0x0001E143
		public unsafe void AddNoResize(T value)
		{
			UnsafeUtility.WriteArrayElement<T>((void*)this.Ptr, this.m_length, value);
			this.m_length++;
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0001FF68 File Offset: 0x0001E168
		public unsafe void AddRangeNoResize(void* ptr, int count)
		{
			int num = sizeof(T);
			void* ptr2 = (void*)(this.Ptr + this.m_length * num / sizeof(T));
			UnsafeUtility.MemCpy(ptr2, ptr, (long)(count * num));
			this.m_length += count;
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0001FFA5 File Offset: 0x0001E1A5
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe void AddRangeNoResize(UnsafeList<T> list)
		{
			this.AddRangeNoResize((void*)list.Ptr, CollectionHelper.AssumePositive(list.m_length));
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0001FFC0 File Offset: 0x0001E1C0
		public unsafe void Add(in T value)
		{
			int num = this.m_length;
			if (this.m_length + 1 > this.Capacity)
			{
				this.Resize(num + 1, NativeArrayOptions.UninitializedMemory);
			}
			else
			{
				this.m_length++;
			}
			UnsafeUtility.WriteArrayElement<T>((void*)this.Ptr, num, value);
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00020010 File Offset: 0x0001E210
		public unsafe void AddRange(void* ptr, int count)
		{
			int num = this.m_length;
			if (this.m_length + count > this.Capacity)
			{
				this.Resize(this.m_length + count, NativeArrayOptions.UninitializedMemory);
			}
			else
			{
				this.m_length += count;
			}
			int num2 = sizeof(T);
			void* ptr2 = (void*)(this.Ptr + num * num2 / sizeof(T));
			UnsafeUtility.MemCpy(ptr2, ptr, (long)(count * num2));
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00020070 File Offset: 0x0001E270
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe void AddRange(UnsafeList<T> list)
		{
			this.AddRange((void*)list.Ptr, list.Length);
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00020088 File Offset: 0x0001E288
		public unsafe void InsertRangeWithBeginEnd(int begin, int end)
		{
			int num = end - begin;
			if (num < 1)
			{
				return;
			}
			int num2 = this.m_length;
			if (this.m_length + num > this.Capacity)
			{
				this.Resize(this.m_length + num, NativeArrayOptions.UninitializedMemory);
			}
			else
			{
				this.m_length += num;
			}
			int num3 = num2 - begin;
			if (num3 < 1)
			{
				return;
			}
			int num4 = sizeof(T);
			int num5 = num3 * num4;
			byte* ptr = (byte*)this.Ptr;
			void* ptr2 = (void*)(ptr + end * num4);
			byte* ptr3 = ptr + begin * num4;
			UnsafeUtility.MemMove(ptr2, (void*)ptr3, (long)num5);
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00020105 File Offset: 0x0001E305
		public void RemoveAtSwapBack(int index)
		{
			this.RemoveRangeSwapBack(index, 1);
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00020110 File Offset: 0x0001E310
		public unsafe void RemoveRangeSwapBack(int index, int count)
		{
			if (count > 0)
			{
				int num = math.max(this.m_length - count, index + count);
				int num2 = sizeof(T);
				void* ptr = (void*)(this.Ptr + index * num2 / sizeof(T));
				void* ptr2 = (void*)(this.Ptr + num * num2 / sizeof(T));
				UnsafeUtility.MemCpy(ptr, ptr2, (long)((this.m_length - num) * num2));
				this.m_length -= count;
			}
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00020170 File Offset: 0x0001E370
		[Obsolete("RemoveRangeSwapBackWithBeginEnd(begin, end) is deprecated, use RemoveRangeSwapBack(index, count) instead. (RemovedAfter 2021-06-02)", false)]
		public unsafe void RemoveRangeSwapBackWithBeginEnd(int begin, int end)
		{
			int num = end - begin;
			if (num > 0)
			{
				int num2 = math.max(this.m_length - num, end);
				int num3 = sizeof(T);
				void* ptr = (void*)(this.Ptr + begin * num3 / sizeof(T));
				void* ptr2 = (void*)(this.Ptr + num2 * num3 / sizeof(T));
				UnsafeUtility.MemCpy(ptr, ptr2, (long)((this.m_length - num2) * num3));
				this.m_length -= num;
			}
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x000201D3 File Offset: 0x0001E3D3
		public void RemoveAt(int index)
		{
			this.RemoveRange(index, 1);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x000201E0 File Offset: 0x0001E3E0
		public unsafe void RemoveRange(int index, int count)
		{
			if (count > 0)
			{
				int num = math.min(index + count, this.m_length);
				int num2 = sizeof(T);
				void* ptr = (void*)(this.Ptr + index * num2 / sizeof(T));
				void* ptr2 = (void*)(this.Ptr + num * num2 / sizeof(T));
				UnsafeUtility.MemCpy(ptr, ptr2, (long)((this.m_length - num) * num2));
				this.m_length -= count;
			}
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00020240 File Offset: 0x0001E440
		[Obsolete("RemoveRangeWithBeginEnd(begin, end) is deprecated, use RemoveRange(index, count) instead. (RemovedAfter 2021-06-02)", false)]
		public unsafe void RemoveRangeWithBeginEnd(int begin, int end)
		{
			int num = end - begin;
			if (num > 0)
			{
				int num2 = math.min(begin + num, this.m_length);
				int num3 = sizeof(T);
				void* ptr = (void*)(this.Ptr + begin * num3 / sizeof(T));
				void* ptr2 = (void*)(this.Ptr + num2 * num3 / sizeof(T));
				UnsafeUtility.MemCpy(ptr, ptr2, (long)((this.m_length - num2) * num3));
				this.m_length -= num;
			}
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x000202A3 File Offset: 0x0001E4A3
		public UnsafeList<T>.ParallelReader AsParallelReader()
		{
			return new UnsafeList<T>.ParallelReader(this.Ptr, this.Length);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x000202B6 File Offset: 0x0001E4B6
		public unsafe UnsafeList<T>.ParallelWriter AsParallelWriter()
		{
			return new UnsafeList<T>.ParallelWriter((UnsafeList<T>*)UnsafeUtility.AddressOf<UnsafeList<T>>(ref this));
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x000202C3 File Offset: 0x0001E4C3
		public unsafe void CopyFrom(UnsafeList<T> array)
		{
			this.Resize(array.Length, NativeArrayOptions.UninitializedMemory);
			UnsafeUtility.MemCpy((void*)this.Ptr, (void*)array.Ptr, (long)(UnsafeUtility.SizeOf<T>() * this.Length));
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x000202F4 File Offset: 0x0001E4F4
		public UnsafeList<T>.Enumerator GetEnumerator()
		{
			return new UnsafeList<T>.Enumerator
			{
				m_Ptr = this.Ptr,
				m_Length = this.Length,
				m_Index = -1
			};
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00004A65 File Offset: 0x00002C65
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00004A65 File Offset: 0x00002C65
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0001D2EF File Offset: 0x0001B4EF
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		internal unsafe static void CheckNull(void* listData)
		{
			if (listData == null)
			{
				throw new Exception("UnsafeList has yet to be created or has been destroyed!");
			}
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0002032C File Offset: 0x0001E52C
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckIndexCount(int index, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("Value for cound {0} must be positive.", count));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("Value for index {0} must be positive.", index));
			}
			if (index > this.Length)
			{
				throw new ArgumentOutOfRangeException(string.Format("Value for index {0} is out of bounds.", index));
			}
			if (index + count > this.Length)
			{
				throw new ArgumentOutOfRangeException(string.Format("Value for count {0} is out of bounds.", count));
			}
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x000203B0 File Offset: 0x0001E5B0
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckBeginEnd(int begin, int end)
		{
			if (begin > end)
			{
				throw new ArgumentException(string.Format("Value for begin {0} index must less or equal to end {1}.", begin, end));
			}
			if (begin < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("Value for begin {0} must be positive.", begin));
			}
			if (begin > this.Length)
			{
				throw new ArgumentOutOfRangeException(string.Format("Value for begin {0} is out of bounds.", begin));
			}
			if (end > this.Length)
			{
				throw new ArgumentOutOfRangeException(string.Format("Value for end {0} is out of bounds.", end));
			}
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00002C2B File Offset: 0x00000E2B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckNoResizeHasEnoughCapacity(int length)
		{
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00020435 File Offset: 0x0001E635
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckNoResizeHasEnoughCapacity(int length, int index)
		{
			if (this.Capacity < index + length)
			{
				throw new Exception(string.Format("AddNoResize assumes that list capacity is sufficient (Capacity {0}, Length {1}), requested length {2}!", this.Capacity, this.Length, length));
			}
		}

		// Token: 0x04000380 RID: 896
		[NativeDisableUnsafePtrRestriction]
		public unsafe T* Ptr;

		// Token: 0x04000381 RID: 897
		public int m_length;

		// Token: 0x04000382 RID: 898
		public int m_capacity;

		// Token: 0x04000383 RID: 899
		public AllocatorManager.AllocatorHandle Allocator;

		// Token: 0x04000384 RID: 900
		[Obsolete("Use Length property (UnityUpgradable) -> Length", true)]
		public int length;

		// Token: 0x04000385 RID: 901
		[Obsolete("Use Capacity property (UnityUpgradable) -> Capacity", true)]
		public int capacity;

		// Token: 0x02000124 RID: 292
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		public struct ParallelReader
		{
			// Token: 0x06000AD0 RID: 2768 RVA: 0x0002046E File Offset: 0x0001E66E
			internal unsafe ParallelReader(T* ptr, int length)
			{
				this.Ptr = ptr;
				this.Length = length;
			}

			// Token: 0x04000386 RID: 902
			[NativeDisableUnsafePtrRestriction]
			public unsafe readonly T* Ptr;

			// Token: 0x04000387 RID: 903
			public readonly int Length;
		}

		// Token: 0x02000125 RID: 293
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		public struct ParallelWriter
		{
			// Token: 0x1700012C RID: 300
			// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x0002047E File Offset: 0x0001E67E
			public unsafe readonly void* Ptr
			{
				get
				{
					return (void*)this.ListData->Ptr;
				}
			}

			// Token: 0x06000AD2 RID: 2770 RVA: 0x0002048B File Offset: 0x0001E68B
			internal unsafe ParallelWriter(UnsafeList<T>* listData)
			{
				this.ListData = listData;
			}

			// Token: 0x06000AD3 RID: 2771 RVA: 0x00020494 File Offset: 0x0001E694
			[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
			public unsafe void AddNoResize(T value)
			{
				int num = Interlocked.Increment(ref this.ListData->m_length) - 1;
				UnsafeUtility.WriteArrayElement<T>((void*)this.ListData->Ptr, num, value);
			}

			// Token: 0x06000AD4 RID: 2772 RVA: 0x000204C8 File Offset: 0x0001E6C8
			[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
			public unsafe void AddRangeNoResize(void* ptr, int count)
			{
				int num = Interlocked.Add(ref this.ListData->m_length, count) - count;
				void* ptr2 = (void*)(this.ListData->Ptr + num * sizeof(T) / sizeof(T));
				UnsafeUtility.MemCpy(ptr2, ptr, (long)(count * sizeof(T)));
			}

			// Token: 0x06000AD5 RID: 2773 RVA: 0x0002050E File Offset: 0x0001E70E
			[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
			public unsafe void AddRangeNoResize(UnsafeList<T> list)
			{
				this.AddRangeNoResize((void*)list.Ptr, list.Length);
			}

			// Token: 0x04000388 RID: 904
			[NativeDisableUnsafePtrRestriction]
			public unsafe UnsafeList<T>* ListData;
		}

		// Token: 0x02000126 RID: 294
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06000AD6 RID: 2774 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x06000AD7 RID: 2775 RVA: 0x00020524 File Offset: 0x0001E724
			public bool MoveNext()
			{
				int num = this.m_Index + 1;
				this.m_Index = num;
				return num < this.m_Length;
			}

			// Token: 0x06000AD8 RID: 2776 RVA: 0x0002054A File Offset: 0x0001E74A
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x1700012D RID: 301
			// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x00020553 File Offset: 0x0001E753
			public unsafe T Current
			{
				get
				{
					return this.m_Ptr[(IntPtr)this.m_Index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
				}
			}

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x06000ADA RID: 2778 RVA: 0x0002056F File Offset: 0x0001E76F
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000389 RID: 905
			internal unsafe T* m_Ptr;

			// Token: 0x0400038A RID: 906
			internal int m_Length;

			// Token: 0x0400038B RID: 907
			internal int m_Index;
		}
	}
}
