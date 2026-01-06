using System;

namespace UnityEngine
{
	// Token: 0x0200020F RID: 527
	public struct RangeInt
	{
		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001742 RID: 5954 RVA: 0x00025598 File Offset: 0x00023798
		public int end
		{
			get
			{
				return this.start + this.length;
			}
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x000255B7 File Offset: 0x000237B7
		public RangeInt(int start, int length)
		{
			this.start = start;
			this.length = length;
		}

		// Token: 0x040007F5 RID: 2037
		public int start;

		// Token: 0x040007F6 RID: 2038
		public int length;
	}
}
