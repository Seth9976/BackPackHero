using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Tags;

namespace TwitchLib.Api.Helix
{
	// Token: 0x0200001F RID: 31
	public class Tags : ApiBase
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x000058E4 File Offset: 0x00003AE4
		public Tags(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
			: base(settings, rateLimiter, http)
		{
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000058F0 File Offset: 0x00003AF0
		public Task<GetAllStreamTagsResponse> GetAllStreamTagsAsync(string after = null, int first = 20, List<string> tagIds = null, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			if (!string.IsNullOrWhiteSpace(after))
			{
				list.Add(new KeyValuePair<string, string>("after", after));
			}
			if (first >= 0 && first <= 100)
			{
				list.Add(new KeyValuePair<string, string>("first", first.ToString()));
				if (tagIds != null && tagIds.Count > 0)
				{
					list.AddRange(Enumerable.Select<string, KeyValuePair<string, string>>(tagIds, (string tagId) => new KeyValuePair<string, string>("tag_id", tagId)));
				}
				return base.TwitchGetGenericAsync<GetAllStreamTagsResponse>("/tags/streams", ApiVersion.Helix, list, accessToken, null, null);
			}
			throw new ArgumentOutOfRangeException("first", "first value cannot exceed 100 and cannot be less than 1");
		}
	}
}
