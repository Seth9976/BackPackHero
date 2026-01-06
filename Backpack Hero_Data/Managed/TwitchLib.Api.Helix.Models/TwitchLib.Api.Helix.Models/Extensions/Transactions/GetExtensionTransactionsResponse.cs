using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Extensions.Transactions
{
	// Token: 0x02000078 RID: 120
	public class GetExtensionTransactionsResponse
	{
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x000041B2 File Offset: 0x000023B2
		// (set) Token: 0x060003D6 RID: 982 RVA: 0x000041BA File Offset: 0x000023BA
		[JsonProperty(PropertyName = "data")]
		public Transaction[] Data { get; protected set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x000041C3 File Offset: 0x000023C3
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x000041CB File Offset: 0x000023CB
		[JsonProperty(PropertyName = "pagination")]
		public Pagination Pagination { get; protected set; }
	}
}
