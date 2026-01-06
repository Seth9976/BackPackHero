using System;

namespace TwitchLib.PubSub.Events
{
	// Token: 0x0200004F RID: 79
	public class OnTimeoutArgs : EventArgs
	{
		// Token: 0x0400017A RID: 378
		public string TimedoutUserId;

		// Token: 0x0400017B RID: 379
		public string TimedoutUser;

		// Token: 0x0400017C RID: 380
		public TimeSpan TimeoutDuration;

		// Token: 0x0400017D RID: 381
		public string TimeoutReason;

		// Token: 0x0400017E RID: 382
		public string TimedoutBy;

		// Token: 0x0400017F RID: 383
		public string TimedoutById;

		// Token: 0x04000180 RID: 384
		public string ChannelId;
	}
}
