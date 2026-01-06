using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
	// Token: 0x02000017 RID: 23
	public class SubMessage : MessageData
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00006B9B File Offset: 0x00004D9B
		public string Message { get; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00006BA3 File Offset: 0x00004DA3
		public List<SubMessage.Emote> Emotes { get; } = new List<SubMessage.Emote>();

		// Token: 0x06000133 RID: 307 RVA: 0x00006BAC File Offset: 0x00004DAC
		public SubMessage(JToken json)
		{
			JToken jtoken = json.SelectToken("message");
			this.Message = ((jtoken != null) ? jtoken.ToString() : null);
			foreach (JToken jtoken2 in json.SelectToken("emotes"))
			{
				this.Emotes.Add(new SubMessage.Emote(jtoken2));
			}
		}

		// Token: 0x02000064 RID: 100
		public class Emote
		{
			// Token: 0x170000BB RID: 187
			// (get) Token: 0x06000262 RID: 610 RVA: 0x000075DF File Offset: 0x000057DF
			public int Start { get; }

			// Token: 0x170000BC RID: 188
			// (get) Token: 0x06000263 RID: 611 RVA: 0x000075E7 File Offset: 0x000057E7
			public int End { get; }

			// Token: 0x170000BD RID: 189
			// (get) Token: 0x06000264 RID: 612 RVA: 0x000075EF File Offset: 0x000057EF
			public string Id { get; }

			// Token: 0x06000265 RID: 613 RVA: 0x000075F8 File Offset: 0x000057F8
			public Emote(JToken json)
			{
				this.Start = int.Parse(json.SelectToken("start").ToString());
				this.End = int.Parse(json.SelectToken("end").ToString());
				this.Id = json.SelectToken("id").ToString();
			}
		}
	}
}
