using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Search;

namespace TwitchLib.Api.Helix
{
	// Token: 0x0200001B RID: 27
	public class Search : ApiBase
	{
		// Token: 0x0600009F RID: 159 RVA: 0x00005158 File Offset: 0x00003358
		public Search(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
			: base(settings, rateLimiter, http)
		{
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00005164 File Offset: 0x00003364
		public Task<SearchCategoriesResponse> SearchCategoriesAsync(string encodedSearchQuery, string after = null, int first = 20, string accessToken = null)
		{
			if (first < 0 || first > 100)
			{
				throw new BadParameterException("'first' parameter must be between 1 (inclusive) and 100 (inclusive).");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("query", encodedSearchQuery));
			List<KeyValuePair<string, string>> list2 = list;
			if (!string.IsNullOrWhiteSpace(after))
			{
				list2.Add(new KeyValuePair<string, string>("after", after));
			}
			list2.Add(new KeyValuePair<string, string>("first", first.ToString()));
			return base.TwitchGetGenericAsync<SearchCategoriesResponse>("/search/categories", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000051E0 File Offset: 0x000033E0
		public Task<SearchChannelsResponse> SearchChannelsAsync(string encodedSearchQuery, bool liveOnly = false, string after = null, int first = 20, string accessToken = null)
		{
			if (first < 0 || first > 100)
			{
				throw new BadParameterException("'first' parameter must be between 1 (inclusive) and 100 (inclusive).");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("query", encodedSearchQuery));
			list.Add(new KeyValuePair<string, string>("live_only", liveOnly.ToString()));
			List<KeyValuePair<string, string>> list2 = list;
			if (!string.IsNullOrWhiteSpace(after))
			{
				list2.Add(new KeyValuePair<string, string>("after", after));
			}
			list2.Add(new KeyValuePair<string, string>("first", first.ToString()));
			return base.TwitchGetGenericAsync<SearchChannelsResponse>("/search/channels", ApiVersion.Helix, list2, accessToken, null, null);
		}
	}
}
