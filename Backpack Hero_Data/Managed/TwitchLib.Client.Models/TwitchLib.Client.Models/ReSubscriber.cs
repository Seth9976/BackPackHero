using System;
using System.Collections.Generic;
using System.Drawing;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Models.Internal;

namespace TwitchLib.Client.Models
{
	// Token: 0x02000019 RID: 25
	public class ReSubscriber : SubscriberBase
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00005A9C File Offset: 0x00003C9C
		public int Months
		{
			get
			{
				return this.monthsInternal;
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005AA4 File Offset: 0x00003CA4
		public ReSubscriber(IrcMessage ircMessage)
			: base(ircMessage)
		{
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005AB0 File Offset: 0x00003CB0
		public ReSubscriber(List<KeyValuePair<string, string>> badges, List<KeyValuePair<string, string>> badgeInfo, string colorHex, Color color, string displayName, string emoteSet, string id, string login, string systemMessage, string msgId, string msgParamCumulativeMonths, string msgParamStreakMonths, bool msgParamShouldShareStreak, string systemMessageParsed, string resubMessage, SubscriptionPlan subscriptionPlan, string subscriptionPlanName, string roomId, string userId, bool isModerator, bool isTurbo, bool isSubscriber, bool isPartner, string tmiSentTs, UserType userType, string rawIrc, string channel, int months = 0)
			: base(badges, badgeInfo, colorHex, color, displayName, emoteSet, id, login, systemMessage, msgId, msgParamCumulativeMonths, msgParamStreakMonths, msgParamShouldShareStreak, systemMessageParsed, resubMessage, subscriptionPlan, subscriptionPlanName, roomId, userId, isModerator, isTurbo, isSubscriber, isPartner, tmiSentTs, userType, rawIrc, channel, months)
		{
		}
	}
}
