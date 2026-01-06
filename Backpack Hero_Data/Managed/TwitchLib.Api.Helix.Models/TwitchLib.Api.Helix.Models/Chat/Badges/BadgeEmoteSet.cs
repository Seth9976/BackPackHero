using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.Badges
{
	// Token: 0x020000AB RID: 171
	public class BadgeEmoteSet
	{
		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00005140 File Offset: 0x00003340
		// (set) Token: 0x060005A4 RID: 1444 RVA: 0x00005148 File Offset: 0x00003348
		[JsonProperty(PropertyName = "set_id")]
		public string SetId { get; protected set; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x00005151 File Offset: 0x00003351
		// (set) Token: 0x060005A6 RID: 1446 RVA: 0x00005159 File Offset: 0x00003359
		[JsonProperty(PropertyName = "versions")]
		public BadgeVersion[] Versions { get; protected set; }
	}
}
