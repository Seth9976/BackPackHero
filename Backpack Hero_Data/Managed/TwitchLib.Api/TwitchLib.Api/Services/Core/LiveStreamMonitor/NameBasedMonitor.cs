using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TwitchLib.Api.Helix.Models.Streams.GetStreams;
using TwitchLib.Api.Interfaces;

namespace TwitchLib.Api.Services.Core.LiveStreamMonitor
{
	// Token: 0x0200001D RID: 29
	internal class NameBasedMonitor : CoreMonitor
	{
		// Token: 0x060000AC RID: 172 RVA: 0x00003894 File Offset: 0x00001A94
		public NameBasedMonitor(ITwitchAPI api)
			: base(api)
		{
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000038B0 File Offset: 0x00001AB0
		public override Task<Func<Stream, bool>> CompareStream(string channel, string accessToken = null)
		{
			NameBasedMonitor.<CompareStream>d__2 <CompareStream>d__;
			<CompareStream>d__.<>4__this = this;
			<CompareStream>d__.channel = channel;
			<CompareStream>d__.accessToken = accessToken;
			<CompareStream>d__.<>t__builder = AsyncTaskMethodBuilder<Func<Stream, bool>>.Create();
			<CompareStream>d__.<>1__state = -1;
			<CompareStream>d__.<>t__builder.Start<NameBasedMonitor.<CompareStream>d__2>(ref <CompareStream>d__);
			return <CompareStream>d__.<>t__builder.Task;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003903 File Offset: 0x00001B03
		public override Task<GetStreamsResponse> GetStreamsAsync(List<string> channels, string accessToken = null)
		{
			return this._api.Helix.Streams.GetStreamsAsync(null, channels.Count, null, null, null, channels, accessToken);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003926 File Offset: 0x00001B26
		public void ClearCache()
		{
			this._channelToId.Clear();
		}

		// Token: 0x04000055 RID: 85
		private readonly ConcurrentDictionary<string, string> _channelToId = new ConcurrentDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
	}
}
