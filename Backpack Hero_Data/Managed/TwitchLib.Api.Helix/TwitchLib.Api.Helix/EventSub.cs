using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.EventSub;

namespace TwitchLib.Api.Helix
{
	// Token: 0x02000010 RID: 16
	public class EventSub : ApiBase
	{
		// Token: 0x06000050 RID: 80 RVA: 0x0000386D File Offset: 0x00001A6D
		public EventSub(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
			: base(settings, rateLimiter, http)
		{
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003878 File Offset: 0x00001A78
		public Task<CreateEventSubSubscriptionResponse> CreateEventSubSubscriptionAsync(string type, string version, Dictionary<string, string> condition, EventSubTransportMethod method, string websocketSessionId = null, string webhookCallback = null, string webhookSecret = null, string clientId = null, string accessToken = null)
		{
			if (string.IsNullOrEmpty(type))
			{
				throw new BadParameterException("type must be set");
			}
			if (string.IsNullOrEmpty(version))
			{
				throw new BadParameterException("version must be set");
			}
			if (condition == null || condition.Count == 0)
			{
				throw new BadParameterException("condition must be set");
			}
			if (method != EventSubTransportMethod.Webhook)
			{
				if (method != EventSubTransportMethod.Websocket)
				{
					throw new ArgumentOutOfRangeException("method", method, null);
				}
				if (string.IsNullOrWhiteSpace(websocketSessionId))
				{
					throw new BadParameterException("websocketSessionId must be set");
				}
				var <>f__AnonymousType = new
				{
					type = type,
					version = version,
					condition = condition,
					transport = new
					{
						method = method.ToString().ToLowerInvariant(),
						session_id = websocketSessionId
					}
				};
				return base.TwitchPostGenericAsync<CreateEventSubSubscriptionResponse>("/eventsub/subscriptions", ApiVersion.Helix, JsonConvert.SerializeObject(<>f__AnonymousType), null, accessToken, clientId, null);
			}
			else
			{
				if (string.IsNullOrWhiteSpace(webhookCallback))
				{
					throw new BadParameterException("webhookCallback must be set");
				}
				if (webhookSecret == null || webhookSecret.Length < 10 || webhookSecret.Length > 100)
				{
					throw new BadParameterException("webhookSecret must be set, and be between 10 (inclusive) and 100 (inclusive)");
				}
				var <>f__AnonymousType2 = new
				{
					type = type,
					version = version,
					condition = condition,
					transport = new
					{
						method = method.ToString().ToLowerInvariant(),
						callback = webhookCallback,
						secret = webhookSecret
					}
				};
				return base.TwitchPostGenericAsync<CreateEventSubSubscriptionResponse>("/eventsub/subscriptions", ApiVersion.Helix, JsonConvert.SerializeObject(<>f__AnonymousType2), null, accessToken, clientId, null);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000039A8 File Offset: 0x00001BA8
		public Task<GetEventSubSubscriptionsResponse> GetEventSubSubscriptionsAsync(string status = null, string type = null, string userId = null, string after = null, string clientId = null, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			if (!string.IsNullOrWhiteSpace(status))
			{
				list.Add(new KeyValuePair<string, string>("status", status));
			}
			if (!string.IsNullOrWhiteSpace(type))
			{
				list.Add(new KeyValuePair<string, string>("type", type));
			}
			if (!string.IsNullOrWhiteSpace(userId))
			{
				list.Add(new KeyValuePair<string, string>("user_id", userId));
			}
			if (!string.IsNullOrWhiteSpace(after))
			{
				list.Add(new KeyValuePair<string, string>("after", after));
			}
			return base.TwitchGetGenericAsync<GetEventSubSubscriptionsResponse>("/eventsub/subscriptions", ApiVersion.Helix, list, accessToken, clientId, null);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003A34 File Offset: 0x00001C34
		public Task<bool> DeleteEventSubSubscriptionAsync(string id, string clientId = null, string accessToken = null)
		{
			EventSub.<DeleteEventSubSubscriptionAsync>d__3 <DeleteEventSubSubscriptionAsync>d__;
			<DeleteEventSubSubscriptionAsync>d__.<>4__this = this;
			<DeleteEventSubSubscriptionAsync>d__.id = id;
			<DeleteEventSubSubscriptionAsync>d__.clientId = clientId;
			<DeleteEventSubSubscriptionAsync>d__.accessToken = accessToken;
			<DeleteEventSubSubscriptionAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<DeleteEventSubSubscriptionAsync>d__.<>1__state = -1;
			<DeleteEventSubSubscriptionAsync>d__.<>t__builder.Start<EventSub.<DeleteEventSubSubscriptionAsync>d__3>(ref <DeleteEventSubSubscriptionAsync>d__);
			return <DeleteEventSubSubscriptionAsync>d__.<>t__builder.Task;
		}
	}
}
