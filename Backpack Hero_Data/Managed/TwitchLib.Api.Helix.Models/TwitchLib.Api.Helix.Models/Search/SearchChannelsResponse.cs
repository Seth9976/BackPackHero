using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Search
{
	// Token: 0x02000039 RID: 57
	public class SearchChannelsResponse
	{
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00002FF1 File Offset: 0x000011F1
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x00002FF9 File Offset: 0x000011F9
		[JsonProperty(PropertyName = "data")]
		public Channel[] Channels { get; protected set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00003002 File Offset: 0x00001202
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x0000300A File Offset: 0x0000120A
		[JsonProperty(PropertyName = "pagination")]
		public Pagination Pagination { get; protected set; }
	}
}
