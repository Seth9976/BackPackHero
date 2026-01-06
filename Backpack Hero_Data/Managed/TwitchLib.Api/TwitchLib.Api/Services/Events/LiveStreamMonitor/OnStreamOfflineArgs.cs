using System;
using TwitchLib.Api.Helix.Models.Streams.GetStreams;

namespace TwitchLib.Api.Services.Events.LiveStreamMonitor
{
	// Token: 0x02000016 RID: 22
	public class OnStreamOfflineArgs : EventArgs
	{
		// Token: 0x0400004A RID: 74
		public string Channel;

		// Token: 0x0400004B RID: 75
		public Stream Stream;
	}
}
