using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.Emotes.GetGlobalEmotes
{
	// Token: 0x020000A3 RID: 163
	public class GetGlobalEmotesResponse
	{
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00004F13 File Offset: 0x00003113
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x00004F1B File Offset: 0x0000311B
		[JsonProperty("data")]
		public GlobalEmote[] GlobalEmotes { get; protected set; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00004F24 File Offset: 0x00003124
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x00004F2C File Offset: 0x0000312C
		[JsonProperty("template")]
		public string Template { get; protected set; }
	}
}
