using System;
using System.Collections.Generic;
using TwitchLib.Api.Helix.Models.Users.GetUserFollows;

namespace TwitchLib.Api.Services.Events.FollowerService
{
	// Token: 0x02000019 RID: 25
	public class OnNewFollowersDetectedArgs : EventArgs
	{
		// Token: 0x04000050 RID: 80
		public string Channel;

		// Token: 0x04000051 RID: 81
		public List<Follow> NewFollowers;
	}
}
