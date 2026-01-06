using System;
using Newtonsoft.Json.Linq;

namespace TwitchLib.PubSub.Models.Responses
{
	// Token: 0x02000007 RID: 7
	public class Response
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000053FF File Offset: 0x000035FF
		public string Error { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00005407 File Offset: 0x00003607
		public string Nonce { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600008F RID: 143 RVA: 0x0000540F File Offset: 0x0000360F
		public bool Successful { get; }

		// Token: 0x06000090 RID: 144 RVA: 0x00005418 File Offset: 0x00003618
		public Response(string json)
		{
			JToken jtoken = JObject.Parse(json).SelectToken("error");
			this.Error = ((jtoken != null) ? jtoken.ToString() : null);
			JToken jtoken2 = JObject.Parse(json).SelectToken("nonce");
			this.Nonce = ((jtoken2 != null) ? jtoken2.ToString() : null);
			if (string.IsNullOrWhiteSpace(this.Error))
			{
				this.Successful = true;
			}
		}
	}
}
