using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Users.GetUserBlockList
{
	// Token: 0x02000011 RID: 17
	public class BlockedUser
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00002543 File Offset: 0x00000743
		// (set) Token: 0x06000093 RID: 147 RVA: 0x0000254B File Offset: 0x0000074B
		[JsonProperty(PropertyName = "user_id")]
		public string Id { get; protected set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00002554 File Offset: 0x00000754
		// (set) Token: 0x06000095 RID: 149 RVA: 0x0000255C File Offset: 0x0000075C
		[JsonProperty(PropertyName = "user_login")]
		public string UserLogin { get; protected set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00002565 File Offset: 0x00000765
		// (set) Token: 0x06000097 RID: 151 RVA: 0x0000256D File Offset: 0x0000076D
		[JsonProperty(PropertyName = "display_name")]
		public string DisplayName { get; protected set; }
	}
}
