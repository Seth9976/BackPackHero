using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Streams.GetFollowedStreams
{
	// Token: 0x0200002A RID: 42
	public class Stream
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00002B5B File Offset: 0x00000D5B
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00002B63 File Offset: 0x00000D63
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00002B6C File Offset: 0x00000D6C
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00002B74 File Offset: 0x00000D74
		[JsonProperty(PropertyName = "user_id")]
		public string UserId { get; protected set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00002B7D File Offset: 0x00000D7D
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00002B85 File Offset: 0x00000D85
		[JsonProperty(PropertyName = "user_login")]
		public string UserLogin { get; protected set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00002B8E File Offset: 0x00000D8E
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00002B96 File Offset: 0x00000D96
		[JsonProperty(PropertyName = "user_name")]
		public string UserName { get; protected set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00002B9F File Offset: 0x00000D9F
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00002BA7 File Offset: 0x00000DA7
		[JsonProperty(PropertyName = "game_id")]
		public string GameId { get; protected set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00002BB0 File Offset: 0x00000DB0
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00002BB8 File Offset: 0x00000DB8
		[JsonProperty(PropertyName = "game_name")]
		public string GameName { get; protected set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00002BC1 File Offset: 0x00000DC1
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00002BC9 File Offset: 0x00000DC9
		[JsonProperty(PropertyName = "type")]
		public string Type { get; protected set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00002BD2 File Offset: 0x00000DD2
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00002BDA File Offset: 0x00000DDA
		[JsonProperty(PropertyName = "title")]
		public string Title { get; protected set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00002BE3 File Offset: 0x00000DE3
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00002BEB File Offset: 0x00000DEB
		[JsonProperty(PropertyName = "viewer_count")]
		public int ViewerCount { get; protected set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00002BF4 File Offset: 0x00000DF4
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00002BFC File Offset: 0x00000DFC
		[JsonProperty(PropertyName = "started_at")]
		public DateTime StartedAt { get; protected set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00002C05 File Offset: 0x00000E05
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00002C0D File Offset: 0x00000E0D
		[JsonProperty(PropertyName = "language")]
		public string Language { get; protected set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00002C16 File Offset: 0x00000E16
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00002C1E File Offset: 0x00000E1E
		[JsonProperty(PropertyName = "thumbnail_url")]
		public string ThumbnailUrl { get; protected set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00002C27 File Offset: 0x00000E27
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00002C2F File Offset: 0x00000E2F
		[JsonProperty(PropertyName = "tag_ids")]
		public string[] TagIds { get; protected set; }
	}
}
