using System;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Events
{
	// Token: 0x0200002F RID: 47
	public class OnConnectionErrorArgs : EventArgs
	{
		// Token: 0x0400006B RID: 107
		public ErrorEvent Error;

		// Token: 0x0400006C RID: 108
		public string BotUsername;
	}
}
