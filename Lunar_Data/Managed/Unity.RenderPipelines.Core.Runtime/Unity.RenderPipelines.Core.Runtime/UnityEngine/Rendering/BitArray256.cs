using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace UnityEngine.Rendering
{
	// Token: 0x020000A1 RID: 161
	[DebuggerDisplay("{this.GetType().Name} {humanizedData}")]
	[Serializable]
	public struct BitArray256 : IBitArray
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x000186A9 File Offset: 0x000168A9
		public uint capacity
		{
			get
			{
				return 256U;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x000186B0 File Offset: 0x000168B0
		public bool allFalse
		{
			get
			{
				return this.data1 == 0UL && this.data2 == 0UL && this.data3 == 0UL && this.data4 == 0UL;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x000186D6 File Offset: 0x000168D6
		public bool allTrue
		{
			get
			{
				return this.data1 == ulong.MaxValue && this.data2 == ulong.MaxValue && this.data3 == ulong.MaxValue && this.data4 == ulong.MaxValue;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x00018704 File Offset: 0x00016904
		public string humanizedData
		{
			get
			{
				return Regex.Replace(string.Format("{0, " + 64U.ToString() + "}", Convert.ToString((long)this.data4, 2)).Replace(' ', '0'), ".{8}", "$0.") + Regex.Replace(string.Format("{0, " + 64U.ToString() + "}", Convert.ToString((long)this.data3, 2)).Replace(' ', '0'), ".{8}", "$0.") + Regex.Replace(string.Format("{0, " + 64U.ToString() + "}", Convert.ToString((long)this.data2, 2)).Replace(' ', '0'), ".{8}", "$0.") + Regex.Replace(string.Format("{0, " + 64U.ToString() + "}", Convert.ToString((long)this.data1, 2)).Replace(' ', '0'), ".{8}", "$0.").TrimEnd('.');
			}
		}

		// Token: 0x170000B9 RID: 185
		public bool this[uint index]
		{
			get
			{
				return BitArrayUtilities.Get256(index, this.data1, this.data2, this.data3, this.data4);
			}
			set
			{
				BitArrayUtilities.Set256(index, ref this.data1, ref this.data2, ref this.data3, ref this.data4, value);
			}
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00018866 File Offset: 0x00016A66
		public BitArray256(ulong initValue1, ulong initValue2, ulong initValue3, ulong initValue4)
		{
			this.data1 = initValue1;
			this.data2 = initValue2;
			this.data3 = initValue3;
			this.data4 = initValue4;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00018888 File Offset: 0x00016A88
		public BitArray256(IEnumerable<uint> bitIndexTrue)
		{
			this.data1 = (this.data2 = (this.data3 = (this.data4 = 0UL)));
			if (bitIndexTrue == null)
			{
				return;
			}
			for (int i = bitIndexTrue.Count<uint>() - 1; i >= 0; i--)
			{
				uint num = bitIndexTrue.ElementAt(i);
				if (num < 64U)
				{
					this.data1 |= 1UL << (int)num;
				}
				else if (num < 128U)
				{
					this.data2 |= 1UL << (int)(num - 64U);
				}
				else if (num < 192U)
				{
					this.data3 |= 1UL << (int)(num - 128U);
				}
				else if (num < this.capacity)
				{
					this.data4 |= 1UL << (int)(num - 192U);
				}
			}
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00018960 File Offset: 0x00016B60
		public static BitArray256 operator ~(BitArray256 a)
		{
			return new BitArray256(~a.data1, ~a.data2, ~a.data3, ~a.data4);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00018983 File Offset: 0x00016B83
		public static BitArray256 operator |(BitArray256 a, BitArray256 b)
		{
			return new BitArray256(a.data1 | b.data1, a.data2 | b.data2, a.data3 | b.data3, a.data4 | b.data4);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x000189BE File Offset: 0x00016BBE
		public static BitArray256 operator &(BitArray256 a, BitArray256 b)
		{
			return new BitArray256(a.data1 & b.data1, a.data2 & b.data2, a.data3 & b.data3, a.data4 & b.data4);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x000189F9 File Offset: 0x00016BF9
		public IBitArray BitAnd(IBitArray other)
		{
			return this & (BitArray256)other;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00018A11 File Offset: 0x00016C11
		public IBitArray BitOr(IBitArray other)
		{
			return this | (BitArray256)other;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00018A29 File Offset: 0x00016C29
		public IBitArray BitNot()
		{
			return ~this;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00018A3B File Offset: 0x00016C3B
		public static bool operator ==(BitArray256 a, BitArray256 b)
		{
			return a.data1 == b.data1 && a.data2 == b.data2 && a.data3 == b.data3 && a.data4 == b.data4;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00018A77 File Offset: 0x00016C77
		public static bool operator !=(BitArray256 a, BitArray256 b)
		{
			return a.data1 != b.data1 || a.data2 != b.data2 || a.data3 != b.data3 || a.data4 != b.data4;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00018AB8 File Offset: 0x00016CB8
		public override bool Equals(object obj)
		{
			return obj is BitArray256 && this.data1.Equals(((BitArray256)obj).data1) && this.data2.Equals(((BitArray256)obj).data2) && this.data3.Equals(((BitArray256)obj).data3) && this.data4.Equals(((BitArray256)obj).data4);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00018B30 File Offset: 0x00016D30
		public override int GetHashCode()
		{
			return (((1870826326 * -1521134295 + this.data1.GetHashCode()) * -1521134295 + this.data2.GetHashCode()) * -1521134295 + this.data3.GetHashCode()) * -1521134295 + this.data4.GetHashCode();
		}

		// Token: 0x04000345 RID: 837
		[SerializeField]
		private ulong data1;

		// Token: 0x04000346 RID: 838
		[SerializeField]
		private ulong data2;

		// Token: 0x04000347 RID: 839
		[SerializeField]
		private ulong data3;

		// Token: 0x04000348 RID: 840
		[SerializeField]
		private ulong data4;
	}
}
