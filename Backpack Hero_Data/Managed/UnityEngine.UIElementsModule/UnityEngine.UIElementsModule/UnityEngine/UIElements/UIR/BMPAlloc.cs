using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000329 RID: 809
	internal struct BMPAlloc
	{
		// Token: 0x06001A0B RID: 6667 RVA: 0x0006EBF4 File Offset: 0x0006CDF4
		public bool Equals(BMPAlloc other)
		{
			return this.page == other.page && this.pageLine == other.pageLine && this.bitIndex == other.bitIndex;
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x0006EC34 File Offset: 0x0006CE34
		public bool IsValid()
		{
			return this.page >= 0;
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x0006EC54 File Offset: 0x0006CE54
		public override string ToString()
		{
			return string.Format("{0},{1},{2}", this.page, this.pageLine, this.bitIndex);
		}

		// Token: 0x04000BED RID: 3053
		public static readonly BMPAlloc Invalid = new BMPAlloc
		{
			page = -1
		};

		// Token: 0x04000BEE RID: 3054
		public int page;

		// Token: 0x04000BEF RID: 3055
		public ushort pageLine;

		// Token: 0x04000BF0 RID: 3056
		public byte bitIndex;

		// Token: 0x04000BF1 RID: 3057
		public OwnedState ownedState;
	}
}
