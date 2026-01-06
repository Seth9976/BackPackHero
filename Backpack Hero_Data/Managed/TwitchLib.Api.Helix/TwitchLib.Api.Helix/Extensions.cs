using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Extensions.LiveChannels;
using TwitchLib.Api.Helix.Models.Extensions.ReleasedExtensions;
using TwitchLib.Api.Helix.Models.Extensions.Transactions;

namespace TwitchLib.Api.Helix
{
	// Token: 0x02000011 RID: 17
	public class Extensions : ApiBase
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00003A8F File Offset: 0x00001C8F
		public Extensions(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
			: base(settings, rateLimiter, http)
		{
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003A9C File Offset: 0x00001C9C
		public Task<GetExtensionTransactionsResponse> GetExtensionTransactionsAsync(string extensionId, List<string> ids = null, string after = null, int first = 20, string applicationAccessToken = null)
		{
			if (string.IsNullOrWhiteSpace(extensionId))
			{
				throw new BadParameterException("extensionId cannot be null");
			}
			if (first < 1 || first > 100)
			{
				throw new BadParameterException("'first' must between 1 (inclusive) and 100 (inclusive).");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("extension_id", extensionId));
			List<KeyValuePair<string, string>> list2 = list;
			if (ids != null)
			{
				list2.AddRange(Enumerable.Select<string, KeyValuePair<string, string>>(ids, (string id) => new KeyValuePair<string, string>("id", id)));
			}
			if (!string.IsNullOrWhiteSpace(after))
			{
				list2.Add(new KeyValuePair<string, string>("after", after));
			}
			list2.Add(new KeyValuePair<string, string>("first", first.ToString()));
			return base.TwitchGetGenericAsync<GetExtensionTransactionsResponse>("/extensions/transactions", ApiVersion.Helix, list2, applicationAccessToken, null, null);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003B58 File Offset: 0x00001D58
		public Task<GetExtensionLiveChannelsResponse> GetExtensionLiveChannelsAsync(string extensionId, int first = 20, string after = null, string accessToken = null)
		{
			if (string.IsNullOrEmpty(extensionId))
			{
				throw new BadParameterException("extensionId must be set");
			}
			if (first < 1 || first > 100)
			{
				throw new BadParameterException("'first' must between 1 (inclusive) and 100 (inclusive).");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("extension_id", extensionId));
			list.Add(new KeyValuePair<string, string>("first", first.ToString()));
			List<KeyValuePair<string, string>> list2 = list;
			if (!string.IsNullOrWhiteSpace(after))
			{
				list2.Add(new KeyValuePair<string, string>("after", after));
			}
			return base.TwitchGetGenericAsync<GetExtensionLiveChannelsResponse>("/extensions/live", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003BE4 File Offset: 0x00001DE4
		public Task<GetReleasedExtensionsResponse> GetReleasedExtensionsAsync(string extensionId, string extensionVersion = null, string accessToken = null)
		{
			if (string.IsNullOrEmpty(extensionId))
			{
				throw new BadParameterException("extensionId must be set");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("extension_id", extensionId));
			List<KeyValuePair<string, string>> list2 = list;
			if (!string.IsNullOrWhiteSpace(extensionVersion))
			{
				list2.Add(new KeyValuePair<string, string>("extension_version", extensionVersion));
			}
			return base.TwitchGetGenericAsync<GetReleasedExtensionsResponse>("/extensions/released", ApiVersion.Helix, list2, accessToken, null, null);
		}
	}
}
