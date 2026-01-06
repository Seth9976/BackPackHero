using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.ChannelPoints
{
	// Token: 0x020000B9 RID: 185
	public class CustomReward
	{
		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x0000549C File Offset: 0x0000369C
		// (set) Token: 0x0600060A RID: 1546 RVA: 0x000054A4 File Offset: 0x000036A4
		[JsonProperty(PropertyName = "broadcaster_id")]
		public string BroadcasterId { get; protected set; }

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x000054AD File Offset: 0x000036AD
		// (set) Token: 0x0600060C RID: 1548 RVA: 0x000054B5 File Offset: 0x000036B5
		[JsonProperty(PropertyName = "broadcaster_login")]
		public string BroadcasterLogin { get; protected set; }

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x000054BE File Offset: 0x000036BE
		// (set) Token: 0x0600060E RID: 1550 RVA: 0x000054C6 File Offset: 0x000036C6
		[JsonProperty(PropertyName = "broadcaster_name")]
		public string BroadcasterName { get; protected set; }

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x000054CF File Offset: 0x000036CF
		// (set) Token: 0x06000610 RID: 1552 RVA: 0x000054D7 File Offset: 0x000036D7
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x000054E0 File Offset: 0x000036E0
		// (set) Token: 0x06000612 RID: 1554 RVA: 0x000054E8 File Offset: 0x000036E8
		[JsonProperty(PropertyName = "title")]
		public string Title { get; protected set; }

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x000054F1 File Offset: 0x000036F1
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x000054F9 File Offset: 0x000036F9
		[JsonProperty(PropertyName = "prompt")]
		public string Prompt { get; protected set; }

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x00005502 File Offset: 0x00003702
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x0000550A File Offset: 0x0000370A
		[JsonProperty(PropertyName = "cost")]
		public int Cost { get; protected set; }

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x00005513 File Offset: 0x00003713
		// (set) Token: 0x06000618 RID: 1560 RVA: 0x0000551B File Offset: 0x0000371B
		[JsonProperty(PropertyName = "image")]
		public Image Image { get; protected set; }

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x00005524 File Offset: 0x00003724
		// (set) Token: 0x0600061A RID: 1562 RVA: 0x0000552C File Offset: 0x0000372C
		[JsonProperty(PropertyName = "default_image")]
		public DefaultImage DefaultImage { get; protected set; }

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x00005535 File Offset: 0x00003735
		// (set) Token: 0x0600061C RID: 1564 RVA: 0x0000553D File Offset: 0x0000373D
		[JsonProperty(PropertyName = "background_color")]
		public string BackgroundColor { get; protected set; }

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00005546 File Offset: 0x00003746
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x0000554E File Offset: 0x0000374E
		[JsonProperty(PropertyName = "is_enabled")]
		public bool IsEnabled { get; protected set; }

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x00005557 File Offset: 0x00003757
		// (set) Token: 0x06000620 RID: 1568 RVA: 0x0000555F File Offset: 0x0000375F
		[JsonProperty(PropertyName = "is_user_input_required")]
		public bool IsUserInputRequired { get; protected set; }

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x00005568 File Offset: 0x00003768
		// (set) Token: 0x06000622 RID: 1570 RVA: 0x00005570 File Offset: 0x00003770
		[JsonProperty(PropertyName = "max_per_stream_setting")]
		public MaxPerStreamSetting MaxPerStreamSetting { get; protected set; }

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x00005579 File Offset: 0x00003779
		// (set) Token: 0x06000624 RID: 1572 RVA: 0x00005581 File Offset: 0x00003781
		[JsonProperty(PropertyName = "max_per_user_per_stream_setting")]
		public MaxPerUserPerStreamSetting MaxPerUserPerStreamSetting { get; protected set; }

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x0000558A File Offset: 0x0000378A
		// (set) Token: 0x06000626 RID: 1574 RVA: 0x00005592 File Offset: 0x00003792
		[JsonProperty(PropertyName = "global_cooldown_setting")]
		public GlobalCooldownSetting GlobalCooldownSetting { get; protected set; }

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x0000559B File Offset: 0x0000379B
		// (set) Token: 0x06000628 RID: 1576 RVA: 0x000055A3 File Offset: 0x000037A3
		[JsonProperty(PropertyName = "is_paused")]
		public bool IsPaused { get; protected set; }

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x000055AC File Offset: 0x000037AC
		// (set) Token: 0x0600062A RID: 1578 RVA: 0x000055B4 File Offset: 0x000037B4
		[JsonProperty(PropertyName = "is_in_stock")]
		public bool IsInStock { get; protected set; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x000055BD File Offset: 0x000037BD
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x000055C5 File Offset: 0x000037C5
		[JsonProperty(PropertyName = "should_redemptions_skip_request_queue")]
		public bool ShouldRedemptionsSkipQueue { get; protected set; }

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x000055CE File Offset: 0x000037CE
		// (set) Token: 0x0600062E RID: 1582 RVA: 0x000055D6 File Offset: 0x000037D6
		[JsonProperty(PropertyName = "redemptions_redeemed_current_stream")]
		public int? RedemptionsRedeemedCurrentStream { get; protected set; }

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x000055DF File Offset: 0x000037DF
		// (set) Token: 0x06000630 RID: 1584 RVA: 0x000055E7 File Offset: 0x000037E7
		[JsonProperty(PropertyName = "cooldown_expires_at")]
		public string CooldownExpiresAt { get; protected set; }
	}
}
