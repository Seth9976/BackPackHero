using System;
using UnityEngine.Networking;

namespace Unity.Services.Analytics.Internal
{
	// Token: 0x02000039 RID: 57
	internal interface IWebRequest
	{
		// Token: 0x0600013C RID: 316
		UnityWebRequestAsyncOperation SendWebRequest();

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600013D RID: 317
		// (set) Token: 0x0600013E RID: 318
		UploadHandler uploadHandler { get; set; }

		// Token: 0x0600013F RID: 319
		void SetRequestHeader(string key, string value);

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000140 RID: 320
		bool isNetworkError { get; }

		// Token: 0x06000141 RID: 321
		void Dispose();
	}
}
