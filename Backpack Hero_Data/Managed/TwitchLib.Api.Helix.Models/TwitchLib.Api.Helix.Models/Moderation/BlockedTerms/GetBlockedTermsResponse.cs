using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Moderation.BlockedTerms
{
	// Token: 0x02000064 RID: 100
	public class GetBlockedTermsResponse
	{
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00003BE9 File Offset: 0x00001DE9
		// (set) Token: 0x06000342 RID: 834 RVA: 0x00003BF1 File Offset: 0x00001DF1
		[JsonProperty(PropertyName = "data")]
		public BlockedTerm[] Data { get; protected set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00003BFA File Offset: 0x00001DFA
		// (set) Token: 0x06000344 RID: 836 RVA: 0x00003C02 File Offset: 0x00001E02
		[JsonProperty(PropertyName = "pagination")]
		public Pagination Pagination { get; protected set; }
	}
}
