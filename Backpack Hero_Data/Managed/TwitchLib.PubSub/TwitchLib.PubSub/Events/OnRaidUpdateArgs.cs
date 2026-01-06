using System;

namespace TwitchLib.PubSub.Events
{
	// Token: 0x02000048 RID: 72
	public class OnRaidUpdateArgs : EventArgs
	{
		// Token: 0x04000158 RID: 344
		public string ChannelId;

		// Token: 0x04000159 RID: 345
		public Guid Id;

		// Token: 0x0400015A RID: 346
		public string TargetChannelId;

		// Token: 0x0400015B RID: 347
		public DateTime AnnounceTime;

		// Token: 0x0400015C RID: 348
		public DateTime RaidTime;

		// Token: 0x0400015D RID: 349
		public int RemainingDurationSeconds;

		// Token: 0x0400015E RID: 350
		public int ViewerCount;
	}
}
