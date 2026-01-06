using System;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Events
{
	// Token: 0x02000046 RID: 70
	public class OnRaidNotificationArgs : EventArgs
	{
		// Token: 0x04000097 RID: 151
		public RaidNotification RaidNotification;

		// Token: 0x04000098 RID: 152
		public string Channel;
	}
}
