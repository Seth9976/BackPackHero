using System;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Extensions
{
	// Token: 0x0200000D RID: 13
	public static class EmoteOnlyExt
	{
		// Token: 0x060001B7 RID: 439 RVA: 0x000072D5 File Offset: 0x000054D5
		public static void EmoteOnlyOn(this ITwitchClient client, JoinedChannel channel)
		{
			client.SendMessage(channel, ".emoteonly", false);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000072E4 File Offset: 0x000054E4
		public static void EmoteOnlyOn(this ITwitchClient client, string channel)
		{
			client.SendMessage(channel, ".emoteonly", false);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000072F3 File Offset: 0x000054F3
		public static void EmoteOnlyOff(this ITwitchClient client, JoinedChannel channel)
		{
			client.SendMessage(channel, ".emoteonlyoff", false);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00007302 File Offset: 0x00005502
		public static void EmoteOnlyOff(this ITwitchClient client, string channel)
		{
			client.SendMessage(channel, ".emoteonlyoff", false);
		}
	}
}
