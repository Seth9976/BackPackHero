using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Channels.GetChannelInformation
{
	// Token: 0x020000B5 RID: 181
	public class ChannelInformation
	{
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x0000539F File Offset: 0x0000359F
		// (set) Token: 0x060005EC RID: 1516 RVA: 0x000053A7 File Offset: 0x000035A7
		[JsonProperty(PropertyName = "broadcaster_id")]
		public string BroadcasterId { get; protected set; }

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x000053B0 File Offset: 0x000035B0
		// (set) Token: 0x060005EE RID: 1518 RVA: 0x000053B8 File Offset: 0x000035B8
		[JsonProperty(PropertyName = "broadcaster_login")]
		public string BroadcasterLogin { get; protected set; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x000053C1 File Offset: 0x000035C1
		// (set) Token: 0x060005F0 RID: 1520 RVA: 0x000053C9 File Offset: 0x000035C9
		[JsonProperty(PropertyName = "broadcaster_name")]
		public string BroadcasterName { get; protected set; }

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x000053D2 File Offset: 0x000035D2
		// (set) Token: 0x060005F2 RID: 1522 RVA: 0x000053DA File Offset: 0x000035DA
		[JsonProperty(PropertyName = "broadcaster_language")]
		public string BroadcasterLanguage { get; protected set; }

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x000053E3 File Offset: 0x000035E3
		// (set) Token: 0x060005F4 RID: 1524 RVA: 0x000053EB File Offset: 0x000035EB
		[JsonProperty(PropertyName = "game_id")]
		public string GameId { get; protected set; }

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x000053F4 File Offset: 0x000035F4
		// (set) Token: 0x060005F6 RID: 1526 RVA: 0x000053FC File Offset: 0x000035FC
		[JsonProperty(PropertyName = "game_name")]
		public string GameName { get; protected set; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x00005405 File Offset: 0x00003605
		// (set) Token: 0x060005F8 RID: 1528 RVA: 0x0000540D File Offset: 0x0000360D
		[JsonProperty(PropertyName = "title")]
		public string Title { get; protected set; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00005416 File Offset: 0x00003616
		// (set) Token: 0x060005FA RID: 1530 RVA: 0x0000541E File Offset: 0x0000361E
		[JsonProperty(PropertyName = "delay")]
		public int Delay { get; protected set; }
	}
}
