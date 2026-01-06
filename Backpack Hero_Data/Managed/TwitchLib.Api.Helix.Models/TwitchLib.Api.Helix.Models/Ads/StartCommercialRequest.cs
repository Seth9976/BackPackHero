using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Ads
{
	// Token: 0x020000D9 RID: 217
	public class StartCommercialRequest
	{
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x00005E23 File Offset: 0x00004023
		// (set) Token: 0x0600072B RID: 1835 RVA: 0x00005E2B File Offset: 0x0000402B
		[JsonProperty(PropertyName = "broadcaster_id")]
		public string BroadcasterId { get; set; }

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x00005E34 File Offset: 0x00004034
		// (set) Token: 0x0600072D RID: 1837 RVA: 0x00005E3C File Offset: 0x0000403C
		[JsonProperty(PropertyName = "length")]
		public int Length { get; set; }
	}
}
