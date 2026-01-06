using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x0200004B RID: 75
	[Obsolete("NativeMultiHashMap is renamed to NativeParallelMultiHashMap. (UnityUpgradable) -> NativeParallelMultiHashMap<TKey, TValue>", false)]
	[NativeContainer]
	public struct NativeMultiHashMap<TKey, TValue> : INativeDisposable, IDisposable, IEnumerable<KeyValue<TKey, TValue>>, IEnumerable where TKey : struct, IEquatable<TKey> where TValue : struct
	{
		// Token: 0x0600015F RID: 351 RVA: 0x00004AF1 File Offset: 0x00002CF1
		public NativeMultiHashMap(int capacity, AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeMultiHashMap<TKey, TValue>(capacity, allocator, 2);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00004AFC File Offset: 0x00002CFC
		internal void Initialize<[global::System.Runtime.CompilerServices.IsUnmanaged] U>(int capacity, ref U allocator, int disposeSentinelStackDepth) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			this.m_MultiHashMapData = new UnsafeParallelMultiHashMap<TKey, TValue>(capacity, allocator.Handle);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00004B16 File Offset: 0x00002D16
		private NativeMultiHashMap(int capacity, AllocatorManager.AllocatorHandle allocator, int disposeSentinelStackDepth)
		{
			this = default(NativeMultiHashMap<TKey, TValue>);
			this.Initialize<AllocatorManager.AllocatorHandle>(capacity, ref allocator, disposeSentinelStackDepth);
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00004B29 File Offset: 0x00002D29
		public bool IsEmpty
		{
			get
			{
				return this.m_MultiHashMapData.IsEmpty;
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00004B36 File Offset: 0x00002D36
		public int Count()
		{
			return this.m_MultiHashMapData.Count();
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00004B43 File Offset: 0x00002D43
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00004B50 File Offset: 0x00002D50
		public int Capacity
		{
			get
			{
				return this.m_MultiHashMapData.Capacity;
			}
			set
			{
				this.m_MultiHashMapData.Capacity = value;
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00004B5E File Offset: 0x00002D5E
		public void Clear()
		{
			this.m_MultiHashMapData.Clear();
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00004B6B File Offset: 0x00002D6B
		public void Add(TKey key, TValue item)
		{
			this.m_MultiHashMapData.Add(key, item);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00004B7A File Offset: 0x00002D7A
		public int Remove(TKey key)
		{
			return this.m_MultiHashMapData.Remove(key);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00004B88 File Offset: 0x00002D88
		public void Remove(NativeParallelMultiHashMapIterator<TKey> it)
		{
			this.m_MultiHashMapData.Remove(it);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00004B96 File Offset: 0x00002D96
		public bool TryGetFirstValue(TKey key, out TValue item, out NativeParallelMultiHashMapIterator<TKey> it)
		{
			return this.m_MultiHashMapData.TryGetFirstValue(key, out item, out it);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00004BA6 File Offset: 0x00002DA6
		public bool TryGetNextValue(out TValue item, ref NativeParallelMultiHashMapIterator<TKey> it)
		{
			return this.m_MultiHashMapData.TryGetNextValue(out item, ref it);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00004BB8 File Offset: 0x00002DB8
		public bool ContainsKey(TKey key)
		{
			TValue tvalue;
			NativeParallelMultiHashMapIterator<TKey> nativeParallelMultiHashMapIterator;
			return this.TryGetFirstValue(key, out tvalue, out nativeParallelMultiHashMapIterator);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00004BD0 File Offset: 0x00002DD0
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

		// Token: 0x0600016E RID: 366 RVA: 0x00004C01 File Offset: 0x00002E01
		public bool SetValue(TValue item, NativeParallelMultiHashMapIterator<TKey> it)
		{
			return this.m_MultiHashMapData.SetValue(item, it);
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00004C10 File Offset: 0x00002E10
		public bool IsCreated
		{
			get
			{
				return this.m_MultiHashMapData.IsCreated;
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00004C1D File Offset: 0x00002E1D
		public void Dispose()
		{
			this.m_MultiHashMapData.Dispose();
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00004C2C File Offset: 0x00002E2C
		[NotBurstCompatible]
		public JobHandle Dispose(JobHandle inputDeps)
		{
			JobHandle jobHandle = new UnsafeParallelHashMapDataDisposeJob
			{
				Data = new UnsafeParallelHashMapDataDispose
				{
					m_Buffer = this.m_MultiHashMapData.m_Buffer,
					m_AllocatorLabel = this.m_MultiHashMapData.m_AllocatorLabel
				}
			}.Schedule(inputDeps);
			this.m_MultiHashMapData.m_Buffer = null;
			return jobHandle;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00004C89 File Offset: 0x00002E89
		public NativeArray<TKey> GetKeyArray(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_MultiHashMapData.GetKeyArray(allocator);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00004C97 File Offset: 0x00002E97
		public NativeArray<TValue> GetValueArray(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_MultiHashMapData.GetValueArray(allocator);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00004CA5 File Offset: 0x00002EA5
		public NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_MultiHashMapData.GetKeyValueArrays(allocator);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00004CB4 File Offset: 0x00002EB4
		public NativeMultiHashMap<TKey, TValue>.ParallelWriter AsParallelWriter()
		{
			NativeMultiHashMap<TKey, TValue>.ParallelWriter parallelWriter;
			parallelWriter.m_Writer = this.m_MultiHashMapData.AsParallelWriter();
			return parallelWriter;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00004CD4 File Offset: 0x00002ED4
		public NativeMultiHashMap<TKey, TValue>.Enumerator GetValuesForKey(TKey key)
		{
			return new NativeMultiHashMap<TKey, TValue>.Enumerator
			{
				hashmap = this,
				key = key,
				isFirst = true
			};
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00004D08 File Offset: 0x00002F08
		public NativeMultiHashMap<TKey, TValue>.KeyValueEnumerator GetEnumerator()
		{
			return new NativeMultiHashMap<TKey, TValue>.KeyValueEnumerator
			{
				m_Enumerator = new UnsafeParallelHashMapDataEnumerator(this.m_MultiHashMapData.m_Buffer)
			};
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00004A65 File Offset: 0x00002C65
		IEnumerator<KeyValue<TKey, TValue>> IEnumerable<KeyValue<TKey, TValue>>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00004A65 File Offset: 0x00002C65
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00002C2B File Offset: 0x00000E2B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckRead()
		{
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00002C2B File Offset: 0x00000E2B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckWrite()
		{
		}

		// Token: 0x040000A4 RID: 164
		internal UnsafeParallelMultiHashMap<TKey, TValue> m_MultiHashMapData;

		// Token: 0x0200004C RID: 76
		[NativeContainer]
		[NativeContainerIsAtomicWriteOnly]
		public struct ParallelWriter
		{
			// Token: 0x1700002C RID: 44
			// (get) Token: 0x0600017C RID: 380 RVA: 0x00004D35 File Offset: 0x00002F35
			public int m_ThreadIndex
			{
				get
				{
					return this.m_Writer.m_ThreadIndex;
				}
			}

			// Token: 0x1700002D RID: 45
			// (get) Token: 0x0600017D RID: 381 RVA: 0x00004D42 File Offset: 0x00002F42
			public int Capacity
			{
				get
				{
					return this.m_Writer.Capacity;
				}
			}

			// Token: 0x0600017E RID: 382 RVA: 0x00004D4F File Offset: 0x00002F4F
			public void Add(TKey key, TValue item)
			{
				this.m_Writer.Add(key, item);
			}

			// Token: 0x040000A5 RID: 165
			internal UnsafeParallelMultiHashMap<TKey, TValue>.ParallelWriter m_Writer;
		}

		// Token: 0x0200004D RID: 77
		public struct Enumerator : IEnumerator<TValue>, IEnumerator, IDisposable
		{
			// Token: 0x0600017F RID: 383 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x06000180 RID: 384 RVA: 0x00004D60 File Offset: 0x00002F60
			public bool MoveNext()
			{
				if (this.isFirst)
				{
					this.isFirst = false;
					return this.hashmap.TryGetFirstValue(this.key, out this.value, out this.iterator);
				}
				return this.hashmap.TryGetNextValue(out this.value, ref this.iterator);
			}

			// Token: 0x06000181 RID: 385 RVA: 0x00004DB1 File Offset: 0x00002FB1
			public void Reset()
			{
				this.isFirst = true;
			}

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x06000182 RID: 386 RVA: 0x00004DBA File Offset: 0x00002FBA
			public TValue Current
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x06000183 RID: 387 RVA: 0x00004DC2 File Offset: 0x00002FC2
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000184 RID: 388 RVA: 0x00004DCF File Offset: 0x00002FCF
			public NativeMultiHashMap<TKey, TValue>.Enumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x040000A6 RID: 166
			internal NativeMultiHashMap<TKey, TValue> hashmap;

			// Token: 0x040000A7 RID: 167
			internal TKey key;

			// Token: 0x040000A8 RID: 168
			internal bool isFirst;

			// Token: 0x040000A9 RID: 169
			private TValue value;

			// Token: 0x040000AA RID: 170
			private NativeParallelMultiHashMapIterator<TKey> iterator;
		}

		// Token: 0x0200004E RID: 78
		[NativeContainer]
		[NativeContainerIsReadOnly]
		public struct KeyValueEnumerator : IEnumerator<KeyValue<TKey, TValue>>, IEnumerator, IDisposable
		{
			// Token: 0x06000185 RID: 389 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x06000186 RID: 390 RVA: 0x00004DD7 File Offset: 0x00002FD7
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x06000187 RID: 391 RVA: 0x00004DE4 File Offset: 0x00002FE4
			public void Reset()
			{
				this.m_Enumerator.Reset();
			}

			// Token: 0x17000030 RID: 48
			// (get) Token: 0x06000188 RID: 392 RVA: 0x00004DF1 File Offset: 0x00002FF1
			public KeyValue<TKey, TValue> Current
			{
				get
				{
					return this.m_Enumerator.GetCurrent<TKey, TValue>();
				}
			}

			// Token: 0x17000031 RID: 49
			// (get) Token: 0x06000189 RID: 393 RVA: 0x00004DFE File Offset: 0x00002FFE
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040000AB RID: 171
			internal UnsafeParallelHashMapDataEnumerator m_Enumerator;
		}
	}
}
