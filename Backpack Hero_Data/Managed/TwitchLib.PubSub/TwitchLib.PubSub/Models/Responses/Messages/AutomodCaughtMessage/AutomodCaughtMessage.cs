using System;
using Newtonsoft.Json;

namespace TwitchLib.PubSub.Models.Responses.Messages.AutomodCaughtMessage
{
	// Token: 0x02000026 RID: 38
	public class AutomodCaughtMessage : AutomodQueueData
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000721E File Offset: 0x0000541E
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00007226 File Offset: 0x00005426
		[JsonProperty(PropertyName = "content_classification")]
		public ContentClassification ContentClassification { get; protected set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001AA RID: 426 RVA: 0x0000722F File Offset: 0x0000542F
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00007237 File Offset: 0x00005437
		[JsonProperty(PropertyName = "message")]
		public Message Message { get; protected set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00007240 File Offset: 0x00005440
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00007248 File Offset: 0x00005448
		[JsonProperty(PropertyName = "reason_code")]
		public string ReasonCode { get; protected set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00007251 File Offset: 0x00005451
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00007259 File Offset: 0x00005459
		[JsonProperty(PropertyName = "resolver_id")]
		public string ResolverId { get; protected set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00007262 File Offset: 0x00005462
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x0000726A File Offset: 0x0000546A
		[JsonProperty(PropertyName = "resolver_login")]
		public string ResolverLogin { get; protected set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00007273 File Offset: 0x00005473
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x0000727B File Offset: 0x0000547B
		[JsonProperty(PropertyName = "status")]
		public string Status { get; protected set; }
	}
}
