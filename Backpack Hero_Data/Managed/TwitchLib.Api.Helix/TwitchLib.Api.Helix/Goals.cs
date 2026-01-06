using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Goals;

namespace TwitchLib.Api.Helix
{
	// Token: 0x02000013 RID: 19
	public class Goals : ApiBase
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00003DC1 File Offset: 0x00001FC1
		public Goals(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
			: base(settings, rateLimiter, http)
		{
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003DCC File Offset: 0x00001FCC
		public Task<GetCreatorGoalsResponse> GetCreatorGoalsAsync(string broadcasterId, string accessToken = null)
		{
			if (string.IsNullOrEmpty(broadcasterId))
			{
				throw new BadParameterException("broadcasterId cannot be null or empty");
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchGetGenericAsync<GetCreatorGoalsResponse>("/goals", ApiVersion.Helix, list2, accessToken, null, null);
		}
	}
}
