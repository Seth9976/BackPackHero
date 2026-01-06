using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Predictions.CreatePrediction
{
	// Token: 0x0200004A RID: 74
	public class CreatePredictionRequest
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00003497 File Offset: 0x00001697
		// (set) Token: 0x06000264 RID: 612 RVA: 0x0000349F File Offset: 0x0000169F
		[JsonProperty(PropertyName = "broadcaster_id")]
		public string BroadcasterId { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000265 RID: 613 RVA: 0x000034A8 File Offset: 0x000016A8
		// (set) Token: 0x06000266 RID: 614 RVA: 0x000034B0 File Offset: 0x000016B0
		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000267 RID: 615 RVA: 0x000034B9 File Offset: 0x000016B9
		// (set) Token: 0x06000268 RID: 616 RVA: 0x000034C1 File Offset: 0x000016C1
		[JsonProperty(PropertyName = "outcomes")]
		public Outcome[] Outcomes { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000269 RID: 617 RVA: 0x000034CA File Offset: 0x000016CA
		// (set) Token: 0x0600026A RID: 618 RVA: 0x000034D2 File Offset: 0x000016D2
		[JsonProperty(PropertyName = "prediction_window")]
		public int PredictionWindowSeconds { get; set; }
	}
}
