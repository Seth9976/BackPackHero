using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.ChatSettings
{
	// Token: 0x020000A6 RID: 166
	public class ChatSettings
	{
		// Token: 0x04000279 RID: 633
		[JsonProperty(PropertyName = "slow_mode")]
		public bool SlowMode;

		// Token: 0x0400027A RID: 634
		[JsonProperty(PropertyName = "slow_mode_wait_time")]
		public int? SlowModeWaitTime;

		// Token: 0x0400027B RID: 635
		[JsonProperty(PropertyName = "follower_mode")]
		public bool FollowerMode;

		// Token: 0x0400027C RID: 636
		[JsonProperty(PropertyName = "follower_mode_duration")]
		public int? FollowerModeDuration;

		// Token: 0x0400027D RID: 637
		[JsonProperty(PropertyName = "subscriber_mode")]
		public bool SubscriberMode;

		// Token: 0x0400027E RID: 638
		[JsonProperty(PropertyName = "emote_mode")]
		public bool EmoteMode;

		// Token: 0x0400027F RID: 639
		[JsonProperty(PropertyName = "unique_chat_mode")]
		public bool UniqueChatMode;

		// Token: 0x04000280 RID: 640
		[JsonProperty(PropertyName = "non_moderator_chat_delay")]
		public bool NonModeratorChatDelay;

		// Token: 0x04000281 RID: 641
		[JsonProperty(PropertyName = "non_moderator_chat_delay_duration")]
		public int? NonModeratorChatDelayDuration;
	}
}
