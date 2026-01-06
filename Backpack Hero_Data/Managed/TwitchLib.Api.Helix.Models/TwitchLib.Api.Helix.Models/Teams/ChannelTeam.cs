using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Teams
{
	// Token: 0x02000014 RID: 20
	public class ChannelTeam : TeamBase
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000025B0 File Offset: 0x000007B0
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x000025B8 File Offset: 0x000007B8
		[JsonProperty(PropertyName = "broadcaster_id")]
		public string BroadcasterId { get; protected set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000025C1 File Offset: 0x000007C1
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x000025C9 File Offset: 0x000007C9
		[JsonProperty(PropertyName = "broadcaster_name")]
		public string BroadcasterName { get; protected set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000025D2 File Offset: 0x000007D2
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x000025DA File Offset: 0x000007DA
		[JsonProperty(PropertyName = "broadcaster_login")]
		public string BroadcasterLogin { get; protected set; }
	}
}
