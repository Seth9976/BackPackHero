using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.GetBannedEvents
{
	// Token: 0x0200005C RID: 92
	public class EventData
	{
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000303 RID: 771 RVA: 0x000039DE File Offset: 0x00001BDE
		// (set) Token: 0x06000304 RID: 772 RVA: 0x000039E6 File Offset: 0x00001BE6
		[JsonProperty(PropertyName = "broadcaster_id")]
		public string BroadcasterId { get; protected set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000305 RID: 773 RVA: 0x000039EF File Offset: 0x00001BEF
		// (set) Token: 0x06000306 RID: 774 RVA: 0x000039F7 File Offset: 0x00001BF7
		[JsonProperty(PropertyName = "broadcaster_login")]
		public string BroadcasterLogin { get; protected set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00003A00 File Offset: 0x00001C00
		// (set) Token: 0x06000308 RID: 776 RVA: 0x00003A08 File Offset: 0x00001C08
		[JsonProperty(PropertyName = "broadcaster_name")]
		public string BroadcasterName { get; protected set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00003A11 File Offset: 0x00001C11
		// (set) Token: 0x0600030A RID: 778 RVA: 0x00003A19 File Offset: 0x00001C19
		[JsonProperty(PropertyName = "user_id")]
		public string UserId { get; protected set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00003A22 File Offset: 0x00001C22
		// (set) Token: 0x0600030C RID: 780 RVA: 0x00003A2A File Offset: 0x00001C2A
		[JsonProperty(PropertyName = "user_login")]
		public string UserLogin { get; protected set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00003A33 File Offset: 0x00001C33
		// (set) Token: 0x0600030E RID: 782 RVA: 0x00003A3B File Offset: 0x00001C3B
		[JsonProperty(PropertyName = "user_name")]
		public string UserName { get; protected set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00003A44 File Offset: 0x00001C44
		// (set) Token: 0x06000310 RID: 784 RVA: 0x00003A4C File Offset: 0x00001C4C
		[JsonProperty(PropertyName = "expires_at")]
		public DateTime? ExpiresAt { get; protected set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00003A55 File Offset: 0x00001C55
		// (set) Token: 0x06000312 RID: 786 RVA: 0x00003A5D File Offset: 0x00001C5D
		[JsonProperty(PropertyName = "reason")]
		public string Reason { get; protected set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00003A66 File Offset: 0x00001C66
		// (set) Token: 0x06000314 RID: 788 RVA: 0x00003A6E File Offset: 0x00001C6E
		[JsonProperty(PropertyName = "moderator_id")]
		public string ModeratorId { get; protected set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00003A77 File Offset: 0x00001C77
		// (set) Token: 0x06000316 RID: 790 RVA: 0x00003A7F File Offset: 0x00001C7F
		[JsonProperty(PropertyName = "moderator_login")]
		public string ModeratorLogin { get; protected set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00003A88 File Offset: 0x00001C88
		// (set) Token: 0x06000318 RID: 792 RVA: 0x00003A90 File Offset: 0x00001C90
		[JsonProperty(PropertyName = "moderator_name")]
		public string ModeratorName { get; protected set; }
	}
}
