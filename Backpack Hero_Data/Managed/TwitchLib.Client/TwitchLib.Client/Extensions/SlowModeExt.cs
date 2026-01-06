using System;
using TwitchLib.Client.Exceptions;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Extensions
{
	// Token: 0x02000015 RID: 21
	public static class SlowModeExt
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x00007BE0 File Offset: 0x00005DE0
		public static void SlowModeOn(this ITwitchClient client, JoinedChannel channel, TimeSpan messageCooldown)
		{
			if (messageCooldown > TimeSpan.FromDays(1.0))
			{
				throw new InvalidParameterException("The message cooldown time supplied exceeded the maximum allowed by Twitch, which is 1 day.", client.TwitchUsername);
			}
			client.SendMessage(channel, string.Format(".slow {0}", messageCooldown.TotalSeconds), false);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00007C34 File Offset: 0x00005E34
		public static void SlowModeOn(this ITwitchClient client, string channel, TimeSpan messageCooldown)
		{
			if (messageCooldown > TimeSpan.FromDays(1.0))
			{
				throw new InvalidParameterException("The message cooldown time supplied exceeded the maximum allowed by Twitch, which is 1 day.", client.TwitchUsername);
			}
			client.SendMessage(channel, string.Format(".slow {0}", messageCooldown.TotalSeconds), false);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00007C86 File Offset: 0x00005E86
		public static void SlowModeOff(this ITwitchClient client, JoinedChannel channel)
		{
			client.SendMessage(channel, ".slowoff", false);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00007C95 File Offset: 0x00005E95
		public static void SlowModeOff(this ITwitchClient client, string channel)
		{
			client.SendMessage(channel, ".slowoff", false);
		}
	}
}
