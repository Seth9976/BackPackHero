using System;
using System.Collections.Generic;
using TwitchLib.Client.Enums;

namespace TwitchLib.Client.Models.Builders
{
	// Token: 0x0200003B RID: 59
	public sealed class RaidNotificationBuilder : IBuilder<RaidNotification>, IFromIrcMessageBuilder<RaidNotification>
	{
		// Token: 0x06000202 RID: 514 RVA: 0x00008492 File Offset: 0x00006692
		public RaidNotificationBuilder WithBadges(params KeyValuePair<string, string>[] badges)
		{
			this._badges.AddRange(badges);
			return this;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000084A1 File Offset: 0x000066A1
		public RaidNotificationBuilder WithBadgeInfos(params KeyValuePair<string, string>[] badgeInfos)
		{
			this._badgeInfo.AddRange(badgeInfos);
			return this;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000084B0 File Offset: 0x000066B0
		public RaidNotificationBuilder WithColor(string color)
		{
			this._color = color;
			return this;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000084BA File Offset: 0x000066BA
		public RaidNotificationBuilder WithDisplayName(string displayName)
		{
			this._displayName = displayName;
			return this;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000084C4 File Offset: 0x000066C4
		public RaidNotificationBuilder WithEmotes(string emotes)
		{
			this._emotes = emotes;
			return this;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000084CE File Offset: 0x000066CE
		public RaidNotificationBuilder WithId(string id)
		{
			this._id = id;
			return this;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000084D8 File Offset: 0x000066D8
		public RaidNotificationBuilder WithIsModerator(bool isModerator)
		{
			this._isModerator = isModerator;
			return this;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x000084E2 File Offset: 0x000066E2
		public RaidNotificationBuilder WithIsSubscriber(bool isSubscriber)
		{
			this._isSubscriber = isSubscriber;
			return this;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x000084EC File Offset: 0x000066EC
		public RaidNotificationBuilder WithIsTurbo(bool isTurbo)
		{
			this._isTurbo = isTurbo;
			return this;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000084F6 File Offset: 0x000066F6
		public RaidNotificationBuilder WithLogin(string login)
		{
			this._login = login;
			return this;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00008500 File Offset: 0x00006700
		public RaidNotificationBuilder WithMessageId(string msgId)
		{
			this._msgId = msgId;
			return this;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000850A File Offset: 0x0000670A
		public RaidNotificationBuilder WithMsgParamDisplayName(string msgParamDisplayName)
		{
			this._msgParamDisplayName = msgParamDisplayName;
			return this;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00008514 File Offset: 0x00006714
		public RaidNotificationBuilder WithMsgParamLogin(string msgParamLogin)
		{
			this._msgParamLogin = msgParamLogin;
			return this;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000851E File Offset: 0x0000671E
		public RaidNotificationBuilder WithMsgParamViewerCount(string msgParamViewerCount)
		{
			this._msgParamViewerCount = msgParamViewerCount;
			return this;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00008528 File Offset: 0x00006728
		public RaidNotificationBuilder WithRoomId(string roomId)
		{
			this._roomId = roomId;
			return this;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00008532 File Offset: 0x00006732
		public RaidNotificationBuilder WithSystemMsg(string systemMsg)
		{
			this._systemMsg = systemMsg;
			return this;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000853C File Offset: 0x0000673C
		public RaidNotificationBuilder WithSystemMsgParsed(string systemMsgParsed)
		{
			this._systemMsgParsed = systemMsgParsed;
			return this;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00008546 File Offset: 0x00006746
		public RaidNotificationBuilder WithTmiSentTs(string tmiSentTs)
		{
			this._tmiSentTs = tmiSentTs;
			return this;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00008550 File Offset: 0x00006750
		public RaidNotificationBuilder WithUserId(string userId)
		{
			this._userId = userId;
			return this;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000855A File Offset: 0x0000675A
		public RaidNotificationBuilder WithUserType(UserType userType)
		{
			this._userType = userType;
			return this;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00008564 File Offset: 0x00006764
		private RaidNotificationBuilder()
		{
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00008584 File Offset: 0x00006784
		public RaidNotification Build()
		{
			return new RaidNotification(this._badges, this._badgeInfo, this._color, this._displayName, this._emotes, this._id, this._login, this._isModerator, this._msgId, this._msgParamDisplayName, this._msgParamLogin, this._msgParamViewerCount, this._roomId, this._isSubscriber, this._systemMsg, this._systemMsgParsed, this._tmiSentTs, this._isTurbo, this._userType, this._userId);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000860E File Offset: 0x0000680E
		public RaidNotification BuildFromIrcMessage(FromIrcMessageBuilderDataObject fromIrcMessageBuilderDataObject)
		{
			return new RaidNotification(fromIrcMessageBuilderDataObject.Message);
		}

		// Token: 0x040001FB RID: 507
		private readonly List<KeyValuePair<string, string>> _badges = new List<KeyValuePair<string, string>>();

		// Token: 0x040001FC RID: 508
		private readonly List<KeyValuePair<string, string>> _badgeInfo = new List<KeyValuePair<string, string>>();

		// Token: 0x040001FD RID: 509
		private string _color;

		// Token: 0x040001FE RID: 510
		private string _displayName;

		// Token: 0x040001FF RID: 511
		private string _emotes;

		// Token: 0x04000200 RID: 512
		private string _id;

		// Token: 0x04000201 RID: 513
		private bool _isModerator;

		// Token: 0x04000202 RID: 514
		private bool _isSubscriber;

		// Token: 0x04000203 RID: 515
		private bool _isTurbo;

		// Token: 0x04000204 RID: 516
		private string _login;

		// Token: 0x04000205 RID: 517
		private string _msgId;

		// Token: 0x04000206 RID: 518
		private string _msgParamDisplayName;

		// Token: 0x04000207 RID: 519
		private string _msgParamLogin;

		// Token: 0x04000208 RID: 520
		private string _msgParamViewerCount;

		// Token: 0x04000209 RID: 521
		private string _roomId;

		// Token: 0x0400020A RID: 522
		private string _systemMsg;

		// Token: 0x0400020B RID: 523
		private string _systemMsgParsed;

		// Token: 0x0400020C RID: 524
		private string _tmiSentTs;

		// Token: 0x0400020D RID: 525
		private string _userId;

		// Token: 0x0400020E RID: 526
		private UserType _userType;
	}
}
