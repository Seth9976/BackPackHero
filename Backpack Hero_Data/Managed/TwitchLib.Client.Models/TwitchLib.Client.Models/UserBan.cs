using System;
using TwitchLib.Client.Models.Internal;

namespace TwitchLib.Client.Models
{
	// Token: 0x0200001F RID: 31
	public class UserBan
	{
		// Token: 0x06000150 RID: 336 RVA: 0x00006B38 File Offset: 0x00004D38
		public UserBan(IrcMessage ircMessage)
		{
			this.Channel = ircMessage.Channel;
			this.Username = ircMessage.Message;
			string text;
			if (ircMessage.Tags.TryGetValue("ban-reason", ref text))
			{
				this.BanReason = text;
			}
			string text2;
			if (ircMessage.Tags.TryGetValue("room-id", ref text2))
			{
				this.RoomId = text2;
			}
			string text3;
			if (ircMessage.Tags.TryGetValue("target-user-id", ref text3))
			{
				this.TargetUserId = text3;
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00006BB4 File Offset: 0x00004DB4
		public UserBan(string channel, string username, string banReason, string roomId, string targetUserId)
		{
			this.Channel = channel;
			this.Username = username;
			this.BanReason = banReason;
			this.RoomId = roomId;
			this.TargetUserId = targetUserId;
		}

		// Token: 0x04000114 RID: 276
		public string BanReason;

		// Token: 0x04000115 RID: 277
		public string Channel;

		// Token: 0x04000116 RID: 278
		public string Username;

		// Token: 0x04000117 RID: 279
		public string RoomId;

		// Token: 0x04000118 RID: 280
		public string TargetUserId;
	}
}
