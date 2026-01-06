using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Channels.GetChannelVIPs
{
	// Token: 0x020000B4 RID: 180
	public class GetChannelVIPsResponse
	{
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x00005375 File Offset: 0x00003575
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x0000537D File Offset: 0x0000357D
		[JsonProperty(PropertyName = "data")]
		public ChannelVIPsResponseModel[] Data { get; protected set; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x00005386 File Offset: 0x00003586
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x0000538E File Offset: 0x0000358E
		[JsonProperty(PropertyName = "pagination")]
		public Pagination Pagination { get; protected set; }
	}
}
