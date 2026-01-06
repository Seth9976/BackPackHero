using System;
using TwitchLib.Client.Models.Internal;

namespace TwitchLib.Client.Models
{
	// Token: 0x02000021 RID: 33
	public class UserTimeout
	{
		// Token: 0x0600015E RID: 350 RVA: 0x00006F8C File Offset: 0x0000518C
		public UserTimeout(IrcMessage ircMessage)
		{
			this.Channel = ircMessage.Channel;
			this.Username = ircMessage.Message;
			foreach (string text in ircMessage.Tags.Keys)
			{
				string text2 = ircMessage.Tags[text];
				if (text != null)
				{
					if (!(text == "ban-duration"))
					{
						if (!(text == "ban-reason"))
						{
							if (text == "target-user-id")
							{
								this.TargetUserId = text2;
							}
						}
						else
						{
							this.TimeoutReason = text2;
						}
					}
					else
					{
						this.TimeoutDuration = int.Parse(text2);
					}
				}
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00007054 File Offset: 0x00005254
		public UserTimeout(string channel, string username, string targetuserId, int timeoutDuration, string timeoutReason)
		{
			this.Channel = channel;
			this.Username = username;
			this.TargetUserId = targetuserId;
			this.TimeoutDuration = timeoutDuration;
			this.TimeoutReason = timeoutReason;
		}

		// Token: 0x04000123 RID: 291
		public string Channel;

		// Token: 0x04000124 RID: 292
		public int TimeoutDuration;

		// Token: 0x04000125 RID: 293
		public string TimeoutReason;

		// Token: 0x04000126 RID: 294
		public string Username;

		// Token: 0x04000127 RID: 295
		public string TargetUserId;
	}
}
