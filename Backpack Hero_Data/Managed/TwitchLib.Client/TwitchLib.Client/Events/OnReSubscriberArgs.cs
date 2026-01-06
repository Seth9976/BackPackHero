using System;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Events
{
	// Token: 0x0200004A RID: 74
	public class OnReSubscriberArgs : EventArgs
	{
		// Token: 0x0400009F RID: 159
		public ReSubscriber ReSubscriber;

		// Token: 0x040000A0 RID: 160
		public string Channel;
	}
}
