using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000111 RID: 273
	[Obsolete("UnsafeMultiHashMap is renamed to UnsafeParallelMultiHashMap. (UnityUpgradable) -> UnsafeParallelMultiHashMap<TKey, TValue>", false)]
	public struct UnsafeMultiHashMap<TKey, TValue> : INativeDisposable, IDisposable, IEnumerable<KeyValue<TKey, TValue>>, IEnumerable where TKey : struct, IEquatable<TKey> where TValue : struct
	{
		// Token: 0x06000A12 RID: 2578 RVA: 0x0001E46E File Offset: 0x0001C66E
		public UnsafeMultiHashMap(int capacity, AllocatorManager.AllocatorHandle allocator)
		{
			this.m_AllocatorLabel = allocator;
			UnsafeParallelHashMapData.AllocateHashMap<TKey, TValue>(capacity, capacity * 2, allocator, out this.m_Buffer);
			this.Clear();
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x0001E48D File Offset: 0x0001C68D
		public bool IsEmpty
		{
			get
			{
				return !this.IsCreated || UnsafeParallelHashMapData.IsEmpty(this.m_Buffer);
			}
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0001E4A4 File Offset: 0x0001C6A4
		public unsafe int Count()
		{
			if (this.m_Buffer->allocatedIndexLength <= 0)
			{
				return 0;
			}
			return UnsafeParallelHashMapData.GetCount(this.m_Buffer);
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x0001E4C1 File Offset: 0x0001C6C1
		// (set) Token: 0x06000A16 RID: 2582 RVA: 0x0001E4CE File Offset: 0x0001C6CE
		public unsafe int Capacity
		{
			get
			{
				return this.m_Buffer->keyCapacity;
			}
			set
			{
				UnsafeParallelHashMapData.ReallocateHashMap<TKey, TValue>(this.m_Buffer, value, UnsafeParallelHashMapData.GetBucketSize(value), this.m_AllocatorLabel);
			}
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0001E4E8 File Offset: 0x0001C6E8
		public void Clear()
		{
			UnsafeParallelHashMapBase<TKey, TValue>.Clear(this.m_Buffer);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0001E4F5 File Offset: 0x0001C6F5
		public void Add(TKey key, TValue item)
		{
			UnsafeParallelHashMapBase<TKey, TValue>.TryAdd(this.m_Buffer, key, item, true, this.m_AllocatorLabel);
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0001E50C File Offset: 0x0001C70C
		public int Remove(TKey key)
		{
			return UnsafeParallelHashMapBase<TKey, TValue>.Remove(this.m_Buffer, key, true);
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0001E51B File Offset: 0x0001C71B
		public void Remove<TValueEQ>(TKey key, TValueEQ value) where TValueEQ : struct, IEquatable<TValueEQ>
		{
			UnsafeParallelHashMapBase<TKey, TValueEQ>.RemoveKeyValue<TValueEQ>(this.m_Buffer, key, value);
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0001E52A File Offset: 0x0001C72A
		public void Remove(NativeParallelMultiHashMapIterator<TKey> it)
		{
			UnsafeParallelHashMapBase<TKey, TValue>.Remove(this.m_Buffer, it);
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0001E538 File Offset: 0x0001C738
		public bool TryGetFirstValue(TKey key, out TValue item, out NativeParallelMultiHashMapIterator<TKey> it)
		{
			return UnsafeParallelHashMapBase<TKey, TValue>.TryGetFirstValueAtomic(this.m_Buffer, key, out item, out it);
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0001E548 File Offset: 0x0001C748
		public bool TryGetNextValue(out TValue item, ref NativeParallelMultiHashMapIterator<TKey> it)
		{
			return UnsafeParallelHashMapBase<TKey, TValue>.TryGetNextValueAtomic(this.m_Buffer, out item, ref it);
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0001E558 File Offset: 0x0001C758
		public bool ContainsKey(TKey key)
		{
			TValue tvalue;
			NativeParallelMultiHashMapIterator<TKey> nativeParallelMultiHashMapIterator;
			return this.TryGetFirstValue(key, out tvalue, out nativeParallelMultiHashMapIterator);
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0001E570 File Offset: 0x0001C770
		public int CountValuesForKey(TKey key)
		{
			TValue tvalue;
			NativeParallelMultiHashMapIterator<TKey> nativeParallelMultiHashMapIterator;
			if (!this.TryGetFirstValue(key, out tvalue, out nativeParallelMultiHashMapIterator))
			{
				return 0;
			}
			int num = 1;
			while (this.TryGetNextValue(out tvalue, ref nativeParallelMultiHashMapIterator))
			{
				num++;
			}
			return num;
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0001E5A1 File Offset: 0x0001C7A1
		public bool SetValue(TValue item, NativeParallelMultiHashMapIterator<TKey> it)
		{
			return UnsafeParallelHashMapBase<TKey, TValue>.SetValue(this.m_Buffer, ref it, ref item);
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000A21 RID: 2593 RVA: 0x0001E5B2 File Offset: 0x0001C7B2
		public bool IsCreated
		{
			get
			{
				return this.m_Buffer != null;
			}
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0001E5C1 File Offset: 0x0001C7C1
		public void Dispose()
		{
			UnsafeParallelHashMapData.DeallocateHashMap(this.m_Buffer, this.m_AllocatorLabel);
			this.m_Buffer = null;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0001E5DC File Offset: 0x0001C7DC
		[NotBurstCompatible]
		public JobHandle Dispose(JobHandle inputDeps)
		{
			JobHandle jobHandle = new UnsafeParallelHashMapDisposeJob
			{
				Data = this.m_Buffer,
				Allocator = this.m_AllocatorLabel
			}.Schedule(inputDeps);
			this.m_Buffer = null;
			return jobHandle;
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0001E61C File Offset: 0x0001C81C
		public NativeArray<TKey> GetKeyArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<TKey> nativeArray = CollectionHelper.CreateNativeArray<TKey>(this.Count(), allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeParallelHashMapData.GetKeyArray<TKey>(this.m_Buffer, nativeArray);
			return nativeArray;
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0001E644 File Offset: 0x0001C844
		public NativeArray<TValue> GetValueArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<TValue> nativeArray = CollectionHelper.CreateNativeArray<TValue>(this.Count(), allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeParallelHashMapData.GetValueArray<TValue>(this.m_Buffer, nativeArray);
			return nativeArray;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0001E66C File Offset: 0x0001C86C
		public NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(AllocatorManager.AllocatorHandle allocator)
		{
			NativeKeyValueArrays<TKey, TValue> nativeKeyValueArrays = new NativeKeyValueArrays<TKey, TValue>(this.Count(), allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeParallelHashMapData.GetKeyValueArrays<TKey, TValue>(this.m_Buffer, nativeKeyValueArrays);
			return nativeKeyValueArrays;
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0001E698 File Offset: 0x0001C898
		public UnsafeMultiHashMap<TKey, TValue>.Enumerator GetValuesForKey(TKey key)
		{
			return new UnsafeMultiHashMap<TKey, TValue>.Enumerator
			{
				hashmap = this,
				key = key,
				isFirst = true
			};
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0001E6CC File Offset: 0x0001C8CC
		public UnsafeMultiHashMap<TKey, TValue>.ParallelWriter AsParallelWriter()
		{
			UnsafeMultiHashMap<TKey, TValue>.ParallelWriter parallelWriter;
			parallelWriter.m_ThreadIndex = 0;
			parallelWriter.m_Buffer = this.m_Buffer;
			return parallelWriter;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0001E6F0 File Offset: 0x0001C8F0
		public UnsafeMultiHashMap<TKey, TValue>.KeyValueEnumerator GetEnumerator()
		{
			return new UnsafeMultiHashMap<TKey, TValue>.KeyValueEnumerator
			{
				m_Enumerator = new UnsafeParallelHashMapDataEnumerator(this.m_Buffer)
			};
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00004A65 File Offset: 0x00002C65
		IEnumerator<KeyValue<TKey, TValue>> IEnumerable<KeyValue<TKey, TValue>>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00004A65 File Offset: 0x00002C65
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400035D RID: 861
		[NativeDisableUnsafePtrRestriction]
		internal unsafe UnsafeParallelHashMapData* m_Buffer;

		// Token: 0x0400035E RID: 862
		internal AllocatorManager.AllocatorHandle m_AllocatorLabel;

		// Token: 0x02000112 RID: 274
		public struct Enumerator : IEnumerator<TValue>, IEnumerator, IDisposable
		{
			// Token: 0x06000A2C RID: 2604 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x06000A2D RID: 2605 RVA: 0x0001E718 File Offset: 0x0001C918
			public bool MoveNext()
			{
				if (this.isFirst)
				{
					this.isFirst = false;
					return this.hashmap.TryGetFirstValue(this.key, out this.value, out this.iterator);
				}
				return this.hashmap.TryGetNextValue(out this.value, ref this.iterator);
			}

			// Token: 0x06000A2E RID: 2606 RVA: 0x0001E769 File Offset: 0x0001C969
			public void Reset()
			{
				this.isFirst = true;
			}

			// Token: 0x17000117 RID: 279
			// (get) Token: 0x06000A2F RID: 2607 RVA: 0x0001E772 File Offset: 0x0001C972
			public TValue Current
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x17000118 RID: 280
			// (get) Token: 0x06000A30 RID: 2608 RVA: 0x0001E77A File Offset: 0x0001C97A
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000A31 RID: 2609 RVA: 0x0001E787 File Offset: 0x0001C987
			public UnsafeMultiHashMap<TKey, TValue>.Enumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x0400035F RID: 863
			internal UnsafeMultiHashMap<TKey, TValue> hashmap;

			// Token: 0x04000360 RID: 864
			internal TKey key;

			// Token: 0x04000361 RID: 865
			internal bool isFirst;

			// Token: 0x04000362 RID: 866
			private TValue value;

			// Token: 0x04000363 RID: 867
			private NativeParallelMultiHashMapIterator<TKey> iterator;
		}

		// Token: 0x02000113 RID: 275
		[NativeContainerIsAtomicWriteOnly]
		public struct ParallelWriter
		{
			// Token: 0x17000119 RID: 281
			// (get) Token: 0x06000A32 RID: 2610 RVA: 0x0001E78F File Offset: 0x0001C98F
			public unsafe int Capacity
			{
				get
				{
					return this.m_Buffer->keyCapacity;
				}
			}

			// Token: 0x06000A33 RID: 2611 RVA: 0x0001E79C File Offset: 0x0001C99C
			public void Add(TKey key, TValue item)
			{
				UnsafeParallelHashMapBase<TKey, TValue>.AddAtomicMulti(this.m_Buffer, key, item, this.m_ThreadIndex);
			}

			// Token: 0x04000364 RID: 868
			[NativeDisableUnsafePtrRestriction]
			internal unsafe UnsafeParallelHashMapData* m_Buffer;

			// Token: 0x04000365 RID: 869
			[NativeSetThreadIndex]
			internal int m_ThreadIndex;
		}

		// Token: 0x02000114 RID: 276
		public struct KeyValueEnumerator : IEnumerator<KeyValue<TKey, TValue>>, IEnumerator, IDisposable
		{
			// Token: 0x06000A34 RID: 2612 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x06000A35 RID: 2613 RVA: 0x0001E7B1 File Offset: 0x0001C9B1
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x06000A36 RID: 2614 RVA: 0x0001E7BE File Offset: 0x0001C9BE
			public void Reset()
			{
				this.m_Enumerator.Reset();
			}

			// Token: 0x1700011A RID: 282
			// (get) Token: 0x06000A37 RID: 2615 RVA: 0x0001E7CB File Offset: 0x0001C9CB
			public KeyValue<TKey, TValue> Current
			{
				get
				{
					return this.m_Enumerator.GetCurrent<TKey, TValue>();
				}
			}

			// Token: 0x1700011B RID: 283
			// (get) Token: 0x06000A38 RID: 2616 RVA: 0x0001E7D8 File Offset: 0x0001C9D8
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000366 RID: 870
			internal UnsafeParallelHashMapDataEnumerator m_Enumerator;
		}
	}
}
