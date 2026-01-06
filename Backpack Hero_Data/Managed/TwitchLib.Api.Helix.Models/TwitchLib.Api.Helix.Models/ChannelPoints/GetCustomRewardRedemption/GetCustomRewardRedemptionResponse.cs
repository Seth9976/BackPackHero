using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.ChannelPoints.GetCustomRewardRedemption
{
	// Token: 0x020000C6 RID: 198
	public class GetCustomRewardRedemptionResponse
	{
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x0000594B File Offset: 0x00003B4B
		// (set) Token: 0x06000698 RID: 1688 RVA: 0x00005953 File Offset: 0x00003B53
		[JsonProperty(PropertyName = "data")]
		public RewardRedemption[] Data { get; protected set; }

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x0000595C File Offset: 0x00003B5C
		// (set) Token: 0x0600069A RID: 1690 RVA: 0x00005964 File Offset: 0x00003B64
		[JsonProperty(PropertyName = "pagination")]
		public Pagination Pagination { get; protected set; }
	}
}
