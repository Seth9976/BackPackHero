using System;
using TwitchLib.Client.Interfaces;

namespace TwitchLib.Client.Extensions
{
	// Token: 0x02000014 RID: 20
	public static class ReplyWhisperExt
	{
		// Token: 0x060001E6 RID: 486 RVA: 0x00007BC2 File Offset: 0x00005DC2
		public static void ReplyToLastWhisper(this ITwitchClient client, string message = "", bool dryRun = false)
		{
			if (client.PreviousWhisper != null)
			{
				client.SendWhisper(client.PreviousWhisper.Username, message, dryRun);
			}
		}
	}
}
