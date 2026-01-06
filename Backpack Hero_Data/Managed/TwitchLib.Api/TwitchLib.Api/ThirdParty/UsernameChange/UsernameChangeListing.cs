using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.ThirdParty.UsernameChange
{
	// Token: 0x02000004 RID: 4
	public class UsernameChangeListing
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000229C File Offset: 0x0000049C
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000022A4 File Offset: 0x000004A4
		[JsonProperty(PropertyName = "userid")]
		public string UserId { get; protected set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000022AD File Offset: 0x000004AD
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000022B5 File Offset: 0x000004B5
		[JsonProperty(PropertyName = "username_old")]
		public string UsernameOld { get; protected set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000022BE File Offset: 0x000004BE
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000022C6 File Offset: 0x000004C6
		[JsonProperty(PropertyName = "username_new")]
		public string UsernameNew { get; protected set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000022CF File Offset: 0x000004CF
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000022D7 File Offset: 0x000004D7
		[JsonProperty(PropertyName = "found_at")]
		public DateTime FoundAt { get; protected set; }
	}
}
