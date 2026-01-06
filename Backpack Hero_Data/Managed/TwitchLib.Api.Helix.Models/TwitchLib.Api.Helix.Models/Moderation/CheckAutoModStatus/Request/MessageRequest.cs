using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.CheckAutoModStatus.Request
{
	// Token: 0x02000061 RID: 97
	public class MessageRequest
	{
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00003B38 File Offset: 0x00001D38
		// (set) Token: 0x0600032D RID: 813 RVA: 0x00003B40 File Offset: 0x00001D40
		[JsonProperty(PropertyName = "data")]
		public Message[] Messages { get; set; }
	}
}
