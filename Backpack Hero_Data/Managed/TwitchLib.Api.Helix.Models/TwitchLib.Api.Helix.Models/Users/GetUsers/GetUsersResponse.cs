using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Users.GetUsers
{
	// Token: 0x0200000C RID: 12
	public class GetUsersResponse
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002394 File Offset: 0x00000594
		// (set) Token: 0x06000060 RID: 96 RVA: 0x0000239C File Offset: 0x0000059C
		[JsonProperty(PropertyName = "data")]
		public User[] Users { get; protected set; }
	}
}
