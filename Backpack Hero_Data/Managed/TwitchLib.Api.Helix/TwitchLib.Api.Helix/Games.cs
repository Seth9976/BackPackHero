using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Games;

namespace TwitchLib.Api.Helix
{
	// Token: 0x02000012 RID: 18
	public class Games : ApiBase
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00003C44 File Offset: 0x00001E44
		public Games(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
			: base(settings, rateLimiter, http)
		{
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003C50 File Offset: 0x00001E50
		public Task<GetGamesResponse> GetGamesAsync(List<string> gameIds = null, List<string> gameNames = null, string accessToken = null)
		{
			if ((gameIds == null && gameNames == null) || (gameIds != null && gameIds.Count == 0 && gameNames == null) || (gameNames != null && gameNames.Count == 0 && gameIds == null))
			{
				throw new BadParameterException("Either gameIds or gameNames must have at least one value");
			}
			if (gameIds != null && gameIds.Count > 100)
			{
				throw new BadParameterException("gameIds list cannot exceed 100 items");
			}
			if (gameNames != null && gameNames.Count > 100)
			{
				throw new BadParameterException("gameNames list cannot exceed 100 items");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			if (gameIds != null && gameIds.Count > 0)
			{
				list.AddRange(Enumerable.Select<string, KeyValuePair<string, string>>(gameIds, (string gameId) => new KeyValuePair<string, string>("id", gameId)));
			}
			if (gameNames != null && gameNames.Count > 0)
			{
				list.AddRange(Enumerable.Select<string, KeyValuePair<string, string>>(gameNames, (string gameName) => new KeyValuePair<string, string>("name", gameName)));
			}
			return base.TwitchGetGenericAsync<GetGamesResponse>("/games", ApiVersion.Helix, list, accessToken, null, null);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003D40 File Offset: 0x00001F40
		public Task<GetTopGamesResponse> GetTopGamesAsync(string before = null, string after = null, int first = 20, string accessToken = null)
		{
			if (first < 0 || first > 100)
			{
				throw new BadParameterException("'first' parameter must be between 1 (inclusive) and 100 (inclusive).");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("first", first.ToString()));
			List<KeyValuePair<string, string>> list2 = list;
			if (!string.IsNullOrWhiteSpace(before))
			{
				list2.Add(new KeyValuePair<string, string>("before", before));
			}
			if (!string.IsNullOrWhiteSpace(after))
			{
				list2.Add(new KeyValuePair<string, string>("after", after));
			}
			return base.TwitchGetGenericAsync<GetTopGamesResponse>("/games/top", ApiVersion.Helix, list2, accessToken, null, null);
		}
	}
}
