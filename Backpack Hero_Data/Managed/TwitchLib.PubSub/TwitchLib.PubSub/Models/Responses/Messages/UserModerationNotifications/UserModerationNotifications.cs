using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TwitchLib.PubSub.Enums;
using TwitchLib.PubSub.Models.Responses.Messages.UserModerationNotificationsTypes;

namespace TwitchLib.PubSub.Models.Responses.Messages.UserModerationNotifications
{
	// Token: 0x0200001D RID: 29
	public class UserModerationNotifications : MessageData
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00006EB9 File Offset: 0x000050B9
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00006EC1 File Offset: 0x000050C1
		public UserModerationNotificationsType Type { get; private set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00006ECA File Offset: 0x000050CA
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00006ED2 File Offset: 0x000050D2
		public UserModerationNotificationsData Data { get; private set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00006EDB File Offset: 0x000050DB
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00006EE3 File Offset: 0x000050E3
		public string RawData { get; private set; }

		// Token: 0x06000153 RID: 339 RVA: 0x00006EEC File Offset: 0x000050EC
		public UserModerationNotifications(string jsonStr)
		{
			this.RawData = jsonStr;
			JToken jtoken = JObject.Parse(jsonStr);
			string text = jtoken.SelectToken("type").ToString();
			if (text != null && text == "automod_caught_message")
			{
				this.Type = UserModerationNotificationsType.AutomodCaughtMessage;
				this.Data = JsonConvert.DeserializeObject<AutomodCaughtMessage>(jtoken.SelectToken("data").ToString());
				return;
			}
			this.Type = UserModerationNotificationsType.Unknown;
		}
	}
}
