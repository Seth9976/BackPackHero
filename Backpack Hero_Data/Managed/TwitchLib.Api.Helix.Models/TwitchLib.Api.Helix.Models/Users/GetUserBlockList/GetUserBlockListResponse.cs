using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Users.GetUserBlockList
{
	// Token: 0x02000012 RID: 18
	public class GetUserBlockListResponse
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000099 RID: 153 RVA: 0x0000257E File Offset: 0x0000077E
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00002586 File Offset: 0x00000786
		[JsonProperty(PropertyName = "data")]
		public BlockedUser[] Data { get; protected set; }
	}
}
