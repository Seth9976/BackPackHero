using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Predictions
{
	// Token: 0x02000047 RID: 71
	public class TopPredictor
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000250 RID: 592 RVA: 0x000033F7 File Offset: 0x000015F7
		// (set) Token: 0x06000251 RID: 593 RVA: 0x000033FF File Offset: 0x000015FF
		[JsonProperty(PropertyName = "user_id")]
		public string UserId { get; protected set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00003408 File Offset: 0x00001608
		// (set) Token: 0x06000253 RID: 595 RVA: 0x00003410 File Offset: 0x00001610
		[JsonProperty(PropertyName = "user_name")]
		public string UserName { get; protected set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00003419 File Offset: 0x00001619
		// (set) Token: 0x06000255 RID: 597 RVA: 0x00003421 File Offset: 0x00001621
		[JsonProperty(PropertyName = "user_login")]
		public string UserLogin { get; protected set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000342A File Offset: 0x0000162A
		// (set) Token: 0x06000257 RID: 599 RVA: 0x00003432 File Offset: 0x00001632
		[JsonProperty(PropertyName = "channel_points_used")]
		public int ChannelPointsUsed { get; protected set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000343B File Offset: 0x0000163B
		// (set) Token: 0x06000259 RID: 601 RVA: 0x00003443 File Offset: 0x00001643
		[JsonProperty(PropertyName = "channel_points_won")]
		public int ChannelPointsWon { get; protected set; }
	}
}
