using System;
using Unity.Services.Core.Networking.Internal;

namespace Unity.Services.Core.Networking
{
	// Token: 0x02000002 RID: 2
	[Serializable]
	internal struct HttpServiceConfig
	{
		// Token: 0x04000001 RID: 1
		public string ServiceId;

		// Token: 0x04000002 RID: 2
		public string BaseUrl;

		// Token: 0x04000003 RID: 3
		public HttpOptions DefaultOptions;
	}
}
