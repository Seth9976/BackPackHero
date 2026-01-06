using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Ads;

namespace TwitchLib.Api.Helix
{
	// Token: 0x02000007 RID: 7
	public class Ads : ApiBase
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002692 File Offset: 0x00000892
		public Ads(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
			: base(settings, rateLimiter, http)
		{
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000269D File Offset: 0x0000089D
		public Task<StartCommercialResponse> StartCommercialAsync(StartCommercialRequest request, string accessToken = null)
		{
			return base.TwitchPostGenericAsync<StartCommercialResponse>("/channels/commercial", ApiVersion.Helix, JsonConvert.SerializeObject(request), null, accessToken, null, null);
		}
	}
}
