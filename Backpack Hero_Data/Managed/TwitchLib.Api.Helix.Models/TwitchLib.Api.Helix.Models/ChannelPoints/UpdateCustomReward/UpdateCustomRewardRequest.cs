using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.ChannelPoints.UpdateCustomReward
{
	// Token: 0x020000C3 RID: 195
	[JsonObject(ItemNullValueHandling = 1)]
	public class UpdateCustomRewardRequest
	{
		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x00005812 File Offset: 0x00003A12
		// (set) Token: 0x06000673 RID: 1651 RVA: 0x0000581A File Offset: 0x00003A1A
		[JsonProperty(PropertyName = "broadcaster_id")]
		public string BroadcasterId { get; set; }

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x00005823 File Offset: 0x00003A23
		// (set) Token: 0x06000675 RID: 1653 RVA: 0x0000582B File Offset: 0x00003A2B
		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x00005834 File Offset: 0x00003A34
		// (set) Token: 0x06000677 RID: 1655 RVA: 0x0000583C File Offset: 0x00003A3C
		[JsonProperty(PropertyName = "prompt")]
		public string Prompt { get; set; }

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x00005845 File Offset: 0x00003A45
		// (set) Token: 0x06000679 RID: 1657 RVA: 0x0000584D File Offset: 0x00003A4D
		[JsonProperty(PropertyName = "cost")]
		public int? Cost { get; set; }

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x00005856 File Offset: 0x00003A56
		// (set) Token: 0x0600067B RID: 1659 RVA: 0x0000585E File Offset: 0x00003A5E
		[JsonProperty(PropertyName = "is_enabled")]
		public bool? IsEnabled { get; set; }

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x00005867 File Offset: 0x00003A67
		// (set) Token: 0x0600067D RID: 1661 RVA: 0x0000586F File Offset: 0x00003A6F
		[JsonProperty(PropertyName = "background_color")]
		public string BackgroundColor { get; set; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x00005878 File Offset: 0x00003A78
		// (set) Token: 0x0600067F RID: 1663 RVA: 0x00005880 File Offset: 0x00003A80
		[JsonProperty(PropertyName = "is_user_input_required")]
		public bool? IsUserInputRequired { get; set; }

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x00005889 File Offset: 0x00003A89
		// (set) Token: 0x06000681 RID: 1665 RVA: 0x00005891 File Offset: 0x00003A91
		[JsonProperty(PropertyName = "is_max_per_stream_enabled")]
		public bool? IsMaxPerStreamEnabled { get; set; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x0000589A File Offset: 0x00003A9A
		// (set) Token: 0x06000683 RID: 1667 RVA: 0x000058A2 File Offset: 0x00003AA2
		[JsonProperty(PropertyName = "max_per_stream")]
		public int? MaxPerStream { get; set; }

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x000058AB File Offset: 0x00003AAB
		// (set) Token: 0x06000685 RID: 1669 RVA: 0x000058B3 File Offset: 0x00003AB3
		[JsonProperty(PropertyName = "is_max_per_user_per_stream_enabled")]
		public bool? IsMaxPerUserPerStreamEnabled { get; set; }

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x000058BC File Offset: 0x00003ABC
		// (set) Token: 0x06000687 RID: 1671 RVA: 0x000058C4 File Offset: 0x00003AC4
		[JsonProperty(PropertyName = "max_per_user_per_stream")]
		public int? MaxPerUserPerStream { get; set; }

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x000058CD File Offset: 0x00003ACD
		// (set) Token: 0x06000689 RID: 1673 RVA: 0x000058D5 File Offset: 0x00003AD5
		[JsonProperty(PropertyName = "is_global_cooldown_enabled")]
		public bool? IsGlobalCooldownEnabled { get; set; }

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x000058DE File Offset: 0x00003ADE
		// (set) Token: 0x0600068B RID: 1675 RVA: 0x000058E6 File Offset: 0x00003AE6
		[JsonProperty(PropertyName = "global_cooldown_seconds")]
		public int? GlobalCooldownSeconds { get; set; }

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x000058EF File Offset: 0x00003AEF
		// (set) Token: 0x0600068D RID: 1677 RVA: 0x000058F7 File Offset: 0x00003AF7
		[JsonProperty(PropertyName = "is_paused")]
		public bool? IsPaused { get; set; }

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x00005900 File Offset: 0x00003B00
		// (set) Token: 0x0600068F RID: 1679 RVA: 0x00005908 File Offset: 0x00003B08
		[JsonProperty(PropertyName = "should_redemptions_skip_request_queue")]
		public bool? ShouldRedemptionsSkipRequestQueue { get; set; }
	}
}
