using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.Emotes.GetEmoteSets
{
	// Token: 0x020000A4 RID: 164
	public class GetEmoteSetsResponse
	{
		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x00004F3D File Offset: 0x0000313D
		// (set) Token: 0x06000567 RID: 1383 RVA: 0x00004F45 File Offset: 0x00003145
		[JsonProperty("data")]
		public EmoteSet[] EmoteSets { get; protected set; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x00004F4E File Offset: 0x0000314E
		// (set) Token: 0x06000569 RID: 1385 RVA: 0x00004F56 File Offset: 0x00003156
		[JsonProperty("template")]
		public string Template { get; protected set; }
	}
}
