using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Helix.Models.Streams.GetStreams;
using TwitchLib.Api.Interfaces;

namespace TwitchLib.Api.Services.Core.LiveStreamMonitor
{
	// Token: 0x0200001B RID: 27
	internal abstract class CoreMonitor
	{
		// Token: 0x060000A6 RID: 166
		public abstract Task<GetStreamsResponse> GetStreamsAsync(List<string> channels, string accessToken = null);

		// Token: 0x060000A7 RID: 167
		public abstract Task<Func<Stream, bool>> CompareStream(string channel, string accessToken = null);

		// Token: 0x060000A8 RID: 168 RVA: 0x0000383B File Offset: 0x00001A3B
		protected CoreMonitor(ITwitchAPI api)
		{
			this._api = api;
		}

		// Token: 0x04000054 RID: 84
		protected readonly ITwitchAPI _api;
	}
}
