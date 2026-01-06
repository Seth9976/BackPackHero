using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Analytics
{
	// Token: 0x020000D8 RID: 216
	public class GetGameAnalyticsResponse
	{
		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x00005DF9 File Offset: 0x00003FF9
		// (set) Token: 0x06000726 RID: 1830 RVA: 0x00005E01 File Offset: 0x00004001
		[JsonProperty(PropertyName = "data")]
		public GameAnalytics[] Data { get; protected set; }

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x00005E0A File Offset: 0x0000400A
		// (set) Token: 0x06000728 RID: 1832 RVA: 0x00005E12 File Offset: 0x00004012
		[JsonProperty(PropertyName = "pagination")]
		public Pagination Pagination { get; protected set; }
	}
}
