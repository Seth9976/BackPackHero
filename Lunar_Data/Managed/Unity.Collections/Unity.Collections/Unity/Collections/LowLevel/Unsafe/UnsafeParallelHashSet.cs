using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Jobs;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200013C RID: 316
	[DebuggerTypeProxy(typeof(UnsafeHashSetDebuggerTypeProxy<>))]
	[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
	public struct UnsafeParallelHashSet<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : INativeDisposable, IDisposable, IEnumerable<T>, IEnumerable where T : struct, ValueType, IEquatable<T>
	{
		// Token: 0x06000B5F RID: 2911 RVA: 0x00021F04 File Offset: 0x00020104
		public UnsafeParallelHashSet(int capacity, AllocatorManager.AllocatorHandle allocator)
		{
			this.m_Data = new UnsafeParallelHashMap<T, bool>(capacity, allocator);
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x00021F13 File Offset: 0x00020113
		public bool IsEmpty
		{
			get
			{
				return this.m_Data.IsEmpty;
			}
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x00021F20 File Offset: 0x00020120
		public int Count()
		{
			return this.m_Data.Count();
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x00021F2D File Offset: 0x0002012D
		// (set) Token: 0x06000B63 RID: 2915 RVA: 0x00021F3A File Offset: 0x0002013A
		public int Capacity
		{
			get
			{
				return this.m_Data.Capacity;
			}
			set
			{
				this.m_Data.Capacity = value;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x00021F48 File Offset: 0x00020148
		public bool IsCreated
		{
			get
			{
				return this.m_Data.IsCreated;
			}
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00021F55 File Offset: 0x00020155
		public void Dispose()
		{
			this.m_Data.Dispose();
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x00021F62 File Offset: 0x00020162
		[NotBurstCompatible]
		public JobHandle Dispose(JobHandle inputDeps)
		{
			return this.m_Data.Dispose(inputDeps);
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x00021F70 File Offset: 0x00020170
		public void Clear()
		{
			this.m_Data.Clear();
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x00021F7D File Offset: 0x0002017D
		public bool Add(T item)
		{
			return this.m_Data.TryAdd(item, false);
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00021F8C File Offset: 0x0002018C
		public bool Remove(T item)
		{
			return this.m_Data.Remove(item);
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x00021F9A File Offset: 0x0002019A
		public bool Contains(T item)
		{
			return this.m_Data.ContainsKey(item);
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x00021FA8 File Offset: 0x000201A8
		public NativeArray<T> ToNativeArray(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_Data.GetKeyArray(allocator);
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x00021FB8 File Offset: 0x000201B8
		public UnsafeParallelHashSet<T>.ParallelWriter AsParallelWriter()
		{
			return new UnsafeParallelHashSet<T>.ParallelWriter
			{
				m_Data = this.m_Data.AsParallelWriter()
			};
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x00021FE0 File Offset: 0x000201E0
		public UnsafeParallelHashSet<T>.Enumerator GetEnumerator()
		{
			return new UnsafeParallelHashSet<T>.Enumerator
			{
				m_Enumerator = new UnsafeParallelHashMapDataEnumerator(this.m_Data.m_Buffer)
			};
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x00004A65 File Offset: 0x00002C65
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x00004A65 File Offset: 0x00002C65
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040003BB RID: 955
		internal UnsafeParallelHashMap<T, bool> m_Data;

		// Token: 0x0200013D RID: 317
		[NativeContainerIsAtomicWriteOnly]
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		public struct ParallelWriter
		{
			// Token: 0x17000144 RID: 324
			// (get) Token: 0x06000B70 RID: 2928 RVA: 0x0002200D File Offset: 0x0002020D
			public int Capacity
			{
				get
				{
					return this.m_Data.Capacity;
				}
			}

			// Token: 0x06000B71 RID: 2929 RVA: 0x0002201A File Offset: 0x0002021A
			public bool Add(T item)
			{
				return this.m_Data.TryAdd(item, false);
			}

			// Token: 0x040003BC RID: 956
			internal UnsafeParallelHashMap<T, bool>.ParallelWriter m_Data;
		}

		// Token: 0x0200013E RID: 318
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06000B72 RID: 2930 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x06000B73 RID: 2931 RVA: 0x00022029 File Offset: 0x00020229
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x06000B74 RID: 2932 RVA: 0x00022036 File Offset: 0x00020236
			public void Reset()
			{
				this.m_Enumerator.Reset();
			}

			// Token: 0x17000145 RID: 325
			// (get) Token: 0x06000B75 RID: 2933 RVA: 0x00022043 File Offset: 0x00020243
			public T Current
			{
				get
				{
					return this.m_Enumerator.GetCurrentKey<T>();
				}
			}

			// Token: 0x17000146 RID: 326
			// (get) Token: 0x06000B76 RID: 2934 RVA: 0x00022050 File Offset: 0x00020250
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040003BD RID: 957
			internal UnsafeParallelHashMapDataEnumerator m_Enumerator;
		}
	}
}
