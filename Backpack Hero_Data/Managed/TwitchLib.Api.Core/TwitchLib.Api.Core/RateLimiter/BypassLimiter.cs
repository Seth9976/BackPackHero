using System;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchLib.Api.Core.RateLimiter
{
	// Token: 0x02000005 RID: 5
	public class BypassLimiter : IRateLimiter
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002BC7 File Offset: 0x00000DC7
		public Task Perform(Func<Task> perform)
		{
			return this.Perform(perform, CancellationToken.None);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002BD5 File Offset: 0x00000DD5
		public Task<T> Perform<T>(Func<Task<T>> perform)
		{
			return this.Perform<T>(perform, CancellationToken.None);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002BE3 File Offset: 0x00000DE3
		public Task Perform(Func<Task> perform, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			return perform.Invoke();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002BF2 File Offset: 0x00000DF2
		public Task<T> Perform<T>(Func<Task<T>> perform, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			return perform.Invoke();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002C01 File Offset: 0x00000E01
		private static Func<Task> Transform(Action act)
		{
			return delegate
			{
				act.Invoke();
				return Task.FromResult<int>(0);
			};
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002C1A File Offset: 0x00000E1A
		private static Func<Task<T>> Transform<T>(Func<T> compute)
		{
			return () => Task.FromResult<T>(compute.Invoke());
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002C34 File Offset: 0x00000E34
		public Task Perform(Action perform, CancellationToken cancellationToken)
		{
			Func<Task> func = BypassLimiter.Transform(perform);
			return this.Perform(func, cancellationToken);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002C50 File Offset: 0x00000E50
		public Task Perform(Action perform)
		{
			Func<Task> func = BypassLimiter.Transform(perform);
			return this.Perform(func);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002C6C File Offset: 0x00000E6C
		public Task<T> Perform<T>(Func<T> perform)
		{
			Func<Task<T>> func = BypassLimiter.Transform<T>(perform);
			return this.Perform<T>(func);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002C88 File Offset: 0x00000E88
		public Task<T> Perform<T>(Func<T> perform, CancellationToken cancellationToken)
		{
			Func<Task<T>> func = BypassLimiter.Transform<T>(perform);
			return this.Perform<T>(func, cancellationToken);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002CA4 File Offset: 0x00000EA4
		public static BypassLimiter CreateLimiterBypassInstance()
		{
			return new BypassLimiter();
		}
	}
}
