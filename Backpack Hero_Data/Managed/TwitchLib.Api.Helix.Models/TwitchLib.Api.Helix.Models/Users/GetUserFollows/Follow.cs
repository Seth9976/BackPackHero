using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Users.GetUserFollows
{
	// Token: 0x0200000E RID: 14
	public class Follow
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002470 File Offset: 0x00000670
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00002478 File Offset: 0x00000678
		[JsonProperty(PropertyName = "from_id")]
		public string FromUserId { get; protected set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002481 File Offset: 0x00000681
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00002489 File Offset: 0x00000689
		[JsonProperty(PropertyName = "from_login")]
		public string FromLogin { get; protected set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002492 File Offset: 0x00000692
		// (set) Token: 0x0600007E RID: 126 RVA: 0x0000249A File Offset: 0x0000069A
		[JsonProperty(PropertyName = "from_name")]
		public string FromUserName { get; protected set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000024A3 File Offset: 0x000006A3
		// (set) Token: 0x06000080 RID: 128 RVA: 0x000024AB File Offset: 0x000006AB
		[JsonProperty(PropertyName = "to_id")]
		public string ToUserId { get; protected set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000024B4 File Offset: 0x000006B4
		// (set) Token: 0x06000082 RID: 130 RVA: 0x000024BC File Offset: 0x000006BC
		[JsonProperty(PropertyName = "to_login")]
		public string ToLogin { get; protected set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000024C5 File Offset: 0x000006C5
		// (set) Token: 0x06000084 RID: 132 RVA: 0x000024CD File Offset: 0x000006CD
		[JsonProperty(PropertyName = "to_name")]
		public string ToUserName { get; protected set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000024D6 File Offset: 0x000006D6
		// (set) Token: 0x06000086 RID: 134 RVA: 0x000024DE File Offset: 0x000006DE
		[JsonProperty(PropertyName = "followed_at")]
		public DateTime FollowedAt { get; protected set; }
	}
}
