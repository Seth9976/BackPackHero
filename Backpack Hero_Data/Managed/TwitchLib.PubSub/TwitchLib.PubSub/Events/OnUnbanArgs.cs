using System;

namespace TwitchLib.PubSub.Events
{
	// Token: 0x02000050 RID: 80
	public class OnUnbanArgs : EventArgs
	{
		// Token: 0x04000181 RID: 385
		public string UnbannedUser;

		// Token: 0x04000182 RID: 386
		public string UnbannedUserId;

		// Token: 0x04000183 RID: 387
		public string UnbannedBy;

		// Token: 0x04000184 RID: 388
		public string UnbannedByUserId;

		// Token: 0x04000185 RID: 389
		public string ChannelId;
	}
}
