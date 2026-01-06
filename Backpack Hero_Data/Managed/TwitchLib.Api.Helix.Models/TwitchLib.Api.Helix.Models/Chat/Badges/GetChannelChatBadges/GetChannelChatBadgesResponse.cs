using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.Badges.GetChannelChatBadges
{
	// Token: 0x020000AE RID: 174
	public class GetChannelChatBadgesResponse
	{
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x000051CF File Offset: 0x000033CF
		// (set) Token: 0x060005B5 RID: 1461 RVA: 0x000051D7 File Offset: 0x000033D7
		[JsonProperty(PropertyName = "data")]
		public BadgeEmoteSet[] EmoteSet { get; protected set; }
	}
}
