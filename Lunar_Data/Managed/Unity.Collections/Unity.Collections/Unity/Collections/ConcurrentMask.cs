using System;
using System.Threading;
using Unity.Mathematics;

namespace Unity.Collections
{
	// Token: 0x02000044 RID: 68
	internal class ConcurrentMask
	{
		// Token: 0x06000129 RID: 297 RVA: 0x000045B4 File Offset: 0x000027B4
		internal static void longestConsecutiveOnes(long value, out int offset, out int count)
		{
			count = 0;
			long num = value;
			while (num != 0L)
			{
				value = num;
				num = value & (long)((ulong)value >> 1);
				count++;
			}
			offset = math.tzcnt(value);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000045E2 File Offset: 0x000027E2
		internal static bool foundAtLeastThisManyConsecutiveOnes(long value, int minimum, out int offset, out int count)
		{
			if (minimum == 1)
			{
				offset = math.tzcnt(value);
				count = 1;
				return offset != 64;
			}
			ConcurrentMask.longestConsecutiveOnes(value, out offset, out count);
			return count >= minimum;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000460D File Offset: 0x0000280D
		internal static bool foundAtLeastThisManyConsecutiveZeroes(long value, int minimum, out int offset, out int count)
		{
			return ConcurrentMask.foundAtLeastThisManyConsecutiveOnes(~value, minimum, out offset, out count);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00004619 File Offset: 0x00002819
		internal static bool Succeeded(int error)
		{
			return error >= 0;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004622 File Offset: 0x00002822
		internal static long MakeMask(int offset, int bits)
		{
			return (long)((long)(ulong.MaxValue >> 64 - bits) << offset);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004634 File Offset: 0x00002834
		internal static int TryAllocate(ref long l, int offset, int bits)
		{
			long num = ConcurrentMask.MakeMask(offset, bits);
			long num2 = Interlocked.Read(ref l);
			while ((num2 & num) == 0L)
			{
				long num3 = num2 | num;
				long num4 = num2;
				num2 = Interlocked.CompareExchange(ref l, num3, num4);
				if (num2 == num4)
				{
					return math.countbits(num2);
				}
			}
			return -2;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004674 File Offset: 0x00002874
		internal static int TryFree(ref long l, int offset, int bits)
		{
			long num = ConcurrentMask.MakeMask(offset, bits);
			long num2 = Interlocked.Read(ref l);
			while ((num2 & num) == num)
			{
				long num3 = num2 & ~num;
				long num4 = num2;
				num2 = Interlocked.CompareExchange(ref l, num3, num4);
				if (num2 == num4)
				{
					return math.countbits(num3);
				}
			}
			return -1;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000046B4 File Offset: 0x000028B4
		internal static int TryAllocate(ref long l, out int offset, int bits)
		{
			long num = Interlocked.Read(ref l);
			int num2;
			while (ConcurrentMask.foundAtLeastThisManyConsecutiveZeroes(num, bits, out offset, out num2))
			{
				long num3 = ConcurrentMask.MakeMask(offset, bits);
				long num4 = num | num3;
				long num5 = num;
				num = Interlocked.CompareExchange(ref l, num4, num5);
				if (num == num5)
				{
					return math.countbits(num);
				}
			}
			return -2;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000046FC File Offset: 0x000028FC
		internal static int TryAllocate<T>(ref T t, int offset, int bits) where T : IIndexable<long>
		{
			int num = offset >> 6;
			int num2 = offset & 63;
			return ConcurrentMask.TryAllocate(t.ElementAt(num), num2, bits);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004728 File Offset: 0x00002928
		internal static int TryFree<T>(ref T t, int offset, int bits) where T : IIndexable<long>
		{
			int num = offset >> 6;
			int num2 = offset & 63;
			return ConcurrentMask.TryFree(t.ElementAt(num), num2, bits);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004754 File Offset: 0x00002954
		internal static int TryAllocate<T>(ref T t, out int offset, int begin, int end, int bits) where T : IIndexable<long>
		{
			for (int i = begin; i < end; i++)
			{
				int num2;
				int num = ConcurrentMask.TryAllocate(t.ElementAt(i), out num2, bits);
				if (ConcurrentMask.Succeeded(num))
				{
					offset = i * 64 + num2;
					return num;
				}
			}
			offset = -1;
			return -2;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000479B File Offset: 0x0000299B
		internal static int TryAllocate<T>(ref T t, out int offset, int bits) where T : IIndexable<long>
		{
			return ConcurrentMask.TryAllocate<T>(ref t, out offset, 0, t.Length, bits);
		}

		// Token: 0x04000096 RID: 150
		internal const int ErrorFailedToFree = -1;

		// Token: 0x04000097 RID: 151
		internal const int ErrorFailedToAllocate = -2;

		// Token: 0x04000098 RID: 152
		internal const int EmptyBeforeAllocation = 0;

		// Token: 0x04000099 RID: 153
		internal const int EmptyAfterFree = 0;
	}
}
