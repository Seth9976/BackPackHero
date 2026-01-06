using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Search
{
	// Token: 0x02000037 RID: 55
	public class Channel
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00002F04 File Offset: 0x00001104
		// (set) Token: 0x060001BB RID: 443 RVA: 0x00002F0C File Offset: 0x0000110C
		[JsonProperty(PropertyName = "game_id")]
		public string GameId { get; protected set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00002F15 File Offset: 0x00001115
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00002F1D File Offset: 0x0000111D
		[JsonProperty(PropertyName = "game_name")]
		public string GameName { get; protected set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00002F26 File Offset: 0x00001126
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00002F2E File Offset: 0x0000112E
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00002F37 File Offset: 0x00001137
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x00002F3F File Offset: 0x0000113F
		[JsonProperty(PropertyName = "broadcaster_login")]
		public string BroadcasterLogin { get; protected set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00002F48 File Offset: 0x00001148
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x00002F50 File Offset: 0x00001150
		[JsonProperty(PropertyName = "display_name")]
		public string DisplayName { get; protected set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00002F59 File Offset: 0x00001159
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x00002F61 File Offset: 0x00001161
		[JsonProperty(PropertyName = "broadcaster_language")]
		public string BroadcasterLanguage { get; protected set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00002F6A File Offset: 0x0000116A
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x00002F72 File Offset: 0x00001172
		[JsonProperty(PropertyName = "title")]
		public string Title { get; protected set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00002F7B File Offset: 0x0000117B
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x00002F83 File Offset: 0x00001183
		[JsonProperty(PropertyName = "thumbnail_url")]
		public string ThumbnailUrl { get; protected set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00002F8C File Offset: 0x0000118C
		// (set) Token: 0x060001CB RID: 459 RVA: 0x00002F94 File Offset: 0x00001194
		[JsonProperty(PropertyName = "is_live")]
		public bool IsLive { get; protected set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00002F9D File Offset: 0x0000119D
		// (set) Token: 0x060001CD RID: 461 RVA: 0x00002FA5 File Offset: 0x000011A5
		[JsonProperty(PropertyName = "started_at")]
		public DateTime? StartedAt { get; protected set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00002FAE File Offset: 0x000011AE
		// (set) Token: 0x060001CF RID: 463 RVA: 0x00002FB6 File Offset: 0x000011B6
		[JsonProperty(PropertyName = "tag_ids")]
		public List<string> TagIds { get; protected set; }
	}
}
