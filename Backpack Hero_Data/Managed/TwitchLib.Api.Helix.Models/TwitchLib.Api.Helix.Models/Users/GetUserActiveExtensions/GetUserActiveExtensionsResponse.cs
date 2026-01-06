using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Users.Internal;

namespace TwitchLib.Api.Helix.Models.Users.GetUserActiveExtensions
{
	// Token: 0x02000013 RID: 19
	public class GetUserActiveExtensionsResponse
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00002597 File Offset: 0x00000797
		// (set) Token: 0x0600009D RID: 157 RVA: 0x0000259F File Offset: 0x0000079F
		[JsonProperty(PropertyName = "data")]
		public ActiveExtensions Data { get; protected set; }
	}
}
