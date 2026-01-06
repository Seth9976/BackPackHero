using System;
using TwitchLib.Client.Exceptions;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Extensions
{
	// Token: 0x0200000F RID: 15
	public static class FollowersOnlyExt
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x000079FC File Offset: 0x00005BFC
		public static void FollowersOnlyOn(this ITwitchClient client, JoinedChannel channel, TimeSpan requiredFollowTime)
		{
			if (requiredFollowTime > TimeSpan.FromDays(90.0))
			{
				throw new InvalidParameterException("The amount of time required to chat exceeded the maximum allowed by Twitch, which is 3 months.", client.TwitchUsername);
			}
			string text = string.Format("{0}d {1}h {2}m", requiredFollowTime.Days, requiredFollowTime.Hours, requiredFollowTime.Minutes);
			client.SendMessage(channel, ".followers " + text, false);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00007A74 File Offset: 0x00005C74
		public static void FollowersOnlyOn(this ITwitchClient client, string channel, TimeSpan requiredFollowTime)
		{
			if (requiredFollowTime > TimeSpan.FromDays(90.0))
			{
				throw new InvalidParameterException("The amount of time required to chat exceeded the maximum allowed by Twitch, which is 3 months.", client.TwitchUsername);
			}
			string text = string.Format("{0}d {1}h {2}m", requiredFollowTime.Days, requiredFollowTime.Hours, requiredFollowTime.Minutes);
			client.SendMessage(channel, ".followers " + text, false);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00007AEA File Offset: 0x00005CEA
		public static void FollowersOnlyOff(this ITwitchClient client, JoinedChannel channel)
		{
			client.SendMessage(channel, ".followersoff", false);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00007AF9 File Offset: 0x00005CF9
		public static void FollowersOnlyOff(this TwitchClient client, string channel)
		{
			client.SendMessage(channel, ".followersoff", false);
		}

		// Token: 0x04000051 RID: 81
		private const int MaximumDurationAllowedDays = 90;
	}
}
