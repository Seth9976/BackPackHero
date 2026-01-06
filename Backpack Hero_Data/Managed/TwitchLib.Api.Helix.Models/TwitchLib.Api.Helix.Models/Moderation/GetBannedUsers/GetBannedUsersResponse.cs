using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Moderation.GetBannedUsers
{
	// Token: 0x0200005A RID: 90
	public class GetBannedUsersResponse
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00003957 File Offset: 0x00001B57
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x0000395F File Offset: 0x00001B5F
		[JsonProperty(PropertyName = "data")]
		public BannedUserEvent[] Data { get; protected set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00003968 File Offset: 0x00001B68
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x00003970 File Offset: 0x00001B70
		[JsonProperty(PropertyName = "pagination")]
		public Pagination Pagination { get; protected set; }
	}
}
