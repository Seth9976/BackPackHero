using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000B8 RID: 184
	[NativeContainer]
	[DebuggerDisplay("Count = {m_HashMapData.Count()}, Capacity = {m_HashMapData.Capacity}, IsCreated = {m_HashMapData.IsCreated}, IsEmpty = {IsEmpty}")]
	[DebuggerTypeProxy(typeof(NativeParallelHashMapDebuggerTypeProxy<, >))]
	[BurstCompatible(GenericTypeArguments = new Type[]
	{
		typeof(int),
		typeof(int)
	})]
	public struct NativeParallelHashMap<TKey, TValue> : INativeDisposable, IDisposable, IEnumerable<KeyValue<TKey, TValue>>, IEnumerable where TKey : struct, IEquatable<TKey> where TValue : struct
	{
		// Token: 0x0600070B RID: 1803 RVA: 0x00016100 File Offset: 0x00014300
		public NativeParallelHashMap(int capacity, AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeParallelHashMap<TKey, TValue>(capacity, allocator, 2);
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001610B File Offset: 0x0001430B
		private NativeParallelHashMap(int capacity, AllocatorManager.AllocatorHandle allocator, int disposeSentinelStackDepth)
		{
			this.m_HashMapData = new UnsafeParallelHashMap<TKey, TValue>(capacity, allocator);
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x0001611A File Offset: 0x0001431A
		public bool IsEmpty
		{
			get
			{
				return !this.IsCreated || this.m_HashMapData.IsEmpty;
			}
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00016131 File Offset: 0x00014331
		public int Count()
		{
			return this.m_HashMapData.Count();
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x0001613E File Offset: 0x0001433E
		// (set) Token: 0x06000710 RID: 1808 RVA: 0x0001614B File Offset: 0x0001434B
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

		// Token: 0x06000711 RID: 1809 RVA: 0x00016159 File Offset: 0x00014359
		public void Clear()
		{
			this.m_HashMapData.Clear();
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00016166 File Offset: 0x00014366
		public bool TryAdd(TKey key, TValue item)
		{
			return this.m_HashMapData.TryAdd(key, item);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00016175 File Offset: 0x00014375
		public void Add(TKey key, TValue item)
		{
			this.TryAdd(key, item);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00016180 File Offset: 0x00014380
		public bool Remove(TKey key)
		{
			return this.m_HashMapData.Remove(key);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0001618E File Offset: 0x0001438E
		public bool TryGetValue(TKey key, out TValue item)
		{
			return this.m_HashMapData.TryGetValue(key, out item);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0001619D File Offset: 0x0001439D
		public bool ContainsKey(TKey key)
		{
			return this.m_HashMapData.ContainsKey(key);
		}

		// Token: 0x170000C1 RID: 193
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

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x000161E3 File Offset: 0x000143E3
		public bool IsCreated
		{
			get
			{
				return this.m_HashMapData.IsCreated;
			}
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x000161F0 File Offset: 0x000143F0
		public void Dispose()
		{
			this.m_HashMapData.Dispose();
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00016200 File Offset: 0x00014400
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

		// Token: 0x0600071C RID: 1820 RVA: 0x0001625D File Offset: 0x0001445D
		public NativeArray<TKey> GetKeyArray(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_HashMapData.GetKeyArray(allocator);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0001626B File Offset: 0x0001446B
		public NativeArray<TValue> GetValueArray(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_HashMapData.GetValueArray(allocator);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00016279 File Offset: 0x00014479
		public NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_HashMapData.GetKeyValueArrays(allocator);
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00016288 File Offset: 0x00014488
		public NativeParallelHashMap<TKey, TValue>.ParallelWriter AsParallelWriter()
		{
			NativeParallelHashMap<TKey, TValue>.ParallelWriter parallelWriter;
			parallelWriter.m_Writer = this.m_HashMapData.AsParallelWriter();
			return parallelWriter;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x000162A8 File Offset: 0x000144A8
		public NativeParallelHashMap<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new NativeParallelHashMap<TKey, TValue>.Enumerator
			{
				m_Enumerator = new UnsafeParallelHashMapDataEnumerator(this.m_HashMapData.m_Buffer)
			};
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00004A65 File Offset: 0x00002C65
		IEnumerator<KeyValue<TKey, TValue>> IEnumerable<KeyValue<TKey, TValue>>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00004A65 File Offset: 0x00002C65
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00002C2B File Offset: 0x00000E2B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckRead()
		{
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00002C2B File Offset: 0x00000E2B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckWrite()
		{
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00004A6C File Offset: 0x00002C6C
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void ThrowKeyNotPresent(TKey key)
		{
			throw new ArgumentException(string.Format("Key: {0} is not present in the NativeParallelHashMap.", key));
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00004A83 File Offset: 0x00002C83
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void ThrowKeyAlreadyAdded(TKey key)
		{
			throw new ArgumentException("An item with the same key has already been added", "key");
		}

		// Token: 0x04000287 RID: 647
		internal UnsafeParallelHashMap<TKey, TValue> m_HashMapData;

		// Token: 0x020000B9 RID: 185
		[NativeContainer]
		[NativeContainerIsAtomicWriteOnly]
		[DebuggerDisplay("Capacity = {m_Writer.Capacity}")]
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public struct ParallelWriter
		{
			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x06000727 RID: 1831 RVA: 0x000162D5 File Offset: 0x000144D5
			public int m_ThreadIndex
			{
				get
				{
					return this.m_Writer.m_ThreadIndex;
				}
			}

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x06000728 RID: 1832 RVA: 0x000162E2 File Offset: 0x000144E2
			public int Capacity
			{
				get
				{
					return this.m_Writer.Capacity;
				}
			}

			// Token: 0x06000729 RID: 1833 RVA: 0x000162EF File Offset: 0x000144EF
			public bool TryAdd(TKey key, TValue item)
			{
				return this.m_Writer.TryAdd(key, item);
			}

			// Token: 0x04000288 RID: 648
			internal UnsafeParallelHashMap<TKey, TValue>.ParallelWriter m_Writer;
		}

		// Token: 0x020000BA RID: 186
		[NativeContainer]
		[NativeContainerIsReadOnly]
		public struct Enumerator : IEnumerator<KeyValue<TKey, TValue>>, IEnumerator, IDisposable
		{
			// Token: 0x0600072A RID: 1834 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x0600072B RID: 1835 RVA: 0x000162FE File Offset: 0x000144FE
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x0600072C RID: 1836 RVA: 0x0001630B File Offset: 0x0001450B
			public void Reset()
			{
				this.m_Enumerator.Reset();
			}

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x0600072D RID: 1837 RVA: 0x00016318 File Offset: 0x00014518
			public KeyValue<TKey, TValue> Current
			{
				get
				{
					return this.m_Enumerator.GetCurrent<TKey, TValue>();
				}
			}

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x0600072E RID: 1838 RVA: 0x00016325 File Offset: 0x00014525
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000289 RID: 649
			internal UnsafeParallelHashMapDataEnumerator m_Enumerator;
		}
	}
}
