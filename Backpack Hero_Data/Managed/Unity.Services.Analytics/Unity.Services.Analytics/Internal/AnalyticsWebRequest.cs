using System;
using UnityEngine.Networking;

namespace Unity.Services.Analytics.Internal
{
	// Token: 0x0200003B RID: 59
	internal class AnalyticsWebRequest : UnityWebRequest, IWebRequest
	{
		// Token: 0x06000144 RID: 324 RVA: 0x00004F24 File Offset: 0x00003124
		internal AnalyticsWebRequest(string url, string method)
			: base(url, method)
		{
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00004F2E File Offset: 0x0000312E
		UnityWebRequestAsyncOperation IWebRequest.SendWebRequest()
		{
			return base.SendWebRequest();
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00004F36 File Offset: 0x00003136
		UploadHandler IWebRequest.get_uploadHandler()
		{
			return base.uploadHandler;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00004F3E File Offset: 0x0000313E
		void IWebRequest.set_uploadHandler(UploadHandler value)
		{
			base.uploadHandler = value;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004F47 File Offset: 0x00003147
		void IWebRequest.SetRequestHeader(string key, string value)
		{
			base.SetRequestHeader(key, value);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00004F51 File Offset: 0x00003151
		bool IWebRequest.get_isNetworkError()
		{
			return base.isNetworkError;
		}
	}
}
