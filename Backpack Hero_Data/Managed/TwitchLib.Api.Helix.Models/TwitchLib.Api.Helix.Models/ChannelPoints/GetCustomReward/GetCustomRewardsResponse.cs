using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.ChannelPoints.GetCustomReward
{
	// Token: 0x020000C5 RID: 197
	public class GetCustomRewardsResponse
	{
		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x00005932 File Offset: 0x00003B32
		// (set) Token: 0x06000695 RID: 1685 RVA: 0x0000593A File Offset: 0x00003B3A
		[JsonProperty(PropertyName = "data")]
		public CustomReward[] Data { get; protected set; }
	}
}
