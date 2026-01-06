using System;
using Newtonsoft.Json;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
	// Token: 0x02000018 RID: 24
	public class User
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00006C38 File Offset: 0x00004E38
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00006C40 File Offset: 0x00004E40
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00006C49 File Offset: 0x00004E49
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00006C51 File Offset: 0x00004E51
		[JsonProperty(PropertyName = "login")]
		public string Login { get; protected set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00006C5A File Offset: 0x00004E5A
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00006C62 File Offset: 0x00004E62
		[JsonProperty(PropertyName = "display_name")]
		public string DisplayName { get; protected set; }
	}
}
