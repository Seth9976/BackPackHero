using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000B4 RID: 180
	internal enum DnsClass : ushort
	{
		// Token: 0x0400029C RID: 668
		Internet = 1,
		// Token: 0x0400029D RID: 669
		IN = 1,
		// Token: 0x0400029E RID: 670
		CSNET,
		// Token: 0x0400029F RID: 671
		CS = 2,
		// Token: 0x040002A0 RID: 672
		CHAOS,
		// Token: 0x040002A1 RID: 673
		CH = 3,
		// Token: 0x040002A2 RID: 674
		Hesiod,
		// Token: 0x040002A3 RID: 675
		HS = 4
	}
}
