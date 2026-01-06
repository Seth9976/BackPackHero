using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.GetUserChatColor
{
	// Token: 0x0200009B RID: 155
	public class UserChatColorResponseModel
	{
		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x00004D4C File Offset: 0x00002F4C
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x00004D54 File Offset: 0x00002F54
		[JsonProperty(PropertyName = "user_id")]
		public string UserId { get; protected set; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x00004D5D File Offset: 0x00002F5D
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x00004D65 File Offset: 0x00002F65
		[JsonProperty(PropertyName = "user_login")]
		public string UserLogin { get; protected set; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x00004D6E File Offset: 0x00002F6E
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x00004D76 File Offset: 0x00002F76
		[JsonProperty(PropertyName = "user_name")]
		public string UserName { get; protected set; }

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x00004D7F File Offset: 0x00002F7F
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x00004D87 File Offset: 0x00002F87
		[JsonProperty(PropertyName = "color")]
		public string Color { get; protected set; }
	}
}
