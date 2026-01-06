using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Goals
{
	// Token: 0x02000073 RID: 115
	public class CreatorGoal
	{
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000407A File Offset: 0x0000227A
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x00004082 File Offset: 0x00002282
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000408B File Offset: 0x0000228B
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x00004093 File Offset: 0x00002293
		[JsonProperty(PropertyName = "broadcaster_id")]
		public string BroadcasterId { get; protected set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000409C File Offset: 0x0000229C
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x000040A4 File Offset: 0x000022A4
		[JsonProperty(PropertyName = "broadcaster_name")]
		public string BroadcasterName { get; protected set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x000040AD File Offset: 0x000022AD
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x000040B5 File Offset: 0x000022B5
		[JsonProperty(PropertyName = "broadcaster_login")]
		public string BroadcasterLogin { get; protected set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x000040BE File Offset: 0x000022BE
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x000040C6 File Offset: 0x000022C6
		[JsonProperty(PropertyName = "type")]
		public string Type { get; protected set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060003BA RID: 954 RVA: 0x000040CF File Offset: 0x000022CF
		// (set) Token: 0x060003BB RID: 955 RVA: 0x000040D7 File Offset: 0x000022D7
		[JsonProperty(PropertyName = "description")]
		public string Description { get; protected set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060003BC RID: 956 RVA: 0x000040E0 File Offset: 0x000022E0
		// (set) Token: 0x060003BD RID: 957 RVA: 0x000040E8 File Offset: 0x000022E8
		[JsonProperty(PropertyName = "current_amount")]
		public int CurrentAmount { get; protected set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060003BE RID: 958 RVA: 0x000040F1 File Offset: 0x000022F1
		// (set) Token: 0x060003BF RID: 959 RVA: 0x000040F9 File Offset: 0x000022F9
		[JsonProperty(PropertyName = "target_amount")]
		public int TargetAmount { get; protected set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00004102 File Offset: 0x00002302
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x0000410A File Offset: 0x0000230A
		[JsonProperty(PropertyName = "created_at")]
		public DateTime CreatedAt { get; protected set; }
	}
}
