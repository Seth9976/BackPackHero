using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Teams
{
	// Token: 0x02000015 RID: 21
	public class GetChannelTeamsResponse
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000025EB File Offset: 0x000007EB
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x000025F3 File Offset: 0x000007F3
		[JsonProperty(PropertyName = "data")]
		public ChannelTeam[] ChannelTeams { get; protected set; }
	}
}
