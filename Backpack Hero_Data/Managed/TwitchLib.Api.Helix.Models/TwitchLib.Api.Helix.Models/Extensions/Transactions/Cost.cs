using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Extensions.Transactions
{
	// Token: 0x0200007A RID: 122
	public class Cost
	{
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000425B File Offset: 0x0000245B
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x00004263 File Offset: 0x00002463
		[JsonProperty(PropertyName = "amount")]
		public int Amount { get; protected set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000426C File Offset: 0x0000246C
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x00004274 File Offset: 0x00002474
		[JsonProperty(PropertyName = "type")]
		public string Type { get; protected set; }
	}
}
