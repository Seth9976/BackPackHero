using System;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Events
{
	// Token: 0x02000044 RID: 68
	public class OnPrimePaidSubscriberArgs : EventArgs
	{
		// Token: 0x04000093 RID: 147
		public PrimePaidSubscriber PrimePaidSubscriber;

		// Token: 0x04000094 RID: 148
		public string Channel;
	}
}
