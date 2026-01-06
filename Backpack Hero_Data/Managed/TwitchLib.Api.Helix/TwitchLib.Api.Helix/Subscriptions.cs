using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Subscriptions;

namespace TwitchLib.Api.Helix
{
	// Token: 0x0200001E RID: 30
	public class Subscriptions : ApiBase
	{
		// Token: 0x060000AE RID: 174 RVA: 0x0000575D File Offset: 0x0000395D
		public Subscriptions(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
			: base(settings, rateLimiter, http)
		{
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00005768 File Offset: 0x00003968
		public Task<CheckUserSubscriptionResponse> CheckUserSubscriptionAsync(string broadcasterId, string userId, string accessToken = null)
		{
			if (string.IsNullOrWhiteSpace(broadcasterId))
			{
				throw new BadParameterException("BroadcasterId must be set");
			}
			if (string.IsNullOrWhiteSpace(userId))
			{
				throw new BadParameterException("UserId must be set");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			list.Add(new KeyValuePair<string, string>("user_id", userId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchGetGenericAsync<CheckUserSubscriptionResponse>("/subscriptions/user", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000057D4 File Offset: 0x000039D4
		public Task<GetUserSubscriptionsResponse> GetUserSubscriptionsAsync(string broadcasterId, List<string> userIds, string accessToken = null)
		{
			if (string.IsNullOrWhiteSpace(broadcasterId))
			{
				throw new BadParameterException("BroadcasterId must be set");
			}
			if (userIds == null || userIds.Count == 0)
			{
				throw new BadParameterException("UserIds must be set contain at least one user id");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			List<KeyValuePair<string, string>> list2 = list;
			list2.AddRange(Enumerable.Select<string, KeyValuePair<string, string>>(userIds, (string userId) => new KeyValuePair<string, string>("user_id", userId)));
			return base.TwitchGetGenericAsync<GetUserSubscriptionsResponse>("/subscriptions", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000585C File Offset: 0x00003A5C
		public Task<GetBroadcasterSubscriptionsResponse> GetBroadcasterSubscriptionsAsync(string broadcasterId, int first = 20, string after = null, string accessToken = null)
		{
			if (string.IsNullOrWhiteSpace(broadcasterId))
			{
				throw new BadParameterException("BroadcasterId must be set");
			}
			if (first > 100)
			{
				throw new BadParameterException("First must be 100 or less");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			list.Add(new KeyValuePair<string, string>("first", first.ToString()));
			List<KeyValuePair<string, string>> list2 = list;
			if (!string.IsNullOrWhiteSpace(after))
			{
				list2.Add(new KeyValuePair<string, string>("after", after));
			}
			return base.TwitchGetGenericAsync<GetBroadcasterSubscriptionsResponse>("/subscriptions", ApiVersion.Helix, list2, accessToken, null, null);
		}
	}
}
