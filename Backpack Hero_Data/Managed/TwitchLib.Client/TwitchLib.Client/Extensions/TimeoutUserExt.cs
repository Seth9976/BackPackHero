using System;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Extensions
{
	// Token: 0x02000017 RID: 23
	public static class TimeoutUserExt
	{
		// Token: 0x060001EF RID: 495 RVA: 0x00007CE0 File Offset: 0x00005EE0
		public static void TimeoutUser(this ITwitchClient client, JoinedChannel channel, string viewer, TimeSpan duration, string message = "", bool dryRun = false)
		{
			client.SendMessage(channel, string.Format(".timeout {0} {1} {2}", viewer, duration.TotalSeconds, message), dryRun);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00007D04 File Offset: 0x00005F04
		public static void TimeoutUser(this ITwitchClient client, string channel, string viewer, TimeSpan duration, string message = "", bool dryRun = false)
		{
			JoinedChannel joinedChannel = client.GetJoinedChannel(channel);
			if (joinedChannel != null)
			{
				client.TimeoutUser(joinedChannel, viewer, duration, message, dryRun);
			}
		}
	}
}
