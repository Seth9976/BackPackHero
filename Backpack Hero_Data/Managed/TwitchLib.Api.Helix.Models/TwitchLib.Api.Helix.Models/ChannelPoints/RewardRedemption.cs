using System;
using Newtonsoft.Json;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Helix.Models.ChannelPoints
{
	// Token: 0x020000C0 RID: 192
	public class RewardRedemption
	{
		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x0000571D File Offset: 0x0000391D
		// (set) Token: 0x06000656 RID: 1622 RVA: 0x00005725 File Offset: 0x00003925
		[JsonProperty(PropertyName = "broadcaster_id")]
		public string BroadcasterId { get; protected set; }

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x0000572E File Offset: 0x0000392E
		// (set) Token: 0x06000658 RID: 1624 RVA: 0x00005736 File Offset: 0x00003936
		[JsonProperty(PropertyName = "broadcaster_login")]
		public string BroadcasterLogin { get; protected set; }

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x0000573F File Offset: 0x0000393F
		// (set) Token: 0x0600065A RID: 1626 RVA: 0x00005747 File Offset: 0x00003947
		[JsonProperty(PropertyName = "broadcaster_name")]
		public string BroadcasterName { get; protected set; }

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x00005750 File Offset: 0x00003950
		// (set) Token: 0x0600065C RID: 1628 RVA: 0x00005758 File Offset: 0x00003958
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x00005761 File Offset: 0x00003961
		// (set) Token: 0x0600065E RID: 1630 RVA: 0x00005769 File Offset: 0x00003969
		[JsonProperty(PropertyName = "user_id")]
		public string UserId { get; protected set; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x00005772 File Offset: 0x00003972
		// (set) Token: 0x06000660 RID: 1632 RVA: 0x0000577A File Offset: 0x0000397A
		[JsonProperty(PropertyName = "user_login")]
		public string UserLogin { get; protected set; }

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x00005783 File Offset: 0x00003983
		// (set) Token: 0x06000662 RID: 1634 RVA: 0x0000578B File Offset: 0x0000398B
		[JsonProperty(PropertyName = "user_name")]
		public string UserName { get; protected set; }

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x00005794 File Offset: 0x00003994
		// (set) Token: 0x06000664 RID: 1636 RVA: 0x0000579C File Offset: 0x0000399C
		[JsonProperty(PropertyName = "user_input")]
		public string UserInput { get; protected set; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x000057A5 File Offset: 0x000039A5
		// (set) Token: 0x06000666 RID: 1638 RVA: 0x000057AD File Offset: 0x000039AD
		[JsonProperty(PropertyName = "status")]
		public CustomRewardRedemptionStatus Status { get; protected set; }

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x000057B6 File Offset: 0x000039B6
		// (set) Token: 0x06000668 RID: 1640 RVA: 0x000057BE File Offset: 0x000039BE
		[JsonProperty(PropertyName = "redeemed_at")]
		public DateTime RedeemedAt { get; protected set; }

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x000057C7 File Offset: 0x000039C7
		// (set) Token: 0x0600066A RID: 1642 RVA: 0x000057CF File Offset: 0x000039CF
		[JsonProperty(PropertyName = "reward")]
		public Reward Reward { get; protected set; }
	}
}
