using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Common;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchLib.Api.Auth
{
	// Token: 0x02000025 RID: 37
	public class Auth : ApiBase
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x00003B2F File Offset: 0x00001D2F
		public Auth(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
			: base(settings, rateLimiter, http)
		{
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003B3C File Offset: 0x00001D3C
		public Task<RefreshResponse> RefreshAuthTokenAsync(string refreshToken, string clientSecret, string clientId = null)
		{
			string text = clientId ?? this.Settings.ClientId;
			if (string.IsNullOrWhiteSpace(refreshToken))
			{
				throw new BadParameterException("The refresh token is not valid. It is not allowed to be null, empty or filled with whitespaces.");
			}
			if (string.IsNullOrWhiteSpace(clientSecret))
			{
				throw new BadParameterException("The client secret is not valid. It is not allowed to be null, empty or filled with whitespaces.");
			}
			if (string.IsNullOrWhiteSpace(text))
			{
				throw new BadParameterException("The clientId is not valid. It is not allowed to be null, empty or filled with whitespaces.");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("grant_type", "refresh_token"));
			list.Add(new KeyValuePair<string, string>("refresh_token", refreshToken));
			list.Add(new KeyValuePair<string, string>("client_id", text));
			list.Add(new KeyValuePair<string, string>("client_secret", clientSecret));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchPostGenericAsync<RefreshResponse>("/token", ApiVersion.Auth, null, list2, null, text, null);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003BF4 File Offset: 0x00001DF4
		public string GetAuthorizationCodeUrl(string redirectUri, IEnumerable<AuthScopes> scopes, bool forceVerify = false, string state = null, string clientId = null)
		{
			string text = clientId ?? this.Settings.ClientId;
			string text2 = null;
			foreach (AuthScopes authScopes in scopes)
			{
				if (text2 == null)
				{
					text2 = Helpers.AuthScopesToString(authScopes);
				}
				else
				{
					text2 = text2 + "+" + Helpers.AuthScopesToString(authScopes);
				}
			}
			if (string.IsNullOrWhiteSpace(text))
			{
				throw new BadParameterException("The clientId is not valid. It is not allowed to be null, empty or filled with whitespaces.");
			}
			return string.Concat(new string[]
			{
				"https://id.twitch.tv/oauth2/authorize?client_id=",
				text,
				"&redirect_uri=",
				HttpUtility.UrlEncode(redirectUri),
				"&response_type=code&scope=",
				text2,
				"&state=",
				state,
				"&",
				string.Format("force_verify={0}", forceVerify)
			});
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003CD8 File Offset: 0x00001ED8
		public Task<AuthCodeResponse> GetAccessTokenFromCodeAsync(string code, string clientSecret, string redirectUri, string clientId = null)
		{
			string text = clientId ?? this.Settings.ClientId;
			if (string.IsNullOrWhiteSpace(code))
			{
				throw new BadParameterException("The code is not valid. It is not allowed to be null, empty or filled with whitespaces.");
			}
			if (string.IsNullOrWhiteSpace(clientSecret))
			{
				throw new BadParameterException("The client secret is not valid. It is not allowed to be null, empty or filled with whitespaces.");
			}
			if (string.IsNullOrWhiteSpace(redirectUri))
			{
				throw new BadParameterException("The redirectUri is not valid. It is not allowed to be null, empty or filled with whitespaces.");
			}
			if (string.IsNullOrWhiteSpace(text))
			{
				throw new BadParameterException("The clientId is not valid. It is not allowed to be null, empty or filled with whitespaces.");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
			list.Add(new KeyValuePair<string, string>("code", code));
			list.Add(new KeyValuePair<string, string>("client_id", text));
			list.Add(new KeyValuePair<string, string>("client_secret", clientSecret));
			list.Add(new KeyValuePair<string, string>("redirect_uri", redirectUri));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchPostGenericAsync<AuthCodeResponse>("/token", ApiVersion.Auth, null, list2, null, text, null);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003DB4 File Offset: 0x00001FB4
		public Task<ValidateAccessTokenResponse> ValidateAccessTokenAsync(string accessToken = null)
		{
			Auth.<ValidateAccessTokenAsync>d__4 <ValidateAccessTokenAsync>d__;
			<ValidateAccessTokenAsync>d__.<>4__this = this;
			<ValidateAccessTokenAsync>d__.accessToken = accessToken;
			<ValidateAccessTokenAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ValidateAccessTokenResponse>.Create();
			<ValidateAccessTokenAsync>d__.<>1__state = -1;
			<ValidateAccessTokenAsync>d__.<>t__builder.Start<Auth.<ValidateAccessTokenAsync>d__4>(ref <ValidateAccessTokenAsync>d__);
			return <ValidateAccessTokenAsync>d__.<>t__builder.Task;
		}
	}
}
