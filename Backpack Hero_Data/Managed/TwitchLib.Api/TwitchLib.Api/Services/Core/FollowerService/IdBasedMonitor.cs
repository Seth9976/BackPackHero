using System;
using System.Threading.Tasks;
using TwitchLib.Api.Helix.Models.Users.GetUserFollows;
using TwitchLib.Api.Interfaces;

namespace TwitchLib.Api.Services.Core.FollowerService
{
	// Token: 0x0200001F RID: 31
	internal class IdBasedMonitor : CoreMonitor
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x00003942 File Offset: 0x00001B42
		public IdBasedMonitor(ITwitchAPI api)
			: base(api)
		{
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000394B File Offset: 0x00001B4B
		public override Task<GetUsersFollowsResponse> GetUsersFollowsAsync(string channel, int queryCount)
		{
			return this._api.Helix.Users.GetUsersFollowsAsync(null, null, queryCount, null, channel, null);
		}
	}
}
