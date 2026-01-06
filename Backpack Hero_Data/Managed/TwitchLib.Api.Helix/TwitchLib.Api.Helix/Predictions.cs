using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Predictions.CreatePrediction;
using TwitchLib.Api.Helix.Models.Predictions.EndPrediction;
using TwitchLib.Api.Helix.Models.Predictions.GetPredictions;

namespace TwitchLib.Api.Helix
{
	// Token: 0x02000018 RID: 24
	public class Predictions : ApiBase
	{
		// Token: 0x06000091 RID: 145 RVA: 0x00004CB5 File Offset: 0x00002EB5
		public Predictions(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
			: base(settings, rateLimiter, http)
		{
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004CC0 File Offset: 0x00002EC0
		public Task<GetPredictionsResponse> GetPredictionsAsync(string broadcasterId, List<string> ids = null, string after = null, int first = 20, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			list.Add(new KeyValuePair<string, string>("first", first.ToString()));
			List<KeyValuePair<string, string>> list2 = list;
			if (ids != null && ids.Count > 0)
			{
				list2.AddRange(Enumerable.Select<string, KeyValuePair<string, string>>(ids, (string id) => new KeyValuePair<string, string>("id", id)));
			}
			if (!string.IsNullOrWhiteSpace(after))
			{
				list2.Add(new KeyValuePair<string, string>("after", after));
			}
			return base.TwitchGetGenericAsync<GetPredictionsResponse>("/predictions", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004D5C File Offset: 0x00002F5C
		public Task<CreatePredictionResponse> CreatePredictionAsync(CreatePredictionRequest request, string accessToken = null)
		{
			return base.TwitchPostGenericAsync<CreatePredictionResponse>("/predictions", ApiVersion.Helix, JsonConvert.SerializeObject(request), null, accessToken, null, null);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004D74 File Offset: 0x00002F74
		public Task<EndPredictionResponse> EndPredictionAsync(string broadcasterId, string id, PredictionEndStatus status, string winningOutcomeId = null, string accessToken = null)
		{
			JObject jobject = new JObject();
			jobject["broadcaster_id"] = broadcasterId;
			jobject["id"] = id;
			jobject["status"] = status.ToString();
			JObject jobject2 = jobject;
			if (!string.IsNullOrWhiteSpace(winningOutcomeId))
			{
				jobject2["winning_outcome_id"] = winningOutcomeId;
			}
			return base.TwitchPatchGenericAsync<EndPredictionResponse>("/predictions", ApiVersion.Helix, jobject2.ToString(), null, accessToken, null, null);
		}
	}
}
