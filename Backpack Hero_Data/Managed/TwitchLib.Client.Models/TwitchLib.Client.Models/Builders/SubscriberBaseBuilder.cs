using System;
using System.Collections.Generic;
using System.Drawing;
using TwitchLib.Client.Enums;

namespace TwitchLib.Client.Models.Builders
{
	// Token: 0x0200003F RID: 63
	public class SubscriberBaseBuilder : IBuilder<SubscriberBase>
	{
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600022D RID: 557 RVA: 0x000087E0 File Offset: 0x000069E0
		protected List<KeyValuePair<string, string>> Badges { get; } = new List<KeyValuePair<string, string>>();

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600022E RID: 558 RVA: 0x000087E8 File Offset: 0x000069E8
		public List<KeyValuePair<string, string>> BadgeInfo { get; } = new List<KeyValuePair<string, string>>();

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600022F RID: 559 RVA: 0x000087F0 File Offset: 0x000069F0
		// (set) Token: 0x06000230 RID: 560 RVA: 0x000087F8 File Offset: 0x000069F8
		protected string ColorHex { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00008801 File Offset: 0x00006A01
		// (set) Token: 0x06000232 RID: 562 RVA: 0x00008809 File Offset: 0x00006A09
		protected Color Color { get; set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00008812 File Offset: 0x00006A12
		// (set) Token: 0x06000234 RID: 564 RVA: 0x0000881A File Offset: 0x00006A1A
		protected string DisplayName { get; set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00008823 File Offset: 0x00006A23
		// (set) Token: 0x06000236 RID: 566 RVA: 0x0000882B File Offset: 0x00006A2B
		protected string EmoteSet { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00008834 File Offset: 0x00006A34
		// (set) Token: 0x06000238 RID: 568 RVA: 0x0000883C File Offset: 0x00006A3C
		protected string Id { get; set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00008845 File Offset: 0x00006A45
		// (set) Token: 0x0600023A RID: 570 RVA: 0x0000884D File Offset: 0x00006A4D
		protected bool IsModerator { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00008856 File Offset: 0x00006A56
		// (set) Token: 0x0600023C RID: 572 RVA: 0x0000885E File Offset: 0x00006A5E
		protected bool IsPartner { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00008867 File Offset: 0x00006A67
		// (set) Token: 0x0600023E RID: 574 RVA: 0x0000886F File Offset: 0x00006A6F
		protected bool IsSubscriber { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00008878 File Offset: 0x00006A78
		// (set) Token: 0x06000240 RID: 576 RVA: 0x00008880 File Offset: 0x00006A80
		protected bool IsTurbo { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00008889 File Offset: 0x00006A89
		// (set) Token: 0x06000242 RID: 578 RVA: 0x00008891 File Offset: 0x00006A91
		protected string Login { get; set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000889A File Offset: 0x00006A9A
		// (set) Token: 0x06000244 RID: 580 RVA: 0x000088A2 File Offset: 0x00006AA2
		protected string RawIrc { get; set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000245 RID: 581 RVA: 0x000088AB File Offset: 0x00006AAB
		// (set) Token: 0x06000246 RID: 582 RVA: 0x000088B3 File Offset: 0x00006AB3
		protected string ResubMessage { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000247 RID: 583 RVA: 0x000088BC File Offset: 0x00006ABC
		// (set) Token: 0x06000248 RID: 584 RVA: 0x000088C4 File Offset: 0x00006AC4
		protected string RoomId { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000249 RID: 585 RVA: 0x000088CD File Offset: 0x00006ACD
		// (set) Token: 0x0600024A RID: 586 RVA: 0x000088D5 File Offset: 0x00006AD5
		protected SubscriptionPlan SubscriptionPlan { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600024B RID: 587 RVA: 0x000088DE File Offset: 0x00006ADE
		// (set) Token: 0x0600024C RID: 588 RVA: 0x000088E6 File Offset: 0x00006AE6
		protected string SubscriptionPlanName { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600024D RID: 589 RVA: 0x000088EF File Offset: 0x00006AEF
		// (set) Token: 0x0600024E RID: 590 RVA: 0x000088F7 File Offset: 0x00006AF7
		protected string SystemMessage { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00008900 File Offset: 0x00006B00
		// (set) Token: 0x06000250 RID: 592 RVA: 0x00008908 File Offset: 0x00006B08
		protected string ParsedSystemMessage { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00008911 File Offset: 0x00006B11
		// (set) Token: 0x06000252 RID: 594 RVA: 0x00008919 File Offset: 0x00006B19
		protected string TmiSentTs { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00008922 File Offset: 0x00006B22
		// (set) Token: 0x06000254 RID: 596 RVA: 0x0000892A File Offset: 0x00006B2A
		protected string UserId { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00008933 File Offset: 0x00006B33
		// (set) Token: 0x06000256 RID: 598 RVA: 0x0000893B File Offset: 0x00006B3B
		protected UserType UserType { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00008944 File Offset: 0x00006B44
		// (set) Token: 0x06000258 RID: 600 RVA: 0x0000894C File Offset: 0x00006B4C
		protected string Channel { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00008955 File Offset: 0x00006B55
		// (set) Token: 0x0600025A RID: 602 RVA: 0x0000895D File Offset: 0x00006B5D
		protected string MessageId { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00008966 File Offset: 0x00006B66
		// (set) Token: 0x0600025C RID: 604 RVA: 0x0000896E File Offset: 0x00006B6E
		protected string MsgParamCumulativeMonths { get; set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00008977 File Offset: 0x00006B77
		// (set) Token: 0x0600025E RID: 606 RVA: 0x0000897F File Offset: 0x00006B7F
		protected string MsgParamStreakMonths { get; set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00008988 File Offset: 0x00006B88
		// (set) Token: 0x06000260 RID: 608 RVA: 0x00008990 File Offset: 0x00006B90
		protected bool MsgParamShouldShareStreak { get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00008999 File Offset: 0x00006B99
		// (set) Token: 0x06000262 RID: 610 RVA: 0x000089A1 File Offset: 0x00006BA1
		protected int Months { get; set; }

		// Token: 0x06000263 RID: 611 RVA: 0x000089AA File Offset: 0x00006BAA
		protected SubscriberBaseBuilder()
		{
		}

		// Token: 0x06000264 RID: 612 RVA: 0x000089C8 File Offset: 0x00006BC8
		public static SubscriberBaseBuilder Create()
		{
			return new SubscriberBaseBuilder();
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000089CF File Offset: 0x00006BCF
		public SubscriberBaseBuilder WithBadges(params KeyValuePair<string, string>[] badges)
		{
			this.Badges.AddRange(badges);
			return this;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000089DE File Offset: 0x00006BDE
		public SubscriberBaseBuilder WithBadgeInfos(params KeyValuePair<string, string>[] badgeInfos)
		{
			this.BadgeInfo.AddRange(badgeInfos);
			return this;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000089ED File Offset: 0x00006BED
		public SubscriberBaseBuilder WithColorHex(string colorHex)
		{
			this.ColorHex = colorHex;
			return this;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000089F7 File Offset: 0x00006BF7
		public SubscriberBaseBuilder WithColor(Color color)
		{
			this.Color = color;
			return this;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00008A01 File Offset: 0x00006C01
		public SubscriberBaseBuilder WithDisplayName(string displayName)
		{
			this.DisplayName = displayName;
			return this;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00008A0B File Offset: 0x00006C0B
		public SubscriberBaseBuilder WithEmoteSet(string emoteSet)
		{
			this.EmoteSet = emoteSet;
			return this;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00008A15 File Offset: 0x00006C15
		public SubscriberBaseBuilder WithId(string id)
		{
			this.Id = id;
			return this;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00008A1F File Offset: 0x00006C1F
		public SubscriberBaseBuilder WithIsModerator(bool isModerator)
		{
			this.IsModerator = isModerator;
			return this;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00008A29 File Offset: 0x00006C29
		public SubscriberBaseBuilder WithIsPartner(bool isPartner)
		{
			this.IsPartner = isPartner;
			return this;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00008A33 File Offset: 0x00006C33
		public SubscriberBaseBuilder WithIsSubscriber(bool isSubscriber)
		{
			this.IsSubscriber = isSubscriber;
			return this;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00008A3D File Offset: 0x00006C3D
		public SubscriberBaseBuilder WithIsTurbo(bool isTurbo)
		{
			this.IsTurbo = isTurbo;
			return this;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00008A47 File Offset: 0x00006C47
		public SubscriberBaseBuilder WithLogin(string login)
		{
			this.Login = login;
			return this;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00008A51 File Offset: 0x00006C51
		public SubscriberBaseBuilder WithRawIrc(string rawIrc)
		{
			this.RawIrc = rawIrc;
			return this;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00008A5B File Offset: 0x00006C5B
		public SubscriberBaseBuilder WithResubMessage(string resubMessage)
		{
			this.ResubMessage = resubMessage;
			return this;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00008A65 File Offset: 0x00006C65
		public SubscriberBaseBuilder WithRoomId(string roomId)
		{
			this.RoomId = roomId;
			return this;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00008A6F File Offset: 0x00006C6F
		public SubscriberBaseBuilder WithSubscribtionPlan(SubscriptionPlan subscriptionPlan)
		{
			this.SubscriptionPlan = subscriptionPlan;
			return this;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00008A79 File Offset: 0x00006C79
		public SubscriberBaseBuilder WithSubscriptionPlanName(string subscriptionPlanName)
		{
			this.SubscriptionPlanName = subscriptionPlanName;
			return this;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00008A83 File Offset: 0x00006C83
		public SubscriberBaseBuilder WithSystemMessage(string systemMessage)
		{
			this.SystemMessage = systemMessage;
			return this;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00008A8D File Offset: 0x00006C8D
		public SubscriberBaseBuilder WithParsedSystemMessage(string parsedSystemMessage)
		{
			this.ParsedSystemMessage = parsedSystemMessage;
			return this;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00008A97 File Offset: 0x00006C97
		public SubscriberBaseBuilder WithTmiSentTs(string tmiSentTs)
		{
			this.TmiSentTs = tmiSentTs;
			return this;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00008AA1 File Offset: 0x00006CA1
		public SubscriberBaseBuilder WithUserType(UserType userType)
		{
			this.UserType = userType;
			return this;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00008AAB File Offset: 0x00006CAB
		public SubscriberBaseBuilder WithUserId(string userId)
		{
			this.UserId = userId;
			return this;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00008AB5 File Offset: 0x00006CB5
		public SubscriberBaseBuilder WithMonths(int months)
		{
			this.Months = months;
			return this;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00008ABF File Offset: 0x00006CBF
		public SubscriberBaseBuilder WithMessageId(string messageId)
		{
			this.MessageId = messageId;
			return this;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00008AC9 File Offset: 0x00006CC9
		public SubscriberBaseBuilder WithMsgParamCumulativeMonths(string msgParamCumulativeMonths)
		{
			this.MsgParamCumulativeMonths = msgParamCumulativeMonths;
			return this;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00008AD3 File Offset: 0x00006CD3
		public SubscriberBaseBuilder WithMsgParamStreakMonths(string msgParamStreakMonths)
		{
			this.MsgParamStreakMonths = msgParamStreakMonths;
			return this;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00008ADD File Offset: 0x00006CDD
		public SubscriberBaseBuilder WithMsgParamShouldShareStreak(bool msgParamShouldShareStreak)
		{
			this.MsgParamShouldShareStreak = msgParamShouldShareStreak;
			return this;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00008AE7 File Offset: 0x00006CE7
		public SubscriberBaseBuilder WithChannel(string channel)
		{
			this.Channel = channel;
			return this;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00008AF4 File Offset: 0x00006CF4
		public virtual SubscriberBase Build()
		{
			return new SubscriberBase(this.Badges, this.BadgeInfo, this.ColorHex, this.Color, this.DisplayName, this.EmoteSet, this.Id, this.Login, this.SystemMessage, this.MessageId, this.MsgParamCumulativeMonths, this.MsgParamStreakMonths, this.MsgParamShouldShareStreak, this.ParsedSystemMessage, this.ResubMessage, this.SubscriptionPlan, this.SubscriptionPlanName, this.RoomId, this.UserId, this.IsModerator, this.IsTurbo, this.IsSubscriber, this.IsPartner, this.TmiSentTs, this.UserType, this.RawIrc, this.Channel, this.Months);
		}
	}
}
