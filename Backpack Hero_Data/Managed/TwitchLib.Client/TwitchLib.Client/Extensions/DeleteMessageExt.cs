using System;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Extensions
{
	// Token: 0x0200000C RID: 12
	public static class DeleteMessageExt
	{
		// Token: 0x060001B3 RID: 435 RVA: 0x00007277 File Offset: 0x00005477
		public static void DeleteMessage(this ITwitchClient client, JoinedChannel channel, string messageId)
		{
			client.SendMessage(channel, ".delete " + messageId, false);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000728C File Offset: 0x0000548C
		public static void DeleteMessage(this ITwitchClient client, string channel, string messageId)
		{
			client.SendMessage(channel, ".delete " + messageId, false);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000072A1 File Offset: 0x000054A1
		public static void DeleteMessage(this ITwitchClient client, JoinedChannel channel, ChatMessage msg)
		{
			client.SendMessage(channel, ".delete " + msg.Id, false);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000072BB File Offset: 0x000054BB
		public static void DeleteMessage(this ITwitchClient client, string channel, ChatMessage msg)
		{
			client.SendMessage(channel, ".delete " + msg.Id, false);
		}
	}
}
