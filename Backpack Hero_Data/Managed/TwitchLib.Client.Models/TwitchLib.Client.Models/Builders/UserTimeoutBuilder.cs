using System;

namespace TwitchLib.Client.Models.Builders
{
	// Token: 0x02000044 RID: 68
	public sealed class UserTimeoutBuilder : IBuilder<UserTimeout>, IFromIrcMessageBuilder<UserTimeout>
	{
		// Token: 0x060002AB RID: 683 RVA: 0x00008E66 File Offset: 0x00007066
		private UserTimeoutBuilder()
		{
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00008E6E File Offset: 0x0000706E
		public UserTimeoutBuilder WithChannel(string channel)
		{
			this._channel = channel;
			return this;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00008E78 File Offset: 0x00007078
		public UserTimeoutBuilder WithTimeoutDuration(int timeoutDuration)
		{
			this._timeoutDuration = timeoutDuration;
			return this;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00008E82 File Offset: 0x00007082
		public UserTimeoutBuilder WithTimeoutReason(string timeoutReason)
		{
			this._timeoutReason = timeoutReason;
			return this;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00008E8C File Offset: 0x0000708C
		public UserTimeoutBuilder WithUsername(string username)
		{
			this._username = username;
			return this;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00008E96 File Offset: 0x00007096
		public UserTimeoutBuilder WithTargetUserId(string targetUserId)
		{
			this._targetUserId = targetUserId;
			return this;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00008EA0 File Offset: 0x000070A0
		public static UserTimeoutBuilder Create()
		{
			return new UserTimeoutBuilder();
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00008EA7 File Offset: 0x000070A7
		public UserTimeout BuildFromIrcMessage(FromIrcMessageBuilderDataObject fromIrcMessageBuilderDataObject)
		{
			return new UserTimeout(fromIrcMessageBuilderDataObject.Message);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00008EB4 File Offset: 0x000070B4
		public UserTimeout Build()
		{
			return new UserTimeout(this._channel, this._username, this._targetUserId, this._timeoutDuration, this._timeoutReason);
		}

		// Token: 0x04000243 RID: 579
		private string _channel;

		// Token: 0x04000244 RID: 580
		private int _timeoutDuration;

		// Token: 0x04000245 RID: 581
		private string _timeoutReason;

		// Token: 0x04000246 RID: 582
		private string _username;

		// Token: 0x04000247 RID: 583
		private string _targetUserId;
	}
}
