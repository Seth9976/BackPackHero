using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Burst;
using Unity.Burst.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Pathfinding.Util
{
	// Token: 0x02000249 RID: 585
	[BurstCompile]
	public struct HierarchicalBitset
	{
		// Token: 0x06000DB4 RID: 3508 RVA: 0x00057070 File Offset: 0x00055270
		public HierarchicalBitset(int size, Allocator allocator)
		{
			this.allocator = allocator;
			this.l1 = new UnsafeSpan<ulong>(allocator, size + 64 - 1 >> 6);
			this.l2 = new UnsafeSpan<ulong>(allocator, size + 4095 >> 6 >> 6);
			this.l3 = new UnsafeSpan<ulong>(allocator, size + 262143 >> 6 >> 6 >> 6);
			this.l1.FillZeros<ulong>();
			this.l2.FillZeros<ulong>();
			this.l3.FillZeros<ulong>();
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x000570E9 File Offset: 0x000552E9
		public bool IsCreated
		{
			get
			{
				return this.Capacity > 0;
			}
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x000570F4 File Offset: 0x000552F4
		public void Dispose()
		{
			this.l1.Free(this.allocator);
			this.l2.Free(this.allocator);
			this.l3.Free(this.allocator);
			this = default(HierarchicalBitset);
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x00057130 File Offset: 0x00055330
		// (set) Token: 0x06000DB8 RID: 3512 RVA: 0x00057140 File Offset: 0x00055340
		public int Capacity
		{
			get
			{
				return this.l1.Length << 6;
			}
			set
			{
				if (value < this.Capacity)
				{
					throw new ArgumentException("Shrinking the bitset is not supported");
				}
				if (value == this.Capacity)
				{
					return;
				}
				HierarchicalBitset hierarchicalBitset = new HierarchicalBitset(value, this.allocator);
				this.l1.CopyTo(hierarchicalBitset.l1);
				this.l2.CopyTo(hierarchicalBitset.l2);
				this.l3.CopyTo(hierarchicalBitset.l3);
				this.Dispose();
				this = hierarchicalBitset;
			}
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x000571BC File Offset: 0x000553BC
		public unsafe int Count()
		{
			int num = 0;
			for (int i = 0; i < this.l1.Length; i++)
			{
				num += math.countbits(*this.l1[i]);
			}
			return num;
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000DBA RID: 3514 RVA: 0x000571F8 File Offset: 0x000553F8
		public unsafe bool IsEmpty
		{
			get
			{
				for (int i = 0; i < this.l3.Length; i++)
				{
					if (*this.l3[i] != 0UL)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0005722D File Offset: 0x0005542D
		public void Clear()
		{
			this.l1.FillZeros<ulong>();
			this.l2.FillZeros<ulong>();
			this.l3.FillZeros<ulong>();
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x00057250 File Offset: 0x00055450
		public void GetIndices(NativeList<int> result)
		{
			NativeArray<int> nativeArray = new NativeArray<int>(256, Allocator.Temp, NativeArrayOptions.ClearMemory);
			HierarchicalBitset.Iterator iterator = this.GetIterator(nativeArray.AsUnsafeSpan<int>());
			while (iterator.MoveNext())
			{
				UnsafeSpan<int> unsafeSpan = iterator.Current;
				for (int i = 0; i < unsafeSpan.Length; i++)
				{
					result.Add(unsafeSpan[i]);
				}
			}
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x000572AC File Offset: 0x000554AC
		[MethodImpl(256)]
		private unsafe static bool SetAtomic(ref UnsafeSpan<ulong> span, int index)
		{
			int num = index >> 6;
			ulong num2 = *span[num];
			if ((num2 & (1UL << index)) != 0UL)
			{
				return true;
			}
			for (;;)
			{
				ulong num3 = (ulong)Interlocked.CompareExchange(UnsafeUtility.As<ulong, long>(span[num]), (long)(num2 | (1UL << index)), (long)num2);
				if (num3 == num2)
				{
					break;
				}
				num2 = num3;
			}
			return false;
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x000572F8 File Offset: 0x000554F8
		[MethodImpl(256)]
		private unsafe static bool ResetAtomic(ref UnsafeSpan<ulong> span, int index)
		{
			int num = index >> 6;
			ulong num2 = *span[num];
			if ((num2 & (1UL << index)) == 0UL)
			{
				return true;
			}
			for (;;)
			{
				ulong num3 = (ulong)Interlocked.CompareExchange(UnsafeUtility.As<ulong, long>(span[num]), (long)(num2 & ~(1UL << index)), (long)num2);
				if (num3 == num2)
				{
					break;
				}
				num2 = num3;
			}
			return false;
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x00057345 File Offset: 0x00055545
		[MethodImpl(256)]
		public unsafe bool Get(int index)
		{
			return (*this.l1[index >> 6] & (1UL << index)) > 0UL;
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x00057362 File Offset: 0x00055562
		[MethodImpl(256)]
		public void Set(int index)
		{
			if (HierarchicalBitset.SetAtomic(ref this.l1, index))
			{
				return;
			}
			HierarchicalBitset.SetAtomic(ref this.l2, index >> 6);
			HierarchicalBitset.SetAtomic(ref this.l3, index >> 12);
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x00057394 File Offset: 0x00055594
		[MethodImpl(256)]
		public unsafe void Reset(int index)
		{
			if (HierarchicalBitset.ResetAtomic(ref this.l1, index))
			{
				return;
			}
			if (*this.l1[index >> 6] == 0UL)
			{
				HierarchicalBitset.ResetAtomic(ref this.l2, index >> 6);
			}
			if (*this.l2[index >> 12] == 0UL)
			{
				HierarchicalBitset.ResetAtomic(ref this.l3, index >> 12);
			}
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x000573F2 File Offset: 0x000555F2
		public HierarchicalBitset.Iterator GetIterator(UnsafeSpan<int> scratchBuffer)
		{
			return new HierarchicalBitset.Iterator(this, scratchBuffer);
		}

		// Token: 0x04000AB9 RID: 2745
		private UnsafeSpan<ulong> l1;

		// Token: 0x04000ABA RID: 2746
		private UnsafeSpan<ulong> l2;

		// Token: 0x04000ABB RID: 2747
		private UnsafeSpan<ulong> l3;

		// Token: 0x04000ABC RID: 2748
		private Allocator allocator;

		// Token: 0x04000ABD RID: 2749
		private const int Log64 = 6;

		// Token: 0x0200024A RID: 586
		[BurstCompile]
		public struct Iterator : IEnumerator<UnsafeSpan<int>>, IEnumerator, IDisposable, IEnumerable<UnsafeSpan<int>>, IEnumerable
		{
			// Token: 0x170001DB RID: 475
			// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x00057400 File Offset: 0x00055600
			public UnsafeSpan<int> Current
			{
				get
				{
					return this.result.Slice(0, this.resultCount);
				}
			}

			// Token: 0x170001DC RID: 476
			// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x00022029 File Offset: 0x00020229
			object IEnumerator.Current
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x06000DC5 RID: 3525 RVA: 0x00022029 File Offset: 0x00020229
			public void Reset()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000DC6 RID: 3526 RVA: 0x000033F6 File Offset: 0x000015F6
			public void Dispose()
			{
			}

			// Token: 0x06000DC7 RID: 3527 RVA: 0x00057414 File Offset: 0x00055614
			public IEnumerator<UnsafeSpan<int>> GetEnumerator()
			{
				return this;
			}

			// Token: 0x06000DC8 RID: 3528 RVA: 0x00022029 File Offset: 0x00020229
			IEnumerator IEnumerable.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000DC9 RID: 3529 RVA: 0x00057421 File Offset: 0x00055621
			private static int l2index(int l3index, int l3bitIndex)
			{
				return (l3index << 6) + l3bitIndex;
			}

			// Token: 0x06000DCA RID: 3530 RVA: 0x00057421 File Offset: 0x00055621
			private static int l1index(int l2index, int l2bitIndex)
			{
				return (l2index << 6) + l2bitIndex;
			}

			// Token: 0x06000DCB RID: 3531 RVA: 0x00057428 File Offset: 0x00055628
			public Iterator(HierarchicalBitset bitSet, UnsafeSpan<int> result)
			{
				this.bitSet = bitSet;
				this.result = result;
				this.resultCount = 0;
				this.l3index = 0;
				this.l3bitIndex = 0;
				this.l2bitIndex = 0;
				if (result.Length < 128)
				{
					throw new ArgumentException("Result array must be at least 128 elements long");
				}
			}

			// Token: 0x06000DCC RID: 3532 RVA: 0x00057478 File Offset: 0x00055678
			public bool MoveNext()
			{
				return HierarchicalBitset.Iterator.MoveNextBurst(ref this);
			}

			// Token: 0x06000DCD RID: 3533 RVA: 0x00057480 File Offset: 0x00055680
			[BurstCompile]
			public static bool MoveNextBurst(ref HierarchicalBitset.Iterator iter)
			{
				return HierarchicalBitset.Iterator.MoveNextBurst_00000CDA$BurstDirectCall.Invoke(ref iter);
			}

			// Token: 0x06000DCE RID: 3534 RVA: 0x00057488 File Offset: 0x00055688
			[MethodImpl(256)]
			private unsafe bool MoveNextInternal()
			{
				uint num = 0U;
				int num2 = this.l3index;
				int num3 = this.l3bitIndex;
				int num4 = this.l2bitIndex;
				while ((long)num2 < (long)((ulong)this.bitSet.l3.length))
				{
					ulong num5 = *this.bitSet.l3[num2] & (ulong.MaxValue << num3);
					if (num5 != 0UL)
					{
						while (num5 != 0UL)
						{
							num3 = math.tzcnt(num5);
							int num6 = HierarchicalBitset.Iterator.l2index(num2, num3);
							for (ulong num7 = *this.bitSet.l2[num6] & (ulong.MaxValue << num4); num7 != 0UL; num7 &= num7 - 1UL)
							{
								num4 = math.tzcnt(num7);
								if ((ulong)(num + 64U) > (ulong)((long)this.result.Length))
								{
									this.resultCount = (int)num;
									this.l3index = num2;
									this.l3bitIndex = num3;
									this.l2bitIndex = num4;
									return true;
								}
								int num8 = HierarchicalBitset.Iterator.l1index(num6, num4);
								ulong num9 = *this.bitSet.l1[num8];
								int num10 = num8 << 6;
								while (num9 != 0UL)
								{
									int num11 = math.tzcnt(num9);
									num9 &= num9 - 1UL;
									int num12 = num10 + num11;
									Hint.Assume(num < (uint)this.result.Length);
									*this.result[num++] = num12;
								}
							}
							num5 &= num5 - 1UL;
							num4 = 0;
						}
						num4 = 0;
						num3 = 0;
					}
					num2++;
				}
				this.resultCount = (int)num;
				this.l3index = num2;
				this.l3bitIndex = num3;
				this.l2bitIndex = num4;
				return num > 0U;
			}

			// Token: 0x06000DCF RID: 3535 RVA: 0x00057614 File Offset: 0x00055814
			[BurstCompile]
			[MethodImpl(256)]
			public static bool MoveNextBurst$BurstManaged(ref HierarchicalBitset.Iterator iter)
			{
				return iter.MoveNextInternal();
			}

			// Token: 0x04000ABE RID: 2750
			private HierarchicalBitset bitSet;

			// Token: 0x04000ABF RID: 2751
			private UnsafeSpan<int> result;

			// Token: 0x04000AC0 RID: 2752
			private int resultCount;

			// Token: 0x04000AC1 RID: 2753
			private int l3index;

			// Token: 0x04000AC2 RID: 2754
			private int l3bitIndex;

			// Token: 0x04000AC3 RID: 2755
			private int l2bitIndex;

			// Token: 0x0200024B RID: 587
			// (Invoke) Token: 0x06000DD1 RID: 3537
			public delegate bool MoveNextBurst_00000CDA$PostfixBurstDelegate(ref HierarchicalBitset.Iterator iter);

			// Token: 0x0200024C RID: 588
			internal static class MoveNextBurst_00000CDA$BurstDirectCall
			{
				// Token: 0x06000DD4 RID: 3540 RVA: 0x0005761C File Offset: 0x0005581C
				[BurstDiscard]
				private static void GetFunctionPointerDiscard(ref IntPtr A_0)
				{
					if (HierarchicalBitset.Iterator.MoveNextBurst_00000CDA$BurstDirectCall.Pointer == 0)
					{
						HierarchicalBitset.Iterator.MoveNextBurst_00000CDA$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(HierarchicalBitset.Iterator.MoveNextBurst_00000CDA$BurstDirectCall.DeferredCompilation, methodof(HierarchicalBitset.Iterator.MoveNextBurst$BurstManaged(ref HierarchicalBitset.Iterator)).MethodHandle, typeof(HierarchicalBitset.Iterator.MoveNextBurst_00000CDA$PostfixBurstDelegate).TypeHandle);
					}
					A_0 = HierarchicalBitset.Iterator.MoveNextBurst_00000CDA$BurstDirectCall.Pointer;
				}

				// Token: 0x06000DD5 RID: 3541 RVA: 0x00057648 File Offset: 0x00055848
				private static IntPtr GetFunctionPointer()
				{
					IntPtr intPtr = (IntPtr)0;
					HierarchicalBitset.Iterator.MoveNextBurst_00000CDA$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
					return intPtr;
				}

				// Token: 0x06000DD6 RID: 3542 RVA: 0x00057660 File Offset: 0x00055860
				public static void Constructor()
				{
					HierarchicalBitset.Iterator.MoveNextBurst_00000CDA$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(HierarchicalBitset.Iterator.MoveNextBurst(ref HierarchicalBitset.Iterator)).MethodHandle);
				}

				// Token: 0x06000DD7 RID: 3543 RVA: 0x000033F6 File Offset: 0x000015F6
				public static void Initialize()
				{
				}

				// Token: 0x06000DD8 RID: 3544 RVA: 0x00057671 File Offset: 0x00055871
				// Note: this type is marked as 'beforefieldinit'.
				static MoveNextBurst_00000CDA$BurstDirectCall()
				{
					HierarchicalBitset.Iterator.MoveNextBurst_00000CDA$BurstDirectCall.Constructor();
				}

				// Token: 0x06000DD9 RID: 3545 RVA: 0x00057678 File Offset: 0x00055878
				public static bool Invoke(ref HierarchicalBitset.Iterator iter)
				{
					if (BurstCompiler.IsEnabled)
					{
						IntPtr functionPointer = HierarchicalBitset.Iterator.MoveNextBurst_00000CDA$BurstDirectCall.GetFunctionPointer();
						if (functionPointer != 0)
						{
							return calli(System.Boolean(Pathfinding.Util.HierarchicalBitset/Iterator&), ref iter, functionPointer);
						}
					}
					return HierarchicalBitset.Iterator.MoveNextBurst$BurstManaged(ref iter);
				}

				// Token: 0x04000AC4 RID: 2756
				private static IntPtr Pointer;

				// Token: 0x04000AC5 RID: 2757
				private static IntPtr DeferredCompilation;
			}
		}
	}
}
