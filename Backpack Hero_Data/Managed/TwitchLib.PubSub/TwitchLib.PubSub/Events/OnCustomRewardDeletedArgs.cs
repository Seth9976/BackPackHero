using System;

namespace TwitchLib.PubSub.Events
{
	// Token: 0x02000039 RID: 57
	public class OnCustomRewardDeletedArgs
	{
		// Token: 0x0400011D RID: 285
		public DateTime TimeStamp;

		// Token: 0x0400011E RID: 286
		public string ChannelId;

		// Token: 0x0400011F RID: 287
		public Guid RewardId;

		// Token: 0x04000120 RID: 288
		public string RewardTitle;

		// Token: 0x04000121 RID: 289
		public string RewardPrompt;
	}
}
