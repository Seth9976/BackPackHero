using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Channels.GetChannelVIPs
{
	// Token: 0x020000B3 RID: 179
	public class ChannelVIPsResponseModel
	{
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x0000533A File Offset: 0x0000353A
		// (set) Token: 0x060005E0 RID: 1504 RVA: 0x00005342 File Offset: 0x00003542
		[JsonProperty(PropertyName = "user_id")]
		public string UserId { get; protected set; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x0000534B File Offset: 0x0000354B
		// (set) Token: 0x060005E2 RID: 1506 RVA: 0x00005353 File Offset: 0x00003553
		[JsonProperty(PropertyName = "user_name")]
		public string UserName { get; protected set; }

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x0000535C File Offset: 0x0000355C
		// (set) Token: 0x060005E4 RID: 1508 RVA: 0x00005364 File Offset: 0x00003564
		[JsonProperty(PropertyName = "user_login")]
		public string UserLogin { get; protected set; }
	}
}
