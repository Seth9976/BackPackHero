using System;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Events
{
	// Token: 0x02000023 RID: 35
	public class OnAnnouncementArgs : EventArgs
	{
		// Token: 0x04000056 RID: 86
		public Announcement Announcement;

		// Token: 0x04000057 RID: 87
		public string Channel;
	}
}
