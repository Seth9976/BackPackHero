using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.ChatSettings
{
	// Token: 0x020000A8 RID: 168
	public class GetChatSettingsResponse
	{
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x0000504B File Offset: 0x0000324B
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x00005053 File Offset: 0x00003253
		[JsonProperty(PropertyName = "data")]
		public ChatSettingsResponseModel[] Data { get; protected set; }
	}
}
