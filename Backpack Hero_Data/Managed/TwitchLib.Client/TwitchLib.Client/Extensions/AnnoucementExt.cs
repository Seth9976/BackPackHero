using System;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Extensions
{
	// Token: 0x02000007 RID: 7
	public static class AnnoucementExt
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x0000707A File Offset: 0x0000527A
		public static void Announce(this ITwitchClient client, JoinedChannel channel, string message)
		{
			client.SendMessage(channel, ".announce " + message, false);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000708F File Offset: 0x0000528F
		public static void Announce(this ITwitchClient client, string channel, string message)
		{
			client.SendMessage(channel, ".announce " + message, false);
		}
	}
}
