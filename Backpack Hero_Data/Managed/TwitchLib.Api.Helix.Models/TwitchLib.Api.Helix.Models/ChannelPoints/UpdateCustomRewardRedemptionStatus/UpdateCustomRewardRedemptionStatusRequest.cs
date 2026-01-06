using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Helix.Models.ChannelPoints.UpdateCustomRewardRedemptionStatus
{
	// Token: 0x020000C2 RID: 194
	public class UpdateCustomRewardRedemptionStatusRequest
	{
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x000057F9 File Offset: 0x000039F9
		// (set) Token: 0x06000670 RID: 1648 RVA: 0x00005801 File Offset: 0x00003A01
		[JsonConverter(typeof(StringEnumConverter))]
		[JsonProperty(PropertyName = "status")]
		public CustomRewardRedemptionStatus Status { get; set; }
	}
}
