using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Charity.GetCharityCampaign
{
	// Token: 0x020000AF RID: 175
	public class Amount
	{
		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x000051E8 File Offset: 0x000033E8
		// (set) Token: 0x060005B8 RID: 1464 RVA: 0x000051F0 File Offset: 0x000033F0
		[JsonProperty(PropertyName = "value")]
		public int? Value { get; protected set; }

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x000051F9 File Offset: 0x000033F9
		// (set) Token: 0x060005BA RID: 1466 RVA: 0x00005201 File Offset: 0x00003401
		[JsonProperty(PropertyName = "decimal_places")]
		public int? DecimalPlaces { get; protected set; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0000520A File Offset: 0x0000340A
		// (set) Token: 0x060005BC RID: 1468 RVA: 0x00005212 File Offset: 0x00003412
		[JsonProperty(PropertyName = "currency")]
		public string Currency { get; protected set; }
	}
}
