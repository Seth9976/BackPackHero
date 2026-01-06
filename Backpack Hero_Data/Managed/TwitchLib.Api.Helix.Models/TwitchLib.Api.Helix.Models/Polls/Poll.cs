using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Polls
{
	// Token: 0x0200004E RID: 78
	public class Poll
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600027D RID: 637 RVA: 0x00003572 File Offset: 0x00001772
		// (set) Token: 0x0600027E RID: 638 RVA: 0x0000357A File Offset: 0x0000177A
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600027F RID: 639 RVA: 0x00003583 File Offset: 0x00001783
		// (set) Token: 0x06000280 RID: 640 RVA: 0x0000358B File Offset: 0x0000178B
		[JsonProperty(PropertyName = "broadcaster_id")]
		public string BroadcasterId { get; protected set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000281 RID: 641 RVA: 0x00003594 File Offset: 0x00001794
		// (set) Token: 0x06000282 RID: 642 RVA: 0x0000359C File Offset: 0x0000179C
		[JsonProperty(PropertyName = "broadcaster_name")]
		public string BroadcasterName { get; protected set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000283 RID: 643 RVA: 0x000035A5 File Offset: 0x000017A5
		// (set) Token: 0x06000284 RID: 644 RVA: 0x000035AD File Offset: 0x000017AD
		[JsonProperty(PropertyName = "broadcaster_login")]
		public string BroadcasterLogin { get; protected set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000285 RID: 645 RVA: 0x000035B6 File Offset: 0x000017B6
		// (set) Token: 0x06000286 RID: 646 RVA: 0x000035BE File Offset: 0x000017BE
		[JsonProperty(PropertyName = "title")]
		public string Title { get; protected set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000287 RID: 647 RVA: 0x000035C7 File Offset: 0x000017C7
		// (set) Token: 0x06000288 RID: 648 RVA: 0x000035CF File Offset: 0x000017CF
		[JsonProperty(PropertyName = "choices")]
		public Choice[] Choices { get; protected set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000289 RID: 649 RVA: 0x000035D8 File Offset: 0x000017D8
		// (set) Token: 0x0600028A RID: 650 RVA: 0x000035E0 File Offset: 0x000017E0
		[JsonProperty(PropertyName = "bits_voting_enabled")]
		public bool BitsVotingEnabled { get; protected set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600028B RID: 651 RVA: 0x000035E9 File Offset: 0x000017E9
		// (set) Token: 0x0600028C RID: 652 RVA: 0x000035F1 File Offset: 0x000017F1
		[JsonProperty(PropertyName = "bits_per_vote")]
		public int BitsPerVote { get; protected set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600028D RID: 653 RVA: 0x000035FA File Offset: 0x000017FA
		// (set) Token: 0x0600028E RID: 654 RVA: 0x00003602 File Offset: 0x00001802
		[JsonProperty(PropertyName = "channel_points_voting_enabled")]
		public bool ChannelPointsVotingEnabled { get; protected set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000360B File Offset: 0x0000180B
		// (set) Token: 0x06000290 RID: 656 RVA: 0x00003613 File Offset: 0x00001813
		[JsonProperty(PropertyName = "channel_points_per_vote")]
		public int ChannelPointsPerVote { get; protected set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000361C File Offset: 0x0000181C
		// (set) Token: 0x06000292 RID: 658 RVA: 0x00003624 File Offset: 0x00001824
		[JsonProperty(PropertyName = "status")]
		public string Status { get; protected set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000362D File Offset: 0x0000182D
		// (set) Token: 0x06000294 RID: 660 RVA: 0x00003635 File Offset: 0x00001835
		[JsonProperty(PropertyName = "duration")]
		public int DurationSeconds { get; protected set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000363E File Offset: 0x0000183E
		// (set) Token: 0x06000296 RID: 662 RVA: 0x00003646 File Offset: 0x00001846
		[JsonProperty(PropertyName = "started_at")]
		public DateTime StartedAt { get; protected set; }
	}
}
