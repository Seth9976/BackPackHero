using System;
using Newtonsoft.Json;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Helix.Models.Entitlements.UpdateDropsEntitlements
{
	// Token: 0x0200008B RID: 139
	public class DropEntitlementUpdate
	{
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x000048DD File Offset: 0x00002ADD
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x000048E5 File Offset: 0x00002AE5
		[JsonProperty(PropertyName = "status")]
		public DropEntitlementUpdateStatus Status { get; protected set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x000048EE File Offset: 0x00002AEE
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x000048F6 File Offset: 0x00002AF6
		[JsonProperty(PropertyName = "ids")]
		public string[] Ids { get; protected set; }
	}
}
