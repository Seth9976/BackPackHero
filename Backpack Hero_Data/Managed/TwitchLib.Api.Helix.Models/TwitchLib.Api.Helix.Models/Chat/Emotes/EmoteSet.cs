using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.Emotes
{
	// Token: 0x020000A1 RID: 161
	public class EmoteSet : Emote
	{
		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x00004ED0 File Offset: 0x000030D0
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x00004ED8 File Offset: 0x000030D8
		[JsonProperty("emote_type")]
		public string EmoteType { get; protected set; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00004EE1 File Offset: 0x000030E1
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x00004EE9 File Offset: 0x000030E9
		[JsonProperty("emote_set_id")]
		public string EmoteSetId { get; protected set; }

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x00004EF2 File Offset: 0x000030F2
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x00004EFA File Offset: 0x000030FA
		[JsonProperty("owner_id")]
		public string OwnerId { get; protected set; }
	}
}
