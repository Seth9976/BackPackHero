using System;
using UnityEngine.Networking;

namespace Unity.Services.Analytics.Internal
{
	// Token: 0x0200003C RID: 60
	internal class WebRequestHelper : IWebRequestHelper
	{
		// Token: 0x0600014A RID: 330 RVA: 0x00004F5C File Offset: 0x0000315C
		public IWebRequest CreateWebRequest(string url, string method, byte[] postBytes)
		{
			AnalyticsWebRequest analyticsWebRequest = new AnalyticsWebRequest(url, method);
			UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(postBytes)
			{
				contentType = "application/json"
			};
			analyticsWebRequest.uploadHandler = uploadHandlerRaw;
			return analyticsWebRequest;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004F8C File Offset: 0x0000318C
		public void SendWebRequest(IWebRequest request, Action<long> onCompleted)
		{
			UnityWebRequestAsyncOperation requestOp = request.SendWebRequest();
			requestOp.completed += delegate
			{
				onCompleted(requestOp.webRequest.responseCode);
			};
		}
	}
}
