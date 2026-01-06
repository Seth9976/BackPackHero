using System;

namespace TwitchLib.Client.Events
{
	// Token: 0x02000059 RID: 89
	public class OnWhisperSentArgs : EventArgs
	{
		// Token: 0x040000B9 RID: 185
		public string Username;

		// Token: 0x040000BA RID: 186
		public string Receiver;

		// Token: 0x040000BB RID: 187
		public string Message;
	}
}
