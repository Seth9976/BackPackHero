using System;
using Newtonsoft.Json.Linq;
using TwitchLib.PubSub.Common;
using TwitchLib.PubSub.Enums;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
	// Token: 0x0200000F RID: 15
	public class ChannelSubscription : MessageData
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000058C0 File Offset: 0x00003AC0
		public string Username { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000058C8 File Offset: 0x00003AC8
		public string DisplayName { get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000058D0 File Offset: 0x00003AD0
		public string RecipientName { get; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000058D8 File Offset: 0x00003AD8
		public string RecipientDisplayName { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000058E0 File Offset: 0x00003AE0
		public string ChannelName { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000058E8 File Offset: 0x00003AE8
		public string UserId { get; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000058F0 File Offset: 0x00003AF0
		public string ChannelId { get; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000058F8 File Offset: 0x00003AF8
		public string RecipientId { get; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00005900 File Offset: 0x00003B00
		public DateTime Time { get; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00005908 File Offset: 0x00003B08
		public SubscriptionPlan SubscriptionPlan { get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00005910 File Offset: 0x00003B10
		public string SubscriptionPlanName { get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00005918 File Offset: 0x00003B18
		public int? Months { get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00005920 File Offset: 0x00003B20
		public int? CumulativeMonths { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00005928 File Offset: 0x00003B28
		public int? StreakMonths { get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00005930 File Offset: 0x00003B30
		public string Context { get; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00005938 File Offset: 0x00003B38
		public SubMessage SubMessage { get; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00005940 File Offset: 0x00003B40
		public bool? IsGift { get; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00005948 File Offset: 0x00003B48
		public int? MultiMonthDuration { get; }

		// Token: 0x060000D4 RID: 212 RVA: 0x00005950 File Offset: 0x00003B50
		public ChannelSubscription(string jsonStr)
		{
			JObject jobject = JObject.Parse(jsonStr);
			JToken jtoken = jobject.SelectToken("user_name");
			this.Username = ((jtoken != null) ? jtoken.ToString() : null);
			JToken jtoken2 = jobject.SelectToken("display_name");
			this.DisplayName = ((jtoken2 != null) ? jtoken2.ToString() : null);
			JToken jtoken3 = jobject.SelectToken("recipient_user_name");
			this.RecipientName = ((jtoken3 != null) ? jtoken3.ToString() : null);
			JToken jtoken4 = jobject.SelectToken("recipient_display_name");
			this.RecipientDisplayName = ((jtoken4 != null) ? jtoken4.ToString() : null);
			JToken jtoken5 = jobject.SelectToken("channel_name");
			this.ChannelName = ((jtoken5 != null) ? jtoken5.ToString() : null);
			JToken jtoken6 = jobject.SelectToken("user_id");
			this.UserId = ((jtoken6 != null) ? jtoken6.ToString() : null);
			JToken jtoken7 = jobject.SelectToken("recipient_id");
			this.RecipientId = ((jtoken7 != null) ? jtoken7.ToString() : null);
			JToken jtoken8 = jobject.SelectToken("channel_id");
			this.ChannelId = ((jtoken8 != null) ? jtoken8.ToString() : null);
			JToken jtoken9 = jobject.SelectToken("time");
			this.Time = Helpers.DateTimeStringToObject((jtoken9 != null) ? jtoken9.ToString() : null);
			string text = jobject.SelectToken("sub_plan").ToString().ToLower();
			if (text != null)
			{
				if (!(text == "prime"))
				{
					if (!(text == "1000"))
					{
						if (!(text == "2000"))
						{
							if (!(text == "3000"))
							{
								goto IL_0190;
							}
							this.SubscriptionPlan = SubscriptionPlan.Tier3;
						}
						else
						{
							this.SubscriptionPlan = SubscriptionPlan.Tier2;
						}
					}
					else
					{
						this.SubscriptionPlan = SubscriptionPlan.Tier1;
					}
				}
				else
				{
					this.SubscriptionPlan = SubscriptionPlan.Prime;
				}
				JToken jtoken10 = jobject.SelectToken("sub_plan_name");
				this.SubscriptionPlanName = ((jtoken10 != null) ? jtoken10.ToString() : null);
				this.SubMessage = new SubMessage(jobject.SelectToken("sub_message"));
				JToken jtoken11 = jobject.SelectToken("is_gift");
				string text2 = ((jtoken11 != null) ? jtoken11.ToString() : null);
				if (text2 != null)
				{
					this.IsGift = new bool?(Convert.ToBoolean(text2.ToString()));
				}
				JToken jtoken12 = jobject.SelectToken("multi_month_duration");
				string text3 = ((jtoken12 != null) ? jtoken12.ToString() : null);
				if (text3 != null)
				{
					this.MultiMonthDuration = new int?(int.Parse(text3.ToString()));
				}
				JToken jtoken13 = jobject.SelectToken("context");
				this.Context = ((jtoken13 != null) ? jtoken13.ToString() : null);
				JToken jtoken14 = jobject.SelectToken("months");
				if (jtoken14 != null)
				{
					this.Months = new int?(int.Parse(jtoken14.ToString()));
				}
				JToken jtoken15 = jobject.SelectToken("cumulative_months");
				if (jtoken15 != null)
				{
					this.CumulativeMonths = new int?(int.Parse(jtoken15.ToString()));
				}
				JToken jtoken16 = jobject.SelectToken("streak_months");
				if (jtoken16 != null)
				{
					this.StreakMonths = new int?(int.Parse(jtoken16.ToString()));
				}
				return;
			}
			IL_0190:
			throw new ArgumentOutOfRangeException();
		}
	}
}
