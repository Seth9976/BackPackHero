using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Bits.ExtensionBitsProducts
{
	// Token: 0x020000D2 RID: 210
	public class ExtensionBitsProduct
	{
		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x00005CA8 File Offset: 0x00003EA8
		// (set) Token: 0x060006FE RID: 1790 RVA: 0x00005CB0 File Offset: 0x00003EB0
		[JsonProperty(PropertyName = "sku")]
		public string Sku { get; protected set; }

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x00005CB9 File Offset: 0x00003EB9
		// (set) Token: 0x06000700 RID: 1792 RVA: 0x00005CC1 File Offset: 0x00003EC1
		[JsonProperty(PropertyName = "cost")]
		public Cost Cost { get; protected set; }

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x00005CCA File Offset: 0x00003ECA
		// (set) Token: 0x06000702 RID: 1794 RVA: 0x00005CD2 File Offset: 0x00003ED2
		[JsonProperty(PropertyName = "in_development")]
		public bool InDevelopment { get; protected set; }

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x00005CDB File Offset: 0x00003EDB
		// (set) Token: 0x06000704 RID: 1796 RVA: 0x00005CE3 File Offset: 0x00003EE3
		[JsonProperty(PropertyName = "display_name")]
		public string DisplayName { get; protected set; }

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x00005CEC File Offset: 0x00003EEC
		// (set) Token: 0x06000706 RID: 1798 RVA: 0x00005CF4 File Offset: 0x00003EF4
		[JsonProperty(PropertyName = "expiration")]
		public DateTime Expiration { get; protected set; }

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x00005CFD File Offset: 0x00003EFD
		// (set) Token: 0x06000708 RID: 1800 RVA: 0x00005D05 File Offset: 0x00003F05
		[JsonProperty(PropertyName = "is_broadcast")]
		public bool IsBroadcast { get; protected set; }
	}
}
