using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.ThirdParty.ModLookup
{
	// Token: 0x0200000A RID: 10
	public class Top
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002420 File Offset: 0x00000620
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002428 File Offset: 0x00000628
		[JsonProperty(PropertyName = "modcount")]
		public ModLookupListing[] ModCount { get; protected set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002431 File Offset: 0x00000631
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002439 File Offset: 0x00000639
		[JsonProperty(PropertyName = "views")]
		public ModLookupListing[] Views { get; protected set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002442 File Offset: 0x00000642
		// (set) Token: 0x0600003F RID: 63 RVA: 0x0000244A File Offset: 0x0000064A
		[JsonProperty(PropertyName = "followers")]
		public ModLookupListing[] Followers { get; protected set; }
	}
}
