using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Jobs;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000129 RID: 297
	[DebuggerDisplay("Length = {Length}, Capacity = {Capacity}, IsCreated = {IsCreated}, IsEmpty = {IsEmpty}")]
	[DebuggerTypeProxy(typeof(UnsafePtrListTDebugView<>))]
	[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
	public struct UnsafePtrList<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : INativeDisposable, IDisposable, IEnumerable<IntPtr>, IEnumerable where T : struct, ValueType
	{
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x00020676 File Offset: 0x0001E876
		// (set) Token: 0x06000AE3 RID: 2787 RVA: 0x00020683 File Offset: 0x0001E883
		public int Length
		{
			get
			{
				return (ref this).ListData<T>().Length;
			}
			set
			{
				(ref this).ListData<T>().Length = value;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x00020691 File Offset: 0x0001E891
		// (set) Token: 0x06000AE5 RID: 2789 RVA: 0x0002069E File Offset: 0x0001E89E
		public int Capacity
		{
			get
			{
				return (ref this).ListData<T>().Capacity;
			}
			set
			{
				(ref this).ListData<T>().Capacity = value;
			}
		}

		// Token: 0x17000132 RID: 306
		public unsafe T* this[int index]
		{
			get
			{
				return *(IntPtr*)(this.Ptr + (IntPtr)CollectionHelper.AssumePositive(index) * (IntPtr)sizeof(T*) / (IntPtr)sizeof(T*));
			}
			set
			{
				*(IntPtr*)(this.Ptr + (IntPtr)CollectionHelper.AssumePositive(index) * (IntPtr)sizeof(T*) / (IntPtr)sizeof(T*)) = value;
			}
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x000206DD File Offset: 0x0001E8DD
		public unsafe ref T* ElementAt(int index)
		{
			return ref this.Ptr[(IntPtr)CollectionHelper.AssumePositive(index) * (IntPtr)sizeof(T*) / (IntPtr)sizeof(T*)];
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x000206F4 File Offset: 0x0001E8F4
		public unsafe UnsafePtrList(T** ptr, int length)
		{
			this = default(UnsafePtrList<T>);
			this.Ptr = ptr;
			this.m_length = length;
			this.m_capacity = length;
			this.Allocator = AllocatorManager.None;
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0002071D File Offset: 0x0001E91D
		public unsafe UnsafePtrList(int initialCapacity, AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory)
		{
			this = default(UnsafePtrList<T>);
			this.Ptr = null;
			this.m_length = 0;
			this.m_capacity = 0;
			this.Allocator = AllocatorManager.None;
			*(ref this).ListData<T>() = new UnsafeList<IntPtr>(initialCapacity, allocator, options);
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0002075A File Offset: 0x0001E95A
		public unsafe static UnsafePtrList<T>* Create(T** ptr, int length)
		{
			UnsafePtrList<T>* ptr2 = AllocatorManager.Allocate<UnsafePtrList<T>>(AllocatorManager.Persistent, 1);
			*ptr2 = new UnsafePtrList<T>(ptr, length);
			return ptr2;
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00020774 File Offset: 0x0001E974
		public unsafe static UnsafePtrList<T>* Create(int initialCapacity, AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory)
		{
			UnsafePtrList<T>* ptr = AllocatorManager.Allocate<UnsafePtrList<T>>(allocator, 1);
			*ptr = new UnsafePtrList<T>(initialCapacity, allocator, options);
			return ptr;
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0002078C File Offset: 0x0001E98C
		public unsafe static void Destroy(UnsafePtrList<T>* listData)
		{
			AllocatorManager.AllocatorHandle allocatorHandle = (((ref *listData).ListData<T>().Allocator.Value == AllocatorManager.Invalid.Value) ? AllocatorManager.Persistent : (ref *listData).ListData<T>().Allocator);
			listData->Dispose();
			AllocatorManager.Free<UnsafePtrList<T>>(allocatorHandle, listData, 1);
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x000207D7 File Offset: 0x0001E9D7
		public bool IsEmpty
		{
			get
			{
				return !this.IsCreated || this.Length == 0;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x000207EC File Offset: 0x0001E9EC
		public bool IsCreated
		{
			get
			{
				return this.Ptr != null;
			}
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x000207FB File Offset: 0x0001E9FB
		public void Dispose()
		{
			(ref this).ListData<T>().Dispose();
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00020808 File Offset: 0x0001EA08
		[NotBurstCompatible]
		public JobHandle Dispose(JobHandle inputDeps)
		{
			return (ref this).ListData<T>().Dispose(inputDeps);
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00020816 File Offset: 0x0001EA16
		public void Clear()
		{
			(ref this).ListData<T>().Clear();
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00020823 File Offset: 0x0001EA23
		public void Resize(int length, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory)
		{
			(ref this).ListData<T>().Resize(length, options);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00020832 File Offset: 0x0001EA32
		public void SetCapacity(int capacity)
		{
			(ref this).ListData<T>().SetCapacity(capacity);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00020840 File Offset: 0x0001EA40
		public void TrimExcess()
		{
			(ref this).ListData<T>().TrimExcess();
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00020850 File Offset: 0x0001EA50
		public unsafe int IndexOf(void* ptr)
		{
			for (int i = 0; i < this.Length; i++)
			{
				if (*(IntPtr*)(this.Ptr + (IntPtr)i * (IntPtr)sizeof(T*) / (IntPtr)sizeof(T*)) == ptr)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00020885 File Offset: 0x0001EA85
		public unsafe bool Contains(void* ptr)
		{
			return this.IndexOf(ptr) != -1;
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00020894 File Offset: 0x0001EA94
		public unsafe void AddNoResize(void* value)
		{
			(ref this).ListData<T>().AddNoResize((IntPtr)value);
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x000208A7 File Offset: 0x0001EAA7
		public unsafe void AddRangeNoResize(void** ptr, int count)
		{
			(ref this).ListData<T>().AddRangeNoResize((void*)ptr, count);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x000208B6 File Offset: 0x0001EAB6
		public unsafe void AddRangeNoResize(UnsafePtrList<T> list)
		{
			(ref this).ListData<T>().AddRangeNoResize((void*)list.Ptr, list.Length);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x000208D0 File Offset: 0x0001EAD0
		public void Add(in IntPtr value)
		{
			(ref this).ListData<T>().Add(in value);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x000208E0 File Offset: 0x0001EAE0
		public unsafe void Add(void* value)
		{
			ref UnsafeList<IntPtr> ptr = ref (ref this).ListData<T>();
			IntPtr intPtr = (IntPtr)value;
			ptr.Add(in intPtr);
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00020901 File Offset: 0x0001EB01
		public unsafe void AddRange(void* ptr, int length)
		{
			(ref this).ListData<T>().AddRange(ptr, length);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00020910 File Offset: 0x0001EB10
		public unsafe void AddRange(UnsafePtrList<T> list)
		{
			(ref this).ListData<T>().AddRange(*(ref list).ListData<T>());
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00020929 File Offset: 0x0001EB29
		public void InsertRangeWithBeginEnd(int begin, int end)
		{
			(ref this).ListData<T>().InsertRangeWithBeginEnd(begin, end);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00020938 File Offset: 0x0001EB38
		public void RemoveAtSwapBack(int index)
		{
			(ref this).ListData<T>().RemoveAtSwapBack(index);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00020946 File Offset: 0x0001EB46
		public void RemoveRangeSwapBack(int index, int count)
		{
			(ref this).ListData<T>().RemoveRangeSwapBack(index, count);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00020955 File Offset: 0x0001EB55
		[Obsolete("RemoveRangeSwapBackWithBeginEnd(begin, end) is deprecated, use RemoveRangeSwapBack(index, count) instead. (RemovedAfter 2021-06-02)", false)]
		public void RemoveRangeSwapBackWithBeginEnd(int begin, int end)
		{
			(ref this).ListData<T>().RemoveRangeSwapBackWithBeginEnd(begin, end);
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00020964 File Offset: 0x0001EB64
		public void RemoveAt(int index)
		{
			(ref this).ListData<T>().RemoveAt(index);
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00020972 File Offset: 0x0001EB72
		public void RemoveRange(int index, int count)
		{
			(ref this).ListData<T>().RemoveRange(index, count);
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00020981 File Offset: 0x0001EB81
		[Obsolete("RemoveRangeWithBeginEnd(begin, end) is deprecated, use RemoveRange(index, count) instead. (RemovedAfter 2021-06-02)", false)]
		public void RemoveRangeWithBeginEnd(int begin, int end)
		{
			(ref this).ListData<T>().RemoveRangeWithBeginEnd(begin, end);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00004A65 File Offset: 0x00002C65
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00004A65 File Offset: 0x00002C65
		IEnumerator<IntPtr> IEnumerable<IntPtr>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00020990 File Offset: 0x0001EB90
		public UnsafePtrList<T>.ParallelReader AsParallelReader()
		{
			return new UnsafePtrList<T>.ParallelReader(this.Ptr, this.Length);
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x000209A3 File Offset: 0x0001EBA3
		public unsafe UnsafePtrList<T>.ParallelWriter AsParallelWriter()
		{
			return new UnsafePtrList<T>.ParallelWriter(this.Ptr, (UnsafeList<IntPtr>*)UnsafeUtility.AddressOf<UnsafePtrList<T>>(ref this));
		}

		// Token: 0x0400038D RID: 909
		[NativeDisableUnsafePtrRestriction]
		public unsafe readonly T** Ptr;

		// Token: 0x0400038E RID: 910
		public readonly int m_length;

		// Token: 0x0400038F RID: 911
		public readonly int m_capacity;

		// Token: 0x04000390 RID: 912
		public readonly AllocatorManager.AllocatorHandle Allocator;

		// Token: 0x04000391 RID: 913
		[Obsolete("Use Length property (UnityUpgradable) -> Length", true)]
		public int length;

		// Token: 0x04000392 RID: 914
		[Obsolete("Use Capacity property (UnityUpgradable) -> Capacity", true)]
		public int capacity;

		// Token: 0x0200012A RID: 298
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		public struct ParallelReader
		{
			// Token: 0x06000B0A RID: 2826 RVA: 0x000209B6 File Offset: 0x0001EBB6
			internal unsafe ParallelReader(T** ptr, int length)
			{
				this.Ptr = ptr;
				this.Length = length;
			}

			// Token: 0x06000B0B RID: 2827 RVA: 0x000209C8 File Offset: 0x0001EBC8
			public unsafe int IndexOf(void* ptr)
			{
				for (int i = 0; i < this.Length; i++)
				{
					if (*(IntPtr*)(this.Ptr + (IntPtr)i * (IntPtr)sizeof(T*) / (IntPtr)sizeof(T*)) == ptr)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06000B0C RID: 2828 RVA: 0x000209FD File Offset: 0x0001EBFD
			public unsafe bool Contains(void* ptr)
			{
				return this.IndexOf(ptr) != -1;
			}

			// Token: 0x04000393 RID: 915
			[NativeDisableUnsafePtrRestriction]
			public unsafe readonly T** Ptr;

			// Token: 0x04000394 RID: 916
			public readonly int Length;
		}

		// Token: 0x0200012B RID: 299
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		public struct ParallelWriter
		{
			// Token: 0x06000B0D RID: 2829 RVA: 0x00020A0C File Offset: 0x0001EC0C
			internal unsafe ParallelWriter(T** ptr, UnsafeList<IntPtr>* listData)
			{
				this.Ptr = ptr;
				this.ListData = listData;
			}

			// Token: 0x06000B0E RID: 2830 RVA: 0x00020A1C File Offset: 0x0001EC1C
			public unsafe void AddNoResize(T* value)
			{
				this.ListData->AddNoResize((IntPtr)((void*)value));
			}

			// Token: 0x06000B0F RID: 2831 RVA: 0x00020A2F File Offset: 0x0001EC2F
			public unsafe void AddRangeNoResize(T** ptr, int count)
			{
				this.ListData->AddRangeNoResize((void*)ptr, count);
			}

			// Token: 0x06000B10 RID: 2832 RVA: 0x00020A3E File Offset: 0x0001EC3E
			public unsafe void AddRangeNoResize(UnsafePtrList<T> list)
			{
				this.ListData->AddRangeNoResize((void*)list.Ptr, list.Length);
			}

			// Token: 0x04000395 RID: 917
			[NativeDisableUnsafePtrRestriction]
			public unsafe readonly T** Ptr;

			// Token: 0x04000396 RID: 918
			[NativeDisableUnsafePtrRestriction]
			public unsafe UnsafeList<IntPtr>* ListData;
		}
	}
}
