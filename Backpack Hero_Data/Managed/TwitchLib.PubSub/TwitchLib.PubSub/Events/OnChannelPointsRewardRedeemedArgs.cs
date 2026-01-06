using System;
using TwitchLib.PubSub.Models.Responses.Messages.Redemption;

namespace TwitchLib.PubSub.Events
{
	// Token: 0x02000034 RID: 52
	public class OnChannelPointsRewardRedeemedArgs : EventArgs
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000231 RID: 561 RVA: 0x000073E7 File Offset: 0x000055E7
		// (set) Token: 0x06000232 RID: 562 RVA: 0x000073EF File Offset: 0x000055EF
		public string ChannelId { get; internal set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000233 RID: 563 RVA: 0x000073F8 File Offset: 0x000055F8
		// (set) Token: 0x06000234 RID: 564 RVA: 0x00007400 File Offset: 0x00005600
		public RewardRedeemed RewardRedeemed { get; internal set; }
	}
}
