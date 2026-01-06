using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.BanUser
{
	// Token: 0x02000067 RID: 103
	public class BanUserRequest
	{
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000352 RID: 850 RVA: 0x00003C78 File Offset: 0x00001E78
		// (set) Token: 0x06000353 RID: 851 RVA: 0x00003C80 File Offset: 0x00001E80
		[JsonProperty("user_id")]
		public string UserId { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000354 RID: 852 RVA: 0x00003C89 File Offset: 0x00001E89
		// (set) Token: 0x06000355 RID: 853 RVA: 0x00003C91 File Offset: 0x00001E91
		[JsonProperty("reason")]
		public string Reason { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00003C9A File Offset: 0x00001E9A
		// (set) Token: 0x06000357 RID: 855 RVA: 0x00003CA2 File Offset: 0x00001EA2
		[JsonProperty("duration", NullValueHandling = 1)]
		public int? Duration { get; set; }
	}
}
