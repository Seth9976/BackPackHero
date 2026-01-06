using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000BF RID: 191
	internal class DnsResourceRecordAAAA : DnsResourceRecordIPAddress
	{
		// Token: 0x060003BC RID: 956 RVA: 0x0000B48C File Offset: 0x0000968C
		internal DnsResourceRecordAAAA(DnsResourceRecord rr)
			: base(rr, 16)
		{
		}
	}
}
