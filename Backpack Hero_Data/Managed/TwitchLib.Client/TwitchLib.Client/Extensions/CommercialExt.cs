using System;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Extensions
{
	// Token: 0x0200000B RID: 11
	public static class CommercialExt
	{
		// Token: 0x060001B1 RID: 433 RVA: 0x00007138 File Offset: 0x00005338
		public static void StartCommercial(this ITwitchClient client, JoinedChannel channel, CommercialLength length)
		{
			if (length <= CommercialLength.Seconds90)
			{
				if (length == CommercialLength.Seconds30)
				{
					client.SendMessage(channel, ".commercial 30", false);
					return;
				}
				if (length == CommercialLength.Seconds60)
				{
					client.SendMessage(channel, ".commercial 60", false);
					return;
				}
				if (length == CommercialLength.Seconds90)
				{
					client.SendMessage(channel, ".commercial 90", false);
					return;
				}
			}
			else
			{
				if (length == CommercialLength.Seconds120)
				{
					client.SendMessage(channel, ".commercial 120", false);
					return;
				}
				if (length == CommercialLength.Seconds150)
				{
					client.SendMessage(channel, ".commercial 150", false);
					return;
				}
				if (length == CommercialLength.Seconds180)
				{
					client.SendMessage(channel, ".commercial 180", false);
					return;
				}
			}
			throw new ArgumentOutOfRangeException("length", length, null);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000071D8 File Offset: 0x000053D8
		public static void StartCommercial(this ITwitchClient client, string channel, CommercialLength length)
		{
			if (length <= CommercialLength.Seconds90)
			{
				if (length == CommercialLength.Seconds30)
				{
					client.SendMessage(channel, ".commercial 30", false);
					return;
				}
				if (length == CommercialLength.Seconds60)
				{
					client.SendMessage(channel, ".commercial 60", false);
					return;
				}
				if (length == CommercialLength.Seconds90)
				{
					client.SendMessage(channel, ".commercial 90", false);
					return;
				}
			}
			else
			{
				if (length == CommercialLength.Seconds120)
				{
					client.SendMessage(channel, ".commercial 120", false);
					return;
				}
				if (length == CommercialLength.Seconds150)
				{
					client.SendMessage(channel, ".commercial 150", false);
					return;
				}
				if (length == CommercialLength.Seconds180)
				{
					client.SendMessage(channel, ".commercial 180", false);
					return;
				}
			}
			throw new ArgumentOutOfRangeException("length", length, null);
		}
	}
}
