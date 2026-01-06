using System;

namespace TwitchLib.PubSub.Events
{
	// Token: 0x0200003A RID: 58
	public class OnCustomRewardUpdatedArgs : EventArgs
	{
		// Token: 0x04000122 RID: 290
		public DateTime TimeStamp;

		// Token: 0x04000123 RID: 291
		public string ChannelId;

		// Token: 0x04000124 RID: 292
		public Guid RewardId;

		// Token: 0x04000125 RID: 293
		public string RewardTitle;

		// Token: 0x04000126 RID: 294
		public string RewardPrompt;

		// Token: 0x04000127 RID: 295
		public int RewardCost;
	}
}
