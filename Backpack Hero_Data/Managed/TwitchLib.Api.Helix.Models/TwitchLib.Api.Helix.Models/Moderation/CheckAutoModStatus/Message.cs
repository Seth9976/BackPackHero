using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.CheckAutoModStatus
{
	// Token: 0x02000060 RID: 96
	public class Message
	{
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000327 RID: 807 RVA: 0x00003B0E File Offset: 0x00001D0E
		// (set) Token: 0x06000328 RID: 808 RVA: 0x00003B16 File Offset: 0x00001D16
		[JsonProperty(PropertyName = "msg_id")]
		public string MsgId { get; set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00003B1F File Offset: 0x00001D1F
		// (set) Token: 0x0600032A RID: 810 RVA: 0x00003B27 File Offset: 0x00001D27
		[JsonProperty(PropertyName = "msg_text")]
		public string MsgText { get; set; }
	}
}
