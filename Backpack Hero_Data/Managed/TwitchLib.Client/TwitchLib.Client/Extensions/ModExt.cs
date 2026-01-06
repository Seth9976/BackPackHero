using System;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Extensions
{
	// Token: 0x02000012 RID: 18
	public static class ModExt
	{
		// Token: 0x060001E0 RID: 480 RVA: 0x00007B44 File Offset: 0x00005D44
		public static void Mod(this ITwitchClient client, JoinedChannel channel, string viewerToMod)
		{
			client.SendMessage(channel, ".mod " + viewerToMod, false);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00007B59 File Offset: 0x00005D59
		public static void Mod(this ITwitchClient client, string channel, string viewerToMod)
		{
			client.SendMessage(channel, ".mod " + viewerToMod, false);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00007B6E File Offset: 0x00005D6E
		public static void Unmod(this ITwitchClient client, JoinedChannel channel, string viewerToUnmod)
		{
			client.SendMessage(channel, ".unmod " + viewerToUnmod, false);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00007B83 File Offset: 0x00005D83
		public static void Unmod(this ITwitchClient client, string channel, string viewerToUnmod)
		{
			client.SendMessage(channel, ".unmod " + viewerToUnmod, false);
		}
	}
}
