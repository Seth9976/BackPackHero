using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Analytics
{
	// Token: 0x020000D6 RID: 214
	public class GameAnalytics
	{
		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x00005D94 File Offset: 0x00003F94
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x00005D9C File Offset: 0x00003F9C
		[JsonProperty(PropertyName = "game_id")]
		public string GameId { get; protected set; }

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x00005DA5 File Offset: 0x00003FA5
		// (set) Token: 0x0600071C RID: 1820 RVA: 0x00005DAD File Offset: 0x00003FAD
		[JsonProperty(PropertyName = "URL")]
		public string Url { get; protected set; }

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x00005DB6 File Offset: 0x00003FB6
		// (set) Token: 0x0600071E RID: 1822 RVA: 0x00005DBE File Offset: 0x00003FBE
		[JsonProperty(PropertyName = "type")]
		public string Type { get; protected set; }

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x00005DC7 File Offset: 0x00003FC7
		// (set) Token: 0x06000720 RID: 1824 RVA: 0x00005DCF File Offset: 0x00003FCF
		[JsonProperty(PropertyName = "date_range")]
		public DateRange DateRange { get; protected set; }
	}
}
