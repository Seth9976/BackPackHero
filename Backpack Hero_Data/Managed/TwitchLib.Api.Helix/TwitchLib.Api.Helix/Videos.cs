using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Videos.DeleteVideos;
using TwitchLib.Api.Helix.Models.Videos.GetVideos;

namespace TwitchLib.Api.Helix
{
	// Token: 0x02000022 RID: 34
	public class Videos : ApiBase
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x00005E74 File Offset: 0x00004074
		public Videos(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
			: base(settings, rateLimiter, http)
		{
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005E80 File Offset: 0x00004080
		public Task<DeleteVideosResponse> DeleteVideosAsync(List<string> videoIds, string accessToken = null)
		{
			if (videoIds.Count > 5)
			{
				throw new BadParameterException(string.Format("Maximum of 5 video ids allowed per request (you passed {0})", videoIds.Count));
			}
			List<KeyValuePair<string, string>> list = Enumerable.ToList<KeyValuePair<string, string>>(Enumerable.Select<string, KeyValuePair<string, string>>(videoIds, (string videoId) => new KeyValuePair<string, string>("id", videoId)));
			return base.TwitchDeleteGenericAsync<DeleteVideosResponse>("/videos", ApiVersion.Helix, list, accessToken, null, null);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00005EEC File Offset: 0x000040EC
		public Task<GetVideosResponse> GetVideosAsync(List<string> videoIds = null, string userId = null, string gameId = null, string after = null, string before = null, int first = 20, string language = null, Period period = Period.All, VideoSort sort = VideoSort.Time, VideoType type = VideoType.All, string accessToken = null)
		{
			if ((videoIds == null || videoIds.Count == 0) && userId == null && gameId == null)
			{
				throw new BadParameterException("VideoIds, userId, and gameId cannot all be null/empty.");
			}
			if ((videoIds != null && videoIds.Count > 0 && userId != null) || (videoIds != null && videoIds.Count > 0 && gameId != null) || (userId != null && gameId != null))
			{
				throw new BadParameterException("If videoIds are present, you may not use userid or gameid. If gameid is present, you may not use videoIds or userid. If userid is present, you may not use videoids or gameids.");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			if (videoIds != null && videoIds.Count > 0)
			{
				list.AddRange(Enumerable.Select<string, KeyValuePair<string, string>>(videoIds, (string videoId) => new KeyValuePair<string, string>("id", videoId)));
			}
			if (!string.IsNullOrWhiteSpace(userId))
			{
				list.Add(new KeyValuePair<string, string>("user_id", userId));
			}
			if (!string.IsNullOrWhiteSpace(gameId))
			{
				list.Add(new KeyValuePair<string, string>("game_id", gameId));
			}
			if (userId != null || gameId != null)
			{
				if (!string.IsNullOrWhiteSpace(after))
				{
					list.Add(new KeyValuePair<string, string>("after", after));
				}
				if (!string.IsNullOrWhiteSpace(before))
				{
					list.Add(new KeyValuePair<string, string>("before", before));
				}
				list.Add(new KeyValuePair<string, string>("first", first.ToString()));
				if (!string.IsNullOrWhiteSpace(language))
				{
					list.Add(new KeyValuePair<string, string>("language", language));
				}
				switch (period)
				{
				case Period.Day:
					list.Add(new KeyValuePair<string, string>("period", "day"));
					break;
				case Period.Week:
					list.Add(new KeyValuePair<string, string>("period", "week"));
					break;
				case Period.Month:
					list.Add(new KeyValuePair<string, string>("period", "month"));
					break;
				case Period.All:
					list.Add(new KeyValuePair<string, string>("period", "all"));
					break;
				default:
					throw new ArgumentOutOfRangeException("period", period, null);
				}
				switch (sort)
				{
				case VideoSort.Time:
					list.Add(new KeyValuePair<string, string>("sort", "time"));
					break;
				case VideoSort.Trending:
					list.Add(new KeyValuePair<string, string>("sort", "trending"));
					break;
				case VideoSort.Views:
					list.Add(new KeyValuePair<string, string>("sort", "views"));
					break;
				default:
					throw new ArgumentOutOfRangeException("sort", sort, null);
				}
				switch (type)
				{
				case VideoType.All:
					list.Add(new KeyValuePair<string, string>("type", "all"));
					break;
				case VideoType.Upload:
					list.Add(new KeyValuePair<string, string>("type", "upload"));
					break;
				case VideoType.Archive:
					list.Add(new KeyValuePair<string, string>("type", "archive"));
					break;
				case VideoType.Highlight:
					list.Add(new KeyValuePair<string, string>("type", "highlight"));
					break;
				default:
					throw new ArgumentOutOfRangeException("type", type, null);
				}
			}
			return base.TwitchGetGenericAsync<GetVideosResponse>("/videos", ApiVersion.Helix, list, accessToken, null, null);
		}
	}
}
