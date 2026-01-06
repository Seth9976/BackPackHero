using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe.NotBurstCompatible;
using Unity.Jobs;
using Unity.Mathematics;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200011B RID: 283
	[BurstCompatible]
	public struct UnsafeAppendBuffer : INativeDisposable, IDisposable
	{
		// Token: 0x06000A58 RID: 2648 RVA: 0x0001E982 File Offset: 0x0001CB82
		public UnsafeAppendBuffer(int initialCapacity, int alignment, AllocatorManager.AllocatorHandle allocator)
		{
			this.Alignment = alignment;
			this.Allocator = allocator;
			this.Ptr = null;
			this.Length = 0;
			this.Capacity = 0;
			this.SetCapacity(initialCapacity);
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0001E9AF File Offset: 0x0001CBAF
		public unsafe UnsafeAppendBuffer(void* ptr, int length)
		{
			this.Alignment = 0;
			this.Allocator = AllocatorManager.None;
			this.Ptr = (byte*)ptr;
			this.Length = 0;
			this.Capacity = length;
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x0001E9D8 File Offset: 0x0001CBD8
		public bool IsEmpty
		{
			get
			{
				return this.Length == 0;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0001E9E3 File Offset: 0x0001CBE3
		public bool IsCreated
		{
			get
			{
				return this.Ptr != null;
			}
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0001E9F4 File Offset: 0x0001CBF4
		public void Dispose()
		{
			if (CollectionHelper.ShouldDeallocate(this.Allocator))
			{
				Memory.Unmanaged.Free<byte>(this.Ptr, this.Allocator);
				this.Allocator = AllocatorManager.Invalid;
			}
			this.Ptr = null;
			this.Length = 0;
			this.Capacity = 0;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0001EA40 File Offset: 0x0001CC40
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

		// Token: 0x06000A5E RID: 2654 RVA: 0x0001EAA0 File Offset: 0x0001CCA0
		public void Reset()
		{
			this.Length = 0;
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0001EAAC File Offset: 0x0001CCAC
		public unsafe void SetCapacity(int capacity)
		{
			if (capacity <= this.Capacity)
			{
				return;
			}
			capacity = math.max(64, math.ceilpow2(capacity));
			byte* ptr = (byte*)Memory.Unmanaged.Allocate((long)capacity, this.Alignment, this.Allocator);
			if (this.Ptr != null)
			{
				UnsafeUtility.MemCpy((void*)ptr, (void*)this.Ptr, (long)this.Length);
				Memory.Unmanaged.Free<byte>(this.Ptr, this.Allocator);
			}
			this.Ptr = ptr;
			this.Capacity = capacity;
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0001EB22 File Offset: 0x0001CD22
		public void ResizeUninitialized(int length)
		{
			this.SetCapacity(length);
			this.Length = length;
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0001EB34 File Offset: 0x0001CD34
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe void Add<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(T value) where T : struct, ValueType
		{
			int num = UnsafeUtility.SizeOf<T>();
			this.SetCapacity(this.Length + num);
			void* ptr = (void*)(this.Ptr + this.Length);
			if (CollectionHelper.IsAligned(ptr, UnsafeUtility.AlignOf<T>()))
			{
				UnsafeUtility.CopyStructureToPtr<T>(ref value, ptr);
			}
			else
			{
				UnsafeUtility.MemCpy(ptr, (void*)(&value), (long)num);
			}
			this.Length += num;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0001EB94 File Offset: 0x0001CD94
		public unsafe void Add(void* ptr, int structSize)
		{
			this.SetCapacity(this.Length + structSize);
			UnsafeUtility.MemCpy((void*)(this.Ptr + this.Length), ptr, (long)structSize);
			this.Length += structSize;
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0001EBC7 File Offset: 0x0001CDC7
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe void AddArray<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(void* ptr, int length) where T : struct, ValueType
		{
			this.Add<int>(length);
			if (length != 0)
			{
				this.Add(ptr, length * UnsafeUtility.SizeOf<T>());
			}
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0001EBE1 File Offset: 0x0001CDE1
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		public void Add<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(NativeArray<T> value) where T : struct, ValueType
		{
			this.Add<int>(value.Length);
			this.Add(value.GetUnsafeReadOnlyPtr<T>(), UnsafeUtility.SizeOf<T>() * value.Length);
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0001EC09 File Offset: 0x0001CE09
		[NotBurstCompatible]
		[Obsolete("Please use `AddNBC` from `Unity.Collections.LowLevel.Unsafe.NotBurstCompatible` namespace instead. (RemovedAfter 2021-06-22)", false)]
		public void Add(string value)
		{
			(ref this).AddNBC(value);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0001EC14 File Offset: 0x0001CE14
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe T Pop<[global::System.Runtime.CompilerServices.IsUnmanaged] T>() where T : struct, ValueType
		{
			int num = UnsafeUtility.SizeOf<T>();
			long num2 = this.Ptr;
			long num3 = (long)this.Length;
			long num4 = num2 + num3 - (long)num;
			T t;
			if (CollectionHelper.IsAligned((ulong)num4, UnsafeUtility.AlignOf<T>()))
			{
				t = UnsafeUtility.ReadArrayElement<T>(num4, 0);
			}
			else
			{
				UnsafeUtility.MemCpy((void*)(&t), num4, (long)num);
			}
			this.Length -= num;
			return t;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0001EC70 File Offset: 0x0001CE70
		public unsafe void Pop(void* ptr, int structSize)
		{
			long num = this.Ptr;
			long num2 = (long)this.Length;
			long num3 = num + num2 - (long)structSize;
			UnsafeUtility.MemCpy(ptr, num3, (long)structSize);
			this.Length -= structSize;
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0001ECAA File Offset: 0x0001CEAA
		[NotBurstCompatible]
		[Obsolete("Please use `ToBytesNBC` from `Unity.Collections.LowLevel.Unsafe.NotBurstCompatible` namespace instead. (RemovedAfter 2021-06-22)", false)]
		public byte[] ToBytes()
		{
			return (ref this).ToBytesNBC();
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0001ECB2 File Offset: 0x0001CEB2
		public UnsafeAppendBuffer.Reader AsReader()
		{
			return new UnsafeAppendBuffer.Reader(ref this);
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0001ECBC File Offset: 0x0001CEBC
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckAlignment(int alignment)
		{
			int num = ((alignment == 0) ? 1 : 0);
			bool flag = ((alignment - 1) & alignment) == 0;
			if (num != 0 || !flag)
			{
				throw new ArgumentException(string.Format("Specified alignment must be non-zero positive power of two. Requested: {0}", alignment));
			}
		}

		// Token: 0x0400036A RID: 874
		[NativeDisableUnsafePtrRestriction]
		public unsafe byte* Ptr;

		// Token: 0x0400036B RID: 875
		public int Length;

		// Token: 0x0400036C RID: 876
		public int Capacity;

		// Token: 0x0400036D RID: 877
		public AllocatorManager.AllocatorHandle Allocator;

		// Token: 0x0400036E RID: 878
		public readonly int Alignment;

		// Token: 0x0200011C RID: 284
		[BurstCompatible]
		public struct Reader
		{
			// Token: 0x06000A6B RID: 2667 RVA: 0x0001ECF3 File Offset: 0x0001CEF3
			public Reader(ref UnsafeAppendBuffer buffer)
			{
				this.Ptr = buffer.Ptr;
				this.Size = buffer.Length;
				this.Offset = 0;
			}

			// Token: 0x06000A6C RID: 2668 RVA: 0x0001ED14 File Offset: 0x0001CF14
			public unsafe Reader(void* ptr, int length)
			{
				this.Ptr = (byte*)ptr;
				this.Size = length;
				this.Offset = 0;
			}

			// Token: 0x17000124 RID: 292
			// (get) Token: 0x06000A6D RID: 2669 RVA: 0x0001ED2B File Offset: 0x0001CF2B
			public bool EndOfBuffer
			{
				get
				{
					return this.Offset == this.Size;
				}
			}

			// Token: 0x06000A6E RID: 2670 RVA: 0x0001ED3C File Offset: 0x0001CF3C
			[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
			public unsafe void ReadNext<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(out T value) where T : struct, ValueType
			{
				int num = UnsafeUtility.SizeOf<T>();
				void* ptr = (void*)(this.Ptr + this.Offset);
				if (CollectionHelper.IsAligned(ptr, UnsafeUtility.AlignOf<T>()))
				{
					UnsafeUtility.CopyPtrToStructure<T>(ptr, out value);
				}
				else
				{
					fixed (T* ptr2 = &value)
					{
						void* ptr3 = (void*)ptr2;
						UnsafeUtility.MemCpy(ptr3, ptr, (long)num);
					}
				}
				this.Offset += num;
			}

			// Token: 0x06000A6F RID: 2671 RVA: 0x0001ED94 File Offset: 0x0001CF94
			[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
			public unsafe T ReadNext<[global::System.Runtime.CompilerServices.IsUnmanaged] T>() where T : struct, ValueType
			{
				int num = UnsafeUtility.SizeOf<T>();
				void* ptr = (void*)(this.Ptr + this.Offset);
				T t;
				if (CollectionHelper.IsAligned(ptr, UnsafeUtility.AlignOf<T>()))
				{
					t = UnsafeUtility.ReadArrayElement<T>(ptr, 0);
				}
				else
				{
					UnsafeUtility.MemCpy((void*)(&t), ptr, (long)num);
				}
				this.Offset += num;
				return t;
			}

			// Token: 0x06000A70 RID: 2672 RVA: 0x0001EDE7 File Offset: 0x0001CFE7
			public unsafe void* ReadNext(int structSize)
			{
				void* ptr = (void*)((IntPtr)((void*)this.Ptr) + this.Offset);
				this.Offset += structSize;
				return ptr;
			}

			// Token: 0x06000A71 RID: 2673 RVA: 0x0001EE14 File Offset: 0x0001D014
			[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
			public unsafe void ReadNext<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(out NativeArray<T> value, AllocatorManager.AllocatorHandle allocator) where T : struct, ValueType
			{
				int num = this.ReadNext<int>();
				value = CollectionHelper.CreateNativeArray<T>(num, allocator, NativeArrayOptions.ClearMemory);
				int num2 = num * UnsafeUtility.SizeOf<T>();
				if (num2 > 0)
				{
					void* ptr = this.ReadNext(num2);
					UnsafeUtility.MemCpy(value.GetUnsafePtr<T>(), ptr, (long)num2);
				}
			}

			// Token: 0x06000A72 RID: 2674 RVA: 0x0001EE5D File Offset: 0x0001D05D
			[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
			public unsafe void* ReadNextArray<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(out int length) where T : struct, ValueType
			{
				length = this.ReadNext<int>();
				if (length != 0)
				{
					return this.ReadNext(length * UnsafeUtility.SizeOf<T>());
				}
				return null;
			}

			// Token: 0x06000A73 RID: 2675 RVA: 0x0001EE7C File Offset: 0x0001D07C
			[NotBurstCompatible]
			[Obsolete("Please use `ReadNextNBC` from `Unity.Collections.LowLevel.Unsafe.NotBurstCompatible` namespace instead. (RemovedAfter 2021-06-22)", false)]
			public void ReadNext(out string value)
			{
				(ref this).ReadNextNBC(out value);
			}

			// Token: 0x06000A74 RID: 2676 RVA: 0x0001EE85 File Offset: 0x0001D085
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private void CheckBounds(int structSize)
			{
				if (this.Offset + structSize > this.Size)
				{
					throw new ArgumentException(string.Format("Requested value outside bounds of UnsafeAppendOnlyBuffer. Remaining bytes: {0} Requested: {1}", this.Size - this.Offset, structSize));
				}
			}

			// Token: 0x0400036F RID: 879
			public unsafe readonly byte* Ptr;

			// Token: 0x04000370 RID: 880
			public readonly int Size;

			// Token: 0x04000371 RID: 881
			public int Offset;
		}
	}
}
