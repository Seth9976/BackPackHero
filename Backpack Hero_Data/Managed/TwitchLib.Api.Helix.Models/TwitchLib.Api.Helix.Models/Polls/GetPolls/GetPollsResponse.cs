using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Polls.GetPolls
{
	// Token: 0x0200004F RID: 79
	public class GetPollsResponse
	{
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00003657 File Offset: 0x00001857
		// (set) Token: 0x06000299 RID: 665 RVA: 0x0000365F File Offset: 0x0000185F
		[JsonProperty(PropertyName = "data")]
		public Poll[] Data { get; protected set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00003668 File Offset: 0x00001868
		// (set) Token: 0x0600029B RID: 667 RVA: 0x00003670 File Offset: 0x00001870
		[JsonProperty(PropertyName = "pagination")]
		public Pagination Pagination { get; protected set; }
	}
}
