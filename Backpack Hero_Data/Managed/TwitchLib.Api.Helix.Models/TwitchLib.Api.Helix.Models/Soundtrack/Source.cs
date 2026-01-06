using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Soundtrack
{
	// Token: 0x02000030 RID: 48
	public class Source
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00002D45 File Offset: 0x00000F45
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00002D4D File Offset: 0x00000F4D
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00002D56 File Offset: 0x00000F56
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00002D5E File Offset: 0x00000F5E
		[JsonProperty(PropertyName = "content_type")]
		public string ContentType { get; protected set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00002D67 File Offset: 0x00000F67
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00002D6F File Offset: 0x00000F6F
		[JsonProperty(PropertyName = "title")]
		public string Title { get; protected set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00002D78 File Offset: 0x00000F78
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00002D80 File Offset: 0x00000F80
		[JsonProperty(PropertyName = "image_url")]
		public string ImageUrl { get; protected set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00002D89 File Offset: 0x00000F89
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00002D91 File Offset: 0x00000F91
		[JsonProperty(PropertyName = "soundtrack_url")]
		public string SoundtrackUrl { get; protected set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00002D9A File Offset: 0x00000F9A
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00002DA2 File Offset: 0x00000FA2
		[JsonProperty(PropertyName = "spotify_url")]
		public string SpotifyUrl { get; protected set; }
	}
}
