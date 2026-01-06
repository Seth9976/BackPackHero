using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.Emotes.GetChannelEmotes
{
	// Token: 0x020000A5 RID: 165
	public class GetChannelEmotesResponse
	{
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x00004F67 File Offset: 0x00003167
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x00004F6F File Offset: 0x0000316F
		[JsonProperty("data")]
		public ChannelEmote[] ChannelEmotes { get; protected set; }

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x00004F78 File Offset: 0x00003178
		// (set) Token: 0x0600056E RID: 1390 RVA: 0x00004F80 File Offset: 0x00003180
		[JsonProperty("template")]
		public string Template { get; protected set; }
	}
}
