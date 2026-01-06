using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Jobs;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000147 RID: 327
	[DebuggerDisplay("Length = {Length}, Capacity = {Capacity}, IsCreated = {IsCreated}, IsEmpty = {IsEmpty}")]
	[DebuggerTypeProxy(typeof(UnsafeRingQueueDebugView<>))]
	[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
	public struct UnsafeRingQueue<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : INativeDisposable, IDisposable where T : struct, ValueType
	{
		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x000234EB File Offset: 0x000216EB
		public bool IsEmpty
		{
			get
			{
				return !this.IsCreated || this.Length == 0;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x00023500 File Offset: 0x00021700
		public int Length
		{
			get
			{
				return this.Control.Length;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x0002350D File Offset: 0x0002170D
		public int Capacity
		{
			get
			{
				return this.Control.Capacity;
			}
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0002351A File Offset: 0x0002171A
		public unsafe UnsafeRingQueue(T* ptr, int capacity)
		{
			this.Ptr = ptr;
			this.Allocator = AllocatorManager.None;
			this.Control = new RingControl(capacity);
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0002353C File Offset: 0x0002173C
		public unsafe UnsafeRingQueue(int capacity, AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options = NativeArrayOptions.ClearMemory)
		{
			capacity++;
			this.Allocator = allocator;
			this.Control = new RingControl(capacity);
			int num = capacity * UnsafeUtility.SizeOf<T>();
			this.Ptr = (T*)Memory.Unmanaged.Allocate((long)num, 16, allocator);
			if (options == NativeArrayOptions.ClearMemory)
			{
				UnsafeUtility.MemClear((void*)this.Ptr, (long)num);
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x0002358A File Offset: 0x0002178A
		public bool IsCreated
		{
			get
			{
				return this.Ptr != null;
			}
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00023599 File Offset: 0x00021799
		public void Dispose()
		{
			if (CollectionHelper.ShouldDeallocate(this.Allocator))
			{
				Memory.Unmanaged.Free<T>(this.Ptr, this.Allocator);
				this.Allocator = AllocatorManager.Invalid;
			}
			this.Ptr = null;
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x000235CC File Offset: 0x000217CC
		[NotBurstCompatible]
		public unsafe JobHandle Dispose(JobHandle inputDeps)
		{
			if (CollectionHelper.ShouldDeallocate(this.Allocator))
			{
				JobHandle jobHandle = new UnsafeDisposeJob
				{
					Ptr = (void*)this.Ptr,
					Allocator = this.Allocator
				}.Schedule(inputDeps);
				this.Ptr = null;
				this.Allocator = AllocatorManager.Invalid;
				return jobHandle;
			}
			this.Ptr = null;
			return inputDeps;
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0002362C File Offset: 0x0002182C
		public unsafe bool TryEnqueue(T value)
		{
			if (1 != this.Control.Reserve(1))
			{
				return false;
			}
			this.Ptr[(IntPtr)this.Control.Current * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = value;
			this.Control.Commit(1);
			return true;
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x00023678 File Offset: 0x00021878
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void ThrowQueueFull()
		{
			throw new InvalidOperationException("Trying to enqueue into full queue.");
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x00023684 File Offset: 0x00021884
		public void Enqueue(T value)
		{
			this.TryEnqueue(value);
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0002368E File Offset: 0x0002188E
		public unsafe bool TryDequeue(out T item)
		{
			item = this.Ptr[(IntPtr)this.Control.Read * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
			return 1 == this.Control.Consume(1);
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x000236C4 File Offset: 0x000218C4
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void ThrowQueueEmpty()
		{
			throw new InvalidOperationException("Trying to dequeue from an empty queue");
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x000236D0 File Offset: 0x000218D0
		public T Dequeue()
		{
			T t;
			this.TryDequeue(out t);
			return t;
		}

		// Token: 0x040003CE RID: 974
		[NativeDisableUnsafePtrRestriction]
		public unsafe T* Ptr;

		// Token: 0x040003CF RID: 975
		public AllocatorManager.AllocatorHandle Allocator;

		// Token: 0x040003D0 RID: 976
		internal RingControl Control;
	}
}
