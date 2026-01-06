using System;
using System.Diagnostics;
using Unity.Mathematics;

namespace Unity.Collections
{
	// Token: 0x02000036 RID: 54
	[DebuggerTypeProxy(typeof(BitField32DebugView))]
	[BurstCompatible]
	public struct BitField32
	{
		// Token: 0x060000EB RID: 235 RVA: 0x00003EF8 File Offset: 0x000020F8
		public BitField32(uint initialValue = 0U)
		{
			this.Value = initialValue;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00003F01 File Offset: 0x00002101
		public void Clear()
		{
			this.Value = 0U;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00003F0A File Offset: 0x0000210A
		public void SetBits(int pos, bool value)
		{
			this.Value = Bitwise.SetBits(this.Value, pos, 1U, value);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00003F20 File Offset: 0x00002120
		public void SetBits(int pos, bool value, int numBits)
		{
			uint num = uint.MaxValue >> 32 - numBits;
			this.Value = Bitwise.SetBits(this.Value, pos, num, value);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00003F4C File Offset: 0x0000214C
		public uint GetBits(int pos, int numBits = 1)
		{
			uint num = uint.MaxValue >> 32 - numBits;
			return Bitwise.ExtractBits(this.Value, pos, num);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00003F70 File Offset: 0x00002170
		public bool IsSet(int pos)
		{
			return this.GetBits(pos, 1) > 0U;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00003F7D File Offset: 0x0000217D
		public bool TestNone(int pos, int numBits = 1)
		{
			return this.GetBits(pos, numBits) == 0U;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003F8A File Offset: 0x0000218A
		public bool TestAny(int pos, int numBits = 1)
		{
			return this.GetBits(pos, numBits) > 0U;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00003F98 File Offset: 0x00002198
		public bool TestAll(int pos, int numBits = 1)
		{
			uint num = uint.MaxValue >> 32 - numBits;
			return num == Bitwise.ExtractBits(this.Value, pos, num);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00003FBF File Offset: 0x000021BF
		public int CountBits()
		{
			return math.countbits(this.Value);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00003FCC File Offset: 0x000021CC
		public int CountLeadingZeros()
		{
			return math.lzcnt(this.Value);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00003FD9 File Offset: 0x000021D9
		public int CountTrailingZeros()
		{
			return math.tzcnt(this.Value);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00003FE6 File Offset: 0x000021E6
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckArgs(int pos, int numBits)
		{
			if (pos > 31 || numBits == 0 || numBits > 32 || pos + numBits > 32)
			{
				throw new ArgumentException(string.Format("BitField32 invalid arguments: pos {0} (must be 0-31), numBits {1} (must be 1-32).", pos, numBits));
			}
		}

		// Token: 0x0400006E RID: 110
		public uint Value;
	}
}
