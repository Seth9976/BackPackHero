using System;
using System.Diagnostics;
using Unity.Jobs;
using Unity.Mathematics;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200011F RID: 287
	[DebuggerDisplay("Length = {Length}, IsCreated = {IsCreated}")]
	[DebuggerTypeProxy(typeof(UnsafeBitArrayDebugView))]
	[BurstCompatible]
	public struct UnsafeBitArray : INativeDisposable, IDisposable
	{
		// Token: 0x06000A81 RID: 2689 RVA: 0x0001F038 File Offset: 0x0001D238
		public unsafe UnsafeBitArray(void* ptr, int sizeInBytes, AllocatorManager.AllocatorHandle allocator = default(AllocatorManager.AllocatorHandle))
		{
			this.Ptr = (ulong*)ptr;
			this.Length = sizeInBytes * 8;
			this.Allocator = allocator;
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0001F054 File Offset: 0x0001D254
		public unsafe UnsafeBitArray(int numBits, AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options = NativeArrayOptions.ClearMemory)
		{
			this.Allocator = allocator;
			int num = Bitwise.AlignUp(numBits, 64) / 8;
			this.Ptr = (ulong*)Memory.Unmanaged.Allocate((long)num, 16, allocator);
			this.Length = numBits;
			if (options == NativeArrayOptions.ClearMemory)
			{
				UnsafeUtility.MemClear((void*)this.Ptr, (long)num);
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x0001F09B File Offset: 0x0001D29B
		public bool IsCreated
		{
			get
			{
				return this.Ptr != null;
			}
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0001F0AA File Offset: 0x0001D2AA
		public void Dispose()
		{
			if (CollectionHelper.ShouldDeallocate(this.Allocator))
			{
				Memory.Unmanaged.Free<ulong>(this.Ptr, this.Allocator);
				this.Allocator = AllocatorManager.Invalid;
			}
			this.Ptr = null;
			this.Length = 0;
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0001F0E4 File Offset: 0x0001D2E4
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

		// Token: 0x06000A86 RID: 2694 RVA: 0x0001F144 File Offset: 0x0001D344
		public unsafe void Clear()
		{
			int num = Bitwise.AlignUp(this.Length, 64) / 8;
			UnsafeUtility.MemClear((void*)this.Ptr, (long)num);
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0001F170 File Offset: 0x0001D370
		public unsafe void Set(int pos, bool value)
		{
			int num = pos >> 6;
			int num2 = pos & 63;
			ulong num3 = 1UL << num2;
			ulong num4 = (this.Ptr[num] & ~num3) | (ulong)((long)(-(long)Bitwise.FromBool(value)) & (long)num3);
			this.Ptr[num] = num4;
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0001F1B8 File Offset: 0x0001D3B8
		public unsafe void SetBits(int pos, bool value, int numBits)
		{
			int num = math.min(pos + numBits, this.Length);
			int num2 = pos >> 6;
			int num3 = pos & 63;
			int num4 = num - 1 >> 6;
			int num5 = num & 63;
			ulong num6 = ulong.MaxValue << num3;
			ulong num7 = ulong.MaxValue >> 64 - num5;
			ulong num8 = (ulong)((long)(-(long)Bitwise.FromBool(value)));
			ulong num9 = num6 & num8;
			ulong num10 = num7 & num8;
			ulong num11 = ~num6;
			ulong num12 = ~num7;
			if (num2 == num4)
			{
				ulong num13 = ~(num6 & num7);
				ulong num14 = num9 & num10;
				this.Ptr[num2] = (this.Ptr[num2] & num13) | num14;
				return;
			}
			this.Ptr[num2] = (this.Ptr[num2] & num11) | num9;
			for (int i = num2 + 1; i < num4; i++)
			{
				this.Ptr[i] = num8;
			}
			this.Ptr[num4] = (this.Ptr[num4] & num12) | num10;
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0001F2AC File Offset: 0x0001D4AC
		public unsafe void SetBits(int pos, ulong value, int numBits = 1)
		{
			int num = pos >> 6;
			int num2 = pos & 63;
			if (num2 + numBits <= 64)
			{
				ulong num3 = ulong.MaxValue >> 64 - numBits;
				this.Ptr[num] = Bitwise.ReplaceBits(this.Ptr[num], num2, num3, value);
				return;
			}
			int num4 = math.min(pos + numBits, this.Length);
			int num5 = num4 - 1 >> 6;
			int num6 = num4 & 63;
			ulong num7 = ulong.MaxValue >> num2;
			this.Ptr[num] = Bitwise.ReplaceBits(this.Ptr[num], num2, num7, value);
			ulong num8 = value >> 64 - num2;
			ulong num9 = ulong.MaxValue >> 64 - num6;
			this.Ptr[num5] = Bitwise.ReplaceBits(this.Ptr[num5], 0, num9, num8);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0001F374 File Offset: 0x0001D574
		public unsafe ulong GetBits(int pos, int numBits = 1)
		{
			int num = pos >> 6;
			int num2 = pos & 63;
			if (num2 + numBits <= 64)
			{
				ulong num3 = ulong.MaxValue >> 64 - numBits;
				return Bitwise.ExtractBits(this.Ptr[num], num2, num3);
			}
			int num4 = math.min(pos + numBits, this.Length);
			int num5 = num4 - 1 >> 6;
			int num6 = num4 & 63;
			ulong num7 = ulong.MaxValue >> num2;
			ulong num8 = Bitwise.ExtractBits(this.Ptr[num], num2, num7);
			ulong num9 = ulong.MaxValue >> 64 - num6;
			return (Bitwise.ExtractBits(this.Ptr[num5], 0, num9) << 64 - num2) | num8;
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0001F414 File Offset: 0x0001D614
		public unsafe bool IsSet(int pos)
		{
			int num = pos >> 6;
			int num2 = pos & 63;
			ulong num3 = 1UL << num2;
			return (this.Ptr[num] & num3) > 0UL;
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0001F444 File Offset: 0x0001D644
		internal void CopyUlong(int dstPos, ref UnsafeBitArray srcBitArray, int srcPos, int numBits)
		{
			this.SetBits(dstPos, srcBitArray.GetBits(srcPos, numBits), numBits);
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0001F458 File Offset: 0x0001D658
		public void Copy(int dstPos, int srcPos, int numBits)
		{
			if (dstPos == srcPos)
			{
				return;
			}
			this.Copy(dstPos, ref this, srcPos, numBits);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0001F46C File Offset: 0x0001D66C
		public unsafe void Copy(int dstPos, ref UnsafeBitArray srcBitArray, int srcPos, int numBits)
		{
			if (numBits == 0)
			{
				return;
			}
			if (numBits <= 64)
			{
				this.CopyUlong(dstPos, ref srcBitArray, srcPos, numBits);
				return;
			}
			if (numBits <= 128)
			{
				this.CopyUlong(dstPos, ref srcBitArray, srcPos, 64);
				numBits -= 64;
				if (numBits > 0)
				{
					this.CopyUlong(dstPos + 64, ref srcBitArray, srcPos + 64, numBits);
					return;
				}
			}
			else if ((dstPos & 7) == (srcPos & 7))
			{
				int num = CollectionHelper.Align(dstPos, 8) >> 3;
				int num2 = CollectionHelper.Align(srcPos, 8) >> 3;
				int num3 = num * 8 - dstPos;
				if (num3 > 0)
				{
					this.CopyUlong(dstPos, ref srcBitArray, srcPos, num3);
				}
				int num4 = numBits - num3;
				int num5 = num4 / 8;
				if (num5 > 0)
				{
					UnsafeUtility.MemMove((void*)(this.Ptr + num / 8), (void*)(srcBitArray.Ptr + num2 / 8), (long)num5);
				}
				int num6 = num4 & 7;
				if (num6 > 0)
				{
					this.CopyUlong((num + num5) * 8, ref srcBitArray, (num2 + num5) * 8, num6);
					return;
				}
			}
			else
			{
				int num7 = CollectionHelper.Align(dstPos, 64) - dstPos;
				if (num7 > 0)
				{
					this.CopyUlong(dstPos, ref srcBitArray, srcPos, num7);
					numBits -= num7;
					dstPos += num7;
					srcPos += num7;
				}
				while (numBits >= 64)
				{
					this.Ptr[dstPos >> 6] = srcBitArray.GetBits(srcPos, 64);
					numBits -= 64;
					dstPos += 64;
					srcPos += 64;
				}
				if (numBits > 0)
				{
					this.CopyUlong(dstPos, ref srcBitArray, srcPos, numBits);
				}
			}
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0001F5AC File Offset: 0x0001D7AC
		public int Find(int pos, int numBits)
		{
			int num = this.Length - pos;
			return Bitwise.Find(this.Ptr, pos, num, numBits);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0001F5D0 File Offset: 0x0001D7D0
		public int Find(int pos, int count, int numBits)
		{
			return Bitwise.Find(this.Ptr, pos, count, numBits);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0001F5E0 File Offset: 0x0001D7E0
		public unsafe bool TestNone(int pos, int numBits = 1)
		{
			int num = math.min(pos + numBits, this.Length);
			int num2 = pos >> 6;
			int num3 = pos & 63;
			int num4 = num - 1 >> 6;
			int num5 = num & 63;
			ulong num6 = ulong.MaxValue << num3;
			ulong num7 = ulong.MaxValue >> 64 - num5;
			if (num2 == num4)
			{
				ulong num8 = num6 & num7;
				return (this.Ptr[num2] & num8) == 0UL;
			}
			if ((this.Ptr[num2] & num6) != 0UL)
			{
				return false;
			}
			for (int i = num2 + 1; i < num4; i++)
			{
				if (this.Ptr[i] != 0UL)
				{
					return false;
				}
			}
			return (this.Ptr[num4] & num7) == 0UL;
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0001F68C File Offset: 0x0001D88C
		public unsafe bool TestAny(int pos, int numBits = 1)
		{
			int num = math.min(pos + numBits, this.Length);
			int num2 = pos >> 6;
			int num3 = pos & 63;
			int num4 = num - 1 >> 6;
			int num5 = num & 63;
			ulong num6 = ulong.MaxValue << num3;
			ulong num7 = ulong.MaxValue >> 64 - num5;
			if (num2 == num4)
			{
				ulong num8 = num6 & num7;
				return (this.Ptr[num2] & num8) > 0UL;
			}
			if ((this.Ptr[num2] & num6) != 0UL)
			{
				return true;
			}
			for (int i = num2 + 1; i < num4; i++)
			{
				if (this.Ptr[i] != 0UL)
				{
					return true;
				}
			}
			return (this.Ptr[num4] & num7) > 0UL;
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0001F738 File Offset: 0x0001D938
		public unsafe bool TestAll(int pos, int numBits = 1)
		{
			int num = math.min(pos + numBits, this.Length);
			int num2 = pos >> 6;
			int num3 = pos & 63;
			int num4 = num - 1 >> 6;
			int num5 = num & 63;
			ulong num6 = ulong.MaxValue << num3;
			ulong num7 = ulong.MaxValue >> 64 - num5;
			if (num2 == num4)
			{
				ulong num8 = num6 & num7;
				return num8 == (this.Ptr[num2] & num8);
			}
			if (num6 != (this.Ptr[num2] & num6))
			{
				return false;
			}
			for (int i = num2 + 1; i < num4; i++)
			{
				if (18446744073709551615UL != this.Ptr[i])
				{
					return false;
				}
			}
			return num7 == (this.Ptr[num4] & num7);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0001F7E8 File Offset: 0x0001D9E8
		public unsafe int CountBits(int pos, int numBits = 1)
		{
			int num = math.min(pos + numBits, this.Length);
			int num2 = pos >> 6;
			int num3 = pos & 63;
			int num4 = num - 1 >> 6;
			int num5 = num & 63;
			ulong num6 = ulong.MaxValue << num3;
			ulong num7 = ulong.MaxValue >> 64 - num5;
			if (num2 == num4)
			{
				ulong num8 = num6 & num7;
				return math.countbits(this.Ptr[num2] & num8);
			}
			int num9 = math.countbits(this.Ptr[num2] & num6);
			for (int i = num2 + 1; i < num4; i++)
			{
				num9 += math.countbits(this.Ptr[i]);
			}
			return num9 + math.countbits(this.Ptr[num4] & num7);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0001F8A5 File Offset: 0x0001DAA5
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckSizeMultipleOf8(int sizeInBytes)
		{
			if ((sizeInBytes & 7) != 0)
			{
				throw new ArgumentException(string.Format("BitArray invalid arguments: sizeInBytes {0} (must be multiple of 8-bytes, sizeInBytes: {1}).", sizeInBytes, sizeInBytes));
			}
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0001F8C8 File Offset: 0x0001DAC8
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckArgs(int pos, int numBits)
		{
			if (pos < 0 || pos >= this.Length || numBits < 1)
			{
				throw new ArgumentException(string.Format("BitArray invalid arguments: pos {0} (must be 0-{1}), numBits {2} (must be greater than 0).", pos, this.Length - 1, numBits));
			}
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0001F904 File Offset: 0x0001DB04
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckArgsPosCount(int begin, int count, int numBits)
		{
			if (begin < 0 || begin >= this.Length)
			{
				throw new ArgumentException(string.Format("BitArray invalid argument: begin {0} (must be 0-{1}).", begin, this.Length - 1));
			}
			if (count < 0 || count > this.Length)
			{
				throw new ArgumentException(string.Format("BitArray invalid argument: count {0} (must be 0-{1}).", count, this.Length));
			}
			if (numBits < 1 || count < numBits)
			{
				throw new ArgumentException(string.Format("BitArray invalid argument: numBits {0} (must be greater than 0).", numBits));
			}
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0001F990 File Offset: 0x0001DB90
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckArgsUlong(int pos, int numBits)
		{
			if (numBits < 1 || numBits > 64)
			{
				throw new ArgumentException(string.Format("BitArray invalid arguments: numBits {0} (must be 1-64).", numBits));
			}
			if (pos + numBits > this.Length)
			{
				throw new ArgumentException(string.Format("BitArray invalid arguments: Out of bounds pos {0}, numBits {1}, Length {2}.", pos, numBits, this.Length));
			}
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0001F9F0 File Offset: 0x0001DBF0
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckArgsCopy(ref UnsafeBitArray dstBitArray, int dstPos, ref UnsafeBitArray srcBitArray, int srcPos, int numBits)
		{
			if (srcPos + numBits > srcBitArray.Length)
			{
				throw new ArgumentException(string.Format("BitArray invalid arguments: Out of bounds - source position {0}, numBits {1}, source bit array Length {2}.", srcPos, numBits, srcBitArray.Length));
			}
			if (dstPos + numBits > dstBitArray.Length)
			{
				throw new ArgumentException(string.Format("BitArray invalid arguments: Out of bounds - destination position {0}, numBits {1}, destination bit array Length {2}.", dstPos, numBits, dstBitArray.Length));
			}
		}

		// Token: 0x04000374 RID: 884
		[NativeDisableUnsafePtrRestriction]
		public unsafe ulong* Ptr;

		// Token: 0x04000375 RID: 885
		public int Length;

		// Token: 0x04000376 RID: 886
		public AllocatorManager.AllocatorHandle Allocator;
	}
}
