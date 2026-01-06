using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.ThirdParty.ModLookup
{
	// Token: 0x02000008 RID: 8
	public class Stats
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002399 File Offset: 0x00000599
		// (set) Token: 0x0600002B RID: 43 RVA: 0x000023A1 File Offset: 0x000005A1
		[JsonProperty(PropertyName = "relations")]
		public int Relations { get; protected set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000023AA File Offset: 0x000005AA
		// (set) Token: 0x0600002D RID: 45 RVA: 0x000023B2 File Offset: 0x000005B2
		[JsonProperty(PropertyName = "channels_total")]
		public int ChannelsTotal { get; protected set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000023BB File Offset: 0x000005BB
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000023C3 File Offset: 0x000005C3
		[JsonProperty(PropertyName = "users")]
		public int Users { get; protected set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000023CC File Offset: 0x000005CC
		// (set) Token: 0x06000031 RID: 49 RVA: 0x000023D4 File Offset: 0x000005D4
		[JsonProperty(PropertyName = "channels_no_mods")]
		public int ChannelsNoMods { get; protected set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000023DD File Offset: 0x000005DD
		// (set) Token: 0x06000033 RID: 51 RVA: 0x000023E5 File Offset: 0x000005E5
		[JsonProperty(PropertyName = "channels_only_broadcaster")]
		public int ChannelsOnlyBroadcaster { get; protected set; }
	}
}
