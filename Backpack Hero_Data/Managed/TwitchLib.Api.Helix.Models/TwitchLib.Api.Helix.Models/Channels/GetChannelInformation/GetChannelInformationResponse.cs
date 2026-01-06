using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Channels.GetChannelInformation
{
	// Token: 0x020000B6 RID: 182
	public class GetChannelInformationResponse
	{
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x0000542F File Offset: 0x0000362F
		// (set) Token: 0x060005FD RID: 1533 RVA: 0x00005437 File Offset: 0x00003637
		[JsonProperty(PropertyName = "data")]
		public ChannelInformation[] Data { get; protected set; }
	}
}
