using System;

namespace TwitchLib.PubSub.Events
{
	// Token: 0x0200003D RID: 61
	public class OnFollowArgs : EventArgs
	{
		// Token: 0x0400012C RID: 300
		public string FollowedChannelId;

		// Token: 0x0400012D RID: 301
		public string DisplayName;

		// Token: 0x0400012E RID: 302
		public string Username;

		// Token: 0x0400012F RID: 303
		public string UserId;
	}
}
