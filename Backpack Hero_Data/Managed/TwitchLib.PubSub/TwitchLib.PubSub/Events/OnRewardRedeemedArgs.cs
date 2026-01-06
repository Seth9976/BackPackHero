using System;

namespace TwitchLib.PubSub.Events
{
	// Token: 0x0200004A RID: 74
	public class OnRewardRedeemedArgs : EventArgs
	{
		// Token: 0x04000166 RID: 358
		public DateTime TimeStamp;

		// Token: 0x04000167 RID: 359
		public string ChannelId;

		// Token: 0x04000168 RID: 360
		public string Login;

		// Token: 0x04000169 RID: 361
		public string DisplayName;

		// Token: 0x0400016A RID: 362
		public string Message;

		// Token: 0x0400016B RID: 363
		public Guid RewardId;

		// Token: 0x0400016C RID: 364
		public string RewardTitle;

		// Token: 0x0400016D RID: 365
		public string RewardPrompt;

		// Token: 0x0400016E RID: 366
		public int RewardCost;

		// Token: 0x0400016F RID: 367
		public string Status;

		// Token: 0x04000170 RID: 368
		public Guid RedemptionId;
	}
}
