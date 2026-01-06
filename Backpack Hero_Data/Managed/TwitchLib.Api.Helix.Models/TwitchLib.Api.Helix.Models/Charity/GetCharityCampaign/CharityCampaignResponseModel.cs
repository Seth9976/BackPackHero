using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Charity.GetCharityCampaign
{
	// Token: 0x020000B0 RID: 176
	public class CharityCampaignResponseModel
	{
		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x00005223 File Offset: 0x00003423
		// (set) Token: 0x060005BF RID: 1471 RVA: 0x0000522B File Offset: 0x0000342B
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x00005234 File Offset: 0x00003434
		// (set) Token: 0x060005C1 RID: 1473 RVA: 0x0000523C File Offset: 0x0000343C
		[JsonProperty(PropertyName = "broadcaster_id")]
		public string BroadcasterId { get; protected set; }

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x00005245 File Offset: 0x00003445
		// (set) Token: 0x060005C3 RID: 1475 RVA: 0x0000524D File Offset: 0x0000344D
		[JsonProperty(PropertyName = "broadcaster_login")]
		public string BroadcasterLogin { get; protected set; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x00005256 File Offset: 0x00003456
		// (set) Token: 0x060005C5 RID: 1477 RVA: 0x0000525E File Offset: 0x0000345E
		[JsonProperty(PropertyName = "broadcaster_name")]
		public string BroadcasterName { get; protected set; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x00005267 File Offset: 0x00003467
		// (set) Token: 0x060005C7 RID: 1479 RVA: 0x0000526F File Offset: 0x0000346F
		[JsonProperty(PropertyName = "charity_name")]
		public string CharityName { get; protected set; }

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x00005278 File Offset: 0x00003478
		// (set) Token: 0x060005C9 RID: 1481 RVA: 0x00005280 File Offset: 0x00003480
		[JsonProperty(PropertyName = "charity_description")]
		public string CharityDescription { get; protected set; }

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x00005289 File Offset: 0x00003489
		// (set) Token: 0x060005CB RID: 1483 RVA: 0x00005291 File Offset: 0x00003491
		[JsonProperty(PropertyName = "charity_logo")]
		public string CharityLogo { get; protected set; }

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x0000529A File Offset: 0x0000349A
		// (set) Token: 0x060005CD RID: 1485 RVA: 0x000052A2 File Offset: 0x000034A2
		[JsonProperty(PropertyName = "charity_website")]
		public string CharityWebsite { get; protected set; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x000052AB File Offset: 0x000034AB
		// (set) Token: 0x060005CF RID: 1487 RVA: 0x000052B3 File Offset: 0x000034B3
		[JsonProperty(PropertyName = "current_amount")]
		public Amount CurrentAmount { get; protected set; }

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x000052BC File Offset: 0x000034BC
		// (set) Token: 0x060005D1 RID: 1489 RVA: 0x000052C4 File Offset: 0x000034C4
		[JsonProperty(PropertyName = "target_amount")]
		public Amount TargetAmount { get; protected set; }
	}
}
