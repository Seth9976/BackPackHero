using System;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Events
{
	// Token: 0x02000037 RID: 55
	public class OnGiftedSubscriptionArgs : EventArgs
	{
		// Token: 0x04000079 RID: 121
		public GiftedSubscription GiftedSubscription;

		// Token: 0x0400007A RID: 122
		public string Channel;
	}
}
