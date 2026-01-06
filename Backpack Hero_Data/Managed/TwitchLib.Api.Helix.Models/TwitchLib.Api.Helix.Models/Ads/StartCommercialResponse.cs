using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Ads
{
	// Token: 0x020000DA RID: 218
	public class StartCommercialResponse
	{
		// Token: 0x17000337 RID: 823
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x00005E4D File Offset: 0x0000404D
		// (set) Token: 0x06000730 RID: 1840 RVA: 0x00005E55 File Offset: 0x00004055
		[JsonProperty(PropertyName = "length")]
		public int Length { get; protected set; }

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x00005E5E File Offset: 0x0000405E
		// (set) Token: 0x06000732 RID: 1842 RVA: 0x00005E66 File Offset: 0x00004066
		[JsonProperty(PropertyName = "message")]
		public string Message { get; protected set; }

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x00005E6F File Offset: 0x0000406F
		// (set) Token: 0x06000734 RID: 1844 RVA: 0x00005E77 File Offset: 0x00004077
		[JsonProperty(PropertyName = "retry_after")]
		public int RetryAfter { get; protected set; }
	}
}
