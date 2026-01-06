using System;

namespace TwitchLib.PubSub.Events
{
	// Token: 0x02000030 RID: 48
	public class OnBanArgs : EventArgs
	{
		// Token: 0x040000F3 RID: 243
		public string BannedUserId;

		// Token: 0x040000F4 RID: 244
		public string BannedUser;

		// Token: 0x040000F5 RID: 245
		public string BanReason;

		// Token: 0x040000F6 RID: 246
		public string BannedBy;

		// Token: 0x040000F7 RID: 247
		public string BannedByUserId;

		// Token: 0x040000F8 RID: 248
		public string ChannelId;
	}
}
