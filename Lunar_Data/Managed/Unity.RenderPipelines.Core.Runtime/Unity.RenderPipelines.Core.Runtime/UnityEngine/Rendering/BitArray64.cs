using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace UnityEngine.Rendering
{
	// Token: 0x0200009F RID: 159
	[DebuggerDisplay("{this.GetType().Name} {humanizedData}")]
	[Serializable]
	public struct BitArray64 : IBitArray
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x00018228 File Offset: 0x00016428
		public uint capacity
		{
			get
			{
				return 64U;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x0001822C File Offset: 0x0001642C
		public bool allFalse
		{
			get
			{
				return this.data == 0UL;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x00018238 File Offset: 0x00016438
		public bool allTrue
		{
			get
			{
				return this.data == ulong.MaxValue;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x00018244 File Offset: 0x00016444
		public string humanizedData
		{
			get
			{
				return Regex.Replace(string.Format("{0, " + this.capacity.ToString() + "}", Convert.ToString((long)this.data, 2)).Replace(' ', '0'), ".{8}", "$0.").TrimEnd('.');
			}
		}

		// Token: 0x170000AF RID: 175
		public bool this[uint index]
		{
			get
			{
				return BitArrayUtilities.Get64(index, this.data);
			}
			set
			{
				BitArrayUtilities.Set64(index, ref this.data, value);
			}
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x000182BB File Offset: 0x000164BB
		public BitArray64(ulong initValue)
		{
			this.data = initValue;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x000182C4 File Offset: 0x000164C4
		public BitArray64(IEnumerable<uint> bitIndexTrue)
		{
			this.data = 0UL;
			if (bitIndexTrue == null)
			{
				return;
			}
			for (int i = bitIndexTrue.Count<uint>() - 1; i >= 0; i--)
			{
				uint num = bitIndexTrue.ElementAt(i);
				if (num < this.capacity)
				{
					this.data |= 1UL << (int)num;
				}
			}
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00018315 File Offset: 0x00016515
		public static BitArray64 operator ~(BitArray64 a)
		{
			return new BitArray64(~a.data);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00018323 File Offset: 0x00016523
		public static BitArray64 operator |(BitArray64 a, BitArray64 b)
		{
			return new BitArray64(a.data | b.data);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00018337 File Offset: 0x00016537
		public static BitArray64 operator &(BitArray64 a, BitArray64 b)
		{
			return new BitArray64(a.data & b.data);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0001834B File Offset: 0x0001654B
		public IBitArray BitAnd(IBitArray other)
		{
			return this & (BitArray64)other;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00018363 File Offset: 0x00016563
		public IBitArray BitOr(IBitArray other)
		{
			return this | (BitArray64)other;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0001837B File Offset: 0x0001657B
		public IBitArray BitNot()
		{
			return ~this;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0001838D File Offset: 0x0001658D
		public static bool operator ==(BitArray64 a, BitArray64 b)
		{
			return a.data == b.data;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0001839D File Offset: 0x0001659D
		public static bool operator !=(BitArray64 a, BitArray64 b)
		{
			return a.data != b.data;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x000183B0 File Offset: 0x000165B0
		public override bool Equals(object obj)
		{
			return obj is BitArray64 && ((BitArray64)obj).data == this.data;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x000183CF File Offset: 0x000165CF
		public override int GetHashCode()
		{
			return 1768953197 + this.data.GetHashCode();
		}

		// Token: 0x04000342 RID: 834
		[SerializeField]
		private ulong data;
	}
}
