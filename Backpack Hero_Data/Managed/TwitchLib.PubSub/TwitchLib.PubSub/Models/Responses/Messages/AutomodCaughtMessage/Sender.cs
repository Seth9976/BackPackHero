using System;
using Newtonsoft.Json;

namespace TwitchLib.PubSub.Models.Responses.Messages.AutomodCaughtMessage
{
	// Token: 0x0200002B RID: 43
	public class Sender
	{
		// Token: 0x040000EB RID: 235
		[JsonProperty(PropertyName = "user_id")]
		public string UserId;

		// Token: 0x040000EC RID: 236
		[JsonProperty(PropertyName = "login")]
		public string Login;

		// Token: 0x040000ED RID: 237
		[JsonProperty(PropertyName = "display_name")]
		public string DisplayName;
	}
}
