using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.GetUserChatColor
{
	// Token: 0x0200009A RID: 154
	public class GetUserChatColorResponse
	{
		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x00004D33 File Offset: 0x00002F33
		// (set) Token: 0x06000529 RID: 1321 RVA: 0x00004D3B File Offset: 0x00002F3B
		[JsonProperty(PropertyName = "data")]
		public UserChatColorResponseModel[] Data { get; protected set; }
	}
}
