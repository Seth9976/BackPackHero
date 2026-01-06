using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Entitlements.GetDropsEntitlements
{
	// Token: 0x0200008F RID: 143
	public class GetDropsEntitlementsResponse
	{
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x000049B8 File Offset: 0x00002BB8
		// (set) Token: 0x060004C9 RID: 1225 RVA: 0x000049C0 File Offset: 0x00002BC0
		[JsonProperty(PropertyName = "data")]
		public DropsEntitlement[] DropEntitlements { get; protected set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x000049C9 File Offset: 0x00002BC9
		// (set) Token: 0x060004CB RID: 1227 RVA: 0x000049D1 File Offset: 0x00002BD1
		[JsonProperty(PropertyName = "pagination")]
		public Pagination Pagination { get; protected set; }
	}
}
