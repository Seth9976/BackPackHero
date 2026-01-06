using System;
using Newtonsoft.Json;

namespace TwitchLib.PubSub.Models.Responses.Messages.Redemption
{
	// Token: 0x02000024 RID: 36
	public class RewardRedeemed : ChannelPointsData
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x000071EC File Offset: 0x000053EC
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x000071F4 File Offset: 0x000053F4
		[JsonProperty(PropertyName = "timestamp")]
		public DateTime Timestamp { get; protected set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x000071FD File Offset: 0x000053FD
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00007205 File Offset: 0x00005405
		[JsonProperty(PropertyName = "redemption")]
		public Redemption Redemption { get; protected set; }
	}
}
