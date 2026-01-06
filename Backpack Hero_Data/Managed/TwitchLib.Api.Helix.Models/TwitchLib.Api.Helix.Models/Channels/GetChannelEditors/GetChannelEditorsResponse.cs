using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Channels.GetChannelEditors
{
	// Token: 0x020000B8 RID: 184
	public class GetChannelEditorsResponse
	{
		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x00005483 File Offset: 0x00003683
		// (set) Token: 0x06000607 RID: 1543 RVA: 0x0000548B File Offset: 0x0000368B
		[JsonProperty(PropertyName = "data")]
		public ChannelEditor[] Data { get; protected set; }
	}
}
