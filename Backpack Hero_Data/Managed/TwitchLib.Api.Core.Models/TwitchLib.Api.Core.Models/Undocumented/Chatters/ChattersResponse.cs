using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Core.Models.Undocumented.Chatters
{
	// Token: 0x02000005 RID: 5
	public class ChattersResponse
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000020FE File Offset: 0x000002FE
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002106 File Offset: 0x00000306
		[JsonProperty(PropertyName = "chatter_count")]
		public int ChatterCount { get; protected set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000210F File Offset: 0x0000030F
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002117 File Offset: 0x00000317
		[JsonProperty(PropertyName = "chatters")]
		public Chatters Chatters { get; protected set; }
	}
}
