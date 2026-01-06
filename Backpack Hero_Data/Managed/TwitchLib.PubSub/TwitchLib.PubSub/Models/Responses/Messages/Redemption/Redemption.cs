using System;
using Newtonsoft.Json;

namespace TwitchLib.PubSub.Models.Responses.Messages.Redemption
{
	// Token: 0x02000021 RID: 33
	public class Redemption
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00006FD6 File Offset: 0x000051D6
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00006FDE File Offset: 0x000051DE
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00006FE7 File Offset: 0x000051E7
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00006FEF File Offset: 0x000051EF
		[JsonProperty(PropertyName = "user")]
		public User User { get; protected set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00006FF8 File Offset: 0x000051F8
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00007000 File Offset: 0x00005200
		[JsonProperty(PropertyName = "channel_id")]
		public string ChannelId { get; protected set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00007009 File Offset: 0x00005209
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00007011 File Offset: 0x00005211
		[JsonProperty(PropertyName = "redeemed_at")]
		public DateTime RedeemedAt { get; protected set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600016B RID: 363 RVA: 0x0000701A File Offset: 0x0000521A
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00007022 File Offset: 0x00005222
		[JsonProperty(PropertyName = "reward")]
		public Reward Reward { get; protected set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600016D RID: 365 RVA: 0x0000702B File Offset: 0x0000522B
		// (set) Token: 0x0600016E RID: 366 RVA: 0x00007033 File Offset: 0x00005233
		[JsonProperty(PropertyName = "user_input")]
		public string UserInput { get; protected set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600016F RID: 367 RVA: 0x0000703C File Offset: 0x0000523C
		// (set) Token: 0x06000170 RID: 368 RVA: 0x00007044 File Offset: 0x00005244
		[JsonProperty(PropertyName = "status")]
		public string Status { get; protected set; }
	}
}
