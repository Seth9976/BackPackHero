using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Bits
{
	// Token: 0x020000CB RID: 203
	public class GetBitsLeaderboardResponse
	{
		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x00005B0B File Offset: 0x00003D0B
		// (set) Token: 0x060006CD RID: 1741 RVA: 0x00005B13 File Offset: 0x00003D13
		[JsonProperty(PropertyName = "data")]
		public Listing[] Listings { get; protected set; }

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x00005B1C File Offset: 0x00003D1C
		// (set) Token: 0x060006CF RID: 1743 RVA: 0x00005B24 File Offset: 0x00003D24
		[JsonProperty(PropertyName = "date_range")]
		public DateRange DateRange { get; protected set; }

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x00005B2D File Offset: 0x00003D2D
		// (set) Token: 0x060006D1 RID: 1745 RVA: 0x00005B35 File Offset: 0x00003D35
		[JsonProperty(PropertyName = "total")]
		public int Total { get; protected set; }
	}
}
