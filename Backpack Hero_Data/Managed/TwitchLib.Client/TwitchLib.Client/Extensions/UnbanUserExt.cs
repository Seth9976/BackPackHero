using System;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Extensions
{
	// Token: 0x02000018 RID: 24
	public static class UnbanUserExt
	{
		// Token: 0x060001F1 RID: 497 RVA: 0x00007D29 File Offset: 0x00005F29
		public static void UnbanUser(this ITwitchClient client, JoinedChannel channel, string viewer, bool dryRun = false)
		{
			client.SendMessage(channel, ".unban " + viewer, dryRun);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00007D40 File Offset: 0x00005F40
		public static void UnbanUser(this ITwitchClient client, string channel, string viewer, bool dryRun = false)
		{
			JoinedChannel joinedChannel = client.GetJoinedChannel(channel);
			if (joinedChannel != null)
			{
				client.UnbanUser(joinedChannel, viewer, dryRun);
			}
		}
	}
}
