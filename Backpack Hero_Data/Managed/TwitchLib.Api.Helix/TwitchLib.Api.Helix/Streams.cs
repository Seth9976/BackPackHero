using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Streams.CreateStreamMarker;
using TwitchLib.Api.Helix.Models.Streams.GetFollowedStreams;
using TwitchLib.Api.Helix.Models.Streams.GetStreamKey;
using TwitchLib.Api.Helix.Models.Streams.GetStreamMarkers;
using TwitchLib.Api.Helix.Models.Streams.GetStreams;
using TwitchLib.Api.Helix.Models.Streams.GetStreamTags;

namespace TwitchLib.Api.Helix
{
	// Token: 0x0200001D RID: 29
	public class Streams : ApiBase
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x000053D5 File Offset: 0x000035D5
		public Streams(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
			: base(settings, rateLimiter, http)
		{
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000053E0 File Offset: 0x000035E0
		public Task<GetStreamsResponse> GetStreamsAsync(string after = null, int first = 20, List<string> gameIds = null, List<string> languages = null, List<string> userIds = null, List<string> userLogins = null, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("first", first.ToString()));
			List<KeyValuePair<string, string>> list2 = list;
			if (!string.IsNullOrWhiteSpace(after))
			{
				list2.Add(new KeyValuePair<string, string>("after", after));
			}
			if (gameIds != null && gameIds.Count > 0)
			{
				list2.AddRange(Enumerable.Select<string, KeyValuePair<string, string>>(gameIds, (string gameId) => new KeyValuePair<string, string>("game_id", gameId)));
			}
			if (languages != null && languages.Count > 0)
			{
				list2.AddRange(Enumerable.Select<string, KeyValuePair<string, string>>(languages, (string language) => new KeyValuePair<string, string>("language", language)));
			}
			if (userIds != null && userIds.Count > 0)
			{
				list2.AddRange(Enumerable.Select<string, KeyValuePair<string, string>>(userIds, (string userId) => new KeyValuePair<string, string>("user_id", userId)));
			}
			if (userLogins != null && userLogins.Count > 0)
			{
				list2.AddRange(Enumerable.Select<string, KeyValuePair<string, string>>(userLogins, (string userLogin) => new KeyValuePair<string, string>("user_login", userLogin)));
			}
			return base.TwitchGetGenericAsync<GetStreamsResponse>("/streams", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000551C File Offset: 0x0000371C
		public Task<GetStreamTagsResponse> GetStreamTagsAsync(string broadcasterId, string accessToken = null)
		{
			if (string.IsNullOrWhiteSpace(broadcasterId))
			{
				throw new BadParameterException("BroadcasterId must be set");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchGetGenericAsync<GetStreamTagsResponse>("/streams/tags", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00005564 File Offset: 0x00003764
		public Task ReplaceStreamTagsAsync(string broadcasterId, List<string> tagIds = null, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			List<KeyValuePair<string, string>> list2 = list;
			string text = null;
			if (tagIds != null && tagIds.Count > 0)
			{
				JObject jobject = new JObject();
				jobject.Add("tag_ids", new JArray(tagIds));
				text = jobject.ToString();
			}
			return base.TwitchPutAsync("/streams/tags", ApiVersion.Helix, text, list2, accessToken, null, null);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000055C4 File Offset: 0x000037C4
		public Task<GetStreamKeyResponse> GetStreamKeyAsync(string broadcasterId, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchGetGenericAsync<GetStreamKeyResponse>("/streams/key", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000055F8 File Offset: 0x000037F8
		public Task<CreateStreamMarkerResponse> CreateStreamMarkerAsync(CreateStreamMarkerRequest request, string accessToken = null)
		{
			return base.TwitchPostGenericAsync<CreateStreamMarkerResponse>("/streams/markers", ApiVersion.Helix, JsonConvert.SerializeObject(request), null, accessToken, null, null);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00005610 File Offset: 0x00003810
		public Task<GetStreamMarkersResponse> GetStreamMarkersAsync(string userId = null, string videoId = null, int first = 20, string after = null, string accessToken = null)
		{
			if (string.IsNullOrWhiteSpace(userId) && string.IsNullOrWhiteSpace(videoId))
			{
				throw new BadParameterException("One of userId and videoId has to be specified");
			}
			if (!string.IsNullOrWhiteSpace(userId) && !string.IsNullOrWhiteSpace(videoId))
			{
				throw new BadParameterException("userId and videoId are mutually exclusive");
			}
			if (first < 1 || first > 100)
			{
				throw new BadParameterException("first cannot be less than 1 or greater than 100");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			if (!string.IsNullOrWhiteSpace(userId))
			{
				list.Add(new KeyValuePair<string, string>("user_id", userId));
			}
			if (!string.IsNullOrWhiteSpace(videoId))
			{
				list.Add(new KeyValuePair<string, string>("video_id", videoId));
			}
			list.Add(new KeyValuePair<string, string>("first", first.ToString()));
			if (!string.IsNullOrWhiteSpace(after))
			{
				list.Add(new KeyValuePair<string, string>("after", after));
			}
			return base.TwitchGetGenericAsync<GetStreamMarkersResponse>("/streams/markers", ApiVersion.Helix, list, accessToken, null, null);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000056E4 File Offset: 0x000038E4
		public Task<GetFollowedStreamsResponse> GetFollowedStreamsAsync(string userId, int first = 100, string after = null, string accessToken = null)
		{
			if (first < 1 || first > 100)
			{
				throw new BadParameterException("first cannot be less than 1 or greater than 100");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("user_id", userId));
			list.Add(new KeyValuePair<string, string>("first", first.ToString()));
			List<KeyValuePair<string, string>> list2 = list;
			if (!string.IsNullOrWhiteSpace(after))
			{
				list2.Add(new KeyValuePair<string, string>("after", after));
			}
			return base.TwitchGetGenericAsync<GetFollowedStreamsResponse>("/streams/followed", ApiVersion.Helix, list2, accessToken, null, null);
		}
	}
}
