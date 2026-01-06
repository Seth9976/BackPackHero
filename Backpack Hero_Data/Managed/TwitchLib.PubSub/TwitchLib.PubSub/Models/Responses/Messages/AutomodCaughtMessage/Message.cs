using System;
using Newtonsoft.Json;

namespace TwitchLib.PubSub.Models.Responses.Messages.AutomodCaughtMessage
{
	// Token: 0x0200002A RID: 42
	public class Message
	{
		// Token: 0x040000E7 RID: 231
		[JsonProperty(PropertyName = "content")]
		public Content Content;

		// Token: 0x040000E8 RID: 232
		[JsonProperty(PropertyName = "id")]
		public string Id;

		// Token: 0x040000E9 RID: 233
		[JsonProperty(PropertyName = "sender")]
		public Sender Sender;

		// Token: 0x040000EA RID: 234
		[JsonProperty(PropertyName = "sent_at")]
		public DateTime SentAt;
	}
}
