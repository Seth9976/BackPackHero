using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;
using TwitchLib.Api.Helix.Models.Games;

namespace TwitchLib.Api.Helix.Models.Search
{
	// Token: 0x02000038 RID: 56
	public class SearchCategoriesResponse
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00002FC7 File Offset: 0x000011C7
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x00002FCF File Offset: 0x000011CF
		[JsonProperty(PropertyName = "data")]
		public Game[] Games { get; protected set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00002FD8 File Offset: 0x000011D8
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x00002FE0 File Offset: 0x000011E0
		[JsonProperty(PropertyName = "pagination")]
		public Pagination Pagination { get; protected set; }
	}
}
