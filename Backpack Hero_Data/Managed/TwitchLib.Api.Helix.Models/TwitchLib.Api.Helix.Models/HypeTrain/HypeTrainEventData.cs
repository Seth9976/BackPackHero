using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.HypeTrain
{
	// Token: 0x02000071 RID: 113
	public class HypeTrainEventData
	{
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00003DD8 File Offset: 0x00001FD8
		// (set) Token: 0x0600037D RID: 893 RVA: 0x00003DE0 File Offset: 0x00001FE0
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00003DE9 File Offset: 0x00001FE9
		// (set) Token: 0x0600037F RID: 895 RVA: 0x00003DF1 File Offset: 0x00001FF1
		[JsonProperty(PropertyName = "broadcaster_id")]
		public string BroadcasterId { get; protected set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00003DFA File Offset: 0x00001FFA
		// (set) Token: 0x06000381 RID: 897 RVA: 0x00003E02 File Offset: 0x00002002
		[JsonProperty(PropertyName = "started_at")]
		public string StartedAt { get; protected set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000382 RID: 898 RVA: 0x00003E0B File Offset: 0x0000200B
		// (set) Token: 0x06000383 RID: 899 RVA: 0x00003E13 File Offset: 0x00002013
		[JsonProperty(PropertyName = "expires_at")]
		public string ExpiresAt { get; protected set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000384 RID: 900 RVA: 0x00003E1C File Offset: 0x0000201C
		// (set) Token: 0x06000385 RID: 901 RVA: 0x00003E24 File Offset: 0x00002024
		[JsonProperty(PropertyName = "cooldown_end_time")]
		public string CooldownEndTime { get; protected set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000386 RID: 902 RVA: 0x00003E2D File Offset: 0x0000202D
		// (set) Token: 0x06000387 RID: 903 RVA: 0x00003E35 File Offset: 0x00002035
		[JsonProperty(PropertyName = "level")]
		public int Level { get; protected set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000388 RID: 904 RVA: 0x00003E3E File Offset: 0x0000203E
		// (set) Token: 0x06000389 RID: 905 RVA: 0x00003E46 File Offset: 0x00002046
		[JsonProperty(PropertyName = "goal")]
		public int Goal { get; protected set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600038A RID: 906 RVA: 0x00003E4F File Offset: 0x0000204F
		// (set) Token: 0x0600038B RID: 907 RVA: 0x00003E57 File Offset: 0x00002057
		[JsonProperty(PropertyName = "total")]
		public int Total { get; protected set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600038C RID: 908 RVA: 0x00003E60 File Offset: 0x00002060
		// (set) Token: 0x0600038D RID: 909 RVA: 0x00003E68 File Offset: 0x00002068
		[JsonProperty(PropertyName = "top_contribution")]
		public HypeTrainContribution TopContribution { get; protected set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600038E RID: 910 RVA: 0x00003E71 File Offset: 0x00002071
		// (set) Token: 0x0600038F RID: 911 RVA: 0x00003E79 File Offset: 0x00002079
		[JsonProperty(PropertyName = "last_contribution")]
		public HypeTrainContribution LastContribution { get; protected set; }
	}
}
