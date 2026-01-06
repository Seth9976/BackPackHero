using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.BanUser
{
	// Token: 0x02000065 RID: 101
	public class BannedUser
	{
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00003C13 File Offset: 0x00001E13
		// (set) Token: 0x06000347 RID: 839 RVA: 0x00003C1B File Offset: 0x00001E1B
		[JsonProperty(PropertyName = "broadcaster_id")]
		public string BroadcasterId { get; protected set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00003C24 File Offset: 0x00001E24
		// (set) Token: 0x06000349 RID: 841 RVA: 0x00003C2C File Offset: 0x00001E2C
		[JsonProperty(PropertyName = "created_at")]
		public string CreatedAt { get; protected set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600034A RID: 842 RVA: 0x00003C35 File Offset: 0x00001E35
		// (set) Token: 0x0600034B RID: 843 RVA: 0x00003C3D File Offset: 0x00001E3D
		[JsonProperty(PropertyName = "moderator_id")]
		public string ModeratorId { get; protected set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600034C RID: 844 RVA: 0x00003C46 File Offset: 0x00001E46
		// (set) Token: 0x0600034D RID: 845 RVA: 0x00003C4E File Offset: 0x00001E4E
		[JsonProperty(PropertyName = "user_id")]
		public string UserId { get; protected set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00003C57 File Offset: 0x00001E57
		// (set) Token: 0x0600034F RID: 847 RVA: 0x00003C5F File Offset: 0x00001E5F
		[JsonProperty(PropertyName = "end_time")]
		public DateTime? EndTime { get; protected set; }
	}
}
