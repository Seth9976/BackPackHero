using System;

namespace TwitchLib.Client.Events
{
	// Token: 0x0200003C RID: 60
	public class OnMessageClearedArgs : EventArgs
	{
		// Token: 0x04000083 RID: 131
		public string Channel;

		// Token: 0x04000084 RID: 132
		public string Message;

		// Token: 0x04000085 RID: 133
		public string TargetMessageId;

		// Token: 0x04000086 RID: 134
		public string TmiSentTs;
	}
}
