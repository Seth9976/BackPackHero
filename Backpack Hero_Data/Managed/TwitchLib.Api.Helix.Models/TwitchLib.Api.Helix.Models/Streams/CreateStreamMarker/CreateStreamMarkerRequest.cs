using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Streams.CreateStreamMarker
{
	// Token: 0x0200002C RID: 44
	[JsonObject(ItemNullValueHandling = 1)]
	public class CreateStreamMarkerRequest
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00002C8C File Offset: 0x00000E8C
		// (set) Token: 0x06000170 RID: 368 RVA: 0x00002C94 File Offset: 0x00000E94
		[JsonProperty(PropertyName = "user_id")]
		public string UserId { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00002C9D File Offset: 0x00000E9D
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00002CA5 File Offset: 0x00000EA5
		[JsonProperty(PropertyName = "description")]
		public string Description { get; set; }
	}
}
