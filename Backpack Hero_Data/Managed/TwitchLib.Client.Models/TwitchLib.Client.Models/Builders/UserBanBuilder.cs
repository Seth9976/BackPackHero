using System;

namespace TwitchLib.Client.Models.Builders
{
	// Token: 0x02000042 RID: 66
	public sealed class UserBanBuilder : IBuilder<UserBan>, IFromIrcMessageBuilder<UserBan>
	{
		// Token: 0x06000294 RID: 660 RVA: 0x00008D02 File Offset: 0x00006F02
		public static UserBanBuilder Create()
		{
			return new UserBanBuilder();
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00008D09 File Offset: 0x00006F09
		private UserBanBuilder()
		{
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00008D11 File Offset: 0x00006F11
		public UserBanBuilder WithChannel(string channel)
		{
			this._channelName = channel;
			return this;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00008D1B File Offset: 0x00006F1B
		public UserBanBuilder WithUserName(string userName)
		{
			this._userName = userName;
			return this;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00008D25 File Offset: 0x00006F25
		public UserBanBuilder WithBanReason(string banReason)
		{
			this._banReason = banReason;
			return this;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00008D2F File Offset: 0x00006F2F
		public UserBanBuilder WithRoomId(string roomId)
		{
			this._roomId = roomId;
			return this;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00008D39 File Offset: 0x00006F39
		public UserBanBuilder WithTargetUserId(string targetUserId)
		{
			this._targetUserId = targetUserId;
			return this;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00008D43 File Offset: 0x00006F43
		public UserBan BuildFromIrcMessage(FromIrcMessageBuilderDataObject fromIrcMessageBuilderDataObject)
		{
			return new UserBan(fromIrcMessageBuilderDataObject.Message);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00008D50 File Offset: 0x00006F50
		public UserBan Build()
		{
			return new UserBan(this._channelName, this._userName, this._banReason, this._roomId, this._targetUserId);
		}

		// Token: 0x04000234 RID: 564
		private string _channelName;

		// Token: 0x04000235 RID: 565
		private string _userName;

		// Token: 0x04000236 RID: 566
		private string _banReason;

		// Token: 0x04000237 RID: 567
		private string _roomId;

		// Token: 0x04000238 RID: 568
		private string _targetUserId;
	}
}
