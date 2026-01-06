using System;
using Newtonsoft.Json.Linq;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
	// Token: 0x0200000A RID: 10
	public class ChannelBitsEvents : MessageData
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000099 RID: 153 RVA: 0x0000552C File Offset: 0x0000372C
		public string Username { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00005534 File Offset: 0x00003734
		public string ChannelName { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600009B RID: 155 RVA: 0x0000553C File Offset: 0x0000373C
		public string UserId { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00005544 File Offset: 0x00003744
		public string ChannelId { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000554C File Offset: 0x0000374C
		public string Time { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00005554 File Offset: 0x00003754
		public string ChatMessage { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600009F RID: 159 RVA: 0x0000555C File Offset: 0x0000375C
		public int BitsUsed { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00005564 File Offset: 0x00003764
		public int TotalBitsUsed { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x0000556C File Offset: 0x0000376C
		public string Context { get; }

		// Token: 0x060000A2 RID: 162 RVA: 0x00005574 File Offset: 0x00003774
		public ChannelBitsEvents(string jsonStr)
		{
			JObject jobject = JObject.Parse(jsonStr);
			JToken jtoken = jobject.SelectToken("data").SelectToken("user_name");
			this.Username = ((jtoken != null) ? jtoken.ToString() : null);
			JToken jtoken2 = jobject.SelectToken("data").SelectToken("channel_name");
			this.ChannelName = ((jtoken2 != null) ? jtoken2.ToString() : null);
			JToken jtoken3 = jobject.SelectToken("data").SelectToken("user_id");
			this.UserId = ((jtoken3 != null) ? jtoken3.ToString() : null);
			JToken jtoken4 = jobject.SelectToken("data").SelectToken("channel_id");
			this.ChannelId = ((jtoken4 != null) ? jtoken4.ToString() : null);
			JToken jtoken5 = jobject.SelectToken("data").SelectToken("time");
			this.Time = ((jtoken5 != null) ? jtoken5.ToString() : null);
			JToken jtoken6 = jobject.SelectToken("data").SelectToken("chat_message");
			this.ChatMessage = ((jtoken6 != null) ? jtoken6.ToString() : null);
			this.BitsUsed = int.Parse(jobject.SelectToken("data").SelectToken("bits_used").ToString());
			this.TotalBitsUsed = int.Parse(jobject.SelectToken("data").SelectToken("total_bits_used").ToString());
			JToken jtoken7 = jobject.SelectToken("data").SelectToken("context");
			this.Context = ((jtoken7 != null) ? jtoken7.ToString() : null);
		}
	}
}
