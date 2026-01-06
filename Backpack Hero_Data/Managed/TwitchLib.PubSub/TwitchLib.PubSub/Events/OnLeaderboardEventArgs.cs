using System;
using System.Collections.Generic;
using TwitchLib.PubSub.Models;

namespace TwitchLib.PubSub.Events
{
	// Token: 0x0200003F RID: 63
	public class OnLeaderboardEventArgs : EventArgs
	{
		// Token: 0x04000133 RID: 307
		public string ChannelId;

		// Token: 0x04000134 RID: 308
		public List<LeaderBoard> TopList;
	}
}
