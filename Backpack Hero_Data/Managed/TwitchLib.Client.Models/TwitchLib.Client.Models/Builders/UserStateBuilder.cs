using System;
using System.Collections.Generic;
using TwitchLib.Client.Enums;

namespace TwitchLib.Client.Models.Builders
{
	// Token: 0x02000043 RID: 67
	public sealed class UserStateBuilder : IBuilder<UserState>, IFromIrcMessageBuilder<UserState>
	{
		// Token: 0x0600029D RID: 669 RVA: 0x00008D75 File Offset: 0x00006F75
		private UserStateBuilder()
		{
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00008D93 File Offset: 0x00006F93
		public UserStateBuilder WithBadges(params KeyValuePair<string, string>[] badges)
		{
			this._badges.AddRange(badges);
			return this;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00008DA2 File Offset: 0x00006FA2
		public UserStateBuilder WithBadgeInfos(params KeyValuePair<string, string>[] badgeInfos)
		{
			this._badgeInfo.AddRange(badgeInfos);
			return this;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00008DB1 File Offset: 0x00006FB1
		public UserStateBuilder WithChannel(string channel)
		{
			this._channel = channel;
			return this;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00008DBB File Offset: 0x00006FBB
		public UserStateBuilder WithColorHex(string olorHex)
		{
			this._colorHex = olorHex;
			return this;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00008DC5 File Offset: 0x00006FC5
		public UserStateBuilder WithDisplayName(string displayName)
		{
			this._displayName = displayName;
			return this;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00008DCF File Offset: 0x00006FCF
		public UserStateBuilder WithEmoteSet(string emoteSet)
		{
			this._emoteSet = emoteSet;
			return this;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00008DD9 File Offset: 0x00006FD9
		public UserStateBuilder Id(string id)
		{
			this._id = id;
			return this;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00008DE3 File Offset: 0x00006FE3
		public UserStateBuilder WithIsModerator(bool isModerator)
		{
			this._isModerator = isModerator;
			return this;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00008DED File Offset: 0x00006FED
		public UserStateBuilder WithIsSubscriber(bool isSubscriber)
		{
			this._isSubscriber = isSubscriber;
			return this;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00008DF7 File Offset: 0x00006FF7
		public UserStateBuilder WithUserType(UserType userType)
		{
			this._userType = userType;
			return this;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00008E01 File Offset: 0x00007001
		public static UserStateBuilder Create()
		{
			return new UserStateBuilder();
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00008E08 File Offset: 0x00007008
		public UserState BuildFromIrcMessage(FromIrcMessageBuilderDataObject fromIrcMessageBuilderDataObject)
		{
			return new UserState(fromIrcMessageBuilderDataObject.Message);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00008E18 File Offset: 0x00007018
		public UserState Build()
		{
			return new UserState(this._badges, this._badgeInfo, this._colorHex, this._displayName, this._emoteSet, this._channel, this._id, this._isSubscriber, this._isModerator, this._userType);
		}

		// Token: 0x04000239 RID: 569
		private readonly List<KeyValuePair<string, string>> _badges = new List<KeyValuePair<string, string>>();

		// Token: 0x0400023A RID: 570
		private readonly List<KeyValuePair<string, string>> _badgeInfo = new List<KeyValuePair<string, string>>();

		// Token: 0x0400023B RID: 571
		private string _channel;

		// Token: 0x0400023C RID: 572
		private string _colorHex;

		// Token: 0x0400023D RID: 573
		private string _displayName;

		// Token: 0x0400023E RID: 574
		private string _emoteSet;

		// Token: 0x0400023F RID: 575
		private string _id;

		// Token: 0x04000240 RID: 576
		private bool _isModerator;

		// Token: 0x04000241 RID: 577
		private bool _isSubscriber;

		// Token: 0x04000242 RID: 578
		private UserType _userType;
	}
}
