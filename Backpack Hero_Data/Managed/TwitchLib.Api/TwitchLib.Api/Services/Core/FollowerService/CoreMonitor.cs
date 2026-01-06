using System;
using System.Threading.Tasks;
using TwitchLib.Api.Helix.Models.Users.GetUserFollows;
using TwitchLib.Api.Interfaces;

namespace TwitchLib.Api.Services.Core.FollowerService
{
	// Token: 0x0200001E RID: 30
	internal abstract class CoreMonitor
	{
		// Token: 0x060000B0 RID: 176
		public abstract Task<GetUsersFollowsResponse> GetUsersFollowsAsync(string channel, int queryCount);

		// Token: 0x060000B1 RID: 177 RVA: 0x00003933 File Offset: 0x00001B33
		protected CoreMonitor(ITwitchAPI api)
		{
			this._api = api;
		}

		// Token: 0x04000056 RID: 86
		protected readonly ITwitchAPI _api;
	}
}
