using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.GetBannedUsers
{
	// Token: 0x02000059 RID: 89
	public class BannedUserEvent
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x000038B6 File Offset: 0x00001AB6
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x000038BE File Offset: 0x00001ABE
		[JsonProperty(PropertyName = "user_id")]
		public string UserId { get; protected set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x000038C7 File Offset: 0x00001AC7
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x000038CF File Offset: 0x00001ACF
		[JsonProperty(PropertyName = "user_login")]
		public string UserLogin { get; protected set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x000038D8 File Offset: 0x00001AD8
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x000038E0 File Offset: 0x00001AE0
		[JsonProperty(PropertyName = "user_name")]
		public string UserName { get; protected set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x000038E9 File Offset: 0x00001AE9
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x000038F1 File Offset: 0x00001AF1
		[JsonProperty(PropertyName = "expires_at")]
		public DateTime? ExpiresAt { get; protected set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x000038FA File Offset: 0x00001AFA
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x00003902 File Offset: 0x00001B02
		[JsonProperty(PropertyName = "created_at")]
		public string CreatedAt { get; protected set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000390B File Offset: 0x00001B0B
		// (set) Token: 0x060002EB RID: 747 RVA: 0x00003913 File Offset: 0x00001B13
		[JsonProperty(PropertyName = "reason")]
		public string Reason { get; protected set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000391C File Offset: 0x00001B1C
		// (set) Token: 0x060002ED RID: 749 RVA: 0x00003924 File Offset: 0x00001B24
		[JsonProperty(PropertyName = "moderator_id")]
		public string ModeratorId { get; protected set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000392D File Offset: 0x00001B2D
		// (set) Token: 0x060002EF RID: 751 RVA: 0x00003935 File Offset: 0x00001B35
		[JsonProperty(PropertyName = "moderator_login")]
		public string ModeratorLogin { get; protected set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000393E File Offset: 0x00001B3E
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x00003946 File Offset: 0x00001B46
		[JsonProperty(PropertyName = "moderator_name")]
		public string ModeratorName { get; protected set; }
	}
}
