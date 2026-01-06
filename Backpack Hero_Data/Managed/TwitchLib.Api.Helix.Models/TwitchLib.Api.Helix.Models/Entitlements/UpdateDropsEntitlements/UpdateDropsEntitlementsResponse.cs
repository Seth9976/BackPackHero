using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Entitlements.UpdateDropsEntitlements
{
	// Token: 0x0200008C RID: 140
	public class UpdateDropsEntitlementsResponse
	{
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00004907 File Offset: 0x00002B07
		// (set) Token: 0x060004B4 RID: 1204 RVA: 0x0000490F File Offset: 0x00002B0F
		[JsonProperty(PropertyName = "data")]
		public DropEntitlementUpdate[] DropEntitlementUpdates { get; protected set; }
	}
}
