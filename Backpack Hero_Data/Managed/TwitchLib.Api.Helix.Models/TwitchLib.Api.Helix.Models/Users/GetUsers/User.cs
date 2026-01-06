using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Users.GetUsers
{
	// Token: 0x0200000D RID: 13
	public class User
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000023AD File Offset: 0x000005AD
		// (set) Token: 0x06000063 RID: 99 RVA: 0x000023B5 File Offset: 0x000005B5
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000023BE File Offset: 0x000005BE
		// (set) Token: 0x06000065 RID: 101 RVA: 0x000023C6 File Offset: 0x000005C6
		[JsonProperty(PropertyName = "login")]
		public string Login { get; protected set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000023CF File Offset: 0x000005CF
		// (set) Token: 0x06000067 RID: 103 RVA: 0x000023D7 File Offset: 0x000005D7
		[JsonProperty(PropertyName = "display_name")]
		public string DisplayName { get; protected set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000023E0 File Offset: 0x000005E0
		// (set) Token: 0x06000069 RID: 105 RVA: 0x000023E8 File Offset: 0x000005E8
		[JsonProperty(PropertyName = "created_at")]
		public DateTime CreatedAt { get; protected set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000023F1 File Offset: 0x000005F1
		// (set) Token: 0x0600006B RID: 107 RVA: 0x000023F9 File Offset: 0x000005F9
		[JsonProperty(PropertyName = "type")]
		public string Type { get; protected set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002402 File Offset: 0x00000602
		// (set) Token: 0x0600006D RID: 109 RVA: 0x0000240A File Offset: 0x0000060A
		[JsonProperty(PropertyName = "broadcaster_type")]
		public string BroadcasterType { get; protected set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002413 File Offset: 0x00000613
		// (set) Token: 0x0600006F RID: 111 RVA: 0x0000241B File Offset: 0x0000061B
		[JsonProperty(PropertyName = "description")]
		public string Description { get; protected set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002424 File Offset: 0x00000624
		// (set) Token: 0x06000071 RID: 113 RVA: 0x0000242C File Offset: 0x0000062C
		[JsonProperty(PropertyName = "profile_image_url")]
		public string ProfileImageUrl { get; protected set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002435 File Offset: 0x00000635
		// (set) Token: 0x06000073 RID: 115 RVA: 0x0000243D File Offset: 0x0000063D
		[JsonProperty(PropertyName = "offline_image_url")]
		public string OfflineImageUrl { get; protected set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002446 File Offset: 0x00000646
		// (set) Token: 0x06000075 RID: 117 RVA: 0x0000244E File Offset: 0x0000064E
		[JsonProperty(PropertyName = "view_count")]
		public long ViewCount { get; protected set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002457 File Offset: 0x00000657
		// (set) Token: 0x06000077 RID: 119 RVA: 0x0000245F File Offset: 0x0000065F
		[JsonProperty(PropertyName = "email")]
		public string Email { get; protected set; }
	}
}
