using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.BlockedTerms
{
	// Token: 0x02000063 RID: 99
	public class BlockedTerm
	{
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000332 RID: 818 RVA: 0x00003B6A File Offset: 0x00001D6A
		// (set) Token: 0x06000333 RID: 819 RVA: 0x00003B72 File Offset: 0x00001D72
		[JsonProperty(PropertyName = "broadcaster_id")]
		public string BroadcasterId { get; protected set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000334 RID: 820 RVA: 0x00003B7B File Offset: 0x00001D7B
		// (set) Token: 0x06000335 RID: 821 RVA: 0x00003B83 File Offset: 0x00001D83
		[JsonProperty(PropertyName = "moderator_id")]
		public string ModeratorId { get; protected set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000336 RID: 822 RVA: 0x00003B8C File Offset: 0x00001D8C
		// (set) Token: 0x06000337 RID: 823 RVA: 0x00003B94 File Offset: 0x00001D94
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00003B9D File Offset: 0x00001D9D
		// (set) Token: 0x06000339 RID: 825 RVA: 0x00003BA5 File Offset: 0x00001DA5
		[JsonProperty(PropertyName = "text")]
		public string Text { get; protected set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00003BAE File Offset: 0x00001DAE
		// (set) Token: 0x0600033B RID: 827 RVA: 0x00003BB6 File Offset: 0x00001DB6
		[JsonProperty(PropertyName = "created_at")]
		public DateTime CreatedAt { get; protected set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00003BBF File Offset: 0x00001DBF
		// (set) Token: 0x0600033D RID: 829 RVA: 0x00003BC7 File Offset: 0x00001DC7
		[JsonProperty(PropertyName = "updated_at")]
		public DateTime UpdatedAt { get; protected set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00003BD0 File Offset: 0x00001DD0
		// (set) Token: 0x0600033F RID: 831 RVA: 0x00003BD8 File Offset: 0x00001DD8
		[JsonProperty(PropertyName = "expires_at")]
		public DateTime? ExpiresAt { get; protected set; }
	}
}
