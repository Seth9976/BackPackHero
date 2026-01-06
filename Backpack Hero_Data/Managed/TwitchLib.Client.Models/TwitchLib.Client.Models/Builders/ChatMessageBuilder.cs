using System;
using System.Collections.Generic;
using TwitchLib.Client.Enums;

namespace TwitchLib.Client.Models.Builders
{
	// Token: 0x0200002D RID: 45
	public sealed class ChatMessageBuilder : IBuilder<ChatMessage>
	{
		// Token: 0x06000193 RID: 403 RVA: 0x00007DCD File Offset: 0x00005FCD
		private ChatMessageBuilder()
		{
			this._twitchLibMessage = TwitchLibMessageBuilder.Create().Build();
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00007DF0 File Offset: 0x00005FF0
		public ChatMessageBuilder WithTwitchLibMessage(TwitchLibMessage twitchLibMessage)
		{
			this._twitchLibMessage = twitchLibMessage;
			return this;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00007DFA File Offset: 0x00005FFA
		public ChatMessageBuilder WithBadgeInfos(params KeyValuePair<string, string>[] badgeInfos)
		{
			this.BadgeInfo.AddRange(badgeInfos);
			return this;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00007E09 File Offset: 0x00006009
		public ChatMessageBuilder WithBits(int bits)
		{
			this._bits = bits;
			return this;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00007E13 File Offset: 0x00006013
		public ChatMessageBuilder WithBitsInDollars(double bitsInDollars)
		{
			this._bitsInDollars = bitsInDollars;
			return this;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00007E1D File Offset: 0x0000601D
		public ChatMessageBuilder WithChannel(string channel)
		{
			this._channel = channel;
			return this;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00007E27 File Offset: 0x00006027
		public ChatMessageBuilder WithCheerBadge(CheerBadge cheerBadge)
		{
			this._cheerBadge = cheerBadge;
			return this;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00007E31 File Offset: 0x00006031
		public ChatMessageBuilder WithEmoteReplaceMessage(string emoteReplaceMessage)
		{
			this._emoteReplacedMessage = emoteReplaceMessage;
			return this;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00007E3B File Offset: 0x0000603B
		public ChatMessageBuilder WithId(string id)
		{
			this._id = id;
			return this;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00007E45 File Offset: 0x00006045
		public ChatMessageBuilder WithIsBroadcaster(bool isBroadcaster)
		{
			this._isBroadcaster = isBroadcaster;
			return this;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00007E4F File Offset: 0x0000604F
		public ChatMessageBuilder WithIsMe(bool isMe)
		{
			this._isMe = isMe;
			return this;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00007E59 File Offset: 0x00006059
		public ChatMessageBuilder WithIsModerator(bool isModerator)
		{
			this._isModerator = isModerator;
			return this;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00007E63 File Offset: 0x00006063
		public ChatMessageBuilder WithIsSubscriber(bool isSubscriber)
		{
			this._isSubscriber = isSubscriber;
			return this;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00007E6D File Offset: 0x0000606D
		public ChatMessageBuilder WithIsVip(bool isVip)
		{
			this._isVip = isVip;
			return this;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00007E77 File Offset: 0x00006077
		public ChatMessageBuilder WithIsStaff(bool isStaff)
		{
			this._isStaff = isStaff;
			return this;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00007E81 File Offset: 0x00006081
		public ChatMessageBuilder WithIsPartner(bool isPartner)
		{
			this._isPartner = isPartner;
			return this;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00007E8B File Offset: 0x0000608B
		public ChatMessageBuilder WithMessage(string message)
		{
			this._message = message;
			return this;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00007E95 File Offset: 0x00006095
		public ChatMessageBuilder WithNoisy(Noisy noisy)
		{
			this._noisy = noisy;
			return this;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00007E9F File Offset: 0x0000609F
		public ChatMessageBuilder WithRawIrcMessage(string rawIrcMessage)
		{
			this._rawIrcMessage = rawIrcMessage;
			return this;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00007EA9 File Offset: 0x000060A9
		public ChatMessageBuilder WithRoomId(string roomId)
		{
			this._roomId = roomId;
			return this;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00007EB3 File Offset: 0x000060B3
		public ChatMessageBuilder WithSubscribedMonthCount(int subscribedMonthCount)
		{
			this._subscribedMonthCount = subscribedMonthCount;
			return this;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00007EBD File Offset: 0x000060BD
		public static ChatMessageBuilder Create()
		{
			return new ChatMessageBuilder();
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00007EC4 File Offset: 0x000060C4
		public ChatMessage Build()
		{
			return new ChatMessage(this._twitchLibMessage.BotUsername, this._twitchLibMessage.UserId, this._twitchLibMessage.Username, this._twitchLibMessage.DisplayName, this._twitchLibMessage.ColorHex, this._twitchLibMessage.Color, this._twitchLibMessage.EmoteSet, this._message, this._twitchLibMessage.UserType, this._channel, this._id, this._isSubscriber, this._subscribedMonthCount, this._roomId, this._twitchLibMessage.IsTurbo, this._isModerator, this._isMe, this._isBroadcaster, this._isVip, this._isPartner, this._isStaff, this._noisy, this._rawIrcMessage, this._emoteReplacedMessage, this._twitchLibMessage.Badges, this._cheerBadge, this._bits, this._bitsInDollars);
		}

		// Token: 0x040001B4 RID: 436
		private TwitchLibMessage _twitchLibMessage;

		// Token: 0x040001B5 RID: 437
		private readonly List<KeyValuePair<string, string>> BadgeInfo = new List<KeyValuePair<string, string>>();

		// Token: 0x040001B6 RID: 438
		private int _bits;

		// Token: 0x040001B7 RID: 439
		private double _bitsInDollars;

		// Token: 0x040001B8 RID: 440
		private string _channel;

		// Token: 0x040001B9 RID: 441
		private CheerBadge _cheerBadge;

		// Token: 0x040001BA RID: 442
		private string _emoteReplacedMessage;

		// Token: 0x040001BB RID: 443
		private string _id;

		// Token: 0x040001BC RID: 444
		private bool _isBroadcaster;

		// Token: 0x040001BD RID: 445
		private bool _isMe;

		// Token: 0x040001BE RID: 446
		private bool _isModerator;

		// Token: 0x040001BF RID: 447
		private bool _isSubscriber;

		// Token: 0x040001C0 RID: 448
		private bool _isVip;

		// Token: 0x040001C1 RID: 449
		private bool _isStaff;

		// Token: 0x040001C2 RID: 450
		private bool _isPartner;

		// Token: 0x040001C3 RID: 451
		private string _message;

		// Token: 0x040001C4 RID: 452
		private Noisy _noisy;

		// Token: 0x040001C5 RID: 453
		private string _rawIrcMessage;

		// Token: 0x040001C6 RID: 454
		private string _roomId;

		// Token: 0x040001C7 RID: 455
		private int _subscribedMonthCount;
	}
}
