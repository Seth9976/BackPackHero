using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Raids.StartRaid;

namespace TwitchLib.Api.Helix
{
	// Token: 0x02000019 RID: 25
	public class Raids : ApiBase
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00004DF8 File Offset: 0x00002FF8
		public Raids(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
			: base(settings, rateLimiter, http)
		{
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004E04 File Offset: 0x00003004
		public Task<StartRaidResponse> StartRaidAsync(string fromBroadcasterId, string toBroadcasterId, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("from_broadcaster_id", fromBroadcasterId));
			list.Add(new KeyValuePair<string, string>("to_broadcaster_id", toBroadcasterId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchPostGenericAsync<StartRaidResponse>("/raids", ApiVersion.Helix, string.Empty, list2, accessToken, null, null);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004E50 File Offset: 0x00003050
		public Task CancelRaidAsync(string broadcasterId, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchDeleteAsync("/raids", ApiVersion.Helix, list2, accessToken, null, null);
		}
	}
}
