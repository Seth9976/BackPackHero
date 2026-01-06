using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000BE RID: 190
	internal class DnsResourceRecordA : DnsResourceRecordIPAddress
	{
		// Token: 0x060003BB RID: 955 RVA: 0x0000B482 File Offset: 0x00009682
		internal DnsResourceRecordA(DnsResourceRecord rr)
			: base(rr, 4)
		{
		}
	}
}
