using System;
using System.Collections.Generic;

namespace TwitchLib.PubSub.Events
{
	// Token: 0x02000033 RID: 51
	public class OnChannelExtensionBroadcastArgs : EventArgs
	{
		// Token: 0x0400010C RID: 268
		public List<string> Messages;

		// Token: 0x0400010D RID: 269
		public string ChannelId;
	}
}
