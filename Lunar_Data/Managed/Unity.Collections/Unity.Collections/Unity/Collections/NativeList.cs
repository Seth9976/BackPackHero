using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Collections.NotBurstCompatible;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000B2 RID: 178
	[NativeContainer]
	[DebuggerDisplay("Length = {Length}")]
	[DebuggerTypeProxy(typeof(NativeListDebugView<>))]
	[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
	public struct NativeList<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : INativeDisposable, IDisposable, INativeList<T>, IIndexable<T>, IEnumerable<T>, IEnumerable where T : struct, ValueType
	{
		// Token: 0x060006C6 RID: 1734 RVA: 0x00015A80 File Offset: 0x00013C80
		public NativeList(AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeList<T>(1, allocator, 2);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00015A8B File Offset: 0x00013C8B
		public NativeList(int initialCapacity, AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeList<T>(initialCapacity, allocator, 2);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00015A96 File Offset: 0x00013C96
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(AllocatorManager.AllocatorHandle) })]
		internal void Initialize<[global::System.Runtime.CompilerServices.IsUnmanaged] U>(int initialCapacity, ref U allocator, int disposeSentinelStackDepth) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			this.m_ListData = UnsafeList<T>.Create<U>(initialCapacity, ref allocator, NativeArrayOptions.UninitializedMemory);
			this.m_DeprecatedAllocator = allocator.Handle;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00015AB8 File Offset: 0x00013CB8
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(AllocatorManager.AllocatorHandle) })]
		internal static NativeList<T> New<[global::System.Runtime.CompilerServices.IsUnmanaged] U>(int initialCapacity, ref U allocator, int disposeSentinelStackDepth) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			NativeList<T> nativeList = default(NativeList<T>);
			nativeList.Initialize<U>(initialCapacity, ref allocator, disposeSentinelStackDepth);
			return nativeList;
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00015AD8 File Offset: 0x00013CD8
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(AllocatorManager.AllocatorHandle) })]
		internal static NativeList<T> New<[global::System.Runtime.CompilerServices.IsUnmanaged] U>(int initialCapacity, ref U allocator) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			return NativeList<T>.New<U>(initialCapacity, ref allocator, 2);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00015AE4 File Offset: 0x00013CE4
		private NativeList(int initialCapacity, AllocatorManager.AllocatorHandle allocator, int disposeSentinelStackDepth)
		{
			this = default(NativeList<T>);
			AllocatorManager.AllocatorHandle allocatorHandle = allocator;
			this.Initialize<AllocatorManager.AllocatorHandle>(initialCapacity, ref allocatorHandle, disposeSentinelStackDepth);
		}

		// Token: 0x170000B7 RID: 183
		public unsafe T this[int index]
		{
			get
			{
				return (*this.m_ListData)[index];
			}
			set
			{
				(*this.m_ListData)[index] = value;
			}
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00015B21 File Offset: 0x00013D21
		public unsafe ref T ElementAt(int index)
		{
			return this.m_ListData->ElementAt(index);
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x00015B2F File Offset: 0x00013D2F
		// (set) Token: 0x060006D0 RID: 1744 RVA: 0x00015B41 File Offset: 0x00013D41
		public unsafe int Length
		{
			get
			{
				return CollectionHelper.AssumePositive(this.m_ListData->Length);
			}
			set
			{
				this.m_ListData->Resize(value, NativeArrayOptions.ClearMemory);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x00015B50 File Offset: 0x00013D50
		// (set) Token: 0x060006D2 RID: 1746 RVA: 0x00015B5D File Offset: 0x00013D5D
		public unsafe int Capacity
		{
			get
			{
				return this.m_ListData->Capacity;
			}
			set
			{
				this.m_ListData->Capacity = value;
			}
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00015B6B File Offset: 0x00013D6B
		public unsafe UnsafeList<T>* GetUnsafeList()
		{
			return this.m_ListData;
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00015B73 File Offset: 0x00013D73
		public unsafe void AddNoResize(T value)
		{
			this.m_ListData->AddNoResize(value);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00015B81 File Offset: 0x00013D81
		public unsafe void AddRangeNoResize(void* ptr, int count)
		{
			this.m_ListData->AddRangeNoResize(ptr, count);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00015B90 File Offset: 0x00013D90
		public unsafe void AddRangeNoResize(NativeList<T> list)
		{
			this.m_ListData->AddRangeNoResize(*list.m_ListData);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00015BA8 File Offset: 0x00013DA8
		public unsafe void Add(in T value)
		{
			this.m_ListData->Add(in value);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00015BB6 File Offset: 0x00013DB6
		public void AddRange(NativeArray<T> array)
		{
			this.AddRange(array.GetUnsafeReadOnlyPtr<T>(), array.Length);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00015BCB File Offset: 0x00013DCB
		public unsafe void AddRange(void* ptr, int count)
		{
			this.m_ListData->AddRange(ptr, CollectionHelper.AssumePositive(count));
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00015BDF File Offset: 0x00013DDF
		public unsafe void InsertRangeWithBeginEnd(int begin, int end)
		{
			this.m_ListData->InsertRangeWithBeginEnd(CollectionHelper.AssumePositive(begin), CollectionHelper.AssumePositive(end));
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00015BF8 File Offset: 0x00013DF8
		public unsafe void RemoveAtSwapBack(int index)
		{
			this.m_ListData->RemoveAtSwapBack(CollectionHelper.AssumePositive(index));
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00015C0B File Offset: 0x00013E0B
		public unsafe void RemoveRangeSwapBack(int index, int count)
		{
			this.m_ListData->RemoveRangeSwapBack(CollectionHelper.AssumePositive(index), CollectionHelper.AssumePositive(count));
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00015C24 File Offset: 0x00013E24
		[Obsolete("RemoveRangeSwapBackWithBeginEnd(begin, end) is deprecated, use RemoveRangeSwapBack(index, count) instead. (RemovedAfter 2021-06-02)", false)]
		public unsafe void RemoveRangeSwapBackWithBeginEnd(int begin, int end)
		{
			this.m_ListData->RemoveRangeSwapBackWithBeginEnd(CollectionHelper.AssumePositive(begin), CollectionHelper.AssumePositive(end));
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00015C3D File Offset: 0x00013E3D
		public unsafe void RemoveAt(int index)
		{
			this.m_ListData->RemoveAt(CollectionHelper.AssumePositive(index));
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00015C50 File Offset: 0x00013E50
		public unsafe void RemoveRange(int index, int count)
		{
			this.m_ListData->RemoveRange(index, count);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00015C5F File Offset: 0x00013E5F
		[Obsolete("RemoveRangeWithBeginEnd(begin, end) is deprecated, use RemoveRange(index, count) instead. (RemovedAfter 2021-06-02)", false)]
		public unsafe void RemoveRangeWithBeginEnd(int begin, int end)
		{
			this.m_ListData->RemoveRangeWithBeginEnd(begin, end);
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x00015C6E File Offset: 0x00013E6E
		public bool IsEmpty
		{
			get
			{
				return !this.IsCreated || this.Length == 0;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x00015C83 File Offset: 0x00013E83
		public bool IsCreated
		{
			get
			{
				return this.m_ListData != null;
			}
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00015C92 File Offset: 0x00013E92
		public void Dispose()
		{
			UnsafeList<T>.Destroy(this.m_ListData);
			this.m_ListData = null;
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00015CA7 File Offset: 0x00013EA7
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(AllocatorManager.AllocatorHandle) })]
		internal void Dispose<[global::System.Runtime.CompilerServices.IsUnmanaged] U>(ref U allocator) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			UnsafeList<T>.Destroy<U>(this.m_ListData, ref allocator);
			this.m_ListData = null;
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00015CC0 File Offset: 0x00013EC0
		[NotBurstCompatible]
		public unsafe JobHandle Dispose(JobHandle inputDeps)
		{
			JobHandle jobHandle = new NativeListDisposeJob
			{
				Data = new NativeListDispose
				{
					m_ListData = (UntypedUnsafeList*)this.m_ListData
				}
			}.Schedule(inputDeps);
			this.m_ListData = null;
			return jobHandle;
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00015D01 File Offset: 0x00013F01
		public unsafe void Clear()
		{
			this.m_ListData->Clear();
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00015D0E File Offset: 0x00013F0E
		public static implicit operator NativeArray<T>(NativeList<T> nativeList)
		{
			return nativeList.AsArray();
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00015D17 File Offset: 0x00013F17
		public unsafe NativeArray<T> AsArray()
		{
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)this.m_ListData->Ptr, this.m_ListData->Length, Allocator.None);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00015D38 File Offset: 0x00013F38
		public unsafe NativeArray<T> AsDeferredJobArray()
		{
			byte* ptr = (byte*)this.m_ListData;
			ptr++;
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)ptr, 0, Allocator.Invalid);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00015D58 File Offset: 0x00013F58
		[NotBurstCompatible]
		public T[] ToArray()
		{
			return this.ToArrayNBC<T>();
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00015D68 File Offset: 0x00013F68
		public NativeArray<T> ToArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<T> nativeArray = CollectionHelper.CreateNativeArray<T>(this.Length, allocator, NativeArrayOptions.UninitializedMemory);
			nativeArray.CopyFrom(this);
			return nativeArray;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00015D98 File Offset: 0x00013F98
		public NativeArray<T>.Enumerator GetEnumerator()
		{
			NativeArray<T> nativeArray = this.AsArray();
			return new NativeArray<T>.Enumerator(ref nativeArray);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00004A65 File Offset: 0x00002C65
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00004A65 File Offset: 0x00002C65
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00015DB3 File Offset: 0x00013FB3
		[NotBurstCompatible]
		[Obsolete("Please use `CopyFromNBC` from `Unity.Collections.NotBurstCompatible` namespace instead. (RemovedAfter 2021-06-22)", false)]
		public void CopyFrom(T[] array)
		{
			this.CopyFromNBC(array);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00015DC4 File Offset: 0x00013FC4
		public void CopyFrom(NativeArray<T> array)
		{
			this.Clear();
			this.Resize(array.Length, NativeArrayOptions.UninitializedMemory);
			this.AsArray().CopyFrom(array);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00015DF4 File Offset: 0x00013FF4
		public unsafe void Resize(int length, NativeArrayOptions options)
		{
			this.m_ListData->Resize(length, options);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00015E03 File Offset: 0x00014003
		public void ResizeUninitialized(int length)
		{
			this.Resize(length, NativeArrayOptions.UninitializedMemory);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00015E0D File Offset: 0x0001400D
		public unsafe void SetCapacity(int capacity)
		{
			this.m_ListData->SetCapacity(capacity);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00015E1B File Offset: 0x0001401B
		public unsafe void TrimExcess()
		{
			this.m_ListData->TrimExcess();
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00015E28 File Offset: 0x00014028
		public unsafe NativeArray<T>.ReadOnly AsParallelReader()
		{
			return new NativeArray<T>.ReadOnly((void*)this.m_ListData->Ptr, this.m_ListData->Length);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00015E45 File Offset: 0x00014045
		public NativeList<T>.ParallelWriter AsParallelWriter()
		{
			return new NativeList<T>.ParallelWriter(this.m_ListData);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00015E52 File Offset: 0x00014052
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckInitialCapacity(int initialCapacity)
		{
			if (initialCapacity < 0)
			{
				throw new ArgumentOutOfRangeException("initialCapacity", "Capacity must be >= 0");
			}
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00015E68 File Offset: 0x00014068
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckTotalSize(int initialCapacity, long totalSize)
		{
			if (totalSize > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("initialCapacity", string.Format("Capacity * sizeof(T) cannot exceed {0} bytes", int.MaxValue));
			}
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00015E92 File Offset: 0x00014092
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckSufficientCapacity(int capacity, int length)
		{
			if (capacity < length)
			{
				throw new Exception(string.Format("Length {0} exceeds capacity Capacity {1}", length, capacity));
			}
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00015EB4 File Offset: 0x000140B4
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckIndexInRange(int value, int length)
		{
			if (value < 0)
			{
				throw new IndexOutOfRangeException(string.Format("Value {0} must be positive.", value));
			}
			if (value >= length)
			{
				throw new IndexOutOfRangeException(string.Format("Value {0} is out of range in NativeList of '{1}' Length.", value, length));
			}
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00015EF0 File Offset: 0x000140F0
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckArgPositive(int value)
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("Value {0} must be positive.", value));
			}
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00015F0C File Offset: 0x0001410C
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private unsafe void CheckHandleMatches(AllocatorManager.AllocatorHandle handle)
		{
			if (this.m_ListData == null)
			{
				throw new ArgumentOutOfRangeException(string.Format("Allocator handle {0} can't match because container is not initialized.", handle));
			}
			if (this.m_ListData->Allocator.Index != handle.Index)
			{
				throw new ArgumentOutOfRangeException(string.Format("Allocator handle {0} can't match because container handle index doesn't match.", handle));
			}
			if (this.m_ListData->Allocator.Version != handle.Version)
			{
				throw new ArgumentOutOfRangeException(string.Format("Allocator handle {0} matches container handle index, but has different version.", handle));
			}
		}

		// Token: 0x0400027F RID: 639
		[NativeDisableUnsafePtrRestriction]
		internal unsafe UnsafeList<T>* m_ListData;

		// Token: 0x04000280 RID: 640
		internal AllocatorManager.AllocatorHandle m_DeprecatedAllocator;

		// Token: 0x020000B3 RID: 179
		[NativeContainer]
		[NativeContainerIsAtomicWriteOnly]
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		public struct ParallelWriter
		{
			// Token: 0x170000BC RID: 188
			// (get) Token: 0x060006FD RID: 1789 RVA: 0x00015F95 File Offset: 0x00014195
			public unsafe readonly void* Ptr
			{
				get
				{
					return (void*)this.ListData->Ptr;
				}
			}

			// Token: 0x060006FE RID: 1790 RVA: 0x00015FA2 File Offset: 0x000141A2
			internal unsafe ParallelWriter(UnsafeList<T>* listData)
			{
				this.ListData = listData;
			}

			// Token: 0x060006FF RID: 1791 RVA: 0x00015FAC File Offset: 0x000141AC
			public unsafe void AddNoResize(T value)
			{
				int num = Interlocked.Increment(ref this.ListData->m_length) - 1;
				UnsafeUtility.WriteArrayElement<T>((void*)this.ListData->Ptr, num, value);
			}

			// Token: 0x06000700 RID: 1792 RVA: 0x00015FE0 File Offset: 0x000141E0
			public unsafe void AddRangeNoResize(void* ptr, int count)
			{
				int num = Interlocked.Add(ref this.ListData->m_length, count) - count;
				int num2 = sizeof(T);
				void* ptr2 = (void*)(this.ListData->Ptr + num * num2 / sizeof(T));
				UnsafeUtility.MemCpy(ptr2, ptr, (long)(count * num2));
			}

			// Token: 0x06000701 RID: 1793 RVA: 0x00016023 File Offset: 0x00014223
			public unsafe void AddRangeNoResize(UnsafeList<T> list)
			{
				this.AddRangeNoResize((void*)list.Ptr, list.Length);
			}

			// Token: 0x06000702 RID: 1794 RVA: 0x00016038 File Offset: 0x00014238
			public unsafe void AddRangeNoResize(NativeList<T> list)
			{
				this.AddRangeNoResize(*list.m_ListData);
			}

			// Token: 0x04000281 RID: 641
			[NativeDisableUnsafePtrRestriction]
			public unsafe UnsafeList<T>* ListData;
		}
	}
}
