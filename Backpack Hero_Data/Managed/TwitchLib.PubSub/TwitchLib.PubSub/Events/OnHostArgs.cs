using System;

namespace TwitchLib.PubSub.Events
{
	// Token: 0x0200003E RID: 62
	public class OnHostArgs : EventArgs
	{
		// Token: 0x04000130 RID: 304
		public string Moderator;

		// Token: 0x04000131 RID: 305
		public string HostedChannel;

		// Token: 0x04000132 RID: 306
		public string ChannelId;
	}
}
