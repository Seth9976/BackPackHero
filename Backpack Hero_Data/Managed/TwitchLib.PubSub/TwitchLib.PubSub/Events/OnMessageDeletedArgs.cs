using System;

namespace TwitchLib.PubSub.Events
{
	// Token: 0x02000042 RID: 66
	public class OnMessageDeletedArgs : EventArgs
	{
		// Token: 0x0400013A RID: 314
		public string TargetUser;

		// Token: 0x0400013B RID: 315
		public string TargetUserId;

		// Token: 0x0400013C RID: 316
		public string DeletedBy;

		// Token: 0x0400013D RID: 317
		public string DeletedByUserId;

		// Token: 0x0400013E RID: 318
		public string Message;

		// Token: 0x0400013F RID: 319
		public string MessageId;

		// Token: 0x04000140 RID: 320
		public string ChannelId;
	}
}
