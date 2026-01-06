using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000BD RID: 189
	[DebuggerTypeProxy(typeof(NativeHashSetDebuggerTypeProxy<>))]
	[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
	public struct NativeParallelHashSet<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : INativeDisposable, IDisposable, IEnumerable<T>, IEnumerable where T : struct, ValueType, IEquatable<T>
	{
		// Token: 0x06000738 RID: 1848 RVA: 0x000164B8 File Offset: 0x000146B8
		public NativeParallelHashSet(int capacity, AllocatorManager.AllocatorHandle allocator)
		{
			this.m_Data = new NativeParallelHashMap<T, bool>(capacity, allocator);
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x000164C7 File Offset: 0x000146C7
		public bool IsEmpty
		{
			get
			{
				return this.m_Data.IsEmpty;
			}
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x000164D4 File Offset: 0x000146D4
		public int Count()
		{
			return this.m_Data.Count();
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x000164E1 File Offset: 0x000146E1
		// (set) Token: 0x0600073C RID: 1852 RVA: 0x000164EE File Offset: 0x000146EE
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

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x000164FC File Offset: 0x000146FC
		public bool IsCreated
		{
			get
			{
				return this.m_Data.IsCreated;
			}
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00016509 File Offset: 0x00014709
		public void Dispose()
		{
			this.m_Data.Dispose();
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00016516 File Offset: 0x00014716
		[NotBurstCompatible]
		public JobHandle Dispose(JobHandle inputDeps)
		{
			return this.m_Data.Dispose(inputDeps);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00016524 File Offset: 0x00014724
		public void Clear()
		{
			this.m_Data.Clear();
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00016531 File Offset: 0x00014731
		public bool Add(T item)
		{
			return this.m_Data.TryAdd(item, false);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00016540 File Offset: 0x00014740
		public bool Remove(T item)
		{
			return this.m_Data.Remove(item);
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001654E File Offset: 0x0001474E
		public bool Contains(T item)
		{
			return this.m_Data.ContainsKey(item);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001655C File Offset: 0x0001475C
		public NativeArray<T> ToNativeArray(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_Data.GetKeyArray(allocator);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001656C File Offset: 0x0001476C
		public NativeParallelHashSet<T>.ParallelWriter AsParallelWriter()
		{
			NativeParallelHashSet<T>.ParallelWriter parallelWriter;
			parallelWriter.m_Data = this.m_Data.AsParallelWriter();
			return parallelWriter;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0001658C File Offset: 0x0001478C
		public NativeParallelHashSet<T>.Enumerator GetEnumerator()
		{
			return new NativeParallelHashSet<T>.Enumerator
			{
				m_Enumerator = new UnsafeParallelHashMapDataEnumerator(this.m_Data.m_HashMapData.m_Buffer)
			};
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00004A65 File Offset: 0x00002C65
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00004A65 File Offset: 0x00002C65
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400028B RID: 651
		internal NativeParallelHashMap<T, bool> m_Data;

		// Token: 0x020000BE RID: 190
		[NativeContainerIsAtomicWriteOnly]
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		public struct ParallelWriter
		{
			// Token: 0x170000CB RID: 203
			// (get) Token: 0x06000749 RID: 1865 RVA: 0x000165BE File Offset: 0x000147BE
			public int Capacity
			{
				get
				{
					return this.m_Data.Capacity;
				}
			}

			// Token: 0x0600074A RID: 1866 RVA: 0x000165CB File Offset: 0x000147CB
			public bool Add(T item)
			{
				return this.m_Data.TryAdd(item, false);
			}

			// Token: 0x0400028C RID: 652
			internal NativeParallelHashMap<T, bool>.ParallelWriter m_Data;
		}

		// Token: 0x020000BF RID: 191
		[NativeContainer]
		[NativeContainerIsReadOnly]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x0600074B RID: 1867 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x0600074C RID: 1868 RVA: 0x000165DA File Offset: 0x000147DA
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x0600074D RID: 1869 RVA: 0x000165E7 File Offset: 0x000147E7
			public void Reset()
			{
				this.m_Enumerator.Reset();
			}

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x0600074E RID: 1870 RVA: 0x000165F4 File Offset: 0x000147F4
			public T Current
			{
				get
				{
					return this.m_Enumerator.GetCurrentKey<T>();
				}
			}

			// Token: 0x170000CD RID: 205
			// (get) Token: 0x0600074F RID: 1871 RVA: 0x00016601 File Offset: 0x00014801
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0400028D RID: 653
			internal UnsafeParallelHashMapDataEnumerator m_Enumerator;
		}
	}
}
