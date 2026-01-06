using System;

namespace TwitchLib.PubSub.Events
{
	// Token: 0x02000031 RID: 49
	public class OnBitsReceivedArgs : EventArgs
	{
		// Token: 0x040000F9 RID: 249
		public string Username;

		// Token: 0x040000FA RID: 250
		public string ChannelName;

		// Token: 0x040000FB RID: 251
		public string UserId;

		// Token: 0x040000FC RID: 252
		public string ChannelId;

		// Token: 0x040000FD RID: 253
		public string Time;

		// Token: 0x040000FE RID: 254
		public string ChatMessage;

		// Token: 0x040000FF RID: 255
		public int BitsUsed;

		// Token: 0x04000100 RID: 256
		public int TotalBitsUsed;

		// Token: 0x04000101 RID: 257
		public string Context;
	}
}
