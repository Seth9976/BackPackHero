using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.EventSub
{
	// Token: 0x02000089 RID: 137
	public class GetEventSubSubscriptionsResponse
	{
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x00004856 File Offset: 0x00002A56
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x0000485E File Offset: 0x00002A5E
		[JsonProperty(PropertyName = "total")]
		public int Total { get; protected set; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00004867 File Offset: 0x00002A67
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x0000486F File Offset: 0x00002A6F
		[JsonProperty(PropertyName = "data")]
		public EventSubSubscription[] Subscriptions { get; protected set; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x00004878 File Offset: 0x00002A78
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x00004880 File Offset: 0x00002A80
		[JsonProperty(PropertyName = "total_cost")]
		public int TotalCost { get; protected set; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00004889 File Offset: 0x00002A89
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x00004891 File Offset: 0x00002A91
		[JsonProperty(PropertyName = "max_total_cost")]
		public int MaxTotalCost { get; protected set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0000489A File Offset: 0x00002A9A
		// (set) Token: 0x060004A7 RID: 1191 RVA: 0x000048A2 File Offset: 0x00002AA2
		[JsonProperty(PropertyName = "pagination")]
		public Pagination Pagination { get; protected set; }
	}
}
