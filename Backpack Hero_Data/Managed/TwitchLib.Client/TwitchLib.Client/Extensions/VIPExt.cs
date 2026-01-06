using System;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Extensions
{
	// Token: 0x02000019 RID: 25
	public static class VIPExt
	{
		// Token: 0x060001F3 RID: 499 RVA: 0x00007D61 File Offset: 0x00005F61
		public static void VIP(this ITwitchClient client, JoinedChannel channel, string viewerToVIP)
		{
			client.SendMessage(channel, ".vip " + viewerToVIP, false);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00007D76 File Offset: 0x00005F76
		public static void VIP(this ITwitchClient client, string channel, string viewerToVIP)
		{
			client.SendMessage(channel, ".vip " + viewerToVIP, false);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00007D8B File Offset: 0x00005F8B
		public static void UnVIP(this ITwitchClient client, JoinedChannel channel, string viewerToUnVIP)
		{
			client.SendMessage(channel, ".unvip " + viewerToUnVIP, false);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00007DA0 File Offset: 0x00005FA0
		public static void UnVIP(this ITwitchClient client, string channel, string viewerToUnVIP)
		{
			client.SendMessage(channel, ".unvip " + viewerToUnVIP, false);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00007DB5 File Offset: 0x00005FB5
		public static void GetVIPs(this ITwitchClient client, JoinedChannel channel)
		{
			client.SendMessage(channel, ".vips", false);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00007DC4 File Offset: 0x00005FC4
		public static void GetVIPs(this ITwitchClient client, string channel)
		{
			client.SendMessage(channel, ".vips", false);
		}
	}
}
