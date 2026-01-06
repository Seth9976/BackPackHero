using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.Emotes
{
	// Token: 0x0200009E RID: 158
	public class ChannelEmote : Emote
	{
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x00004DEC File Offset: 0x00002FEC
		// (set) Token: 0x0600053F RID: 1343 RVA: 0x00004DF4 File Offset: 0x00002FF4
		[JsonProperty("tier")]
		public string Tier { get; protected set; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x00004DFD File Offset: 0x00002FFD
		// (set) Token: 0x06000541 RID: 1345 RVA: 0x00004E05 File Offset: 0x00003005
		[JsonProperty("emote_type")]
		public string EmoteType { get; protected set; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x00004E0E File Offset: 0x0000300E
		// (set) Token: 0x06000543 RID: 1347 RVA: 0x00004E16 File Offset: 0x00003016
		[JsonProperty("emote_set_id")]
		public string EmoteSetId { get; protected set; }
	}
}
