using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Soundtrack
{
	// Token: 0x02000031 RID: 49
	public class Track
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00002DB3 File Offset: 0x00000FB3
		// (set) Token: 0x06000193 RID: 403 RVA: 0x00002DBB File Offset: 0x00000FBB
		[JsonProperty(PropertyName = "artists")]
		public Artist[] Artists { get; protected set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00002DC4 File Offset: 0x00000FC4
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00002DCC File Offset: 0x00000FCC
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00002DD5 File Offset: 0x00000FD5
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00002DDD File Offset: 0x00000FDD
		[JsonProperty(PropertyName = "duration")]
		public int Duration { get; protected set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00002DE6 File Offset: 0x00000FE6
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00002DEE File Offset: 0x00000FEE
		[JsonProperty(PropertyName = "title")]
		public string Title { get; protected set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00002DF7 File Offset: 0x00000FF7
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00002DFF File Offset: 0x00000FFF
		[JsonProperty(PropertyName = "album")]
		public Album Album { get; protected set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00002E08 File Offset: 0x00001008
		// (set) Token: 0x0600019D RID: 413 RVA: 0x00002E10 File Offset: 0x00001010
		[JsonProperty(PropertyName = "isrc")]
		public string ISRC { get; protected set; }
	}
}
