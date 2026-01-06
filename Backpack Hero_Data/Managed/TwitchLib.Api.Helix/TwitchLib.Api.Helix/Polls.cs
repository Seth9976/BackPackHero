using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Polls.CreatePoll;
using TwitchLib.Api.Helix.Models.Polls.EndPoll;
using TwitchLib.Api.Helix.Models.Polls.GetPolls;

namespace TwitchLib.Api.Helix
{
	// Token: 0x02000017 RID: 23
	public class Polls : ApiBase
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00004B87 File Offset: 0x00002D87
		public Polls(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
			: base(settings, rateLimiter, http)
		{
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004B94 File Offset: 0x00002D94
		public Task<GetPollsResponse> GetPollsAsync(string broadcasterId, List<string> ids = null, string after = null, int first = 20, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			list.Add(new KeyValuePair<string, string>("first", first.ToString()));
			List<KeyValuePair<string, string>> list2 = list;
			if (ids != null && ids.Count > 0)
			{
				list2.AddRange(Enumerable.Select<string, KeyValuePair<string, string>>(ids, (string id) => new KeyValuePair<string, string>("id", id)));
			}
			if (!string.IsNullOrWhiteSpace(accessToken))
			{
				list2.Add(new KeyValuePair<string, string>("after", after));
			}
			return base.TwitchGetGenericAsync<GetPollsResponse>("/polls", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004C31 File Offset: 0x00002E31
		public Task<CreatePollResponse> CreatePollAsync(CreatePollRequest request, string accessToken = null)
		{
			return base.TwitchPostGenericAsync<CreatePollResponse>("/polls", ApiVersion.Helix, JsonConvert.SerializeObject(request), null, accessToken, null, null);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004C4C File Offset: 0x00002E4C
		public Task<EndPollResponse> EndPollAsync(string broadcasterId, string id, PollStatusEnum status, string accessToken = null)
		{
			JObject jobject = new JObject();
			jobject["broadcaster_id"] = broadcasterId;
			jobject["id"] = id;
			jobject["status"] = status.ToString();
			JObject jobject2 = jobject;
			return base.TwitchPatchGenericAsync<EndPollResponse>("/polls", ApiVersion.Helix, jobject2.ToString(), null, accessToken, null, null);
		}
	}
}
