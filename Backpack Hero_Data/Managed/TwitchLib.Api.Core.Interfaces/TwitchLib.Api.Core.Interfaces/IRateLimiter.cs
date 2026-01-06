using System;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchLib.Api.Core.Interfaces
{
	// Token: 0x02000007 RID: 7
	public interface IRateLimiter
	{
		// Token: 0x06000019 RID: 25
		Task Perform(Func<Task> perform, CancellationToken cancellationToken);

		// Token: 0x0600001A RID: 26
		Task Perform(Func<Task> perform);

		// Token: 0x0600001B RID: 27
		Task<T> Perform<T>(Func<Task<T>> perform);

		// Token: 0x0600001C RID: 28
		Task<T> Perform<T>(Func<Task<T>> perform, CancellationToken cancellationToken);

		// Token: 0x0600001D RID: 29
		Task Perform(Action perform, CancellationToken cancellationToken);

		// Token: 0x0600001E RID: 30
		Task Perform(Action perform);

		// Token: 0x0600001F RID: 31
		Task<T> Perform<T>(Func<T> perform);

		// Token: 0x06000020 RID: 32
		Task<T> Perform<T>(Func<T> perform, CancellationToken cancellationToken);
	}
}
