using System;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Extensions
{
	// Token: 0x0200000A RID: 10
	public static class ClearChatExt
	{
		// Token: 0x060001AF RID: 431 RVA: 0x00007117 File Offset: 0x00005317
		public static void ClearChat(this ITwitchClient client, JoinedChannel channel)
		{
			client.SendMessage(channel, ".clear", false);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00007126 File Offset: 0x00005326
		public static void ClearChat(this ITwitchClient client, string channel)
		{
			client.SendMessage(channel, ".clear", false);
		}
	}
}
