using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Jobs;
using Unity.Mathematics;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x020000FC RID: 252
	[DebuggerDisplay("Length = {Length}, Capacity = {Capacity}, IsCreated = {IsCreated}, IsEmpty = {IsEmpty}")]
	[Obsolete("Untyped UnsafeList is deprecated, please use UnsafeList<T> instead. (RemovedAfter 2021-05-18)", false)]
	public struct UnsafeList : INativeDisposable, IDisposable
	{
		// Token: 0x0600095E RID: 2398 RVA: 0x0001CAD9 File Offset: 0x0001ACD9
		public UnsafeList(Allocator allocator)
		{
			this = default(UnsafeList);
			this.Ptr = null;
			this.Length = 0;
			this.Capacity = 0;
			this.Allocator = allocator;
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0001CB04 File Offset: 0x0001AD04
		public unsafe UnsafeList(void* ptr, int length)
		{
			this = default(UnsafeList);
			this.Ptr = ptr;
			this.Length = length;
			this.Capacity = length;
			this.Allocator = Unity.Collections.Allocator.None;
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0001CB30 File Offset: 0x0001AD30
		internal void Initialize<[global::System.Runtime.CompilerServices.IsUnmanaged] U>(int sizeOf, int alignOf, int initialCapacity, ref U allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			this.Allocator = allocator.Handle;
			this.Ptr = null;
			this.Length = 0;
			this.Capacity = 0;
			if (initialCapacity != 0)
			{
				this.SetCapacity<U>(ref allocator, sizeOf, alignOf, initialCapacity);
			}
			if (options == NativeArrayOptions.ClearMemory && this.Ptr != null)
			{
				UnsafeUtility.MemClear(this.Ptr, (long)(this.Capacity * sizeOf));
			}
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0001CB98 File Offset: 0x0001AD98
		internal static UnsafeList New<[global::System.Runtime.CompilerServices.IsUnmanaged] U>(int sizeOf, int alignOf, int initialCapacity, ref U allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			UnsafeList unsafeList = default(UnsafeList);
			unsafeList.Initialize<U>(sizeOf, alignOf, initialCapacity, ref allocator, options);
			return unsafeList;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0001CBBB File Offset: 0x0001ADBB
		public UnsafeList(int sizeOf, int alignOf, int initialCapacity, AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory)
		{
			this = default(UnsafeList);
			this = default(UnsafeList);
			this.Initialize<AllocatorManager.AllocatorHandle>(sizeOf, alignOf, initialCapacity, ref allocator, options);
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0001CBD8 File Offset: 0x0001ADD8
		public UnsafeList(int sizeOf, int alignOf, int initialCapacity, Allocator allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory)
		{
			this = default(UnsafeList);
			this.Allocator = allocator;
			this.Ptr = null;
			this.Length = 0;
			this.Capacity = 0;
			if (initialCapacity != 0)
			{
				this.SetCapacity(sizeOf, alignOf, initialCapacity);
			}
			if (options == NativeArrayOptions.ClearMemory && this.Ptr != null)
			{
				UnsafeUtility.MemClear(this.Ptr, (long)(this.Capacity * sizeOf));
			}
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0001CC40 File Offset: 0x0001AE40
		public unsafe static UnsafeList* Create(int sizeOf, int alignOf, int initialCapacity, Allocator allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory)
		{
			UnsafeList* ptr = AllocatorManager.Allocate<UnsafeList>(allocator, 1);
			UnsafeUtility.MemClear((void*)ptr, (long)UnsafeUtility.SizeOf<UnsafeList>());
			ptr->Allocator = allocator;
			if (initialCapacity != 0)
			{
				ptr->SetCapacity(sizeOf, alignOf, initialCapacity);
			}
			if (options == NativeArrayOptions.ClearMemory && ptr->Ptr != null)
			{
				UnsafeUtility.MemClear(ptr->Ptr, (long)(ptr->Capacity * sizeOf));
			}
			return ptr;
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0001CCA4 File Offset: 0x0001AEA4
		internal unsafe static UnsafeList* Create<[global::System.Runtime.CompilerServices.IsUnmanaged] U>(int sizeOf, int alignOf, int initialCapacity, ref U allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			UnsafeList* ptr = (ref allocator).Allocate(default(UnsafeList), 1);
			UnsafeUtility.MemClear((void*)ptr, (long)UnsafeUtility.SizeOf<UnsafeList>());
			ptr->Allocator = allocator.Handle;
			if (initialCapacity != 0)
			{
				ptr->SetCapacity<U>(ref allocator, sizeOf, alignOf, initialCapacity);
			}
			if (options == NativeArrayOptions.ClearMemory && ptr->Ptr != null)
			{
				UnsafeUtility.MemClear(ptr->Ptr, (long)(ptr->Capacity * sizeOf));
			}
			return ptr;
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0001CD11 File Offset: 0x0001AF11
		internal unsafe static void Destroy<[global::System.Runtime.CompilerServices.IsUnmanaged] U>(UnsafeList* listData, ref U allocator, int sizeOf, int alignOf) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			listData->Dispose<U>(ref allocator, sizeOf, alignOf);
			(ref allocator).Free((void*)listData, UnsafeUtility.SizeOf<UnsafeList>(), UnsafeUtility.AlignOf<UnsafeList>(), 1);
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0001CD2E File Offset: 0x0001AF2E
		public unsafe static void Destroy(UnsafeList* listData)
		{
			AllocatorManager.AllocatorHandle allocator = listData->Allocator;
			listData->Dispose();
			AllocatorManager.Free<UnsafeList>(allocator, listData, 1);
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x0001CD43 File Offset: 0x0001AF43
		public bool IsEmpty
		{
			get
			{
				return !this.IsCreated || this.Length == 0;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x0001CD58 File Offset: 0x0001AF58
		public bool IsCreated
		{
			get
			{
				return this.Ptr != null;
			}
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0001CD68 File Offset: 0x0001AF68
		public void Dispose()
		{
			if (CollectionHelper.ShouldDeallocate(this.Allocator))
			{
				AllocatorManager.Free(this.Allocator, this.Ptr);
				this.Allocator = AllocatorManager.Invalid;
			}
			this.Ptr = null;
			this.Length = 0;
			this.Capacity = 0;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0001CDB4 File Offset: 0x0001AFB4
		internal void Dispose<[global::System.Runtime.CompilerServices.IsUnmanaged] U>(ref U allocator, int sizeOf, int alignOf) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			(ref allocator).Free(this.Ptr, sizeOf, alignOf, this.Length);
			this.Ptr = null;
			this.Length = 0;
			this.Capacity = 0;
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0001CDE0 File Offset: 0x0001AFE0
		[NotBurstCompatible]
		public JobHandle Dispose(JobHandle inputDeps)
		{
			if (CollectionHelper.ShouldDeallocate(this.Allocator))
			{
				JobHandle jobHandle = new UnsafeDisposeJob
				{
					Ptr = this.Ptr,
					Allocator = (Allocator)this.Allocator.Value
				}.Schedule(inputDeps);
				this.Ptr = null;
				this.Allocator = AllocatorManager.Invalid;
				return jobHandle;
			}
			this.Ptr = null;
			return inputDeps;
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0001CE4A File Offset: 0x0001B04A
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0001CE54 File Offset: 0x0001B054
		public unsafe void Resize(int sizeOf, int alignOf, int length, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory)
		{
			int length2 = this.Length;
			if (length > this.Capacity)
			{
				this.SetCapacity(sizeOf, alignOf, length);
			}
			this.Length = length;
			if (options == NativeArrayOptions.ClearMemory && length2 < length)
			{
				int num = length - length2;
				byte* ptr = (byte*)this.Ptr;
				UnsafeUtility.MemClear((void*)(ptr + length2 * sizeOf), (long)(num * sizeOf));
			}
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0001CEA3 File Offset: 0x0001B0A3
		public void Resize<T>(int length, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory) where T : struct
		{
			this.Resize(UnsafeUtility.SizeOf<T>(), UnsafeUtility.AlignOf<T>(), length, options);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0001CEB8 File Offset: 0x0001B0B8
		private unsafe void Realloc<[global::System.Runtime.CompilerServices.IsUnmanaged] U>(ref U allocator, int sizeOf, int alignOf, int capacity) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			void* ptr = null;
			if (capacity > 0)
			{
				ptr = (ref allocator).Allocate(sizeOf, alignOf, capacity);
				if (this.Capacity > 0)
				{
					int num = math.min(capacity, this.Capacity) * sizeOf;
					UnsafeUtility.MemCpy(ptr, this.Ptr, (long)num);
				}
			}
			(ref allocator).Free(this.Ptr, sizeOf, alignOf, this.Capacity);
			this.Ptr = ptr;
			this.Capacity = capacity;
			this.Length = math.min(this.Length, capacity);
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0001CF35 File Offset: 0x0001B135
		private void Realloc(int sizeOf, int alignOf, int capacity)
		{
			this.Realloc<AllocatorManager.AllocatorHandle>(ref this.Allocator, sizeOf, alignOf, capacity);
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0001CF48 File Offset: 0x0001B148
		private void SetCapacity<[global::System.Runtime.CompilerServices.IsUnmanaged] U>(ref U allocator, int sizeOf, int alignOf, int capacity) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			int num = math.max(capacity, 64 / sizeOf);
			num = math.ceilpow2(num);
			if (num == this.Capacity)
			{
				return;
			}
			this.Realloc<U>(ref allocator, sizeOf, alignOf, num);
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0001CF7C File Offset: 0x0001B17C
		private void SetCapacity(int sizeOf, int alignOf, int capacity)
		{
			this.SetCapacity<AllocatorManager.AllocatorHandle>(ref this.Allocator, sizeOf, alignOf, capacity);
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0001CF8D File Offset: 0x0001B18D
		public void SetCapacity<T>(int capacity) where T : struct
		{
			this.SetCapacity(UnsafeUtility.SizeOf<T>(), UnsafeUtility.AlignOf<T>(), capacity);
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0001CFA0 File Offset: 0x0001B1A0
		public void TrimExcess<T>() where T : struct
		{
			if (this.Capacity != this.Length)
			{
				this.Realloc(UnsafeUtility.SizeOf<T>(), UnsafeUtility.AlignOf<T>(), this.Length);
			}
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0001CFC6 File Offset: 0x0001B1C6
		public int IndexOf<T>(T value) where T : struct, IEquatable<T>
		{
			return NativeArrayExtensions.IndexOf<T, T>(this.Ptr, this.Length, value);
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0001CFDA File Offset: 0x0001B1DA
		public bool Contains<T>(T value) where T : struct, IEquatable<T>
		{
			return this.IndexOf<T>(value) != -1;
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0001CFE9 File Offset: 0x0001B1E9
		public void AddNoResize<T>(T value) where T : struct
		{
			UnsafeUtility.WriteArrayElement<T>(this.Ptr, this.Length, value);
			this.Length++;
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0001D00C File Offset: 0x0001B20C
		private unsafe void AddRangeNoResize(int sizeOf, void* ptr, int length)
		{
			void* ptr2 = (void*)((byte*)this.Ptr + this.Length * sizeOf);
			UnsafeUtility.MemCpy(ptr2, ptr, (long)(length * sizeOf));
			this.Length += length;
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0001D042 File Offset: 0x0001B242
		public unsafe void AddRangeNoResize<T>(void* ptr, int length) where T : struct
		{
			this.AddRangeNoResize(UnsafeUtility.SizeOf<T>(), ptr, length);
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0001D051 File Offset: 0x0001B251
		public void AddRangeNoResize<T>(UnsafeList list) where T : struct
		{
			this.AddRangeNoResize(UnsafeUtility.SizeOf<T>(), list.Ptr, CollectionHelper.AssumePositive(list.Length));
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0001D070 File Offset: 0x0001B270
		public void Add<T>(T value) where T : struct
		{
			int length = this.Length;
			if (this.Length + 1 > this.Capacity)
			{
				this.Resize<T>(length + 1, NativeArrayOptions.UninitializedMemory);
			}
			else
			{
				this.Length++;
			}
			UnsafeUtility.WriteArrayElement<T>(this.Ptr, length, value);
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0001D0BC File Offset: 0x0001B2BC
		private unsafe void AddRange(int sizeOf, int alignOf, void* ptr, int length)
		{
			int length2 = this.Length;
			if (this.Length + length > this.Capacity)
			{
				this.Resize(sizeOf, alignOf, this.Length + length, NativeArrayOptions.UninitializedMemory);
			}
			else
			{
				this.Length += length;
			}
			void* ptr2 = (void*)((byte*)this.Ptr + length2 * sizeOf);
			UnsafeUtility.MemCpy(ptr2, ptr, (long)(length * sizeOf));
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0001D11B File Offset: 0x0001B31B
		public unsafe void AddRange<T>(void* ptr, int length) where T : struct
		{
			this.AddRange(UnsafeUtility.SizeOf<T>(), UnsafeUtility.AlignOf<T>(), ptr, length);
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0001D12F File Offset: 0x0001B32F
		public void AddRange<T>(UnsafeList list) where T : struct
		{
			this.AddRange(UnsafeUtility.SizeOf<T>(), UnsafeUtility.AlignOf<T>(), list.Ptr, list.Length);
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0001D150 File Offset: 0x0001B350
		private unsafe void InsertRangeWithBeginEnd(int sizeOf, int alignOf, int begin, int end)
		{
			int num = end - begin;
			if (num < 1)
			{
				return;
			}
			int length = this.Length;
			if (this.Length + num > this.Capacity)
			{
				this.Resize(sizeOf, alignOf, this.Length + num, NativeArrayOptions.UninitializedMemory);
			}
			else
			{
				this.Length += num;
			}
			int num2 = length - begin;
			if (num2 < 1)
			{
				return;
			}
			int num3 = num2 * sizeOf;
			byte* ptr = (byte*)this.Ptr;
			void* ptr2 = (void*)(ptr + end * sizeOf);
			byte* ptr3 = ptr + begin * sizeOf;
			UnsafeUtility.MemMove(ptr2, (void*)ptr3, (long)num3);
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0001D1C7 File Offset: 0x0001B3C7
		public void InsertRangeWithBeginEnd<T>(int begin, int end) where T : struct
		{
			this.InsertRangeWithBeginEnd(UnsafeUtility.SizeOf<T>(), UnsafeUtility.AlignOf<T>(), begin, end);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0001D1DC File Offset: 0x0001B3DC
		private unsafe void RemoveRangeSwapBackWithBeginEnd(int sizeOf, int begin, int end)
		{
			int num = end - begin;
			if (num > 0)
			{
				int num2 = math.max(this.Length - num, end);
				void* ptr = (void*)((byte*)this.Ptr + begin * sizeOf);
				void* ptr2 = (void*)((byte*)this.Ptr + num2 * sizeOf);
				UnsafeUtility.MemCpy(ptr, ptr2, (long)((this.Length - num2) * sizeOf));
				this.Length -= num;
			}
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0001D236 File Offset: 0x0001B436
		public void RemoveAtSwapBack<T>(int index) where T : struct
		{
			this.RemoveRangeSwapBackWithBeginEnd<T>(index, index + 1);
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0001D242 File Offset: 0x0001B442
		public void RemoveRangeSwapBackWithBeginEnd<T>(int begin, int end) where T : struct
		{
			this.RemoveRangeSwapBackWithBeginEnd(UnsafeUtility.SizeOf<T>(), begin, end);
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0001D254 File Offset: 0x0001B454
		private unsafe void RemoveRangeWithBeginEnd(int sizeOf, int begin, int end)
		{
			int num = end - begin;
			if (num > 0)
			{
				int num2 = math.min(begin + num, this.Length);
				void* ptr = (void*)((byte*)this.Ptr + begin * sizeOf);
				void* ptr2 = (void*)((byte*)this.Ptr + num2 * sizeOf);
				UnsafeUtility.MemCpy(ptr, ptr2, (long)((this.Length - num2) * sizeOf));
				this.Length -= num;
			}
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0001D2AE File Offset: 0x0001B4AE
		public void RemoveAt<T>(int index) where T : struct
		{
			this.RemoveRangeWithBeginEnd<T>(index, index + 1);
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0001D2BA File Offset: 0x0001B4BA
		public void RemoveRangeWithBeginEnd<T>(int begin, int end) where T : struct
		{
			this.RemoveRangeWithBeginEnd(UnsafeUtility.SizeOf<T>(), begin, end);
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0001D2C9 File Offset: 0x0001B4C9
		public UnsafeList.ParallelReader AsParallelReader()
		{
			return new UnsafeList.ParallelReader(this.Ptr, this.Length);
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0001D2DC File Offset: 0x0001B4DC
		public unsafe UnsafeList.ParallelWriter AsParallelWriter()
		{
			return new UnsafeList.ParallelWriter(this.Ptr, (UnsafeList*)UnsafeUtility.AddressOf<UnsafeList>(ref this));
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0001D2EF File Offset: 0x0001B4EF
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		internal unsafe static void CheckNull(void* listData)
		{
			if (listData == null)
			{
				throw new Exception("UnsafeList has yet to be created or has been destroyed!");
			}
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0001D301 File Offset: 0x0001B501
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckAllocator(Allocator a)
		{
			if (!CollectionHelper.ShouldDeallocate(a))
			{
				throw new Exception("UnsafeList is not initialized, it must be initialized with allocator before use.");
			}
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0001D31B File Offset: 0x0001B51B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckAllocator(AllocatorManager.AllocatorHandle a)
		{
			if (!CollectionHelper.ShouldDeallocate(a))
			{
				throw new Exception("UnsafeList is not initialized, it must be initialized with allocator before use.");
			}
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0001D330 File Offset: 0x0001B530
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

		// Token: 0x0600098E RID: 2446 RVA: 0x00002C2B File Offset: 0x00000E2B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckNoResizeHasEnoughCapacity(int length)
		{
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0001D3B5 File Offset: 0x0001B5B5
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckNoResizeHasEnoughCapacity(int length, int index)
		{
			if (this.Capacity < index + length)
			{
				throw new Exception(string.Format("AddNoResize assumes that list capacity is sufficient (Capacity {0}, Length {1}), requested length {2}!", this.Capacity, this.Length, length));
			}
		}

		// Token: 0x04000328 RID: 808
		[NativeDisableUnsafePtrRestriction]
		public unsafe void* Ptr;

		// Token: 0x04000329 RID: 809
		public int Length;

		// Token: 0x0400032A RID: 810
		public readonly int unused;

		// Token: 0x0400032B RID: 811
		public int Capacity;

		// Token: 0x0400032C RID: 812
		public AllocatorManager.AllocatorHandle Allocator;

		// Token: 0x020000FD RID: 253
		public struct ParallelReader
		{
			// Token: 0x06000990 RID: 2448 RVA: 0x0001D3EE File Offset: 0x0001B5EE
			internal unsafe ParallelReader(void* ptr, int length)
			{
				this.Ptr = ptr;
				this.Length = length;
			}

			// Token: 0x06000991 RID: 2449 RVA: 0x0001D3FE File Offset: 0x0001B5FE
			public int IndexOf<T>(T value) where T : struct, IEquatable<T>
			{
				return NativeArrayExtensions.IndexOf<T, T>(this.Ptr, this.Length, value);
			}

			// Token: 0x06000992 RID: 2450 RVA: 0x0001D412 File Offset: 0x0001B612
			public bool Contains<T>(T value) where T : struct, IEquatable<T>
			{
				return this.IndexOf<T>(value) != -1;
			}

			// Token: 0x0400032D RID: 813
			[NativeDisableUnsafePtrRestriction]
			public unsafe readonly void* Ptr;

			// Token: 0x0400032E RID: 814
			public readonly int Length;
		}

		// Token: 0x020000FE RID: 254
		public struct ParallelWriter
		{
			// Token: 0x06000993 RID: 2451 RVA: 0x0001D421 File Offset: 0x0001B621
			internal unsafe ParallelWriter(void* ptr, UnsafeList* listData)
			{
				this.Ptr = ptr;
				this.ListData = listData;
			}

			// Token: 0x06000994 RID: 2452 RVA: 0x0001D434 File Offset: 0x0001B634
			public unsafe void AddNoResize<T>(T value) where T : struct
			{
				int num = Interlocked.Increment(ref this.ListData->Length) - 1;
				UnsafeUtility.WriteArrayElement<T>(this.Ptr, num, value);
			}

			// Token: 0x06000995 RID: 2453 RVA: 0x0001D464 File Offset: 0x0001B664
			private unsafe void AddRangeNoResize(int sizeOf, int alignOf, void* ptr, int length)
			{
				int num = Interlocked.Add(ref this.ListData->Length, length) - length;
				void* ptr2 = (void*)((byte*)this.Ptr + num * sizeOf);
				UnsafeUtility.MemCpy(ptr2, ptr, (long)(length * sizeOf));
			}

			// Token: 0x06000996 RID: 2454 RVA: 0x0001D49E File Offset: 0x0001B69E
			public unsafe void AddRangeNoResize<T>(void* ptr, int length) where T : struct
			{
				this.AddRangeNoResize(UnsafeUtility.SizeOf<T>(), UnsafeUtility.AlignOf<T>(), ptr, length);
			}

			// Token: 0x06000997 RID: 2455 RVA: 0x0001D4B2 File Offset: 0x0001B6B2
			public void AddRangeNoResize<T>(UnsafeList list) where T : struct
			{
				this.AddRangeNoResize(UnsafeUtility.SizeOf<T>(), UnsafeUtility.AlignOf<T>(), list.Ptr, list.Length);
			}

			// Token: 0x0400032F RID: 815
			[NativeDisableUnsafePtrRestriction]
			public unsafe readonly void* Ptr;

			// Token: 0x04000330 RID: 816
			[NativeDisableUnsafePtrRestriction]
			public unsafe UnsafeList* ListData;
		}
	}
}
