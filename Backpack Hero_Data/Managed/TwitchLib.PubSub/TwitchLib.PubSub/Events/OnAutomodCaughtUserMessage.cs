using System;
using TwitchLib.PubSub.Models.Responses.Messages.UserModerationNotificationsTypes;

namespace TwitchLib.PubSub.Events
{
	// Token: 0x0200002F RID: 47
	public class OnAutomodCaughtUserMessage
	{
		// Token: 0x040000F0 RID: 240
		public AutomodCaughtMessage AutomodCaughtMessage;

		// Token: 0x040000F1 RID: 241
		public string ChannelId;

		// Token: 0x040000F2 RID: 242
		public string UserId;
	}
}
