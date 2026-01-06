using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Extensions.System;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Clips.CreateClip;
using TwitchLib.Api.Helix.Models.Clips.GetClips;

namespace TwitchLib.Api.Helix
{
	// Token: 0x0200000E RID: 14
	public class Clips : ApiBase
	{
		// Token: 0x06000048 RID: 72 RVA: 0x000034DC File Offset: 0x000016DC
		public Clips(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
			: base(settings, rateLimiter, http)
		{
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000034E8 File Offset: 0x000016E8
		public Task<GetClipsResponse> GetClipsAsync(List<string> clipIds = null, string gameId = null, string broadcasterId = null, string before = null, string after = null, DateTime? startedAt = null, DateTime? endedAt = null, int first = 20, string accessToken = null)
		{
			if (first < 0 || first > 100)
			{
				throw new BadParameterException("'first' must between 0 (inclusive) and 100 (inclusive).");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			if (clipIds != null)
			{
				list.AddRange(Enumerable.Select<string, KeyValuePair<string, string>>(clipIds, (string clipId) => new KeyValuePair<string, string>("id", clipId)));
			}
			if (!string.IsNullOrWhiteSpace(gameId))
			{
				list.Add(new KeyValuePair<string, string>("game_id", gameId));
			}
			if (!string.IsNullOrWhiteSpace(broadcasterId))
			{
				list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			}
			if (list.Count == 0 || (list.Count > 1 && gameId != null && broadcasterId != null))
			{
				throw new BadParameterException("One of the following parameters must be set: clipId, gameId, broadcasterId. Only one is allowed to be set.");
			}
			if (startedAt == null && endedAt != null)
			{
				throw new BadParameterException("The ended_at parameter cannot be used without the started_at parameter. Please include both parameters!");
			}
			if (startedAt != null)
			{
				list.Add(new KeyValuePair<string, string>("started_at", startedAt.Value.ToRfc3339String()));
			}
			if (endedAt != null)
			{
				list.Add(new KeyValuePair<string, string>("ended_at", endedAt.Value.ToRfc3339String()));
			}
			if (!string.IsNullOrWhiteSpace(before))
			{
				list.Add(new KeyValuePair<string, string>("before", before));
			}
			if (!string.IsNullOrWhiteSpace(after))
			{
				list.Add(new KeyValuePair<string, string>("after", after));
			}
			list.Add(new KeyValuePair<string, string>("first", first.ToString()));
			return base.TwitchGetGenericAsync<GetClipsResponse>("/clips", ApiVersion.Helix, list, accessToken, null, null);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003658 File Offset: 0x00001858
		public Task<CreatedClipResponse> CreateClipAsync(string broadcasterId, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchPostGenericAsync<CreatedClipResponse>("/clips", ApiVersion.Helix, null, list2, accessToken, null, null);
		}
	}
}
