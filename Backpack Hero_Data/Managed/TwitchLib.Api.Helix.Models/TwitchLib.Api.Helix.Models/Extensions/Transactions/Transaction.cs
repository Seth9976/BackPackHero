using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Extensions.Transactions
{
	// Token: 0x0200007B RID: 123
	public class Transaction
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x00004285 File Offset: 0x00002485
		// (set) Token: 0x060003EF RID: 1007 RVA: 0x0000428D File Offset: 0x0000248D
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00004296 File Offset: 0x00002496
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x0000429E File Offset: 0x0000249E
		[JsonProperty(PropertyName = "timestamp")]
		public DateTime Timestamp { get; protected set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x000042A7 File Offset: 0x000024A7
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x000042AF File Offset: 0x000024AF
		[JsonProperty(PropertyName = "broadcaster_id")]
		public string BroadcasterId { get; protected set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x000042B8 File Offset: 0x000024B8
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x000042C0 File Offset: 0x000024C0
		[JsonProperty(PropertyName = "broadcaster_login")]
		public string BroadcasterLogin { get; protected set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x000042C9 File Offset: 0x000024C9
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x000042D1 File Offset: 0x000024D1
		[JsonProperty(PropertyName = "broadcaster_name")]
		public string BroadcasterName { get; protected set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x000042DA File Offset: 0x000024DA
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x000042E2 File Offset: 0x000024E2
		[JsonProperty(PropertyName = "user_id")]
		public string UserId { get; protected set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x000042EB File Offset: 0x000024EB
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x000042F3 File Offset: 0x000024F3
		[JsonProperty(PropertyName = "user_login")]
		public string UserLogin { get; protected set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x000042FC File Offset: 0x000024FC
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x00004304 File Offset: 0x00002504
		[JsonProperty(PropertyName = "user_name")]
		public string UserName { get; protected set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0000430D File Offset: 0x0000250D
		// (set) Token: 0x060003FF RID: 1023 RVA: 0x00004315 File Offset: 0x00002515
		[JsonProperty(PropertyName = "product_type")]
		public string ProductType { get; protected set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000431E File Offset: 0x0000251E
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x00004326 File Offset: 0x00002526
		[JsonProperty(PropertyName = "product_data")]
		public ProductData ProductData { get; protected set; }
	}
}
