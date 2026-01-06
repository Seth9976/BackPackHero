using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Core.Models;

namespace TwitchLib.Api.Core
{
	// Token: 0x02000002 RID: 2
	public class ApiBase
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public ApiBase(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
		{
			this.Settings = settings;
			this._rateLimiter = rateLimiter;
			this._http = http;
			this._jsonSerializer = new ApiBase.TwitchLibJsonSerializer();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000209C File Offset: 0x0000029C
		public ValueTask<string> GetAccessTokenAsync(string accessToken = null)
		{
			ApiBase.<GetAccessTokenAsync>d__9 <GetAccessTokenAsync>d__;
			<GetAccessTokenAsync>d__.<>4__this = this;
			<GetAccessTokenAsync>d__.accessToken = accessToken;
			<GetAccessTokenAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<string>.Create();
			<GetAccessTokenAsync>d__.<>1__state = -1;
			<GetAccessTokenAsync>d__.<>t__builder.Start<ApiBase.<GetAccessTokenAsync>d__9>(ref <GetAccessTokenAsync>d__);
			return <GetAccessTokenAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020E8 File Offset: 0x000002E8
		internal Task<string> GenerateServerBasedAccessToken()
		{
			ApiBase.<GenerateServerBasedAccessToken>d__10 <GenerateServerBasedAccessToken>d__;
			<GenerateServerBasedAccessToken>d__.<>4__this = this;
			<GenerateServerBasedAccessToken>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<GenerateServerBasedAccessToken>d__.<>1__state = -1;
			<GenerateServerBasedAccessToken>d__.<>t__builder.Start<ApiBase.<GenerateServerBasedAccessToken>d__10>(ref <GenerateServerBasedAccessToken>d__);
			return <GenerateServerBasedAccessToken>d__.<>t__builder.Task;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000212B File Offset: 0x0000032B
		internal void ForceAccessTokenAndClientIdForHelix(string clientId, string accessToken, ApiVersion api)
		{
			if (api != ApiVersion.Helix)
			{
				return;
			}
			if (!string.IsNullOrWhiteSpace(clientId) && !string.IsNullOrWhiteSpace(accessToken))
			{
				return;
			}
			throw new ClientIdAndOAuthTokenRequired("As of May 1, all calls to Twitch's Helix API require Client-ID and OAuth access token be set. Example: api.Settings.AccessToken = \"twitch-oauth-access-token-here\"; api.Settings.ClientId = \"twitch-client-id-here\";");
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002150 File Offset: 0x00000350
		protected Task<string> TwitchGetAsync(string resource, ApiVersion api, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
		{
			ApiBase.<TwitchGetAsync>d__12 <TwitchGetAsync>d__;
			<TwitchGetAsync>d__.<>4__this = this;
			<TwitchGetAsync>d__.resource = resource;
			<TwitchGetAsync>d__.api = api;
			<TwitchGetAsync>d__.getParams = getParams;
			<TwitchGetAsync>d__.accessToken = accessToken;
			<TwitchGetAsync>d__.clientId = clientId;
			<TwitchGetAsync>d__.customBase = customBase;
			<TwitchGetAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<TwitchGetAsync>d__.<>1__state = -1;
			<TwitchGetAsync>d__.<>t__builder.Start<ApiBase.<TwitchGetAsync>d__12>(ref <TwitchGetAsync>d__);
			return <TwitchGetAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021C8 File Offset: 0x000003C8
		protected Task<T> TwitchGetGenericAsync<T>(string resource, ApiVersion api, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
		{
			ApiBase.<TwitchGetGenericAsync>d__13<T> <TwitchGetGenericAsync>d__;
			<TwitchGetGenericAsync>d__.<>4__this = this;
			<TwitchGetGenericAsync>d__.resource = resource;
			<TwitchGetGenericAsync>d__.api = api;
			<TwitchGetGenericAsync>d__.getParams = getParams;
			<TwitchGetGenericAsync>d__.accessToken = accessToken;
			<TwitchGetGenericAsync>d__.clientId = clientId;
			<TwitchGetGenericAsync>d__.customBase = customBase;
			<TwitchGetGenericAsync>d__.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
			<TwitchGetGenericAsync>d__.<>1__state = -1;
			<TwitchGetGenericAsync>d__.<>t__builder.Start<ApiBase.<TwitchGetGenericAsync>d__13<T>>(ref <TwitchGetGenericAsync>d__);
			return <TwitchGetGenericAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002240 File Offset: 0x00000440
		protected Task<T> TwitchPatchGenericAsync<T>(string resource, ApiVersion api, string payload, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
		{
			ApiBase.<TwitchPatchGenericAsync>d__14<T> <TwitchPatchGenericAsync>d__;
			<TwitchPatchGenericAsync>d__.<>4__this = this;
			<TwitchPatchGenericAsync>d__.resource = resource;
			<TwitchPatchGenericAsync>d__.api = api;
			<TwitchPatchGenericAsync>d__.payload = payload;
			<TwitchPatchGenericAsync>d__.getParams = getParams;
			<TwitchPatchGenericAsync>d__.accessToken = accessToken;
			<TwitchPatchGenericAsync>d__.clientId = clientId;
			<TwitchPatchGenericAsync>d__.customBase = customBase;
			<TwitchPatchGenericAsync>d__.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
			<TwitchPatchGenericAsync>d__.<>1__state = -1;
			<TwitchPatchGenericAsync>d__.<>t__builder.Start<ApiBase.<TwitchPatchGenericAsync>d__14<T>>(ref <TwitchPatchGenericAsync>d__);
			return <TwitchPatchGenericAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022C0 File Offset: 0x000004C0
		protected Task<string> TwitchPatchAsync(string resource, ApiVersion api, string payload, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
		{
			ApiBase.<TwitchPatchAsync>d__15 <TwitchPatchAsync>d__;
			<TwitchPatchAsync>d__.<>4__this = this;
			<TwitchPatchAsync>d__.resource = resource;
			<TwitchPatchAsync>d__.api = api;
			<TwitchPatchAsync>d__.payload = payload;
			<TwitchPatchAsync>d__.getParams = getParams;
			<TwitchPatchAsync>d__.accessToken = accessToken;
			<TwitchPatchAsync>d__.clientId = clientId;
			<TwitchPatchAsync>d__.customBase = customBase;
			<TwitchPatchAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<TwitchPatchAsync>d__.<>1__state = -1;
			<TwitchPatchAsync>d__.<>t__builder.Start<ApiBase.<TwitchPatchAsync>d__15>(ref <TwitchPatchAsync>d__);
			return <TwitchPatchAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002340 File Offset: 0x00000540
		protected Task<KeyValuePair<int, string>> TwitchDeleteAsync(string resource, ApiVersion api, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
		{
			ApiBase.<TwitchDeleteAsync>d__16 <TwitchDeleteAsync>d__;
			<TwitchDeleteAsync>d__.<>4__this = this;
			<TwitchDeleteAsync>d__.resource = resource;
			<TwitchDeleteAsync>d__.api = api;
			<TwitchDeleteAsync>d__.getParams = getParams;
			<TwitchDeleteAsync>d__.accessToken = accessToken;
			<TwitchDeleteAsync>d__.clientId = clientId;
			<TwitchDeleteAsync>d__.customBase = customBase;
			<TwitchDeleteAsync>d__.<>t__builder = AsyncTaskMethodBuilder<KeyValuePair<int, string>>.Create();
			<TwitchDeleteAsync>d__.<>1__state = -1;
			<TwitchDeleteAsync>d__.<>t__builder.Start<ApiBase.<TwitchDeleteAsync>d__16>(ref <TwitchDeleteAsync>d__);
			return <TwitchDeleteAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000023B8 File Offset: 0x000005B8
		protected Task<T> TwitchPostGenericAsync<T>(string resource, ApiVersion api, string payload, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
		{
			ApiBase.<TwitchPostGenericAsync>d__17<T> <TwitchPostGenericAsync>d__;
			<TwitchPostGenericAsync>d__.<>4__this = this;
			<TwitchPostGenericAsync>d__.resource = resource;
			<TwitchPostGenericAsync>d__.api = api;
			<TwitchPostGenericAsync>d__.payload = payload;
			<TwitchPostGenericAsync>d__.getParams = getParams;
			<TwitchPostGenericAsync>d__.accessToken = accessToken;
			<TwitchPostGenericAsync>d__.clientId = clientId;
			<TwitchPostGenericAsync>d__.customBase = customBase;
			<TwitchPostGenericAsync>d__.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
			<TwitchPostGenericAsync>d__.<>1__state = -1;
			<TwitchPostGenericAsync>d__.<>t__builder.Start<ApiBase.<TwitchPostGenericAsync>d__17<T>>(ref <TwitchPostGenericAsync>d__);
			return <TwitchPostGenericAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002438 File Offset: 0x00000638
		protected Task<T> TwitchPostGenericModelAsync<T>(string resource, ApiVersion api, RequestModel model, string accessToken = null, string clientId = null, string customBase = null)
		{
			ApiBase.<TwitchPostGenericModelAsync>d__18<T> <TwitchPostGenericModelAsync>d__;
			<TwitchPostGenericModelAsync>d__.<>4__this = this;
			<TwitchPostGenericModelAsync>d__.resource = resource;
			<TwitchPostGenericModelAsync>d__.api = api;
			<TwitchPostGenericModelAsync>d__.model = model;
			<TwitchPostGenericModelAsync>d__.accessToken = accessToken;
			<TwitchPostGenericModelAsync>d__.clientId = clientId;
			<TwitchPostGenericModelAsync>d__.customBase = customBase;
			<TwitchPostGenericModelAsync>d__.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
			<TwitchPostGenericModelAsync>d__.<>1__state = -1;
			<TwitchPostGenericModelAsync>d__.<>t__builder.Start<ApiBase.<TwitchPostGenericModelAsync>d__18<T>>(ref <TwitchPostGenericModelAsync>d__);
			return <TwitchPostGenericModelAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000024B0 File Offset: 0x000006B0
		protected Task<T> TwitchDeleteGenericAsync<T>(string resource, ApiVersion api, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
		{
			ApiBase.<TwitchDeleteGenericAsync>d__19<T> <TwitchDeleteGenericAsync>d__;
			<TwitchDeleteGenericAsync>d__.<>4__this = this;
			<TwitchDeleteGenericAsync>d__.resource = resource;
			<TwitchDeleteGenericAsync>d__.api = api;
			<TwitchDeleteGenericAsync>d__.getParams = getParams;
			<TwitchDeleteGenericAsync>d__.accessToken = accessToken;
			<TwitchDeleteGenericAsync>d__.clientId = clientId;
			<TwitchDeleteGenericAsync>d__.customBase = customBase;
			<TwitchDeleteGenericAsync>d__.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
			<TwitchDeleteGenericAsync>d__.<>1__state = -1;
			<TwitchDeleteGenericAsync>d__.<>t__builder.Start<ApiBase.<TwitchDeleteGenericAsync>d__19<T>>(ref <TwitchDeleteGenericAsync>d__);
			return <TwitchDeleteGenericAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002528 File Offset: 0x00000728
		protected Task<T> TwitchPutGenericAsync<T>(string resource, ApiVersion api, string payload = null, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
		{
			ApiBase.<TwitchPutGenericAsync>d__20<T> <TwitchPutGenericAsync>d__;
			<TwitchPutGenericAsync>d__.<>4__this = this;
			<TwitchPutGenericAsync>d__.resource = resource;
			<TwitchPutGenericAsync>d__.api = api;
			<TwitchPutGenericAsync>d__.payload = payload;
			<TwitchPutGenericAsync>d__.getParams = getParams;
			<TwitchPutGenericAsync>d__.accessToken = accessToken;
			<TwitchPutGenericAsync>d__.clientId = clientId;
			<TwitchPutGenericAsync>d__.customBase = customBase;
			<TwitchPutGenericAsync>d__.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
			<TwitchPutGenericAsync>d__.<>1__state = -1;
			<TwitchPutGenericAsync>d__.<>t__builder.Start<ApiBase.<TwitchPutGenericAsync>d__20<T>>(ref <TwitchPutGenericAsync>d__);
			return <TwitchPutGenericAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000025A8 File Offset: 0x000007A8
		protected Task<string> TwitchPutAsync(string resource, ApiVersion api, string payload, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
		{
			ApiBase.<TwitchPutAsync>d__21 <TwitchPutAsync>d__;
			<TwitchPutAsync>d__.<>4__this = this;
			<TwitchPutAsync>d__.resource = resource;
			<TwitchPutAsync>d__.api = api;
			<TwitchPutAsync>d__.payload = payload;
			<TwitchPutAsync>d__.getParams = getParams;
			<TwitchPutAsync>d__.accessToken = accessToken;
			<TwitchPutAsync>d__.clientId = clientId;
			<TwitchPutAsync>d__.customBase = customBase;
			<TwitchPutAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<TwitchPutAsync>d__.<>1__state = -1;
			<TwitchPutAsync>d__.<>t__builder.Start<ApiBase.<TwitchPutAsync>d__21>(ref <TwitchPutAsync>d__);
			return <TwitchPutAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002628 File Offset: 0x00000828
		protected Task<KeyValuePair<int, string>> TwitchPostAsync(string resource, ApiVersion api, string payload, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
		{
			ApiBase.<TwitchPostAsync>d__22 <TwitchPostAsync>d__;
			<TwitchPostAsync>d__.<>4__this = this;
			<TwitchPostAsync>d__.resource = resource;
			<TwitchPostAsync>d__.api = api;
			<TwitchPostAsync>d__.payload = payload;
			<TwitchPostAsync>d__.getParams = getParams;
			<TwitchPostAsync>d__.accessToken = accessToken;
			<TwitchPostAsync>d__.clientId = clientId;
			<TwitchPostAsync>d__.customBase = customBase;
			<TwitchPostAsync>d__.<>t__builder = AsyncTaskMethodBuilder<KeyValuePair<int, string>>.Create();
			<TwitchPostAsync>d__.<>1__state = -1;
			<TwitchPostAsync>d__.<>t__builder.Start<ApiBase.<TwitchPostAsync>d__22>(ref <TwitchPostAsync>d__);
			return <TwitchPostAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000026A7 File Offset: 0x000008A7
		protected Task PutBytesAsync(string url, byte[] payload)
		{
			return this._http.PutBytesAsync(url, payload);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000026B6 File Offset: 0x000008B6
		internal Task<int> RequestReturnResponseCode(string url, string method, List<KeyValuePair<string, string>> getParams = null)
		{
			return this._http.RequestReturnResponseCodeAsync(url, method, getParams);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000026C8 File Offset: 0x000008C8
		protected Task<T> GetGenericAsync<T>(string url, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, ApiVersion api = ApiVersion.Helix, string clientId = null)
		{
			ApiBase.<GetGenericAsync>d__25<T> <GetGenericAsync>d__;
			<GetGenericAsync>d__.<>4__this = this;
			<GetGenericAsync>d__.url = url;
			<GetGenericAsync>d__.getParams = getParams;
			<GetGenericAsync>d__.accessToken = accessToken;
			<GetGenericAsync>d__.api = api;
			<GetGenericAsync>d__.clientId = clientId;
			<GetGenericAsync>d__.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
			<GetGenericAsync>d__.<>1__state = -1;
			<GetGenericAsync>d__.<>t__builder.Start<ApiBase.<GetGenericAsync>d__25<T>>(ref <GetGenericAsync>d__);
			return <GetGenericAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002738 File Offset: 0x00000938
		internal Task<T> GetSimpleGenericAsync<T>(string url, List<KeyValuePair<string, string>> getParams = null)
		{
			ApiBase.<>c__DisplayClass26_0<T> CS$<>8__locals1 = new ApiBase.<>c__DisplayClass26_0<T>();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.url = url;
			if (getParams != null)
			{
				for (int i = 0; i < getParams.Count; i++)
				{
					if (i == 0)
					{
						CS$<>8__locals1.url = string.Concat(new string[]
						{
							CS$<>8__locals1.url,
							"?",
							getParams[i].Key,
							"=",
							Uri.EscapeDataString(getParams[i].Value)
						});
					}
					else
					{
						CS$<>8__locals1.url = string.Concat(new string[]
						{
							CS$<>8__locals1.url,
							"&",
							getParams[i].Key,
							"=",
							Uri.EscapeDataString(getParams[i].Value)
						});
					}
				}
			}
			return this._rateLimiter.Perform<T>(delegate
			{
				ApiBase.<>c__DisplayClass26_0<T>.<<GetSimpleGenericAsync>b__0>d <<GetSimpleGenericAsync>b__0>d;
				<<GetSimpleGenericAsync>b__0>d.<>4__this = CS$<>8__locals1;
				<<GetSimpleGenericAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
				<<GetSimpleGenericAsync>b__0>d.<>1__state = -1;
				<<GetSimpleGenericAsync>b__0>d.<>t__builder.Start<ApiBase.<>c__DisplayClass26_0<T>.<<GetSimpleGenericAsync>b__0>d>(ref <<GetSimpleGenericAsync>b__0>d);
				return <<GetSimpleGenericAsync>b__0>d.<>t__builder.Task;
			});
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002838 File Offset: 0x00000A38
		private Task<string> SimpleRequestAsync(string url)
		{
			ApiBase.<>c__DisplayClass27_0 CS$<>8__locals1 = new ApiBase.<>c__DisplayClass27_0();
			CS$<>8__locals1.tcs = new TaskCompletionSource<string>();
			CS$<>8__locals1.client = new WebClient();
			CS$<>8__locals1.client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(CS$<>8__locals1.<SimpleRequestAsync>g__DownloadStringCompletedEventHandler|0);
			CS$<>8__locals1.client.DownloadString(new Uri(url));
			return CS$<>8__locals1.tcs.Task;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002898 File Offset: 0x00000A98
		private string ConstructResourceUrl(string resource = null, List<KeyValuePair<string, string>> getParams = null, ApiVersion api = ApiVersion.Helix, string overrideUrl = null)
		{
			string text = "";
			if (overrideUrl == null)
			{
				if (resource == null)
				{
					throw new Exception("Cannot pass null resource with null override url");
				}
				if (api != ApiVersion.Auth)
				{
					if (api == ApiVersion.Helix)
					{
						text = "https://api.twitch.tv/helix" + resource;
					}
				}
				else
				{
					text = "https://id.twitch.tv/oauth2" + resource;
				}
			}
			else
			{
				text = ((resource == null) ? overrideUrl : (overrideUrl + resource));
			}
			if (getParams != null)
			{
				for (int i = 0; i < getParams.Count; i++)
				{
					if (i == 0)
					{
						text = string.Concat(new string[]
						{
							text,
							"?",
							getParams[i].Key,
							"=",
							Uri.EscapeDataString(getParams[i].Value)
						});
					}
					else
					{
						text = string.Concat(new string[]
						{
							text,
							"&",
							getParams[i].Key,
							"=",
							Uri.EscapeDataString(getParams[i].Value)
						});
					}
				}
			}
			return text;
		}

		// Token: 0x04000001 RID: 1
		private readonly ApiBase.TwitchLibJsonSerializer _jsonSerializer;

		// Token: 0x04000002 RID: 2
		protected readonly IApiSettings Settings;

		// Token: 0x04000003 RID: 3
		private readonly IRateLimiter _rateLimiter;

		// Token: 0x04000004 RID: 4
		private readonly IHttpCallHandler _http;

		// Token: 0x04000005 RID: 5
		internal const string BaseHelix = "https://api.twitch.tv/helix";

		// Token: 0x04000006 RID: 6
		internal const string BaseAuth = "https://id.twitch.tv/oauth2";

		// Token: 0x04000007 RID: 7
		private DateTime? _serverBasedAccessTokenExpiry;

		// Token: 0x04000008 RID: 8
		private string _serverBasedAccessToken;

		// Token: 0x04000009 RID: 9
		private readonly JsonSerializerSettings _twitchLibJsonDeserializer = new JsonSerializerSettings
		{
			NullValueHandling = 1,
			MissingMemberHandling = 0
		};

		// Token: 0x02000021 RID: 33
		private class TwitchLibJsonSerializer
		{
			// Token: 0x0600007B RID: 123 RVA: 0x000039B7 File Offset: 0x00001BB7
			public string SerializeObject(object o)
			{
				return JsonConvert.SerializeObject(o, 1, this._settings);
			}

			// Token: 0x04000022 RID: 34
			private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
			{
				ContractResolver = new ApiBase.TwitchLibJsonSerializer.LowercaseContractResolver(),
				NullValueHandling = 1
			};

			// Token: 0x02000052 RID: 82
			private class LowercaseContractResolver : DefaultContractResolver
			{
				// Token: 0x060000E3 RID: 227 RVA: 0x00007EB6 File Offset: 0x000060B6
				protected override string ResolvePropertyName(string propertyName)
				{
					return propertyName.ToLower();
				}
			}
		}
	}
}
