using System;
using Newtonsoft.Json;

namespace TwitchLib.PubSub.Models.Responses.Messages.Redemption
{
	// Token: 0x02000022 RID: 34
	public class RedemptionImage
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00007055 File Offset: 0x00005255
		// (set) Token: 0x06000173 RID: 371 RVA: 0x0000705D File Offset: 0x0000525D
		[JsonProperty(PropertyName = "url_1x")]
		public string Url1x { get; protected set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00007066 File Offset: 0x00005266
		// (set) Token: 0x06000175 RID: 373 RVA: 0x0000706E File Offset: 0x0000526E
		[JsonProperty(PropertyName = "url_2x")]
		public string Url2x { get; protected set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00007077 File Offset: 0x00005277
		// (set) Token: 0x06000177 RID: 375 RVA: 0x0000707F File Offset: 0x0000527F
		[JsonProperty(PropertyName = "url_4x")]
		public string Url4x { get; protected set; }
	}
}
