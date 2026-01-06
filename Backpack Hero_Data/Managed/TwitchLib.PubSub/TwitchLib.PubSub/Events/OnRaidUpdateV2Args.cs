using System;

namespace TwitchLib.PubSub.Events
{
	// Token: 0x02000049 RID: 73
	public class OnRaidUpdateV2Args : EventArgs
	{
		// Token: 0x0400015F RID: 351
		public string ChannelId;

		// Token: 0x04000160 RID: 352
		public Guid Id;

		// Token: 0x04000161 RID: 353
		public string TargetChannelId;

		// Token: 0x04000162 RID: 354
		public string TargetLogin;

		// Token: 0x04000163 RID: 355
		public string TargetDisplayName;

		// Token: 0x04000164 RID: 356
		public string TargetProfileImage;

		// Token: 0x04000165 RID: 357
		public int ViewerCount;
	}
}
