using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Polls.CreatePoll
{
	// Token: 0x02000052 RID: 82
	public class CreatePollRequest
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x000036B3 File Offset: 0x000018B3
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x000036BB File Offset: 0x000018BB
		[JsonProperty(PropertyName = "broadcaster_id")]
		public string BroadcasterId { get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x000036C4 File Offset: 0x000018C4
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x000036CC File Offset: 0x000018CC
		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x000036D5 File Offset: 0x000018D5
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x000036DD File Offset: 0x000018DD
		[JsonProperty(PropertyName = "choices")]
		public Choice[] Choices { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x000036E6 File Offset: 0x000018E6
		// (set) Token: 0x060002AA RID: 682 RVA: 0x000036EE File Offset: 0x000018EE
		[JsonProperty(PropertyName = "bits_voting_enabled")]
		public bool BitsVotingEnabled { get; set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060002AB RID: 683 RVA: 0x000036F7 File Offset: 0x000018F7
		// (set) Token: 0x060002AC RID: 684 RVA: 0x000036FF File Offset: 0x000018FF
		[JsonProperty(PropertyName = "bits_per_vote")]
		public int BitsPerVote { get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00003708 File Offset: 0x00001908
		// (set) Token: 0x060002AE RID: 686 RVA: 0x00003710 File Offset: 0x00001910
		[JsonProperty(PropertyName = "channel_points_voting_enabled")]
		public bool ChannelPointsVotingEnabled { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00003719 File Offset: 0x00001919
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x00003721 File Offset: 0x00001921
		[JsonProperty(PropertyName = "channel_points_per_vote")]
		public int ChannelPointsPerVote { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000372A File Offset: 0x0000192A
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x00003732 File Offset: 0x00001932
		[JsonProperty(PropertyName = "duration")]
		public int DurationSeconds { get; set; }
	}
}
