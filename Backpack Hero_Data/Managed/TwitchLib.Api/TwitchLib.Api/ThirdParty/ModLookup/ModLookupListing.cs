using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.ThirdParty.ModLookup
{
	// Token: 0x02000006 RID: 6
	public class ModLookupListing
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002301 File Offset: 0x00000501
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002309 File Offset: 0x00000509
		[JsonProperty(PropertyName = "name")]
		public string Name { get; protected set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002312 File Offset: 0x00000512
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000231A File Offset: 0x0000051A
		[JsonProperty(PropertyName = "followers")]
		public int Followers { get; protected set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002323 File Offset: 0x00000523
		// (set) Token: 0x0600001D RID: 29 RVA: 0x0000232B File Offset: 0x0000052B
		[JsonProperty(PropertyName = "views")]
		public int Views { get; protected set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002334 File Offset: 0x00000534
		// (set) Token: 0x0600001F RID: 31 RVA: 0x0000233C File Offset: 0x0000053C
		[JsonProperty(PropertyName = "partnered")]
		public bool Partnered { get; protected set; }
	}
}
