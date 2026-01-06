using System;

namespace TwitchLib.PubSub.Events
{
	// Token: 0x02000051 RID: 81
	public class OnUntimeoutArgs : EventArgs
	{
		// Token: 0x04000186 RID: 390
		public string UntimeoutedUser;

		// Token: 0x04000187 RID: 391
		public string UntimeoutedUserId;

		// Token: 0x04000188 RID: 392
		public string UntimeoutedBy;

		// Token: 0x04000189 RID: 393
		public string UntimeoutedByUserId;

		// Token: 0x0400018A RID: 394
		public string ChannelId;
	}
}
