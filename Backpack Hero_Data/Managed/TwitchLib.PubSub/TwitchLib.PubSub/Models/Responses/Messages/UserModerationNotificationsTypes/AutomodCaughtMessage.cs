using System;
using Newtonsoft.Json;

namespace TwitchLib.PubSub.Models.Responses.Messages.UserModerationNotificationsTypes
{
	// Token: 0x0200001C RID: 28
	public class AutomodCaughtMessage : UserModerationNotificationsData
	{
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00006E8F File Offset: 0x0000508F
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00006E97 File Offset: 0x00005097
		[JsonProperty(PropertyName = "message_id")]
		public string MessageId { get; protected set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00006EA0 File Offset: 0x000050A0
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00006EA8 File Offset: 0x000050A8
		[JsonProperty(PropertyName = "status")]
		public string Status { get; protected set; }
	}
}
