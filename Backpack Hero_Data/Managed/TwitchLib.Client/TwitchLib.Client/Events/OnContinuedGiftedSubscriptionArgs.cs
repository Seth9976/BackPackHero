using System;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Events
{
	// Token: 0x02000030 RID: 48
	public class OnContinuedGiftedSubscriptionArgs : EventArgs
	{
		// Token: 0x0400006D RID: 109
		public ContinuedGiftedSubscription ContinuedGiftedSubscription;

		// Token: 0x0400006E RID: 110
		public string Channel;
	}
}
