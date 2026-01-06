using System;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Extensions
{
	// Token: 0x02000009 RID: 9
	public static class ChangeChatColorExt
	{
		// Token: 0x060001AD RID: 429 RVA: 0x000070E3 File Offset: 0x000052E3
		public static void ChangeChatColor(this ITwitchClient client, JoinedChannel channel, ChatColorPresets color)
		{
			client.SendMessage(channel, string.Format(".color {0}", color), false);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000070FD File Offset: 0x000052FD
		public static void ChangeChatColor(this ITwitchClient client, string channel, ChatColorPresets color)
		{
			client.SendMessage(channel, string.Format(".color {0}", color), false);
		}
	}
}
