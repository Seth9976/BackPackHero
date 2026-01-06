using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Timers;
using Newtonsoft.Json;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Common;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Events;
using TwitchLib.Api.ThirdParty.AuthorizationFlow;
using TwitchLib.Api.ThirdParty.ModLookup;
using TwitchLib.Api.ThirdParty.UsernameChange;

namespace TwitchLib.Api.ThirdParty
{
	// Token: 0x02000003 RID: 3
	public class ThirdParty
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002252 File Offset: 0x00000452
		public ThirdParty(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
		{
			this.UsernameChange = new ThirdParty.UsernameChangeApi(settings, rateLimiter, http);
			this.ModLookup = new ThirdParty.ModLookupApi(settings, rateLimiter, http);
			this.AuthorizationFlow = new ThirdParty.AuthorizationFlowApi(settings, rateLimiter, http);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002284 File Offset: 0x00000484
		public ThirdParty.UsernameChangeApi UsernameChange { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000228C File Offset: 0x0000048C
		public ThirdParty.ModLookupApi ModLookup { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002294 File Offset: 0x00000494
		public ThirdParty.AuthorizationFlowApi AuthorizationFlow { get; }

		// Token: 0x0200002A RID: 42
		public class UsernameChangeApi : ApiBase
		{
			// Token: 0x060000F5 RID: 245 RVA: 0x00003F40 File Offset: 0x00002140
			public UsernameChangeApi(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
				: base(settings, rateLimiter, http)
			{
			}

			// Token: 0x060000F6 RID: 246 RVA: 0x00003F4C File Offset: 0x0000214C
			public Task<List<UsernameChangeListing>> GetUsernameChangesAsync(string username)
			{
				List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
				list.Add(new KeyValuePair<string, string>("q", username));
				list.Add(new KeyValuePair<string, string>("format", "json"));
				List<KeyValuePair<string, string>> list2 = list;
				return base.GetGenericAsync<List<UsernameChangeListing>>("https://twitch-tools.rootonline.de/username_changelogs_search.php", list2, null, ApiVersion.Void, null);
			}
		}

		// Token: 0x0200002B RID: 43
		public class ModLookupApi : ApiBase
		{
			// Token: 0x060000F7 RID: 247 RVA: 0x00003F94 File Offset: 0x00002194
			public ModLookupApi(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
				: base(settings, rateLimiter, http)
			{
			}

			// Token: 0x060000F8 RID: 248 RVA: 0x00003FA0 File Offset: 0x000021A0
			public Task<ModLookupResponse> GetChannelsModdedForByNameAsync(string username, int offset = 0, int limit = 100, bool useTls12 = true)
			{
				if (useTls12)
				{
					ServicePointManager.SecurityProtocol = 3072;
				}
				List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
				list.Add(new KeyValuePair<string, string>("offset", offset.ToString()));
				list.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
				List<KeyValuePair<string, string>> list2 = list;
				return base.GetGenericAsync<ModLookupResponse>("https://twitchstuff.3v.fi/modlookup/api/user/" + username, list2, null, ApiVersion.Void, null);
			}

			// Token: 0x060000F9 RID: 249 RVA: 0x00004004 File Offset: 0x00002204
			public Task<TopResponse> GetChannelsModdedForByTopAsync(bool useTls12 = true)
			{
				if (useTls12)
				{
					ServicePointManager.SecurityProtocol = 3072;
				}
				return base.GetGenericAsync<TopResponse>("https://twitchstuff.3v.fi/modlookup/api/top", null, null, ApiVersion.Helix, null);
			}

			// Token: 0x060000FA RID: 250 RVA: 0x00004022 File Offset: 0x00002222
			public Task<StatsResponse> GetChannelsModdedForStatsAsync(bool useTls12 = true)
			{
				if (useTls12)
				{
					ServicePointManager.SecurityProtocol = 3072;
				}
				return base.GetGenericAsync<StatsResponse>("https://twitchstuff.3v.fi/modlookup/api/stats", null, null, ApiVersion.Helix, null);
			}
		}

		// Token: 0x0200002C RID: 44
		public class AuthorizationFlowApi : ApiBase
		{
			// Token: 0x060000FB RID: 251 RVA: 0x00004040 File Offset: 0x00002240
			public AuthorizationFlowApi(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
				: base(settings, rateLimiter, http)
			{
			}

			// Token: 0x14000009 RID: 9
			// (add) Token: 0x060000FC RID: 252 RVA: 0x0000404C File Offset: 0x0000224C
			// (remove) Token: 0x060000FD RID: 253 RVA: 0x00004084 File Offset: 0x00002284
			public event EventHandler<OnUserAuthorizationDetectedArgs> OnUserAuthorizationDetected;

			// Token: 0x1400000A RID: 10
			// (add) Token: 0x060000FE RID: 254 RVA: 0x000040BC File Offset: 0x000022BC
			// (remove) Token: 0x060000FF RID: 255 RVA: 0x000040F4 File Offset: 0x000022F4
			public event EventHandler<OnAuthorizationFlowErrorArgs> OnError;

			// Token: 0x06000100 RID: 256 RVA: 0x0000412C File Offset: 0x0000232C
			public CreatedFlow CreateFlow(string applicationTitle, IEnumerable<AuthScopes> scopes)
			{
				string text = null;
				foreach (AuthScopes authScopes in scopes)
				{
					if (text == null)
					{
						text = Helpers.AuthScopesToString(authScopes);
					}
					else
					{
						text = text + "+" + Helpers.AuthScopesToString(authScopes);
					}
				}
				string text2 = "https://twitchtokengenerator.com/api/create/" + Helpers.Base64Encode(applicationTitle) + "/" + text;
				return JsonConvert.DeserializeObject<CreatedFlow>(new WebClient().DownloadString(text2));
			}

			// Token: 0x06000101 RID: 257 RVA: 0x000041B4 File Offset: 0x000023B4
			public RefreshTokenResponse RefreshToken(string refreshToken)
			{
				string text = "https://twitchtokengenerator.com/api/refresh/" + refreshToken;
				return JsonConvert.DeserializeObject<RefreshTokenResponse>(new WebClient().DownloadString(text));
			}

			// Token: 0x06000102 RID: 258 RVA: 0x000041DD File Offset: 0x000023DD
			public void BeginPingingStatus(string id, int intervalMs = 5000)
			{
				this._apiId = id;
				this._pingTimer = new Timer((double)intervalMs);
				this._pingTimer.Elapsed += new ElapsedEventHandler(this.OnPingTimerElapsed);
				this._pingTimer.Start();
			}

			// Token: 0x06000103 RID: 259 RVA: 0x00004215 File Offset: 0x00002415
			public PingResponse PingStatus(string id = null)
			{
				if (id != null)
				{
					this._apiId = id;
				}
				return new PingResponse(new WebClient().DownloadString("https://twitchtokengenerator.com/api/status/" + this._apiId));
			}

			// Token: 0x06000104 RID: 260 RVA: 0x00004240 File Offset: 0x00002440
			private void OnPingTimerElapsed(object sender, ElapsedEventArgs e)
			{
				PingResponse pingResponse = this.PingStatus(null);
				if (pingResponse.Success)
				{
					this._pingTimer.Stop();
					EventHandler<OnUserAuthorizationDetectedArgs> onUserAuthorizationDetected = this.OnUserAuthorizationDetected;
					if (onUserAuthorizationDetected == null)
					{
						return;
					}
					onUserAuthorizationDetected.Invoke(null, new OnUserAuthorizationDetectedArgs
					{
						Id = pingResponse.Id,
						Scopes = pingResponse.Scopes,
						Token = pingResponse.Token,
						Username = pingResponse.Username,
						Refresh = pingResponse.Refresh,
						ClientId = pingResponse.ClientId
					});
					return;
				}
				else
				{
					if (pingResponse.Error == 3)
					{
						return;
					}
					this._pingTimer.Stop();
					EventHandler<OnAuthorizationFlowErrorArgs> onError = this.OnError;
					if (onError == null)
					{
						return;
					}
					onError.Invoke(null, new OnAuthorizationFlowErrorArgs
					{
						Error = pingResponse.Error,
						Message = pingResponse.Message
					});
					return;
				}
			}

			// Token: 0x0400006E RID: 110
			private const string BaseUrl = "https://twitchtokengenerator.com/api";

			// Token: 0x0400006F RID: 111
			private string _apiId;

			// Token: 0x04000070 RID: 112
			private Timer _pingTimer;
		}
	}
}
