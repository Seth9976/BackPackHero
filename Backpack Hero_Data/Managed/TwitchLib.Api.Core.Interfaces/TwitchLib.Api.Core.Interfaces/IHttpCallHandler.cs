using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Core.Interfaces
{
	// Token: 0x02000006 RID: 6
	public interface IHttpCallHandler
	{
		// Token: 0x06000016 RID: 22
		Task<KeyValuePair<int, string>> GeneralRequestAsync(string url, string method, string payload = null, ApiVersion api = ApiVersion.Helix, string clientId = null, string accessToken = null);

		// Token: 0x06000017 RID: 23
		Task PutBytesAsync(string url, byte[] payload);

		// Token: 0x06000018 RID: 24
		Task<int> RequestReturnResponseCodeAsync(string url, string method, List<KeyValuePair<string, string>> getParams = null);
	}
}
