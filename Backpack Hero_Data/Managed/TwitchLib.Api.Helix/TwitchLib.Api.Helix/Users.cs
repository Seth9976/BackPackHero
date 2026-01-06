using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Users.GetUserActiveExtensions;
using TwitchLib.Api.Helix.Models.Users.GetUserBlockList;
using TwitchLib.Api.Helix.Models.Users.GetUserExtensions;
using TwitchLib.Api.Helix.Models.Users.GetUserFollows;
using TwitchLib.Api.Helix.Models.Users.GetUsers;
using TwitchLib.Api.Helix.Models.Users.Internal;
using TwitchLib.Api.Helix.Models.Users.UpdateUserExtensions;

namespace TwitchLib.Api.Helix
{
	// Token: 0x02000021 RID: 33
	public class Users : ApiBase
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00005A2D File Offset: 0x00003C2D
		public Users(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
			: base(settings, rateLimiter, http)
		{
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00005A38 File Offset: 0x00003C38
		public Task<GetUserBlockListResponse> GetUserBlockListAsync(string broadcasterId, int first = 20, string after = null, string accessToken = null)
		{
			if (first > 100)
			{
				throw new BadParameterException(string.Format("Maximum allowed objects is 100 (you passed {0})", first));
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			list.Add(new KeyValuePair<string, string>("first", first.ToString()));
			List<KeyValuePair<string, string>> list2 = list;
			if (!string.IsNullOrWhiteSpace(after))
			{
				list2.Add(new KeyValuePair<string, string>("after", after));
			}
			return base.TwitchGetGenericAsync<GetUserBlockListResponse>("/users/blocks", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00005AB8 File Offset: 0x00003CB8
		public Task BlockUserAsync(string targetUserId, BlockUserSourceContextEnum? sourceContext = null, BlockUserReasonEnum? reason = null, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("target_user_id", targetUserId));
			List<KeyValuePair<string, string>> list2 = list;
			if (sourceContext != null)
			{
				list2.Add(new KeyValuePair<string, string>("source_context", sourceContext.Value.ToString().ToLower()));
			}
			if (reason != null)
			{
				list2.Add(new KeyValuePair<string, string>("reason", reason.Value.ToString().ToLower()));
			}
			return base.TwitchPutAsync("/users/blocks", ApiVersion.Helix, null, list2, accessToken, null, null);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005B54 File Offset: 0x00003D54
		public Task UnblockUserAsync(string targetUserId, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("target_user_id", targetUserId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchDeleteAsync("/user/blocks", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00005B88 File Offset: 0x00003D88
		public Task<GetUsersResponse> GetUsersAsync(List<string> ids = null, List<string> logins = null, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			if (ids != null && ids.Count > 0)
			{
				list.AddRange(Enumerable.Select<string, KeyValuePair<string, string>>(ids, (string id) => new KeyValuePair<string, string>("id", id)));
			}
			if (logins != null && logins.Count > 0)
			{
				list.AddRange(Enumerable.Select<string, KeyValuePair<string, string>>(logins, (string login) => new KeyValuePair<string, string>("login", login)));
			}
			return base.TwitchGetGenericAsync<GetUsersResponse>("/users", ApiVersion.Helix, list, accessToken, null, null);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00005C1C File Offset: 0x00003E1C
		public Task<GetUsersFollowsResponse> GetUsersFollowsAsync(string after = null, string before = null, int first = 20, string fromId = null, string toId = null, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("first", first.ToString()));
			List<KeyValuePair<string, string>> list2 = list;
			if (!string.IsNullOrWhiteSpace(after))
			{
				list2.Add(new KeyValuePair<string, string>("after", after));
			}
			if (!string.IsNullOrWhiteSpace(before))
			{
				list2.Add(new KeyValuePair<string, string>("before", before));
			}
			if (!string.IsNullOrWhiteSpace(fromId))
			{
				list2.Add(new KeyValuePair<string, string>("from_id", fromId));
			}
			if (!string.IsNullOrWhiteSpace(toId))
			{
				list2.Add(new KeyValuePair<string, string>("to_id", toId));
			}
			return base.TwitchGetGenericAsync<GetUsersFollowsResponse>("/users/follows", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00005CC0 File Offset: 0x00003EC0
		public Task UpdateUserAsync(string description, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("description", description));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchPutAsync("/users", ApiVersion.Helix, null, list2, accessToken, null, null);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005CF5 File Offset: 0x00003EF5
		public Task<GetUserExtensionsResponse> GetUserExtensionsAsync(string accessToken = null)
		{
			return base.TwitchGetGenericAsync<GetUserExtensionsResponse>("/users/extensions/list", ApiVersion.Helix, null, accessToken, null, null);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00005D08 File Offset: 0x00003F08
		public Task<GetUserActiveExtensionsResponse> GetUserActiveExtensionsAsync(string userid = null, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			if (!string.IsNullOrWhiteSpace(userid))
			{
				list.Add(new KeyValuePair<string, string>("user_id", userid));
			}
			return base.TwitchGetGenericAsync<GetUserActiveExtensionsResponse>("/users/extensions", ApiVersion.Helix, list, accessToken, null, null);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005D44 File Offset: 0x00003F44
		public Task<GetUserActiveExtensionsResponse> UpdateUserExtensionsAsync(IEnumerable<ExtensionSlot> userExtensionStates, string accessToken = null)
		{
			Dictionary<string, UserExtensionState> dictionary = new Dictionary<string, UserExtensionState>();
			Dictionary<string, UserExtensionState> dictionary2 = new Dictionary<string, UserExtensionState>();
			Dictionary<string, UserExtensionState> dictionary3 = new Dictionary<string, UserExtensionState>();
			foreach (ExtensionSlot extensionSlot in userExtensionStates)
			{
				switch (extensionSlot.Type)
				{
				case ExtensionType.Panel:
					dictionary.Add(extensionSlot.Slot, extensionSlot.UserExtensionState);
					break;
				case ExtensionType.Overlay:
					dictionary2.Add(extensionSlot.Slot, extensionSlot.UserExtensionState);
					break;
				case ExtensionType.Component:
					dictionary3.Add(extensionSlot.Slot, extensionSlot.UserExtensionState);
					break;
				default:
					throw new ArgumentOutOfRangeException("ExtensionType");
				}
			}
			JObject jobject = new JObject();
			UpdateUserExtensionsRequest updateUserExtensionsRequest = new UpdateUserExtensionsRequest();
			if (dictionary.Count > 0)
			{
				updateUserExtensionsRequest.Panel = dictionary;
			}
			if (dictionary2.Count > 0)
			{
				updateUserExtensionsRequest.Overlay = dictionary2;
			}
			if (dictionary3.Count > 0)
			{
				updateUserExtensionsRequest.Component = dictionary3;
			}
			jobject.Add(new JProperty("data", JObject.FromObject(updateUserExtensionsRequest)));
			string text = jobject.ToString();
			return base.TwitchPutGenericAsync<GetUserActiveExtensionsResponse>("/users/extensions", ApiVersion.Helix, text, null, accessToken, null, null);
		}
	}
}
