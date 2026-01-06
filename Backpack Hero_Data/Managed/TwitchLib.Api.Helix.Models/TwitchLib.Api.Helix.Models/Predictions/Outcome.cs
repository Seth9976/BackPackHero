using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Predictions
{
	// Token: 0x02000045 RID: 69
	public class Outcome
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600022A RID: 554 RVA: 0x000032B5 File Offset: 0x000014B5
		// (set) Token: 0x0600022B RID: 555 RVA: 0x000032BD File Offset: 0x000014BD
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600022C RID: 556 RVA: 0x000032C6 File Offset: 0x000014C6
		// (set) Token: 0x0600022D RID: 557 RVA: 0x000032CE File Offset: 0x000014CE
		[JsonProperty(PropertyName = "title")]
		public string Title { get; protected set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600022E RID: 558 RVA: 0x000032D7 File Offset: 0x000014D7
		// (set) Token: 0x0600022F RID: 559 RVA: 0x000032DF File Offset: 0x000014DF
		[JsonProperty(PropertyName = "users")]
		public int ChannelPoints { get; protected set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000230 RID: 560 RVA: 0x000032E8 File Offset: 0x000014E8
		// (set) Token: 0x06000231 RID: 561 RVA: 0x000032F0 File Offset: 0x000014F0
		[JsonProperty(PropertyName = "channel_points")]
		public int ChannelPointsVotes { get; protected set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000232 RID: 562 RVA: 0x000032F9 File Offset: 0x000014F9
		// (set) Token: 0x06000233 RID: 563 RVA: 0x00003301 File Offset: 0x00001501
		[JsonProperty(PropertyName = "top_predictors")]
		public TopPredictor[] TopPredictors { get; protected set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000330A File Offset: 0x0000150A
		// (set) Token: 0x06000235 RID: 565 RVA: 0x00003312 File Offset: 0x00001512
		[JsonProperty(PropertyName = "color")]
		public string Color { get; protected set; }
	}
}
