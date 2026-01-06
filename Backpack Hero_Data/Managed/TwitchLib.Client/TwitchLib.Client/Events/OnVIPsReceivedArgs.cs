using System;
using System.Collections.Generic;

namespace TwitchLib.Client.Events
{
	// Token: 0x02000056 RID: 86
	public class OnVIPsReceivedArgs : EventArgs
	{
		// Token: 0x040000B5 RID: 181
		public string Channel;

		// Token: 0x040000B6 RID: 182
		public List<string> VIPs;
	}
}
