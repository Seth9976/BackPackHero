using System;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Extensions
{
	// Token: 0x02000016 RID: 22
	public static class SubscribersOnly
	{
		// Token: 0x060001EB RID: 491 RVA: 0x00007CA4 File Offset: 0x00005EA4
		public static void SubscribersOnlyOn(this ITwitchClient client, JoinedChannel channel)
		{
			client.SendMessage(channel, ".subscribers", false);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00007CB3 File Offset: 0x00005EB3
		public static void SubscribersOnlyOn(this ITwitchClient client, string channel)
		{
			client.SendMessage(channel, ".subscribers", false);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00007CC2 File Offset: 0x00005EC2
		public static void SubscribersOnlyOff(this ITwitchClient client, JoinedChannel channel)
		{
			client.SendMessage(channel, ".subscribersoff", false);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00007CD1 File Offset: 0x00005ED1
		public static void SubscribersOnlyOff(this ITwitchClient client, string channel)
		{
			client.SendMessage(channel, ".subscribersoff", false);
		}
	}
}
