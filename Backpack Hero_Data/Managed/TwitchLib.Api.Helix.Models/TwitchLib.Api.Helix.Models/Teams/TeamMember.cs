using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Teams
{
	// Token: 0x02000019 RID: 25
	public class TeamMember
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000026D7 File Offset: 0x000008D7
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x000026DF File Offset: 0x000008DF
		[JsonProperty(PropertyName = "user_id")]
		public string UserId { get; protected set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000026E8 File Offset: 0x000008E8
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x000026F0 File Offset: 0x000008F0
		[JsonProperty(PropertyName = "user_name")]
		public string UserName { get; protected set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000026F9 File Offset: 0x000008F9
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00002701 File Offset: 0x00000901
		[JsonProperty(PropertyName = "user_login")]
		public string UserLogin { get; protected set; }
	}
}
