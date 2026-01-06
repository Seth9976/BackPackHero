using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Polls
{
	// Token: 0x0200004D RID: 77
	public class Choice
	{
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000272 RID: 626 RVA: 0x00003515 File Offset: 0x00001715
		// (set) Token: 0x06000273 RID: 627 RVA: 0x0000351D File Offset: 0x0000171D
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000274 RID: 628 RVA: 0x00003526 File Offset: 0x00001726
		// (set) Token: 0x06000275 RID: 629 RVA: 0x0000352E File Offset: 0x0000172E
		[JsonProperty(PropertyName = "title")]
		public string Title { get; protected set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000276 RID: 630 RVA: 0x00003537 File Offset: 0x00001737
		// (set) Token: 0x06000277 RID: 631 RVA: 0x0000353F File Offset: 0x0000173F
		[JsonProperty(PropertyName = "votes")]
		public int Votes { get; protected set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000278 RID: 632 RVA: 0x00003548 File Offset: 0x00001748
		// (set) Token: 0x06000279 RID: 633 RVA: 0x00003550 File Offset: 0x00001750
		[JsonProperty(PropertyName = "channel_points_votes")]
		public int ChannelPointsVotes { get; protected set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600027A RID: 634 RVA: 0x00003559 File Offset: 0x00001759
		// (set) Token: 0x0600027B RID: 635 RVA: 0x00003561 File Offset: 0x00001761
		[JsonProperty(PropertyName = "bits_votes")]
		public int BitsVotes { get; protected set; }
	}
}
