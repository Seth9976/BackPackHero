using System;
using System.Threading;
using Unity.Mathematics;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200011D RID: 285
	[BurstCompatible]
	public struct UnsafeAtomicCounter32
	{
		// Token: 0x06000A75 RID: 2677 RVA: 0x0001EEBF File Offset: 0x0001D0BF
		public unsafe UnsafeAtomicCounter32(void* ptr)
		{
			this.Counter = (int*)ptr;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0001EEC8 File Offset: 0x0001D0C8
		public unsafe void Reset(int value = 0)
		{
			*this.Counter = value;
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0001EED2 File Offset: 0x0001D0D2
		public unsafe int Add(int value)
		{
			return Interlocked.Add(UnsafeUtility.AsRef<int>((void*)this.Counter), value) - value;
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0001EEE7 File Offset: 0x0001D0E7
		public int Sub(int value)
		{
			return this.Add(-value);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0001EEF4 File Offset: 0x0001D0F4
		public unsafe int AddSat(int value, int max = 2147483647)
		{
			int num = *this.Counter;
			int num2;
			do
			{
				num2 = num;
				num = ((num >= max) ? max : math.min(max, num + value));
				num = Interlocked.CompareExchange(UnsafeUtility.AsRef<int>((void*)this.Counter), num, num2);
			}
			while (num2 != num && num2 != max);
			return num2;
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0001EF38 File Offset: 0x0001D138
		public unsafe int SubSat(int value, int min = -2147483648)
		{
			int num = *this.Counter;
			int num2;
			do
			{
				num2 = num;
				num = ((num <= min) ? min : math.max(min, num - value));
				num = Interlocked.CompareExchange(UnsafeUtility.AsRef<int>((void*)this.Counter), num, num2);
			}
			while (num2 != num && num2 != min);
			return num2;
		}

		// Token: 0x04000372 RID: 882
		public unsafe int* Counter;
	}
}
