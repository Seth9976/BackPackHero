using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace UnityEngine.Rendering
{
	// Token: 0x0200009D RID: 157
	[DebuggerDisplay("{this.GetType().Name} {humanizedData}")]
	[Serializable]
	public struct BitArray16 : IBitArray
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x00017E9D File Offset: 0x0001609D
		public uint capacity
		{
			get
			{
				return 16U;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x00017EA1 File Offset: 0x000160A1
		public bool allFalse
		{
			get
			{
				return this.data == 0;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x00017EAC File Offset: 0x000160AC
		public bool allTrue
		{
			get
			{
				return this.data == ushort.MaxValue;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00017EBC File Offset: 0x000160BC
		public string humanizedData
		{
			get
			{
				return Regex.Replace(string.Format("{0, " + this.capacity.ToString() + "}", Convert.ToString((int)this.data, 2)).Replace(' ', '0'), ".{8}", "$0.").TrimEnd('.');
			}
		}

		// Token: 0x170000A4 RID: 164
		public bool this[uint index]
		{
			get
			{
				return BitArrayUtilities.Get16(index, this.data);
			}
			set
			{
				BitArrayUtilities.Set16(index, ref this.data, value);
			}
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00017F33 File Offset: 0x00016133
		public BitArray16(ushort initValue)
		{
			this.data = initValue;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00017F3C File Offset: 0x0001613C
		public BitArray16(IEnumerable<uint> bitIndexTrue)
		{
			this.data = 0;
			if (bitIndexTrue == null)
			{
				return;
			}
			for (int i = bitIndexTrue.Count<uint>() - 1; i >= 0; i--)
			{
				uint num = bitIndexTrue.ElementAt(i);
				if (num < this.capacity)
				{
					this.data |= (ushort)(1 << (int)num);
				}
			}
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00017F8D File Offset: 0x0001618D
		public static BitArray16 operator ~(BitArray16 a)
		{
			return new BitArray16(~a.data);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00017F9C File Offset: 0x0001619C
		public static BitArray16 operator |(BitArray16 a, BitArray16 b)
		{
			return new BitArray16(a.data | b.data);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00017FB1 File Offset: 0x000161B1
		public static BitArray16 operator &(BitArray16 a, BitArray16 b)
		{
			return new BitArray16(a.data & b.data);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00017FC6 File Offset: 0x000161C6
		public IBitArray BitAnd(IBitArray other)
		{
			return this & (BitArray16)other;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00017FDE File Offset: 0x000161DE
		public IBitArray BitOr(IBitArray other)
		{
			return this | (BitArray16)other;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00017FF6 File Offset: 0x000161F6
		public IBitArray BitNot()
		{
			return ~this;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00018008 File Offset: 0x00016208
		public static bool operator ==(BitArray16 a, BitArray16 b)
		{
			return a.data == b.data;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00018018 File Offset: 0x00016218
		public static bool operator !=(BitArray16 a, BitArray16 b)
		{
			return a.data != b.data;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0001802B File Offset: 0x0001622B
		public override bool Equals(object obj)
		{
			return obj is BitArray16 && ((BitArray16)obj).data == this.data;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001804A File Offset: 0x0001624A
		public override int GetHashCode()
		{
			return 1768953197 + this.data.GetHashCode();
		}

		// Token: 0x04000340 RID: 832
		[SerializeField]
		private ushort data;
	}
}
