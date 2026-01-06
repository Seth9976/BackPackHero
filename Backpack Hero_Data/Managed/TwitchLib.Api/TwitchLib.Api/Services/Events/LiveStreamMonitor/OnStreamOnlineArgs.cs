using System;
using TwitchLib.Api.Helix.Models.Streams.GetStreams;

namespace TwitchLib.Api.Services.Events.LiveStreamMonitor
{
	// Token: 0x02000017 RID: 23
	public class OnStreamOnlineArgs : EventArgs
	{
		// Token: 0x0400004C RID: 76
		public string Channel;

		// Token: 0x0400004D RID: 77
		public Stream Stream;
	}
}
