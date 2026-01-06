using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.BlockedTerms
{
	// Token: 0x02000062 RID: 98
	public class AddBlockedTermResponse
	{
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00003B51 File Offset: 0x00001D51
		// (set) Token: 0x06000330 RID: 816 RVA: 0x00003B59 File Offset: 0x00001D59
		[JsonProperty(PropertyName = "data")]
		public BlockedTerm[] Data { get; protected set; }
	}
}
