using System;

namespace TwitchLib.PubSub.Events
{
	// Token: 0x02000037 RID: 55
	public class OnCommercialArgs : EventArgs
	{
		// Token: 0x04000114 RID: 276
		public int Length;

		// Token: 0x04000115 RID: 277
		public string ServerTime;

		// Token: 0x04000116 RID: 278
		public string ChannelId;
	}
}
