using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Extensions.LiveChannels
{
	// Token: 0x02000085 RID: 133
	public class LiveChannel
	{
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x000046D1 File Offset: 0x000028D1
		// (set) Token: 0x06000471 RID: 1137 RVA: 0x000046D9 File Offset: 0x000028D9
		[JsonProperty(PropertyName = "broadcaster_id")]
		public string BroadcasterId { get; protected set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x000046E2 File Offset: 0x000028E2
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x000046EA File Offset: 0x000028EA
		[JsonProperty(PropertyName = "broadcaster_name")]
		public string BroadcasterName { get; protected set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x000046F3 File Offset: 0x000028F3
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x000046FB File Offset: 0x000028FB
		[JsonProperty(PropertyName = "game_name")]
		public string GameName { get; protected set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x00004704 File Offset: 0x00002904
		// (set) Token: 0x06000477 RID: 1143 RVA: 0x0000470C File Offset: 0x0000290C
		[JsonProperty(PropertyName = "game_id")]
		public string GameId { get; protected set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x00004715 File Offset: 0x00002915
		// (set) Token: 0x06000479 RID: 1145 RVA: 0x0000471D File Offset: 0x0000291D
		[JsonProperty(PropertyName = "title")]
		public string Title { get; protected set; }
	}
}
