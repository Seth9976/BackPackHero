using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000501 RID: 1281
	[Flags]
	internal enum StartIPOptions
	{
		// Token: 0x0400185B RID: 6235
		Both = 3,
		// Token: 0x0400185C RID: 6236
		None = 0,
		// Token: 0x0400185D RID: 6237
		StartIPv4 = 1,
		// Token: 0x0400185E RID: 6238
		StartIPv6 = 2
	}
}
