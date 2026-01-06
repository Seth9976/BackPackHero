using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.ChatSettings
{
	// Token: 0x020000A7 RID: 167
	public class ChatSettingsResponseModel
	{
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00004F99 File Offset: 0x00003199
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x00004FA1 File Offset: 0x000031A1
		[JsonProperty(PropertyName = "broadcaster_id")]
		public string BroadcasterId { get; protected set; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00004FAA File Offset: 0x000031AA
		// (set) Token: 0x06000574 RID: 1396 RVA: 0x00004FB2 File Offset: 0x000031B2
		[JsonProperty(PropertyName = "slow_mode")]
		public bool SlowMode { get; protected set; }

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00004FBB File Offset: 0x000031BB
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x00004FC3 File Offset: 0x000031C3
		[JsonProperty(PropertyName = "slow_mode_wait_time")]
		public int? SlowModeWaitDuration { get; protected set; }

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00004FCC File Offset: 0x000031CC
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x00004FD4 File Offset: 0x000031D4
		[JsonProperty(PropertyName = "follower_mode")]
		public bool FollowerMode { get; protected set; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x00004FDD File Offset: 0x000031DD
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x00004FE5 File Offset: 0x000031E5
		[JsonProperty(PropertyName = "follower_mode_duration")]
		public int? FollowerModeDuration { get; protected set; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x00004FEE File Offset: 0x000031EE
		// (set) Token: 0x0600057C RID: 1404 RVA: 0x00004FF6 File Offset: 0x000031F6
		[JsonProperty(PropertyName = "subscriber_mode")]
		public bool SubscriberMode { get; protected set; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x00004FFF File Offset: 0x000031FF
		// (set) Token: 0x0600057E RID: 1406 RVA: 0x00005007 File Offset: 0x00003207
		[JsonProperty(PropertyName = "emote_mode")]
		public bool EmoteMode { get; protected set; }

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x00005010 File Offset: 0x00003210
		// (set) Token: 0x06000580 RID: 1408 RVA: 0x00005018 File Offset: 0x00003218
		[JsonProperty(PropertyName = "unique_chat_mode")]
		public bool UniqueChatMode { get; protected set; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x00005021 File Offset: 0x00003221
		// (set) Token: 0x06000582 RID: 1410 RVA: 0x00005029 File Offset: 0x00003229
		[JsonProperty(PropertyName = "non_moderator_chat_delay")]
		public bool NonModeratorChatDelay { get; protected set; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x00005032 File Offset: 0x00003232
		// (set) Token: 0x06000584 RID: 1412 RVA: 0x0000503A File Offset: 0x0000323A
		[JsonProperty(PropertyName = "non_moderator_chat_delay_duration")]
		public int? NonModeratorChatDelayDuration { get; protected set; }
	}
}
