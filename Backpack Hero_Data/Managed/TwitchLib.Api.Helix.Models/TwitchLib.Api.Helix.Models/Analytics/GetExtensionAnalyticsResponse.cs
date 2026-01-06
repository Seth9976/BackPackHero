using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Analytics
{
	// Token: 0x020000D7 RID: 215
	public class GetExtensionAnalyticsResponse
	{
		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x00005DE0 File Offset: 0x00003FE0
		// (set) Token: 0x06000723 RID: 1827 RVA: 0x00005DE8 File Offset: 0x00003FE8
		[JsonProperty(PropertyName = "data")]
		public ExtensionAnalytics[] Data { get; protected set; }
	}
}
