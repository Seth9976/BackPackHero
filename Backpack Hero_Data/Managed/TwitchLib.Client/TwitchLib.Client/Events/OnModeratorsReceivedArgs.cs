using System;
using System.Collections.Generic;

namespace TwitchLib.Client.Events
{
	// Token: 0x02000041 RID: 65
	public class OnModeratorsReceivedArgs : EventArgs
	{
		// Token: 0x0400008D RID: 141
		public string Channel;

		// Token: 0x0400008E RID: 142
		public List<string> Moderators;
	}
}
