using System;
using System.Diagnostics;
using Unity.Mathematics;

namespace Unity.Collections
{
	// Token: 0x02000038 RID: 56
	[DebuggerTypeProxy(typeof(BitField64DebugView))]
	[BurstCompatible]
	public struct BitField64
	{
		// Token: 0x060000FA RID: 250 RVA: 0x0000405A File Offset: 0x0000225A
		public BitField64(ulong initialValue = 0UL)
		{
			this.Value = initialValue;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004063 File Offset: 0x00002263
		public void Clear()
		{
			this.Value = 0UL;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000406D File Offset: 0x0000226D
		public void SetBits(int pos, bool value)
		{
			this.Value = Bitwise.SetBits(this.Value, pos, 1UL, value);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004084 File Offset: 0x00002284
		public void SetBits(int pos, bool value, int numBits = 1)
		{
			ulong num = ulong.MaxValue >> 64 - numBits;
			this.Value = Bitwise.SetBits(this.Value, pos, num, value);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000040B0 File Offset: 0x000022B0
		public ulong GetBits(int pos, int numBits = 1)
		{
			ulong num = ulong.MaxValue >> 64 - numBits;
			return Bitwise.ExtractBits(this.Value, pos, num);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000040D5 File Offset: 0x000022D5
		public bool IsSet(int pos)
		{
			return this.GetBits(pos, 1) > 0UL;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000040E3 File Offset: 0x000022E3
		public bool TestNone(int pos, int numBits = 1)
		{
			return this.GetBits(pos, numBits) == 0UL;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000040F1 File Offset: 0x000022F1
		public bool TestAny(int pos, int numBits = 1)
		{
			return this.GetBits(pos, numBits) > 0UL;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004100 File Offset: 0x00002300
		public bool TestAll(int pos, int numBits = 1)
		{
			ulong num = ulong.MaxValue >> 64 - numBits;
			return num == Bitwise.ExtractBits(this.Value, pos, num);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004128 File Offset: 0x00002328
		public int CountBits()
		{
			return math.countbits(this.Value);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004135 File Offset: 0x00002335
		public int CountLeadingZeros()
		{
			return math.lzcnt(this.Value);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004142 File Offset: 0x00002342
		public int CountTrailingZeros()
		{
			return math.tzcnt(this.Value);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000414F File Offset: 0x0000234F
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckArgs(int pos, int numBits)
		{
			if (pos > 63 || numBits == 0 || numBits > 64 || pos + numBits > 64)
			{
				throw new ArgumentException(string.Format("BitField32 invalid arguments: pos {0} (must be 0-63), numBits {1} (must be 1-64).", pos, numBits));
			}
		}

		// Token: 0x04000070 RID: 112
		public ulong Value;
	}
}
