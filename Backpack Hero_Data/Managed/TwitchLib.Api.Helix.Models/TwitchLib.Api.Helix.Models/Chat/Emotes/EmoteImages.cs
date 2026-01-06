using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.Emotes
{
	// Token: 0x020000A0 RID: 160
	public class EmoteImages
	{
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x00004E95 File Offset: 0x00003095
		// (set) Token: 0x06000553 RID: 1363 RVA: 0x00004E9D File Offset: 0x0000309D
		[JsonProperty("url_1x")]
		public string Url1X { get; protected set; }

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x00004EA6 File Offset: 0x000030A6
		// (set) Token: 0x06000555 RID: 1365 RVA: 0x00004EAE File Offset: 0x000030AE
		[JsonProperty("url_2x")]
		public string Url2X { get; protected set; }

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x00004EB7 File Offset: 0x000030B7
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x00004EBF File Offset: 0x000030BF
		[JsonProperty("url_4x")]
		public string Url4X { get; protected set; }
	}
}
