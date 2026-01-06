using System;

namespace TwitchLib.PubSub.Events
{
	// Token: 0x02000052 RID: 82
	public class OnViewCountArgs : EventArgs
	{
		// Token: 0x0400018B RID: 395
		public string ServerTime;

		// Token: 0x0400018C RID: 396
		public int Viewers;

		// Token: 0x0400018D RID: 397
		public string ChannelId;
	}
}
