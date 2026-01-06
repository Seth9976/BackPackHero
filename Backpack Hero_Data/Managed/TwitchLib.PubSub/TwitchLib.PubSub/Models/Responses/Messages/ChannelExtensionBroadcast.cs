using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
	// Token: 0x0200000C RID: 12
	public class ChannelExtensionBroadcast : MessageData
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000579B File Offset: 0x0000399B
		public List<string> Messages { get; } = new List<string>();

		// Token: 0x060000B9 RID: 185 RVA: 0x000057A4 File Offset: 0x000039A4
		public ChannelExtensionBroadcast(string jsonStr)
		{
			foreach (JToken jtoken in JObject.Parse(jsonStr)["content"])
			{
				this.Messages.Add(jtoken.ToString());
			}
		}
	}
}
