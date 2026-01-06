using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchLib.Api.Core.HttpCallHandlers
{
	// Token: 0x0200000F RID: 15
	[Obsolete("The WebRequest handler is deprecated and is not updated to be working with Helix correctly")]
	public class TwitchWebRequest : IHttpCallHandler
	{
		// Token: 0x06000063 RID: 99 RVA: 0x000033F7 File Offset: 0x000015F7
		public TwitchWebRequest(ILogger<TwitchWebRequest> logger = null)
		{
			this._logger = logger;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003408 File Offset: 0x00001608
		public Task PutBytesAsync(string url, byte[] payload)
		{
			return Task.Factory.StartNew(delegate
			{
				try
				{
					using (WebClient webClient = new WebClient())
					{
						webClient.UploadData(new Uri(url), "PUT", payload);
					}
				}
				catch (WebException ex)
				{
					this.HandleWebException(ex);
				}
			});
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003448 File Offset: 0x00001648
		public Task<KeyValuePair<int, string>> GeneralRequestAsync(string url, string method, string payload = null, ApiVersion api = ApiVersion.Helix, string clientId = null, string accessToken = null)
		{
			TwitchWebRequest.<GeneralRequestAsync>d__3 <GeneralRequestAsync>d__;
			<GeneralRequestAsync>d__.<>4__this = this;
			<GeneralRequestAsync>d__.url = url;
			<GeneralRequestAsync>d__.method = method;
			<GeneralRequestAsync>d__.payload = payload;
			<GeneralRequestAsync>d__.api = api;
			<GeneralRequestAsync>d__.clientId = clientId;
			<GeneralRequestAsync>d__.accessToken = accessToken;
			<GeneralRequestAsync>d__.<>t__builder = AsyncTaskMethodBuilder<KeyValuePair<int, string>>.Create();
			<GeneralRequestAsync>d__.<>1__state = -1;
			<GeneralRequestAsync>d__.<>t__builder.Start<TwitchWebRequest.<GeneralRequestAsync>d__3>(ref <GeneralRequestAsync>d__);
			return <GeneralRequestAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000034C0 File Offset: 0x000016C0
		public Task<int> RequestReturnResponseCodeAsync(string url, string method, List<KeyValuePair<string, string>> getParams = null)
		{
			return Task.Factory.StartNew<int>(delegate
			{
				if (getParams != null)
				{
					for (int i = 0; i < getParams.Count; i++)
					{
						if (i == 0)
						{
							url = string.Concat(new string[]
							{
								url,
								"?",
								getParams[i].Key,
								"=",
								Uri.EscapeDataString(getParams[i].Value)
							});
						}
						else
						{
							url = string.Concat(new string[]
							{
								url,
								"&",
								getParams[i].Key,
								"=",
								Uri.EscapeDataString(getParams[i].Value)
							});
						}
					}
				}
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
				httpWebRequest.Method = method;
				return ((HttpWebResponse)httpWebRequest.GetResponse()).StatusCode;
			});
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003500 File Offset: 0x00001700
		private void HandleWebException(WebException e)
		{
			HttpWebResponse httpWebResponse = e.Response as HttpWebResponse;
			if (httpWebResponse == null)
			{
				throw e;
			}
			HttpStatusCode statusCode = httpWebResponse.StatusCode;
			switch (statusCode)
			{
			case 400:
				throw new BadRequestException("Your request failed because either: \n 1. Your ClientID was invalid/not set. \n 2. Your refresh token was invalid. \n 3. You requested a username when the server was expecting a user ID.");
			case 401:
			{
				string[] values = httpWebResponse.Headers.GetValues("WWW-Authenticate");
				if ((values != null && values.Length == 0) || string.IsNullOrEmpty((values != null) ? values[0] : null))
				{
					throw new BadScopeException("Your request was blocked due to bad credentials (do you have the right scope for your access token?).");
				}
				if (values[0].Contains("error='invalid_token'"))
				{
					throw new TokenExpiredException("Your request was blocked du to an expired Token. Please refresh your token and update your API instance settings.");
				}
				return;
			}
			case 402:
			case 403:
				break;
			case 404:
				throw new BadResourceException("The resource you tried to access was not valid.");
			default:
				if (statusCode == 429)
				{
					string text = httpWebResponse.Headers.Get("Ratelimit-Reset");
					throw new TooManyRequestsException("You have reached your rate limit. Too many requests were made", text);
				}
				break;
			}
			throw e;
		}

		// Token: 0x04000021 RID: 33
		private readonly ILogger<TwitchWebRequest> _logger;
	}
}
