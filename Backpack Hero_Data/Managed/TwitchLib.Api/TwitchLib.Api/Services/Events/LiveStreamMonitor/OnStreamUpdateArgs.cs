using System;
using TwitchLib.Api.Helix.Models.Streams.GetStreams;

namespace TwitchLib.Api.Services.Events.LiveStreamMonitor
{
	// Token: 0x02000018 RID: 24
	public class OnStreamUpdateArgs : EventArgs
	{
		// Token: 0x0400004E RID: 78
		public string Channel;

		// Token: 0x0400004F RID: 79
		public Stream Stream;
	}
}
