using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace UnityEngine.Rendering
{
	// Token: 0x0200009E RID: 158
	[DebuggerDisplay("{this.GetType().Name} {humanizedData}")]
	[Serializable]
	public struct BitArray32 : IBitArray
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0001805D File Offset: 0x0001625D
		public uint capacity
		{
			get
			{
				return 32U;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00018061 File Offset: 0x00016261
		public bool allFalse
		{
			get
			{
				return this.data == 0U;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0001806C File Offset: 0x0001626C
		public bool allTrue
		{
			get
			{
				return this.data == uint.MaxValue;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x00018077 File Offset: 0x00016277
		private string humanizedVersion
		{
			get
			{
				return Convert.ToString((long)((ulong)this.data), 2);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x00018088 File Offset: 0x00016288
		public string humanizedData
		{
			get
			{
				return Regex.Replace(string.Format("{0, " + this.capacity.ToString() + "}", Convert.ToString((long)((ulong)this.data), 2)).Replace(' ', '0'), ".{8}", "$0.").TrimEnd('.');
			}
		}

		// Token: 0x170000AA RID: 170
		public bool this[uint index]
		{
			get
			{
				return BitArrayUtilities.Get32(index, this.data);
			}
			set
			{
				BitArrayUtilities.Set32(index, ref this.data, value);
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00018100 File Offset: 0x00016300
		public BitArray32(uint initValue)
		{
			this.data = initValue;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0001810C File Offset: 0x0001630C
		public BitArray32(IEnumerable<uint> bitIndexTrue)
		{
			this.data = 0U;
			if (bitIndexTrue == null)
			{
				return;
			}
			for (int i = bitIndexTrue.Count<uint>() - 1; i >= 0; i--)
			{
				uint num = bitIndexTrue.ElementAt(i);
				if (num < this.capacity)
				{
					this.data |= 1U << (int)num;
				}
			}
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0001815B File Offset: 0x0001635B
		public IBitArray BitAnd(IBitArray other)
		{
			return this & (BitArray32)other;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00018173 File Offset: 0x00016373
		public IBitArray BitOr(IBitArray other)
		{
			return this | (BitArray32)other;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0001818B File Offset: 0x0001638B
		public IBitArray BitNot()
		{
			return ~this;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001819D File Offset: 0x0001639D
		public static BitArray32 operator ~(BitArray32 a)
		{
			return new BitArray32(~a.data);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x000181AB File Offset: 0x000163AB
		public static BitArray32 operator |(BitArray32 a, BitArray32 b)
		{
			return new BitArray32(a.data | b.data);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x000181BF File Offset: 0x000163BF
		public static BitArray32 operator &(BitArray32 a, BitArray32 b)
		{
			return new BitArray32(a.data & b.data);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x000181D3 File Offset: 0x000163D3
		public static bool operator ==(BitArray32 a, BitArray32 b)
		{
			return a.data == b.data;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x000181E3 File Offset: 0x000163E3
		public static bool operator !=(BitArray32 a, BitArray32 b)
		{
			return a.data != b.data;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x000181F6 File Offset: 0x000163F6
		public override bool Equals(object obj)
		{
			return obj is BitArray32 && ((BitArray32)obj).data == this.data;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00018215 File Offset: 0x00016415
		public override int GetHashCode()
		{
			return 1768953197 + this.data.GetHashCode();
		}

		// Token: 0x04000341 RID: 833
		[SerializeField]
		private uint data;
	}
}
