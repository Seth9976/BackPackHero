using System;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Extensions
{
	// Token: 0x02000011 RID: 17
	public static class MarkerExt
	{
		// Token: 0x060001DE RID: 478 RVA: 0x00007B26 File Offset: 0x00005D26
		public static void Marker(this ITwitchClient client, JoinedChannel channel)
		{
			client.SendMessage(channel, ".marker", false);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00007B35 File Offset: 0x00005D35
		public static void Marker(this ITwitchClient client, string channel)
		{
			client.SendMessage(channel, ".marker", false);
		}
	}
}
