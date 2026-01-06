using System;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Events
{
	// Token: 0x02000029 RID: 41
	public class OnChannelStateChangedArgs : EventArgs
	{
		// Token: 0x04000062 RID: 98
		public ChannelState ChannelState;

		// Token: 0x04000063 RID: 99
		public string Channel;
	}
}
