using System;
using System.Threading;
using Unity.Mathematics;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200011E RID: 286
	[BurstCompatible]
	public struct UnsafeAtomicCounter64
	{
		// Token: 0x06000A7B RID: 2683 RVA: 0x0001EF7C File Offset: 0x0001D17C
		public unsafe UnsafeAtomicCounter64(void* ptr)
		{
			this.Counter = (long*)ptr;
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0001EF85 File Offset: 0x0001D185
		public unsafe void Reset(long value = 0L)
		{
			*this.Counter = value;
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0001EF8F File Offset: 0x0001D18F
		public unsafe long Add(long value)
		{
			return Interlocked.Add(UnsafeUtility.AsRef<long>((void*)this.Counter), value) - value;
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0001EFA4 File Offset: 0x0001D1A4
		public long Sub(long value)
		{
			return this.Add(-value);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0001EFB0 File Offset: 0x0001D1B0
		public unsafe long AddSat(long value, long max = 9223372036854775807L)
		{
			long num = *this.Counter;
			long num2;
			do
			{
				num2 = num;
				num = ((num >= max) ? max : math.min(max, num + value));
				num = Interlocked.CompareExchange(UnsafeUtility.AsRef<long>((void*)this.Counter), num, num2);
			}
			while (num2 != num && num2 != max);
			return num2;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0001EFF4 File Offset: 0x0001D1F4
		public unsafe long SubSat(long value, long min = -9223372036854775808L)
		{
			long num = *this.Counter;
			long num2;
			do
			{
				num2 = num;
				num = ((num <= min) ? min : math.max(min, num - value));
				num = Interlocked.CompareExchange(UnsafeUtility.AsRef<long>((void*)this.Counter), num, num2);
			}
			while (num2 != num && num2 != min);
			return num2;
		}

		// Token: 0x04000373 RID: 883
		public unsafe long* Counter;
	}
}
