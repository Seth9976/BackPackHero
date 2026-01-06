using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.GetChatters
{
	// Token: 0x0200009C RID: 156
	public class Chatter
	{
		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x00004D98 File Offset: 0x00002F98
		// (set) Token: 0x06000535 RID: 1333 RVA: 0x00004DA0 File Offset: 0x00002FA0
		[JsonProperty("user_login")]
		public string UserLogin { get; protected set; }
	}
}
