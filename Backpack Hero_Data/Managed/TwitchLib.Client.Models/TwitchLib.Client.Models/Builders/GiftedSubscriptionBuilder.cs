using System;
using System.Collections.Generic;
using TwitchLib.Client.Enums;

namespace TwitchLib.Client.Models.Builders
{
	// Token: 0x02000034 RID: 52
	public sealed class GiftedSubscriptionBuilder : IBuilder<GiftedSubscription>, IFromIrcMessageBuilder<GiftedSubscription>
	{
		// Token: 0x060001C7 RID: 455 RVA: 0x000080F9 File Offset: 0x000062F9
		private GiftedSubscriptionBuilder()
		{
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00008117 File Offset: 0x00006317
		public GiftedSubscriptionBuilder WithBadges(params KeyValuePair<string, string>[] badges)
		{
			this._badges.AddRange(badges);
			return this;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00008126 File Offset: 0x00006326
		public GiftedSubscriptionBuilder WithBadgeInfos(params KeyValuePair<string, string>[] badgeInfos)
		{
			this._badgeInfo.AddRange(badgeInfos);
			return this;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00008135 File Offset: 0x00006335
		public GiftedSubscriptionBuilder WithColor(string color)
		{
			this._color = color;
			return this;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000813F File Offset: 0x0000633F
		public GiftedSubscriptionBuilder WithDisplayName(string displayName)
		{
			this._displayName = displayName;
			return this;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00008149 File Offset: 0x00006349
		public GiftedSubscriptionBuilder WithEmotes(string emotes)
		{
			this._emotes = emotes;
			return this;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00008153 File Offset: 0x00006353
		public GiftedSubscriptionBuilder WithId(string id)
		{
			this._id = id;
			return this;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000815D File Offset: 0x0000635D
		public GiftedSubscriptionBuilder WithIsModerator(bool isModerator)
		{
			this._isModerator = isModerator;
			return this;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00008167 File Offset: 0x00006367
		public GiftedSubscriptionBuilder WithIsSubscriber(bool isSubscriber)
		{
			this._isSubscriber = isSubscriber;
			return this;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00008171 File Offset: 0x00006371
		public GiftedSubscriptionBuilder WithIsTurbo(bool isTurbo)
		{
			this._isTurbo = isTurbo;
			return this;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000817B File Offset: 0x0000637B
		public GiftedSubscriptionBuilder WithLogin(string login)
		{
			this._login = login;
			return this;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00008185 File Offset: 0x00006385
		public GiftedSubscriptionBuilder WithMessageId(string msgId)
		{
			this._msgId = msgId;
			return this;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000818F File Offset: 0x0000638F
		public GiftedSubscriptionBuilder WithMsgParamCumulativeMonths(string msgParamMonths)
		{
			this._msgParamMonths = msgParamMonths;
			return this;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00008199 File Offset: 0x00006399
		public GiftedSubscriptionBuilder WithMsgParamRecipientDisplayName(string msgParamRecipientDisplayName)
		{
			this._msgParamRecipientDisplayName = msgParamRecipientDisplayName;
			return this;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000081A3 File Offset: 0x000063A3
		public GiftedSubscriptionBuilder WithMsgParamRecipientId(string msgParamRecipientId)
		{
			this._msgParamRecipientId = msgParamRecipientId;
			return this;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000081AD File Offset: 0x000063AD
		public GiftedSubscriptionBuilder WithMsgParamRecipientUserName(string msgParamRecipientUserName)
		{
			this._msgParamRecipientUserName = msgParamRecipientUserName;
			return this;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000081B7 File Offset: 0x000063B7
		public GiftedSubscriptionBuilder WithMsgParamSubPlanName(string msgParamSubPlanName)
		{
			this._msgParamSubPlanName = msgParamSubPlanName;
			return this;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000081C1 File Offset: 0x000063C1
		public GiftedSubscriptionBuilder WithMsgParamMultiMonthGiftDuration(string msgParamMultiMonthGiftDuration)
		{
			this._msgParamMultiMonthGiftDuration = msgParamMultiMonthGiftDuration;
			return this;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000081CB File Offset: 0x000063CB
		public GiftedSubscriptionBuilder WithMsgParamSubPlan(SubscriptionPlan msgParamSubPlan)
		{
			this._msgParamSubPlan = msgParamSubPlan;
			return this;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000081D5 File Offset: 0x000063D5
		public GiftedSubscriptionBuilder WithRoomId(string roomId)
		{
			this._roomId = roomId;
			return this;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000081DF File Offset: 0x000063DF
		public GiftedSubscriptionBuilder WithSystemMsg(string systemMsg)
		{
			this._systemMsg = systemMsg;
			return this;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000081E9 File Offset: 0x000063E9
		public GiftedSubscriptionBuilder WithSystemMsgParsed(string systemMsgParsed)
		{
			this._systemMsgParsed = systemMsgParsed;
			return this;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000081F3 File Offset: 0x000063F3
		public GiftedSubscriptionBuilder WithTmiSentTs(string tmiSentTs)
		{
			this._tmiSentTs = tmiSentTs;
			return this;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000081FD File Offset: 0x000063FD
		public GiftedSubscriptionBuilder WithUserId(string userId)
		{
			this._userId = userId;
			return this;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00008207 File Offset: 0x00006407
		public GiftedSubscriptionBuilder WithUserType(UserType userType)
		{
			this._userType = userType;
			return this;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00008211 File Offset: 0x00006411
		public static GiftedSubscriptionBuilder Create()
		{
			return new GiftedSubscriptionBuilder();
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00008218 File Offset: 0x00006418
		public GiftedSubscription Build()
		{
			return new GiftedSubscription(this._badges, this._badgeInfo, this._color, this._displayName, this._emotes, this._id, this._login, this._isModerator, this._msgId, this._msgParamMonths, this._msgParamRecipientDisplayName, this._msgParamRecipientId, this._msgParamRecipientUserName, this._msgParamSubPlanName, this._msgParamMultiMonthGiftDuration, this._msgParamSubPlan, this._roomId, this._isSubscriber, this._systemMsg, this._systemMsgParsed, this._tmiSentTs, this._isTurbo, this._userType, this._userId);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000082BA File Offset: 0x000064BA
		public GiftedSubscription BuildFromIrcMessage(FromIrcMessageBuilderDataObject fromIrcMessageBuilderDataObject)
		{
			return new GiftedSubscription(fromIrcMessageBuilderDataObject.Message);
		}

		// Token: 0x040001D4 RID: 468
		private readonly List<KeyValuePair<string, string>> _badges = new List<KeyValuePair<string, string>>();

		// Token: 0x040001D5 RID: 469
		private readonly List<KeyValuePair<string, string>> _badgeInfo = new List<KeyValuePair<string, string>>();

		// Token: 0x040001D6 RID: 470
		private string _color;

		// Token: 0x040001D7 RID: 471
		private string _displayName;

		// Token: 0x040001D8 RID: 472
		private string _emotes;

		// Token: 0x040001D9 RID: 473
		private string _id;

		// Token: 0x040001DA RID: 474
		private bool _isModerator;

		// Token: 0x040001DB RID: 475
		private bool _isSubscriber;

		// Token: 0x040001DC RID: 476
		private bool _isTurbo;

		// Token: 0x040001DD RID: 477
		private string _login;

		// Token: 0x040001DE RID: 478
		private string _msgId;

		// Token: 0x040001DF RID: 479
		private string _msgParamMonths;

		// Token: 0x040001E0 RID: 480
		private string _msgParamRecipientDisplayName;

		// Token: 0x040001E1 RID: 481
		private string _msgParamRecipientId;

		// Token: 0x040001E2 RID: 482
		private string _msgParamRecipientUserName;

		// Token: 0x040001E3 RID: 483
		private string _msgParamSubPlanName;

		// Token: 0x040001E4 RID: 484
		private string _msgParamMultiMonthGiftDuration;

		// Token: 0x040001E5 RID: 485
		private SubscriptionPlan _msgParamSubPlan;

		// Token: 0x040001E6 RID: 486
		private string _roomId;

		// Token: 0x040001E7 RID: 487
		private string _systemMsg;

		// Token: 0x040001E8 RID: 488
		private string _systemMsgParsed;

		// Token: 0x040001E9 RID: 489
		private string _tmiSentTs;

		// Token: 0x040001EA RID: 490
		private string _userId;

		// Token: 0x040001EB RID: 491
		private UserType _userType;
	}
}
