using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Chat;
using TwitchLib.Api.Helix.Models.Chat.Badges.GetChannelChatBadges;
using TwitchLib.Api.Helix.Models.Chat.Badges.GetGlobalChatBadges;
using TwitchLib.Api.Helix.Models.Chat.ChatSettings;
using TwitchLib.Api.Helix.Models.Chat.Emotes.GetChannelEmotes;
using TwitchLib.Api.Helix.Models.Chat.Emotes.GetEmoteSets;
using TwitchLib.Api.Helix.Models.Chat.Emotes.GetGlobalEmotes;
using TwitchLib.Api.Helix.Models.Chat.GetChatters;
using TwitchLib.Api.Helix.Models.Chat.GetUserChatColor;

namespace TwitchLib.Api.Helix
{
	// Token: 0x0200000D RID: 13
	public class Chat : ApiBase
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00002FDB File Offset: 0x000011DB
		public Chat(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
			: base(settings, rateLimiter, http)
		{
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002FE8 File Offset: 0x000011E8
		public Task<GetChannelChatBadgesResponse> GetChannelChatBadgesAsync(string broadcasterId, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchGetGenericAsync<GetChannelChatBadgesResponse>("/chat/badges", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000301C File Offset: 0x0000121C
		public Task<GetGlobalChatBadgesResponse> GetGlobalChatBadgesAsync(string accessToken = null)
		{
			return base.TwitchGetGenericAsync<GetGlobalChatBadgesResponse>("/chat/badges/global", ApiVersion.Helix, null, accessToken, null, null);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003030 File Offset: 0x00001230
		public Task<GetChattersResponse> GetChattersAsync(string broadcasterId, string moderatorId, int first = 100, string after = null, string accessToken = null)
		{
			if (string.IsNullOrWhiteSpace(broadcasterId))
			{
				throw new BadParameterException("broadcasterId cannot be null/empty/whitespace");
			}
			if (string.IsNullOrWhiteSpace(moderatorId))
			{
				throw new BadParameterException("moderatorId cannot be null/empty/whitespace");
			}
			if (first < 1 || first > 1000)
			{
				throw new BadParameterException("first cannot be less than 1 or greater than 1000");
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
			return base.TwitchGetGenericAsync<GetChattersResponse>("/chat/chatters", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000030E8 File Offset: 0x000012E8
		public Task<GetChannelEmotesResponse> GetChannelEmotesAsync(string broadcasterId, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchGetGenericAsync<GetChannelEmotesResponse>("/chat/emotes", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000311C File Offset: 0x0000131C
		public Task<GetEmoteSetsResponse> GetEmoteSetsAsync(List<string> emoteSetIds, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.AddRange(Enumerable.Select<string, KeyValuePair<string, string>>(emoteSetIds, (string emoteSetId) => new KeyValuePair<string, string>("emote_set_id", emoteSetId)));
			return base.TwitchGetGenericAsync<GetEmoteSetsResponse>("/chat/emotes/set", ApiVersion.Helix, list, accessToken, null, null);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000316A File Offset: 0x0000136A
		public Task<GetGlobalEmotesResponse> GetGlobalEmotesAsync(string accessToken = null)
		{
			return base.TwitchGetGenericAsync<GetGlobalEmotesResponse>("/chat/emotes/global", ApiVersion.Helix, null, accessToken, null, null);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000317C File Offset: 0x0000137C
		public Task<GetChatSettingsResponse> GetChatSettingsAsync(string broadcasterId, string moderatorId, string accessToken = null)
		{
			if (string.IsNullOrEmpty(broadcasterId))
			{
				throw new BadParameterException("broadcasterId must be set");
			}
			if (string.IsNullOrEmpty(moderatorId))
			{
				throw new BadParameterException("moderatorId must be set");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			list.Add(new KeyValuePair<string, string>("moderator_id", moderatorId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchGetGenericAsync<GetChatSettingsResponse>("/chat/settings", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000031E8 File Offset: 0x000013E8
		public Task<UpdateChatSettingsResponse> UpdateChatSettingsAsync(string broadcasterId, string moderatorId, ChatSettings settings, string accessToken = null)
		{
			if (string.IsNullOrEmpty(broadcasterId))
			{
				throw new BadParameterException("broadcasterId must be set");
			}
			if (string.IsNullOrEmpty(moderatorId))
			{
				throw new BadParameterException("moderatorId must be set");
			}
			if (settings == null)
			{
				throw new BadParameterException("settings must be set");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			list.Add(new KeyValuePair<string, string>("moderator_id", moderatorId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchPatchGenericAsync<UpdateChatSettingsResponse>("/chat/settings", ApiVersion.Helix, JsonConvert.SerializeObject(settings), list2, accessToken, null, null);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003268 File Offset: 0x00001468
		public Task SendChatAnnouncementAsync(string broadcasterId, string moderatorId, string message, AnnouncementColors color = null, string accessToken = null)
		{
			if (string.IsNullOrEmpty(broadcasterId))
			{
				throw new BadParameterException("broadcasterId must be set");
			}
			if (string.IsNullOrEmpty(moderatorId))
			{
				throw new BadParameterException("moderatorId must be set");
			}
			if (message == null)
			{
				throw new BadParameterException("message must be set");
			}
			if (message.Length > 500)
			{
				throw new BadParameterException("message length must be less than or equal to 500 characters");
			}
			if (color == null)
			{
				color = AnnouncementColors.Primary;
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			list.Add(new KeyValuePair<string, string>("moderator_id", moderatorId));
			List<KeyValuePair<string, string>> list2 = list;
			JObject jobject = new JObject();
			jobject["message"] = message;
			jobject["color"] = color.Value;
			JObject jobject2 = jobject;
			return base.TwitchPostAsync("/chat/announcements", ApiVersion.Helix, jobject2.ToString(), list2, accessToken, null, null);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000333C File Offset: 0x0000153C
		public Task UpdateUserChatColorAsync(string userId, UserColors color, string accessToken = null)
		{
			if (string.IsNullOrEmpty(userId))
			{
				throw new BadParameterException("userId must be set");
			}
			if (string.IsNullOrEmpty(color.Value))
			{
				throw new BadParameterException("color must be set");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("user_id", userId));
			list.Add(new KeyValuePair<string, string>("color", color.Value));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchPostAsync("/chat/color", ApiVersion.Helix, null, list2, accessToken, null, null);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000033B4 File Offset: 0x000015B4
		public Task UpdateUserChatColorAsync(string userId, string colorHex, string accessToken = null)
		{
			if (string.IsNullOrEmpty(userId))
			{
				throw new BadParameterException("userId must be set");
			}
			if (string.IsNullOrEmpty(colorHex))
			{
				throw new BadParameterException("colorHex must be set");
			}
			if (colorHex.Length != 6)
			{
				throw new BadParameterException("colorHex length must be equal to 6 characters \"######\"");
			}
			string text = HttpUtility.UrlEncode("#" + colorHex);
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("user_id", userId));
			list.Add(new KeyValuePair<string, string>("color", text));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchPostAsync("/chat/color", ApiVersion.Helix, null, list2, accessToken, null, null);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003448 File Offset: 0x00001648
		public Task<GetUserChatColorResponse> GetUserChatColorAsync(List<string> userIds, string accessToken = null)
		{
			if (userIds.Count == 0)
			{
				throw new BadParameterException("userIds must contain at least 1 userId");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			foreach (string text in userIds)
			{
				if (string.IsNullOrEmpty(text))
				{
					throw new BadParameterException("userId must be set");
				}
				list.Add(new KeyValuePair<string, string>("user_id", text));
			}
			return base.TwitchGetGenericAsync<GetUserChatColorResponse>("/chat/color", ApiVersion.Helix, list, accessToken, null, null);
		}
	}
}
