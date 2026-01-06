using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.ChannelPoints.UpdateRedemptionStatus
{
	// Token: 0x020000C1 RID: 193
	public class UpdateRedemptionStatusResponse
	{
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x000057E0 File Offset: 0x000039E0
		// (set) Token: 0x0600066D RID: 1645 RVA: 0x000057E8 File Offset: 0x000039E8
		[JsonProperty(PropertyName = "data")]
		public RewardRedemption[] Data { get; protected set; }
	}
}
