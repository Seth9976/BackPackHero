using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace UnityEngine.Rendering
{
	// Token: 0x0200009C RID: 156
	[DebuggerDisplay("{this.GetType().Name} {humanizedData}")]
	[Serializable]
	public struct BitArray8 : IBitArray
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00017CF0 File Offset: 0x00015EF0
		public uint capacity
		{
			get
			{
				return 8U;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00017CF3 File Offset: 0x00015EF3
		public bool allFalse
		{
			get
			{
				return this.data == 0;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00017CFE File Offset: 0x00015EFE
		public bool allTrue
		{
			get
			{
				return this.data == byte.MaxValue;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00017D10 File Offset: 0x00015F10
		public string humanizedData
		{
			get
			{
				return string.Format("{0, " + this.capacity.ToString() + "}", Convert.ToString(this.data, 2)).Replace(' ', '0');
			}
		}

		// Token: 0x1700009F RID: 159
		public bool this[uint index]
		{
			get
			{
				return BitArrayUtilities.Get8(index, this.data);
			}
			set
			{
				BitArrayUtilities.Set8(index, ref this.data, value);
			}
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00017D71 File Offset: 0x00015F71
		public BitArray8(byte initValue)
		{
			this.data = initValue;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00017D7C File Offset: 0x00015F7C
		public BitArray8(IEnumerable<uint> bitIndexTrue)
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
					this.data |= (byte)(1 << (int)num);
				}
			}
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00017DCD File Offset: 0x00015FCD
		public static BitArray8 operator ~(BitArray8 a)
		{
			return new BitArray8(~a.data);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00017DDC File Offset: 0x00015FDC
		public static BitArray8 operator |(BitArray8 a, BitArray8 b)
		{
			return new BitArray8(a.data | b.data);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00017DF1 File Offset: 0x00015FF1
		public static BitArray8 operator &(BitArray8 a, BitArray8 b)
		{
			return new BitArray8(a.data & b.data);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00017E06 File Offset: 0x00016006
		public IBitArray BitAnd(IBitArray other)
		{
			return this & (BitArray8)other;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00017E1E File Offset: 0x0001601E
		public IBitArray BitOr(IBitArray other)
		{
			return this | (BitArray8)other;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00017E36 File Offset: 0x00016036
		public IBitArray BitNot()
		{
			return ~this;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00017E48 File Offset: 0x00016048
		public static bool operator ==(BitArray8 a, BitArray8 b)
		{
			return a.data == b.data;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00017E58 File Offset: 0x00016058
		public static bool operator !=(BitArray8 a, BitArray8 b)
		{
			return a.data != b.data;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00017E6B File Offset: 0x0001606B
		public override bool Equals(object obj)
		{
			return obj is BitArray8 && ((BitArray8)obj).data == this.data;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00017E8A File Offset: 0x0001608A
		public override int GetHashCode()
		{
			return 1768953197 + this.data.GetHashCode();
		}

		// Token: 0x0400033F RID: 831
		[SerializeField]
		private byte data;
	}
}
