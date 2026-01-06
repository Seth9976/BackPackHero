using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Helix.Models.Streams.GetStreams;
using TwitchLib.Api.Interfaces;

namespace TwitchLib.Api.Services.Core.LiveStreamMonitor
{
	// Token: 0x0200001C RID: 28
	internal class IdBasedMonitor : CoreMonitor
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x0000384A File Offset: 0x00001A4A
		public IdBasedMonitor(ITwitchAPI api)
			: base(api)
		{
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003853 File Offset: 0x00001A53
		public override Task<Func<Stream, bool>> CompareStream(string channel, string accessToken = null)
		{
			return Task.FromResult<Func<Stream, bool>>((Stream stream) => stream.UserId == channel);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003871 File Offset: 0x00001A71
		public override Task<GetStreamsResponse> GetStreamsAsync(List<string> channels, string accessToken = null)
		{
			return this._api.Helix.Streams.GetStreamsAsync(null, channels.Count, null, null, channels, null, accessToken);
		}
	}
}
