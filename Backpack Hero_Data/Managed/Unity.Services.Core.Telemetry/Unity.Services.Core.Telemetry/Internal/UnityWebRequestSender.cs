using System;
using Unity.Services.Core.Internal;
using UnityEngine.Networking;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x0200001D RID: 29
	internal class UnityWebRequestSender : IUnityWebRequestSender
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00003160 File Offset: 0x00001360
		public void SendRequest(UnityWebRequest request, Action<WebRequest> callback)
		{
			UnityWebRequestSender.<>c__DisplayClass0_0 CS$<>8__locals1 = new UnityWebRequestSender.<>c__DisplayClass0_0();
			CS$<>8__locals1.callback = callback;
			request.SendWebRequest().completed += CS$<>8__locals1.<SendRequest>g__OnSendingRequestCompleted|0;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003194 File Offset: 0x00001394
		private static WebRequest Simplify(UnityWebRequest webRequest)
		{
			WebRequest webRequest2 = new WebRequest
			{
				ResponseCode = webRequest.responseCode
			};
			if (webRequest.HasSucceeded())
			{
				webRequest2.Result = WebRequestResult.Success;
			}
			else
			{
				UnityWebRequest.Result result = webRequest.result;
				if (result != UnityWebRequest.Result.ConnectionError)
				{
					if (result != UnityWebRequest.Result.ProtocolError)
					{
						webRequest2.Result = WebRequestResult.UnknownError;
					}
					else
					{
						webRequest2.Result = WebRequestResult.ProtocolError;
					}
				}
				else
				{
					webRequest2.Result = WebRequestResult.ConnectionError;
				}
				webRequest2.ErrorMessage = webRequest.error;
				webRequest2.ErrorBody = webRequest.downloadHandler.text;
			}
			return webRequest2;
		}
	}
}
