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
using TwitchLib.Api.Helix.Models.Moderation.AutomodSettings;
using TwitchLib.Api.Helix.Models.Moderation.BanUser;
using TwitchLib.Api.Helix.Models.Moderation.BlockedTerms;
using TwitchLib.Api.Helix.Models.Moderation.CheckAutoModStatus;
using TwitchLib.Api.Helix.Models.Moderation.CheckAutoModStatus.Request;
using TwitchLib.Api.Helix.Models.Moderation.GetBannedEvents;
using TwitchLib.Api.Helix.Models.Moderation.GetBannedUsers;
using TwitchLib.Api.Helix.Models.Moderation.GetModeratorEvents;
using TwitchLib.Api.Helix.Models.Moderation.GetModerators;

namespace TwitchLib.Api.Helix
{
	// Token: 0x02000016 RID: 22
	public class Moderation : ApiBase
	{
		// Token: 0x0600007C RID: 124 RVA: 0x000041E4 File Offset: 0x000023E4
		public Moderation(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
			: base(settings, rateLimiter, http)
		{
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000041F0 File Offset: 0x000023F0
		public Task ManageHeldAutoModMessagesAsync(string userId, string msgId, ManageHeldAutoModMessageActionEnum action, string accessToken = null)
		{
			if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(msgId))
			{
				throw new BadParameterException("userId and msgId cannot be null and must be greater than 0 length");
			}
			JObject jobject = new JObject();
			jobject["user_id"] = userId;
			jobject["msg_id"] = msgId;
			jobject["action"] = action.ToString().ToUpper();
			JObject jobject2 = jobject;
			return base.TwitchPostAsync("/moderation/automod/message", ApiVersion.Helix, jobject2.ToString(), null, accessToken, null, null);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000427C File Offset: 0x0000247C
		public Task<CheckAutoModStatusResponse> CheckAutoModStatusAsync(List<Message> messages, string broadcasterId, string accessToken = null)
		{
			if (messages == null || messages.Count == 0)
			{
				throw new BadParameterException("messages cannot be null and must be greater than 0 length");
			}
			if (string.IsNullOrWhiteSpace(broadcasterId))
			{
				throw new BadParameterException("broadcasterId cannot be null/empty/whitespace");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			List<KeyValuePair<string, string>> list2 = list;
			MessageRequest messageRequest = new MessageRequest
			{
				Messages = messages.ToArray()
			};
			return base.TwitchPostGenericAsync<CheckAutoModStatusResponse>("/moderation/enforcements/status", ApiVersion.Helix, JsonConvert.SerializeObject(messageRequest), list2, accessToken, null, null);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000042F4 File Offset: 0x000024F4
		public Task<GetBannedEventsResponse> GetBannedEventsAsync(string broadcasterId, List<string> userIds = null, string after = null, int first = 20, string accessToken = null)
		{
			if (string.IsNullOrWhiteSpace(broadcasterId))
			{
				throw new BadParameterException("broadcasterId cannot be null/empty/whitespace");
			}
			if (first < 1 || first > 100)
			{
				throw new BadParameterException("first cannot be less than 1 or greater than 100");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			List<KeyValuePair<string, string>> list2 = list;
			if (userIds != null && userIds.Count > 0)
			{
				list2.AddRange(Enumerable.Select<string, KeyValuePair<string, string>>(userIds, (string userId) => new KeyValuePair<string, string>("user_id", userId)));
			}
			if (string.IsNullOrWhiteSpace(after))
			{
				list2.Add(new KeyValuePair<string, string>("after", after));
			}
			list2.Add(new KeyValuePair<string, string>("first", first.ToString()));
			return base.TwitchGetGenericAsync<GetBannedEventsResponse>("/moderation/banned/events", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000043BC File Offset: 0x000025BC
		public Task<GetBannedUsersResponse> GetBannedUsersAsync(string broadcasterId, List<string> userIds = null, int first = 20, string after = null, string before = null, string accessToken = null)
		{
			if (string.IsNullOrEmpty(broadcasterId))
			{
				throw new BadParameterException("broadcasterId cannot be null/empty/whitespace");
			}
			if (first < 1 || first > 100)
			{
				throw new BadParameterException("first cannot be less than 1 or greater than 100");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			list.Add(new KeyValuePair<string, string>("first", first.ToString()));
			List<KeyValuePair<string, string>> list2 = list;
			if (userIds != null && userIds.Count > 0)
			{
				list2.AddRange(Enumerable.Select<string, KeyValuePair<string, string>>(userIds, (string userId) => new KeyValuePair<string, string>("user_id", userId)));
			}
			if (!string.IsNullOrWhiteSpace(after))
			{
				list2.Add(new KeyValuePair<string, string>("after", after));
			}
			if (!string.IsNullOrWhiteSpace(before))
			{
				list2.Add(new KeyValuePair<string, string>("before", before));
			}
			return base.TwitchGetGenericAsync<GetBannedUsersResponse>("/moderation/banned", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000449C File Offset: 0x0000269C
		public Task<GetModeratorsResponse> GetModeratorsAsync(string broadcasterId, List<string> userIds = null, int first = 20, string after = null, string accessToken = null)
		{
			if (string.IsNullOrWhiteSpace(broadcasterId))
			{
				throw new BadParameterException("broadcasterId cannot be null/empty/whitespace");
			}
			if (first > 100 || first < 1)
			{
				throw new BadParameterException("first must be greater than 0 and less than 101");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			list.Add(new KeyValuePair<string, string>("first", first.ToString()));
			List<KeyValuePair<string, string>> list2 = list;
			if (userIds != null && userIds.Count > 0)
			{
				list2.AddRange(Enumerable.Select<string, KeyValuePair<string, string>>(userIds, (string userId) => new KeyValuePair<string, string>("user_id", userId)));
			}
			if (!string.IsNullOrWhiteSpace(after))
			{
				list2.Add(new KeyValuePair<string, string>("after", after));
			}
			return base.TwitchGetGenericAsync<GetModeratorsResponse>("/moderation/moderators", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004564 File Offset: 0x00002764
		public Task<GetModeratorEventsResponse> GetModeratorEventsAsync(string broadcasterId, List<string> userIds = null, string accessToken = null)
		{
			if (string.IsNullOrWhiteSpace(broadcasterId))
			{
				throw new BadParameterException("broadcasterId cannot be null/empty/whitespace");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			List<KeyValuePair<string, string>> list2 = list;
			if (userIds != null && userIds.Count > 0)
			{
				list2.AddRange(Enumerable.Select<string, KeyValuePair<string, string>>(userIds, (string userId) => new KeyValuePair<string, string>("user_id", userId)));
			}
			return base.TwitchGetGenericAsync<GetModeratorEventsResponse>("/moderation/moderators/events", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000045E4 File Offset: 0x000027E4
		public Task<BanUserResponse> BanUserAsync(string broadcasterId, string moderatorId, BanUserRequest banUserRequest, string accessToken = null)
		{
			if (string.IsNullOrEmpty(broadcasterId))
			{
				throw new BadParameterException("broadcasterId must be set");
			}
			if (string.IsNullOrEmpty(moderatorId))
			{
				throw new BadParameterException("moderatorId must be set");
			}
			if (banUserRequest == null)
			{
				throw new BadParameterException("banUserRequest cannot be null");
			}
			if (string.IsNullOrWhiteSpace(banUserRequest.UserId))
			{
				throw new BadParameterException("banUserRequest.UserId must be set");
			}
			if (banUserRequest.Reason == null)
			{
				throw new BadParameterException("banUserRequest.Reason cannot be null and must be set to at least an empty string");
			}
			if (banUserRequest.Duration != null && (banUserRequest.Duration.Value <= 0 || banUserRequest.Duration.Value > 1209600))
			{
				throw new BadParameterException("banUserRequest.Duration has to be between including 1 and including 1209600");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			list.Add(new KeyValuePair<string, string>("moderator_id", moderatorId));
			List<KeyValuePair<string, string>> list2 = list;
			var <>f__AnonymousType = new
			{
				data = banUserRequest
			};
			return base.TwitchPostGenericAsync<BanUserResponse>("/moderation/bans", ApiVersion.Helix, JsonConvert.SerializeObject(<>f__AnonymousType), list2, accessToken, null, null);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000046D8 File Offset: 0x000028D8
		public Task UnbanUserAsync(string broadcasterId, string moderatorId, string userId, string accessToken = null)
		{
			if (string.IsNullOrWhiteSpace(broadcasterId))
			{
				throw new BadParameterException("broadcasterId must be set");
			}
			if (string.IsNullOrWhiteSpace(moderatorId))
			{
				throw new BadParameterException("moderatorId must be set");
			}
			if (string.IsNullOrWhiteSpace(userId))
			{
				throw new BadParameterException("userId must be set");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			list.Add(new KeyValuePair<string, string>("moderator_id", moderatorId));
			list.Add(new KeyValuePair<string, string>("user_id", userId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchDeleteAsync("/moderation/bans", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004768 File Offset: 0x00002968
		public Task<GetAutomodSettingsResponse> GetAutomodSettingsAsync(string broadcasterId, string moderatorId, string accessToken = null)
		{
			if (string.IsNullOrWhiteSpace(broadcasterId))
			{
				throw new BadParameterException("broadcasterId must be set");
			}
			if (string.IsNullOrWhiteSpace(moderatorId))
			{
				throw new BadParameterException("moderatorId must be set");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			list.Add(new KeyValuePair<string, string>("moderator_id", moderatorId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchGetGenericAsync<GetAutomodSettingsResponse>("/moderation/automod/settings", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000047D4 File Offset: 0x000029D4
		public Task<UpdateAutomodSettingsResponse> UpdateAutomodSettingsAsync(string broadcasterId, string moderatorId, AutomodSettings settings, string accessToken = null)
		{
			if (string.IsNullOrWhiteSpace(broadcasterId))
			{
				throw new BadParameterException("broadcasterId must be set");
			}
			if (string.IsNullOrWhiteSpace(moderatorId))
			{
				throw new BadParameterException("moderatorId must be set");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			list.Add(new KeyValuePair<string, string>("moderator_id", moderatorId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchPutGenericAsync<UpdateAutomodSettingsResponse>("/moderation/automod/settings", ApiVersion.Helix, JsonConvert.SerializeObject(settings), list2, accessToken, null, null);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004848 File Offset: 0x00002A48
		public Task<GetBlockedTermsResponse> GetBlockedTermsAsync(string broadcasterId, string moderatorId, string after = null, int first = 20, string accessToken = null)
		{
			if (string.IsNullOrWhiteSpace(broadcasterId))
			{
				throw new BadParameterException("broadcasterId must be set");
			}
			if (string.IsNullOrWhiteSpace(moderatorId))
			{
				throw new BadParameterException("moderatorId must be set");
			}
			if (first < 1 || first > 100)
			{
				throw new BadParameterException("first must be greater than 0 and less than 101");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			list.Add(new KeyValuePair<string, string>("moderator_id", moderatorId));
			list.Add(new KeyValuePair<string, string>("first", first.ToString()));
			List<KeyValuePair<string, string>> list2 = list;
			if (!string.IsNullOrWhiteSpace(after))
			{
				list2.Add(new KeyValuePair<string, string>("after", after));
			}
			return base.TwitchGetGenericAsync<GetBlockedTermsResponse>("/moderation/blocked_terms", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000048FC File Offset: 0x00002AFC
		public Task<AddBlockedTermResponse> AddBlockedTermAsync(string broadcasterId, string moderatorId, string term, string accessToken = null)
		{
			if (string.IsNullOrWhiteSpace(broadcasterId))
			{
				throw new BadParameterException("broadcasterId must be set");
			}
			if (string.IsNullOrWhiteSpace(moderatorId))
			{
				throw new BadParameterException("moderatorId must be set");
			}
			if (string.IsNullOrWhiteSpace(term))
			{
				throw new BadParameterException("term must be set");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			list.Add(new KeyValuePair<string, string>("moderator_id", moderatorId));
			List<KeyValuePair<string, string>> list2 = list;
			JObject jobject = new JObject();
			jobject["text"] = term;
			JObject jobject2 = jobject;
			return base.TwitchPostGenericAsync<AddBlockedTermResponse>("/moderation/blocked_terms", ApiVersion.Helix, jobject2.ToString(), list2, accessToken, null, null);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004998 File Offset: 0x00002B98
		public Task DeleteBlockedTermAsync(string broadcasterId, string moderatorId, string termId, string accessToken = null)
		{
			if (string.IsNullOrWhiteSpace(broadcasterId))
			{
				throw new BadParameterException("broadcasterId must be set");
			}
			if (string.IsNullOrWhiteSpace(moderatorId))
			{
				throw new BadParameterException("moderatorId must be set");
			}
			if (string.IsNullOrWhiteSpace(termId))
			{
				throw new BadParameterException("termId must be set");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			list.Add(new KeyValuePair<string, string>("moderator_id", moderatorId));
			list.Add(new KeyValuePair<string, string>("id", termId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchDeleteAsync("/moderation/blocked_terms", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004A28 File Offset: 0x00002C28
		public Task DeleteChatMessagesAsync(string broadcasterId, string moderatorId, string messageId = null, string accessToken = null)
		{
			if (string.IsNullOrWhiteSpace(broadcasterId))
			{
				throw new BadParameterException("broadcasterId must be set");
			}
			if (string.IsNullOrWhiteSpace(moderatorId))
			{
				throw new BadParameterException("moderatorId must be set");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			list.Add(new KeyValuePair<string, string>("moderator_id", moderatorId));
			List<KeyValuePair<string, string>> list2 = list;
			if (!string.IsNullOrWhiteSpace(messageId))
			{
				list2.Add(new KeyValuePair<string, string>("message_id", messageId));
			}
			return base.TwitchDeleteAsync("/moderation/chat", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004AB0 File Offset: 0x00002CB0
		public Task AddChannelModeratorAsync(string broadcasterId, string userId, string accessToken = null)
		{
			if (string.IsNullOrWhiteSpace(broadcasterId))
			{
				throw new BadParameterException("broadcasterId must be set");
			}
			if (string.IsNullOrWhiteSpace(userId))
			{
				throw new BadParameterException("userId must be set");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			list.Add(new KeyValuePair<string, string>("user_id", userId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchPostAsync("/moderation/moderators", ApiVersion.Helix, null, list2, accessToken, null, null);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004B1C File Offset: 0x00002D1C
		public Task DeleteChannelModeratorAsync(string broadcasterId, string userId, string accessToken = null)
		{
			if (string.IsNullOrWhiteSpace(broadcasterId))
			{
				throw new BadParameterException("broadcasterId must be set");
			}
			if (string.IsNullOrWhiteSpace(userId))
			{
				throw new BadParameterException("userId must be set");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			list.Add(new KeyValuePair<string, string>("user_id", userId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchDeleteAsync("/moderation/moderators", ApiVersion.Helix, list2, accessToken, null, null);
		}
	}
}
