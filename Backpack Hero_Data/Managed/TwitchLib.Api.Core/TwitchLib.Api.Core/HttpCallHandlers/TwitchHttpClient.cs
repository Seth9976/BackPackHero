using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Core.Internal;

namespace TwitchLib.Api.Core.HttpCallHandlers
{
	// Token: 0x0200000E RID: 14
	public class TwitchHttpClient : IHttpCallHandler
	{
		// Token: 0x0600005E RID: 94 RVA: 0x000031A9 File Offset: 0x000013A9
		public TwitchHttpClient(ILogger<TwitchHttpClient> logger = null)
		{
			this._logger = logger;
			this._http = new HttpClient(new TwitchHttpClientHandler(this._logger));
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000031D0 File Offset: 0x000013D0
		public Task PutBytesAsync(string url, byte[] payload)
		{
			TwitchHttpClient.<PutBytesAsync>d__3 <PutBytesAsync>d__;
			<PutBytesAsync>d__.<>4__this = this;
			<PutBytesAsync>d__.url = url;
			<PutBytesAsync>d__.payload = payload;
			<PutBytesAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<PutBytesAsync>d__.<>1__state = -1;
			<PutBytesAsync>d__.<>t__builder.Start<TwitchHttpClient.<PutBytesAsync>d__3>(ref <PutBytesAsync>d__);
			return <PutBytesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003224 File Offset: 0x00001424
		public Task<KeyValuePair<int, string>> GeneralRequestAsync(string url, string method, string payload = null, ApiVersion api = ApiVersion.Helix, string clientId = null, string accessToken = null)
		{
			TwitchHttpClient.<GeneralRequestAsync>d__4 <GeneralRequestAsync>d__;
			<GeneralRequestAsync>d__.<>4__this = this;
			<GeneralRequestAsync>d__.url = url;
			<GeneralRequestAsync>d__.method = method;
			<GeneralRequestAsync>d__.payload = payload;
			<GeneralRequestAsync>d__.api = api;
			<GeneralRequestAsync>d__.clientId = clientId;
			<GeneralRequestAsync>d__.accessToken = accessToken;
			<GeneralRequestAsync>d__.<>t__builder = AsyncTaskMethodBuilder<KeyValuePair<int, string>>.Create();
			<GeneralRequestAsync>d__.<>1__state = -1;
			<GeneralRequestAsync>d__.<>t__builder.Start<TwitchHttpClient.<GeneralRequestAsync>d__4>(ref <GeneralRequestAsync>d__);
			return <GeneralRequestAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000329C File Offset: 0x0000149C
		public Task<int> RequestReturnResponseCodeAsync(string url, string method, List<KeyValuePair<string, string>> getParams = null)
		{
			TwitchHttpClient.<RequestReturnResponseCodeAsync>d__5 <RequestReturnResponseCodeAsync>d__;
			<RequestReturnResponseCodeAsync>d__.<>4__this = this;
			<RequestReturnResponseCodeAsync>d__.url = url;
			<RequestReturnResponseCodeAsync>d__.method = method;
			<RequestReturnResponseCodeAsync>d__.getParams = getParams;
			<RequestReturnResponseCodeAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<RequestReturnResponseCodeAsync>d__.<>1__state = -1;
			<RequestReturnResponseCodeAsync>d__.<>t__builder.Start<TwitchHttpClient.<RequestReturnResponseCodeAsync>d__5>(ref <RequestReturnResponseCodeAsync>d__);
			return <RequestReturnResponseCodeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000032F8 File Offset: 0x000014F8
		private void HandleWebException(HttpResponseMessage errorResp)
		{
			HttpStatusCode statusCode = errorResp.StatusCode;
			switch (statusCode)
			{
			case 400:
				throw new BadRequestException("Your request failed because either: \n 1. Your ClientID was invalid/not set. \n 2. Your refresh token was invalid. \n 3. You requested a username when the server was expecting a user ID.");
			case 401:
			{
				HttpHeaderValueCollection<AuthenticationHeaderValue> wwwAuthenticate = errorResp.Headers.WwwAuthenticate;
				if (wwwAuthenticate == null || wwwAuthenticate.Count <= 0)
				{
					throw new BadScopeException("Your request was blocked due to bad credentials (Do you have the right scope for your access token?).");
				}
				throw new TokenExpiredException("Your request was blocked due to an expired Token. Please refresh your token and update your API instance settings.");
			}
			case 402:
				break;
			case 403:
				throw new BadTokenException("The token provided in the request did not match the associated user. Make sure the token you're using is from the resource owner (streamer? viewer?)");
			case 404:
				throw new BadResourceException("The resource you tried to access was not valid.");
			default:
				if (statusCode == 429)
				{
					IEnumerable<string> enumerable;
					errorResp.Headers.TryGetValues("Ratelimit-Reset", ref enumerable);
					throw new TooManyRequestsException("You have reached your rate limit. Too many requests were made", Enumerable.FirstOrDefault<string>(enumerable));
				}
				switch (statusCode)
				{
				case 500:
					throw new InternalServerErrorException("The API answered with a 500 Internal Server Error. Please retry your request");
				case 502:
					throw new BadGatewayException("The API answered with a 502 Bad Gateway. Please retry your request");
				case 504:
					throw new GatewayTimeoutException("The API answered with a 504 Gateway Timeout. Please retry your request");
				}
				break;
			}
			throw new HttpRequestException("Something went wrong during the request! Please try again later");
		}

		// Token: 0x0400001F RID: 31
		private readonly ILogger<TwitchHttpClient> _logger;

		// Token: 0x04000020 RID: 32
		private readonly HttpClient _http;
	}
}
