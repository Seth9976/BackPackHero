using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Users.GetUserFollows
{
	// Token: 0x0200000F RID: 15
	public class GetUsersFollowsResponse
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000024EF File Offset: 0x000006EF
		// (set) Token: 0x06000089 RID: 137 RVA: 0x000024F7 File Offset: 0x000006F7
		[JsonProperty(PropertyName = "data")]
		public Follow[] Follows { get; protected set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002500 File Offset: 0x00000700
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00002508 File Offset: 0x00000708
		[JsonProperty(PropertyName = "pagination")]
		public Pagination Pagination { get; protected set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002511 File Offset: 0x00000711
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00002519 File Offset: 0x00000719
		[JsonProperty(PropertyName = "total")]
		public long TotalFollows { get; protected set; }
	}
}
