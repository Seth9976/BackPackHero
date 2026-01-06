using System;

namespace Unity.Services.Analytics.Internal
{
	// Token: 0x0200003A RID: 58
	internal interface IWebRequestHelper
	{
		// Token: 0x06000142 RID: 322
		IWebRequest CreateWebRequest(string url, string method, byte[] postBytes);

		// Token: 0x06000143 RID: 323
		void SendWebRequest(IWebRequest request, Action<long> onCompleted);
	}
}
