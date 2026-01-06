using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Extensions.System;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Analytics;

namespace TwitchLib.Api.Helix
{
	// Token: 0x02000008 RID: 8
	public class Analytics : ApiBase
	{
		// Token: 0x06000023 RID: 35 RVA: 0x000026B5 File Offset: 0x000008B5
		public Analytics(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
			: base(settings, rateLimiter, http)
		{
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000026C0 File Offset: 0x000008C0
		public Task<GetGameAnalyticsResponse> GetGameAnalyticsAsync(string gameId = null, DateTime? startedAt = null, DateTime? endedAt = null, int first = 20, string after = null, string type = null, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("first", first.ToString()));
			List<KeyValuePair<string, string>> list2 = list;
			if (!string.IsNullOrWhiteSpace(gameId))
			{
				list2.Add(new KeyValuePair<string, string>("game_id", gameId));
			}
			if (startedAt != null && endedAt != null)
			{
				list2.Add(new KeyValuePair<string, string>("started_at", startedAt.Value.ToRfc3339String()));
				list2.Add(new KeyValuePair<string, string>("ended_at", endedAt.Value.ToRfc3339String()));
			}
			if (!string.IsNullOrWhiteSpace(type))
			{
				list2.Add(new KeyValuePair<string, string>("type", type));
			}
			if (!string.IsNullOrWhiteSpace(after))
			{
				list2.Add(new KeyValuePair<string, string>("after", after));
			}
			return base.TwitchGetGenericAsync<GetGameAnalyticsResponse>("/analytics/games", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002794 File Offset: 0x00000994
		public Task<GetExtensionAnalyticsResponse> GetExtensionAnalyticsAsync(string extensionId, DateTime? startedAt = null, DateTime? endedAt = null, int first = 20, string after = null, string type = null, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("first", first.ToString()));
			List<KeyValuePair<string, string>> list2 = list;
			if (!string.IsNullOrWhiteSpace(extensionId))
			{
				list2.Add(new KeyValuePair<string, string>("extension_id", extensionId));
			}
			if (startedAt != null && endedAt != null)
			{
				list2.Add(new KeyValuePair<string, string>("started_at", startedAt.Value.ToRfc3339String()));
				list2.Add(new KeyValuePair<string, string>("ended_at", endedAt.Value.ToRfc3339String()));
			}
			if (!string.IsNullOrWhiteSpace(type))
			{
				list2.Add(new KeyValuePair<string, string>("type", type));
			}
			if (!string.IsNullOrWhiteSpace(after))
			{
				list2.Add(new KeyValuePair<string, string>("after", after));
			}
			return base.TwitchGetGenericAsync<GetExtensionAnalyticsResponse>("/analytics/extensions", ApiVersion.Helix, list2, accessToken, null, null);
		}
	}
}
