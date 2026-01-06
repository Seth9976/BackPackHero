using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.ChannelPoints.CreateCustomReward
{
	// Token: 0x020000C8 RID: 200
	public class CreateCustomRewardsResponse
	{
		// Token: 0x17000304 RID: 772
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x00005A5A File Offset: 0x00003C5A
		// (set) Token: 0x060006B8 RID: 1720 RVA: 0x00005A62 File Offset: 0x00003C62
		[JsonProperty(PropertyName = "data")]
		public CustomReward[] Data { get; protected set; }
	}
}
