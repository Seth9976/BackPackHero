using System;

namespace TwitchLib.PubSub.Events
{
	// Token: 0x02000038 RID: 56
	public class OnCustomRewardCreatedArgs : EventArgs
	{
		// Token: 0x04000117 RID: 279
		public DateTime TimeStamp;

		// Token: 0x04000118 RID: 280
		public string ChannelId;

		// Token: 0x04000119 RID: 281
		public Guid RewardId;

		// Token: 0x0400011A RID: 282
		public string RewardTitle;

		// Token: 0x0400011B RID: 283
		public string RewardPrompt;

		// Token: 0x0400011C RID: 284
		public int RewardCost;
	}
}
