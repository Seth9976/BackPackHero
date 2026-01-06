using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Teams;

namespace TwitchLib.Api.Helix
{
	// Token: 0x02000020 RID: 32
	public class Teams : ApiBase
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x00005996 File Offset: 0x00003B96
		public Teams(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
			: base(settings, rateLimiter, http)
		{
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000059A4 File Offset: 0x00003BA4
		public Task<GetChannelTeamsResponse> GetChannelTeamsAsync(string broadcasterId, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchGetGenericAsync<GetChannelTeamsResponse>("/teams/channel", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000059D8 File Offset: 0x00003BD8
		public Task<GetTeamsResponse> GetTeamsAsync(string teamId = null, string teamName = null, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			if (!string.IsNullOrWhiteSpace(teamId))
			{
				list.Add(new KeyValuePair<string, string>("id", teamId));
			}
			if (!string.IsNullOrWhiteSpace(teamName))
			{
				list.Add(new KeyValuePair<string, string>("name", teamName));
			}
			return base.TwitchGetGenericAsync<GetTeamsResponse>("/teams", ApiVersion.Helix, list, accessToken, null, null);
		}
	}
}
