using System;
using Newtonsoft.Json;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
	// Token: 0x0200000B RID: 11
	public class ChannelBitsEventsV2 : MessageData
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000056E9 File Offset: 0x000038E9
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x000056F1 File Offset: 0x000038F1
		[JsonProperty(PropertyName = "user_name")]
		public string UserName { get; protected set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000056FA File Offset: 0x000038FA
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00005702 File Offset: 0x00003902
		[JsonProperty(PropertyName = "channel_name")]
		public string ChannelName { get; protected set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000570B File Offset: 0x0000390B
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00005713 File Offset: 0x00003913
		[JsonProperty(PropertyName = "user_id")]
		public string UserId { get; protected set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x0000571C File Offset: 0x0000391C
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00005724 File Offset: 0x00003924
		[JsonProperty(PropertyName = "channel_id")]
		public string ChannelId { get; protected set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000572D File Offset: 0x0000392D
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00005735 File Offset: 0x00003935
		[JsonProperty(PropertyName = "time")]
		public DateTime Time { get; protected set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000573E File Offset: 0x0000393E
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00005746 File Offset: 0x00003946
		[JsonProperty(PropertyName = "chat_message")]
		public string ChatMessage { get; protected set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000574F File Offset: 0x0000394F
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00005757 File Offset: 0x00003957
		[JsonProperty(PropertyName = "bits_used")]
		public int BitsUsed { get; protected set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00005760 File Offset: 0x00003960
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00005768 File Offset: 0x00003968
		[JsonProperty(PropertyName = "total_bits_used")]
		public int TotalBitsUsed { get; protected set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00005771 File Offset: 0x00003971
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00005779 File Offset: 0x00003979
		[JsonProperty(PropertyName = "is_anonymous")]
		public bool IsAnonymous { get; protected set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00005782 File Offset: 0x00003982
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x0000578A File Offset: 0x0000398A
		[JsonProperty(PropertyName = "context")]
		public string Context { get; protected set; }
	}
}
