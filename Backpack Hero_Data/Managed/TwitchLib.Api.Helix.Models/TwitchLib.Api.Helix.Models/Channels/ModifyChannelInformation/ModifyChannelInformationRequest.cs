using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Channels.ModifyChannelInformation
{
	// Token: 0x020000B2 RID: 178
	[JsonObject(ItemNullValueHandling = 1)]
	public class ModifyChannelInformationRequest
	{
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x000052EE File Offset: 0x000034EE
		// (set) Token: 0x060005D7 RID: 1495 RVA: 0x000052F6 File Offset: 0x000034F6
		[JsonProperty(PropertyName = "game_id", NullValueHandling = 1)]
		public string GameId { get; set; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x000052FF File Offset: 0x000034FF
		// (set) Token: 0x060005D9 RID: 1497 RVA: 0x00005307 File Offset: 0x00003507
		[JsonProperty(PropertyName = "title", NullValueHandling = 1)]
		public string Title { get; set; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00005310 File Offset: 0x00003510
		// (set) Token: 0x060005DB RID: 1499 RVA: 0x00005318 File Offset: 0x00003518
		[JsonProperty(PropertyName = "broadcaster_language", NullValueHandling = 1)]
		public string BroadcasterLanguage { get; set; }

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x00005321 File Offset: 0x00003521
		// (set) Token: 0x060005DD RID: 1501 RVA: 0x00005329 File Offset: 0x00003529
		[JsonProperty(PropertyName = "delay", NullValueHandling = 1)]
		public int? Delay { get; set; }
	}
}
