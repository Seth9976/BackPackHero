using System;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Events
{
	// Token: 0x0200002D RID: 45
	public class OnCommunitySubscriptionArgs : EventArgs
	{
		// Token: 0x04000067 RID: 103
		public CommunitySubscription GiftedSubscription;

		// Token: 0x04000068 RID: 104
		public string Channel;
	}
}
