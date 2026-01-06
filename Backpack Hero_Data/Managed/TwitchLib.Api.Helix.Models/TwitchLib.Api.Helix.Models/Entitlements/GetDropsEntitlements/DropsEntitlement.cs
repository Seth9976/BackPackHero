using System;
using Newtonsoft.Json;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Helix.Models.Entitlements.GetDropsEntitlements
{
	// Token: 0x0200008E RID: 142
	public class DropsEntitlement
	{
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x00004939 File Offset: 0x00002B39
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x00004941 File Offset: 0x00002B41
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x0000494A File Offset: 0x00002B4A
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x00004952 File Offset: 0x00002B52
		[JsonProperty(PropertyName = "benefit_id")]
		public string BenefitId { get; protected set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x0000495B File Offset: 0x00002B5B
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x00004963 File Offset: 0x00002B63
		[JsonProperty(PropertyName = "timestamp")]
		public DateTime Timestamp { get; protected set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0000496C File Offset: 0x00002B6C
		// (set) Token: 0x060004C0 RID: 1216 RVA: 0x00004974 File Offset: 0x00002B74
		[JsonProperty(PropertyName = "user_id")]
		public string UserId { get; protected set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x0000497D File Offset: 0x00002B7D
		// (set) Token: 0x060004C2 RID: 1218 RVA: 0x00004985 File Offset: 0x00002B85
		[JsonProperty(PropertyName = "game_id")]
		public string GameId { get; protected set; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x0000498E File Offset: 0x00002B8E
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x00004996 File Offset: 0x00002B96
		[JsonProperty(PropertyName = "fulfillment_status")]
		public FulfillmentStatus FulfillmentStatus { get; protected set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0000499F File Offset: 0x00002B9F
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x000049A7 File Offset: 0x00002BA7
		[JsonProperty(PropertyName = "updated_at")]
		public DateTime UpdatedAt { get; protected set; }
	}
}
