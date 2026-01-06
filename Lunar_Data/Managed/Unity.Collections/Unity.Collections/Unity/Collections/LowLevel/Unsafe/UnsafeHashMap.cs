using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200010E RID: 270
	[Obsolete("UnsafeHashMap is renamed to UnsafeParallelHashMap. (UnityUpgradable) -> UnsafeParallelHashMap<TKey, TValue>", false)]
	public struct UnsafeHashMap<TKey, TValue> : INativeDisposable, IDisposable, IEnumerable<KeyValue<TKey, TValue>>, IEnumerable where TKey : struct, IEquatable<TKey> where TValue : struct
	{
		// Token: 0x060009F4 RID: 2548 RVA: 0x0001E18C File Offset: 0x0001C38C
		public UnsafeHashMap(int capacity, AllocatorManager.AllocatorHandle allocator)
		{
			this.m_AllocatorLabel = allocator;
			UnsafeParallelHashMapData.AllocateHashMap<TKey, TValue>(capacity, capacity * 2, allocator, out this.m_Buffer);
			this.Clear();
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x0001E1AB File Offset: 0x0001C3AB
		public bool IsEmpty
		{
			get
			{
				return !this.IsCreated || UnsafeParallelHashMapData.IsEmpty(this.m_Buffer);
			}
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0001E1C2 File Offset: 0x0001C3C2
		public int Count()
		{
			return UnsafeParallelHashMapData.GetCount(this.m_Buffer);
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x0001E1CF File Offset: 0x0001C3CF
		// (set) Token: 0x060009F8 RID: 2552 RVA: 0x0001E1DC File Offset: 0x0001C3DC
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

		// Token: 0x060009F9 RID: 2553 RVA: 0x0001E1F6 File Offset: 0x0001C3F6
		public void Clear()
		{
			UnsafeParallelHashMapBase<TKey, TValue>.Clear(this.m_Buffer);
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0001E203 File Offset: 0x0001C403
		public bool TryAdd(TKey key, TValue item)
		{
			return UnsafeParallelHashMapBase<TKey, TValue>.TryAdd(this.m_Buffer, key, item, false, this.m_AllocatorLabel);
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0001E219 File Offset: 0x0001C419
		public void Add(TKey key, TValue item)
		{
			this.TryAdd(key, item);
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0001E224 File Offset: 0x0001C424
		public bool Remove(TKey key)
		{
			return UnsafeParallelHashMapBase<TKey, TValue>.Remove(this.m_Buffer, key, false) != 0;
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0001E238 File Offset: 0x0001C438
		public bool TryGetValue(TKey key, out TValue item)
		{
			NativeParallelMultiHashMapIterator<TKey> nativeParallelMultiHashMapIterator;
			return UnsafeParallelHashMapBase<TKey, TValue>.TryGetFirstValueAtomic(this.m_Buffer, key, out item, out nativeParallelMultiHashMapIterator);
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0001E254 File Offset: 0x0001C454
		public bool ContainsKey(TKey key)
		{
			TValue tvalue;
			NativeParallelMultiHashMapIterator<TKey> nativeParallelMultiHashMapIterator;
			return UnsafeParallelHashMapBase<TKey, TValue>.TryGetFirstValueAtomic(this.m_Buffer, key, out tvalue, out nativeParallelMultiHashMapIterator);
		}

		// Token: 0x1700010F RID: 271
		public TValue this[TKey key]
		{
			get
			{
				TValue tvalue;
				this.TryGetValue(key, out tvalue);
				return tvalue;
			}
			set
			{
				TValue tvalue;
				NativeParallelMultiHashMapIterator<TKey> nativeParallelMultiHashMapIterator;
				if (UnsafeParallelHashMapBase<TKey, TValue>.TryGetFirstValueAtomic(this.m_Buffer, key, out tvalue, out nativeParallelMultiHashMapIterator))
				{
					UnsafeParallelHashMapBase<TKey, TValue>.SetValue(this.m_Buffer, ref nativeParallelMultiHashMapIterator, ref value);
					return;
				}
				UnsafeParallelHashMapBase<TKey, TValue>.TryAdd(this.m_Buffer, key, value, false, this.m_AllocatorLabel);
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x0001E2D1 File Offset: 0x0001C4D1
		public bool IsCreated
		{
			get
			{
				return this.m_Buffer != null;
			}
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0001E2E0 File Offset: 0x0001C4E0
		public void Dispose()
		{
			UnsafeParallelHashMapData.DeallocateHashMap(this.m_Buffer, this.m_AllocatorLabel);
			this.m_Buffer = null;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0001E2FC File Offset: 0x0001C4FC
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

		// Token: 0x06000A04 RID: 2564 RVA: 0x0001E33C File Offset: 0x0001C53C
		public NativeArray<TKey> GetKeyArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<TKey> nativeArray = CollectionHelper.CreateNativeArray<TKey>(UnsafeParallelHashMapData.GetCount(this.m_Buffer), allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeParallelHashMapData.GetKeyArray<TKey>(this.m_Buffer, nativeArray);
			return nativeArray;
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0001E36C File Offset: 0x0001C56C
		public NativeArray<TValue> GetValueArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<TValue> nativeArray = CollectionHelper.CreateNativeArray<TValue>(UnsafeParallelHashMapData.GetCount(this.m_Buffer), allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeParallelHashMapData.GetValueArray<TValue>(this.m_Buffer, nativeArray);
			return nativeArray;
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0001E39C File Offset: 0x0001C59C
		public NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(AllocatorManager.AllocatorHandle allocator)
		{
			NativeKeyValueArrays<TKey, TValue> nativeKeyValueArrays = new NativeKeyValueArrays<TKey, TValue>(UnsafeParallelHashMapData.GetCount(this.m_Buffer), allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeParallelHashMapData.GetKeyValueArrays<TKey, TValue>(this.m_Buffer, nativeKeyValueArrays);
			return nativeKeyValueArrays;
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0001E3CC File Offset: 0x0001C5CC
		public UnsafeHashMap<TKey, TValue>.ParallelWriter AsParallelWriter()
		{
			UnsafeHashMap<TKey, TValue>.ParallelWriter parallelWriter;
			parallelWriter.m_ThreadIndex = 0;
			parallelWriter.m_Buffer = this.m_Buffer;
			return parallelWriter;
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0001E3F0 File Offset: 0x0001C5F0
		public UnsafeHashMap<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new UnsafeHashMap<TKey, TValue>.Enumerator
			{
				m_Enumerator = new UnsafeParallelHashMapDataEnumerator(this.m_Buffer)
			};
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00004A65 File Offset: 0x00002C65
		IEnumerator<KeyValue<TKey, TValue>> IEnumerable<KeyValue<TKey, TValue>>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00004A65 File Offset: 0x00002C65
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000358 RID: 856
		[NativeDisableUnsafePtrRestriction]
		internal unsafe UnsafeParallelHashMapData* m_Buffer;

		// Token: 0x04000359 RID: 857
		internal AllocatorManager.AllocatorHandle m_AllocatorLabel;

		// Token: 0x0200010F RID: 271
		[NativeContainerIsAtomicWriteOnly]
		public struct ParallelWriter
		{
			// Token: 0x17000111 RID: 273
			// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0001E418 File Offset: 0x0001C618
			public unsafe int Capacity
			{
				get
				{
					return this.m_Buffer->keyCapacity;
				}
			}

			// Token: 0x06000A0C RID: 2572 RVA: 0x0001E425 File Offset: 0x0001C625
			public bool TryAdd(TKey key, TValue item)
			{
				return UnsafeParallelHashMapBase<TKey, TValue>.TryAddAtomic(this.m_Buffer, key, item, this.m_ThreadIndex);
			}

			// Token: 0x0400035A RID: 858
			[NativeDisableUnsafePtrRestriction]
			internal unsafe UnsafeParallelHashMapData* m_Buffer;

			// Token: 0x0400035B RID: 859
			[NativeSetThreadIndex]
			internal int m_ThreadIndex;
		}

		// Token: 0x02000110 RID: 272
		public struct Enumerator : IEnumerator<KeyValue<TKey, TValue>>, IEnumerator, IDisposable
		{
			// Token: 0x06000A0D RID: 2573 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x06000A0E RID: 2574 RVA: 0x0001E43A File Offset: 0x0001C63A
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x06000A0F RID: 2575 RVA: 0x0001E447 File Offset: 0x0001C647
			public void Reset()
			{
				this.m_Enumerator.Reset();
			}

			// Token: 0x17000112 RID: 274
			// (get) Token: 0x06000A10 RID: 2576 RVA: 0x0001E454 File Offset: 0x0001C654
			public KeyValue<TKey, TValue> Current
			{
				get
				{
					return this.m_Enumerator.GetCurrent<TKey, TValue>();
				}
			}

			// Token: 0x17000113 RID: 275
			// (get) Token: 0x06000A11 RID: 2577 RVA: 0x0001E461 File Offset: 0x0001C661
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0400035C RID: 860
			internal UnsafeParallelHashMapDataEnumerator m_Enumerator;
		}
	}
}
