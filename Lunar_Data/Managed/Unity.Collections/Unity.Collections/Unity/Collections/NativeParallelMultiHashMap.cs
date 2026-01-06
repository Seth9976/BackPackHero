using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000C3 RID: 195
	[NativeContainer]
	[DebuggerTypeProxy(typeof(NativeParallelMultiHashMapDebuggerTypeProxy<, >))]
	[BurstCompatible(GenericTypeArguments = new Type[]
	{
		typeof(int),
		typeof(int)
	})]
	public struct NativeParallelMultiHashMap<TKey, TValue> : INativeDisposable, IDisposable, IEnumerable<KeyValue<TKey, TValue>>, IEnumerable where TKey : struct, IEquatable<TKey> where TValue : struct
	{
		// Token: 0x0600076B RID: 1899 RVA: 0x00017010 File Offset: 0x00015210
		public NativeParallelMultiHashMap(int capacity, AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeParallelMultiHashMap<TKey, TValue>(capacity, allocator, 2);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0001701B File Offset: 0x0001521B
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(AllocatorManager.AllocatorHandle) })]
		internal void Initialize<[global::System.Runtime.CompilerServices.IsUnmanaged] U>(int capacity, ref U allocator, int disposeSentinelStackDepth) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			this.m_MultiHashMapData = new UnsafeParallelMultiHashMap<TKey, TValue>(capacity, allocator.Handle);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00017035 File Offset: 0x00015235
		private NativeParallelMultiHashMap(int capacity, AllocatorManager.AllocatorHandle allocator, int disposeSentinelStackDepth)
		{
			this = default(NativeParallelMultiHashMap<TKey, TValue>);
			this.Initialize<AllocatorManager.AllocatorHandle>(capacity, ref allocator, disposeSentinelStackDepth);
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x00017048 File Offset: 0x00015248
		public bool IsEmpty
		{
			get
			{
				return this.m_MultiHashMapData.IsEmpty;
			}
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00017055 File Offset: 0x00015255
		public int Count()
		{
			return this.m_MultiHashMapData.Count();
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x00017062 File Offset: 0x00015262
		// (set) Token: 0x06000771 RID: 1905 RVA: 0x0001706F File Offset: 0x0001526F
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

		// Token: 0x06000772 RID: 1906 RVA: 0x0001707D File Offset: 0x0001527D
		public void Clear()
		{
			this.m_MultiHashMapData.Clear();
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0001708A File Offset: 0x0001528A
		public void Add(TKey key, TValue item)
		{
			this.m_MultiHashMapData.Add(key, item);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00017099 File Offset: 0x00015299
		public int Remove(TKey key)
		{
			return this.m_MultiHashMapData.Remove(key);
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x000170A7 File Offset: 0x000152A7
		public void Remove(NativeParallelMultiHashMapIterator<TKey> it)
		{
			this.m_MultiHashMapData.Remove(it);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x000170B5 File Offset: 0x000152B5
		public bool TryGetFirstValue(TKey key, out TValue item, out NativeParallelMultiHashMapIterator<TKey> it)
		{
			return this.m_MultiHashMapData.TryGetFirstValue(key, out item, out it);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x000170C5 File Offset: 0x000152C5
		public bool TryGetNextValue(out TValue item, ref NativeParallelMultiHashMapIterator<TKey> it)
		{
			return this.m_MultiHashMapData.TryGetNextValue(out item, ref it);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x000170D4 File Offset: 0x000152D4
		public bool ContainsKey(TKey key)
		{
			TValue tvalue;
			NativeParallelMultiHashMapIterator<TKey> nativeParallelMultiHashMapIterator;
			return this.TryGetFirstValue(key, out tvalue, out nativeParallelMultiHashMapIterator);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x000170EC File Offset: 0x000152EC
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

		// Token: 0x0600077A RID: 1914 RVA: 0x0001711D File Offset: 0x0001531D
		public bool SetValue(TValue item, NativeParallelMultiHashMapIterator<TKey> it)
		{
			return this.m_MultiHashMapData.SetValue(item, it);
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x0001712C File Offset: 0x0001532C
		public bool IsCreated
		{
			get
			{
				return this.m_MultiHashMapData.IsCreated;
			}
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00017139 File Offset: 0x00015339
		public void Dispose()
		{
			this.m_MultiHashMapData.Dispose();
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00017148 File Offset: 0x00015348
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

		// Token: 0x0600077E RID: 1918 RVA: 0x000171A5 File Offset: 0x000153A5
		public NativeArray<TKey> GetKeyArray(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_MultiHashMapData.GetKeyArray(allocator);
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x000171B3 File Offset: 0x000153B3
		public NativeArray<TValue> GetValueArray(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_MultiHashMapData.GetValueArray(allocator);
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x000171C1 File Offset: 0x000153C1
		public NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_MultiHashMapData.GetKeyValueArrays(allocator);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x000171D0 File Offset: 0x000153D0
		public NativeParallelMultiHashMap<TKey, TValue>.ParallelWriter AsParallelWriter()
		{
			NativeParallelMultiHashMap<TKey, TValue>.ParallelWriter parallelWriter;
			parallelWriter.m_Writer = this.m_MultiHashMapData.AsParallelWriter();
			return parallelWriter;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x000171F0 File Offset: 0x000153F0
		public NativeParallelMultiHashMap<TKey, TValue>.Enumerator GetValuesForKey(TKey key)
		{
			return new NativeParallelMultiHashMap<TKey, TValue>.Enumerator
			{
				hashmap = this,
				key = key,
				isFirst = true
			};
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00017224 File Offset: 0x00015424
		public NativeParallelMultiHashMap<TKey, TValue>.KeyValueEnumerator GetEnumerator()
		{
			return new NativeParallelMultiHashMap<TKey, TValue>.KeyValueEnumerator
			{
				m_Enumerator = new UnsafeParallelHashMapDataEnumerator(this.m_MultiHashMapData.m_Buffer)
			};
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00004A65 File Offset: 0x00002C65
		IEnumerator<KeyValue<TKey, TValue>> IEnumerable<KeyValue<TKey, TValue>>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00004A65 File Offset: 0x00002C65
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00002C2B File Offset: 0x00000E2B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckRead()
		{
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00002C2B File Offset: 0x00000E2B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckWrite()
		{
		}

		// Token: 0x04000292 RID: 658
		internal UnsafeParallelMultiHashMap<TKey, TValue> m_MultiHashMapData;

		// Token: 0x020000C4 RID: 196
		[NativeContainer]
		[NativeContainerIsAtomicWriteOnly]
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public struct ParallelWriter
		{
			// Token: 0x170000D2 RID: 210
			// (get) Token: 0x06000788 RID: 1928 RVA: 0x00017251 File Offset: 0x00015451
			public int m_ThreadIndex
			{
				get
				{
					return this.m_Writer.m_ThreadIndex;
				}
			}

			// Token: 0x170000D3 RID: 211
			// (get) Token: 0x06000789 RID: 1929 RVA: 0x0001725E File Offset: 0x0001545E
			public int Capacity
			{
				get
				{
					return this.m_Writer.Capacity;
				}
			}

			// Token: 0x0600078A RID: 1930 RVA: 0x0001726B File Offset: 0x0001546B
			public void Add(TKey key, TValue item)
			{
				this.m_Writer.Add(key, item);
			}

			// Token: 0x04000293 RID: 659
			internal UnsafeParallelMultiHashMap<TKey, TValue>.ParallelWriter m_Writer;
		}

		// Token: 0x020000C5 RID: 197
		public struct Enumerator : IEnumerator<TValue>, IEnumerator, IDisposable
		{
			// Token: 0x0600078B RID: 1931 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x0600078C RID: 1932 RVA: 0x0001727C File Offset: 0x0001547C
			public bool MoveNext()
			{
				if (this.isFirst)
				{
					this.isFirst = false;
					return this.hashmap.TryGetFirstValue(this.key, out this.value, out this.iterator);
				}
				return this.hashmap.TryGetNextValue(out this.value, ref this.iterator);
			}

			// Token: 0x0600078D RID: 1933 RVA: 0x000172CD File Offset: 0x000154CD
			public void Reset()
			{
				this.isFirst = true;
			}

			// Token: 0x170000D4 RID: 212
			// (get) Token: 0x0600078E RID: 1934 RVA: 0x000172D6 File Offset: 0x000154D6
			public TValue Current
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x0600078F RID: 1935 RVA: 0x000172DE File Offset: 0x000154DE
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000790 RID: 1936 RVA: 0x000172EB File Offset: 0x000154EB
			public NativeParallelMultiHashMap<TKey, TValue>.Enumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x04000294 RID: 660
			internal NativeParallelMultiHashMap<TKey, TValue> hashmap;

			// Token: 0x04000295 RID: 661
			internal TKey key;

			// Token: 0x04000296 RID: 662
			internal bool isFirst;

			// Token: 0x04000297 RID: 663
			private TValue value;

			// Token: 0x04000298 RID: 664
			private NativeParallelMultiHashMapIterator<TKey> iterator;
		}

		// Token: 0x020000C6 RID: 198
		[NativeContainer]
		[NativeContainerIsReadOnly]
		public struct KeyValueEnumerator : IEnumerator<KeyValue<TKey, TValue>>, IEnumerator, IDisposable
		{
			// Token: 0x06000791 RID: 1937 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x06000792 RID: 1938 RVA: 0x000172F3 File Offset: 0x000154F3
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x06000793 RID: 1939 RVA: 0x00017300 File Offset: 0x00015500
			public void Reset()
			{
				this.m_Enumerator.Reset();
			}

			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x06000794 RID: 1940 RVA: 0x0001730D File Offset: 0x0001550D
			public KeyValue<TKey, TValue> Current
			{
				get
				{
					return this.m_Enumerator.GetCurrent<TKey, TValue>();
				}
			}

			// Token: 0x170000D7 RID: 215
			// (get) Token: 0x06000795 RID: 1941 RVA: 0x0001731A File Offset: 0x0001551A
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000299 RID: 665
			internal UnsafeParallelHashMapDataEnumerator m_Enumerator;
		}
	}
}
