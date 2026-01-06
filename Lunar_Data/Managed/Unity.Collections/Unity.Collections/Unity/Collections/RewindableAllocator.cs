using System;
using System.Runtime.CompilerServices;
using System.Threading;
using AOT;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Unity.Collections
{
	// Token: 0x020000E6 RID: 230
	[BurstCompile]
	public struct RewindableAllocator : AllocatorManager.IAllocator, IDisposable
	{
		// Token: 0x060008BE RID: 2238 RVA: 0x00019E98 File Offset: 0x00018098
		public unsafe void Initialize(int initialSizeInBytes, bool enableBlockFree = false)
		{
			this.m_spinner = default(Spinner);
			this.m_block = new UnmanagedArray<RewindableAllocator.MemoryBlock>(64, Allocator.Persistent);
			*this.m_block[0] = new RewindableAllocator.MemoryBlock((long)initialSizeInBytes);
			this.m_last = (this.m_used = (this.m_best = 0));
			this.m_enableBlockFree = enableBlockFree;
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x00019EFC File Offset: 0x000180FC
		// (set) Token: 0x060008C0 RID: 2240 RVA: 0x00019F04 File Offset: 0x00018104
		public bool EnableBlockFree
		{
			get
			{
				return this.m_enableBlockFree;
			}
			set
			{
				this.m_enableBlockFree = value;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x00019F0D File Offset: 0x0001810D
		public int BlocksAllocated
		{
			get
			{
				return this.m_last + 1;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x00019F17 File Offset: 0x00018117
		public int InitialSizeInBytes
		{
			get
			{
				return (int)this.m_block[0].m_bytes;
			}
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00019F2C File Offset: 0x0001812C
		public void Rewind()
		{
			if (JobsUtility.IsExecutingJob)
			{
				throw new InvalidOperationException("You cannot Rewind a RewindableAllocator from a Job.");
			}
			this.m_handle.Rewind();
			while (this.m_last > this.m_used)
			{
				int num = this.m_last;
				this.m_last = num - 1;
				this.m_block[num].Dispose();
			}
			while (this.m_used > 0)
			{
				int num = this.m_used;
				this.m_used = num - 1;
				this.m_block[num].Rewind();
			}
			this.m_block[0].Rewind();
			this.m_best = 0;
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00019FCC File Offset: 0x000181CC
		public void Dispose()
		{
			if (JobsUtility.IsExecutingJob)
			{
				throw new InvalidOperationException("You cannot Dispose a RewindableAllocator from a Job.");
			}
			this.m_used = 0;
			this.Rewind();
			this.m_block[0].Dispose();
			this.m_block.Dispose();
			this.m_last = (this.m_used = (this.m_best = 0));
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x0001A02D File Offset: 0x0001822D
		[NotBurstCompatible]
		public AllocatorManager.TryFunction Function
		{
			get
			{
				return new AllocatorManager.TryFunction(RewindableAllocator.Try);
			}
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0001A03C File Offset: 0x0001823C
		public unsafe int Try(ref AllocatorManager.Block block)
		{
			if (block.Range.Pointer == IntPtr.Zero)
			{
				int num = this.m_block[this.m_best].TryAllocate(ref block);
				if (num == 0)
				{
					return num;
				}
				this.m_spinner.Lock();
				int i;
				for (i = 0; i <= this.m_last; i++)
				{
					num = this.m_block[i].TryAllocate(ref block);
					if (num == 0)
					{
						this.m_used = ((i > this.m_used) ? i : this.m_used);
						this.m_best = i;
						this.m_spinner.Unlock();
						return num;
					}
				}
				long num2 = math.max(this.m_block[0].m_bytes << i, math.ceilpow2(block.Bytes));
				*this.m_block[i] = new RewindableAllocator.MemoryBlock(num2);
				num = this.m_block[i].TryAllocate(ref block);
				this.m_best = i;
				this.m_used = i;
				this.m_last = i;
				this.m_spinner.Unlock();
				return num;
			}
			else
			{
				if (block.Range.Items == 0)
				{
					if (this.m_enableBlockFree)
					{
						this.m_spinner.Lock();
						if (this.m_block[this.m_best].Contains(block.Range.Pointer) && Interlocked.Decrement(ref this.m_block[this.m_best].m_allocations) == 0L)
						{
							this.m_block[this.m_best].Rewind();
						}
						this.m_spinner.Unlock();
					}
					return 0;
				}
				return -1;
			}
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0001A1D5 File Offset: 0x000183D5
		[BurstCompile]
		[MonoPInvokeCallback(typeof(AllocatorManager.TryFunction))]
		internal static int Try(IntPtr state, ref AllocatorManager.Block block)
		{
			return RewindableAllocator.Try_00000756$BurstDirectCall.Invoke(state, ref block);
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x0001A1DE File Offset: 0x000183DE
		// (set) Token: 0x060008C9 RID: 2249 RVA: 0x0001A1E6 File Offset: 0x000183E6
		public AllocatorManager.AllocatorHandle Handle
		{
			get
			{
				return this.m_handle;
			}
			set
			{
				this.m_handle = value;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x0001A1EF File Offset: 0x000183EF
		public Allocator ToAllocator
		{
			get
			{
				return this.m_handle.ToAllocator;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x0001A1FC File Offset: 0x000183FC
		public bool IsCustomAllocator
		{
			get
			{
				return this.m_handle.IsCustomAllocator;
			}
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0001A20C File Offset: 0x0001840C
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		public NativeArray<T> AllocateNativeArray<T>(int length) where T : struct
		{
			return new NativeArray<T>
			{
				m_Buffer = (ref this).AllocateStruct(default(T), length),
				m_Length = length,
				m_AllocatorLabel = Allocator.None
			};
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0001A24C File Offset: 0x0001844C
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe NativeList<T> AllocateNativeList<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(int capacity) where T : struct, ValueType
		{
			NativeList<T> nativeList = default(NativeList<T>);
			nativeList.m_ListData = (ref this).Allocate(default(UnsafeList<T>), 1);
			nativeList.m_ListData->Ptr = (ref this).Allocate(default(T), capacity);
			nativeList.m_ListData->m_capacity = capacity;
			nativeList.m_ListData->m_length = 0;
			nativeList.m_ListData->Allocator = Allocator.None;
			nativeList.m_DeprecatedAllocator = Allocator.None;
			return nativeList;
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0001A2CA File Offset: 0x000184CA
		[BurstCompile]
		[MonoPInvokeCallback(typeof(AllocatorManager.TryFunction))]
		[MethodImpl(256)]
		public unsafe static int Try$BurstManaged(IntPtr state, ref AllocatorManager.Block block)
		{
			return ((RewindableAllocator*)(void*)state)->Try(ref block);
		}

		// Token: 0x040002DC RID: 732
		private Spinner m_spinner;

		// Token: 0x040002DD RID: 733
		private AllocatorManager.AllocatorHandle m_handle;

		// Token: 0x040002DE RID: 734
		private UnmanagedArray<RewindableAllocator.MemoryBlock> m_block;

		// Token: 0x040002DF RID: 735
		private int m_best;

		// Token: 0x040002E0 RID: 736
		private int m_last;

		// Token: 0x040002E1 RID: 737
		private int m_used;

		// Token: 0x040002E2 RID: 738
		private bool m_enableBlockFree;

		// Token: 0x020000E7 RID: 231
		[BurstCompatible]
		internal struct MemoryBlock : IDisposable
		{
			// Token: 0x060008CF RID: 2255 RVA: 0x0001A2D8 File Offset: 0x000184D8
			public unsafe MemoryBlock(long bytes)
			{
				this.m_pointer = (byte*)Memory.Unmanaged.Allocate(bytes, 16384, Allocator.Persistent);
				this.m_bytes = bytes;
				this.m_current = 0L;
				this.m_allocations = 0L;
			}

			// Token: 0x060008D0 RID: 2256 RVA: 0x0001A308 File Offset: 0x00018508
			public void Rewind()
			{
				this.m_current = 0L;
				this.m_allocations = 0L;
			}

			// Token: 0x060008D1 RID: 2257 RVA: 0x0001A31A File Offset: 0x0001851A
			public void Dispose()
			{
				Memory.Unmanaged.Free<byte>(this.m_pointer, Allocator.Persistent);
				this.m_pointer = null;
				this.m_bytes = 0L;
				this.m_current = 0L;
				this.m_allocations = 0L;
			}

			// Token: 0x060008D2 RID: 2258 RVA: 0x0001A350 File Offset: 0x00018550
			public unsafe int TryAllocate(ref AllocatorManager.Block block)
			{
				int num = math.max(64, block.Alignment);
				int num2 = ((num != 64) ? 1 : 0);
				int num3 = 63;
				if (num2 == 1)
				{
					num = (num + num3) & ~num3;
				}
				long num4 = (long)num - 1L;
				long num5 = (block.Bytes + (long)(num2 * num) + num4) & ~num4;
				long num6 = Interlocked.Read(ref this.m_current);
				long num8;
				for (;;)
				{
					long num7 = num6 + num5;
					num8 = (num6 + num4) & ~num4;
					if (num8 + block.Bytes > this.m_bytes)
					{
						break;
					}
					long num9 = num6;
					num6 = Interlocked.CompareExchange(ref this.m_current, num7, num9);
					if (num6 == num9)
					{
						goto Block_4;
					}
				}
				return -1;
				Block_4:
				block.Range.Pointer = (IntPtr)((void*)(this.m_pointer + num8));
				block.AllocatedItems = block.Range.Items;
				Interlocked.Increment(ref this.m_allocations);
				return 0;
			}

			// Token: 0x060008D3 RID: 2259 RVA: 0x0001A420 File Offset: 0x00018620
			public unsafe bool Contains(IntPtr ptr)
			{
				void* ptr2 = (void*)ptr;
				return ptr2 >= (void*)this.m_pointer && ptr2 < (void*)(this.m_pointer + this.m_current);
			}

			// Token: 0x040002E3 RID: 739
			public const int kMaximumAlignment = 16384;

			// Token: 0x040002E4 RID: 740
			public unsafe byte* m_pointer;

			// Token: 0x040002E5 RID: 741
			public long m_bytes;

			// Token: 0x040002E6 RID: 742
			public long m_current;

			// Token: 0x040002E7 RID: 743
			public long m_allocations;
		}

		// Token: 0x020000E8 RID: 232
		// (Invoke) Token: 0x060008D5 RID: 2261
		public delegate int Try_00000756$PostfixBurstDelegate(IntPtr state, ref AllocatorManager.Block block);

		// Token: 0x020000E9 RID: 233
		internal static class Try_00000756$BurstDirectCall
		{
			// Token: 0x060008D8 RID: 2264 RVA: 0x0001A450 File Offset: 0x00018650
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (RewindableAllocator.Try_00000756$BurstDirectCall.Pointer == 0)
				{
					RewindableAllocator.Try_00000756$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(RewindableAllocator.Try_00000756$BurstDirectCall.DeferredCompilation, methodof(RewindableAllocator.Try$BurstManaged(IntPtr, ref AllocatorManager.Block)).MethodHandle, typeof(RewindableAllocator.Try_00000756$PostfixBurstDelegate).TypeHandle);
				}
				A_0 = RewindableAllocator.Try_00000756$BurstDirectCall.Pointer;
			}

			// Token: 0x060008D9 RID: 2265 RVA: 0x0001A47C File Offset: 0x0001867C
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				RewindableAllocator.Try_00000756$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x060008DA RID: 2266 RVA: 0x0001A494 File Offset: 0x00018694
			public static void Constructor()
			{
				RewindableAllocator.Try_00000756$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(RewindableAllocator.Try(IntPtr, ref AllocatorManager.Block)).MethodHandle);
			}

			// Token: 0x060008DB RID: 2267 RVA: 0x00002C2B File Offset: 0x00000E2B
			public static void Initialize()
			{
			}

			// Token: 0x060008DC RID: 2268 RVA: 0x0001A4A5 File Offset: 0x000186A5
			// Note: this type is marked as 'beforefieldinit'.
			static Try_00000756$BurstDirectCall()
			{
				RewindableAllocator.Try_00000756$BurstDirectCall.Constructor();
			}

			// Token: 0x060008DD RID: 2269 RVA: 0x0001A4AC File Offset: 0x000186AC
			public static int Invoke(IntPtr state, ref AllocatorManager.Block block)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = RewindableAllocator.Try_00000756$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						return calli(System.Int32(System.IntPtr,Unity.Collections.AllocatorManager/Block&), state, ref block, functionPointer);
					}
				}
				return RewindableAllocator.Try$BurstManaged(state, ref block);
			}

			// Token: 0x040002E8 RID: 744
			private static IntPtr Pointer;

			// Token: 0x040002E9 RID: 745
			private static IntPtr DeferredCompilation;
		}
	}
}
