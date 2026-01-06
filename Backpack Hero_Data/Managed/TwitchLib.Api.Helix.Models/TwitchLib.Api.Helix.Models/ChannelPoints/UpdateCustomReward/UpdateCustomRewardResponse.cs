using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.ChannelPoints.UpdateCustomReward
{
	// Token: 0x020000C4 RID: 196
	public class UpdateCustomRewardResponse
	{
		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x00005919 File Offset: 0x00003B19
		// (set) Token: 0x06000692 RID: 1682 RVA: 0x00005921 File Offset: 0x00003B21
		[JsonProperty(PropertyName = "data")]
		public CustomReward[] Data { get; protected set; }
	}
}
