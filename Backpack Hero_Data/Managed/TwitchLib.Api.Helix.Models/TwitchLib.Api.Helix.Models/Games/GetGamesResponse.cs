using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Games
{
	// Token: 0x02000076 RID: 118
	public class GetGamesResponse
	{
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000416F File Offset: 0x0000236F
		// (set) Token: 0x060003CE RID: 974 RVA: 0x00004177 File Offset: 0x00002377
		[JsonProperty(PropertyName = "data")]
		public Game[] Games { get; protected set; }
	}
}
