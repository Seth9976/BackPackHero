using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Schedule.CreateChannelStreamSegment;
using TwitchLib.Api.Helix.Models.Schedule.GetChannelStreamSchedule;
using TwitchLib.Api.Helix.Models.Schedule.UpdateChannelStreamSegment;

namespace TwitchLib.Api.Helix
{
	// Token: 0x0200001A RID: 26
	public class Schedule : ApiBase
	{
		// Token: 0x06000098 RID: 152 RVA: 0x00004E84 File Offset: 0x00003084
		public Schedule(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
			: base(settings, rateLimiter, http)
		{
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004E90 File Offset: 0x00003090
		public Task<GetChannelStreamScheduleResponse> GetChannelStreamScheduleAsync(string broadcasterId, List<string> segmentIds = null, string startTime = null, string utcOffset = null, int first = 20, string after = null, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			list.Add(new KeyValuePair<string, string>("first", first.ToString()));
			List<KeyValuePair<string, string>> list2 = list;
			if (segmentIds != null && segmentIds.Count > 0)
			{
				list2.AddRange(Enumerable.Select<string, KeyValuePair<string, string>>(segmentIds, (string segmentId) => new KeyValuePair<string, string>("id", segmentId)));
			}
			if (!string.IsNullOrWhiteSpace(startTime))
			{
				list2.Add(new KeyValuePair<string, string>("start_time", startTime));
			}
			if (!string.IsNullOrWhiteSpace(utcOffset))
			{
				list2.Add(new KeyValuePair<string, string>("utc_offset", utcOffset));
			}
			if (!string.IsNullOrWhiteSpace(after))
			{
				list2.Add(new KeyValuePair<string, string>("after", after));
			}
			return base.TwitchGetGenericAsync<GetChannelStreamScheduleResponse>("/schedule", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004F64 File Offset: 0x00003164
		public Task UpdateChannelStreamScheduleAsync(string broadcasterId, bool? isVacationEnabled = null, DateTime? vacationStartTime = null, DateTime? vacationEndTime = null, string timezone = null, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			List<KeyValuePair<string, string>> list2 = list;
			if (isVacationEnabled != null)
			{
				list2.Add(new KeyValuePair<string, string>("is_vacation_enabled", isVacationEnabled.Value.ToString()));
			}
			if (vacationStartTime != null)
			{
				list2.Add(new KeyValuePair<string, string>("vacation_start_time", vacationStartTime.Value.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss.fffzzz", DateTimeFormatInfo.InvariantInfo)));
			}
			if (vacationEndTime != null)
			{
				list2.Add(new KeyValuePair<string, string>("vacation_end_time", vacationEndTime.Value.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss.fffzzz", DateTimeFormatInfo.InvariantInfo)));
			}
			if (!string.IsNullOrWhiteSpace(timezone))
			{
				list2.Add(new KeyValuePair<string, string>("timezone", timezone));
			}
			return base.TwitchPatchAsync("/schedule/settings", ApiVersion.Helix, null, list2, accessToken, null, null);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00005054 File Offset: 0x00003254
		public Task<CreateChannelStreamSegmentResponse> CreateChannelStreamScheduleSegmentAsync(string broadcasterId, CreateChannelStreamSegmentRequest payload, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchPostGenericAsync<CreateChannelStreamSegmentResponse>("/schedule/segment", ApiVersion.Helix, JsonConvert.SerializeObject(payload), list2, accessToken, null, null);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00005090 File Offset: 0x00003290
		public Task<UpdateChannelStreamSegmentResponse> UpdateChannelStreamScheduleSegmentAsync(string broadcasterId, string segmentId, UpdateChannelStreamSegmentRequest payload, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			list.Add(new KeyValuePair<string, string>("id", segmentId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchPatchGenericAsync<UpdateChannelStreamSegmentResponse>("/schedule/segment", ApiVersion.Helix, JsonConvert.SerializeObject(payload), list2, accessToken, null, null);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000050DC File Offset: 0x000032DC
		public Task DeleteChannelStreamScheduleSegmentAsync(string broadcasterId, string segmentId, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			list.Add(new KeyValuePair<string, string>("id", segmentId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchDeleteAsync("/schedule/segment", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00005124 File Offset: 0x00003324
		public Task GetChanneliCalendarAsync(string broadcasterId)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchGetAsync("/schedule/icalendar", ApiVersion.Helix, list2, null, null, null);
		}
	}
}
