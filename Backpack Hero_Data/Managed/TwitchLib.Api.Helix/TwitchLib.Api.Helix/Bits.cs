using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Extensions.System;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Bits;
using TwitchLib.Api.Helix.Models.Bits.ExtensionBitsProducts;

namespace TwitchLib.Api.Helix
{
	// Token: 0x02000009 RID: 9
	public class Bits : ApiBase
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002868 File Offset: 0x00000A68
		public Bits(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
			: base(settings, rateLimiter, http)
		{
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002874 File Offset: 0x00000A74
		public Task<GetCheermotesResponse> GetCheermotesAsync(string broadcasterId = null, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			if (!string.IsNullOrWhiteSpace(broadcasterId))
			{
				list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			}
			return base.TwitchGetGenericAsync<GetCheermotesResponse>("/bits/cheermotes", ApiVersion.Helix, list, accessToken, null, null);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000028B0 File Offset: 0x00000AB0
		public Task<GetBitsLeaderboardResponse> GetBitsLeaderboardAsync(int count = 10, BitsLeaderboardPeriodEnum period = BitsLeaderboardPeriodEnum.All, DateTime? startedAt = null, string userid = null, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("count", count.ToString()));
			List<KeyValuePair<string, string>> list2 = list;
			switch (period)
			{
			case BitsLeaderboardPeriodEnum.Day:
				list2.Add(new KeyValuePair<string, string>("period", "day"));
				break;
			case BitsLeaderboardPeriodEnum.Week:
				list2.Add(new KeyValuePair<string, string>("period", "week"));
				break;
			case BitsLeaderboardPeriodEnum.Month:
				list2.Add(new KeyValuePair<string, string>("period", "month"));
				break;
			case BitsLeaderboardPeriodEnum.Year:
				list2.Add(new KeyValuePair<string, string>("period", "year"));
				break;
			case BitsLeaderboardPeriodEnum.All:
				list2.Add(new KeyValuePair<string, string>("period", "all"));
				break;
			default:
				throw new ArgumentOutOfRangeException("period", period, null);
			}
			if (startedAt != null)
			{
				list2.Add(new KeyValuePair<string, string>("started_at", startedAt.Value.ToRfc3339String()));
			}
			if (!string.IsNullOrWhiteSpace(userid))
			{
				list2.Add(new KeyValuePair<string, string>("user_id", userid));
			}
			return base.TwitchGetGenericAsync<GetBitsLeaderboardResponse>("/bits/leaderboard", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000029CC File Offset: 0x00000BCC
		public Task<GetExtensionBitsProductsResponse> GetExtensionBitsProductsAsync(bool shouldIncludeAll = false, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("should_include_all", shouldIncludeAll.ToString().ToLower()));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchGetGenericAsync<GetExtensionBitsProductsResponse>("/bits/extensions", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002A0B File Offset: 0x00000C0B
		public Task<UpdateExtensionBitsProductResponse> UpdateExtensionBitsProductAsync(ExtensionBitsProduct extensionBitsProduct, string accessToken = null)
		{
			return base.TwitchPutGenericAsync<UpdateExtensionBitsProductResponse>("/bits/extensions", ApiVersion.Helix, extensionBitsProduct.ToString(), null, accessToken, null, null);
		}
	}
}
