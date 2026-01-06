using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Unity.Services.Analytics.Internal
{
	// Token: 0x0200002F RID: 47
	internal class GeoAPI : IGeoAPI
	{
		// Token: 0x060000FF RID: 255 RVA: 0x00004794 File Offset: 0x00002994
		public async Task<GeoIPResponse> MakeRequest()
		{
			UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = await new GeoAPI.WebRequestTaskWrapper(new UnityWebRequest(this.m_PrivacyEndpoint, "GET")
			{
				timeout = 10,
				downloadHandler = new DownloadHandlerBuffer()
			});
			if (unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ProtocolError || unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ConnectionError)
			{
				throw new ConsentCheckException(ConsentCheckExceptionReason.NoInternetConnection, 1, "The GeoIP request has failed - make sure you're connected to an internet connection and try again.", null);
			}
			GeoIPResponse geoIPResponse2;
			try
			{
				GeoIPResponse geoIPResponse = JsonConvert.DeserializeObject<GeoIPResponse>(unityWebRequestAsyncOperation.webRequest.downloadHandler.text);
				if (geoIPResponse == null)
				{
					throw new ConsentCheckException(ConsentCheckExceptionReason.Unknown, 0, "The error occurred while performing the privacy GeoIP request. Please try again later.", null);
				}
				geoIPResponse2 = geoIPResponse;
			}
			catch (Exception)
			{
				throw new ConsentCheckException(ConsentCheckExceptionReason.DeserializationIssue, 0, "The error occurred while deserializing the privacy GeoIP reseponse. Please try again later.", null);
			}
			return geoIPResponse2;
		}

		// Token: 0x040000C6 RID: 198
		private readonly string m_PrivacyEndpoint = "https://pls.prd.mz.internal.unity3d.com/api/v1/user-lookup";

		// Token: 0x02000059 RID: 89
		private class WebRequestTaskWrapper
		{
			// Token: 0x060001B9 RID: 441 RVA: 0x00006CE6 File Offset: 0x00004EE6
			public WebRequestTaskWrapper(UnityWebRequest request)
			{
				this.m_AsyncOp = request.SendWebRequest();
			}

			// Token: 0x060001BA RID: 442 RVA: 0x00006CFC File Offset: 0x00004EFC
			public TaskAwaiter<UnityWebRequestAsyncOperation> GetAwaiter()
			{
				TaskCompletionSource<UnityWebRequestAsyncOperation> tcs = new TaskCompletionSource<UnityWebRequestAsyncOperation>();
				this.m_AsyncOp.completed += delegate(AsyncOperation obj)
				{
					UnityWebRequestAsyncOperation asyncOp = this.m_AsyncOp;
					tcs.SetResult(asyncOp);
				};
				return tcs.Task.GetAwaiter();
			}

			// Token: 0x04000170 RID: 368
			private readonly UnityWebRequestAsyncOperation m_AsyncOp;
		}
	}
}
