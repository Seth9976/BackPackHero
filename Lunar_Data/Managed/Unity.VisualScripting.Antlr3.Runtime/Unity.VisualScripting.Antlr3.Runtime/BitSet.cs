using System;
using System.Collections;
using System.Text;

namespace Unity.VisualScripting.Antlr3.Runtime
{
	// Token: 0x0200001A RID: 26
	public class BitSet
	{
		// Token: 0x06000147 RID: 327 RVA: 0x00004776 File Offset: 0x00003776
		public BitSet()
			: this(64)
		{
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004780 File Offset: 0x00003780
		public BitSet(ulong[] bits_)
		{
			this.bits = bits_;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00004790 File Offset: 0x00003790
		public BitSet(IList items)
			: this(64)
		{
			for (int i = 0; i < items.Count; i++)
			{
				int num = (int)items[i];
				this.Add(num);
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000047CA File Offset: 0x000037CA
		public BitSet(int nbits)
		{
			this.bits = new ulong[(nbits - 1 >> 6) + 1];
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000047E4 File Offset: 0x000037E4
		public static BitSet Of(int el)
		{
			BitSet bitSet = new BitSet(el + 1);
			bitSet.Add(el);
			return bitSet;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004804 File Offset: 0x00003804
		public static BitSet Of(int a, int b)
		{
			BitSet bitSet = new BitSet(Math.Max(a, b) + 1);
			bitSet.Add(a);
			bitSet.Add(b);
			return bitSet;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00004830 File Offset: 0x00003830
		public static BitSet Of(int a, int b, int c)
		{
			BitSet bitSet = new BitSet();
			bitSet.Add(a);
			bitSet.Add(b);
			bitSet.Add(c);
			return bitSet;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000485C File Offset: 0x0000385C
		public static BitSet Of(int a, int b, int c, int d)
		{
			BitSet bitSet = new BitSet();
			bitSet.Add(a);
			bitSet.Add(b);
			bitSet.Add(c);
			bitSet.Add(d);
			return bitSet;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000488C File Offset: 0x0000388C
		public virtual BitSet Or(BitSet a)
		{
			if (a == null)
			{
				return this;
			}
			BitSet bitSet = (BitSet)this.Clone();
			bitSet.OrInPlace(a);
			return bitSet;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000048B4 File Offset: 0x000038B4
		public virtual void Add(int el)
		{
			int num = BitSet.WordNumber(el);
			if (num >= this.bits.Length)
			{
				this.GrowToInclude(el);
			}
			this.bits[num] |= BitSet.BitMask(el);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000048F8 File Offset: 0x000038F8
		public virtual void GrowToInclude(int bit)
		{
			int num = Math.Max(this.bits.Length << 1, this.NumWordsToHold(bit));
			ulong[] array = new ulong[num];
			Array.Copy(this.bits, 0, array, 0, this.bits.Length);
			this.bits = array;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00004940 File Offset: 0x00003940
		public virtual void OrInPlace(BitSet a)
		{
			if (a == null)
			{
				return;
			}
			if (a.bits.Length > this.bits.Length)
			{
				this.SetSize(a.bits.Length);
			}
			int num = Math.Min(this.bits.Length, a.bits.Length);
			for (int i = num - 1; i >= 0; i--)
			{
				this.bits[i] |= a.bits[i];
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000153 RID: 339 RVA: 0x000049B8 File Offset: 0x000039B8
		public virtual bool Nil
		{
			get
			{
				for (int i = this.bits.Length - 1; i >= 0; i--)
				{
					if (this.bits[i] != 0UL)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000049EC File Offset: 0x000039EC
		public virtual object Clone()
		{
			BitSet bitSet;
			try
			{
				bitSet = (BitSet)base.MemberwiseClone();
				bitSet.bits = new ulong[this.bits.Length];
				Array.Copy(this.bits, 0, bitSet.bits, 0, this.bits.Length);
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Unable to clone BitSet", ex);
			}
			return bitSet;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00004A54 File Offset: 0x00003A54
		public virtual int Count
		{
			get
			{
				int num = 0;
				for (int i = this.bits.Length - 1; i >= 0; i--)
				{
					ulong num2 = this.bits[i];
					if (num2 != 0UL)
					{
						for (int j = 63; j >= 0; j--)
						{
							if ((num2 & (1UL << j)) != 0UL)
							{
								num++;
							}
						}
					}
				}
				return num;
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00004AA8 File Offset: 0x00003AA8
		public virtual bool Member(int el)
		{
			if (el < 0)
			{
				return false;
			}
			int num = BitSet.WordNumber(el);
			return num < this.bits.Length && (this.bits[num] & BitSet.BitMask(el)) != 0UL;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00004AE8 File Offset: 0x00003AE8
		public virtual void Remove(int el)
		{
			int num = BitSet.WordNumber(el);
			if (num < this.bits.Length)
			{
				this.bits[num] &= ~BitSet.BitMask(el);
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00004B26 File Offset: 0x00003B26
		public virtual int NumBits()
		{
			return this.bits.Length << 6;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00004B32 File Offset: 0x00003B32
		public virtual int LengthInLongWords()
		{
			return this.bits.Length;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00004B3C File Offset: 0x00003B3C
		public virtual int[] ToArray()
		{
			int[] array = new int[this.Count];
			int num = 0;
			for (int i = 0; i < this.bits.Length << 6; i++)
			{
				if (this.Member(i))
				{
					array[num++] = i;
				}
			}
			return array;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00004B7E File Offset: 0x00003B7E
		public virtual ulong[] ToPackedArray()
		{
			return this.bits;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00004B86 File Offset: 0x00003B86
		private static int WordNumber(int bit)
		{
			return bit >> 6;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00004B8B File Offset: 0x00003B8B
		public override string ToString()
		{
			return this.ToString(null);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00004B94 File Offset: 0x00003B94
		public virtual string ToString(string[] tokenNames)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = ",";
			bool flag = false;
			stringBuilder.Append('{');
			for (int i = 0; i < this.bits.Length << 6; i++)
			{
				if (this.Member(i))
				{
					if (i > 0 && flag)
					{
						stringBuilder.Append(text);
					}
					if (tokenNames != null)
					{
						stringBuilder.Append(tokenNames[i]);
					}
					else
					{
						stringBuilder.Append(i);
					}
					flag = true;
				}
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00004C10 File Offset: 0x00003C10
		public override bool Equals(object other)
		{
			if (other == null || !(other is BitSet))
			{
				return false;
			}
			BitSet bitSet = (BitSet)other;
			int num = Math.Min(this.bits.Length, bitSet.bits.Length);
			for (int i = 0; i < num; i++)
			{
				if (this.bits[i] != bitSet.bits[i])
				{
					return false;
				}
			}
			if (this.bits.Length > num)
			{
				for (int j = num + 1; j < this.bits.Length; j++)
				{
					if (this.bits[j] != 0UL)
					{
						return false;
					}
				}
			}
			else if (bitSet.bits.Length > num)
			{
				for (int k = num + 1; k < bitSet.bits.Length; k++)
				{
					if (bitSet.bits[k] != 0UL)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00004CCB File Offset: 0x00003CCB
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00004CD4 File Offset: 0x00003CD4
		private static ulong BitMask(int bitNumber)
		{
			int num = bitNumber & BitSet.MOD_MASK;
			return 1UL << num;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00004CF0 File Offset: 0x00003CF0
		private void SetSize(int nwords)
		{
			ulong[] array = new ulong[nwords];
			int num = Math.Min(nwords, this.bits.Length);
			Array.Copy(this.bits, 0, array, 0, num);
			this.bits = array;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00004D29 File Offset: 0x00003D29
		private int NumWordsToHold(int el)
		{
			return (el >> 6) + 1;
		}

		// Token: 0x0400005C RID: 92
		protected internal const int BITS = 64;

		// Token: 0x0400005D RID: 93
		protected internal const int LOG_BITS = 6;

		// Token: 0x0400005E RID: 94
		protected internal static readonly int MOD_MASK = 63;

		// Token: 0x0400005F RID: 95
		protected internal ulong[] bits;
	}
}
