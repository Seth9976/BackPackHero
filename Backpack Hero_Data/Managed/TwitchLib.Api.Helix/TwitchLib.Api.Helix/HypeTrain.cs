using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.HypeTrain;

namespace TwitchLib.Api.Helix
{
	// Token: 0x02000015 RID: 21
	public class HypeTrain : ApiBase
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00004160 File Offset: 0x00002360
		public HypeTrain(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
			: base(settings, rateLimiter, http)
		{
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000416C File Offset: 0x0000236C
		public Task<GetHypeTrainResponse> GetHypeTrainEventsAsync(string broadcasterId, int first = 1, string cursor = null, string accessToken = null)
		{
			if (string.IsNullOrEmpty(broadcasterId))
			{
				throw new BadParameterException("BroadcasterId must be set");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			list.Add(new KeyValuePair<string, string>("first", first.ToString()));
			List<KeyValuePair<string, string>> list2 = list;
			if (!string.IsNullOrWhiteSpace(cursor))
			{
				list2.Add(new KeyValuePair<string, string>("cursor", cursor));
			}
			return base.TwitchGetGenericAsync<GetHypeTrainResponse>("/hypetrain/events", ApiVersion.Helix, list2, accessToken, null, null);
		}
	}
}
