using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TwitchLib.PubSub.Enums;
using TwitchLib.PubSub.Models.Responses.Messages.AutomodCaughtMessage;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
	// Token: 0x02000008 RID: 8
	public class AutomodQueue : MessageData
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00005483 File Offset: 0x00003683
		// (set) Token: 0x06000092 RID: 146 RVA: 0x0000548B File Offset: 0x0000368B
		public AutomodQueueType Type { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00005494 File Offset: 0x00003694
		// (set) Token: 0x06000094 RID: 148 RVA: 0x0000549C File Offset: 0x0000369C
		public AutomodQueueData Data { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000054A5 File Offset: 0x000036A5
		// (set) Token: 0x06000096 RID: 150 RVA: 0x000054AD File Offset: 0x000036AD
		public string RawData { get; private set; }

		// Token: 0x06000097 RID: 151 RVA: 0x000054B8 File Offset: 0x000036B8
		public AutomodQueue(string jsonStr)
		{
			this.RawData = jsonStr;
			JToken jtoken = JObject.Parse(jsonStr);
			string text = jtoken.SelectToken("type").ToString();
			if (text != null && text == "automod_caught_message")
			{
				this.Type = AutomodQueueType.CaughtMessage;
				this.Data = JsonConvert.DeserializeObject<AutomodCaughtMessage>(jtoken.SelectToken("data").ToString());
				return;
			}
			this.Type = AutomodQueueType.Unknown;
		}
	}
}
