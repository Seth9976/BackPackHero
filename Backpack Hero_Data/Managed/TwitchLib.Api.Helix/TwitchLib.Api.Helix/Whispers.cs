using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchLib.Api.Helix
{
	// Token: 0x02000023 RID: 35
	public class Whispers : ApiBase
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x000061B2 File Offset: 0x000043B2
		public Whispers(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
			: base(settings, rateLimiter, http)
		{
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000061C0 File Offset: 0x000043C0
		public Task SendWhisperAsync(string fromUserId, string toUserId, string message, bool newRecipient, string accessToken = null)
		{
			if (string.IsNullOrWhiteSpace(fromUserId))
			{
				throw new BadParameterException("FromUserId must be set");
			}
			if (string.IsNullOrWhiteSpace(toUserId))
			{
				throw new BadParameterException("ToUserId must be set");
			}
			if (message == null)
			{
				throw new BadParameterException("message must be set");
			}
			int num = 500;
			if (!newRecipient)
			{
				num = 10000;
			}
			if (message.Length > num)
			{
				throw new BadParameterException(string.Format("message length must be less than or equal to {0} characters", num));
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("from_user_id", fromUserId));
			list.Add(new KeyValuePair<string, string>("to_user_id", toUserId));
			List<KeyValuePair<string, string>> list2 = list;
			JObject jobject = new JObject();
			jobject["message"] = message;
			JObject jobject2 = jobject;
			return base.TwitchPostAsync("/chat/announcements", ApiVersion.Helix, jobject2.ToString(), list2, accessToken, null, null);
		}
	}
}
