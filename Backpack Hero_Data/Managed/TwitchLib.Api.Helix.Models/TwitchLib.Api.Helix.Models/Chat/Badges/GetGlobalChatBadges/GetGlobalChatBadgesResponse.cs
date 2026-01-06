using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.Badges.GetGlobalChatBadges
{
	// Token: 0x020000AD RID: 173
	public class GetGlobalChatBadgesResponse
	{
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x000051B6 File Offset: 0x000033B6
		// (set) Token: 0x060005B2 RID: 1458 RVA: 0x000051BE File Offset: 0x000033BE
		[JsonProperty(PropertyName = "data")]
		public BadgeEmoteSet[] EmoteSet { get; protected set; }
	}
}
