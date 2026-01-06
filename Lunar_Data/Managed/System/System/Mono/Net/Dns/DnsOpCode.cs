using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000B6 RID: 182
	internal enum DnsOpCode : byte
	{
		// Token: 0x040002A7 RID: 679
		Query,
		// Token: 0x040002A8 RID: 680
		[Obsolete]
		IQuery,
		// Token: 0x040002A9 RID: 681
		Status,
		// Token: 0x040002AA RID: 682
		Notify = 4,
		// Token: 0x040002AB RID: 683
		Update
	}
}
