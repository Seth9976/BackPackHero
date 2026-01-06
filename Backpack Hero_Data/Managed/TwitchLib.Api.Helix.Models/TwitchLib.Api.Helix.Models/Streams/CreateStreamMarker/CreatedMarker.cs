using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Streams.CreateStreamMarker
{
	// Token: 0x0200002B RID: 43
	public class CreatedMarker
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00002C40 File Offset: 0x00000E40
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00002C48 File Offset: 0x00000E48
		[JsonProperty(PropertyName = "id")]
		public int Id { get; protected set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00002C51 File Offset: 0x00000E51
		// (set) Token: 0x06000169 RID: 361 RVA: 0x00002C59 File Offset: 0x00000E59
		[JsonProperty(PropertyName = "created_at")]
		public DateTime CreatedAt { get; protected set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00002C62 File Offset: 0x00000E62
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00002C6A File Offset: 0x00000E6A
		[JsonProperty(PropertyName = "description")]
		public string Description { get; protected set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00002C73 File Offset: 0x00000E73
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00002C7B File Offset: 0x00000E7B
		[JsonProperty(PropertyName = "position_seconds")]
		public int PositionSeconds { get; protected set; }
	}
}
