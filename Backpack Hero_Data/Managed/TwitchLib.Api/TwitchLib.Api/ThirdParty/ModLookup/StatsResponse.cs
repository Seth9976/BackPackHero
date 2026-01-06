using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.ThirdParty.ModLookup
{
	// Token: 0x02000009 RID: 9
	public class StatsResponse
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000023F6 File Offset: 0x000005F6
		// (set) Token: 0x06000036 RID: 54 RVA: 0x000023FE File Offset: 0x000005FE
		[JsonProperty(PropertyName = "status")]
		public int Status { get; protected set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002407 File Offset: 0x00000607
		// (set) Token: 0x06000038 RID: 56 RVA: 0x0000240F File Offset: 0x0000060F
		[JsonProperty(PropertyName = "stats")]
		public Stats Stats { get; protected set; }
	}
}
