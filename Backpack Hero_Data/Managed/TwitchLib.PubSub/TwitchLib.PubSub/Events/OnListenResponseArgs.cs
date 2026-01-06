using System;
using TwitchLib.PubSub.Models.Responses;

namespace TwitchLib.PubSub.Events
{
	// Token: 0x02000040 RID: 64
	public class OnListenResponseArgs : EventArgs
	{
		// Token: 0x04000135 RID: 309
		public string Topic;

		// Token: 0x04000136 RID: 310
		public Response Response;

		// Token: 0x04000137 RID: 311
		public bool Successful;

		// Token: 0x04000138 RID: 312
		public string ChannelId;
	}
}
