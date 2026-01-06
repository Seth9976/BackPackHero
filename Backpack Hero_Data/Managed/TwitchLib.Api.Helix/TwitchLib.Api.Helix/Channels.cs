using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Channels.GetChannelEditors;
using TwitchLib.Api.Helix.Models.Channels.GetChannelInformation;
using TwitchLib.Api.Helix.Models.Channels.GetChannelVIPs;
using TwitchLib.Api.Helix.Models.Channels.ModifyChannelInformation;

namespace TwitchLib.Api.Helix
{
	// Token: 0x0200000B RID: 11
	public class Channels : ApiBase
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002CEC File Offset: 0x00000EEC
		public Channels(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
			: base(settings, rateLimiter, http)
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002CF8 File Offset: 0x00000EF8
		public Task<GetChannelInformationResponse> GetChannelInformationAsync(string broadcasterId, string accessToken = null)
		{
			if (string.IsNullOrEmpty(broadcasterId))
			{
				throw new BadParameterException("broadcasterId must be set");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchGetGenericAsync<GetChannelInformationResponse>("/channels", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002D40 File Offset: 0x00000F40
		public Task ModifyChannelInformationAsync(string broadcasterId, ModifyChannelInformationRequest request, string accessToken = null)
		{
			if (string.IsNullOrEmpty(broadcasterId))
			{
				throw new BadParameterException("broadcasterId must be set");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchPatchAsync("/channels", ApiVersion.Helix, JsonConvert.SerializeObject(request), list2, accessToken, null, null);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002D90 File Offset: 0x00000F90
		public Task<GetChannelEditorsResponse> GetChannelEditorsAsync(string broadcasterId, string accessToken = null)
		{
			if (string.IsNullOrEmpty(broadcasterId))
			{
				throw new BadParameterException("broadcasterId must be set");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchGetGenericAsync<GetChannelEditorsResponse>("/channels/editors", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002DD8 File Offset: 0x00000FD8
		public Task<GetChannelVIPsResponse> GetVIPsAsync(string broadcasterId, List<string> userIds = null, int first = 20, string after = null, string accessToken = null)
		{
			if (string.IsNullOrEmpty(broadcasterId))
			{
				throw new BadParameterException("broadcasterId must be set");
			}
			if ((first > 100) & (first <= 0))
			{
				throw new BadParameterException("first must be greater than 0 and less then 101");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			list.Add(new KeyValuePair<string, string>("first", first.ToString()));
			List<KeyValuePair<string, string>> list2 = list;
			if (userIds != null)
			{
				if (userIds.Count == 0)
				{
					throw new BadParameterException("userIds must contain at least 1 userId if a list is included in the call");
				}
				list2.AddRange(Enumerable.Select<string, KeyValuePair<string, string>>(userIds, (string userId) => new KeyValuePair<string, string>("userId", userId)));
			}
			if (!string.IsNullOrWhiteSpace(after))
			{
				list2.Add(new KeyValuePair<string, string>("after", after));
			}
			return base.TwitchGetGenericAsync<GetChannelVIPsResponse>("/channels/vips", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002EB0 File Offset: 0x000010B0
		public Task AddChannelVIPAsync(string broadcasterId, string userId, string accessToken = null)
		{
			if (string.IsNullOrEmpty(broadcasterId))
			{
				throw new BadParameterException("broadcasterId must be set");
			}
			if (string.IsNullOrEmpty(userId))
			{
				throw new BadParameterException("userId must be set");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			list.Add(new KeyValuePair<string, string>("user_id", userId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchPostAsync("/channels/vips", ApiVersion.Helix, null, list2, accessToken, null, null);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002F1C File Offset: 0x0000111C
		public Task RemoveChannelVIPAsync(string broadcasterId, string userId, string accessToken = null)
		{
			if (string.IsNullOrEmpty(broadcasterId))
			{
				throw new BadParameterException("broadcasterId must be set");
			}
			if (string.IsNullOrEmpty(userId))
			{
				throw new BadParameterException("userId must be set");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			list.Add(new KeyValuePair<string, string>("user_id", userId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchDeleteAsync("/channels/vips", ApiVersion.Helix, list2, accessToken, null, null);
		}
	}
}
