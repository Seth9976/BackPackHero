using System;

namespace TwitchLib.PubSub.Events
{
	// Token: 0x02000047 RID: 71
	public class OnRaidGoArgs : EventArgs
	{
		// Token: 0x04000151 RID: 337
		public string ChannelId;

		// Token: 0x04000152 RID: 338
		public Guid Id;

		// Token: 0x04000153 RID: 339
		public string TargetChannelId;

		// Token: 0x04000154 RID: 340
		public string TargetLogin;

		// Token: 0x04000155 RID: 341
		public string TargetDisplayName;

		// Token: 0x04000156 RID: 342
		public string TargetProfileImage;

		// Token: 0x04000157 RID: 343
		public int ViewerCount;
	}
}
