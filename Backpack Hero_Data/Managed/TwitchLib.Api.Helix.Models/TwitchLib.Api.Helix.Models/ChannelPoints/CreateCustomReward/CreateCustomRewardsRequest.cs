using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.ChannelPoints.CreateCustomReward
{
	// Token: 0x020000C7 RID: 199
	public class CreateCustomRewardsRequest
	{
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x00005975 File Offset: 0x00003B75
		// (set) Token: 0x0600069D RID: 1693 RVA: 0x0000597D File Offset: 0x00003B7D
		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x00005986 File Offset: 0x00003B86
		// (set) Token: 0x0600069F RID: 1695 RVA: 0x0000598E File Offset: 0x00003B8E
		[JsonProperty(PropertyName = "prompt")]
		public string Prompt { get; set; }

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x00005997 File Offset: 0x00003B97
		// (set) Token: 0x060006A1 RID: 1697 RVA: 0x0000599F File Offset: 0x00003B9F
		[JsonProperty(PropertyName = "cost")]
		public int Cost { get; set; }

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x000059A8 File Offset: 0x00003BA8
		// (set) Token: 0x060006A3 RID: 1699 RVA: 0x000059B0 File Offset: 0x00003BB0
		[JsonProperty(PropertyName = "is_enabled")]
		public bool IsEnabled { get; set; }

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x000059B9 File Offset: 0x00003BB9
		// (set) Token: 0x060006A5 RID: 1701 RVA: 0x000059C1 File Offset: 0x00003BC1
		[JsonProperty(PropertyName = "background_color")]
		public string BackgroundColor { get; set; }

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x000059CA File Offset: 0x00003BCA
		// (set) Token: 0x060006A7 RID: 1703 RVA: 0x000059D2 File Offset: 0x00003BD2
		[JsonProperty(PropertyName = "is_user_input_required")]
		public bool IsUserInputRequired { get; set; }

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x000059DB File Offset: 0x00003BDB
		// (set) Token: 0x060006A9 RID: 1705 RVA: 0x000059E3 File Offset: 0x00003BE3
		[JsonProperty(PropertyName = "is_max_per_stream-Enabled")]
		public bool IsMaxPerStreamEnabled { get; set; }

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x000059EC File Offset: 0x00003BEC
		// (set) Token: 0x060006AB RID: 1707 RVA: 0x000059F4 File Offset: 0x00003BF4
		[JsonProperty(PropertyName = "max_per_stream")]
		public int? MaxPerStream { get; set; }

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x000059FD File Offset: 0x00003BFD
		// (set) Token: 0x060006AD RID: 1709 RVA: 0x00005A05 File Offset: 0x00003C05
		[JsonProperty(PropertyName = "is_max_per_user_per_stream_enabled")]
		public bool IsMaxPerUserPerStreamEnabled { get; set; }

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x00005A0E File Offset: 0x00003C0E
		// (set) Token: 0x060006AF RID: 1711 RVA: 0x00005A16 File Offset: 0x00003C16
		[JsonProperty(PropertyName = "max_per_user_per_stream")]
		public int? MaxPerUserPerStream { get; set; }

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x00005A1F File Offset: 0x00003C1F
		// (set) Token: 0x060006B1 RID: 1713 RVA: 0x00005A27 File Offset: 0x00003C27
		[JsonProperty(PropertyName = "is_global_cooldown_enabled")]
		public bool IsGlobalCooldownEnabled { get; set; }

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x00005A30 File Offset: 0x00003C30
		// (set) Token: 0x060006B3 RID: 1715 RVA: 0x00005A38 File Offset: 0x00003C38
		[JsonProperty(PropertyName = "global_cooldown_seconds")]
		public int? GlobalCooldownSeconds { get; set; }

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00005A41 File Offset: 0x00003C41
		// (set) Token: 0x060006B5 RID: 1717 RVA: 0x00005A49 File Offset: 0x00003C49
		[JsonProperty(PropertyName = "should_redemptions_skip_request_queue")]
		public bool ShouldRedemptionsSkipRequestQueue { get; set; }
	}
}
