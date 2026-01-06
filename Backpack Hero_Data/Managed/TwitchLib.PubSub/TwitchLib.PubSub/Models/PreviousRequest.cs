using System;
using TwitchLib.PubSub.Enums;

namespace TwitchLib.PubSub.Models
{
	// Token: 0x02000005 RID: 5
	public class PreviousRequest
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000087 RID: 135 RVA: 0x0000506E File Offset: 0x0000326E
		public string Nonce { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00005076 File Offset: 0x00003276
		public PubSubRequestType RequestType { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000089 RID: 137 RVA: 0x0000507E File Offset: 0x0000327E
		public string Topic { get; }

		// Token: 0x0600008A RID: 138 RVA: 0x00005086 File Offset: 0x00003286
		public PreviousRequest(string nonce, PubSubRequestType requestType, string topic = "none set")
		{
			this.Nonce = nonce;
			this.RequestType = requestType;
			this.Topic = topic;
		}
	}
}
