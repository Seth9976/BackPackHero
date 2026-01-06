using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Extensions.Transactions
{
	// Token: 0x02000079 RID: 121
	public class ProductData
	{
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060003DA RID: 986 RVA: 0x000041DC File Offset: 0x000023DC
		// (set) Token: 0x060003DB RID: 987 RVA: 0x000041E4 File Offset: 0x000023E4
		[JsonProperty(PropertyName = "domain")]
		public string Domain { get; protected set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060003DC RID: 988 RVA: 0x000041ED File Offset: 0x000023ED
		// (set) Token: 0x060003DD RID: 989 RVA: 0x000041F5 File Offset: 0x000023F5
		[JsonProperty(PropertyName = "sku")]
		public string SKU { get; protected set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060003DE RID: 990 RVA: 0x000041FE File Offset: 0x000023FE
		// (set) Token: 0x060003DF RID: 991 RVA: 0x00004206 File Offset: 0x00002406
		[JsonProperty(PropertyName = "cost")]
		public Cost Cost { get; protected set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000420F File Offset: 0x0000240F
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x00004217 File Offset: 0x00002417
		[JsonProperty(PropertyName = "inDevelopment")]
		public bool InDevelopment { get; protected set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00004220 File Offset: 0x00002420
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x00004228 File Offset: 0x00002428
		[JsonProperty(PropertyName = "displayName")]
		public string DisplayName { get; protected set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00004231 File Offset: 0x00002431
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x00004239 File Offset: 0x00002439
		[JsonProperty(PropertyName = "expiration")]
		public string Expiration { get; protected set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x00004242 File Offset: 0x00002442
		// (set) Token: 0x060003E7 RID: 999 RVA: 0x0000424A File Offset: 0x0000244A
		[JsonProperty(PropertyName = "broadcast")]
		public bool Broadcast { get; protected set; }
	}
}
