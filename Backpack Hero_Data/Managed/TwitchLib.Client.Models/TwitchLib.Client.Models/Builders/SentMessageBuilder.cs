using System;
using System.Collections.Generic;
using TwitchLib.Client.Enums;

namespace TwitchLib.Client.Models.Builders
{
	// Token: 0x0200003E RID: 62
	public sealed class SentMessageBuilder : IBuilder<SentMessage>
	{
		// Token: 0x06000220 RID: 544 RVA: 0x00008713 File Offset: 0x00006913
		private SentMessageBuilder()
		{
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00008726 File Offset: 0x00006926
		public SentMessageBuilder WithBadges(params KeyValuePair<string, string>[] badges)
		{
			this._badges.AddRange(badges);
			return this;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00008735 File Offset: 0x00006935
		public SentMessageBuilder WithChannel(string channel)
		{
			this._channel = channel;
			return this;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000873F File Offset: 0x0000693F
		public SentMessageBuilder WithColorHex(string colorHex)
		{
			this._colorHex = colorHex;
			return this;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00008749 File Offset: 0x00006949
		public SentMessageBuilder WithDisplayName(string displayName)
		{
			this._displayName = displayName;
			return this;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00008753 File Offset: 0x00006953
		public SentMessageBuilder WithEmoteSet(string emoteSet)
		{
			this._emoteSet = emoteSet;
			return this;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000875D File Offset: 0x0000695D
		public SentMessageBuilder WithIsModerator(bool isModerator)
		{
			this._isModerator = isModerator;
			return this;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00008767 File Offset: 0x00006967
		public SentMessageBuilder WithIsSubscriber(bool isSubscriber)
		{
			this._isSubscriber = isSubscriber;
			return this;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00008771 File Offset: 0x00006971
		public SentMessageBuilder WithMessage(string message)
		{
			this._message = message;
			return this;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000877B File Offset: 0x0000697B
		public SentMessageBuilder WithUserType(UserType userType)
		{
			this._userType = userType;
			return this;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00008785 File Offset: 0x00006985
		public static SentMessageBuilder Create()
		{
			return new SentMessageBuilder();
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000878C File Offset: 0x0000698C
		public SentMessage BuildFromUserState(UserState userState, string message)
		{
			return new SentMessage(userState, message);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00008798 File Offset: 0x00006998
		public SentMessage Build()
		{
			return new SentMessage(this._badges, this._channel, this._colorHex, this._displayName, this._emoteSet, this._isModerator, this._isSubscriber, this._userType, this._message);
		}

		// Token: 0x0400020F RID: 527
		private readonly List<KeyValuePair<string, string>> _badges = new List<KeyValuePair<string, string>>();

		// Token: 0x04000210 RID: 528
		private string _channel;

		// Token: 0x04000211 RID: 529
		private string _colorHex;

		// Token: 0x04000212 RID: 530
		private string _displayName;

		// Token: 0x04000213 RID: 531
		private string _emoteSet;

		// Token: 0x04000214 RID: 532
		private bool _isModerator;

		// Token: 0x04000215 RID: 533
		private bool _isSubscriber;

		// Token: 0x04000216 RID: 534
		private string _message;

		// Token: 0x04000217 RID: 535
		private UserType _userType;
	}
}
