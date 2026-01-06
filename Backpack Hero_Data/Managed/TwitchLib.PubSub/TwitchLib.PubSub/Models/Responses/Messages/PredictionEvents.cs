using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TwitchLib.PubSub.Enums;
using TwitchLib.PubSub.Extensions;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
	// Token: 0x02000015 RID: 21
	public class PredictionEvents : MessageData
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000063F8 File Offset: 0x000045F8
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00006400 File Offset: 0x00004600
		public PredictionType Type { get; protected set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00006409 File Offset: 0x00004609
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00006411 File Offset: 0x00004611
		public Guid Id { get; protected set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000107 RID: 263 RVA: 0x0000641A File Offset: 0x0000461A
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00006422 File Offset: 0x00004622
		public string ChannelId { get; protected set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000109 RID: 265 RVA: 0x0000642B File Offset: 0x0000462B
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00006433 File Offset: 0x00004633
		public DateTime? CreatedAt { get; protected set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000643C File Offset: 0x0000463C
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00006444 File Offset: 0x00004644
		public DateTime? LockedAt { get; protected set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000644D File Offset: 0x0000464D
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00006455 File Offset: 0x00004655
		public DateTime? EndedAt { get; protected set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600010F RID: 271 RVA: 0x0000645E File Offset: 0x0000465E
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00006466 File Offset: 0x00004666
		public ICollection<Outcome> Outcomes { get; protected set; } = new List<Outcome>();

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000111 RID: 273 RVA: 0x0000646F File Offset: 0x0000466F
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00006477 File Offset: 0x00004677
		public PredictionStatus Status { get; protected set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00006480 File Offset: 0x00004680
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00006488 File Offset: 0x00004688
		public string Title { get; protected set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00006491 File Offset: 0x00004691
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00006499 File Offset: 0x00004699
		public Guid? WinningOutcomeId { get; protected set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000117 RID: 279 RVA: 0x000064A2 File Offset: 0x000046A2
		// (set) Token: 0x06000118 RID: 280 RVA: 0x000064AA File Offset: 0x000046AA
		public int PredictionTime { get; protected set; }

		// Token: 0x06000119 RID: 281 RVA: 0x000064B4 File Offset: 0x000046B4
		public PredictionEvents(string jsonStr)
		{
			JObject jobject = JObject.Parse(jsonStr);
			this.Type = (PredictionType)Enum.Parse(typeof(PredictionType), jobject.SelectToken("type").ToString().Replace("-", ""), true);
			JToken jtoken = jobject.SelectToken("data.event");
			this.Id = Guid.Parse(jtoken.SelectToken("id").ToString());
			this.ChannelId = jtoken.SelectToken("channel_id").ToString();
			this.CreatedAt = (jtoken.SelectToken("created_at").IsEmpty() ? default(DateTime?) : new DateTime?(DateTime.Parse(jtoken.SelectToken("created_at").ToString())));
			this.EndedAt = (jtoken.SelectToken("ended_at").IsEmpty() ? default(DateTime?) : new DateTime?(DateTime.Parse(jtoken.SelectToken("ended_at").ToString())));
			this.LockedAt = (jtoken.SelectToken("locked_at").IsEmpty() ? default(DateTime?) : new DateTime?(DateTime.Parse(jtoken.SelectToken("locked_at").ToString())));
			this.Status = (PredictionStatus)Enum.Parse(typeof(PredictionStatus), jtoken.SelectToken("status").ToString().Replace("_", ""), true);
			this.Title = jtoken.SelectToken("title").ToString();
			this.WinningOutcomeId = (jtoken.SelectToken("winning_outcome_id").IsEmpty() ? default(Guid?) : new Guid?(Guid.Parse(jtoken.SelectToken("winning_outcome_id").ToString())));
			this.PredictionTime = int.Parse(jtoken.SelectToken("prediction_window_seconds").ToString());
			foreach (JToken jtoken2 in jtoken.SelectToken("outcomes").Children())
			{
				Outcome outcome = new Outcome
				{
					Id = Guid.Parse(jtoken2.SelectToken("id").ToString()),
					Color = jtoken2.SelectToken("color").ToString(),
					Title = jtoken2.SelectToken("title").ToString(),
					TotalPoints = long.Parse(jtoken2.SelectToken("total_points").ToString()),
					TotalUsers = long.Parse(jtoken2.SelectToken("total_users").ToString())
				};
				foreach (JToken jtoken3 in jtoken2.SelectToken("top_predictors").Children())
				{
					outcome.TopPredictors.Add(new Outcome.Predictor
					{
						DisplayName = jtoken3.SelectToken("user_display_name").ToString(),
						Points = (long)int.Parse(jtoken3.SelectToken("points").ToString()),
						UserId = jtoken3.SelectToken("user_id").ToString()
					});
				}
				this.Outcomes.Add(outcome);
			}
		}
	}
}
