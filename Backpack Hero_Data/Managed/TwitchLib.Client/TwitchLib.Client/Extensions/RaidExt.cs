using System;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Extensions
{
	// Token: 0x02000013 RID: 19
	public static class RaidExt
	{
		// Token: 0x060001E4 RID: 484 RVA: 0x00007B98 File Offset: 0x00005D98
		public static void Raid(this ITwitchClient client, JoinedChannel channel, string channelToRaid)
		{
			client.SendMessage(channel, ".raid " + channelToRaid, false);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00007BAD File Offset: 0x00005DAD
		public static void Raid(this ITwitchClient client, string channel, string channelToRaid)
		{
			client.SendMessage(channel, ".raid " + channelToRaid, false);
		}
	}
}
