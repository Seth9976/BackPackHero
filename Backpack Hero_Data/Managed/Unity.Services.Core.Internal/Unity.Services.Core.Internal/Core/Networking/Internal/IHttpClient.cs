using System;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Networking.Internal
{
	// Token: 0x02000019 RID: 25
	internal interface IHttpClient : IServiceComponent
	{
		// Token: 0x06000049 RID: 73
		string GetBaseUrlFor(string serviceId);

		// Token: 0x0600004A RID: 74
		HttpOptions GetDefaultOptionsFor(string serviceId);

		// Token: 0x0600004B RID: 75
		HttpRequest CreateRequestForService(string serviceId, string resourcePath);

		// Token: 0x0600004C RID: 76
		IAsyncOperation<ReadOnlyHttpResponse> Send(HttpRequest request);
	}
}
