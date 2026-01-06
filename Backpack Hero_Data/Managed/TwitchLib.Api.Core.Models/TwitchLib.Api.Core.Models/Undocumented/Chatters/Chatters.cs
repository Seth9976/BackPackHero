using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Core.Models.Undocumented.Chatters
{
	// Token: 0x02000004 RID: 4
	public class Chatters
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002090 File Offset: 0x00000290
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002098 File Offset: 0x00000298
		[JsonProperty(PropertyName = "moderators")]
		public string[] Moderators { get; protected set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020A1 File Offset: 0x000002A1
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000020A9 File Offset: 0x000002A9
		[JsonProperty(PropertyName = "staff")]
		public string[] Staff { get; protected set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020B2 File Offset: 0x000002B2
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000020BA File Offset: 0x000002BA
		[JsonProperty(PropertyName = "admins")]
		public string[] Admins { get; protected set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000020C3 File Offset: 0x000002C3
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000020CB File Offset: 0x000002CB
		[JsonProperty(PropertyName = "global_mods")]
		public string[] GlobalMods { get; protected set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000020D4 File Offset: 0x000002D4
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000020DC File Offset: 0x000002DC
		[JsonProperty(PropertyName = "vips")]
		public string[] VIP { get; protected set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000020E5 File Offset: 0x000002E5
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000020ED File Offset: 0x000002ED
		[JsonProperty(PropertyName = "viewers")]
		public string[] Viewers { get; protected set; }
	}
}
