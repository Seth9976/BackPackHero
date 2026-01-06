using System;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Events
{
	// Token: 0x02000042 RID: 66
	public class OnNewSubscriberArgs : EventArgs
	{
		// Token: 0x0400008F RID: 143
		public Subscriber Subscriber;

		// Token: 0x04000090 RID: 144
		public string Channel;
	}
}
