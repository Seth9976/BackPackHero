using System;
using System.Collections.Generic;

namespace TwitchLib.Client.Events
{
	// Token: 0x02000034 RID: 52
	public class OnExistingUsersDetectedArgs : EventArgs
	{
		// Token: 0x04000074 RID: 116
		public List<string> Users;

		// Token: 0x04000075 RID: 117
		public string Channel;
	}
}
