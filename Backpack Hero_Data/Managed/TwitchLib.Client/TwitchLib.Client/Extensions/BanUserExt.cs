using System;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Extensions
{
	// Token: 0x02000008 RID: 8
	public static class BanUserExt
	{
		// Token: 0x060001AB RID: 427 RVA: 0x000070A4 File Offset: 0x000052A4
		public static void BanUser(this ITwitchClient client, JoinedChannel channel, string viewer, string message = "", bool dryRun = false)
		{
			client.SendMessage(channel, ".ban " + viewer + " " + message, false);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000070C0 File Offset: 0x000052C0
		public static void BanUser(this ITwitchClient client, string channel, string viewer, string message = "", bool dryRun = false)
		{
			JoinedChannel joinedChannel = client.GetJoinedChannel(channel);
			if (joinedChannel != null)
			{
				client.BanUser(joinedChannel, viewer, message, dryRun);
			}
		}
	}
}
