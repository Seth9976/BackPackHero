using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Chat.GetChatters
{
	// Token: 0x0200009D RID: 157
	public class GetChattersResponse
	{
		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x00004DB1 File Offset: 0x00002FB1
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x00004DB9 File Offset: 0x00002FB9
		[JsonProperty("data")]
		public Chatter[] Data { get; protected set; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x00004DC2 File Offset: 0x00002FC2
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x00004DCA File Offset: 0x00002FCA
		[JsonProperty(PropertyName = "pagination")]
		public Pagination Pagination { get; protected set; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x00004DD3 File Offset: 0x00002FD3
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x00004DDB File Offset: 0x00002FDB
		[JsonProperty("total")]
		public int Total { get; protected set; }
	}
}
