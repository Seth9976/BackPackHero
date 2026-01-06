using System;
using Newtonsoft.Json;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Helix.Models.Predictions
{
	// Token: 0x02000046 RID: 70
	public class Prediction
	{
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00003323 File Offset: 0x00001523
		// (set) Token: 0x06000238 RID: 568 RVA: 0x0000332B File Offset: 0x0000152B
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00003334 File Offset: 0x00001534
		// (set) Token: 0x0600023A RID: 570 RVA: 0x0000333C File Offset: 0x0000153C
		[JsonProperty(PropertyName = "broadcaster_id")]
		public string BroadcasterId { get; protected set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00003345 File Offset: 0x00001545
		// (set) Token: 0x0600023C RID: 572 RVA: 0x0000334D File Offset: 0x0000154D
		[JsonProperty(PropertyName = "broadcaster_name")]
		public string BroadcasterName { get; protected set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00003356 File Offset: 0x00001556
		// (set) Token: 0x0600023E RID: 574 RVA: 0x0000335E File Offset: 0x0000155E
		[JsonProperty(PropertyName = "broadcaster_login")]
		public string BroadcasterLogin { get; protected set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00003367 File Offset: 0x00001567
		// (set) Token: 0x06000240 RID: 576 RVA: 0x0000336F File Offset: 0x0000156F
		[JsonProperty(PropertyName = "title")]
		public string Title { get; protected set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00003378 File Offset: 0x00001578
		// (set) Token: 0x06000242 RID: 578 RVA: 0x00003380 File Offset: 0x00001580
		[JsonProperty(PropertyName = "winning_outcome_id")]
		public string WinningOutcomeId { get; protected set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00003389 File Offset: 0x00001589
		// (set) Token: 0x06000244 RID: 580 RVA: 0x00003391 File Offset: 0x00001591
		[JsonProperty(PropertyName = "outcomes")]
		public Outcome[] Outcomes { get; protected set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000339A File Offset: 0x0000159A
		// (set) Token: 0x06000246 RID: 582 RVA: 0x000033A2 File Offset: 0x000015A2
		[JsonProperty(PropertyName = "prediction_window")]
		public string PredictionWindow { get; protected set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000247 RID: 583 RVA: 0x000033AB File Offset: 0x000015AB
		// (set) Token: 0x06000248 RID: 584 RVA: 0x000033B3 File Offset: 0x000015B3
		[JsonProperty(PropertyName = "status")]
		public PredictionStatus Status { get; protected set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000249 RID: 585 RVA: 0x000033BC File Offset: 0x000015BC
		// (set) Token: 0x0600024A RID: 586 RVA: 0x000033C4 File Offset: 0x000015C4
		[JsonProperty(PropertyName = "created_at")]
		public string CreatedAt { get; protected set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600024B RID: 587 RVA: 0x000033CD File Offset: 0x000015CD
		// (set) Token: 0x0600024C RID: 588 RVA: 0x000033D5 File Offset: 0x000015D5
		[JsonProperty(PropertyName = "ended_at")]
		public string EndedAt { get; protected set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600024D RID: 589 RVA: 0x000033DE File Offset: 0x000015DE
		// (set) Token: 0x0600024E RID: 590 RVA: 0x000033E6 File Offset: 0x000015E6
		[JsonProperty(PropertyName = "locked_at")]
		public string LockedAt { get; protected set; }
	}
}
