using System;
using TwitchLib.PubSub.Models.Responses.Messages;

namespace TwitchLib.PubSub.Events
{
	// Token: 0x02000035 RID: 53
	public class OnChannelSubscriptionArgs : EventArgs
	{
		// Token: 0x04000110 RID: 272
		public ChannelSubscription Subscription;

		// Token: 0x04000111 RID: 273
		public string ChannelId;
	}
}
