using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Bits
{
	// Token: 0x020000C9 RID: 201
	public class Cheermote
	{
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x00005A73 File Offset: 0x00003C73
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x00005A7B File Offset: 0x00003C7B
		[JsonProperty(PropertyName = "prefix")]
		public string Prefix { get; protected set; }

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x00005A84 File Offset: 0x00003C84
		// (set) Token: 0x060006BD RID: 1725 RVA: 0x00005A8C File Offset: 0x00003C8C
		[JsonProperty(PropertyName = "tiers")]
		public Tier[] Tiers { get; protected set; }

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x00005A95 File Offset: 0x00003C95
		// (set) Token: 0x060006BF RID: 1727 RVA: 0x00005A9D File Offset: 0x00003C9D
		[JsonProperty(PropertyName = "type")]
		public string Type { get; protected set; }

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x00005AA6 File Offset: 0x00003CA6
		// (set) Token: 0x060006C1 RID: 1729 RVA: 0x00005AAE File Offset: 0x00003CAE
		[JsonProperty(PropertyName = "order")]
		public int Order { get; protected set; }

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x00005AB7 File Offset: 0x00003CB7
		// (set) Token: 0x060006C3 RID: 1731 RVA: 0x00005ABF File Offset: 0x00003CBF
		[JsonProperty(PropertyName = "last_updated")]
		public DateTime LastUpdated { get; protected set; }

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00005AC8 File Offset: 0x00003CC8
		// (set) Token: 0x060006C5 RID: 1733 RVA: 0x00005AD0 File Offset: 0x00003CD0
		[JsonProperty(PropertyName = "is_charitable")]
		public bool IsCharitable { get; protected set; }
	}
}
