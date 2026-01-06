using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Entitlements.GetCodeStatus;
using TwitchLib.Api.Helix.Models.Entitlements.GetDropsEntitlements;
using TwitchLib.Api.Helix.Models.Entitlements.RedeemCode;
using TwitchLib.Api.Helix.Models.Entitlements.UpdateDropsEntitlements;

namespace TwitchLib.Api.Helix
{
	// Token: 0x0200000F RID: 15
	public class Entitlements : ApiBase
	{
		// Token: 0x0600004B RID: 75 RVA: 0x0000368D File Offset: 0x0000188D
		public Entitlements(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
			: base(settings, rateLimiter, http)
		{
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003698 File Offset: 0x00001898
		public Task<GetCodeStatusResponse> GetCodeStatusAsync(List<string> codes, string userId, string accessToken = null)
		{
			if (codes == null || codes.Count == 0 || codes.Count > 20)
			{
				throw new BadParameterException("codes cannot be null and must have between 1 and 20 items");
			}
			if (userId == null)
			{
				throw new BadParameterException("userId cannot be null");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("user_id", userId));
			List<KeyValuePair<string, string>> list2 = list;
			list2.AddRange(Enumerable.Select<string, KeyValuePair<string, string>>(codes, (string code) => new KeyValuePair<string, string>("code", code)));
			return base.TwitchPostGenericAsync<GetCodeStatusResponse>("/entitlements/codes", ApiVersion.Helix, null, list2, accessToken, null, null);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003728 File Offset: 0x00001928
		public Task<GetDropsEntitlementsResponse> GetDropsEntitlementsAsync(string id = null, string userId = null, string gameId = null, string after = null, int first = 20, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("first", first.ToString()));
			List<KeyValuePair<string, string>> list2 = list;
			if (!string.IsNullOrWhiteSpace(id))
			{
				list2.Add(new KeyValuePair<string, string>("id", id));
			}
			if (!string.IsNullOrWhiteSpace(userId))
			{
				list2.Add(new KeyValuePair<string, string>("user_id", userId));
			}
			if (!string.IsNullOrWhiteSpace(gameId))
			{
				list2.Add(new KeyValuePair<string, string>("game_id", gameId));
			}
			if (!string.IsNullOrWhiteSpace(after))
			{
				list2.Add(new KeyValuePair<string, string>("after", after));
			}
			return base.TwitchGetGenericAsync<GetDropsEntitlementsResponse>("/entitlements/drops", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000037CC File Offset: 0x000019CC
		public Task<UpdateDropsEntitlementsResponse> UpdateDropsEntitlementsAsync(string[] entitlementIds, FulfillmentStatus fulfillmentStatus, string accessToken)
		{
			var <>f__AnonymousType = new
			{
				entitlement_ids = entitlementIds,
				fulfillment_status = fulfillmentStatus.ToString()
			};
			return base.TwitchPatchGenericAsync<UpdateDropsEntitlementsResponse>("/entitlements/drops", ApiVersion.Helix, JsonConvert.SerializeObject(<>f__AnonymousType), null, accessToken, null, null);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003804 File Offset: 0x00001A04
		public Task<RedeemCodeResponse> RedeemCodeAsync(List<string> codes, string accessToken = null)
		{
			if (codes == null || codes.Count == 0 || codes.Count > 20)
			{
				throw new BadParameterException("codes cannot be null and must have between 1 and 20 items");
			}
			List<KeyValuePair<string, string>> list = Enumerable.ToList<KeyValuePair<string, string>>(Enumerable.Select<string, KeyValuePair<string, string>>(codes, (string code) => new KeyValuePair<string, string>("code", code)));
			return base.TwitchPostGenericAsync<RedeemCodeResponse>("/entitlements/codes", ApiVersion.Helix, null, list, accessToken, null, null);
		}
	}
}
