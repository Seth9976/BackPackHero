using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Charity.GetCharityCampaign;

namespace TwitchLib.Api.Helix
{
	// Token: 0x0200000C RID: 12
	public class Charity : ApiBase
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002F87 File Offset: 0x00001187
		public Charity(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
			: base(settings, rateLimiter, http)
		{
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002F94 File Offset: 0x00001194
		public Task<GetCharityCampaignResponse> GetCharityCampaignAsync(string broadcasterId, string accessToken = null)
		{
			if (string.IsNullOrEmpty(broadcasterId))
			{
				throw new BadParameterException("broadcasterId must be set");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchGetGenericAsync<GetCharityCampaignResponse>("/charity/campaigns", ApiVersion.Helix, list2, accessToken, null, null);
		}
	}
}
