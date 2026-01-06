using System;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Extensions
{
	// Token: 0x02000010 RID: 16
	public static class GetChannelModeratorsExt
	{
		// Token: 0x060001DC RID: 476 RVA: 0x00007B08 File Offset: 0x00005D08
		public static void GetChannelModerators(this ITwitchClient client, JoinedChannel channel)
		{
			client.SendMessage(channel, ".mods", false);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00007B17 File Offset: 0x00005D17
		public static void GetChannelModerators(this ITwitchClient client, string channel)
		{
			client.SendMessage(channel, ".mods", false);
		}
	}
}
