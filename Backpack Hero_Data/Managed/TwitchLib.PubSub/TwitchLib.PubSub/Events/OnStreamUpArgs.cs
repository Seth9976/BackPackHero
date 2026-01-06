using System;

namespace TwitchLib.PubSub.Events
{
	// Token: 0x0200004C RID: 76
	public class OnStreamUpArgs : EventArgs
	{
		// Token: 0x04000173 RID: 371
		public string ServerTime;

		// Token: 0x04000174 RID: 372
		public int PlayDelay;

		// Token: 0x04000175 RID: 373
		public string ChannelId;
	}
}
