using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Soundtrack.GetCurrentTrack;
using TwitchLib.Api.Helix.Models.Soundtrack.GetPlaylist;
using TwitchLib.Api.Helix.Models.Soundtrack.GetPlaylists;

namespace TwitchLib.Api.Helix
{
	// Token: 0x0200001C RID: 28
	public class Soundtrack : ApiBase
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x00005272 File Offset: 0x00003472
		public Soundtrack(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
			: base(settings, rateLimiter, http)
		{
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00005280 File Offset: 0x00003480
		public Task<GetCurrentTrackResponse> GetCurrentTrackAsync(string broadcasterId, string accessToken = null)
		{
			if (string.IsNullOrWhiteSpace(broadcasterId))
			{
				throw new BadParameterException("'broadcasterId' must be set");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchGetGenericAsync<GetCurrentTrackResponse>("/soundtrack/current_track", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000052C8 File Offset: 0x000034C8
		public Task<GetPlaylistResponse> GetPlaylistAsync(string id, int first = 20, string after = null, string accessToken = null)
		{
			if (string.IsNullOrWhiteSpace(id))
			{
				throw new BadParameterException("'id' must be set");
			}
			if (first < 1 || first > 50)
			{
				throw new BadParameterException("'first' must be value of 1 - 50");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("id", id));
			list.Add(new KeyValuePair<string, string>("first", first.ToString()));
			List<KeyValuePair<string, string>> list2 = list;
			if (!string.IsNullOrWhiteSpace(after))
			{
				list2.Add(new KeyValuePair<string, string>("after", after));
			}
			return base.TwitchGetGenericAsync<GetPlaylistResponse>("/soundtrack/playlist", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00005354 File Offset: 0x00003554
		public Task<GetPlaylistsResponse> GetPlaylistsAsync(string id = null, int first = 20, string after = null, string accessToken = null)
		{
			if (first < 1 || first > 50)
			{
				throw new BadParameterException("'first' must be value of 1 - 50");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("first", first.ToString()));
			List<KeyValuePair<string, string>> list2 = list;
			if (!string.IsNullOrWhiteSpace(after))
			{
				list2.Add(new KeyValuePair<string, string>("after", after));
			}
			if (!string.IsNullOrWhiteSpace(id))
			{
				list2.Add(new KeyValuePair<string, string>("id", id));
			}
			return base.TwitchGetGenericAsync<GetPlaylistsResponse>("/soundtrack/playlists", ApiVersion.Helix, list2, accessToken, null, null);
		}
	}
}
