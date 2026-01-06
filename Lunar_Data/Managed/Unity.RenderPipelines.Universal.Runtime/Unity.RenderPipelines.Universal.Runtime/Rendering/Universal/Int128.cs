using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200000A RID: 10
	internal struct Int128
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002D60 File Offset: 0x00000F60
		public Int128(long _lo)
		{
			this.lo = (ulong)_lo;
			if (_lo < 0L)
			{
				this.hi = -1L;
				return;
			}
			this.hi = 0L;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002D7F File Offset: 0x00000F7F
		public Int128(long _hi, ulong _lo)
		{
			this.lo = _lo;
			this.hi = _hi;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002D8F File Offset: 0x00000F8F
		public Int128(Int128 val)
		{
			this.hi = val.hi;
			this.lo = val.lo;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002DA9 File Offset: 0x00000FA9
		public bool IsNegative()
		{
			return this.hi < 0L;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002DB8 File Offset: 0x00000FB8
		public static bool operator ==(Int128 val1, Int128 val2)
		{
			return val1 == val2 || (val1 != null && val2 != null && val1.hi == val2.hi && val1.lo == val2.lo);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002E05 File Offset: 0x00001005
		public static bool operator !=(Int128 val1, Int128 val2)
		{
			return !(val1 == val2);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002E14 File Offset: 0x00001014
		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is Int128))
			{
				return false;
			}
			Int128 @int = (Int128)obj;
			return @int.hi == this.hi && @int.lo == this.lo;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002E53 File Offset: 0x00001053
		public override int GetHashCode()
		{
			return this.hi.GetHashCode() ^ this.lo.GetHashCode();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002E6C File Offset: 0x0000106C
		public static bool operator >(Int128 val1, Int128 val2)
		{
			if (val1.hi != val2.hi)
			{
				return val1.hi > val2.hi;
			}
			return val1.lo > val2.lo;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002E99 File Offset: 0x00001099
		public static bool operator <(Int128 val1, Int128 val2)
		{
			if (val1.hi != val2.hi)
			{
				return val1.hi < val2.hi;
			}
			return val1.lo < val2.lo;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002EC6 File Offset: 0x000010C6
		public static Int128 operator +(Int128 lhs, Int128 rhs)
		{
			lhs.hi += rhs.hi;
			lhs.lo += rhs.lo;
			if (lhs.lo < rhs.lo)
			{
				lhs.hi += 1L;
			}
			return lhs;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002F06 File Offset: 0x00001106
		public static Int128 operator -(Int128 lhs, Int128 rhs)
		{
			return lhs + -rhs;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002F14 File Offset: 0x00001114
		public static Int128 operator -(Int128 val)
		{
			if (val.lo == 0UL)
			{
				return new Int128(-val.hi, 0UL);
			}
			return new Int128(~val.hi, ~val.lo + 1UL);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002F44 File Offset: 0x00001144
		public static explicit operator double(Int128 val)
		{
			if (val.hi >= 0L)
			{
				return val.lo + (double)val.hi * 1.8446744073709552E+19;
			}
			if (val.lo == 0UL)
			{
				return (double)val.hi * 1.8446744073709552E+19;
			}
			return -(~val.lo + (double)(~(double)val.hi) * 1.8446744073709552E+19);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002FB0 File Offset: 0x000011B0
		public static Int128 Int128Mul(long lhs, long rhs)
		{
			bool flag = lhs < 0L != rhs < 0L;
			if (lhs < 0L)
			{
				lhs = -lhs;
			}
			if (rhs < 0L)
			{
				rhs = -rhs;
			}
			ulong num = (ulong)lhs >> 32;
			ulong num2 = (ulong)(lhs & (long)((ulong)(-1)));
			ulong num3 = (ulong)rhs >> 32;
			ulong num4 = (ulong)(rhs & (long)((ulong)(-1)));
			ulong num5 = num * num3;
			ulong num6 = num2 * num4;
			ulong num7 = num * num4 + num2 * num3;
			long num8 = (long)(num5 + (num7 >> 32));
			ulong num9 = (num7 << 32) + num6;
			if (num9 < num6)
			{
				num8 += 1L;
			}
			Int128 @int = new Int128(num8, num9);
			if (!flag)
			{
				return @int;
			}
			return -@int;
		}

		// Token: 0x04000021 RID: 33
		private long hi;

		// Token: 0x04000022 RID: 34
		private ulong lo;
	}
}
