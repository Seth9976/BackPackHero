using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Core.Models.Undocumented.Chatters;

namespace TwitchLib.Api.Core.Undocumented
{
	// Token: 0x02000004 RID: 4
	public class Undocumented : ApiBase
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002B22 File Offset: 0x00000D22
		public Undocumented(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
			: base(settings, rateLimiter, http)
		{
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002B30 File Offset: 0x00000D30
		[Obsolete("Please use the new official Helix GetChatters Endpoint (api.Helix.Chat.GetChattersAsync) instead of this undocumented and unsupported endpoint.")]
		public Task<List<ChatterFormatted>> GetChattersAsync(string channelName)
		{
			Undocumented.<GetChattersAsync>d__1 <GetChattersAsync>d__;
			<GetChattersAsync>d__.<>4__this = this;
			<GetChattersAsync>d__.channelName = channelName;
			<GetChattersAsync>d__.<>t__builder = AsyncTaskMethodBuilder<List<ChatterFormatted>>.Create();
			<GetChattersAsync>d__.<>1__state = -1;
			<GetChattersAsync>d__.<>t__builder.Start<Undocumented.<GetChattersAsync>d__1>(ref <GetChattersAsync>d__);
			return <GetChattersAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002B7C File Offset: 0x00000D7C
		public Task<bool> IsUsernameAvailableAsync(string username)
		{
			Undocumented.<IsUsernameAvailableAsync>d__2 <IsUsernameAvailableAsync>d__;
			<IsUsernameAvailableAsync>d__.<>4__this = this;
			<IsUsernameAvailableAsync>d__.username = username;
			<IsUsernameAvailableAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<IsUsernameAvailableAsync>d__.<>1__state = -1;
			<IsUsernameAvailableAsync>d__.<>t__builder.Start<Undocumented.<IsUsernameAvailableAsync>d__2>(ref <IsUsernameAvailableAsync>d__);
			return <IsUsernameAvailableAsync>d__.<>t__builder.Task;
		}
	}
}
