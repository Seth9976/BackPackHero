using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Streams.GetStreams
{
	// Token: 0x02000022 RID: 34
	public class Stream
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000028FB File Offset: 0x00000AFB
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00002903 File Offset: 0x00000B03
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000290C File Offset: 0x00000B0C
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00002914 File Offset: 0x00000B14
		[JsonProperty(PropertyName = "user_id")]
		public string UserId { get; protected set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000107 RID: 263 RVA: 0x0000291D File Offset: 0x00000B1D
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00002925 File Offset: 0x00000B25
		[JsonProperty(PropertyName = "user_login")]
		public string UserLogin { get; protected set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000109 RID: 265 RVA: 0x0000292E File Offset: 0x00000B2E
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00002936 File Offset: 0x00000B36
		[JsonProperty(PropertyName = "user_name")]
		public string UserName { get; protected set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000293F File Offset: 0x00000B3F
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00002947 File Offset: 0x00000B47
		[JsonProperty(PropertyName = "game_id")]
		public string GameId { get; protected set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00002950 File Offset: 0x00000B50
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00002958 File Offset: 0x00000B58
		[JsonProperty(PropertyName = "game_name")]
		public string GameName { get; protected set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00002961 File Offset: 0x00000B61
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00002969 File Offset: 0x00000B69
		[JsonProperty(PropertyName = "community_ids")]
		public string[] CommunityIds { get; protected set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00002972 File Offset: 0x00000B72
		// (set) Token: 0x06000112 RID: 274 RVA: 0x0000297A File Offset: 0x00000B7A
		[JsonProperty(PropertyName = "type")]
		public string Type { get; protected set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00002983 File Offset: 0x00000B83
		// (set) Token: 0x06000114 RID: 276 RVA: 0x0000298B File Offset: 0x00000B8B
		[JsonProperty(PropertyName = "title")]
		public string Title { get; protected set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00002994 File Offset: 0x00000B94
		// (set) Token: 0x06000116 RID: 278 RVA: 0x0000299C File Offset: 0x00000B9C
		[JsonProperty(PropertyName = "viewer_count")]
		public int ViewerCount { get; protected set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000117 RID: 279 RVA: 0x000029A5 File Offset: 0x00000BA5
		// (set) Token: 0x06000118 RID: 280 RVA: 0x000029AD File Offset: 0x00000BAD
		[JsonProperty(PropertyName = "started_at")]
		public DateTime StartedAt { get; protected set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000119 RID: 281 RVA: 0x000029B6 File Offset: 0x00000BB6
		// (set) Token: 0x0600011A RID: 282 RVA: 0x000029BE File Offset: 0x00000BBE
		[JsonProperty(PropertyName = "language")]
		public string Language { get; protected set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600011B RID: 283 RVA: 0x000029C7 File Offset: 0x00000BC7
		// (set) Token: 0x0600011C RID: 284 RVA: 0x000029CF File Offset: 0x00000BCF
		[JsonProperty(PropertyName = "thumbnail_url")]
		public string ThumbnailUrl { get; protected set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600011D RID: 285 RVA: 0x000029D8 File Offset: 0x00000BD8
		// (set) Token: 0x0600011E RID: 286 RVA: 0x000029E0 File Offset: 0x00000BE0
		[JsonProperty(PropertyName = "tag_ids")]
		public string[] TagIds { get; protected set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600011F RID: 287 RVA: 0x000029E9 File Offset: 0x00000BE9
		// (set) Token: 0x06000120 RID: 288 RVA: 0x000029F1 File Offset: 0x00000BF1
		[JsonProperty(PropertyName = "is_mature")]
		public bool IsMature { get; protected set; }
	}
}
