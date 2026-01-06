using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.ChatSettings
{
	// Token: 0x020000A9 RID: 169
	public class UpdateChatSettingsResponse
	{
		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x00005064 File Offset: 0x00003264
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x0000506C File Offset: 0x0000326C
		[JsonProperty(PropertyName = "data")]
		public UpdateChatSettingsResponseModel[] Data { get; protected set; }
	}
}
