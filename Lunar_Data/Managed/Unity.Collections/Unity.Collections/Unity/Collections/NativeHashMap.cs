using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x02000048 RID: 72
	[Obsolete("NativeHashMap is renamed to NativeParallelHashMap. (UnityUpgradable) -> NativeParallelHashMap<TKey, TValue>", false)]
	[NativeContainer]
	public struct NativeHashMap<TKey, TValue> : INativeDisposable, IDisposable, IEnumerable<KeyValue<TKey, TValue>>, IEnumerable where TKey : struct, IEquatable<TKey> where TValue : struct
	{
		// Token: 0x0600013B RID: 315 RVA: 0x0000488F File Offset: 0x00002A8F
		public NativeHashMap(int capacity, AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeHashMap<TKey, TValue>(capacity, allocator, 2);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000489A File Offset: 0x00002A9A
		private NativeHashMap(int capacity, AllocatorManager.AllocatorHandle allocator, int disposeSentinelStackDepth)
		{
			this.m_HashMapData = new UnsafeParallelHashMap<TKey, TValue>(capacity, allocator);
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600013D RID: 317 RVA: 0x000048A9 File Offset: 0x00002AA9
		public bool IsEmpty
		{
			get
			{
				return !this.IsCreated || this.m_HashMapData.IsEmpty;
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000048C0 File Offset: 0x00002AC0
		public int Count()
		{
			return this.m_HashMapData.Count();
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000048CD File Offset: 0x00002ACD
		// (set) Token: 0x06000140 RID: 320 RVA: 0x000048DA File Offset: 0x00002ADA
		public int Capacity
		{
			get
			{
				return this.m_HashMapData.Capacity;
			}
			set
			{
				this.m_HashMapData.Capacity = value;
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000048E8 File Offset: 0x00002AE8
		public void Clear()
		{
			this.m_HashMapData.Clear();
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000048F5 File Offset: 0x00002AF5
		public bool TryAdd(TKey key, TValue item)
		{
			return this.m_HashMapData.TryAdd(key, item);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004904 File Offset: 0x00002B04
		public void Add(TKey key, TValue item)
		{
			this.TryAdd(key, item);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000490F File Offset: 0x00002B0F
		public bool Remove(TKey key)
		{
			return this.m_HashMapData.Remove(key);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000491D File Offset: 0x00002B1D
		public bool TryGetValue(TKey key, out TValue item)
		{
			return this.m_HashMapData.TryGetValue(key, out item);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000492C File Offset: 0x00002B2C
		public bool ContainsKey(TKey key)
		{
			return this.m_HashMapData.ContainsKey(key);
		}

		// Token: 0x17000023 RID: 35
		public TValue this[TKey key]
		{
			get
			{
				TValue tvalue;
				if (this.m_HashMapData.TryGetValue(key, out tvalue))
				{
					return tvalue;
				}
				return default(TValue);
			}
			set
			{
				this.m_HashMapData[key] = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00004973 File Offset: 0x00002B73
		public bool IsCreated
		{
			get
			{
				return this.m_HashMapData.IsCreated;
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004980 File Offset: 0x00002B80
		public void Dispose()
		{
			this.m_HashMapData.Dispose();
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004990 File Offset: 0x00002B90
		[NotBurstCompatible]
		public JobHandle Dispose(JobHandle inputDeps)
		{
			JobHandle jobHandle = new UnsafeParallelHashMapDataDisposeJob
			{
				Data = new UnsafeParallelHashMapDataDispose
				{
					m_Buffer = this.m_HashMapData.m_Buffer,
					m_AllocatorLabel = this.m_HashMapData.m_AllocatorLabel
				}
			}.Schedule(inputDeps);
			this.m_HashMapData.m_Buffer = null;
			return jobHandle;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000049ED File Offset: 0x00002BED
		public NativeArray<TKey> GetKeyArray(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_HashMapData.GetKeyArray(allocator);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000049FB File Offset: 0x00002BFB
		public NativeArray<TValue> GetValueArray(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_HashMapData.GetValueArray(allocator);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00004A09 File Offset: 0x00002C09
		public NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_HashMapData.GetKeyValueArrays(allocator);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00004A18 File Offset: 0x00002C18
		public NativeHashMap<TKey, TValue>.ParallelWriter AsParallelWriter()
		{
			NativeHashMap<TKey, TValue>.ParallelWriter parallelWriter;
			parallelWriter.m_Writer = this.m_HashMapData.AsParallelWriter();
			return parallelWriter;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00004A38 File Offset: 0x00002C38
		public NativeHashMap<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new NativeHashMap<TKey, TValue>.Enumerator
			{
				m_Enumerator = new UnsafeParallelHashMapDataEnumerator(this.m_HashMapData.m_Buffer)
			};
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00004A65 File Offset: 0x00002C65
		IEnumerator<KeyValue<TKey, TValue>> IEnumerable<KeyValue<TKey, TValue>>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00004A65 File Offset: 0x00002C65
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00002C2B File Offset: 0x00000E2B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckRead()
		{
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00002C2B File Offset: 0x00000E2B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckWrite()
		{
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00004A6C File Offset: 0x00002C6C
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void ThrowKeyNotPresent(TKey key)
		{
			throw new ArgumentException(string.Format("Key: {0} is not present in the NativeParallelHashMap.", key));
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00004A83 File Offset: 0x00002C83
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void ThrowKeyAlreadyAdded(TKey key)
		{
			throw new ArgumentException("An item with the same key has already been added", "key");
		}

		// Token: 0x040000A1 RID: 161
		internal UnsafeParallelHashMap<TKey, TValue> m_HashMapData;

		// Token: 0x02000049 RID: 73
		[NativeContainer]
		[NativeContainerIsAtomicWriteOnly]
		[DebuggerDisplay("Capacity = {m_Writer.Capacity}")]
		public struct ParallelWriter
		{
			// Token: 0x17000025 RID: 37
			// (get) Token: 0x06000157 RID: 343 RVA: 0x00004A94 File Offset: 0x00002C94
			public int m_ThreadIndex
			{
				get
				{
					return this.m_Writer.m_ThreadIndex;
				}
			}

			// Token: 0x17000026 RID: 38
			// (get) Token: 0x06000158 RID: 344 RVA: 0x00004AA1 File Offset: 0x00002CA1
			public int Capacity
			{
				get
				{
					return this.m_Writer.Capacity;
				}
			}

			// Token: 0x06000159 RID: 345 RVA: 0x00004AAE File Offset: 0x00002CAE
			public bool TryAdd(TKey key, TValue item)
			{
				return this.m_Writer.TryAdd(key, item);
			}

			// Token: 0x040000A2 RID: 162
			internal UnsafeParallelHashMap<TKey, TValue>.ParallelWriter m_Writer;
		}

		// Token: 0x0200004A RID: 74
		[NativeContainer]
		[NativeContainerIsReadOnly]
		public struct Enumerator : IEnumerator<KeyValue<TKey, TValue>>, IEnumerator, IDisposable
		{
			// Token: 0x0600015A RID: 346 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x0600015B RID: 347 RVA: 0x00004ABD File Offset: 0x00002CBD
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x0600015C RID: 348 RVA: 0x00004ACA File Offset: 0x00002CCA
			public void Reset()
			{
				this.m_Enumerator.Reset();
			}

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x0600015D RID: 349 RVA: 0x00004AD7 File Offset: 0x00002CD7
			public KeyValue<TKey, TValue> Current
			{
				get
				{
					return this.m_Enumerator.GetCurrent<TKey, TValue>();
				}
			}

			// Token: 0x17000028 RID: 40
			// (get) Token: 0x0600015E RID: 350 RVA: 0x00004AE4 File Offset: 0x00002CE4
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040000A3 RID: 163
			internal UnsafeParallelHashMapDataEnumerator m_Enumerator;
		}
	}
}
