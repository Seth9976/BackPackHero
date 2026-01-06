using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchLib.Api.Core.RateLimiter
{
	// Token: 0x02000007 RID: 7
	public class CountByIntervalAwaitableConstraint : IAwaitableConstraint
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002D23 File Offset: 0x00000F23
		public IReadOnlyList<DateTime> TimeStamps
		{
			get
			{
				return Enumerable.ToList<DateTime>(this._timeStamps);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002D30 File Offset: 0x00000F30
		protected LimitedSizeStack<DateTime> _timeStamps { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002D38 File Offset: 0x00000F38
		private int _count { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002D40 File Offset: 0x00000F40
		private TimeSpan _timeSpan { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002D48 File Offset: 0x00000F48
		private SemaphoreSlim _semafore { get; } = new SemaphoreSlim(1, 1);

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002D50 File Offset: 0x00000F50
		private ITime _time { get; }

		// Token: 0x0600003D RID: 61 RVA: 0x00002D58 File Offset: 0x00000F58
		public CountByIntervalAwaitableConstraint(int count, TimeSpan timeSpan, ITime time = null)
		{
			if (count <= 0)
			{
				throw new ArgumentException("count should be strictly positive", "count");
			}
			if (timeSpan.TotalMilliseconds <= 0.0)
			{
				throw new ArgumentException("timeSpan should be strictly positive", "timeSpan");
			}
			this._count = count;
			this._timeSpan = timeSpan;
			this._timeStamps = new LimitedSizeStack<DateTime>(this._count);
			this._time = time ?? TimeSystem.StandardTime;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002DE0 File Offset: 0x00000FE0
		public Task<IDisposable> WaitForReadiness(CancellationToken cancellationToken)
		{
			CountByIntervalAwaitableConstraint.<WaitForReadiness>d__18 <WaitForReadiness>d__;
			<WaitForReadiness>d__.<>4__this = this;
			<WaitForReadiness>d__.cancellationToken = cancellationToken;
			<WaitForReadiness>d__.<>t__builder = AsyncTaskMethodBuilder<IDisposable>.Create();
			<WaitForReadiness>d__.<>1__state = -1;
			<WaitForReadiness>d__.<>t__builder.Start<CountByIntervalAwaitableConstraint.<WaitForReadiness>d__18>(ref <WaitForReadiness>d__);
			return <WaitForReadiness>d__.<>t__builder.Task;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002E2C File Offset: 0x0000102C
		private void OnEnded()
		{
			DateTime timeNow = this._time.GetTimeNow();
			this._timeStamps.Push(timeNow);
			this.OnEnded(timeNow);
			this._semafore.Release();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002E64 File Offset: 0x00001064
		protected virtual void OnEnded(DateTime now)
		{
		}
	}
}
