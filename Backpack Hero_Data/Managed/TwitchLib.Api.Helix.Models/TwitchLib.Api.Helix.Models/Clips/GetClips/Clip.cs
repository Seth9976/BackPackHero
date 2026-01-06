using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Clips.GetClips
{
	// Token: 0x02000094 RID: 148
	public class Clip
	{
		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x00004A8A File Offset: 0x00002C8A
		// (set) Token: 0x060004E2 RID: 1250 RVA: 0x00004A92 File Offset: 0x00002C92
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x00004A9B File Offset: 0x00002C9B
		// (set) Token: 0x060004E4 RID: 1252 RVA: 0x00004AA3 File Offset: 0x00002CA3
		[JsonProperty(PropertyName = "url")]
		public string Url { get; protected set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00004AAC File Offset: 0x00002CAC
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x00004AB4 File Offset: 0x00002CB4
		[JsonProperty(PropertyName = "embed_url")]
		public string EmbedUrl { get; protected set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x00004ABD File Offset: 0x00002CBD
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x00004AC5 File Offset: 0x00002CC5
		[JsonProperty(PropertyName = "broadcaster_id")]
		public string BroadcasterId { get; protected set; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00004ACE File Offset: 0x00002CCE
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x00004AD6 File Offset: 0x00002CD6
		[JsonProperty(PropertyName = "broadcaster_name")]
		public string BroadcasterName { get; protected set; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x00004ADF File Offset: 0x00002CDF
		// (set) Token: 0x060004EC RID: 1260 RVA: 0x00004AE7 File Offset: 0x00002CE7
		[JsonProperty(PropertyName = "creator_id")]
		public string CreatorId { get; protected set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x00004AF0 File Offset: 0x00002CF0
		// (set) Token: 0x060004EE RID: 1262 RVA: 0x00004AF8 File Offset: 0x00002CF8
		[JsonProperty(PropertyName = "creator_name")]
		public string CreatorName { get; protected set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x00004B01 File Offset: 0x00002D01
		// (set) Token: 0x060004F0 RID: 1264 RVA: 0x00004B09 File Offset: 0x00002D09
		[JsonProperty(PropertyName = "video_id")]
		public string VideoId { get; protected set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00004B12 File Offset: 0x00002D12
		// (set) Token: 0x060004F2 RID: 1266 RVA: 0x00004B1A File Offset: 0x00002D1A
		[JsonProperty(PropertyName = "game_id")]
		public string GameId { get; protected set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x00004B23 File Offset: 0x00002D23
		// (set) Token: 0x060004F4 RID: 1268 RVA: 0x00004B2B File Offset: 0x00002D2B
		[JsonProperty(PropertyName = "language")]
		public string Language { get; protected set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00004B34 File Offset: 0x00002D34
		// (set) Token: 0x060004F6 RID: 1270 RVA: 0x00004B3C File Offset: 0x00002D3C
		[JsonProperty(PropertyName = "title")]
		public string Title { get; protected set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x00004B45 File Offset: 0x00002D45
		// (set) Token: 0x060004F8 RID: 1272 RVA: 0x00004B4D File Offset: 0x00002D4D
		[JsonProperty(PropertyName = "view_count")]
		public int ViewCount { get; protected set; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x00004B56 File Offset: 0x00002D56
		// (set) Token: 0x060004FA RID: 1274 RVA: 0x00004B5E File Offset: 0x00002D5E
		[JsonProperty(PropertyName = "created_at")]
		public string CreatedAt { get; protected set; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x00004B67 File Offset: 0x00002D67
		// (set) Token: 0x060004FC RID: 1276 RVA: 0x00004B6F File Offset: 0x00002D6F
		[JsonProperty(PropertyName = "thumbnail_url")]
		public string ThumbnailUrl { get; protected set; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x00004B78 File Offset: 0x00002D78
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x00004B80 File Offset: 0x00002D80
		[JsonProperty(PropertyName = "duration")]
		public float Duration { get; protected set; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x00004B89 File Offset: 0x00002D89
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x00004B91 File Offset: 0x00002D91
		[JsonProperty(PropertyName = "vod_offset")]
		public int VodOffset { get; protected set; }
	}
}
