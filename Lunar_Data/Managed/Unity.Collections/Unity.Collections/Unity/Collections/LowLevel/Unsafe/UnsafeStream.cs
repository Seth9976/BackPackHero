using System;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Jobs;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200014E RID: 334
	[BurstCompatible]
	public struct UnsafeStream : INativeDisposable, IDisposable
	{
		// Token: 0x06000BE4 RID: 3044 RVA: 0x00023895 File Offset: 0x00021A95
		public UnsafeStream(int bufferCount, AllocatorManager.AllocatorHandle allocator)
		{
			UnsafeStream.AllocateBlock(out this, allocator);
			this.AllocateForEach(bufferCount);
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x000238A8 File Offset: 0x00021AA8
		[NotBurstCompatible]
		public unsafe static JobHandle ScheduleConstruct<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(out UnsafeStream stream, NativeList<T> bufferCount, JobHandle dependency, AllocatorManager.AllocatorHandle allocator) where T : struct, ValueType
		{
			UnsafeStream.AllocateBlock(out stream, allocator);
			return new UnsafeStream.ConstructJobList
			{
				List = (UntypedUnsafeList*)bufferCount.GetUnsafeList(),
				Container = stream
			}.Schedule(dependency);
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x000238E8 File Offset: 0x00021AE8
		[NotBurstCompatible]
		public static JobHandle ScheduleConstruct(out UnsafeStream stream, NativeArray<int> bufferCount, JobHandle dependency, AllocatorManager.AllocatorHandle allocator)
		{
			UnsafeStream.AllocateBlock(out stream, allocator);
			return new UnsafeStream.ConstructJob
			{
				Length = bufferCount,
				Container = stream
			}.Schedule(dependency);
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00023920 File Offset: 0x00021B20
		internal unsafe static void AllocateBlock(out UnsafeStream stream, AllocatorManager.AllocatorHandle allocator)
		{
			int num = 128;
			int num2 = sizeof(UnsafeStreamBlockData) + sizeof(UnsafeStreamBlock*) * num;
			byte* ptr = (byte*)Memory.Unmanaged.Allocate((long)num2, 16, allocator);
			UnsafeUtility.MemClear((void*)ptr, (long)num2);
			UnsafeStreamBlockData* ptr2 = (UnsafeStreamBlockData*)ptr;
			stream.m_Block = ptr2;
			stream.m_Allocator = allocator;
			ptr2->Allocator = allocator;
			ptr2->BlockCount = num;
			ptr2->Blocks = (UnsafeStreamBlock**)(ptr + sizeof(UnsafeStreamBlockData));
			ptr2->Ranges = null;
			ptr2->RangeCount = 0;
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00023994 File Offset: 0x00021B94
		internal unsafe void AllocateForEach(int forEachCount)
		{
			long num = (long)(sizeof(UnsafeStreamRange) * forEachCount);
			this.m_Block->Ranges = (UnsafeStreamRange*)Memory.Unmanaged.Allocate(num, 16, this.m_Allocator);
			this.m_Block->RangeCount = forEachCount;
			UnsafeUtility.MemClear((void*)this.m_Block->Ranges, num);
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x000239E4 File Offset: 0x00021BE4
		public unsafe bool IsEmpty()
		{
			if (!this.IsCreated)
			{
				return true;
			}
			for (int num = 0; num != this.m_Block->RangeCount; num++)
			{
				if (this.m_Block->Ranges[num].ElementCount > 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x00023A31 File Offset: 0x00021C31
		public bool IsCreated
		{
			get
			{
				return this.m_Block != null;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x00023A40 File Offset: 0x00021C40
		public unsafe int ForEachCount
		{
			get
			{
				return this.m_Block->RangeCount;
			}
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00023A4D File Offset: 0x00021C4D
		public UnsafeStream.Reader AsReader()
		{
			return new UnsafeStream.Reader(ref this);
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00023A55 File Offset: 0x00021C55
		public UnsafeStream.Writer AsWriter()
		{
			return new UnsafeStream.Writer(ref this);
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00023A60 File Offset: 0x00021C60
		public unsafe int Count()
		{
			int num = 0;
			for (int num2 = 0; num2 != this.m_Block->RangeCount; num2++)
			{
				num += this.m_Block->Ranges[num2].ElementCount;
			}
			return num;
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00023AA4 File Offset: 0x00021CA4
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe NativeArray<T> ToNativeArray<T>(AllocatorManager.AllocatorHandle allocator) where T : struct
		{
			NativeArray<T> nativeArray = CollectionHelper.CreateNativeArray<T>(this.Count(), allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeStream.Reader reader = this.AsReader();
			int num = 0;
			for (int num2 = 0; num2 != reader.ForEachCount; num2++)
			{
				reader.BeginForEachIndex(num2);
				int remainingItemCount = reader.RemainingItemCount;
				for (int i = 0; i < remainingItemCount; i++)
				{
					nativeArray[num] = *reader.Read<T>();
					num++;
				}
				reader.EndForEachIndex();
			}
			return nativeArray;
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00023B20 File Offset: 0x00021D20
		private unsafe void Deallocate()
		{
			if (this.m_Block == null)
			{
				return;
			}
			for (int num = 0; num != this.m_Block->BlockCount; num++)
			{
				UnsafeStreamBlock* next;
				for (UnsafeStreamBlock* ptr = *(IntPtr*)(this.m_Block->Blocks + (IntPtr)num * (IntPtr)sizeof(UnsafeStreamBlock*) / (IntPtr)sizeof(UnsafeStreamBlock*)); ptr != null; ptr = next)
				{
					next = ptr->Next;
					Memory.Unmanaged.Free<UnsafeStreamBlock>(ptr, this.m_Allocator);
				}
			}
			Memory.Unmanaged.Free<UnsafeStreamRange>(this.m_Block->Ranges, this.m_Allocator);
			Memory.Unmanaged.Free<UnsafeStreamBlockData>(this.m_Block, this.m_Allocator);
			this.m_Block = null;
			this.m_Allocator = Allocator.None;
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00023BBC File Offset: 0x00021DBC
		public void Dispose()
		{
			this.Deallocate();
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00023BC4 File Offset: 0x00021DC4
		[NotBurstCompatible]
		public JobHandle Dispose(JobHandle inputDeps)
		{
			JobHandle jobHandle = new UnsafeStream.DisposeJob
			{
				Container = this
			}.Schedule(inputDeps);
			this.m_Block = null;
			return jobHandle;
		}

		// Token: 0x040003E4 RID: 996
		[NativeDisableUnsafePtrRestriction]
		internal unsafe UnsafeStreamBlockData* m_Block;

		// Token: 0x040003E5 RID: 997
		internal AllocatorManager.AllocatorHandle m_Allocator;

		// Token: 0x0200014F RID: 335
		[BurstCompile]
		private struct DisposeJob : IJob
		{
			// Token: 0x06000BF3 RID: 3059 RVA: 0x00023BF5 File Offset: 0x00021DF5
			public void Execute()
			{
				this.Container.Deallocate();
			}

			// Token: 0x040003E6 RID: 998
			public UnsafeStream Container;
		}

		// Token: 0x02000150 RID: 336
		[BurstCompile]
		private struct ConstructJobList : IJob
		{
			// Token: 0x06000BF4 RID: 3060 RVA: 0x00023C02 File Offset: 0x00021E02
			public unsafe void Execute()
			{
				this.Container.AllocateForEach(this.List->m_length);
			}

			// Token: 0x040003E7 RID: 999
			public UnsafeStream Container;

			// Token: 0x040003E8 RID: 1000
			[ReadOnly]
			[NativeDisableUnsafePtrRestriction]
			public unsafe UntypedUnsafeList* List;
		}

		// Token: 0x02000151 RID: 337
		[BurstCompile]
		private struct ConstructJob : IJob
		{
			// Token: 0x06000BF5 RID: 3061 RVA: 0x00023C1A File Offset: 0x00021E1A
			public void Execute()
			{
				this.Container.AllocateForEach(this.Length[0]);
			}

			// Token: 0x040003E9 RID: 1001
			public UnsafeStream Container;

			// Token: 0x040003EA RID: 1002
			[ReadOnly]
			public NativeArray<int> Length;
		}

		// Token: 0x02000152 RID: 338
		[BurstCompatible]
		public struct Writer
		{
			// Token: 0x06000BF6 RID: 3062 RVA: 0x00023C34 File Offset: 0x00021E34
			internal Writer(ref UnsafeStream stream)
			{
				this.m_BlockStream = stream.m_Block;
				this.m_ForeachIndex = int.MinValue;
				this.m_ElementCount = -1;
				this.m_CurrentBlock = null;
				this.m_CurrentBlockEnd = null;
				this.m_CurrentPtr = null;
				this.m_FirstBlock = null;
				this.m_NumberOfBlocks = 0;
				this.m_FirstOffset = 0;
				this.m_ThreadIndex = 0;
			}

			// Token: 0x17000159 RID: 345
			// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x00023C94 File Offset: 0x00021E94
			public unsafe int ForEachCount
			{
				get
				{
					return this.m_BlockStream->RangeCount;
				}
			}

			// Token: 0x06000BF8 RID: 3064 RVA: 0x00023CA1 File Offset: 0x00021EA1
			public unsafe void BeginForEachIndex(int foreachIndex)
			{
				this.m_ForeachIndex = foreachIndex;
				this.m_ElementCount = 0;
				this.m_NumberOfBlocks = 0;
				this.m_FirstBlock = this.m_CurrentBlock;
				this.m_FirstOffset = (int)((long)((byte*)this.m_CurrentPtr - (byte*)this.m_CurrentBlock));
			}

			// Token: 0x06000BF9 RID: 3065 RVA: 0x00023CDC File Offset: 0x00021EDC
			public unsafe void EndForEachIndex()
			{
				this.m_BlockStream->Ranges[this.m_ForeachIndex].ElementCount = this.m_ElementCount;
				this.m_BlockStream->Ranges[this.m_ForeachIndex].OffsetInFirstBlock = this.m_FirstOffset;
				this.m_BlockStream->Ranges[this.m_ForeachIndex].Block = this.m_FirstBlock;
				this.m_BlockStream->Ranges[this.m_ForeachIndex].LastOffset = (int)((long)((byte*)this.m_CurrentPtr - (byte*)this.m_CurrentBlock));
				this.m_BlockStream->Ranges[this.m_ForeachIndex].NumberOfBlocks = this.m_NumberOfBlocks;
			}

			// Token: 0x06000BFA RID: 3066 RVA: 0x00023DAD File Offset: 0x00021FAD
			[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
			public unsafe void Write<T>(T value) where T : struct
			{
				*this.Allocate<T>() = value;
			}

			// Token: 0x06000BFB RID: 3067 RVA: 0x00023DBC File Offset: 0x00021FBC
			[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
			public unsafe ref T Allocate<T>() where T : struct
			{
				int num = UnsafeUtility.SizeOf<T>();
				return UnsafeUtility.AsRef<T>((void*)this.Allocate(num));
			}

			// Token: 0x06000BFC RID: 3068 RVA: 0x00023DDC File Offset: 0x00021FDC
			public unsafe byte* Allocate(int size)
			{
				byte* ptr = this.m_CurrentPtr;
				this.m_CurrentPtr += size;
				if (this.m_CurrentPtr != this.m_CurrentBlockEnd)
				{
					UnsafeStreamBlock* currentBlock = this.m_CurrentBlock;
					this.m_CurrentBlock = this.m_BlockStream->Allocate(currentBlock, this.m_ThreadIndex);
					this.m_CurrentPtr = &this.m_CurrentBlock->Data.FixedElementField;
					if (this.m_FirstBlock == null)
					{
						this.m_FirstOffset = (int)((long)((byte*)this.m_CurrentPtr - (byte*)this.m_CurrentBlock));
						this.m_FirstBlock = this.m_CurrentBlock;
					}
					else
					{
						this.m_NumberOfBlocks++;
					}
					this.m_CurrentBlockEnd = (byte*)(this.m_CurrentBlock + 4096 / sizeof(UnsafeStreamBlock));
					ptr = this.m_CurrentPtr;
					this.m_CurrentPtr += size;
				}
				this.m_ElementCount++;
				return ptr;
			}

			// Token: 0x040003EB RID: 1003
			[NativeDisableUnsafePtrRestriction]
			internal unsafe UnsafeStreamBlockData* m_BlockStream;

			// Token: 0x040003EC RID: 1004
			[NativeDisableUnsafePtrRestriction]
			private unsafe UnsafeStreamBlock* m_CurrentBlock;

			// Token: 0x040003ED RID: 1005
			[NativeDisableUnsafePtrRestriction]
			private unsafe byte* m_CurrentPtr;

			// Token: 0x040003EE RID: 1006
			[NativeDisableUnsafePtrRestriction]
			private unsafe byte* m_CurrentBlockEnd;

			// Token: 0x040003EF RID: 1007
			internal int m_ForeachIndex;

			// Token: 0x040003F0 RID: 1008
			private int m_ElementCount;

			// Token: 0x040003F1 RID: 1009
			[NativeDisableUnsafePtrRestriction]
			private unsafe UnsafeStreamBlock* m_FirstBlock;

			// Token: 0x040003F2 RID: 1010
			private int m_FirstOffset;

			// Token: 0x040003F3 RID: 1011
			private int m_NumberOfBlocks;

			// Token: 0x040003F4 RID: 1012
			[NativeSetThreadIndex]
			private int m_ThreadIndex;
		}

		// Token: 0x02000153 RID: 339
		[BurstCompatible]
		public struct Reader
		{
			// Token: 0x06000BFD RID: 3069 RVA: 0x00023EB8 File Offset: 0x000220B8
			internal Reader(ref UnsafeStream stream)
			{
				this.m_BlockStream = stream.m_Block;
				this.m_CurrentBlock = null;
				this.m_CurrentPtr = null;
				this.m_CurrentBlockEnd = null;
				this.m_RemainingItemCount = 0;
				this.m_LastBlockSize = 0;
			}

			// Token: 0x06000BFE RID: 3070 RVA: 0x00023EEC File Offset: 0x000220EC
			public unsafe int BeginForEachIndex(int foreachIndex)
			{
				this.m_RemainingItemCount = this.m_BlockStream->Ranges[foreachIndex].ElementCount;
				this.m_LastBlockSize = this.m_BlockStream->Ranges[foreachIndex].LastOffset;
				this.m_CurrentBlock = this.m_BlockStream->Ranges[foreachIndex].Block;
				this.m_CurrentPtr = (byte*)(this.m_CurrentBlock + this.m_BlockStream->Ranges[foreachIndex].OffsetInFirstBlock / sizeof(UnsafeStreamBlock));
				this.m_CurrentBlockEnd = (byte*)(this.m_CurrentBlock + 4096 / sizeof(UnsafeStreamBlock));
				return this.m_RemainingItemCount;
			}

			// Token: 0x06000BFF RID: 3071 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void EndForEachIndex()
			{
			}

			// Token: 0x1700015A RID: 346
			// (get) Token: 0x06000C00 RID: 3072 RVA: 0x00023F98 File Offset: 0x00022198
			public unsafe int ForEachCount
			{
				get
				{
					return this.m_BlockStream->RangeCount;
				}
			}

			// Token: 0x1700015B RID: 347
			// (get) Token: 0x06000C01 RID: 3073 RVA: 0x00023FA5 File Offset: 0x000221A5
			public int RemainingItemCount
			{
				get
				{
					return this.m_RemainingItemCount;
				}
			}

			// Token: 0x06000C02 RID: 3074 RVA: 0x00023FB0 File Offset: 0x000221B0
			public unsafe byte* ReadUnsafePtr(int size)
			{
				this.m_RemainingItemCount--;
				byte* ptr = this.m_CurrentPtr;
				this.m_CurrentPtr += size;
				if (this.m_CurrentPtr != this.m_CurrentBlockEnd)
				{
					this.m_CurrentBlock = this.m_CurrentBlock->Next;
					this.m_CurrentPtr = &this.m_CurrentBlock->Data.FixedElementField;
					this.m_CurrentBlockEnd = (byte*)(this.m_CurrentBlock + 4096 / sizeof(UnsafeStreamBlock));
					ptr = this.m_CurrentPtr;
					this.m_CurrentPtr += size;
				}
				return ptr;
			}

			// Token: 0x06000C03 RID: 3075 RVA: 0x00024040 File Offset: 0x00022240
			[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
			public unsafe ref T Read<T>() where T : struct
			{
				int num = UnsafeUtility.SizeOf<T>();
				return UnsafeUtility.AsRef<T>((void*)this.ReadUnsafePtr(num));
			}

			// Token: 0x06000C04 RID: 3076 RVA: 0x00024060 File Offset: 0x00022260
			[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
			public unsafe ref T Peek<T>() where T : struct
			{
				int num = UnsafeUtility.SizeOf<T>();
				byte* ptr = this.m_CurrentPtr;
				if (ptr + num != this.m_CurrentBlockEnd)
				{
					ptr = &this.m_CurrentBlock->Next->Data.FixedElementField;
				}
				return UnsafeUtility.AsRef<T>((void*)ptr);
			}

			// Token: 0x06000C05 RID: 3077 RVA: 0x000240A4 File Offset: 0x000222A4
			public unsafe int Count()
			{
				int num = 0;
				for (int num2 = 0; num2 != this.m_BlockStream->RangeCount; num2++)
				{
					num += this.m_BlockStream->Ranges[num2].ElementCount;
				}
				return num;
			}

			// Token: 0x040003F5 RID: 1013
			[NativeDisableUnsafePtrRestriction]
			internal unsafe UnsafeStreamBlockData* m_BlockStream;

			// Token: 0x040003F6 RID: 1014
			[NativeDisableUnsafePtrRestriction]
			internal unsafe UnsafeStreamBlock* m_CurrentBlock;

			// Token: 0x040003F7 RID: 1015
			[NativeDisableUnsafePtrRestriction]
			internal unsafe byte* m_CurrentPtr;

			// Token: 0x040003F8 RID: 1016
			[NativeDisableUnsafePtrRestriction]
			internal unsafe byte* m_CurrentBlockEnd;

			// Token: 0x040003F9 RID: 1017
			internal int m_RemainingItemCount;

			// Token: 0x040003FA RID: 1018
			internal int m_LastBlockSize;
		}
	}
}
