using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Api.Core.Extensions.RateLimiter;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchLib.Api.Core.RateLimiter
{
	// Token: 0x0200000B RID: 11
	public class TimeLimiter : IRateLimiter
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00002F2E File Offset: 0x0000112E
		internal TimeLimiter(IAwaitableConstraint ac)
		{
			this._ac = ac;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002F3D File Offset: 0x0000113D
		public Task Perform(Func<Task> perform)
		{
			return this.Perform(perform, CancellationToken.None);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002F4B File Offset: 0x0000114B
		public Task<T> Perform<T>(Func<Task<T>> perform)
		{
			return this.Perform<T>(perform, CancellationToken.None);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002F5C File Offset: 0x0000115C
		public Task Perform(Func<Task> perform, CancellationToken cancellationToken)
		{
			TimeLimiter.<Perform>d__4 <Perform>d__;
			<Perform>d__.<>4__this = this;
			<Perform>d__.perform = perform;
			<Perform>d__.cancellationToken = cancellationToken;
			<Perform>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<Perform>d__.<>1__state = -1;
			<Perform>d__.<>t__builder.Start<TimeLimiter.<Perform>d__4>(ref <Perform>d__);
			return <Perform>d__.<>t__builder.Task;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002FB0 File Offset: 0x000011B0
		public Task<T> Perform<T>(Func<Task<T>> perform, CancellationToken cancellationToken)
		{
			TimeLimiter.<Perform>d__5<T> <Perform>d__;
			<Perform>d__.<>4__this = this;
			<Perform>d__.perform = perform;
			<Perform>d__.cancellationToken = cancellationToken;
			<Perform>d__.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
			<Perform>d__.<>1__state = -1;
			<Perform>d__.<>t__builder.Start<TimeLimiter.<Perform>d__5<T>>(ref <Perform>d__);
			return <Perform>d__.<>t__builder.Task;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003003 File Offset: 0x00001203
		private static Func<Task> Transform(Action act)
		{
			return delegate
			{
				act.Invoke();
				return Task.FromResult<int>(0);
			};
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000301C File Offset: 0x0000121C
		private static Func<Task<T>> Transform<T>(Func<T> compute)
		{
			return () => Task.FromResult<T>(compute.Invoke());
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003038 File Offset: 0x00001238
		public Task Perform(Action perform, CancellationToken cancellationToken)
		{
			Func<Task> func = TimeLimiter.Transform(perform);
			return this.Perform(func, cancellationToken);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003054 File Offset: 0x00001254
		public Task Perform(Action perform)
		{
			Func<Task> func = TimeLimiter.Transform(perform);
			return this.Perform(func);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003070 File Offset: 0x00001270
		public Task<T> Perform<T>(Func<T> perform)
		{
			Func<Task<T>> func = TimeLimiter.Transform<T>(perform);
			return this.Perform<T>(func);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000308C File Offset: 0x0000128C
		public Task<T> Perform<T>(Func<T> perform, CancellationToken cancellationToken)
		{
			Func<Task<T>> func = TimeLimiter.Transform<T>(perform);
			return this.Perform<T>(func, cancellationToken);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000030A8 File Offset: 0x000012A8
		public static TimeLimiter GetFromMaxCountByInterval(int maxCount, TimeSpan timeSpan)
		{
			return new TimeLimiter(new CountByIntervalAwaitableConstraint(maxCount, timeSpan, null));
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000030B7 File Offset: 0x000012B7
		public static TimeLimiter GetPersistentTimeLimiter(int maxCount, TimeSpan timeSpan, Action<DateTime> saveStateAction)
		{
			return TimeLimiter.GetPersistentTimeLimiter(maxCount, timeSpan, saveStateAction, null);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000030C2 File Offset: 0x000012C2
		public static TimeLimiter GetPersistentTimeLimiter(int maxCount, TimeSpan timeSpan, Action<DateTime> saveStateAction, IEnumerable<DateTime> initialTimeStamps)
		{
			return new TimeLimiter(new PersistentCountByIntervalAwaitableConstraint(maxCount, timeSpan, saveStateAction, initialTimeStamps, null));
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000030D4 File Offset: 0x000012D4
		public static TimeLimiter Compose(params IAwaitableConstraint[] constraints)
		{
			IAwaitableConstraint awaitableConstraint = null;
			foreach (IAwaitableConstraint awaitableConstraint2 in constraints)
			{
				awaitableConstraint = ((awaitableConstraint == null) ? awaitableConstraint2 : awaitableConstraint.Compose(awaitableConstraint2));
			}
			return new TimeLimiter(awaitableConstraint);
		}

		// Token: 0x0400001C RID: 28
		private readonly IAwaitableConstraint _ac;
	}
}
