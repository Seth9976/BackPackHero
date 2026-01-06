using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Videos.GetVideos
{
	// Token: 0x02000004 RID: 4
	public class Video
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020A4 File Offset: 0x000002A4
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000020AC File Offset: 0x000002AC
		[JsonProperty(PropertyName = "created_at")]
		public string CreatedAt { get; protected set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000020B5 File Offset: 0x000002B5
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000020BD File Offset: 0x000002BD
		[JsonProperty(PropertyName = "description")]
		public string Description { get; protected set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000020C6 File Offset: 0x000002C6
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000020CE File Offset: 0x000002CE
		[JsonProperty(PropertyName = "duration")]
		public string Duration { get; protected set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000020D7 File Offset: 0x000002D7
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000020DF File Offset: 0x000002DF
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000020E8 File Offset: 0x000002E8
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000020F0 File Offset: 0x000002F0
		[JsonProperty(PropertyName = "language")]
		public string Language { get; protected set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000020F9 File Offset: 0x000002F9
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002101 File Offset: 0x00000301
		[JsonProperty(PropertyName = "published_at")]
		public string PublishedAt { get; protected set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000210A File Offset: 0x0000030A
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002112 File Offset: 0x00000312
		[JsonProperty(PropertyName = "thumbnail_url")]
		public string ThumbnailUrl { get; protected set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000211B File Offset: 0x0000031B
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002123 File Offset: 0x00000323
		[JsonProperty(PropertyName = "title")]
		public string Title { get; protected set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000212C File Offset: 0x0000032C
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002134 File Offset: 0x00000334
		[JsonProperty(PropertyName = "type")]
		public string Type { get; protected set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000213D File Offset: 0x0000033D
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002145 File Offset: 0x00000345
		[JsonProperty(PropertyName = "url")]
		public string Url { get; protected set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000214E File Offset: 0x0000034E
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002156 File Offset: 0x00000356
		[JsonProperty(PropertyName = "user_id")]
		public string UserId { get; protected set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000215F File Offset: 0x0000035F
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002167 File Offset: 0x00000367
		[JsonProperty(PropertyName = "user_login")]
		public string UserLogin { get; protected set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002170 File Offset: 0x00000370
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002178 File Offset: 0x00000378
		[JsonProperty(PropertyName = "user_name")]
		public string UserName { get; protected set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002181 File Offset: 0x00000381
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002189 File Offset: 0x00000389
		[JsonProperty(PropertyName = "viewable")]
		public string Viewable { get; protected set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002192 File Offset: 0x00000392
		// (set) Token: 0x06000028 RID: 40 RVA: 0x0000219A File Offset: 0x0000039A
		[JsonProperty(PropertyName = "view_count")]
		public int ViewCount { get; protected set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000021A3 File Offset: 0x000003A3
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000021AB File Offset: 0x000003AB
		[JsonProperty(PropertyName = "stream_id")]
		public string StreamId { get; protected set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000021B4 File Offset: 0x000003B4
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000021BC File Offset: 0x000003BC
		[JsonProperty(PropertyName = "muted_segments")]
		public MutedSegment[] MutedSegments { get; protected set; }
	}
}
