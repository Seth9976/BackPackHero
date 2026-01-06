using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.EventSub
{
	// Token: 0x02000086 RID: 134
	public class CreateEventSubSubscriptionResponse
	{
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0000472E File Offset: 0x0000292E
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x00004736 File Offset: 0x00002936
		[JsonProperty(PropertyName = "data")]
		public EventSubSubscription[] Subscriptions { get; protected set; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000473F File Offset: 0x0000293F
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x00004747 File Offset: 0x00002947
		[JsonProperty(PropertyName = "total")]
		public int Total { get; protected set; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x00004750 File Offset: 0x00002950
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x00004758 File Offset: 0x00002958
		[JsonProperty(PropertyName = "total_cost")]
		public int TotalCost { get; protected set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x00004761 File Offset: 0x00002961
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x00004769 File Offset: 0x00002969
		[JsonProperty(PropertyName = "max_total_cost")]
		public int MaxTotalCost { get; protected set; }
	}
}
