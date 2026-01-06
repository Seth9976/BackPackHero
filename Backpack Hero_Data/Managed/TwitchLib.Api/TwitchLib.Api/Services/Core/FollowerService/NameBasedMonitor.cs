using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TwitchLib.Api.Helix.Models.Users.GetUserFollows;
using TwitchLib.Api.Interfaces;

namespace TwitchLib.Api.Services.Core.FollowerService
{
	// Token: 0x02000020 RID: 32
	internal class NameBasedMonitor : CoreMonitor
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x00003968 File Offset: 0x00001B68
		public NameBasedMonitor(ITwitchAPI api)
			: base(api)
		{
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003984 File Offset: 0x00001B84
		public override Task<GetUsersFollowsResponse> GetUsersFollowsAsync(string channel, int queryCount)
		{
			NameBasedMonitor.<GetUsersFollowsAsync>d__2 <GetUsersFollowsAsync>d__;
			<GetUsersFollowsAsync>d__.<>4__this = this;
			<GetUsersFollowsAsync>d__.channel = channel;
			<GetUsersFollowsAsync>d__.queryCount = queryCount;
			<GetUsersFollowsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<GetUsersFollowsResponse>.Create();
			<GetUsersFollowsAsync>d__.<>1__state = -1;
			<GetUsersFollowsAsync>d__.<>t__builder.Start<NameBasedMonitor.<GetUsersFollowsAsync>d__2>(ref <GetUsersFollowsAsync>d__);
			return <GetUsersFollowsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000039D7 File Offset: 0x00001BD7
		public void ClearCache()
		{
			this._channelToId.Clear();
		}

		// Token: 0x04000057 RID: 87
		private readonly ConcurrentDictionary<string, string> _channelToId = new ConcurrentDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
	}
}
