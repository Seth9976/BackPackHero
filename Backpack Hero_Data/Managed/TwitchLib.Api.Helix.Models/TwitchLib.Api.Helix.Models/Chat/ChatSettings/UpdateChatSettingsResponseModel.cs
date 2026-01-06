using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.ChatSettings
{
	// Token: 0x020000AA RID: 170
	public class UpdateChatSettingsResponseModel
	{
		// Token: 0x1700027C RID: 636
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x0000507D File Offset: 0x0000327D
		// (set) Token: 0x0600058D RID: 1421 RVA: 0x00005085 File Offset: 0x00003285
		[JsonProperty(PropertyName = "broadcaster_id")]
		public string BroadcasterId { get; protected set; }

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x0000508E File Offset: 0x0000328E
		// (set) Token: 0x0600058F RID: 1423 RVA: 0x00005096 File Offset: 0x00003296
		[JsonProperty(PropertyName = "moderator_id")]
		public string ModeratorId { get; protected set; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x0000509F File Offset: 0x0000329F
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x000050A7 File Offset: 0x000032A7
		[JsonProperty(PropertyName = "slow_mode")]
		public bool SlowMode { get; protected set; }

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x000050B0 File Offset: 0x000032B0
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x000050B8 File Offset: 0x000032B8
		[JsonProperty(PropertyName = "slow_mode_wait_time")]
		public int? SlowModeWaitDuration { get; protected set; }

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x000050C1 File Offset: 0x000032C1
		// (set) Token: 0x06000595 RID: 1429 RVA: 0x000050C9 File Offset: 0x000032C9
		[JsonProperty(PropertyName = "follower_mode")]
		public bool FollowerMode { get; protected set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x000050D2 File Offset: 0x000032D2
		// (set) Token: 0x06000597 RID: 1431 RVA: 0x000050DA File Offset: 0x000032DA
		[JsonProperty(PropertyName = "follower_mode_duration")]
		public int? FollowerModeDuration { get; protected set; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x000050E3 File Offset: 0x000032E3
		// (set) Token: 0x06000599 RID: 1433 RVA: 0x000050EB File Offset: 0x000032EB
		[JsonProperty(PropertyName = "subscriber_mode")]
		public bool SubscriberMode { get; protected set; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x000050F4 File Offset: 0x000032F4
		// (set) Token: 0x0600059B RID: 1435 RVA: 0x000050FC File Offset: 0x000032FC
		[JsonProperty(PropertyName = "emote_mode")]
		public bool EmoteMode { get; protected set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x00005105 File Offset: 0x00003305
		// (set) Token: 0x0600059D RID: 1437 RVA: 0x0000510D File Offset: 0x0000330D
		[JsonProperty(PropertyName = "unique_chat_mode")]
		public bool UniqueChatMode { get; protected set; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x00005116 File Offset: 0x00003316
		// (set) Token: 0x0600059F RID: 1439 RVA: 0x0000511E File Offset: 0x0000331E
		[JsonProperty(PropertyName = "non_moderator_chat_delay")]
		public bool NonModeratorChatDelay { get; protected set; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x00005127 File Offset: 0x00003327
		// (set) Token: 0x060005A1 RID: 1441 RVA: 0x0000512F File Offset: 0x0000332F
		[JsonProperty(PropertyName = "non_moderator_chat_delay_duration")]
		public int? NonModeratorChatDelayDuration { get; protected set; }
	}
}
