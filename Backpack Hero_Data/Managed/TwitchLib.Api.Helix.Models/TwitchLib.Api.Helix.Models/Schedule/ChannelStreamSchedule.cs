using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Schedule
{
	// Token: 0x0200003B RID: 59
	public class ChannelStreamSchedule
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00003045 File Offset: 0x00001245
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x0000304D File Offset: 0x0000124D
		[JsonProperty("segments")]
		public Segment[] Segments { get; protected set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00003056 File Offset: 0x00001256
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x0000305E File Offset: 0x0000125E
		[JsonProperty("broadcaster_id")]
		public string BroadcasterId { get; protected set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00003067 File Offset: 0x00001267
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x0000306F File Offset: 0x0000126F
		[JsonProperty("broadcaster_name")]
		public string BroadcasterName { get; protected set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00003078 File Offset: 0x00001278
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x00003080 File Offset: 0x00001280
		[JsonProperty("broadcaster_login")]
		public string BroadcasterLogin { get; protected set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00003089 File Offset: 0x00001289
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x00003091 File Offset: 0x00001291
		[JsonProperty("vacation")]
		public Vacation Vacation { get; protected set; }
	}
}
